//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace linq.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class roles_usuarios
    {
        public int id_Rol_Usuario { get; set; }
        public Nullable<int> id_usuario { get; set; }
        public string tipo_rol { get; set; }
    
        public virtual tb_usuarios tb_usuarios { get; set; }
    }
}
