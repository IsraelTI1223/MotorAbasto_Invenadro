using System.Collections.Generic;

namespace PortalGRFP.Entities.Models
{
    public class UsuarioModel
    {
        public int IdUsuario { get; set; }
        public int IdPerfil { get; set; }
        public string Correo { get; set; }
        public string Nombre { get; set; }
        public string Perfil { get; set; }
        public string FotoGmail { get; set; }
        /// <summary>
        /// Menu
        /// </summary>
        public List<ModuloModel> Permisos { get; set; }
        /// <summary>
        /// Permisos
        /// </summary>
        public Dictionary<int,List<string>> Guards { get; set; }
    }
}