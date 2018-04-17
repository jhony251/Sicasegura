Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports GuarderiaFluvial
Imports GuarderiaFluvial.JavaScript
Imports System.Data.SqlClient

Namespace CalculosSica

  Public Class SICA_FuncionesCalcVolDiferencial

    Shared Sub obtenerVolumenDiferencial(ByVal tipo As String, ByVal Tabla As DataTable, ByVal page As Page, ByRef mensaje_final As String)
      'vamos a calcular la diferencia de volúmenes según registros
      'el caso de las acequias es un caso especial y se calcula obteniendo la media del caudal por el tiempo trancurrido entre dos lecturas.
            Dim i As Integer
      Dim v_lectura_ant_horas, v_diferencial_horas, v_vol_horas, v_vol_ant_horas, v_diferencial_acum_horas, v_vol, v_vol_ant, v_diferencial, v_diferencial_kwh, v_kwh, v_caudal, v_caudal_ant, v_caudal_medio, v_diferencial_acum, v_acum_kwh, v_lectura_ant, v_diferencial_seg, v_diferencial_m3, v_diferencial_acumm3 As Decimal
      Dim v_tiempo, v_tiempo_ant As Date
      Dim v_tiempo_medio As Integer
      Dim v_segundos As Integer
      Dim primeroNulo As String = "N"
      If tipo = "V" Then
        If Tabla.Rows.Count > 0 Then
          Dim filas As Integer = Tabla.Rows.Count
          If Not Tabla.Columns.Contains("Diferencial") Then
            'añadimos la columna diferencial al dataset
            Tabla.Columns.Add("Diferencial")
            Tabla.Columns.Add("Diferencial_Acum")
                        Tabla.Columns.Add("Diferencial_seg")
                    Else
                        If Not Tabla.Columns.Contains("Diferencial_seg") Then
                            Tabla.Columns.Add("Diferencial_seg")
                        End If
                    End If
          'recorreremos todo el dataset e iremos calculando el diferencial y añadiendo la columna al dataset
          For i = 0 To filas - 1
            If i = 0 Then
              'rellenamos la columna diferencial del dataset con un 0
              Tabla.Rows(i).Item("Diferencial") = "0"
              Tabla.Rows(i).Item("Diferencial_Acum") = "0"
              Tabla.Rows(i).Item("Diferencial_Seg") = "0"
              v_lectura_ant = Convert.ToDecimal(0 & Tabla.Rows(i).Item("LecturaContador_M3").ToString)
              v_vol_ant = v_lectura_ant
              'ncm 18/02/2010 si la primera lectura es nula, marcaremos un parámetro para tenerlo en cuenta en las posteriores.
              If Tabla.Rows(i).Item("LecturaContador_M3").ToString = Nothing Then
                primeroNulo = "S"
              End If
            Else
              'comprobamos si existen incidencias
              '*************************--> obsoleto(19/05/2008)***********************-
              'si la incidencia es reseteo (6) o cambio de contador(7) se deberá tomar el valor del campo Reinicio Lectura Volumetrica 
              '************************* NUEVO ****************************************
              'si la incidencia es reseteo o cambio de contador  la fórmula es:
              '                   ((lecturam3(dia15)-reseteo(dia15) + consumo adic. (dia15)
              'si la incidencia es contador averiado yconsumo negativo (8) (5), la fñormula es :
              '                   (lecturaM3(i) + Consumovolumetricoadicional(i)) - lecturam3(i-1)

              'comprobamos si el primer registro es cero, el vol_antel diferencial es cero

              'Si el caudal es nulo pasaremos al registro siguiente y ése no lo tendremos en cuenta

              If (utiles.nullABlanco(Tabla.Rows(i).Item("LecturaContador_M3").ToString) = "") Then
                v_diferencial = 0
                v_segundos = 0
                v_diferencial_acum = v_diferencial_acum
                'añadimos los valores a la tabla
                Tabla.Rows(i).Item("Diferencial") = String.Format("{0:#,##0}", DBNull.Value)
                Tabla.Rows(i).Item("Diferencial_seg") = String.Format("{0:#,##0}", DBNull.Value)
                Tabla.Rows(i).Item("Diferencial_Acum") = String.Format("{0:#,##0}", v_diferencial_acum)
              Else
                v_vol = Convert.ToDecimal(0 & Tabla.Rows(i).Item("LecturaContador_M3").ToString)
                ' este if es por si la primera lectura es nula
                If (primeroNulo = "S") Then
                  primeroNulo = "N"
                  v_diferencial = 0
                  v_segundos = 0
                  v_lectura_ant = Convert.ToDecimal(0 & Tabla.Rows(i).Item("LecturaContador_M3").ToString)
                Else
                  'calculamos los segundos que hay entre las lecturas para poder mostrr el parcial en m3/s
                  v_segundos = DateDiff(DateInterval.Second, Tabla.Rows(i - 1).Item("Fecha_medida"), Tabla.Rows(i).Item("Fecha_medida"))

                  If (Tabla.Rows(i).Item("idincidenciaVolumetrica").ToString = "6") Or _
                      (Tabla.Rows(i).Item("idincidenciaVolumetrica").ToString = "7") Then
                    v_diferencial = ((Convert.ToDecimal(0 & Tabla.Rows(i).Item("LecturaContador_M3").ToString) - Convert.ToDecimal(0 & Tabla.Rows(i).Item("ReiniciolecturaVolumetrica").ToString)) + _
                    Convert.ToDecimal(0 & Tabla.Rows(i).Item("ConsumoVolumetricoAdicional").ToString))
                  ElseIf _
                     (Tabla.Rows(i).Item("idincidenciaVolumetrica").ToString = "5") Or _
                     (Tabla.Rows(i).Item("idincidenciaVolumetrica").ToString = "8") Then
                    If utiles.nullABlanco(Tabla.Rows(i).Item("ConsumoVolumetricoAdicional").ToString) <> "" Then
                      v_diferencial = (Convert.ToDecimal(0 & Tabla.Rows(i).Item("LecturaContador_M3").ToString) + Tabla.Rows(i).Item("ConsumoVolumetricoAdicional").ToString) - _
                      Convert.ToDecimal(0 & v_lectura_ant)
                    Else
                      v_diferencial = (Convert.ToDecimal(0 & Tabla.Rows(i).Item("LecturaContador_M3").ToString) + Convert.ToDecimal(0 & Tabla.Rows(i).Item("ConsumoVolumetricoAdicional").ToString)) - _
                      Convert.ToDecimal(0 & v_lectura_ant)

                    End If

                  Else

                    v_diferencial = v_vol - v_vol_ant
                  End If

                End If ' lectura inicial nula
                'si los segundos es cero no sacamos nada en el campo
                If v_segundos = 0 Then
                  v_diferencial_seg = 0
                Else
                  v_diferencial_seg = v_diferencial / v_segundos
                End If
                v_vol_ant = v_vol
                'nos guardamos la lectura anterior para realizar el calculo bien por si hay lecturas nulas
                v_lectura_ant = Tabla.Rows(i).Item("LecturaContador_M3").ToString
                'calculamos el diferencial acumulado
                v_diferencial_acum = v_diferencial_acum + v_diferencial
                'añadimos los valores a la tabla
                Tabla.Rows(i).Item("Diferencial") = String.Format("{0:#,##0}", v_diferencial)
                Tabla.Rows(i).Item("Diferencial_seg") = String.Format("{0:#,##0.###}", v_diferencial_seg)
                Tabla.Rows(i).Item("Diferencial_Acum") = String.Format("{0:#,##0}", v_diferencial_acum)
              End If
            End If

          Next
        End If
      ElseIf tipo = "E" Then
        Dim mensaje_idcontador As String = ""
        Dim mensaje_relacion_m3 As String = ""
        If Tabla.Rows.Count > 0 Then
          'cada registro que tenga el id contador nulo, lo guardaremos para mostrar un mensaje al usuario
          If utiles.nullABlanco(Tabla.Rows(i).Item("idContador").ToString) = "" Then
            mensaje_idcontador += "La lectura con fecha " & Tabla.Rows(i).Item("Fecha_medida").ToString & " no tiene contador relacionado \n"
          End If
          'si el factor corrector es nulo tb se mostrará sms
          If utiles.nullABlanco(Tabla.Rows(i).Item("relacionM3_kwh").ToString) = "" Or _
          Tabla.Rows(i).Item("relacionM3_kwh").ToString = "0" Then
            mensaje_relacion_m3 += "El factor corrector no tiene valor para la lectura " & Tabla.Rows(i).Item("Fecha_medida").ToString & " \n"
          End If

          If Not Tabla.Columns.Contains("Diferencial") Then
            'añadimos la columna diferencial al dataset
            Tabla.Columns.Add("Diferencial")
            Tabla.Columns.Add("Diferencial_Seg")
            Tabla.Columns.Add("Diferencial_acum")
            Tabla.Columns.Add("Diferencial_acumM3")
          End If

          'recorreremos todo el dataset e iremos calculando el diferencial y añadiendo la columna al dataset
          For i = 0 To Tabla.Rows.Count - 1

            If i = 0 Then
              'rellenamos la columna diferencial del dataset con un 0
              Tabla.Rows(i).Item("Diferencial") = "0"
              Tabla.Rows(i).Item("Diferencial_Seg") = "0"
              Tabla.Rows(i).Item("Diferencial_acum") = "0" 'Tabla.Rows(i).Item("Total_Kwh").ToString '"0"
              Tabla.Rows(i).Item("Diferencial_acumM3") = "0"
              v_vol_ant = Convert.ToDecimal(0 & Tabla.Rows(i).Item("Total_Kwh").ToString) '* Tabla.Rows(i).Item("relacionM3_kwh").ToString
              v_lectura_ant = v_vol_ant
              'v_acum_kwh = v_acum_kwh + v_lectura_ant
              If Tabla.Rows(i).Item("Total_Kwh").ToString = Nothing Then
                primeroNulo = "S"
              End If
            Else

              'Si el caudal es nulo pasaremos al registro siguiente y ése no lo tendremos en cuenta
              If utiles.nullABlanco(Tabla.Rows(i).Item("Total_Kwh").ToString) = "" Then
                v_diferencial = 0
                v_segundos = 0
                v_diferencial_acum = v_diferencial_acum
                '03/08/2008 ncm calculamos el volumen diferencial en m3
                v_diferencial_acumm3 = v_diferencial_acum '* Tabla.Rows(i).Item("relacionM3_kwh").ToString
                v_lectura_ant = Convert.ToDecimal(0 & Tabla.Rows(i).Item("Total_Kwh").ToString)
                'añadimos los valores a la tabla
                Tabla.Rows(i).Item("Diferencial") = String.Format("{0:#,##0}", DBNull.Value)
                Tabla.Rows(i).Item("Diferencial_Seg") = String.Format("{0:#,##0.###}", DBNull.Value)
                Tabla.Rows(i).Item("Diferencial_acum") = String.Format("{0:#,##0}", v_acum_kwh)
                Tabla.Rows(i).Item("Diferencial_acumM3") = String.Format("{0:#,##0}", v_diferencial_acumm3)

              Else
                v_vol = Convert.ToDecimal(0 & Tabla.Rows(i).Item("Total_Kwh").ToString)
                If (primeroNulo = "S") Then
                  primeroNulo = "N"
                  v_diferencial = 0
                  v_diferencial_m3 = 0
                  v_segundos = 0
                  v_kwh = 0
                Else
                  v_segundos = DateDiff(DateInterval.Second, Tabla.Rows(i - 1).Item("Fecha_medida"), Tabla.Rows(i).Item("Fecha_medida"))

                  v_lectura_ant = v_vol

                  'comprobamos si existen incidencias
                  'si la incidencia es reseteo o cambio de contador se deberá tomar el valor del campo Reinicio Lectura Volumetrica
                  'si la incidencia es contador averiado o consumo negativo, el valor a tomar será el consumo volumétrico adicional
                  If ((Tabla.Rows(i).Item("idIncidenciaElectrica").ToString = "2") Or _
                      (Tabla.Rows(i).Item("idIncidenciaElectrica").ToString = "3")) Then
                    If Tabla.Rows(i).Item("COD_FUENTE_DATO").ToString = "05" Then
                      v_diferencial = (((Convert.ToDecimal(0 & Tabla.Rows(i).Item("Total_Kwh").ToString) - Convert.ToDecimal(0 & Tabla.Rows(i).Item("ReiniciolecturaElectrica").ToString)) + _
                      Convert.ToDecimal(0 & Tabla.Rows(i).Item("ConsumoElectricoAdicional").ToString))) * Tabla.Rows(i).Item("relacionM3_kwh").ToString
                      v_kwh = v_diferencial

                    End If
                  ElseIf (Tabla.Rows(i).Item("idIncidenciaElectrica").ToString = "1") Or _
                      (Tabla.Rows(i).Item("idIncidenciaElectrica").ToString = "4") Then
                    If utiles.nullABlanco(Tabla.Rows(i).Item("ConsumoElectricoAdicional").ToString) <> "" Then
                      v_diferencial = ((Convert.ToDecimal(0 & Tabla.Rows(i).Item("Total_Kwh").ToString) + Tabla.Rows(i).Item("ConsumoElectricoAdicional").ToString) - _
                      Convert.ToDecimal(0 & v_lectura_ant)) * Tabla.Rows(i).Item("relacionM3_kwh").ToString
                      v_kwh = v_diferencial
                    Else
                      v_diferencial = ((Convert.ToDecimal(0 & Tabla.Rows(i).Item("Total_Kwh").ToString) + Convert.ToDecimal(0 & Tabla.Rows(i).Item("ConsumoElectricoAdicional").ToString)) - _
                      Convert.ToDecimal(0 & v_lectura_ant)) * Tabla.Rows(i).Item("relacionM3_kwh").ToString
                      v_kwh = v_diferencial
                    End If
                  Else
                    v_diferencial = (v_vol - v_vol_ant) * Tabla.Rows(i).Item("relacionM3_kwh").ToString
                    v_kwh = (v_vol - v_vol_ant)
                  End If
                End If ' primera lectura nula

                v_vol_ant = v_vol
                'nos guardamos la lectura anterior para realizar el calculo bien por si hay lecturas nulas
                'v_lectura_ant = Tabla.Rows(i).Item("Total_Kwh").ToString
                'calculamos el diferencial acumulado
                v_acum_kwh = v_acum_kwh + v_kwh
                v_diferencial_acum = v_diferencial_acum + v_diferencial

                '03/08/2008 ncm calculamos el volumen diferencial en m3
                v_diferencial_m3 = v_diferencial '* Tabla.Rows(i).Item("relacionM3_kwh").ToString
                'si los segundos es cero no sacamos nada en el campo
                If v_segundos = 0 Then
                  v_diferencial_seg = 0
                Else
                  v_diferencial_seg = v_diferencial_m3 / v_segundos
                End If
                v_diferencial_acumm3 = v_diferencial_acum '* Tabla.Rows(i).Item("relacionM3_kwh").ToString
                'cargamos datos en la tabla, comentado porque éste es el diferencial en KWH y vamos a mostrarlo en m3
                'Tabla.Rows(i).Item("Diferencial") = String.Format("{0:#,##0.##}", v_diferencial)
                Tabla.Rows(i).Item("Diferencial") = String.Format("{0:#,##0}", v_diferencial_m3)
                Tabla.Rows(i).Item("Diferencial_Seg") = String.Format("{0:#,##0.###}", v_diferencial_seg)
                Tabla.Rows(i).Item("Diferencial_acum") = String.Format("{0:#,##0}", v_acum_kwh)
                Tabla.Rows(i).Item("Diferencial_acumm3") = String.Format("{0:#,##0}", v_diferencial_acumm3)
              End If 'total_kw nulo
            End If 'i = 0
          Next
          'si las variables de mensaje tienen valor las mostramos
          mensaje_final = ""
          If mensaje_idcontador <> "" Then
            mensaje_final += mensaje_idcontador
          End If
          If mensaje_relacion_m3 <> "" Then
            mensaje_final += " \n" & mensaje_relacion_m3
          End If
          If mensaje_final <> "" Then
            Alert(page, mensaje_final)
          End If
        End If
      ElseIf tipo = "Q" Then
        'deberemos calcular el volumen, siendo éste el caudal por el tiempo
        If Tabla.Rows.Count > 0 Then
          If Not Tabla.Columns.Contains("Diferencial") Then
            'añadimos la columna diferencial al dataset
            Tabla.Columns.Add("Diferencial")
            Tabla.Columns.Add("Diferencial_acum")
          End If
          'recorreremos todo el dataset e iremos calculando el diferencial y añadiendo la columna al dataset
          For i = 0 To Tabla.Rows.Count - 1

            If i = 0 Then
              'rellenamos la columna diferencial del dataset con un 0
              Tabla.Rows(i).Item("Diferencial") = "0"
              Tabla.Rows(i).Item("Diferencial_acum") = "0"
              If utiles.nullABlanco(Tabla.Rows(i).Item("Caudal_M3S").ToString) = "" Then
                primeroNulo = "S"
              End If
            Else
              v_caudal = Convert.ToDecimal(0 & Tabla.Rows(i).Item("Caudal_M3S").ToString)
              v_caudal_ant = Convert.ToDecimal(0 & Tabla.Rows(i - 1).Item("Caudal_M3S").ToString)
              v_caudal_medio = (v_caudal + v_caudal_ant) / 2
              'el tiempo deberá estar en segundos
              v_tiempo = Tabla.Rows(i).Item("Fecha_medida").ToString
              v_tiempo_ant = Tabla.Rows(i - 1).Item("Fecha_medida").ToString
              v_tiempo_medio = Math.Abs(Convert.ToInt32(DateDiff(DateInterval.Minute, v_tiempo_ant, v_tiempo) * 60))
              'lompartimos entre 1000 para pasarlo de a (m3)
              v_diferencial = (v_caudal_medio * v_tiempo_medio)

              'Si el caudal es nulo pasaremos al registro siguiente y ése no lo tendremos en cuenta
              If utiles.nullABlanco(Tabla.Rows(i).Item("Caudal_M3S").ToString) = "" Then
                v_diferencial_acum = v_diferencial_acum
                Tabla.Rows(i).Item("Diferencial") = String.Format("{0:#,##0}", DBNull.Value)
                Tabla.Rows(i).Item("Diferencial_acum") = String.Format("{0:#,##0}", DBNull.Value)
              Else
                If primeroNulo = "S" Then
                  primeroNulo = "N"
                Else
                  'calculamos el diferencial acumulado
                  v_diferencial_acum = v_diferencial_acum + v_diferencial
                  Tabla.Rows(i).Item("Diferencial") = String.Format("{0:#,##0}", v_diferencial)
                  Tabla.Rows(i).Item("Diferencial_acum") = String.Format("{0:#,##0}", v_diferencial_acum)
                End If
              End If
            End If
          Next
        End If
      ElseIf tipo = "H" Then

        If Tabla.Rows.Count > 0 Then
          Dim mensaje_codMotobomba As String = ""
          Dim mensaje_caudal_m3 As String = ""
          'cada registro que tenga el id contador nulo, lo guardaremos para mostrar un mensaje al usuario
          If utiles.nullABlanco(Tabla.Rows(i).Item("codigomotobomba").ToString) = "" Then
            mensaje_codMotobomba += "La lectura con fecha " & Tabla.Rows(i).Item("Fecha_medida").ToString & " no tiene motobomba relacionada \n"
          End If
          'si el factor corrector es nulo tb se mostrará sms
          If utiles.nullABlanco(Tabla.Rows(i).Item("caudal_lseg").ToString.ToString) = "" Then
            mensaje_caudal_m3 += "El caudal no tiene valor para la lectura " & Tabla.Rows(i).Item("Fecha_medida").ToString & " \n"
          End If
          If Not Tabla.Columns.Contains("Diferencial") Then
            'añadimos la columna diferencial al dataset
            Tabla.Columns.Add("Diferencial")
            Tabla.Columns.Add("Diferencial_Seg")
            Tabla.Columns.Add("Diferencial_Acum")
            Tabla.Columns.Add("Diferencial_Acum_horas")
          End If
          'recorreremos todo el dataset e iremos calculando el diferencial y añadiendo la columna al dataset
          For i = 0 To Tabla.Rows.Count - 1

            If i = 0 Then
              'rellenamos la columna diferencial del dataset con un 0
              Tabla.Rows(i).Item("Diferencial") = "0"
              Tabla.Rows(i).Item("Diferencial_Seg") = "0"
              Tabla.Rows(i).Item("Diferencial_Acum") = "0"
              Tabla.Rows(i).Item("Diferencial_Acum_horas") = "0"
              v_vol_ant = (Convert.ToDecimal(0 & Tabla.Rows(i).Item("HorasIntervalo").ToString) * 3600) * Convert.ToDecimal(0 & Tabla.Rows(i).Item("Caudal_LSeg").ToString) / 1000
              v_vol_ant_horas = Convert.ToDecimal(0 & Tabla.Rows(i).Item("HorasIntervalo").ToString)
              v_lectura_ant = v_vol_ant
              v_segundos = 0
              If Tabla.Rows(i).Item("HorasIntervalo").ToString = Nothing Then
                primeroNulo = "S"
              End If
            Else
              If (primeroNulo = "S") Then
                'comprobamos si el siguiente es nulo, si es así pasaremos al siguiente registro y sino empezaremos con el cálculo
                If utiles.nullABlanco(Tabla.Rows(i).Item("HorasIntervalo").ToString) <> "" Then
                  primeroNulo = "N"
                  v_diferencial = 0
                  v_segundos = 0
                  v_vol_ant = (Convert.ToDecimal(0 & Tabla.Rows(i).Item("HorasIntervalo").ToString) * 3600) * Convert.ToDecimal(0 & Tabla.Rows(i).Item("Caudal_LSeg").ToString) / 1000
                  v_vol_ant_horas = Convert.ToDecimal(0 & Tabla.Rows(i).Item("HorasIntervalo").ToString)
                Else
                  v_vol_ant = 0
                  v_lectura_ant = 0
                End If
              Else
                'comprobamos si existen incidencias
                '*************************--> obsoleto(19/05/2008)***********************-
                'si la incidencia es reseteo (6) o cambio de contador(7) se deberá tomar el valor del campo Reinicio Lectura Volumetrica 
                '************************* NUEVO ****************************************
                'si la incidencia es reseteo o cambio de contador  la fórmula es:
                '                   ((lecturam3(dia15)-reseteo(dia15) + consumo adic. (dia15)
                'si la incidencia es contador averiado yconsumo negativo (8) (5), la fñormula es :
                '                   (lecturaM3(i) + Consumovolumetricoadicional(i)) - lecturam3(i-1)

                'Si el caudal es nulo pasaremos al registro siguiente y ése no lo tendremos en cuenta
                If utiles.nullABlanco(Tabla.Rows(i).Item("HorasIntervalo").ToString) = "" Then
                  v_diferencial = 0
                  v_diferencial_horas = 0
                  v_diferencial_acum = v_diferencial_acum
                  v_diferencial_acum_horas = v_diferencial_acum_horas
                  'añadimos los valores a la tabla
                  Tabla.Rows(i).Item("Diferencial") = String.Format("{0:#,##0.##}", DBNull.Value)
                  Tabla.Rows(i).Item("Diferencial_Acum") = String.Format("{0:#,##0.##}", v_diferencial_acum)
                  Tabla.Rows(i).Item("Diferencial_Acum_horas") = String.Format("{0:#,##0.##}", v_diferencial_acum_horas)
                Else

                  v_vol = (Convert.ToDecimal(0 & Tabla.Rows(i).Item("HorasIntervalo").ToString) * 3600) * Convert.ToDecimal(0 & Tabla.Rows(i).Item("Caudal_LSeg").ToString) / 1000
                  v_vol_horas = Convert.ToDecimal(0 & Tabla.Rows(i).Item("HorasIntervalo").ToString)
                  v_segundos = DateDiff(DateInterval.Second, Tabla.Rows(i - 1).Item("Fecha_medida"), Tabla.Rows(i).Item("Fecha_medida"))
                  If (Tabla.Rows(i).Item("idincidenciaVolumetrica").ToString = "10") Or _
                      (Tabla.Rows(i).Item("idincidenciaVolumetrica").ToString = "11") Then
                    v_diferencial = ((v_vol - Convert.ToDecimal(0 & Tabla.Rows(i).Item("ReiniciolecturaVolumetrica").ToString)) + _
                    Convert.ToDecimal(0 & Tabla.Rows(i).Item("ConsumoVolumetricoAdicional").ToString))

                    v_diferencial_horas = ((v_vol_horas - Convert.ToDecimal(0 & Tabla.Rows(i).Item("ReiniciolecturaVolumetrica").ToString)) + _
                    Convert.ToDecimal(0 & Tabla.Rows(i).Item("ConsumoVolumetricoAdicional").ToString))
                  ElseIf _
                     (Tabla.Rows(i).Item("idincidenciaVolumetrica").ToString = "9") Or _
                     (Tabla.Rows(i).Item("idincidenciaVolumetrica").ToString = "12") Then
                    If utiles.nullABlanco(Tabla.Rows(i).Item("ConsumoVolumetricoAdicional").ToString) <> "" Then
                      v_diferencial = (v_vol + Tabla.Rows(i).Item("ConsumoVolumetricoAdicional").ToString) - _
                      Convert.ToDecimal(0 & v_lectura_ant)
                      v_diferencial_horas = (v_vol_horas + Tabla.Rows(i).Item("ConsumoVolumetricoAdicional").ToString) - _
                      Convert.ToDecimal(0 & v_lectura_ant_horas)
                    Else
                      v_diferencial = (v_vol + Convert.ToDecimal(0 & Tabla.Rows(i).Item("ConsumoVolumetricoAdicional").ToString)) - _
                      Convert.ToDecimal(0 & v_lectura_ant)
                      v_diferencial_horas = (v_vol_horas + Convert.ToDecimal(0 & Tabla.Rows(i).Item("ConsumoVolumetricoAdicional").ToString)) - _
                                                          Convert.ToDecimal(0 & v_lectura_ant_horas)
                    End If

                  Else
                    v_diferencial = v_vol - v_vol_ant
                    v_diferencial_horas = v_vol_horas - v_vol_ant_horas
                  End If
                End If 'primera lectura nula
                v_vol_ant = v_vol
                v_vol_ant_horas = v_vol_horas
                'nos guardamos la lectura anterior para realizar el calculo bien por si hay lecturas nulas
                v_lectura_ant = (Convert.ToDecimal(0 & Tabla.Rows(i).Item("HorasIntervalo").ToString) * 3600) * Convert.ToDecimal(0 & Tabla.Rows(i).Item("Caudal_LSeg").ToString) / 1000
                v_lectura_ant_horas = Convert.ToDecimal(0 & Tabla.Rows(i).Item("HorasIntervalo").ToString)
                'calculamos el diferencial acumulado
                v_diferencial_acum = v_diferencial_acum + v_diferencial
                v_diferencial_acum_horas = v_diferencial_acum_horas + v_diferencial_horas
                'pasamos el diferencial a m3 03/08/2008 ncm, para ello pasamos primero el diferencial a segundos porque lo tenemos en horas
                'v_diferencial_seg = v_diferencial * 3600
                'v_diferencial_m3 = v_diferencial_seg * Tabla.Rows(i).Item("HorasIntervalo")
                'añadimos los valores a la tabla
                'Tabla.Rows(i).Item("Diferencial") = String.Format("{0:#,##0.##}", v_diferencial)

                'si los segundos es cero no sacamos nada en el campo
                If v_segundos = 0 Then
                  v_diferencial_seg = 0
                Else
                  v_diferencial_seg = v_diferencial / v_segundos
                End If

                Tabla.Rows(i).Item("Diferencial") = String.Format("{0:#,##0.##}", v_diferencial)
                Tabla.Rows(i).Item("Diferencial_Seg") = String.Format("{0:#,##0.###}", v_diferencial_seg)
                Tabla.Rows(i).Item("Diferencial_Acum") = String.Format("{0:#,##0.##}", v_diferencial_acum)
                Tabla.Rows(i).Item("Diferencial_Acum_horas") = String.Format("{0:#,##0.##}", v_diferencial_acum_horas)
              End If

              End If
          Next
          'si las variables de mensaje tienen valor las mostramos
          mensaje_final = ""
          If mensaje_codMotobomba <> "" Then
            mensaje_final += mensaje_codMotobomba
            'Alert(Page, mensaje_idcontador)
          End If
          If mensaje_caudal_m3 <> "" Then
            mensaje_final += " \n" & mensaje_caudal_m3
            'Alert(Page, mensaje_relacion_m3)
          End If
          If mensaje_final <> "" Then
            Alert(page, mensaje_final)
          End If
        End If
      ElseIf tipo = "D" Then
        'ncm en este caso el diferencial = acumulado, por tanto el calculo será ir sumando y no restando
        If Tabla.Rows.Count > 0 Then
          If Not Tabla.Columns.Contains("Diferencial") Then
            'añadimos la columna diferencial al dataset
            Tabla.Columns.Add("Diferencial")
            Tabla.Columns.Add("Diferencial_Acum")
          End If
          'recorreremos todo el dataset e iremos calculando el diferencial y añadiendo la columna al dataset
          For i = 0 To Tabla.Rows.Count - 1

            'If i = 0 Then
            '    'rellenamos la columna diferencial del dataset con un 0
            '    Tabla.Rows(i).Item("Diferencial") = "0"
            '    Tabla.Rows(i).Item("Diferencial_Acum") = "0"
            '    v_vol_ant = (Convert.ToDecimal(0 & Tabla.Rows(i).Item("SuministroMensualM3").ToString))
            '    v_lectura_ant = v_vol_ant
            'Else
            'Si el caudal es nulo pasaremos al registro siguiente y ése no lo tendremos en cuenta
            If utiles.nullABlanco(Tabla.Rows(i).Item("SuministroMensualM3").ToString) = "" Then
              v_diferencial = 0
              v_diferencial_acum = v_diferencial_acum
              'añadimos los valores a la tabla
              Tabla.Rows(i).Item("Diferencial") = String.Format("{0:#,##0.##}", DBNull.Value)
              Tabla.Rows(i).Item("Diferencial_Acum") = String.Format("{0:#,##0.##}", v_diferencial_acum)
            Else

              v_vol = (Convert.ToDecimal(0 & Tabla.Rows(i).Item("SuministroMensualM3").ToString))
              v_diferencial = v_vol + v_vol_ant
            End If
            v_vol_ant = v_diferencial 'v_vol
            'nos guardamos la lectura anterior para realizar el calculo bien por si hay lecturas nulas
            v_lectura_ant = (Convert.ToDecimal(0 & Tabla.Rows(i).Item("SuministroMensualM3").ToString))
            'calculamos el diferencial acumulado
            v_diferencial_acum = v_diferencial_acum + v_diferencial
            Tabla.Rows(i).Item("Diferencial") = String.Format("{0:#,##0.##}", v_diferencial)
            Tabla.Rows(i).Item("Diferencial_Acum") = String.Format("{0:#,##0.##}", v_diferencial_acum)
            'End If
          Next
        End If

      End If

    End Sub
    Shared Sub CrearDatasetDiferencial(ByVal tipoElemen As String, ByVal codigoPVYCR As String, ByVal idelemento As String, ByVal page As Page, _
    ByVal FiltroNreg As String, ByVal FiltroNulas As Boolean, ByVal FiltroFechaFin As String, ByVal FiltroFechaIni As String, ByVal FiltrarCodFuentedato As String, _
    ByVal Filtro As String, ByVal ucPaginacion As Object, ByVal ReducirLec As Boolean, ByRef dstElementos As DataSet, ByRef sentenciaSel As String)
      'Criterios de filtrado
      Dim sFiltro As String = ""
      Dim sFiltroInci As String = ""
      Dim fechainicio, fechaFin As DateTime
      Dim Nreg As Integer = 0
      Dim i As Integer = 0
      Dim sentenciaInci As String = ""
      Dim mensaje_final As String = ""
      Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
      Dim comando As SqlCommand = New SqlCommand("", conexion)
      Dim daElementos As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
      'Dim dstElementos As DataSet = New System.Data.DataSet()
      Dim sentenciaOrder As String = ""
      Dim sentenciaSelCount As String = ""
      Dim vFiltroFechaIni As String = ""
      Dim vFiltroFechaFin As String = ""
      'RDF
      'Fecha: 26/02/2009
      'Se comprueba si el punto tiene telemedida

      utiles.Comprobar_Conexion_BD(page, conexion)
      'Se inserta el registro correspondiente
      comando.CommandText = "SELECT isnull(TipoSensor,'') FROM PVYCR_Puntos WHERE codigoPVYCR='" & codigoPVYCR & "'"
      Dim Telemedida As String
      'Dim sentenciaSel As String = ""

      Telemedida = comando.ExecuteScalar
      'Si es sin telemedida se amplia el año hidrológico
      If Telemedida = "" Then
        If DateTime.Now.Month < 10 Then
          fechainicio = "01/10/" & DateTime.Now.Year - 2
          fechaFin = DateTime.Today
        Else
          fechainicio = "01/10/" & DateTime.Now.Year - 1
          fechaFin = DateTime.Today
        End If

      Else
        'dependiendo del tipo seleccionaremos los datos de una tabla u otra
        'calculamos el año hidrológico que va desde el 01/10/añoactual - 1 hasta la fecha actual
        If DateTime.Now.Month < 10 Then
          fechainicio = "01/10/" & DateTime.Now.Year - 1
          fechaFin = DateTime.Today
        Else
          fechainicio = "01/10/" & DateTime.Now.Year
          fechaFin = DateTime.Today
        End If
      End If

      dstElementos.Clear()
      'ncm si hay filtro de fechas incrementamos un año para lo between
      If FiltroFechaFin <> "" Then
        vFiltroFechaFin = FiltroFechaFin & " 23:59:59"
      End If
      If FiltroFechaIni <> "" Then
        vFiltroFechaIni = FiltroFechaIni & " 00:00:00"
      End If
      If tipoElemen = "Q" Then
        'scripts de cliente para el calendario

        'imgCalFechaIniQ.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtfiltroFechaIni.ClientID & "'),'dd/mm/yyyy');")
        'imgCalFechaFinQ.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaFin.ClientID & "'),'dd/mm/yyyy');")
        'imgFfechamedida.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFfechamedida.ClientID & "'),'dd/mm/yyyy');")

        If FiltroNreg <> "" Then
          sentenciaSel = "SELECT top " & FiltroNreg & " d.CodigoPVYCR,d.idElementoMedida, d.Cod_Fuente_Dato,d.Fecha_Medida,Escala_M,d.Calado_M,d.Observaciones " & _
                             ",d.TipoObtencionCaudal,d.RegimenCurva,d.NumeroParada,d.Caudal_M3S,d.DUDA_CALIDAD, F.FUENTE_DATOS DescFuenteDato " & _
                             ", D.modificado FROM PVYCR_DatosAcequias D " & _
                             "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                             "where codigoPVYCR = '" & codigoPVYCR & "' and " & _
                            "idElementoMedida =  '" & idelemento & "' "
        Else
          sentenciaSel = "SELECT d.CodigoPVYCR,d.idElementoMedida, d.Cod_Fuente_Dato,d.Fecha_Medida,Escala_M,d.Calado_M,d.Observaciones " & _
             ",d.TipoObtencionCaudal,d.RegimenCurva,d.NumeroParada,d.Caudal_M3S,d.DUDA_CALIDAD, F.FUENTE_DATOS DescFuenteDato " & _
             ",d.modificado FROM PVYCR_DatosAcequias D " & _
             "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
              "where codigoPVYCR = '" & codigoPVYCR & "' and " & _
              "idElementoMedida =  '" & idelemento & "' "
        End If
        'si el parametro nulas = S es porque el usuario a marcado que aparezcan tb las lecturas nulas (todas las lecturas),
        ' si es N es porque sólo tienen que aparecer las lecturas que tengan valor
        If FiltroNulas = False Then
          sentenciaSel = sentenciaSel & " and d.caudal_M3S is not null "
        End If

        If (FiltroFechaFin <> "" And FiltroFechaIni <> "") Or FiltrarCodFuentedato <> "" Then
          If FiltrarCodFuentedato <> "" Then
            sFiltro = " and d.Cod_Fuente_Dato = '" & FiltrarCodFuentedato & "'"
          End If
          If FiltroFechaFin <> "" And FiltroFechaIni <> "" Then
            sFiltro = sFiltro & " and Fecha_medida between '" & vFiltroFechaIni & "' and '" & vFiltroFechaFin & "' "
          ElseIf FiltroFechaFin = "" And FiltroFechaIni <> "" Then
            Alert(page, "La Fecha Hasta no puede ser nula")
            sFiltro = ""
          ElseIf FiltroFechaFin <> "" And FiltroFechaIni = "" Then
            Alert(page, "La Fecha Desde no puede ser nula")
            sFiltro = ""
          End If
        ElseIf FiltroFechaFin = "" And FiltroFechaIni <> "" Then
          Alert(page, "La Fecha Hasta no puede ser nula")
          sFiltro = ""
        ElseIf FiltroFechaFin <> "" And FiltroFechaIni = "" Then
          Alert(page, "La Fecha Desde no puede ser nula")
          sFiltro = ""
        ElseIf FiltroFechaFin = "" And FiltroFechaIni = "" Then
          'ncm cometado el 29/10/2009 por petición usuario incidencia 159
          'If Telemedida = "" Then
          '    'RDF. Fecha: 03/03/2009
          '    FiltroFechaIni = fechainicio
          '    FiltroFechaFin = fechaFin
          'End If
          sFiltro = sFiltro & " and Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' "
        Else
          sFiltro = ""
        End If


        'RDF 20080714
        If Filtro <> "" Then
          sentenciaSel = sentenciaSel & Filtro
        End If

        sentenciaOrder = " order by codigoPVYCR, Fecha_Medida  desc, d.Cod_Fuente_dato, d.TipoObtencionCaudal "

        If sFiltro <> "" Then
          sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
          sentenciaSelCount = sentenciaSelCount & sFiltro
        Else
          sentenciaSel = sentenciaSel & " and Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' " & sentenciaOrder
        End If

        daElementos.SelectCommand.CommandText = sentenciaSel
        'datos acequias

        'IPIM 26/11/2008: Descomentamos para paginar
        daElementos.Fill(dstElementos, "TablaAcequias")
        'Cálculo del número de páginas
        'ordenamos las lecturas en un dataview por fecha

        Dim txtComando As String = ""
        txtComando = daElementos.SelectCommand.CommandText
        txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))
        'IPIM 26/11/2008: Descomentamos para paginar
        'ucPaginacionA.calcularPags(txtComando)
        If Not ucPaginacion Is Nothing Then
          ucPaginacion.calcularPags(txtComando)
        End If
        '        ElseIf Request.QueryString("tipo").ToString = "E" Then
        'dstElementos.Tables("TablaAcequias").DefaultView.Sort = "CodigoPVYCR, Fecha_medida"
        Dim Tabla As DataTable
        Tabla = dstElementos.Tables("TablaAcequias").Clone()
        For i = dstElementos.Tables("TablaAcequias").Rows.Count - 1 To 0 Step -1
          Tabla.Rows.Add(dstElementos.Tables("TablaAcequias").Rows(i).ItemArray)
        Next
        dstElementos.Tables.Remove(dstElementos.Tables("TablaAcequias"))
        dstElementos.Tables.Add(Tabla)

        'obtenerVolumenDiferencial("Q", dstElementos.Tables("TablaAcequias"), page, mensaje_final)
        'obtenerCaudalAcumulado()
        'If ReducirLec Then
        '    'IPIM 26/09/2008: Eliminar registros que no cumplen el porcentaje
        '    Dim dstNuevo As DataSet
        '    dstNuevo = utiles.QuitarRegistrosSegunPorcentaje(dstElementos, "TablaAcequias", "Caudal_M3s", 5)
        '    dstElementos = dstNuevo.Copy
        'End If

        'CrearTablaParaRepeater("TablaAcequias", "Q")

      ElseIf tipoElemen = "E" Then

        'scripts de cliente para el calendario
        'imgCalFechaIniE.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaIniE.ClientID & "'),'dd/mm/yyyy');")
        'imgCalFechaFinE.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaFinE.ClientID & "'),'dd/mm/yyyy');")
        'imgFfechamedidaE.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFfechamedidaE.ClientID & "'),'dd/mm/yyyy');")
        '------------------------------------------------------------------------------------------------------------------------------------
        'NCM: 1º comprobaremos si el filtro de nº de regitrsos tiene valor, ya que si tiene valor le tendremos que
        'poner el top a la select. Éste filtro junto con el de mostrar 1 registro por día y el de mostrar 1 de cada X son excluyentes.
        '1) si quiieren N registros haremos el top de la select
        '2) si quieren un registro por día, haremos un bucle para obtener un registro cada día, teniendo en cuenta los demás filtros
        '3) si kieren uno de cada X
        '------------------------------------------------------------------------------------------------------------------------------------
        'CASO1
        If FiltroNreg <> "" Then
          sentenciaSel = "SELECT top " & FiltroNreg & " D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
                         "D.LecturaI,D.LecturaII, D.LecturaIII,D.total_Kwh, D.total_kvar, D.Funciona, D.Observaciones, D.idIncidenciaelectrica, " & _
                         "D.ConsumoElectricoAdicional, D.ReinicioLecturaElectrica, D.Justificacion, " & _
                         "IV.descripcion descIncElec, F.FUENTE_DATOS DescFuenteDato,isnull(C.E_FCorrectorContActiva,0) * isnull(C.E_RelacionM3_KWH,0) relacionM3_Kwh, C.IdContador" & _
                         ", D.modificado FROM PVYCR_DatosAlimentacion D " & _
                         "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaElectrica " & _
                         "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                         "LEFT OUTER JOIN  PVYCR_Contadores_ElementosMedida E ON E.codigoPVYCR = d.codigoPVYCR and " & _
                         "E.idelementoMedida = D.idelementomedida and D.Fecha_Medida BETWEEN E.FechaInicio AND ISNULL(E.FECHAFINAL, GETDATE()) " & _
                         "LEFT OUTER JOIN  PVYCR_Contadores C ON C.idContador = E.idContador and " & _
                         "C.FechaRevision = E.fechaRevision and c.tipocontador = 'E' " & _
                         "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' "
        ElseIf FiltroNreg = "" Then
          sentenciaSel = "SELECT  D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
                          "D.LecturaI,D.LecturaII, D.LecturaIII,D.total_Kwh, D.total_kvar, D.Funciona, D.Observaciones, D.idIncidenciaelectrica, " & _
                          "D.ConsumoElectricoAdicional, D.ReinicioLecturaElectrica, D.Justificacion, " & _
                          "IV.descripcion descIncElec, F.FUENTE_DATOS DescFuenteDato,isnull(C.E_FCorrectorContActiva,0) * isnull(C.E_RelacionM3_KWH,0) relacionM3_Kwh, C.IdContador " & _
                          ",D.modificado FROM PVYCR_DatosAlimentacion D " & _
                          "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaElectrica " & _
                          "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                                  "LEFT OUTER JOIN  PVYCR_Contadores_ElementosMedida E ON E.codigoPVYCR = d.codigoPVYCR and " & _
                         "E.idelementoMedida = D.idelementomedida and D.Fecha_Medida BETWEEN E.FechaInicio AND ISNULL(E.FECHAFINAL, GETDATE()) " & _
                         "LEFT OUTER JOIN  PVYCR_Contadores C ON C.idContador = E.idContador and " & _
                         "C.FechaRevision = E.fechaRevision and c.tipocontador = 'E' " & _
                         "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' "

        End If
        If sentenciaSel <> "" Then
          'si el parametro nulas = S es porque el usuario a marcado que aparezcan tb las lecturas nulas (todas las lecturas),
          ' si es N es porque sólo tienen que aparecer las lecturas que tengan valor
          If FiltroNulas = False Then

            sentenciaSel = sentenciaSel & " and d.Total_kwh is not null "

          End If

          If (FiltroFechaFin <> "" And FiltroFechaIni <> "") Or FiltrarCodFuentedato <> "" Then
            If FiltrarCodFuentedato <> "" Then
              sFiltro = " and d.Cod_Fuente_Dato = '" & FiltrarCodFuentedato & "'"
            End If
            If FiltroFechaFin <> "" And FiltroFechaIni <> "" Then
              sFiltro = sFiltro & " and d.Fecha_medida between '" & vFiltroFechaIni & "' and '" & vFiltroFechaFin & "'"
            ElseIf FiltroFechaFin = "" And FiltroFechaIni <> "" Then
              Alert(page, "La Fecha Hasta no puede ser nula")
              sFiltro = ""
            ElseIf FiltroFechaFin <> "" And FiltroFechaIni = "" Then
              Alert(page, "La Fecha Desde no puede ser nula")
              sFiltro = ""
            End If
          ElseIf FiltroFechaFin = "" And FiltroFechaIni <> "" Then
            Alert(page, "La Fecha Hasta no puede ser nula")
            sFiltro = ""
          ElseIf FiltroFechaFin <> "" And FiltroFechaIni = "" Then
            Alert(page, "La Fecha Desde no puede ser nula")
            sFiltro = ""
          ElseIf FiltroFechaFin = "" And FiltroFechaIni = "" Then
            'ncm cometado el 29/10/2009 por petición usuario incidencia 159
            'If Telemedida = "" Then
            '    'RDF. Fecha: 03/03/2009
            '    FiltroFechaIni = fechainicio
            '    FiltroFechaFin = fechaFin
            'End If
            sFiltro = sFiltro & " and d.Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' "
          Else
            sFiltro = ""
          End If

          'RDF 20080714
          If Filtro <> "" Then
            'ncm 22/06/2009
            sentenciaSel = sentenciaSel & Filtro
          Else

            'ncm 22/06/2009
            'sentenciaInci = " AND NOT EXISTS (SELECT D1.CODIGOPVYCR FROM PVYCR_DATOSALIMENTACION D1 where d.codigopvycr=d1.codigopvycr and " & _
            '    "d.idelementomedida = d1.idelementomedida and d.cod_fuente_dato = d1.cod_fuente_dato and d.fecha_medida = d1.fecha_medida and " & _
            '    "d1.cod_fuente_dato <> '05' and (d1.idincidenciaelectrica = 2 or d1.idincidenciaelectrica=3) " & sFiltroInci & ") "
            sentenciaSel = sentenciaSel '& sentenciaInci
          End If

          sentenciaOrder = " order by D.codigoPVYCR, D.Fecha_Medida desc"


          If sFiltro <> "" Then
            sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
          Else
            sentenciaSel = sentenciaSel & " and Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' " & sentenciaOrder
          End If


          daElementos.SelectCommand.CommandText = sentenciaSel

          'IPIM 26/11/2008
          'daElementos.Fill(dstElementos, (CInt(ucPaginacionE.lblPaginatext) - 1) * ucPaginacionE.pageSize, ucPaginacionE.pageSize, "TablaAlimentacion")
          daElementos.Fill(dstElementos, "TablaAlimentacion")

        End If

        'Cálculo del número de páginas
        Dim txtComando As String = ""
        txtComando = daElementos.SelectCommand.CommandText
        txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))
        If Not ucPaginacion Is Nothing Then
          ucPaginacion.calcularPags(txtComando)
        End If

        '        ElseIf Request.QueryString("tipo").ToString = "V" Then

        'ncm 14/07/2008
        Dim Tabla As DataTable
        Tabla = dstElementos.Tables("TablaAlimentacion").Clone()
        For i = dstElementos.Tables("TablaAlimentacion").Rows.Count - 1 To 0 Step -1
          Tabla.Rows.Add(dstElementos.Tables("TablaAlimentacion").Rows(i).ItemArray)
        Next
        dstElementos.Tables.Remove(dstElementos.Tables("TablaAlimentacion"))
        dstElementos.Tables.Add(Tabla)
        'ncm 13/09/2009
        QuitarRegistrosSegunFuenteDato("E", dstElementos)
        'obtenerVolumenDiferencial("E", dstElementos.Tables("TablaAlimentacion"), page, mensaje_final)
        'obtenerVolumenElectricoAcumulado()
        'If ReducirLec Then
        '    'IPIM 26/09/2008: Eliminar registros que no cumplen el porcentaje
        '    Dim dstNuevo As DataSet
        '    dstNuevo = utiles.QuitarRegistrosSegunPorcentaje(dstElementos, "TablaAlimentacion", "Total_Kwh", 5)
        '    dstElementos = dstNuevo.Copy
        'End If

        'CrearTablaParaRepeater("TablaAlimentacion", "E")

      ElseIf tipoElemen = "V" Then

        'scripts de cliente para el calendario
        'imgCalFechaIniV.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaIniV.ClientID & "'),'dd/mm/yyyy');")
        'imgCalFechaFinV.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaFinV.ClientID & "'),'dd/mm/yyyy');")
        'imgFfechamedidaM.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFfechamedidaM.ClientID & "'),'dd/mm/yyyy');")

        If FiltroNreg <> "" Then
          sentenciaSel = "SELECT top " & FiltroNreg & " D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
                         "D.LecturaContador_M3, D.Funciona, D.Justificacion, D.Observaciones, D.idIncidenciaVolumetrica, " & _
                         "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, D.CaudalMedido, " & _
                         "IV.descripcion DescIncVol, F.FUENTE_DATOS DescFuenteDato " & _
                         ",d.modificado FROM PVYCR_DatosMotores D " & _
                         "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
                         "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                         "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' "


        Else
          sentenciaSel = "SELECT D.CodigoPVYCR, D.Cod_Fuente_Dato, D.Fecha_Medida, D.idElementoMedida, " & _
                         "D.LecturaContador_M3, D.Funciona, D.Justificacion, D.Observaciones, D.idIncidenciaVolumetrica, " & _
                         "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica,D.CaudalMedido, " & _
                         "IV.descripcion DescIncVol, F.FUENTE_DATOS DescFuenteDato " & _
                         ",d.modificado FROM PVYCR_DatosMotores D " & _
                         "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
                          "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                         "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' "

        End If
        'si el parametro nulas = S es porque el usuario a marcado que aparezcan tb las lecturas nulas (todas las lecturas),
        ' si es N es porque sólo tienen que aparecer las lecturas que tengan valor
        If FiltroNulas = False Then
          sentenciaSel = sentenciaSel & " and d.lecturacontador_m3 is not null "
        End If

        If (FiltroFechaFin <> "" And FiltroFechaIni <> "") Or FiltrarCodFuentedato <> "" Then
          If FiltrarCodFuentedato <> "" Then
            sFiltro = " and d.Cod_Fuente_Dato = '" & FiltrarCodFuentedato & "'"
          End If
          If FiltroFechaFin <> "" And FiltroFechaIni <> "" Then
            sFiltro = sFiltro & " and d.Fecha_medida between '" & vFiltroFechaIni & "' and '" & vFiltroFechaFin & "'"
          ElseIf FiltroFechaFin = "" And FiltroFechaIni <> "" Then
            Alert(page, "La Fecha Hasta no puede ser nula")
            sFiltro = ""
          ElseIf FiltroFechaFin <> "" And FiltroFechaIni = "" Then
            Alert(page, "La Fecha Desde no puede ser nula")
            sFiltro = ""
          End If
        ElseIf FiltroFechaFin = "" And FiltroFechaIni <> "" Then
          Alert(page, "La Fecha Hasta no puede ser nula")
          sFiltro = ""
        ElseIf FiltroFechaFin <> "" And FiltroFechaIni = "" Then
          Alert(page, "La Fecha Desde no puede ser nula")
          sFiltro = ""
        ElseIf FiltroFechaFin = "" And FiltroFechaIni = "" Then
          'ncm cometado el 29/10/2009 por petición usuario incidencia 159
          'If Telemedida = "" Then
          '    'RDF. Fecha: 03/03/2009
          '    FiltroFechaIni = fechainicio
          '    FiltroFechaFin = fechaFin
          'End If
          sFiltro = sFiltro & " and d.Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' "
        Else
          sFiltro = ""
        End If
        'sFiltroInci = sFiltro.Replace("d.", "d1.")
        sentenciaOrder = " order by D.codigoPVYCR, D.Fecha_Medida desc"

        'RDF 20080716
        If Filtro <> "" Then
          sentenciaSel = sentenciaSel & Filtro '& sentenciaInci
        Else
          sentenciaSel = sentenciaSel '& sentenciaInci
        End If


        If sFiltro <> "" Then
          sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
        Else
          sentenciaSel = sentenciaSel & " and Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' " & sentenciaOrder
        End If

        daElementos.SelectCommand.CommandText = sentenciaSel
        'datos volumétricos
        'IPIM 26/11/2008
        'daElementos.Fill(dstElementos, (CInt(ucPaginacionV.lblPaginatext) - 1) * ucPaginacionV.pageSize, ucPaginacionV.pageSize, "TablaMotores")
        daElementos.Fill(dstElementos, "TablaMotores")
        'Cálculo del número de páginas
        Dim txtComando As String = ""
        txtComando = daElementos.SelectCommand.CommandText
        txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))
        If Not ucPaginacion Is Nothing Then
          ucPaginacion.calcularPags(txtComando)
        End If

        'ncm 14/07/2008
        Dim Tabla As DataTable
        Tabla = dstElementos.Tables("TablaMotores").Clone()
        For i = dstElementos.Tables("TablaMotores").Rows.Count - 1 To 0 Step -1
          Tabla.Rows.Add(dstElementos.Tables("TablaMotores").Rows(i).ItemArray)
        Next
        dstElementos.Tables.Remove(dstElementos.Tables("TablaMotores"))
        dstElementos.Tables.Add(Tabla)
        'ncm 13/09/2009
        QuitarRegistrosSegunFuenteDato("V", dstElementos)
        'obtenerVolumenDiferencial("V", dstElementos.Tables("TablaMotores"), page, mensaje_final)
        ''obtenerVolumenDiferencial("V")
        'obtenerVolumenAcumulado()
        'If ReducirLec Then
        '    'IPIM 26/09/2008: Eliminar registros que no cumplen el porcentaje
        '    Dim dstNuevo As DataSet
        '    dstNuevo = utiles.QuitarRegistrosSegunPorcentaje(dstElementos, "TablaMotores", "LecturaContador_M3", 5)
        '    dstElementos = dstNuevo.Copy
        'End If

        'CrearTablaParaRepeater("TablaMotores", "V")

      ElseIf tipoElemen = "H" Then

        'scripts de cliente para el calendario
        'ImgCalFechaIniH.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltrofechaIniH.ClientID & "'),'dd/mm/yyyy');")
        'ImgCalFechaFinH.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaFinH.ClientID & "'),'dd/mm/yyyy');")
        'imgFFechaH.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFFechaMedidaH.ClientID & "'),'dd/mm/yyyy');")

        If FiltroNreg <> "" Then
          'NCM descomentar cuando quieran calcular con incidencias

          'sentenciaSel = "SELECT top " & FiltroNReg & " D.CodigoPVYCR, D.Cod_Fuente_Dato,D.Fecha_Medida," & _
          '                "D.HorasIntervalo, D.idElementoMedida, " & _
          '                "D.Funciona, D.Observaciones, D.idIncidenciaVolumetrica, " & _
          '                "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, " & _
          '                "IV.descripcion descIncVol, F.FUENTE_DATOS DescFuenteDato, M.Caudal_LSeg " & _
          '                "FROM PVYCR_DatosHorometros D " & _
          '                "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
          '                "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
          '                "INNER JOIN  PVYCR_ElementosMedida_MotoBombas E ON E.codigoPVYCR = d.codigoPVYCR and " & _
          '                "E.idelementoMedida = D.idelementomedida " & _
          '                "INNER JOIN  PVYCR_Motobombas M ON M.codigoMotobomba = E.codigoMotobomba and " & _
          '                "M.FechaRevision = E.fechaRevision " & _
          '                "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' "
          'RDF
          'Fecha: 01/04/2009
          'solución a la duplicidad de lecturas
          sentenciaSel = "SELECT TOP " & FiltroNreg & "  D2.CodigoPVYCR, D2.Cod_Fuente_Dato, D2.Fecha_Medida, D2.HorasIntervalo, D2.idElementoMedida, D2.Funciona, D2.Observaciones, " & _
                         "  D2.idIncidenciaVolumetrica, D2.ConsumoVolumetricoAdicional, D2.ReinicioLecturaVolumetrica, D2.descIncVol,  " & _
                         " D2.DescFuenteDato, D2.Caudal_LSeg, D2.codigoMotobomba " & _
                         "  FROM " & _
                          " (SELECT  D.CodigoPVYCR, D.Cod_Fuente_Dato,D.Fecha_Medida," & _
                          "D.HorasIntervalo, D.idElementoMedida, " & _
                          "D.Funciona,substring(D.Observaciones,0,512) as Observaciones, D.idIncidenciaVolumetrica, " & _
                          "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, " & _
                          "IV.descripcion descIncVol, F.FUENTE_DATOS DescFuenteDato,  C.Caudal_LSeg,C.codigoMotobomba " & _
                          ",d.modificado FROM PVYCR_DatosHorometros D " & _
                          "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
                          "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                          "INNER JOIN  PVYCR_ElementosMedida_MotoBombas E ON E.codigoPVYCR = d.codigoPVYCR and " & _
                         "E.idelementoMedida = D.idelementomedida " & _
                         "INNER JOIN  PVYCR_Motobombas C ON C.codigoMotobomba = E.codigoMotobomba and " & _
                         "C.FechaRevision = E.fechaRevision  " & _
                         "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' AND D.Fecha_Medida BETWEEN C.FechaInicial AND ISNULL(C.FECHAFIN, GETDATE()) " & _
                          " UNION " & _
                          "SELECT  D.CodigoPVYCR, D.Cod_Fuente_Dato,D.Fecha_Medida," & _
                          "D.HorasIntervalo, D.idElementoMedida, " & _
                          "D.Funciona, substring(D.Observaciones,0,512) as Observaciones, D.idIncidenciaVolumetrica, " & _
                          "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, " & _
                          "IV.descripcion descIncVol, F.FUENTE_DATOS DescFuenteDato, NULL AS Caudal_LSeg,NULL  AS codigoMotobomba " & _
                          ",d.modificado FROM PVYCR_DatosHorometros D " & _
                          "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
                          "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                          "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "'  " & _
                          " AND NOT EXISTS(SELECT * FROM dbo.PVYCR_ElementosMedida_MotoBombas AS E WHERE E.codigoPVYCR = D.CodigoPVYCR AND E.idElementoMedida = D.idElementoMedida )) as D2 " & _
                          " WHERE (1=1) "

        Else
          'RDF
          'Fecha: 01/04/2009
          'solución a la duplicidad de lecturas

          sentenciaSel = "SELECT  D2.CodigoPVYCR, D2.Cod_Fuente_Dato, D2.Fecha_Medida, D2.HorasIntervalo, D2.idElementoMedida, D2.Funciona, D2.Observaciones, " & _
                         "  D2.idIncidenciaVolumetrica, D2.ConsumoVolumetricoAdicional, D2.ReinicioLecturaVolumetrica, D2.descIncVol,  " & _
                         " D2.DescFuenteDato, D2.Caudal_LSeg, D2.codigoMotobomba, d2.modificado " & _
                         "  FROM " & _
                          " (SELECT  D.CodigoPVYCR, D.Cod_Fuente_Dato,D.Fecha_Medida," & _
                          "D.HorasIntervalo, D.idElementoMedida, " & _
                          "D.Funciona, substring(D.Observaciones,0,512) as Observaciones, D.idIncidenciaVolumetrica, " & _
                          "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, " & _
                          "IV.descripcion descIncVol, F.FUENTE_DATOS DescFuenteDato,  C.Caudal_LSeg,C.codigoMotobomba " & _
                          ",d.modificado FROM PVYCR_DatosHorometros D " & _
                          "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
                          "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                          "INNER JOIN  PVYCR_ElementosMedida_MotoBombas E ON E.codigoPVYCR = d.codigoPVYCR and " & _
                         "E.idelementoMedida = D.idelementomedida " & _
                         "INNER JOIN  PVYCR_Motobombas C ON C.codigoMotobomba = E.codigoMotobomba and " & _
                         "C.FechaRevision = E.fechaRevision  " & _
                         "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "' AND D.Fecha_Medida BETWEEN C.FechaInicial AND ISNULL(C.FECHAFIN, GETDATE()) " & _
                          " UNION " & _
                          "SELECT  D.CodigoPVYCR, D.Cod_Fuente_Dato,D.Fecha_Medida," & _
                          "D.HorasIntervalo, D.idElementoMedida, " & _
                          "D.Funciona,substring(D.Observaciones,0,512) as Observaciones, D.idIncidenciaVolumetrica, " & _
                          "D.ConsumoVolumetricoAdicional, D.ReinicioLecturaVolumetrica, " & _
                          "IV.descripcion descIncVol, F.FUENTE_DATOS DescFuenteDato,  NULL AS Caudal_LSeg,NULL AS codigoMotobomba " & _
                          ",d.modificado FROM PVYCR_DatosHorometros D " & _
                          "LEFT OUTER JOIN  PVYCR_incidencias IV ON Iv.idIncidencia=D.idIncidenciaVolumetrica " & _
                          "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                          "where D.idElementoMedida = '" & idelemento & "' and D.CodigoPVYCR = '" & codigoPVYCR & "'  " & _
                          " AND NOT EXISTS(SELECT * FROM dbo.PVYCR_ElementosMedida_MotoBombas AS E WHERE E.codigoPVYCR = D.CodigoPVYCR AND E.idElementoMedida = D.idElementoMedida )) as D2 " & _
                          " WHERE (1=1) "
        End If


        'si el parametro nulas = S es porque el usuario a marcado que aparezcan tb las lecturas nulas (todas las lecturas),
        ' si es N es porque sólo tienen que aparecer las lecturas que tengan valor
        If FiltroNulas = False Then
          sentenciaSel = sentenciaSel & " and d2.horasintervalo is not null "
        End If

        If (FiltroFechaFin <> "" And FiltroFechaIni <> "") Or FiltrarCodFuentedato <> "" Then
          If FiltrarCodFuentedato <> "" Then
            sFiltro = " and D2.Cod_Fuente_Dato = '" & FiltrarCodFuentedato & "'"
          End If
          If FiltroFechaFin <> "" And FiltroFechaIni <> "" Then
            sFiltro = sFiltro & " and D2.Fecha_Medida between '" & vFiltroFechaIni & "' and '" & vFiltroFechaFin & "' "
          ElseIf FiltroFechaFin = "" And FiltroFechaIni <> "" Then
            Alert(page, "La Fecha Desde no puede ser nula")
            sFiltro = ""
          ElseIf FiltroFechaFin <> "" And FiltroFechaIni = "" Then
            Alert(page, "La Fecha Hasta no puede ser nula")
            sFiltro = ""
          End If
        ElseIf FiltroFechaFin = "" And FiltroFechaIni <> "" Then
          Alert(page, "La Fecha Hasta no puede ser nula")
          sFiltro = ""
        ElseIf FiltroFechaFin <> "" And FiltroFechaIni = "" Then
          Alert(page, "La Fecha Desde no puede ser nula")
          sFiltro = ""
        ElseIf FiltroFechaFin = "" And FiltroFechaIni = "" Then
          'ncm cometado el 29/10/2009 por petición usuario incidencia 159
          'If Telemedida = "" Then
          '    'RDF. Fecha: 03/03/2009
          '    FiltroFechaIni = fechainicio
          '    FiltroFechaFin = fechaFin
          'End If
          sFiltro = sFiltro & " and D2.Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' "
        Else
          sFiltro = ""

        End If

        sentenciaOrder = " order by D2.codigoPVYCR, D2.Fecha_medida desc"
        sentenciaInci = " AND NOT EXISTS (SELECT D1.CODIGOPVYCR FROM PVYCR_DATOSHOROMETROS D1 where d2.codigopvycr=d1.codigopvycr and " & _
                      "d2.idelementomedida = d1.idelementomedida and d2.cod_fuente_dato = d1.cod_fuente_dato and d2.fecha_medida = d1.fecha_medida and (d1.cod_fuente_dato <> '05' and d1.idincidenciavolumetrica in (10,11)) ) "

        'RDF 20080921
        If Filtro <> "" Then
          sentenciaSel = sentenciaSel & Filtro
        Else
          sentenciaSel = sentenciaSel & sentenciaInci
        End If


        If sFiltro <> "" Then
          sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
        Else
          sentenciaSel = sentenciaSel & " and D2.Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' " & sentenciaOrder
        End If

        daElementos.SelectCommand.CommandText = sentenciaSel
        'datos horómetros
        'IPIM 26/11/2008
        'daElementos.Fill(dstElementos, (CInt(ucPaginacionH.lblPaginatext) - 1) * ucPaginacionH.pageSize, ucPaginacionH.pageSize, "TablaHorometros")
        daElementos.Fill(dstElementos, "TablaHorometros")
        'dstElementos.Tables("TablaHorometros").DefaultView.Sort = "codigoPVYCR, Fecha_medida"
        'Cálculo del número de páginas
        Dim txtComando As String = ""
        txtComando = daElementos.SelectCommand.CommandText
        txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))
        If Not ucPaginacion Is Nothing Then
          ucPaginacion.calcularPags(txtComando)
        End If
        'ncm 14/07/2008
        Dim Tabla As DataTable
        Tabla = dstElementos.Tables("TablaHorometros").Clone()
        For i = dstElementos.Tables("TablaHorometros").Rows.Count - 1 To 0 Step -1
          Tabla.Rows.Add(dstElementos.Tables("TablaHorometros").Rows(i).ItemArray)
        Next
        dstElementos.Tables.Remove(dstElementos.Tables("TablaHorometros"))
        dstElementos.Tables.Add(Tabla)

        'obtenerVolumenDiferencial("H", dstElementos.Tables("TablaHorometros"), page, mensaje_final)
        'obtenerVolumenHorometroAcumulado()

        'If ReducirLec Then
        '    'IPIM 26/09/2008: Eliminar registros que no cumplen el porcentaje
        '    Dim dstNuevo As DataSet
        '    dstNuevo = utiles.QuitarRegistrosSegunPorcentaje(dstElementos, "TablaHorometros", "HorasIntervalo", 5)
        '    dstElementos = dstNuevo.Copy
        'End If

        'CrearTablaParaRepeater("TablaHorometros", "H")

      ElseIf tipoElemen = "D" Then
        'scripts de cliente para el calendario

        'imgCalFechaIniD.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaIniD.ClientID & "'),'dd/mm/yyyy');")
        'imgCalFechaFinD.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFiltroFechaFinD.ClientID & "'),'dd/mm/yyyy');")
        'imgFfechamedidaD.Attributes.Add("onclick", "dxcal(this,document.getElementById('" & txtFfechamedidaD.ClientID & "'),'dd/mm/yyyy');")

        If FiltroNreg <> "" Then
          sentenciaSel = "SELECT top " & FiltroNreg & " d.CodigoPVYCR,d.idElementoMedida, d.Cod_Fuente_Dato,d.Fecha_Medida,suministroMensualM3,d.Observaciones, " & _
                             " F.FUENTE_DATOS DescFuenteDato " & _
                             "FROM PVYCR_DatosSuministros D " & _
                             "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                             "where codigoPVYCR = '" & codigoPVYCR & "' and " & _
                            "idElementoMedida =  '" & idelemento & "' "
        Else
          sentenciaSel = "SELECT d.CodigoPVYCR,d.idElementoMedida, d.Cod_Fuente_Dato,d.Fecha_Medida,suministroMensualM3,d.Observaciones, " & _
                             " F.FUENTE_DATOS DescFuenteDato " & _
                             "FROM PVYCR_DatosSuministros D " & _
                             "INNER JOIN  FUENTES_DE_DATOS F ON F.Cod_Fuente_dato=D.Cod_Fuente_dato " & _
                             "where codigoPVYCR = '" & codigoPVYCR & "' and " & _
                            "idElementoMedida =  '" & idelemento & "' "
        End If
        'si el parametro nulas = S es porque el usuario a marcado que aparezcan tb las lecturas nulas (todas las lecturas),
        ' si es N es porque sólo tienen que aparecer las lecturas que tengan valor
        If FiltroNulas = False Then
          sentenciaSel = sentenciaSel & " and d.suministromensualm3 is not null "
        End If


        If (FiltroFechaFin <> "" And FiltroFechaIni <> "") Or FiltrarCodFuentedato <> "" Then
          If FiltrarCodFuentedato <> "" Then
            sFiltro = " and d.Cod_Fuente_Dato = '" & FiltrarCodFuentedato & "'"
          End If
          If FiltroFechaFin <> "" And FiltroFechaIni <> "" Then
            sFiltro = sFiltro & " and Fecha_medida between '" & vFiltroFechaIni & "' and '" & vFiltroFechaFin & "'"
          ElseIf FiltroFechaFin = "" And FiltroFechaIni <> "" Then
            Alert(page, "La Fecha Hasta no puede ser nula")
            sFiltro = ""
          ElseIf FiltroFechaFin <> "" And FiltroFechaIni = "" Then
            Alert(page, "La Fecha Desde no puede ser nula")
            sFiltro = ""
          End If
        ElseIf FiltroFechaFin = "" And FiltroFechaIni <> "" Then
          Alert(page, "La Fecha Hasta no puede ser nula")
          sFiltro = ""
        ElseIf FiltroFechaFin <> "" And FiltroFechaIni = "" Then
          Alert(page, "La Fecha Desde no puede ser nula")
          sFiltro = ""
        ElseIf FiltroFechaFin = "" And FiltroFechaIni = "" Then
          'ncm cometado el 29/10/2009 por petición usuario incidencia 159
          'If Telemedida = "" Then
          '    'RDF. Fecha: 03/03/2009
          '    FiltroFechaIni = fechainicio
          '    FiltroFechaFin = fechaFin
          'End If
          sFiltro = sFiltro & " and Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' "
        Else
          sFiltro = ""
        End If


        'RDF 20080714
        If Filtro <> "" Then
          sentenciaSel = sentenciaSel & Filtro
        End If

        sentenciaOrder = " order by codigoPVYCR, Fecha_Medida  desc, d.Cod_Fuente_dato "

        If sFiltro <> "" Then
          sentenciaSel = sentenciaSel & sFiltro & sentenciaOrder
          sentenciaSelCount = sentenciaSelCount & sFiltro
        Else
          sentenciaSel = sentenciaSel & " and Fecha_Medida between '" & fechainicio & " 00:00:00' and '" & fechaFin & " 23:59:59' " & sentenciaOrder
        End If

        daElementos.SelectCommand.CommandText = sentenciaSel

        'IPIM 26/11/2008: Descomentamos para paginar
        'daElementos.Fill(dstElementos, (CInt(ucPaginacionA.lblPaginatext) - 1) * ucPaginacionA.pageSize, ucPaginacionA.pageSize, "TablaAcequias")
        daElementos.Fill(dstElementos, "TablaDiferencial")
        'Cálculo del número de páginas
        'ordenamos las lecturas en un dataview por fecha

        Dim txtComando As String = ""
        txtComando = daElementos.SelectCommand.CommandText
        txtComando = txtComando.Substring(0, txtComando.IndexOf("order by"))
        'IPIM 26/11/2008: Descomentamos para paginar
        If Not ucPaginacion Is Nothing Then
          ucPaginacion.calcularPags(txtComando)
        End If
        Dim Tabla As DataTable
        Tabla = dstElementos.Tables("TablaDiferencial").Clone()
        For i = dstElementos.Tables("TablaDiferencial").Rows.Count - 1 To 0 Step -1
          Tabla.Rows.Add(dstElementos.Tables("TablaDiferencial").Rows(i).ItemArray)
        Next
        dstElementos.Tables.Remove(dstElementos.Tables("TablaDiferencial"))
        dstElementos.Tables.Add(Tabla)

        'obtenerVolumenDiferencial("D", dstElementos.Tables("TablaDiferencial"), page, mensaje_final)
        ''obtenerCaudalAcumulado()
        'If ReducirLec Then
        '    'IPIM 26/09/2008: Eliminar registros que no cumplen el porcentaje
        '    Dim dstNuevo As DataSet
        '    dstNuevo = utiles.QuitarRegistrosSegunPorcentaje(dstElementos, "TablaDiferencial", "suministromensualm3", 5)
        '    dstElementos = dstNuevo.Copy
        'End If

        'CrearTablaParaRepeater("TablaDiferencial", "D")

      End If



    End Sub
    Shared Sub QuitarRegistrosSegunFuenteDato(ByVal tipoElemento As String, ByVal dstElementos As DataSet)
      'ncm 13/09/2009
      'Recorreremos el dataset correspondiente y eliminaremos los registros
      'que tengan cod_fuente_dato <> 05 e incidencia (6 o 7 en el caso de los motores y 2 o 3 en el casod de alimentación)
            Dim i As Integer = 0
      Dim contador As Integer
      Select Case tipoElemento
        Case "V"
          'comprobamos si la columna borrarFila ya existe.
          If Not dstElementos.Tables("TablaMotores").Columns.Contains("BorrarFila") Then
            dstElementos.Tables("TablaMotores").Columns.Add("BorrarFila")
          End If

          contador = dstElementos.Tables("TablaMotores").Rows.Count - 1
          For i = 0 To contador
            If dstElementos.Tables("TablaMotores").Rows(i).Item("cod_fuente_dato").ToString <> "05" And _
               (dstElementos.Tables("TablaMotores").Rows(i).Item("idIncidenciavolumetrica").ToString = "6" Or _
                dstElementos.Tables("TablaMotores").Rows(i).Item("idIncidenciavolumetrica").ToString = "7") Then

              If dstElementos.Tables("TablaMotores").Select("cod_fuente_dato = '05' and (idincidenciavolumetrica = 6 or idincidenciavolumetrica = 7)").Length <> 0 Then
                dstElementos.Tables("TablaMotores").Rows(i).Item("BorrarFila") = "S"
              End If
            Else
              dstElementos.Tables("TablaMotores").Rows(i).Item("BorrarFila") = "N"
            End If
          Next
          Dim dt As DataTable
          dt = dstElementos.Tables("TablaMotores").Copy
          Dim filas As Integer = dt.Rows.Count - 1
          For i = 0 To filas
            If utiles.nullABlanco(dt.Rows(i).Item("BorrarFila")) = "S" Then
              dstElementos.Tables("TablaMotores").Rows(i).Delete()
            End If
          Next

        Case "E"
          If Not dstElementos.Tables("TablaAlimentacion").Columns.Contains("BorrarFila") Then
            dstElementos.Tables("TablaAlimentacion").Columns.Add("BorrarFila")
          End If

          contador = dstElementos.Tables("TablaAlimentacion").Rows.Count - 1
          For i = 0 To contador
            If dstElementos.Tables("TablaAlimentacion").Rows(i).Item("cod_fuente_dato").ToString <> "05" And _
               (dstElementos.Tables("TablaAlimentacion").Rows(i).Item("idIncidenciaelectrica").ToString = "2" Or _
                dstElementos.Tables("TablaAlimentacion").Rows(i).Item("idIncidenciaelectrica").ToString = "3") Then

              If dstElementos.Tables("TablaAlimentacion").Select("cod_fuente_dato = '05' and (idIncidenciaelectrica = 2 or idIncidenciaelectrica = 3)").Length <> 0 Then
                dstElementos.Tables("TablaAlimentacion").Rows(i).Item("BorrarFila") = "S"
              End If

            End If
          Next
          Dim dt As DataTable
          dt = dstElementos.Tables("TablaAlimentacion").Copy
          Dim filas As Integer = dt.Rows.Count - 1
          For i = 0 To filas
            If utiles.nullABlanco(dt.Rows(i).Item("BorrarFila")) = "S" Then
              dstElementos.Tables("TablaAlimentacion").Rows(i).Delete()
            End If
          Next
      End Select

    End Sub
  End Class
End Namespace
