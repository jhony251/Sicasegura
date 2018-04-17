using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace IsValido
{
    public class Class1
    {
        private String codigoPVYCR;
        private String fecha;
        private String hora;
        private String[] parentesis = new String[100];
        private String[] operaciones = new String[100];
        private String[] operacionesLogicas = new String[100];
        private double[] porcentajes = new double[100];
        private double[] resultados = new double[100];
        double acumulador;
        int cont, cont2, a, b, c;
        Boolean[] valido = new Boolean[100];
        Boolean resultadoString;


        public Class1()
        {

        }

        /// <summary>
        /// Función que asigna un valor al parámetro CodigoPVYCR.
        /// </summary>
        /// <param name="codigoPVYCR">Parámetro de entrada: String que indica CodigoPVYCR</param>
        public void setCodigoPVYCR(String codigoPVYCR)
        {
            this.codigoPVYCR = codigoPVYCR;

        }

        /// <summary>
        /// Función que asigna un valor al parámetro Fecha.
        /// </summary>
        /// <param name="fecha">Parámetro de entrada: String que indica la Fecha</param>
        public void setFecha(String fecha)
        {
            this.fecha = fecha;

        }

        /// <summary>
        /// Función que asigna un valor al parámetro Hora.
        /// </summary>
        /// <param name="hora">Parámetro de entrada: String que indica la Hora</param>
        public void setHora(String hora)
        {
            this.hora = hora;

        }

        /// <summary>
        /// Función que nos indica si una fórmula dada por el usuario devuelve un valor válido o no válido.
        /// </summary>
        /// <returns>Devuelve un valor booleano indicando si dicho valor es válido o no.</returns>
        public Boolean isValido()
        {
            SqlConnection conecta = new SqlConnection(IsValido.Properties.Settings.Default.Telemedida);
            conecta.Open();
            SqlDataAdapter daCalculos = new SqlDataAdapter("Select ID From iSICA_Puntos_Calculos Where IDSicaPuntos='" + this.codigoPVYCR + "'", conecta);
            DataTable dtPuntosCalculos = new DataTable();
            daCalculos.Fill(dtPuntosCalculos);
            conecta.Close();
            return getCalculosLogicos(dtPuntosCalculos);
        }

        /// <summary>
        /// Función que va recorriendo uno a uno los calculos que tiene asignados el punto PVYCR e indicando si son válidos o no.
        /// </summary>
        /// <param name="dtPuntosCalculos">Datatable con las formulas(String) que son calculos del punto PVYCR</param>
        /// <returns>Devuelve un valor indicando si la la resolución de la fórmula a devuelto un valor válido o no </returns>
        private Boolean getCalculosLogicos(DataTable dtPuntosCalculos)
        {
            for (int i = 0; i < dtPuntosCalculos.Rows.Count; i++)
            {   //Conectamos con la base de datos
                SqlConnection conecta = new SqlConnection(IsValido.Properties.Settings.Default.Telemedida);
                conecta.Open();
                SqlDataAdapter daCalculos = new SqlDataAdapter("Select StrCalculo From iSICA_Calculos Where IDPuntosCalculos='" + dtPuntosCalculos.Rows[i].ItemArray.GetValue(0).ToString().Trim() + "'", conecta);
                DataTable dtStringCalculos = new DataTable();
                daCalculos.Fill(dtStringCalculos);
                conecta.Close();
                getConsultas(dtStringCalculos);
                inicializaArrays(); //Inicializamos los arrays al valor inicial
                if (resultadoString == true) return true;   //Si algun String da "true" es valido
            }
            return false;
        }

        /// <summary>
        /// Función que trata una fórmula y va distribuyendo los datos por distintos arrays para que posteriormente sean tratados.
        /// </summary>
        /// <param name="dtStringCalculos">Datatable con una formula (String) a tratar.</param>
        private void getConsultas(DataTable dtStringCalculos)
        {
            cont = 0;
            cont2 = 0;
            a = 0;
            b = 0;
            for (int i = 0; i < dtStringCalculos.Rows.Count; i++)
            {
                String[] tokenConsultas = dtStringCalculos.Rows[0].ItemArray.GetValue(0).ToString().Trim().Replace(".", ",").Split(' ');
                foreach (String token in tokenConsultas)
                {
                    if (a % 2 == 0)
                    {
                        String[] tokenMini = token.Split('%');  //Introducimos los valores a comparar dentro del vector porcentajes
                        if (tokenMini.Length > 1)
                        {
                            porcentajes[b] = Convert.ToDouble(tokenMini[0]);
                            b++;
                        }
                        else
                        {                               //Introducimos los calculos a realizar entre los operadores
                            parentesis[cont] = token;
                            cont++;
                        }
                    }
                    else
                    {                                   //Introducimos las operaciones intermedias
                        operaciones[cont2] = token;
                        cont2++;
                    }
                    a++;

                }
            }
            getResultados();
        }
        /// <summary>
        /// Función que se encarga de obtener los resultados de los cálculos que hay entre paréntesis y almacenarlos en un vector resultados.
        /// </summary>
        private void getResultados()
        {

            SqlConnection conecta = new SqlConnection(IsValido.Properties.Settings.Default.Telemedida);
            conecta.Open();
            double[] datos = new double[2];
            int ahora = 0;
            bool salida = false;
            String operador = "";
            for (int i = 0; i < cont; i++)
            {
                String[] tokenConsultas = parentesis[i].Split('&');
                foreach (String token in tokenConsultas)         //Vamos cogiendo los operadores y operaciones de las operaciones
                {
                    if (ahora % 2 == 0)
                    {
                        String[] tokenMini = token.Split('+');     //Vemos si el operador es del tipo "+1"
                        if (tokenMini.Length > 1)
                        {
                            SqlDataAdapter daCalculos = new SqlDataAdapter("Select TOP(1) " + tokenMini[0] + " From SICA_DatosAcequias_TopKapi Where codigopvycr='" + this.codigoPVYCR + "' and Fecha>'" + this.fecha + " " + this.hora + "'", conecta);
                            DataTable dtOperando1 = new DataTable();
                            daCalculos.Fill(dtOperando1);
                            datos[ahora / 2] = Convert.ToDouble(dtOperando1.Rows[0].ItemArray.GetValue(0).ToString().Replace(".", ",").Trim());
                            ahora = ahora + 1;
                            salida = true;

                        }
                        tokenMini = token.Split('-');            //Vemos si el operador es del tipo "-1"
                        if (tokenMini.Length > 1)
                        {
                            SqlDataAdapter daCalculos = new SqlDataAdapter("Select TOP(1) " + tokenMini[0] + " From SICA_DatosAcequias_TopKapi Where codigopvycr='" + this.codigoPVYCR + "' and Fecha<'" + this.fecha + " " + this.hora + "' order by Fecha desc", conecta);
                            DataTable dtOperando1 = new DataTable();
                            daCalculos.Fill(dtOperando1);
                            datos[ahora / 2] = Convert.ToDouble(dtOperando1.Rows[0].ItemArray.GetValue(0).ToString().Replace(".", ",").Trim());
                            ahora = ahora + 1;
                            salida = true;
                        }

                        if (salida == false)                        //vemos si el operador es del tipo normal
                        {

                            SqlDataAdapter daCalculos = new SqlDataAdapter("Select " + token + " From SICA_DatosAcequias_TopKapi Where codigopvycr='" + this.codigoPVYCR + "' and Fecha='" + this.fecha + " " + this.hora + "'", conecta);
                            DataTable dtOperando1 = new DataTable();
                            daCalculos.Fill(dtOperando1);
                            datos[ahora / 2] = Convert.ToDouble(dtOperando1.Rows[0].ItemArray.GetValue(0).ToString().Replace(".", ",").Trim());
                            ahora = ahora + 1;
                        }
                        salida = false;
                    }
                    else
                    {                                                //guardamos la operacion a realizar
                        operador = operador + token;
                        ahora = ahora + 1;
                    }
                }
                ahora = 0;

                switch (operador)                                   //Realizamos la operación con los datos obtenidos anteriormente y almacenamos en resultados
                {
                    case "suma":
                        resultados[i] = datos[0] + datos[1];
                        break;
                    case "resta":
                        resultados[i] = datos[0] - datos[1];
                        break;
                    case "multiplica":
                        resultados[i] = datos[0] * datos[1];
                        break;
                    case "divide":
                        if (datos[1] == 0)
                        {
                            resultados[i] = datos[0] / 1;
                        }
                        else resultados[i] = datos[0] / datos[1];
                        break;
                    default:
                        resultados[i] = datos[0];
                        break;
                }
                operador = "";
            }

            getResultadosOperaciones();
        }
        /// <summary>
        /// Función que se encarga de realizar las operaciones intermedias entre los paréntesis utilizando el vector resultados y la comprobación de si es 
        /// mayor, menor o igual que el valor indicado en la fórmula. Indicando si dicho cálculo es válido o no.
        /// </summary>
        private void getResultadosOperaciones()
        {
            a = 0;
            b = 0;
            c = 0;
            Boolean comparador = false;
            double ultimaoperacion = 0;
            acumulador = resultados[a];

            for (int i = 0; i < cont2; i++)                 //Tratamos las operaciones intermedias
            {
                if (operaciones[i] == "and" || operaciones[i] == "or" || operaciones[i] == "not")
                {
                    operacionesLogicas[c] = operaciones[i];             //Si es una operación lógica la guardamos para tratarlas todas al final
                    c++;
                }


                else if (operaciones[i] == "mayor" || operaciones[i] == "menor" || operaciones[i] == "igual")
                {                                               //Si la operación es de este tipo comprobamos si es válido el valor obtenido
                    comparador = false;                         //Utilizamos "comparador" por si acaso se anidan más de una operación matemática intermedia   
                    switch (operaciones[i])
                    {
                        case "mayor":
                            if (acumulador > porcentajes[b])
                            {
                                valido[b] = true;
                            }
                            else valido[b] = false;
                            break;
                        case "menor":
                            if (acumulador < porcentajes[b])
                            {
                                valido[b] = true;
                            }
                            else valido[b] = false;
                            break;
                        case "igual":
                            if (acumulador == porcentajes[b])
                            {
                                valido[b] = true;
                            }
                            else valido[b] = false;
                            break;
                    }
                    b++;
                    a++;
                    acumulador = resultados[a];
                }

                else
                {
                    switch (operaciones[i])                         //Se calculan las operaciones matemáticas intermedias
                    {
                        case "suma":
                            if (comparador == true)                 //Sirve para comprobar si hay varias operaciones matemáticas seguidas
                            {
                                acumulador = ultimaoperacion + resultados[a + 1];
                            }
                            else
                            {
                                acumulador = resultados[a] + resultados[a + 1];
                            }
                            break;
                        case "resta":
                            if (comparador == true)
                            {
                                acumulador = ultimaoperacion - resultados[a + 1];
                            }
                            else
                            {
                                acumulador = resultados[a] - resultados[a + 1];
                            }
                            break;
                        case "multiplica":
                            if (comparador == true)
                            {
                                acumulador = ultimaoperacion * resultados[a + 1];
                            }
                            else
                            {
                                acumulador = resultados[a] * resultados[a + 1];
                            }
                            break;
                        case "divide":
                            if (comparador == true)
                            {
                                if (resultados[a + 1] == 0)
                                {
                                    acumulador = ultimaoperacion / 1;
                                }
                                else acumulador = ultimaoperacion / resultados[a + 1];
                            }
                            else
                            {
                                if (resultados[a + 1] == 0)
                                {
                                    acumulador = resultados[a] / 1;
                                }
                                
                                else acumulador = resultados[a] / resultados[a + 1];
                            }
                            break;
                    }
                    ultimaoperacion = acumulador;
                    comparador = true;
                    a++;
                }
            }
            getOperacionesLogicas();

        }

        /// <summary>
        /// Función que hace las operaciones lógicas que se encuentran los resultados booleanos que se han obtenido de las operaciones 
        /// realizadas anteriormente.
        /// </summary>
        private void getOperacionesLogicas()
        {
            a = 0;
            resultadoString = valido[a];
            a++;
            for (int i = 0; i < c; i++)
            {
                switch (operacionesLogicas[i])              //Realizamos las operaciones lógicas que habiamos dejado para el final teniendo los operandos lógicos obtenidos
                {
                    case "and":
                        resultadoString = resultadoString && valido[a];
                        break;
                    case "or":
                        resultadoString = resultadoString || valido[a];
                        break;
                    case "not":
                        resultadoString = !resultadoString;
                        break;

                }
                a++;
            }

        }

        private void inicializaArrays()
        {
            for (int i = 0; i < parentesis.Length; i++)
            {
                parentesis[i] = "";
                operaciones[i] = "";
                operacionesLogicas[i] = "";
                porcentajes[i] = 0;
                resultados[i] = 0;
                valido[i] = false;
            }
        }

    }
}

