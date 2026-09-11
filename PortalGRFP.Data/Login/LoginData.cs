using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using PortalGRFP.Data.Extensions;
using PortalGRFP.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PortalGRFP.Data.Login
{
    public class LoginData : DBContext
    {
        public UsuarioModel Login(string correo)
        {
            UsuarioModel response = null;
            var command = Context.GetStoredProcCommand("SP_GRFP_CAT_USUARIO_GET");
            command.Parameters.Add(new SqlParameter("@Correo", correo));
            using (IDataReader dr = Context.ExecuteReader(command))
            {
                while (dr.Read())
                {
                    response = new UsuarioModel();
                    response.IdUsuario = dr.Get<int>("IdUsuario");
                    response.IdPerfil = dr.Get<int>("IdPerfil");
                    response.Nombre = dr.Get<string>("Nombre");
                    response.Perfil = dr.Get<string>("Perfil");
                    response.Correo = dr.Get<string>("Correo");
                }
                if (dr.NextResult() && response != null)
                {
                    response.Permisos = new List<ModuloModel>();
                    response.Guards = new Dictionary<int, List<string>>();
                    while (dr.Read())
                    {
                        response.Permisos.Add(new ModuloModel
                        {
                            IdModulo = dr.Get<int>("IdModulo"),
                            Modulo = dr.Get<string>("Modulo"),
                            IconoBase = dr.Get<string>("IconoBase"),
                            Componente = dr.Get<string>("Componente"),
                            Orden = dr.Get<int>("Orden"),
                            SubModulos = new List<SubModuloModel>(),
                            SubModuloHijos = new List<ModuloModel>()
                        });
                    }
                }
                if (dr.NextResult() && response != null)
                {
                    var subMo = new List<SubAux>();
                    while (dr.Read())
                    {
                        subMo.Add(new SubAux
                        {
                            IdModulo = dr.Get<int>("IdModulo"),
                            IdSubModulo = dr.Get<int>("IdSubModulo"),
                            IconoBase = dr.Get<string>("IconoBase"),
                            Orden = dr.Get<int>("Orden"),
                            SubModulo = dr.Get<string>("SubModulo"),
                            Componente = dr.Get<string>("Componente"),
                            IdAccion = dr.Get<int>("IdAccion"),
                            Accion = dr.Get<string>("Accion")
                        });
                    }
                    if (subMo.Count > 0)
                    {
                        int idTem = 0;
                        subMo.ForEach(smo =>
                        {
                            if (response.Permisos.Find(f => f.IdModulo == smo.IdModulo) != null)
                            {
                                if (smo.IdSubModulo != idTem)
                                {
                                    response.Permisos.Find(f => f.IdModulo == smo.IdModulo)
                                    .SubModulos.Add(new SubModuloModel
                                    {
                                        IdSubModulo = smo.IdSubModulo,
                                        SubModulo = smo.SubModulo,
                                        IconoBase = smo.IconoBase,
                                        Componente = smo.Componente,
                                        Orden = smo.Orden,
                                        Acciones = new Dictionary<int, string> { { smo.IdAccion, smo.Accion } }
                                    });
                                }
                                else
                                {
                                    response.Permisos.Find(f => f.IdModulo == smo.IdModulo)
                                    .SubModulos.Find(s => s.IdSubModulo == idTem)
                                    .Acciones.Add(smo.IdAccion, smo.Accion);
                                }
                                idTem = smo.IdSubModulo;
                            }
                        });

                        if (response.Permisos != null && response.Permisos.Count > 0) {
                            response.Permisos.ForEach(p =>
                            {
                                SetPermisos(p.IdModulo, p, subMo);
                            });
                            SetGuards(response, subMo);
                        }
                    }
                }
            }
            if (response == null)
            {
                throw new Exception($"{correo} no es un usuario en la base de datos ó se encuentra sin permisos de acceso.");
            }
            return response;
        }
        /// <summary>
        /// Metodo para setear permisos util para implementar
        /// en un Guardian de sesion
        /// </summary>
        /// <param name="destino">Usuario</param>
        /// <param name="origen">Permisos</param>
        private void SetGuards(UsuarioModel destino, List<SubAux> origen)
        {
            if (destino != null && destino.Guards != null)
            {
                var group = origen.GroupBy(g => g.IdModulo,
                    (key, s) => new
                    {
                        IdPadre = key,
                        G = s.Select(x => $"{x.IdSubModulo}:{x.SubModulo};{x.IdAccion}:{x.Accion}").ToList()
                    });
                if (group != null) {
                    group.ForEach(key => { destino.Guards.Add(key.IdPadre, key.G); });
                }
            }
        }
        /// <summary>
        /// Metodo recursivo para menú de N dimensionces
        /// </summary>
        /// <param name="idPadre">Identificador unico de modulo padre</param>
        /// <param name="destino">Modulo actual</param>
        /// <param name="origen">Lista de submodulos auxiliar</param>
        private void SetPermisos(int idPadre, ModuloModel destino, List<SubAux> origen)
        {
            if (destino != null)
            {
                var subMs = origen.Where(f => f.IdModulo.Equals(idPadre)).ToList();
                int idSub = 0;
                subMs.ForEach(sm =>
                {
                    if (!idSub.Equals(sm.IdSubModulo))
                    {
                        destino.SubModuloHijos.Add(new ModuloModel
                        {
                            IconoBase = sm.IconoBase,
                            Componente = sm.Componente,
                            IdModuloPadre = sm.IdModulo,
                            IdModulo = sm.IdSubModulo,
                            Orden = sm.Orden,
                            Modulo = sm.SubModulo,
                            SubModuloHijos = new List<ModuloModel>(),
                            Acciones = new Dictionary<int, string> { { sm.IdAccion, sm.Accion } }
                        });

                        SetPermisos(sm.IdSubModulo, destino.SubModuloHijos
                        .Find(f => f.IdModulo.Equals(sm.IdSubModulo)), origen);
                    }
                    else
                    {
                        destino.SubModuloHijos
                        .Find(f => f.IdModulo.Equals(idSub))
                        .Acciones.Add(sm.IdAccion, sm.Accion);
                    }
                    idSub = sm.IdSubModulo;
                });
            }
        }
    }
    /// <summary>
    /// Clase local auxiliar
    /// </summary>
    class SubAux : ModuloBaseModel
    {
        public int IdModulo { get; set; }
        public int IdSubModulo { get; set; }
        public int IdAccion { get; set; }
        public string SubModulo { get; set; }
        public string Accion { get; set; }
    }
}