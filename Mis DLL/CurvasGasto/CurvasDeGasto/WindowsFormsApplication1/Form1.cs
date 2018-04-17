using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CurvasDeGasto;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CurvasDeGasto.CalculaCurva CC = new CalculaCurva();
            label4.Text=CC.CalcularCurvaDeGasto(textBox3.Text.Trim().ToString(), textBox2.Text.Trim().ToString(), textBox1.Text.Trim().ToString());
        }
        private void button2_Click(object sender, EventArgs e)
        {
            CalculaCaudalConCurvaYNivel CCCCN = new CalculaCaudalConCurvaYNivel(textBox6.Text, "Q01", Convert.ToDouble(textBox5.Text), DateTime.Parse(textBox4.Text));
            label8.Text = CCCCN.Caudal.ToString();
            label4.Text = CCCCN.Regimen_de_curva.ToString();


        }

    }
}
