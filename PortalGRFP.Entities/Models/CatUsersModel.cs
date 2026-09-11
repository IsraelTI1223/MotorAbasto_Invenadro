using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalGRFP.Entities.Models
{
    public class CatUsersModel
    {
        public int IdUsusario { get; set; }
        [Required(ErrorMessage = "Email requerido")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Correo no valido")]
        [Display(Name = "Correo Electronico")]
        public string Correo { get; set; }
        
        [Required(ErrorMessage = "El Nombre es requerido")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Apellido Paterno es requerido")]
        [Display(Name = "Apellido Paterno")]
        public string ApPaterno { get; set; }

        [Required(ErrorMessage = "El Apellido Materno es requerido")]
        [Display(Name = "Apellido Materno")]
        public string ApMaterno { get; set; }

        [Required(ErrorMessage = "El Perfil es requerido")]
        public int idPerfil { get; set; }
        [Required(ErrorMessage = "El Perfil es requerido")]
        public string Perfil { get; set; }
        public DateTime FechaAlta { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaActualiza { get; set; }
        public int UsuarioALta { get; set; }
        public int IdUsusarioBaja { get; set; }
        public DateTime FehaBaja { get; set; }

        //[Required(ErrorMessage = "El Grupo es requerido")]
        //public int IdGrupo { get; set; }//idgrupo
        ////[Required(ErrorMessage = "El Grupo es requerido")]
        //public string Grupo { get; set; }

        public List<Grupo> Grupos { get; set; }

        public class Grupo
        {
            public string Id { get; set; }
            public string Nombre { get; set; }
            public bool Estatus { get; set; }
            public int IdUsuario { get; set; }
        }
    }
    //public class GruposModelV
    //{
    //    public int Id { get; set; }
    //    public string Nombre { get; set; }
    //    public bool Estatus { get; set; }
    //    public int IdUsuario { get; set; }

    //}
}
