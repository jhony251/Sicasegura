using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace InformaciónColumna
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1(string Nombre_Tabla )
        {
            InitializeComponent();
            Set_Nombre_Tabla(Nombre_Tabla);

        }
        public void Set_Nombre_Tabla(string value)
        {
            NombreTabla.Text=value;
        }
    }
}