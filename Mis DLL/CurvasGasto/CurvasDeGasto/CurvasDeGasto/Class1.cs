using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CurvasDeGasto
{
    public class comun
    {
        public DataTable ejecuta_sentencia_sql_CHS(String queryString)
        {
            // Retrieve the connection string stored in the Web.config file.
            //String connectionString = ConfigurationManager.ConnectionStrings["NorthWindConnectionString"].ConnectionString;
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection("Data Source=10.31.224.74;Initial Catalog=SIGVECTOR;Persist Security Info=True;User ID=PVYCR_Avanzado;Password=caracas-439");
            System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(queryString, connection);
            // Fill the DataSet.
            adapter.Fill(dt);
            return dt;
        }
        public DataTable ejecuta_sentencia_sql_MUR(String queryString)
        {
            // Retrieve the connection string stored in the Web.config file.
            //String connectionString = ConfigurationManager.ConnectionStrings["NorthWindConnectionString"].ConnectionString;
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection("Data Source=192.168.194.11\\MURCIADB2005;Initial Catalog=Telemedida;Persist Security Info=True;User ID=sica2008;Password=sica2008");
            System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(queryString, connection);
            // Fill the DataSet.
            adapter.Fill(dt);
            return dt;
        }
    }
    public class ListadoCurvasValidas
    {
        private string Nombre_Curva;
        private double Caudal_Calculado;
        private int Numero_Apariciones;
        public string get_nombre_curva()
        {
            return Nombre_Curva;
        }
        public double get_Caudal_calculado()
        {
            return Caudal_Calculado;
        }
        public int get_Apariciones()
        {
            return Numero_Apariciones;
        }
        public void set_nombre_curva(string nombre)
        {
            Nombre_Curva = nombre;
        }
        public void set_Caudal_calculado(double caudal)
        {
            Caudal_Calculado = caudal;
        }
        public void set_Apariciones(int apariciones)
        {
            Numero_Apariciones = apariciones;
        }
    }
    public class CalculaCurva
    {
        public CalculaCurva()
        {
        }
        private string CalcularCurvaDeGasto_old(string caudal, string calado, string codigopvycr)
        {
            string Curva_Valida = "P0";
            double diferencial_Valido = 100;
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            string SQL = "SELECT Cod_Curva FROM PVYCR_CurvasAcequias WHERE CodigoPVYCR='" + codigopvycr + "'";
            try
            {
                dt = ejecuta_sentencia_sql_CHS(SQL); //SIGVECTOR
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //recorremos todas las curvas de gasto que posee el punto para recoger los puntos de la curva
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SQL = "SELECT * FROM PVYCR_CurvasAcequias_Valores WHERE Cod_Curva='" + dt.Rows[i].ItemArray.GetValue(0).ToString().Trim() + "' and Caudal is not null";
                try
                {
                    dt2 = ejecuta_sentencia_sql_CHS(SQL);//SIGVECTOR
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                //recorremos los puntos de la curva y vemo slos dos entre los que está comprendido
                //el nivel actual.
                bool encontrado = false;
                int y = 0;
                while ((!encontrado) && (y < dt2.Rows.Count))
                {
                    double datocurva = Convert.ToDouble(dt2.Rows[y].ItemArray.GetValue(1).ToString().Trim());
                    double datocampo = Convert.ToDouble(calado.Replace(".", ","));
                    if (datocurva >= datocampo)
                    {
                        encontrado = true;
                    }
                    else
                    {
                        y++;
                    }
                }
                //Encontrado el elemento igual o el inmediatamente superior, procedemos a ver en que caso
                //estamos y a calcular el diferencial de caudal en cada caso.
                //si el elemento es el mismo sólo vamos a tener que hacer la resta, en caso contrario
                //vamos a tener que hallar el valor del caudal para el nivel dado y entonces calcular
                //el diferencial.
                if (!encontrado)
                {
                    if (Convert.ToDouble(caudal.Replace(".", ",")) < 0.050)
                    {
                        Curva_Valida = "991";
                    }
                    else
                    {
                        Curva_Valida = curvamayoritaria(codigopvycr);
                    }
                }
                else
                {
                    if ((dt.Rows[i].ItemArray.GetValue(0).ToString().Trim().Replace(".", ",") == "991"))
                    {
                        if (Convert.ToDouble(caudal.Replace(".", ",")) < 0.050)
                        {
                            diferencial_Valido = Convert.ToDouble(caudal.Replace(".", ","));
                            Curva_Valida = "991";
                        }
                    }
                    else
                    {
                        if (Convert.ToDouble(dt2.Rows[y].ItemArray.GetValue(1).ToString().Trim().Replace(".", ",")) == Convert.ToDouble(calado.Replace(".", ",")))
                        {
                            //en este caso sólo cálculamos el diferencial
                            if (diferencial_Valido > (System.Math.Abs(Convert.ToDouble(caudal.Replace(".", ",")) - Convert.ToDouble(dt2.Rows[y].ItemArray.GetValue(2).ToString().Trim().Replace(".", ",")))))
                            {
                                diferencial_Valido = System.Math.Abs(Convert.ToDouble(caudal.Replace(".", ",")) - Convert.ToDouble(dt2.Rows[y].ItemArray.GetValue(2).ToString().Trim().Replace(".", ",")));
                                Curva_Valida = dt.Rows[i].ItemArray.GetValue(0).ToString().Trim().Replace(".", ",");
                            }
                        }
                        else
                        {
                            //en este caso hay que calcular el caudal para el nivel dado y hecer el diferencial
                            double a = 0;
                            double b = 0;
                            double Q0 = 0;
                            b = ((Convert.ToDouble(dt2.Rows[y].ItemArray.GetValue(2).ToString().Trim().Replace(".", ",")) - Convert.ToDouble(dt2.Rows[y - 1].ItemArray.GetValue(2).ToString().Trim().Replace(".", ","))) / (Convert.ToDouble(dt2.Rows[y].ItemArray.GetValue(1).ToString().Trim().Replace(".", ",")) - Convert.ToDouble(dt2.Rows[y - 1].ItemArray.GetValue(1).ToString().Trim().Replace(".", ","))));
                            a = Convert.ToDouble(dt2.Rows[y].ItemArray.GetValue(2).ToString().Trim().Replace(".", ",")) - (b * Convert.ToDouble(dt2.Rows[y].ItemArray.GetValue(1).ToString().Trim().Replace(".", ",")));
                            Q0 = a + (b * Convert.ToDouble(calado.Replace(".", ",")));
                            if (diferencial_Valido > (System.Math.Abs(Convert.ToDouble(caudal.Replace(".", ",")) - Q0)))
                            {
                                diferencial_Valido = System.Math.Abs(Convert.ToDouble(caudal.Replace(".", ",")) - Q0);
                                Curva_Valida = dt.Rows[i].ItemArray.GetValue(0).ToString().Trim();
                            }
                        }
                    }
                }
            }
            //Teniendo el codigo PVYCR y el codigo de curva válido buscamos el régimen de curva a al que pertenece.
            //y lo asignamos a la variable curva_valida que es la que devolvemso.
            if (codigopvycr == "VA006P04")//SI EL PUNTO DE CONTROL ES EL VA006P04 LA CURVA DE GASTO ES NULL
            {
                Curva_Valida = "NULL";
            }
            else
            {
                SQL = "SELECT Regimen FROM PVYCR_CurvasAcequias WHERE CodigoPVYCR='" + codigopvycr + "' AND Cod_Curva=" + Convert.ToInt32(Curva_Valida);
                try
                {
                    dt = ejecuta_sentencia_sql_CHS(SQL);//SIGVECTOR
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Curva_Valida = dt.Rows[0].ItemArray.GetValue(0).ToString().Trim();
            }
            
            //MessageBox.Show(Curva_Valida + "  --  " + diferencial_Valido);
            return Curva_Valida;
        }
        public string CalcularCurvaDeGasto(string caudal, string calado, string codigopvycr)
        {
            ListadoCurvasValidas[] listadocurvas = new ListadoCurvasValidas[10];
            bool el_punto_no_tiene_curvas = true;
            string Curva_Valida = "P0";
            double diferencial_Valido = 100;
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            string SQL = "SELECT Cod_Curva FROM PVYCR_CurvasAcequias WHERE CodigoPVYCR='" + codigopvycr + "'";
            try
            {
                dt = ejecuta_sentencia_sql_CHS(SQL); //SIGVECTOR
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (dt.Rows.Count == 0){el_punto_no_tiene_curvas = true;}else{el_punto_no_tiene_curvas = false;}
            //recorremos todas las curvas de gasto que posee el punto para recoger los puntos de la curva
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                listadocurvas[i] = new ListadoCurvasValidas();
                SQL = "SELECT * FROM PVYCR_CurvasAcequias_Valores WHERE Cod_Curva='" + dt.Rows[i].ItemArray.GetValue(0).ToString().Trim() + "' and Caudal is not null";
                try
                {
                    dt2 = ejecuta_sentencia_sql_CHS(SQL);//SIGVECTOR
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                //recorremos los puntos de la curva y vemo slos dos entre los que está comprendido
                //el nivel actual.
                bool encontrado = false;
                int y = 0;
                while ((!encontrado) && (y < dt2.Rows.Count))
                {
                    double datocurva = Convert.ToDouble(dt2.Rows[y].ItemArray.GetValue(1).ToString().Trim());
                    double datocampo = Convert.ToDouble(calado.Replace(".", ","));
                    if (datocurva >= datocampo)
                    {
                        encontrado = true;
                    }
                    else
                    {
                        y++;
                    }
                }
                //Encontrado el elemento igual o el inmediatamente superior, procedemos a ver en que caso
                //estamos y a calcular el diferencial de caudal en cada caso.
                //si el elemento es el mismo sólo vamos a tener que hacer la resta, en caso contrario
                //vamos a tener que hallar el valor del caudal para el nivel dado y entonces calcular
                //el diferencial.
                if (!encontrado)//Si no lo hemos encontrado, miramos si está cortada y si no ponemos la mayoritaria.
                {
                    if (Convert.ToDouble(caudal.Replace(".", ",")) < 0.020)
                    {
                        Curva_Valida = "991";
                        diferencial_Valido = Convert.ToDouble(caudal.Replace(".", ","));
                    }
                    else
                    {
                        Curva_Valida = curvamayoritaria(codigopvycr);
                        diferencial_Valido = Convert.ToDouble(caudal.Replace(".", ","));
                    }
                }
                else
                {
                    if ((dt.Rows[i].ItemArray.GetValue(0).ToString().Trim().Replace(".", ",") == "991"))
                    {
                        //if (Convert.ToDouble(caudal.Replace(".", ",")) < 0.050)
                        //{
                            diferencial_Valido = Convert.ToDouble(caudal.Replace(".", ","));
                            Curva_Valida = "991";                          
                        //}
                    }
                    else
                    {
                        if (Convert.ToDouble(dt2.Rows[y].ItemArray.GetValue(1).ToString().Trim().Replace(".", ",")) == Convert.ToDouble(calado.Replace(".", ",")))
                        {
                            //en este caso sólo cálculamos el diferencial
                            if (diferencial_Valido > (System.Math.Abs(Convert.ToDouble(caudal.Replace(".", ",")) - Convert.ToDouble(dt2.Rows[y].ItemArray.GetValue(2).ToString().Trim().Replace(".", ",")))))
                            {
                                diferencial_Valido = System.Math.Abs(Convert.ToDouble(caudal.Replace(".", ",")) - Convert.ToDouble(dt2.Rows[y].ItemArray.GetValue(2).ToString().Trim().Replace(".", ",")));
                                Curva_Valida = dt.Rows[i].ItemArray.GetValue(0).ToString().Trim().Replace(".", ",");                              
                            }
                        }
                        else
                        {
                            //en este caso hay que calcular el caudal para el nivel dado y hecer el diferencial
                            double a = 0;
                            double b = 0;
                            double Q0 = 0;
                            b = ((Convert.ToDouble(dt2.Rows[y].ItemArray.GetValue(2).ToString().Trim().Replace(".", ",")) - Convert.ToDouble(dt2.Rows[y - 1].ItemArray.GetValue(2).ToString().Trim().Replace(".", ","))) / (Convert.ToDouble(dt2.Rows[y].ItemArray.GetValue(1).ToString().Trim().Replace(".", ",")) - Convert.ToDouble(dt2.Rows[y - 1].ItemArray.GetValue(1).ToString().Trim().Replace(".", ","))));
                            a = Convert.ToDouble(dt2.Rows[y].ItemArray.GetValue(2).ToString().Trim().Replace(".", ",")) - (b * Convert.ToDouble(dt2.Rows[y].ItemArray.GetValue(1).ToString().Trim().Replace(".", ",")));
                            Q0 = a + (b * Convert.ToDouble(calado.Replace(".", ",")));
                            if (diferencial_Valido > (System.Math.Abs(Convert.ToDouble(caudal.Replace(".", ",")) - Q0)))
                            {
                                diferencial_Valido = System.Math.Abs(Convert.ToDouble(caudal.Replace(".", ",")) - Q0);
                                Curva_Valida = dt.Rows[i].ItemArray.GetValue(0).ToString().Trim();                               
                            }
                        }
                    }
                }
                listadocurvas[i].set_nombre_curva(Curva_Valida);
                listadocurvas[i].set_Caudal_calculado(diferencial_Valido);
                listadocurvas[i].set_Apariciones((int) Cuenta_Elementos_Curva(codigopvycr,Curva_Valida));
                //
                //CON ESTE BLOQUE FOR HEMOS IDO BUSCANDO TODAS LAS POSIBILIDADES DE CURVA QUE SERÍAN VALIDAS
                //Y LAS HEMOS METIDO EN EL ARRAY LISTADO CURVAS EN EL QUE HEMOS GUARDADO EL CODIGO DE CURVA
                //EL DIFERENCIAL DE CAUDAL QUE LE CORRESPONDERÍA Y EL NUMERO DE ELEMENTOS EN LA BD PARA ESTA CURVA.
                //
            }

            if (el_punto_no_tiene_curvas)//SI EL PUNTO DE CONTROL NO TIENE CURVAS DE GASTO
            {
                Curva_Valida = "NULL";
            }
            else
            {
                //SI HAY CURVAS ENTONCES PASAMOS A BUSCAR EN EL ARRAY DE POSIBILIDADES LA QUE TIENE EL 
                //DIFERENCIAL MÁS PEQUEÑO.
                
                bool hayCoincidencia = false;
                int posición_valida = 0;
                double diferencial_menor = 1000;
                for (int t = 0; t < 10; t++)
                {
                    if (listadocurvas[t] != null)
                    {
                        if (listadocurvas[t].get_Caudal_calculado() < diferencial_menor)
                        {
                            if ((listadocurvas[t].get_nombre_curva().ToString().Trim() == "991") && (Convert.ToDouble(caudal) > 0.05))
                            {
                                hayCoincidencia = false;
                            }
                            else
                            {
                                hayCoincidencia = true;
                                posición_valida = t;
                                diferencial_menor = listadocurvas[t].get_Caudal_calculado();
                            }
                        }
                        else
                        {
                            //if (listadocurvas[t].get_Caudal_calculado() == diferencial_menor)
                            //{
                            //    hayCoincidencia = true;
                            //}
                        }
                    }
                }
                //SI ENCONTRAMOS MÁS DE UNA CURVA CON EL MISMO DIFERENCIAL MÍNIMO, ENTONCES TENDREMOS QUE BUSCAR
                //LA QUE TENGA MÁS APARICIONES EN LA BASE DE DATOS. COMO ESE ES UN DATO QUE TAMBIEN HEMOS GUARDADO
                //SOLO HABRÁ QUE CONSULTAR EL CAMPO EN EL ARRAY.
                if (hayCoincidencia)
                {
                    posición_valida = 0;
                    double mayor_aparición = 0;
                    for (int t = 0; t < 10; t++)
                    {
                        if (listadocurvas[t] != null)
                        {
                            if (listadocurvas[t].get_Caudal_calculado() == diferencial_menor)
                            {
                                if (listadocurvas[t].get_Apariciones() > mayor_aparición)
                                {
                                    mayor_aparición = listadocurvas[t].get_Apariciones();
                                    posición_valida = t;
                                }
                            }
                        }
                    }
                }
                SQL = "SELECT Regimen FROM PVYCR_CurvasAcequias WHERE CodigoPVYCR='" + codigopvycr + "' AND Cod_Curva=" + Convert.ToInt32(listadocurvas[posición_valida].get_nombre_curva().ToString().Trim());
                try
                {
                    dt = ejecuta_sentencia_sql_CHS(SQL);//SIGVECTOR
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Curva_Valida = dt.Rows[0].ItemArray.GetValue(0).ToString().Trim();
            }

            //MessageBox.Show(Curva_Valida + "  --  " + diferencial_Valido);
            return Curva_Valida;
        }
        private long Cuenta_Elementos_Curva(string codigopvycr, string Cod_Curva)
        {
            string SQL = "";
            
            string Curva = "";
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            
            
            SQL = "SELECT Regimen FROM PVYCR_CurvasAcequias WHERE CodigoPVYCR='" + codigopvycr + "' AND Cod_Curva=" + Convert.ToInt32(Cod_Curva);
            try
            {
                dt = ejecuta_sentencia_sql_CHS(SQL);//SIGVECTOR
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Curva = dt.Rows[0].ItemArray.GetValue(0).ToString().Trim();            
            SQL = "SELECT COUNT(*) FROM PVYCR_DatosAcequias WHERE CodigoPVYCR='" + codigopvycr + "' AND RegimenCurva='" + Curva + "'";
            try
            {
                dt2 = ejecuta_sentencia_sql_CHS(SQL);//SIGVECTOR
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }           
            return Convert.ToInt64( dt2.Rows[0].ItemArray.GetValue(0).ToString().Trim());


        }
        private string curvamayoritaria(string codigopvycr)
        {
            string SQL = "";
            string mayoritaria = "";
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            int elementosmayoritaria = 0;
            SQL = "SELECT Regimen FROM PVYCR_CurvasAcequias WHERE CodigoPVYCR='" + codigopvycr + "'";
            try
            {
                dt = ejecuta_sentencia_sql_CHS(SQL);//SIGVECTOR
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SQL = "SELECT COUNT(*) FROM PVYCR_DatosAcequias WHERE CodigoPVYCR='" + codigopvycr + "' AND RegimenCurva='" + dt.Rows[i].ItemArray.GetValue(0).ToString().Trim() + "'";
                try
                {
                    dt2 = ejecuta_sentencia_sql_CHS(SQL);//SIGVECTOR
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                if (elementosmayoritaria < Convert.ToInt32(dt2.Rows[0].ItemArray.GetValue(0).ToString().Trim()))
                {
                    elementosmayoritaria = Convert.ToInt32(dt2.Rows[0].ItemArray.GetValue(0).ToString().Trim());
                    mayoritaria = dt.Rows[i].ItemArray.GetValue(0).ToString().Trim();
                }
            }
            SQL = "SELECT Cod_Curva FROM PVYCR_CurvasAcequias WHERE CodigoPVYCR='" + codigopvycr + "' AND REGIMEN='" + mayoritaria + "'";
            try
            {
                dt = ejecuta_sentencia_sql_CHS(SQL);//SIGVECTOR
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return dt.Rows[0].ItemArray.GetValue(0).ToString().Trim();


        }
        private DataTable ejecuta_sentencia_sql_CHS(String queryString)
        {
            // Retrieve the connection string stored in the Web.config file.
            //String connectionString = ConfigurationManager.ConnectionStrings["NorthWindConnectionString"].ConnectionString;
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection("Data Source=10.31.224.74;Initial Catalog=SIGVECTOR;Persist Security Info=True;User ID=PVYCR_Avanzado;Password=caracas-439");
            System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(queryString, connection);
            // Fill the DataSet.
            adapter.Fill(dt);
            return dt;
        }
        private DataTable ejecuta_sentencia_sql_MUR(String queryString)
        {
            // Retrieve the connection string stored in the Web.config file.
            //String connectionString = ConfigurationManager.ConnectionStrings["NorthWindConnectionString"].ConnectionString;
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection("Data Source=192.168.194.11\\MURCIADB2005;Initial Catalog=Telemedida;Persist Security Info=True;User ID=sica2008;Password=sica2008");
            System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(queryString, connection);
            // Fill the DataSet.
            adapter.Fill(dt);
            return dt;
        }
    }
    public class CalculaCaudalConCurvaYNivel
    {
        private string Codigopvycr="";
        private string EM = "";
        private string RegimenDeCurva = "";
        private double nivel;
        private DateTime fechadato = new DateTime();
        private comun cm = new comun();
        private double Resultado_Caudal = 0;
        public double Caudal
        {
            get
            {
                return Resultado_Caudal;
            }
            set
            {
                Resultado_Caudal = value;
            }
        }
        public string Regimen_de_curva
        {
            get
            {
                return RegimenDeCurva;
            }
            set
            {
                RegimenDeCurva = value;
            }
        }

        public CalculaCaudalConCurvaYNivel(string pvycr,string elemMed, double niv, DateTime fecDato)
        {
            DataTable dt = new DataTable();
            Codigopvycr = pvycr;
            nivel = niv;
            EM = elemMed;
            if (fecDato == null) {fechadato = DateTime.Today;} else {fechadato = fecDato;}
            RegimenDeCurva = ObtenerRegimenCurva();
            dt = cm.ejecuta_sentencia_sql_CHS("Select cod_curva From PVYCR_CurvasAcequias where codigopvycr='" + Codigopvycr + "' and regimen='" + RegimenDeCurva + "'");
            dt = cm.ejecuta_sentencia_sql_CHS("Select nivel,caudal From PVYCR_CurvasAcequias_Valores where cod_curva='" + dt.Rows[0].ItemArray.GetValue(0).ToString().Trim() + "'");
            if (Se_sale_de_curva_el_nivel(dt))
            {
                RegimenDeCurva=Busca_curva_mayoritaria_para_nivel();
                dt = cm.ejecuta_sentencia_sql_CHS("Select cod_curva From PVYCR_CurvasAcequias where codigopvycr='" + Codigopvycr + "' and regimen='" + RegimenDeCurva + "'");
                dt = cm.ejecuta_sentencia_sql_CHS("Select nivel,caudal From PVYCR_CurvasAcequias_Valores where cod_curva='" + dt.Rows[0].ItemArray.GetValue(0).ToString().Trim() + "'");
            }
            
            Resultado_Caudal = Obtener_caudal_con_los_datos_de_la_curva(dt); 
        }
        /// <summary>
        /// Funcion para buscar la curva que más veces ha sido aplicada con el nivel en cuestión que estamos
        /// procesando.
        /// Va a entrar en juego la variable global que define el nivel
        /// </summary>
        /// <returns>String que contiene el nombre de la curva.</returns>
        private string Busca_curva_mayoritaria_para_nivel()
        {
            string curvamayoritaria = "";
            int apariciones = 0;
            double factor=0;
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            dt = cm.ejecuta_sentencia_sql_CHS("Select Distinct RegimenCurva from PVYCR_DatosAcequias Where codigoPVYCR like '" + Codigopvycr + "' and idElementoMedida like '" + EM + "' and Cod_fuente_dato like '05' and (Escala_m=" + nivel.ToString().Replace(",", ".") + " OR Calado_m=" + nivel.ToString().Replace(",", ".")+")");
            while(dt2.Rows.Count == 0)
                {
                    dt2 = cm.ejecuta_sentencia_sql_CHS("Select Distinct RegimenCurva from PVYCR_DatosAcequias Where codigoPVYCR like '" + Codigopvycr + "' and idElementoMedida like '" + EM + "' and Cod_fuente_dato like '05' and ((Escala_m <" + Convert.ToString(nivel + factor).Replace(",", ".") + " AND Escala_m >" + Convert.ToString(nivel - factor).Replace(",", ".") + ") OR (Calado_m <" + Convert.ToString(nivel + factor).Replace(",", ".") + " AND Calado_m >" + Convert.ToString(nivel - factor).Replace(",", ".") + "))");
                    factor += 0.01;
                }
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                dt3 = cm.ejecuta_sentencia_sql_CHS("Select count (*) from PVYCR_DatosAcequias Where codigoPVYCR like '" + Codigopvycr + "' and idElementoMedida like '" + EM + "' and Cod_fuente_dato like '05' and ((Escala_m <" + Convert.ToString(nivel + factor).Replace(",", ".") + " AND Escala_m >" + Convert.ToString(nivel - factor).Replace(",", ".") + ") OR (Calado_m <" + Convert.ToString(nivel + factor).Replace(",", ".") + " AND Calado_m >" + Convert.ToString(nivel - factor).Replace(",", ".") + ")) AND regimenCurva like '" + dt2.Rows[i].ItemArray.GetValue(0).ToString() + "'");
                if (Convert.ToInt16(dt3.Rows[0].ItemArray.GetValue(0)) > apariciones)
                {
                    apariciones = Convert.ToInt16(dt3.Rows[0].ItemArray.GetValue(0));
                    curvamayoritaria = dt2.Rows[i].ItemArray.GetValue(0).ToString();
                }
            }
            return curvamayoritaria;
        }


        /// <summary>
        /// Esta función detectará si la curva que hemos elegido es aplicable al nivel del que deseamos calcular
        /// el caudal
        /// </summary>
        /// <param name="datos_curva">DataTable que contiene los datos de la curva a testear</param>
        /// <param name="nivel">Duble que contiene el nivel que tenemos que testear</param>
        /// <returns>Booleano afirmano o negando la vlaidez.</returns>
        private bool Se_sale_de_curva_el_nivel(DataTable datos_curva)
        {
            double maximo=0;
            
            for (int i=0; i<datos_curva.Rows.Count;i++)
            {
                if (Convert.ToDouble(datos_curva.Rows[i].ItemArray.GetValue(0).ToString().Replace(".", ",")) > maximo)
                {
                    maximo = Convert.ToDouble(datos_curva.Rows[i].ItemArray.GetValue(0).ToString().Replace(".", ","));
                }
            }
            if (nivel > maximo) { return true; }
            else { return false; }
        }
        /// <summary>
        ///Utilizando el nivel y el caudal y buscamos el último 
        ///valor de curva que no sea más antiguo de 1 mes y si no
        ///buscamos la mayoritaria             
        /// </summary>
        /// <returns>String que contiene el nombre de la curva</returns>
        protected String ObtenerRegimenCurva()
        {
            String regimen = "";
            DataTable dtFechaMedida = new DataTable();
            dtFechaMedida = cm.ejecuta_sentencia_sql_CHS("SELECT TOP (1) Fecha_Medida,RegimenCurva FROM PVYCR_DatosAcequias WHERE (idelementomedida like '" + EM + "') AND (Cod_Fuente_Dato = '05') AND (CodigoPVYCR = '" + Codigopvycr + "') AND (fecha_medida<'" + fechadato + "') ORDER BY Fecha_Medida DESC");
            DateTime fecha_medida = Convert.ToDateTime(dtFechaMedida.Rows[0].ItemArray.GetValue(0).ToString());
            TimeSpan ts = fechadato - fecha_medida;
            int diferencia = ts.Days;

            if (diferencia > 30)
            {
                DataTable dtRegimenCurva = new DataTable();
                dtRegimenCurva = cm.ejecuta_sentencia_sql_CHS("SELECT distinct RegimenCurva FROM PVYCR_DatosAcequias WHERE (idelementomedida like '" + EM + "') AND  (Cod_Fuente_Dato = '05') AND (CodigoPVYCR = '" + Codigopvycr + "')");
                int acumulador = 0;//Variable donde se cuenta el numero de apariciones de la curva
                for (int i = 0; i < dtRegimenCurva.Rows.Count; i++)
                {
                    DataTable dtCuentaRegimen = new DataTable();
                    dtCuentaRegimen = cm.ejecuta_sentencia_sql_CHS("SELECT count(*) FROM PVYCR_DatosAcequias WHERE (idelementomedida like '" + EM + "') AND  (Cod_Fuente_Dato = '05') AND (CodigoPVYCR = '" + Codigopvycr + "') and regimenCurva='" + dtRegimenCurva.Rows[i].ItemArray.GetValue(0).ToString() + "'");
                    if (Convert.ToInt32(dtCuentaRegimen.Rows[0].ItemArray.GetValue(0)) > acumulador)
                    {
                        acumulador = Convert.ToInt32(dtCuentaRegimen.Rows[0].ItemArray.GetValue(0));
                        regimen = dtRegimenCurva.Rows[i].ItemArray.GetValue(0).ToString();

                    }
                    return regimen.ToString().Trim();
                }
            }
            else
            {
                return dtFechaMedida.Rows[0].ItemArray.GetValue(1).ToString();
            }
            return "fallo";
        }
        /// <summary>
        /// Con los datos de la curva vamos a calcula el caudal correspondiente
        /// </summary>
        /// <param name="datoscurva">DataTable que contiene los datos de la curva</param>
        /// <returns>Double que contiene el caudal aplicado</returns>
        private double Obtener_caudal_con_los_datos_de_la_curva(DataTable datoscurva)
        {
            double nivant, nivpost,caudant,caudpost;
            double caudal=0;
            for (int contador = 0;contador < datoscurva.Rows.Count; contador++)
            {
                if (Convert.ToDouble(datoscurva.Rows[contador].ItemArray.GetValue(0).ToString().Trim()) >= nivel)
                {
                    nivant = Convert.ToDouble(datoscurva.Rows[contador-1].ItemArray.GetValue(0).ToString().Trim());
                    caudant = Convert.ToDouble(datoscurva.Rows[contador - 1].ItemArray.GetValue(1).ToString().Trim());
                    nivpost = Convert.ToDouble(datoscurva.Rows[contador].ItemArray.GetValue(0).ToString().Trim());
                    caudpost = Convert.ToDouble(datoscurva.Rows[contador].ItemArray.GetValue(1).ToString().Trim());
                    double pendiente = (caudpost - caudant) / (nivpost - nivant);
                    caudal = (pendiente * nivel) - (pendiente * nivant) + caudant;
                    if (caudal < 0)
                    {
                        return 0;
                    }
                    else
                    {
                        contador = datoscurva.Rows.Count;
                    }
                }

            }  
            return caudal;
        }
    }
}

