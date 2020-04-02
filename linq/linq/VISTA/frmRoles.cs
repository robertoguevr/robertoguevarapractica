﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using linq.Model;

namespace linq.VISTA
{
    public partial class frmRoles : Form
    {
        public frmRoles()
        {
            InitializeComponent();
        }

        private void Roles_Load(object sender, EventArgs e)
        {
            using (sistema_ventasEntities bd = new sistema_ventasEntities()) 
            {
                var Jointablas = from tbusua in bd.tb_usuarios
                                 from rolesusuarios in bd.roles_usuarios
                                 where tbusua.Id == rolesusuarios.id_Rol_Usuario

                                 select new
                                 {
                                     Id = tbusua.Id,
                                     Email = tbusua.Email,
                                     Tipo_rol = rolesusuarios.tipo_rol


                                 };

                dtVistaRoles.DataSource = Jointablas.ToList();

            
            }
        }
    }
}
