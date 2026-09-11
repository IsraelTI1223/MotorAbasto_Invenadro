using PortalGRFP.Business.Users;
using PortalGRFP.Entities.Models;
using PortalGRFP.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalGRFP.Business.Mantenimiento;


namespace PortalGRFP.Controllers
{
    public class UserController : Controller
    {
        
        // GET: User
        public ActionResult Index()
        {
            var usuario = this.GetUsuario();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var pp = usuario.Permisos.ToList().Where(x => x.Modulo == "Configuraciones");
            var per = pp.FirstOrDefault().SubModulos.ToList();
            var action = per.FirstOrDefault().Acciones.ToList();

            foreach(var item in action)
            {
                if (item.Value.Equals("Actualizar") && Session["UsuarioActualiza"] == null)
                {
                    Session["UsuarioActualiza"] = item.Value;
                }
                
                if (item.Value.Equals("Registrar") && Session["UsuarioRegistra"] == null)
                {
                    Session["UsuarioRegistra"] = item.Value;
                }
                
                if (item.Value.Equals("Consultar") && ViewBag.PermisoConsulta == null)
                {
                    ViewBag.PermisoConsulta = "Consultar";
                }
                
                if (Session["UsuarioRegistra"] != null && Session["UsuarioActualiza"] != null && ViewBag.PermisoConsulta != null)
                {
                    break;
                }
                
            }
            if (Session["UsuarioRegistra"] == null)
            {
                Session["UsuarioRegistra"] = "Sin Permisos";
            }
            if (Session["UsuarioActualiza"] == null)
            {
                Session["UsuarioActualiza"] = "Sin Permisos";
            }
            if(ViewBag.PermisoConsulta == null)
            {
                ViewBag.PermisoConsulta = "Sin Permisos";
            }


            UserConfiguration getUsers = new UserConfiguration();
            var list = getUsers.GetUsuarios().OrderBy(x=> x.Nombre).ToList();


            return View(list);
        }

        public ActionResult Crud(int id = 0)
        {
            var usuario = this.GetUsuario();
            UserConfiguration UsersC = new UserConfiguration();
            var perfiles = UsersC.GetPerfilAll().OrderBy(x => x.Nombre).ToList();
            
            TempData["gruposList"] = new GruposBLL().GetGruposBLLUsuario(1, 0).ToList();
            ///MultiSelectList m = new MultiSelectList


            if (id == 0)
            {
                ViewBag.Perfil = new SelectList(perfiles, "IdPerfil", "Nombre");

               // ViewBag.Grupo = new SelectList(grupos, "Id", "Nombre");

                return View();
            }
            else
            {
                var user = UsersC.GetUsuarios().Where(x => x.IdUsusario == id);
                var datos = user.First();
                ViewBag.Perfil = new SelectList(perfiles, "IdPerfil", "Nombre", datos.idPerfil);
               //ViewBag.Grupo = new SelectList(grupos, "Id", "Nombre");

                return View(datos);
            }
        }

        //[HttpPost]
        //public ActionResult Crud(CatUsersModel model, string Perfil)
        public ActionResult CrudD(FormCollection data)
        {
            
            CatUsersModel oUser = new CatUsersModel();

            

            oUser.UsuarioALta = this.GetUsuario().IdUsuario;
            oUser.FechaAlta = DateTime.Now;

            if (data["IdUsusario"] == null)
            {
                oUser.IdUsusario = 0;
            }else
            {
                oUser.IdUsusario = int.Parse(data["IdUsusario"].ToString());
            }
        
            oUser.Nombre = data["Nombre"].ToString();
            oUser.ApPaterno = data["ApPaterno"].ToString();
            oUser.ApMaterno = data["ApMaterno"].ToString();
            oUser.Correo = data["Correo"].ToString();
            oUser.Perfil = data["Perfil"].ToString();
            oUser.Activo = Convert.ToBoolean(data["Activo"].Split(',')[0]);

            //var bandera = data["Activo"].ToString();

            //if (data["Activo"].ToString() == "true")
            //{
            //    oUser.Activo = true;
            //}
            //else
            //{
            //    oUser.Activo = false;
            //}

            var listGrupos = new List<CatUsersModel.Grupo>();
            
            if(data["Grupos"] != null)
            {
                var grupos = data["Grupos"].ToList();

                var gruposL = data["Grupos"].ToString();

               
                string[] values = gruposL.Split(',');
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = values[i].Trim();

                    //if (Char.IsNumber( values[i]))
                    //{
                        listGrupos.Add(new CatUsersModel.Grupo
                        {
                            Id = values[i].ToString()
                        });
                    //}

                }




                //for (int i = 0; i < grupos.Count; i++)
                //{
                //    if (Char.IsNumber(grupos[i]))
                //    {
                //        listGrupos.Add(new CatUsersModel.Grupo
                //        {
                //            Id = grupos[i].ToString()
                //        });
                //    }
                //}

                oUser.Grupos = listGrupos;
            }

            
            if (oUser.Perfil == "")
            {
                ModelState.AddModelError("Perfiles", "Seleccione un perfil");
                UserConfiguration UsersC = new UserConfiguration();
                var perfiles = UsersC.GetPerfilAll().OrderBy(x => x.Nombre).ToList();
                ViewBag.Perfil = new SelectList(perfiles, "IdPerfil", "Nombre", 1);
                return View();
            }


            UserConfiguration Users = new UserConfiguration();

            if (oUser.IdUsusario > 0)
            {
                Users.UpdateUser(oUser);
                //var resp = new GruposBLL().UIGruposUsuarioBLL();

            }
            else
            {
                var idUs = Users.InsertUser(oUser);


                if (idUs > 0)
                {
                    UserConfiguration UsersC = new UserConfiguration();
                    var perfiles = UsersC.GetPerfilAll().OrderBy(x => x.Nombre).ToList();
                    ViewBag.Perfil = new SelectList(perfiles, "IdPerfil", "Nombre", 1);
                    ViewBag.UsuarioId = "Ya existe un usuario con ese Correo";
                    return View();
                }
            }









            //oUser.idPerfil = int.Parse(data["idPerfil"].ToString());

            //IdUsusario
            //Nombre
            //ApPaterno
            //ApMaterno
            //Correo
            //Perfil
            //Activo










            //if (Perfil == "")
            //{
            //    ModelState.AddModelError("Perfiles", "Seleccione un perfil");
            //    UserConfiguration UsersC = new UserConfiguration();
            //    var perfiles = UsersC.GetPerfilAll().OrderBy(x => x.Nombre).ToList();
            //    ViewBag.Perfil = new SelectList(perfiles, "IdPerfil", "Nombre", 1);
            //    return View();
            //}

            //model.UsuarioALta = this.GetUsuario().IdUsuario;
            //model.FechaAlta = DateTime.Now;
            //model.idPerfil = int.Parse(Perfil);

            //UserConfiguration Users = new UserConfiguration();


            //if (model.IdUsusario > 0)
            //{
            //    Users.UpdateUser(model);
            //    //var resp = new GruposBLL().UIGruposUsuarioBLL();

            //}
            //else
            //{
            //    var idUs = Users.InsertUser(model);


            //    if (idUs > 0)
            //    {
            //        UserConfiguration UsersC = new UserConfiguration();
            //        var perfiles = UsersC.GetPerfilAll().OrderBy(x => x.Nombre).ToList();
            //        ViewBag.Perfil = new SelectList(perfiles, "IdPerfil", "Nombre", 1);
            //        ViewBag.UsuarioId = "Ya existe un usuario con ese Correo";
            //        return View();
            //    }
            //}
            var json = Json("OK", JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
           // return RedirectToAction("Index", "User");
        }


        public ActionResult GetGruposUsuario(string texto)
        {
            //string idusuario = data["idusuario"].ToString();
            if (texto == "")
            {
                texto = "0";
            }
            int idusuario = int.Parse(texto);

            var grupos = new GruposBLL().GetGruposUsuarioSelectedBLL(idusuario).ToList();
            //TempData["gruposList"] = new GruposBLL().GetGruposBLLUsuario(1, 0).ToList();
            

            var json = Json(new { data = grupos }, JsonRequestBehavior.AllowGet);
            json.MaxJsonLength = 500000000;
            return json;
        }



        }
}