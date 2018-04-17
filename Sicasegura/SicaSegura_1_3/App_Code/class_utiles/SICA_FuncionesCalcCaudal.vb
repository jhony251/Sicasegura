Imports Microsoft.VisualBasic
Imports System.Data
Imports GuarderiaFluvial
Imports System
Imports System.Math
Public Class SICA_FuncionesCalcCaudal
  
    Structure recta
        Public a As Double
        Public b As Double
    End Structure

    Structure puntos_recta
        Public x As Double
        Public y As Double
    End Structure

    Structure seccion
        Public A1 As Double
        Public A2 As Double
        Public A3 As Double
        Public B1 As Double
        Public B2 As Double
        Public B3 As Double
        Public B4 As Double
        Public H1 As Double
        Public H2 As Double
        Public H12 As Double
        Public H23 As Double
        Public H34 As Double
        Public C1 As Double
        Public O As Double
        Public C As Double
        Public E As Double
    End Structure


    Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))



    Shared Sub CalcularCaudal_final(ByVal codigoPVYCR As String, ByVal idElemento As String, ByVal cod_fuente_datos As String, _
    ByVal dstAcequias As DataSet, _
    ByVal i As Integer, ByVal PcbestimadaC As Boolean, ByVal pcalado As String, _
    ByVal pescala As String, _
    ByRef ddl As DropDownList, ByVal pTiempo As String, ByVal pV11MS As String, _
    ByVal pV12MS As String, ByVal pV21MS As String, ByVal pV22MS As String, _
    ByVal pV31MS As String, ByVal pV32MS As String, ByVal page As Page, _
    ByRef PsuperficieTM As String, ByRef PaltM As String, ByRef PsuperficieFM As String, _
    ByRef PsuperficieRM As String, ByRef PvelocidadM As String, _
    ByRef PsuperficieTF As String, ByRef PaltF As String, ByRef PsuperficieFF As String, _
    ByRef PsuperficieRF As String, ByRef PvelocidadF As String, _
    ByRef pcbC As Boolean, ByRef pcbA As Boolean, ByRef pcbF As Boolean, _
    ByRef Pcolor As String, _
    ByRef PSelCalculoR As Boolean, ByRef PSelCalculoA As Boolean, _
    ByRef pTOCaudalC As String, ByRef pTOCaudalA As String, ByRef pTOCaudalF As String, _
    ByRef Pmensaje As String, _
    ByRef PcaudalC As String, ByRef PcaudalA As String, ByRef PcaudalF As String, ByVal elcbRC As Boolean, _
    ByRef PcaudalC_ant As String, ByRef PcaudalA_ant As String, ByRef PcaudalF_ant As String)

        '****************************** CALCULO DEL CAUDAL ***********************************************************************************
        '***** TENEMOS TRES TIPOS DE CÁCULO, CAUDAL (C), MOLINETE (A) Y FLOTADOR (F). Los registros se van a evaluar siempre en ese orden
        '***** Comprobamos cuantas curvas de gasto hay (excluyendo siempre la CT)
        '***** 1) Una curva de gasto --> se calcula el caudal para ella
        '***** 2) Mas de una:
        '*****   2.1)primero calculamos por molinete:
        '*****     2.1.1)Comprobamos si las velocidades tienen valor, si es así se calcula el caudal por molinete y el C, sino molinete nulo
        '*****     2.1.2)No hay molinete, comprobamos flotador. Si tiene valor la variable tiempo se calcula flotador y C
        '*****     2.1.3) No hay molinete ni flotador, se obtiene un dato existente en la base de datos que sea por molinete o flotador y 
        '*****            que no diste mas de 3 días (inclusive) del dato a calcular y lo usamos del mismo modo que en el punto 2.1.1
        '*****      2.1.4) no encontramos dato válido en el 2.1.3, buscamos la mayoritaria (interpolamos)
        '***************************************************************************************************************************************

        '*************************** declaración de variables ****************************************************************************
        Dim v_calado, v_escala, v_caudal, A1, A2, A3, B1, B2, B3, B4, H12, H23, H34, offset, caudalA, caudalF, v_caudal_ant, v_caudal_C, v_tiempo, v_longitudFlotador, v_factorflotador, v_b, V11, V12, V21, V22, V31, V32, diametro, v_caudal_curva, v_caudal_curva_ant As Decimal
        Dim v_RegimenCurva, tipocaudal, v_RegimenCurva_ant, v_color, v_color_ant As String
        Dim j As Integer
        'Dim elPanel As Panel
        Dim elPanel As String
        Dim v_num_curvas As Integer = 0
        Dim superficieTeoF_ant, altFangosF_ant, superficieFangosF_ant, superficieRealF_ant, velFlotadorF_ant, velMolineteF_ant As Decimal
        Dim superficieTeoA_ant, altFangosA_ant, superficieFangosA_ant, superficieRealA_ant, velFlotadorA_ant, velMolineteA_ant As Decimal
        Dim Valores_perfil As seccion = New seccion

        'Dim elcbRC As CheckBox = New CheckBox
        Dim panel As Integer = i Mod 3
        '******** este crear dataset lo haremos antes de llamar a calcular caudal para así pasarle los puntos de acequias *****************'
        '******** para los que se va a calcular el caudal  ********************************************************************************'

        Dim vectorLecturas As String

        'si es estimada cogeremos la inmediatamente anterior, si no hay cogeremos la del histórico
        If PcbestimadaC = False Then

            v_caudal = 0
            v_caudal_ant = 0
            v_caudal_curva = 0
            v_caudal_C = Nothing
            caudalA = Nothing
            caudalF = Nothing
            pTOCaudalC = "C"
            pTOCaudalA = "A"
            pTOCaudalF = "F"
            Pcolor = "colorB"
            diametro = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("diametro_mm").ToString)

            If panel = 0 Then
                'solo inicializaremos las variables cuando estemos en el panel C, que es la primara vez que se evalua todo
                'Pmensaje = ""
                PcaudalC = Nothing
                PcaudalA = Nothing
                PcaudalF = Nothing
                elPanel = "C"
            ElseIf panel = 1 Then
                elPanel = "A"
            ElseIf panel = 2 Then
                elPanel = "F"
            End If
            ' primero comprobaremos si faltan la escala o el calado no podremos hacer ningún cálculo
            If IsDBNull(pcalado) Or pcalado = "" Then
                'Caudales
                PcaudalC = Nothing
                PcaudalA = Nothing
                PcaudalF = Nothing
                'datos F
                PsuperficieTM = Nothing
                PaltM = Nothing
                PsuperficieFM = Nothing
                PsuperficieRM = Nothing
                PvelocidadM = Nothing
                'datos A
                PsuperficieTF = Nothing
                PaltF = Nothing
                PsuperficieFF = Nothing
                PsuperficieRF = Nothing
                PvelocidadF = Nothing

                'solo mostraremos el mensajeV una vez
                If (i Mod 3 = 0) Then
                    Pmensaje += "No existe el Calado en " & dstAcequias.Tables("TablaAcequias2").Rows(i).Item("Fecha_Medida").ToString & " \n"
                End If
                Exit Sub
            Else
                ' Cogemos los items siempre del panel C que es el que está visible y el que tiene valor (i (i mod 3))
                v_calado = Convert.ToDecimal(0 & pcalado)
            End If
            'no existe la escala y sí el calado, copiamos el valor del calado en la escala
            If IsDBNull(pescala) Or pescala = "" And pcalado <> "" Then
                'cOPIAMOS EL VALOR DEL CALADO EN LA ESCALA
                v_escala = Convert.ToDecimal(0 & pcalado)
            ElseIf pcalado = "" And pescala = "" Then
                'Caudales
                PcaudalC = Nothing
                PcaudalA = Nothing
                PcaudalF = Nothing
                'datos F
                PsuperficieTM = Nothing
                PaltM = Nothing
                PsuperficieFM = Nothing
                PsuperficieRM = Nothing
                PvelocidadM = Nothing
                'datos A
                PsuperficieTF = Nothing
                PaltF = Nothing
                PsuperficieFF = Nothing
                PsuperficieRF = Nothing
                PvelocidadF = Nothing

                If (i Mod 3 = 0) Then
                    Pmensaje += "No existe la Escala en " & dstAcequias.Tables("TablaAcequias2").Rows(i).Item("Fecha_Medida").ToString & " \n"
                End If
                Exit Sub
            Else
                v_escala = Convert.ToDecimal(0 & pescala)
            End If

            'ddl = ddlRegimenCurva
            If ddl.Items.Count = 0 Then
                If (i Mod 3 = 0) Then
                    Pmensaje += "No existe el Regimen Curva en " & dstAcequias.Tables("TablaAcequias2").Rows(i).Item("Fecha_Medida").ToString & " \n"
                End If
                Exit Sub
            Else
                v_RegimenCurva = ddl.Items(0).Value
                v_RegimenCurva_ant = ddl.Items(0).Value
            End If

            'NCM 28/05/2008 Miramos cuantas curva de gasto hay y para saber el número eliminamos la CT ya que sino da problemas
            If Not (IsNothing(ddl.Items.FindByText("CT"))) Then 'si encuentra el valor
                v_num_curvas = ddl.Items.Count - 1
            Else
                v_num_curvas = ddl.Items.Count
            End If
            'Comprobamos si el punto de control tiene una sola curva de gasto (o está habilitado) o varias
            'si tiene una sola la aplicaremos directamente y sino tendremos que obtener molinete o/y flotador
            If v_num_curvas = 1 Or elcbRC = True Then
                If dstAcequias.Tables("tablaAcequias2").Rows(i).Item("tipoobtencioncaudal").ToString = "C" Then
                    ' si sólo tiene un valor el ddl, entonces nos quedaremos con v_regimenCurva, sino
                    'obtendremos el valor que ha seleccionado el usuario
                    If elcbRC = True Then
                        v_RegimenCurva = ddl.SelectedValue
                    End If
                    v_caudal_C = calcular_Caudal(v_calado, v_RegimenCurva, 1, 0, 0, page)
                    'NCM estimada
                    PcaudalC_ant = v_caudal_C
                    PcaudalC = v_caudal_C
                ElseIf dstAcequias.Tables("tablaAcequias2").Rows(i).Item("tipoobtencioncaudal").ToString = "F" Then
                    'calculamos el caudal F si existe el tiempo
                    If pTiempo <> "" Then
                        A1 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("A1_M").ToString)
                        A2 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("A2_M").ToString)
                        A3 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("A3_M").ToString)
                        B1 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("B1_M").ToString)
                        B2 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("B2_M").ToString)
                        B3 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("B3_M").ToString)
                        B4 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("B4_M").ToString)
                        H12 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("H12_M").ToString)
                        'H12 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("H1_M").ToString)
                        'H23 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("H2_M").ToString)
                        H23 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("H23_M").ToString)
                        H34 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("H34_M").ToString)
                        offset = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("offset_M").ToString)
                        v_tiempo = Convert.ToDecimal(pTiempo)
                        v_longitudFlotador = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("longitudFlotador").ToString)
                        v_factorflotador = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("factorflotador").ToString)
                        v_b = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("B").ToString)
                        'en las variables acabadas en _ant tenemos los varoles por si marcan estimada
                        caudalF = CalculosTipoObtCaudal("F", elPanel, A1, A2, A3, B1, B2, B3, B4, H12, H23, H34, offset, v_escala, v_calado, v_tiempo, _
                            v_longitudFlotador, v_factorflotador, v_b, V11, V12, V21, V22, V31, V32, diametro, superficieTeoF_ant, _
                            altFangosF_ant, superficieFangosF_ant, superficieRealF_ant, velFlotadorF_ant, velMolineteF_ant, Valores_perfil)
                        'si el caudal es -1 no se calcula nada
                        If caudalF = -1 Then
                            PsuperficieTF = Nothing
                            PaltF = Nothing
                            PsuperficieFF = Nothing
                            PsuperficieRF = Nothing
                            PvelocidadF = Nothing
                            If vectorLecturas Is Nothing Then
                                vectorLecturas = dstAcequias.Tables("TablaAcequias2").Rows(i).Item("Fecha_Medida").ToString
                            Else
                                vectorLecturas = vectorLecturas & ", " & dstAcequias.Tables("TablaAcequias2").Rows(i).Item("Fecha_Medida").ToString
                            End If
                            PcaudalF = Nothing
                        Else
                            'NCM estimada
                            PcaudalF_ant = caudalF
                            PcaudalF = caudalF
                            PsuperficieTF = String.Format("{0:#,##0.##}", superficieTeoF_ant)
                            PaltF = String.Format("{0:#,##0.##}", altFangosF_ant)
                            PsuperficieFF = String.Format("{0:#,##0.##}", superficieFangosF_ant)
                            PsuperficieRF = String.Format("{0:#,##0.##}", superficieRealF_ant)
                            PvelocidadF = String.Format("{0:#,##0.###}", velFlotadorF_ant)
                        End If
                    Else
                        PsuperficieTF = Nothing
                        PaltF = Nothing
                        PsuperficieFF = Nothing
                        PsuperficieRF = Nothing
                        PvelocidadF = Nothing
                        PcaudalF = Nothing
                        PcaudalF_ant = -9999
                        superficieTeoF_ant = Nothing
                        altFangosF_ant = Nothing
                        superficieFangosF_ant = Nothing
                        superficieRealF_ant = Nothing
                        velFlotadorF_ant = Nothing
                        velMolineteF_ant = Nothing
                    End If
                ElseIf dstAcequias.Tables("tablaAcequias2").Rows(i).Item("tipoobtencioncaudal").ToString = "A" Then
                    'calculamos el caudal por molinete A si existe alguna velocidad
                    If pV11MS <> "" Or pV12MS <> "" Or pV21MS <> "" Or pV22MS <> "" Or pV31MS <> "" Or pV32MS <> "" Then

                        A1 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("A1_M").ToString)
                        A2 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("A2_M").ToString)
                        A3 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("A3_M").ToString)
                        B1 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("B1_M").ToString)
                        B2 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("B2_M").ToString)
                        B3 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("B3_M").ToString)
                        B4 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("B4_M").ToString)
                        H12 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("H12_M").ToString)
                        H23 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("H23_M").ToString)
                        'H12 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("H1_M").ToString)
                        'H23 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("H2_M").ToString)
                        H34 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("H34_M").ToString)
                        V11 = Convert.ToDecimal(0 & pV11MS)
                        V12 = Convert.ToDecimal(0 & pV12MS)
                        V21 = Convert.ToDecimal(0 & pV21MS)
                        V22 = Convert.ToDecimal(0 & pV22MS)
                        V31 = Convert.ToDecimal(0 & pV31MS)
                        V32 = Convert.ToDecimal(0 & pV32MS)
                        caudalA = CalculosTipoObtCaudal("A", elPanel, A1, A2, A3, B1, B2, B3, B4, H12, H23, H34, offset, v_escala, v_calado, v_tiempo, _
                        v_longitudFlotador, v_factorflotador, v_b, V11, V12, V21, V22, V31, V32, diametro, superficieTeoA_ant, _
                            altFangosA_ant, superficieFangosA_ant, superficieRealA_ant, velFlotadorA_ant, velMolineteA_ant, Valores_perfil)
                        'si el caudal es -1 no se calcula nada
                        If caudalA = -1 Then
                            If vectorLecturas Is Nothing Then
                                vectorLecturas = dstAcequias.Tables("TablaAcequias2").Rows(i).Item("Fecha_Medida").ToString
                            Else
                                vectorLecturas = vectorLecturas & ", " & dstAcequias.Tables("TablaAcequias2").Rows(i).Item("Fecha_Medida").ToString
                            End If
                            PsuperficieTM = Nothing
                            PaltM = Nothing
                            PsuperficieFM = Nothing
                            PsuperficieRM = Nothing
                            PvelocidadM = Nothing
                            PcaudalA = Nothing
                            PcaudalA_ant = -9999
                        Else
                            PcaudalA_ant = caudalA
                            PcaudalA = caudalA
                            PsuperficieTM = String.Format("{0:#,##0.##}", superficieTeoA_ant)
                            PaltM = String.Format("{0:#,##0.##}", altFangosA_ant)
                            PsuperficieFM = String.Format("{0:#,##0.##}", superficieFangosA_ant)
                            PsuperficieRM = String.Format("{0:#,##0.##}", superficieRealA_ant)
                            PvelocidadM = String.Format("{0:#,##0.###}", velMolineteA_ant)
                        End If 'caudal distinto de -1 para el calculo por aforo
                    Else
                        PsuperficieTM = Nothing
                        PaltM = Nothing
                        PsuperficieFM = Nothing
                        PsuperficieRM = Nothing
                        PvelocidadM = Nothing
                        PcaudalA = Nothing
                        PcaudalA_ant = -9999
                        superficieTeoA_ant = Nothing
                        altFangosA_ant = Nothing
                        superficieFangosA_ant = Nothing
                        superficieRealA_ant = Nothing
                        velFlotadorA_ant = Nothing
                        velMolineteA_ant = Nothing
                    End If
                End If
            ElseIf v_num_curvas > 1 Then
                'calculamos el caudal F
                If dstAcequias.Tables("tablaAcequias2").Rows(i).Item("tipoobtencioncaudal").ToString = "F" Then
                    A1 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("A1_M").ToString)
                    A2 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("A2_M").ToString)
                    A3 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("A3_M").ToString)
                    B1 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("B1_M").ToString)
                    B2 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("B2_M").ToString)
                    B3 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("B3_M").ToString)
                    B4 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("B4_M").ToString)
                    H12 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("H12_M").ToString)
                    H23 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("H23_M").ToString)
                    'H12 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("H1_M").ToString)
                    'H23 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("H2_M").ToString)
                    H34 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("H34_M").ToString)
                    offset = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("offset_M").ToString)
                    v_longitudFlotador = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("longitudFlotador").ToString)
                    v_factorflotador = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("factorflotador").ToString)
                    v_b = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("B").ToString)
                    'calculamos el caudal F si existe el tiempo
                    If pTiempo <> "" Then
                        v_tiempo = Convert.ToDecimal(pTiempo)
                        caudalF = CalculosTipoObtCaudal("F", elPanel, A1, A2, A3, B1, B2, B3, B4, H12, H23, H34, offset, v_escala, v_calado, v_tiempo, _
                        v_longitudFlotador, v_factorflotador, v_b, V11, V12, V21, V22, V31, V32, diametro, superficieTeoF_ant, _
                            altFangosF_ant, superficieFangosF_ant, superficieRealF_ant, velFlotadorF_ant, velMolineteF_ant, Valores_perfil)
                        'si el caudal es -1 no se calcula nada
                        If caudalF = -1 Then
                            If vectorLecturas Is Nothing Then
                                vectorLecturas = dstAcequias.Tables("TablaAcequias2").Rows(i).Item("Fecha_Medida").ToString
                            Else

                                vectorLecturas = vectorLecturas & ", " & dstAcequias.Tables("TablaAcequias2").Rows(i).Item("Fecha_Medida").ToString
                            End If
                            PcaudalF = Nothing
                            PsuperficieTF = Nothing
                            PaltF = Nothing
                            PsuperficieFF = Nothing
                            PsuperficieRF = Nothing
                            PvelocidadF = Nothing
                            PcaudalF_ant = -9999
                        Else
                            'NCM estimada
                            PcaudalF_ant = caudalF
                            PcaudalF = caudalF
                            'si el caudal por curva de gasto no se ha calculado teniendo en cuenta el aforo, se hará teniendo encuenta el flotador
                            If PcaudalC = Nothing Then
                                'para cada regimen curva calculamos el caudal y nos quedamos con el menor
                                Dim v_numcurvas As Integer = ddl.Items.Count
                                For j = 0 To v_numcurvas - 1
                                    v_RegimenCurva = ddl.Items(j).Value
                                    'calcularemos el caudal para cada curva de gasto del combo, excepto para la CT (por petición del usurio)
                                    If v_RegimenCurva <> 991 Then
                                        ' llamaremos a calcular caudal cuando el tipode caudal sea F ya que previamente habremos calculado
                                        ' el tipocaudal A por el orden en que se ha creado las filas en el dataset (C, A, F)
                                        v_caudal = calcular_Caudal(v_calado, v_RegimenCurva, 2, caudalF, Convert.ToDecimal(0 & PcaudalA), page)
                                        'restremos al caudal obtenido el de molinete, y la menor diferencia será la curva con la que nos quedemos
                                        v_caudal_curva = Math.Abs(v_caudal - PcaudalF_ant)
                                        If j = 0 Then
                                            v_caudal_C = v_caudal
                                            For Each elemento As ListItem In ddl.Items
                                                If v_RegimenCurva <> 991 Then
                                                    If elemento.Value = v_RegimenCurva Then
                                                        elemento.Selected = True
                                                    Else
                                                        elemento.Selected = False
                                                    End If
                                                End If
                                            Next
                                            v_caudal_ant = v_caudal
                                            v_caudal_curva_ant = v_caudal_curva
                                            v_RegimenCurva_ant = v_RegimenCurva
                                        Else
                                            If v_caudal_curva < v_caudal_curva_ant Then
                                                v_caudal_C = v_caudal
                                                For Each elemento As ListItem In ddl.Items
                                                    If v_RegimenCurva <> 991 Then
                                                        If elemento.Value = v_RegimenCurva Then
                                                            elemento.Selected = True
                                                        Else
                                                            elemento.Selected = False
                                                        End If
                                                    End If
                                                Next
                                                v_caudal_ant = v_caudal
                                                v_caudal_curva_ant = v_caudal_curva
                                                v_RegimenCurva_ant = v_RegimenCurva
                                            Else
                                                v_caudal_C = v_caudal_ant
                                                For Each elemento As ListItem In ddl.Items
                                                    If v_RegimenCurva <> 991 Then
                                                        If elemento.Value = v_RegimenCurva_ant Then
                                                            elemento.Selected = True
                                                        Else
                                                            elemento.Selected = False
                                                        End If
                                                    End If
                                                Next
                                            End If
                                        End If
                                    End If
                                Next
                                'ncm estimada
                                PcaudalC_ant = v_caudal_C
                                PcaudalC = v_caudal_C
                            End If ' caudal c nothing y por tanto se calcula
                            PsuperficieTF = String.Format("{0:#,##0.##}", superficieTeoF_ant)
                            PaltF = String.Format("{0:#,##0.##}", altFangosF_ant)
                            PsuperficieFF = String.Format("{0:#,##0.##}", superficieFangosF_ant)
                            PsuperficieRF = String.Format("{0:#,##0.##}", superficieRealF_ant)
                            PvelocidadF = String.Format("{0:#,##0.###}", velFlotadorF_ant)
                        End If 'caudalF <> -1
                    Else ' no hay valor para el tiempo
                        PsuperficieTF = Nothing
                        PaltF = Nothing
                        PsuperficieFF = Nothing
                        PsuperficieRF = Nothing
                        PvelocidadF = Nothing
                        PcaudalF = Nothing
                        PcaudalF_ant = -9999
                        superficieTeoF_ant = Nothing
                        altFangosF_ant = Nothing
                        superficieFangosF_ant = Nothing
                        superficieRealF_ant = Nothing
                        velFlotadorF_ant = Nothing
                        velMolineteF_ant = Nothing
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        Dim caudalMF, caudalMF_ant As Decimal
                        'si el caudal por curva de gasto no se ha calculado teniendo en cuenta el aforo, se hará teniendo encuenta el flotador
                        If PcaudalC = Nothing Then
                            '2.1.3 buscamos en la base de dato el caudal por molinete o flotador para los tres días anteriores
                            Dim tipo As String = ""
                            Dim v_caudalMF As String = ObtenerMolineteFlotadorAnterior(cod_fuente_datos, dstAcequias.Tables("TablaAcequias2").Rows(i).Item("Fecha_Medida").ToString, tipo, codigoPVYCR, _
                            idElemento, dstAcequias)
                            If v_caudalMF <> "N" Then
                                caudalMF = Convert.ToDecimal(0 & v_caudalMF)
                                caudalMF_ant = caudalMF
                                'en las variables acabadas en _ant tenemos los varoles por si marcan estimada
                                Dim auxcaudal As Decimal = CalculosTipoObtCaudal(tipo, tipo, A1, A2, A3, B1, B2, B3, B4, H12, H23, H34, offset, v_escala, v_calado, v_tiempo, _
                                    v_longitudFlotador, v_factorflotador, v_b, V11, V12, V21, V22, V31, V32, diametro, superficieTeoF_ant, _
                                    altFangosF_ant, superficieFangosF_ant, superficieRealF_ant, velFlotadorF_ant, velMolineteF_ant, Valores_perfil)
                                If tipo = "A" Then
                                    PcaudalA = caudalMF
                                    PcaudalA_ant = caudalMF
                                    elPanel = "F"
                                    PsuperficieTM = String.Format("{0:#,##0.##}", superficieTeoF_ant)
                                    PaltM = String.Format("{0:#,##0.##}", altFangosF_ant)
                                    PsuperficieFM = String.Format("{0:#,##0.##}", superficieFangosF_ant)
                                    PsuperficieRM = String.Format("{0:#,##0.##}", superficieRealF_ant)
                                    PvelocidadM = String.Format("{0:#,##0.###}", velMolineteF_ant)
                                Else
                                    PcaudalF = caudalMF
                                    PcaudalF_ant = caudalMF
                                    elPanel = "F"
                                    PsuperficieTF = String.Format("{0:#,##0.##}", superficieTeoF_ant)
                                    PaltF = String.Format("{0:#,##0.##}", altFangosF_ant)
                                    PsuperficieFF = String.Format("{0:#,##0.##}", superficieFangosF_ant)
                                    PsuperficieRF = String.Format("{0:#,##0.##}", superficieRealF_ant)
                                    PvelocidadF = String.Format("{0:#,##0.###}", velFlotadorF_ant)
                                End If
                                'para cada regimen curva calculamos el caudal y nos quedamos con el menor
                                Dim v_numcurvas As Integer = ddl.Items.Count
                                For j = 0 To v_numcurvas - 1
                                    v_RegimenCurva = ddl.Items(j).Value
                                    'calcularemos el caudal para cada curva de gasto del combo, excepto para la CT (por petición del usurio)
                                    If v_RegimenCurva <> 991 Then
                                        ' llamaremos a calcular caudal cuando el tipode caudal sea F ya que previamente habremos calculado
                                        ' el tipocaudal A por el orden en que se ha creado las filas en el dataset (C, A, F)
                                        v_caudal = calcular_Caudal(v_calado, v_RegimenCurva, 2, caudalMF, _
                                        Convert.ToDecimal(0 & PcaudalA), page)
                                        'restremos al caudal obtenido el de molinete, y la menor diferencia será la curva con la que nos quedemos
                                        v_caudal_curva = Math.Abs(v_caudal - caudalMF_ant)
                                        If j = 0 Then
                                            v_caudal_C = v_caudal
                                            For Each elemento As ListItem In ddl.Items
                                                If v_RegimenCurva <> 991 Then
                                                    If elemento.Value = v_RegimenCurva Then
                                                        elemento.Selected = True
                                                    Else
                                                        elemento.Selected = False
                                                    End If
                                                End If
                                            Next
                                            v_caudal_ant = v_caudal
                                            v_caudal_curva_ant = v_caudal_curva
                                            v_RegimenCurva_ant = v_RegimenCurva
                                        Else
                                            If v_caudal_curva < v_caudal_curva_ant Then
                                                v_caudal_C = v_caudal
                                                '                            ddl.SelectedValue = v_RegimenCurva
                                                For Each elemento As ListItem In ddl.Items
                                                    If v_RegimenCurva <> 991 Then
                                                        If elemento.Value = v_RegimenCurva Then
                                                            elemento.Selected = True
                                                        Else
                                                            elemento.Selected = False
                                                        End If
                                                    End If
                                                Next
                                                v_caudal_ant = v_caudal
                                                v_caudal_curva_ant = v_caudal_curva
                                                v_RegimenCurva_ant = v_RegimenCurva
                                            Else
                                                v_caudal_C = v_caudal_ant
                                                'ddl.SelectedValue = v_RegimenCurva_ant
                                                For Each elemento As ListItem In ddl.Items
                                                    If v_RegimenCurva <> 991 Then
                                                        If elemento.Value = v_RegimenCurva_ant Then
                                                            elemento.Selected = True
                                                        Else
                                                            elemento.Selected = False
                                                        End If
                                                    End If
                                                Next
                                            End If
                                        End If 'j<>0
                                    End If 'v_RegimenCurva <> 991
                                Next
                                'ncm estimada
                                PcaudalC_ant = v_caudal_C
                                PcaudalC = v_caudal_C
                            Else 'caudal <> n
                                '2.1.4 no hay caudal y obtenemos la que tiene mayor porcentaje
                                'obtenemos la curva que mayor probabilidad tiene para ese punto
                                v_RegimenCurva = ObtenerCurvaMasProbable(codigoPVYCR, idElemento)
                                'calculamos el caudal para la curva con mayor probabilidad
                                v_caudal_C = calcular_Caudal(v_calado, v_RegimenCurva, 1, 0, 0, page)
                                PcaudalC = v_caudal_C
                                PcaudalC_ant = v_caudal_C
                                ddl.SelectedValue = v_RegimenCurva
                            End If '<> n
                        End If ' caudal c nothing y por tanto se calcula
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    End If
                ElseIf dstAcequias.Tables("tablaAcequias2").Rows(i).Item("tipoobtencioncaudal").ToString = "A" Then
                    If pV11MS <> "" Or pV12MS <> "" Or pV21MS <> "" Or pV22MS <> "" Or pV31MS <> "" Or pV32MS <> "" Then
                        A1 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("A1_M").ToString)
                        A2 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("A2_M").ToString)
                        A3 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("A3_M").ToString)
                        B1 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("B1_M").ToString)
                        B2 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("B2_M").ToString)
                        B3 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("B3_M").ToString)
                        B4 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("B4_M").ToString)
                        H12 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("H12_M").ToString)
                        H23 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("H23_M").ToString)
                        'H12 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("H1_M").ToString)
                        'H23 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("H2_M").ToString)
                        H34 = Convert.ToDecimal(0 & dstAcequias.Tables("tablaacequias2").Rows(i).Item("H34_M").ToString)
                        V11 = Convert.ToDecimal(0 & pV11MS)
                        V12 = Convert.ToDecimal(0 & pV12MS)
                        V21 = Convert.ToDecimal(0 & pV21MS)
                        V22 = Convert.ToDecimal(0 & pV22MS)
                        V31 = Convert.ToDecimal(0 & pV31MS)
                        V32 = Convert.ToDecimal(0 & pV32MS)
                        caudalA = CalculosTipoObtCaudal("A", elPanel, A1, A2, A3, B1, B2, B3, B4, H12, H23, H34, offset, v_escala, v_calado, v_tiempo, _
                        v_longitudFlotador, v_factorflotador, v_b, V11, V12, V21, V22, V31, V32, diametro, superficieTeoA_ant, _
                            altFangosA_ant, superficieFangosA_ant, superficieRealA_ant, velFlotadorA_ant, velMolineteA_ant, Valores_perfil)
                        'si el caudal es -1 no se calcula nada
                        If caudalA = -1 Then
                            If vectorLecturas Is Nothing Then
                                vectorLecturas = dstAcequias.Tables("TablaAcequias2").Rows(i).Item("Fecha_Medida").ToString
                            Else
                                vectorLecturas = vectorLecturas & ", " & dstAcequias.Tables("TablaAcequias2").Rows(i).Item("Fecha_Medida").ToString
                            End If
                            PcaudalA = Nothing
                            PcaudalA_ant = -9999
                            PsuperficieTM = Nothing
                            PaltM = Nothing
                            PsuperficieFM = Nothing
                            PsuperficieRM = Nothing
                            PvelocidadM = Nothing
                        Else

                            PcaudalA_ant = caudalA

                            PcaudalA = caudalA
                            '********************** ncm 05/10/2009 nueva forma de calcular la curva de gasto *******************************
                            '********************** se tendrá en cuenta el caudal de aforo, si no lo hubiera, se cogeria el de flotador ****
                            'para cada regimen curva calculamos el caudal y sacamos la diferencia entre éste y el caudal de aforo, cogeremos el menor
                            Dim v_numcurvas As Integer = ddl.Items.Count
                            For j = 0 To v_numcurvas - 1
                                v_RegimenCurva = ddl.Items(j).Value
                                'calcularemos el caudal para cada curva de gasto del combo, excepto para la CT (por petición del usurio)
                                If v_RegimenCurva <> 991 Then
                                    ' llamaremos a calcular caudal cuando el tipode caudal sea F ya que previamente habremos calculado
                                    ' el tipocaudal A por el orden en que se ha creado las filas en el dataset (C, A, F)
                                    v_caudal = calcular_Caudal(v_calado, v_RegimenCurva, 2, caudalF, Convert.ToDecimal(0 & PcaudalA), page)
                                    'restremos al caudal obtenido el de molinete, y la menor diferencia será la curva con la que nos quedemos
                                    v_caudal_curva = Math.Abs(v_caudal - PcaudalA_ant)
                                    If j = 0 Then
                                        v_caudal_C = v_caudal
                                        For Each elemento As ListItem In ddl.Items
                                            If v_RegimenCurva <> 991 Then
                                                If elemento.Value = v_RegimenCurva Then
                                                    elemento.Selected = True
                                                Else
                                                    elemento.Selected = False
                                                End If
                                            End If
                                        Next
                                        v_caudal_ant = v_caudal
                                        v_caudal_curva_ant = v_caudal_curva
                                        v_RegimenCurva_ant = v_RegimenCurva
                                    Else
                                        If v_caudal_curva < v_caudal_curva_ant Then
                                            v_caudal_C = v_caudal
                                            For Each elemento As ListItem In ddl.Items
                                                If v_RegimenCurva <> 991 Then
                                                    If elemento.Value = v_RegimenCurva Then
                                                        elemento.Selected = True
                                                    Else
                                                        elemento.Selected = False
                                                    End If
                                                End If
                                            Next
                                            v_caudal_ant = v_caudal
                                            v_caudal_curva_ant = v_caudal_curva
                                            v_RegimenCurva_ant = v_RegimenCurva
                                        Else
                                            v_caudal_C = v_caudal_ant
                                            For Each elemento As ListItem In ddl.Items
                                                If v_RegimenCurva <> 991 Then
                                                    If elemento.Value = v_RegimenCurva_ant Then
                                                        elemento.Selected = True
                                                    Else
                                                        elemento.Selected = False
                                                    End If
                                                End If
                                            Next
                                        End If
                                    End If
                                End If
                            Next
                            'ncm estimada
                            PcaudalC_ant = v_caudal_C
                            PcaudalC = v_caudal_C
                            PsuperficieTM = String.Format("{0:#,##0.##}", superficieTeoA_ant)
                            PaltM = String.Format("{0:#,##0.##}", altFangosA_ant)
                            PsuperficieFM = String.Format("{0:#,##0.##}", superficieFangosA_ant)
                            PsuperficieRM = String.Format("{0:#,##0.##}", superficieRealA_ant)
                            PvelocidadM = String.Format("{0:#,##0.###}", velMolineteA_ant)
                        End If ' caudal <> -1 para el calculo por aforo
                    Else
                        PsuperficieTM = Nothing
                        PaltM = Nothing
                        PsuperficieFM = Nothing
                        PsuperficieRM = Nothing
                        PvelocidadM = Nothing
                        PcaudalA = Nothing
                        PcaudalA_ant = -9999
                        superficieTeoA_ant = Nothing
                        altFangosA_ant = Nothing
                        superficieFangosA_ant = Nothing
                        superficieRealA_ant = Nothing
                        velFlotadorA_ant = Nothing
                        velMolineteA_ant = Nothing
                    End If
                End If
            Else
                v_caudal_C = calcular_Caudal(v_calado, v_RegimenCurva, 0, 0, 0, page)
                PcaudalC_ant = v_caudal_C
                PcaudalC = v_caudal_C
                ddl.SelectedValue = v_RegimenCurva
            End If
            'únicamente escribiremos en el panel F que es el último que se evalúa siempre.
            If Not IsNothing(elPanel) And elPanel = "F" Then

                'CÓDIGO DE COLORES
                ' 1)si solo tenemos caudal por curva de gasto, no aplicamos colores,
                ' 2)si tenemos Cy F y diferencial mayor k 40% --> rojo, = 40% --> amarillo, <= 20% --> verde
                ' 3)si tenemos Cy M y diferencial mayor k 40% --> rojo, = 40% --> amarillo, <= 20% --> verde
                ' 4)si tenemos tres datos, diferencial entre C y F, cojemos el mayor y comprobamos k mayor k 40% --> rojo, = 40% --> amarillo, <= 20% --> verde
                '5)si tenemos tres datos, diferencial entre C y F, cojemos el mayor y comprobamos k mayor k 40% --> rojo, = 40% --> amarillo, <= 20% --> verde
                v_color = calcular_codigo_colores(Convert.ToDecimal(0 & PcaudalC), Convert.ToDecimal(0 & PcaudalA), Convert.ToDecimal(0 & PcaudalF))
                'ncm estimada
                v_color_ant = v_color
                'Desmarcamos todos los checks por si no es la primera vez que le da a calcular
                pcbC = False
                pcbA = False
                pcbF = False
                'NCM 10/07/2008 por si se había calculado antes estimada, dejamos el tipoobtencioncaudal como toca

                If v_color = "R" Then
                    Pcolor = "colorR"
                    'Marcamos el radiobutton como rechazado
                    PSelCalculoR = True
                ElseIf v_color = "V" Then
                    Pcolor = "colorV"
                    'Ponemos el radiobutton a aceptar y los checks marcados a true
                    PSelCalculoA = True
                    If PcaudalC <> "" Then
                        pcbC = True
                    End If
                    If PcaudalA <> "" Then
                        pcbA = True
                    End If
                    If PcaudalF <> "" Then
                        pcbF = True
                    End If
                ElseIf v_color = "A" Then
                    Pcolor = "colorA"
                ElseIf v_color = "B" Then
                    Pcolor = "colorB"
                ElseIf v_color = "N" Then
                    ' Ponemos el radiobutton de aceptar pero desmarcamos todos los checks
                    PSelCalculoA = True
                    pcbC = False
                    pcbA = False
                    pcbF = False
                    Pcolor = "colorB"
                End If
            End If
        Else 'cuando es estimada
            If PcaudalA_ant <> -9999 And PcaudalF_ant <> -9999 And PcaudalC_ant <> -9999 Then
                PcaudalA = PcaudalA_ant
                PsuperficieTM = superficieTeoA_ant
                PaltM = altFangosA_ant
                PsuperficieFM = superficieFangosA_ant
                PsuperficieRM = superficieRealA_ant
                PvelocidadM = velMolineteA_ant
                pTOCaudalA = "E"
                'ponemos el resto de campos a nulo por si se ha calculado mas de una vez
                PcaudalF = Nothing
                PsuperficieTF = Nothing
                PaltF = Nothing
                PsuperficieFF = Nothing
                PsuperficieRF = Nothing
                PvelocidadF = Nothing
                pTOCaudalF = "F"
                pTOCaudalC = "C"
                PcaudalC = Nothing
                Pcolor = "colorB"
            ElseIf PcaudalA_ant = -9999 And PcaudalF_ant <> -9999 And PcaudalC_ant <> -9999 Then
                PcaudalF = PcaudalF_ant
                PsuperficieTF = String.Format("{0:#,##0.##}", superficieTeoF_ant)
                PaltF = String.Format("{0:#,##0.##}", altFangosF_ant)
                PsuperficieFF = String.Format("{0:#,##0.##}", superficieFangosF_ant)
                PsuperficieRF = String.Format("{0:#,##0.##}", superficieRealF_ant)
                PvelocidadF = String.Format("{0:#,##0.###}", velFlotadorF_ant)
                pTOCaudalF = "E"
                'ponemos el resto de campos a nulo por si se ha calculado mas de una vez
                PcaudalA = Nothing
                PsuperficieTM = Nothing
                PaltM = Nothing
                PsuperficieFM = Nothing
                PsuperficieRM = Nothing
                PvelocidadM = Nothing
                pTOCaudalA = "A"
                PcaudalC = Nothing
                pTOCaudalC = "C"
                Pcolor = "colorB"
            Else
                PcaudalC = PcaudalC_ant
                pTOCaudalC = "E"
                'limpiamos el resto de campos
                PcaudalF = Nothing
                PsuperficieTF = Nothing
                PaltF = Nothing
                PsuperficieFF = Nothing
                PsuperficieRF = Nothing
                PvelocidadF = Nothing
                pTOCaudalF = "F"
                PcaudalA = Nothing
                PsuperficieTM = Nothing
                PaltM = Nothing
                PsuperficieFM = Nothing
                PsuperficieRM = Nothing
                PvelocidadM = Nothing
                pTOCaudalA = "A"
                Pcolor = "colorB"
            End If

            'Desmarcamos todos los checks por si no es la primera vez que le da a calcular
            pcbC = False
            pcbA = False
            pcbF = False

            If v_color_ant = "R" Then
                Pcolor = "colorR"
                'Marcamos el radiobutton como rechazado
                PSelCalculoR = True
            ElseIf v_color_ant = "V" Then
                Pcolor = "colorV"
                'Ponemos el radiobutton a aceptar y los checks marcados a true
                PSelCalculoA = True
                If PcaudalC <> "" Then
                    pcbC = True
                End If
                If PcaudalA <> "" Then
                    pcbA = True
                End If
                If PcaudalF <> "" Then
                    pcbF = True
                End If
            ElseIf v_color_ant = "A" Then
                Pcolor = "colorA"
            ElseIf v_color_ant = "B" Then
                Pcolor = "colorB"
            ElseIf v_color_ant = "N" Then
                ' Ponemos el radiobutton de aceptar pero desmarcamos todos los checks
                PSelCalculoA = True
                pcbC = False
                pcbA = False
                pcbF = False
                Pcolor = "colorB"
            End If
        End If ' de primer registro

    End Sub
    Shared Function calcular_Caudal(ByVal calado As Double, ByVal RegimenCurva As String, ByVal NumCurvasGasto As Integer, _
    ByVal caudalF As Decimal, ByVal caudalA As Decimal, ByVal page As Page) As Decimal
        Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
        Dim N0, N1, N2, C0, C2, v_caudal, v_caudal_ant As Decimal
        Dim daCaudal As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter("", conexion)
        Dim dstCaudal As DataSet = New DataSet()
        Dim i As Integer

        utiles.Comprobar_Conexion_BD(page, conexion)

        'si el punto de control tiene una sola curva de gasto la plaicaremos directamente
        If NumCurvasGasto = 1 Then
            daCaudal.SelectCommand.CommandText = "SELECT top 1 Cod_Curva,Nivel,Caudal " & _
                                  "FROM PVYCR_CurvasAcequias_Valores " & _
                                  "WHERE Cod_Curva = " & RegimenCurva & " And Nivel = " & Replace(calado, ",", ".")
            daCaudal.Fill(dstCaudal, "tablaCurvas")


            'Si No encontramos el caudal por nivel (igualado con el valor del calado), haremos interpolación
            If (dstCaudal.Tables("tablaCurvas").Rows.Count = 0) Then

                'CALCULO POR INTERPOLACION
                daCaudal.SelectCommand.CommandText = "SELECT top 1 Nivel " & _
                                                       "FROM PVYCR_CurvasAcequias_Valores " & _
                                                       "WHERE Cod_Curva = " & RegimenCurva & " And Nivel < " & Replace(calado, ",", ".") & _
                                                       " order by nivel desc"
                daCaudal.Fill(dstCaudal, "tablaCaudalN0")

                daCaudal.SelectCommand.CommandText = "SELECT top 1 Nivel " & _
                                                       "FROM PVYCR_CurvasAcequias_Valores " & _
                                                       "WHERE Cod_Curva = " & RegimenCurva & " And Nivel > " & Replace(calado, ",", ".") & _
                                                       " order by nivel"

                daCaudal.Fill(dstCaudal, "tablaCaudalN2")

                daCaudal.SelectCommand.CommandText = "SELECT top 1 Caudal " & _
                                                       "FROM PVYCR_CurvasAcequias_Valores " & _
                                                       "WHERE Cod_Curva = " & RegimenCurva & " And Nivel < " & Replace(calado, ",", ".") & _
                                                       " order by nivel desc"
                daCaudal.Fill(dstCaudal, "tablaCaudalC0")

                daCaudal.SelectCommand.CommandText = "SELECT top 1 Caudal " & _
                                                       "FROM PVYCR_CurvasAcequias_Valores " & _
                                                       "WHERE Cod_Curva = " & RegimenCurva & " And Nivel > " & Replace(calado, ",", ".") & _
                                                       " order by nivel"
                daCaudal.Fill(dstCaudal, "tablaCaudalC2")
                'Si no tienen valores ni por arriba ni por bajo, nos quedamos con el que nos pasan
                If (dstCaudal.Tables("tablaCaudalN0").Rows.Count = 0) Then
                    N0 = Replace(calado, ",", ".")
                Else
                    N0 = dstCaudal.Tables("tablaCaudalN0").Rows(0).Item("Nivel")
                End If
                If (dstCaudal.Tables("tablaCaudalN2").Rows.Count = 0) Then
                    N2 = Replace(calado, ",", ".")
                Else
                    N2 = dstCaudal.Tables("tablaCaudalN2").Rows(0).Item("Nivel")
                End If
                If (dstCaudal.Tables("tablaCaudalC0").Rows.Count = 0) Then
                    C0 = Replace(calado, ",", ".")
                Else
                    C0 = dstCaudal.Tables("tablaCaudalC0").Rows(0).Item("Caudal")
                End If
                If (dstCaudal.Tables("tablaCaudalC2").Rows.Count = 0) Then
                    C2 = Replace(calado, ",", ".")
                Else
                    C2 = dstCaudal.Tables("tablaCaudalC2").Rows(0).Item("caudal")
                End If
                v_caudal = ((calado - N0) / (N2 - N0)) * (C2 - C0) + C0
                Return Format(v_caudal, "#,##0.###")
            Else
                Return dstCaudal.Tables("tablaCurvas").Rows(0).Item("Caudal")
            End If

        ElseIf NumCurvasGasto = 2 Then
            'tendremos que calcular el caudal de flotador y/o el de molinete y en función del caudal
            'obtenido y del nivel k ya lo tenemos haremos la interpolación
            'CALCULO POR INTERPOLACION
            'For i = 0 To RegimenCurva.Count - 1
            daCaudal.SelectCommand.CommandText = "SELECT top 1 Nivel " & _
                                                   "FROM PVYCR_CurvasAcequias_Valores " & _
                                                   "WHERE Cod_Curva = " & RegimenCurva & " And Nivel < " & Replace(calado, ",", ".") & _
                                                   " order by nivel desc"
            daCaudal.Fill(dstCaudal, "tablaCaudalN0")

            daCaudal.SelectCommand.CommandText = "SELECT top 1 Nivel " & _
                                                   "FROM PVYCR_CurvasAcequias_Valores " & _
                                                   "WHERE Cod_Curva = " & RegimenCurva & " And Nivel > " & Replace(calado, ",", ".") & _
                                                   " order by nivel"

            daCaudal.Fill(dstCaudal, "tablaCaudalN2")

            daCaudal.SelectCommand.CommandText = "SELECT top 1 Caudal " & _
                                                   "FROM PVYCR_CurvasAcequias_Valores " & _
                                                   "WHERE Cod_Curva = " & RegimenCurva & " And Nivel < " & Replace(calado, ",", ".") & _
                                                   " order by nivel desc"
            daCaudal.Fill(dstCaudal, "tablaCaudalC0")

            daCaudal.SelectCommand.CommandText = "SELECT top 1 Caudal " & _
                                                   "FROM PVYCR_CurvasAcequias_Valores " & _
                                                   "WHERE Cod_Curva = " & RegimenCurva & " And Nivel > " & Replace(calado, ",", ".") & _
                                                   " order by nivel"
            daCaudal.Fill(dstCaudal, "tablaCaudalC2")
            'Si no tienen valores ni por arriba ni por bajo, nos quedamos con el que nos pasan
            If (dstCaudal.Tables("tablaCaudalN0").Rows.Count = 0) Then
                N0 = Replace(calado, ",", ".")
            Else
                N0 = utiles.nullACero(dstCaudal.Tables("tablaCaudalN0").Rows(0).Item("Nivel"))
            End If
            If (dstCaudal.Tables("tablaCaudalN2").Rows.Count = 0) Then
                N2 = Replace(calado, ",", ".")
            Else
                N2 = utiles.nullACero(dstCaudal.Tables("tablaCaudalN2").Rows(0).Item("Nivel"))
            End If
            If (dstCaudal.Tables("tablaCaudalC0").Rows.Count = 0) Then
                C0 = Replace(calado, ",", ".")
            Else
                C0 = utiles.nullACero(dstCaudal.Tables("tablaCaudalC0").Rows(0).Item("Caudal"))
            End If
            If (dstCaudal.Tables("tablaCaudalC2").Rows.Count = 0) Then
                C2 = Replace(calado, ",", ".")
            Else
                C2 = utiles.nullACero(dstCaudal.Tables("tablaCaudalC2").Rows(0).Item("caudal"))
            End If
            v_caudal = ((calado - N0) / (N2 - N0)) * (C2 - C0) + C0

            Return Format(v_caudal, "#,##0.###")

            'Next
            'Nos quedaremos con el que tenga menor caudal
            'If v_caudal < v_caudal_ant Then
            'v_caudal_ant = v_caudal
            'End If

        End If
    End Function
    Shared Function CalculosTipoObtCaudal(ByVal tipocaudal As String, ByVal elPanel As String, ByVal A1 As Decimal, _
 ByVal A2 As Decimal, ByVal A3 As Decimal, ByVal B1 As Decimal, ByVal B2 As Decimal, ByVal B3 As Decimal, ByVal B4 As Decimal, ByVal H12 As Decimal, ByVal H23 As Decimal, _
 ByVal H34 As Decimal, ByVal offset As Decimal, ByVal escala As Decimal, ByVal calado As Decimal, ByVal tiempo As Decimal, ByVal longitudFlotador As Decimal, _
 ByVal factorflotador As Decimal, ByVal B As Decimal, ByVal v11 As Decimal, ByVal v12 As Decimal, ByVal v21 As Decimal, _
 ByVal v22 As Decimal, ByVal v31 As Decimal, ByVal v32 As Decimal, ByVal diametro As Decimal, _
 ByRef superficieTeo As Decimal, ByRef altFangos As Decimal, ByRef superficieFangos As Decimal, ByRef superficieReal As Decimal, _
 ByRef velFlotador As Decimal, ByRef velMolinete As Decimal, ByRef valores_perfil As seccion) As Decimal

        'Dim superficieTeo, altFangos, superficieFangos, superficieReal, velFlotador, velMolinete, caudal_Molinete, caudal_Flotador As Decimal
        Dim caudal_Molinete, caudal_Flotador As Decimal

        ' Calculos Flotador
        If tipocaudal = "F" Then
            altFangos = calcularAltFangos("F", escala, offset, calado)
            'ncm nueva llamada inc 57, descomentar cuando diga JA, lo ha dicho el 10/11/2009
            superficieTeo = calcularSuperficieTeorica("F", B1, B2, B3, A1, A2, H12, H23, altFangos, escala, calado, diametro, offset, B4, A3, H34) ', eldataitem("B1_M"), eldataitem("B2_M"), eldataitem("B3_M"), eldataitem("H1_M"), eldataitem("H2_M"))
            'superficieTeo = calcularSuperficieTeorica("F", B1, B2, B3, A1, A2, H12, H23, altFangos, escala, calado, diametro, offset, Valores_perfil) ', eldataitem("B1_M"), eldataitem("B2_M"), eldataitem("B3_M"), eldataitem("H1_M"), eldataitem("H2_M"))
            superficieFangos = calcularSuperFangos("F", A2, H12, altFangos, B1, A1, H23)
            superficieReal = calcularSuperReal("F", superficieTeo, superficieFangos)
            'NCM si la superficie real es negativa no seguimos calculando y mostramos un mensaje al usuario 02/09/2008
            If superficieReal < 0 Or superficieTeo < 0 Then

                'CType(elPanel.FindControl("txtsuperficieTF"), TextBox).Text = Nothing
                'CType(elPanel.FindControl("txtaltF"), TextBox).Text = Nothing
                'CType(elPanel.FindControl("txtsuperficieFF"), TextBox).Text = Nothing
                'CType(elPanel.FindControl("txtsuperficieRF"), TextBox).Text = Nothing
                'CType(elPanel.FindControl("txtvelocidadF"), TextBox).Text = Nothing
                caudal_Flotador = -1
                Return caudal_Flotador
            Else
                velFlotador = calcularvelocidadF(tiempo, longitudFlotador, factorflotador)

                'CType(elPanel.FindControl("txtsuperficieTF"), TextBox).Text = String.Format("{0:#,##0.##}", superficieTeo)
                'CType(elPanel.FindControl("txtaltF"), TextBox).Text = String.Format("{0:#,##0.##}", altFangos)
                'CType(elPanel.FindControl("txtsuperficieFF"), TextBox).Text = String.Format("{0:#,##0.##}", superficieFangos)
                'CType(elPanel.FindControl("txtsuperficieRF"), TextBox).Text = String.Format("{0:#,##0.##}", superficieReal)
                'CType(elPanel.FindControl("txtvelocidadF"), TextBox).Text = String.Format("{0:#,##0.###}", velFlotador)

                'Comprobaremos si tenemos caudal por flotador
                caudal_Flotador = String.Format("{0:#,##0.###}", calcular_Caudal_FloMol(velFlotador, superficieReal))

                Return caudal_Flotador
            End If
        ElseIf tipocaudal = "A" Then
            ' Calculos Molinete
            'ncm nueva llamada inc57, decomentar cuando diga JA, lo ha dicho el 10/11/2009
            superficieTeo = calcularSuperficieTeorica("A", B1, B2, B3, A1, A2, H12, H23, altFangos, escala, calado, diametro, offset, B4, A3, H34)
            'superficieTeo = calcularSuperficieTeorica("A", B1, B2, B3, A1, A2, H12, H23, altFangos, escala, calado, diametro, offset, valores_perfil)
            If superficieTeo <= 0 Then
                'CType(elPanel.FindControl("txtsuperficieTM"), TextBox).Text = Nothing
                'CType(elPanel.FindControl("txtaltM"), TextBox).Text = Nothing
                'CType(elPanel.FindControl("txtsuperficieFM"), TextBox).Text = Nothing
                'CType(elPanel.FindControl("txtsuperficieRM"), TextBox).Text = Nothing
                'CType(elPanel.FindControl("txtvelocidadM"), TextBox).Text = Nothing
                Return -1
            Else
                altFangos = calcularAltFangos("A", escala, offset, calado)
                superficieFangos = calcularSuperFangos("A", A2, H12, altFangos, B1, A1, H23)
                superficieReal = calcularSuperReal("A", superficieTeo, superficieFangos)
                velMolinete = calcularvelocidadA(calado, B, v11, v12, v21, v22, v31, v32)
                'CType(elPanel.FindControl("txtsuperficieTM"), TextBox).Text = String.Format("{0:#,##0.##}", superficieTeo)
                'CType(elPanel.FindControl("txtaltM"), TextBox).Text = String.Format("{0:#,##0.##}", altFangos)
                'CType(elPanel.FindControl("txtsuperficieFM"), TextBox).Text = String.Format("{0:#,##0.##}", superficieFangos)
                'CType(elPanel.FindControl("txtsuperficieRM"), TextBox).Text = String.Format("{0:#,##0.##}", superficieReal)
                'CType(elPanel.FindControl("txtvelocidadM"), TextBox).Text = String.Format("{0:#,##0.###}", velMolinete)

                'Comprobaremos si tenemos caudal por molinete
                caudal_Molinete = String.Format("{0:#,##0.###}", calcular_Caudal_FloMol(velMolinete, superficieReal))
                Return caudal_Molinete
            End If
        End If

    End Function
    Shared Function calcular_Caudal_FloMol(ByVal velFlotador As Decimal, ByVal superficieReal As Decimal) As Decimal
        'calculamos el caudal para el tipo flotador y/o molinete
        If (utiles.nullACero(velFlotador) = 0) Or (utiles.nullACero(superficieReal) = 0) Then
            Return 0
        Else
            Return velFlotador * superficieReal
        End If
    End Function
    Shared Function calcularAltFangos(ByVal tipocaudal As String, ByVal Escala As Decimal, ByVal Offset As Decimal, ByVal Calado As Decimal) As Decimal
        If tipocaudal = "F" Then
            Return ((utiles.nullACero(Escala) + utiles.nullACero(Offset)) - utiles.nullACero(Calado))
        ElseIf tipocaudal = "A" Then
            Return ((utiles.nullACero(Escala) + utiles.nullACero(Offset)) - utiles.nullACero(Calado))
        End If
    End Function

    '' --------------------- funciones para el calculo de la superficie teorica -----------
    ''
    Shared Sub DefinicionesGeometricas_Grande(ByVal B1 As Decimal, ByVal B2 As Decimal, ByVal B3 As Decimal, ByVal A1 As Decimal, ByVal A2 As Decimal, ByVal H1 As Decimal, ByVal H2 As Decimal, ByVal altfangos As Decimal, ByVal escala As Decimal, ByVal calado As Decimal, ByRef valores_perfil As seccion, _
        ByRef p1 As puntos_recta, ByRef p2 As puntos_recta, ByRef p3 As puntos_recta, ByRef p4 As puntos_recta, ByRef p5 As puntos_recta, ByRef p6 As puntos_recta, _
        ByRef p7 As puntos_recta, ByRef p8 As puntos_recta, ByRef p9 As puntos_recta, ByRef p10 As puntos_recta, _
        ByRef recta12 As recta, ByRef recta23 As recta, ByRef recta34 As recta, ByRef recta45 As recta, ByRef recta56 As recta, ByRef recta92 As recta, ByRef recta510 As recta)
        With valores_perfil
            .A1 = A1
            .A2 = A2
            .B1 = B1
            .B2 = B2
            .B3 = B3
            .H1 = H1
            .H2 = H2
            '---------------------------------------------------------------------------
            '-- NCM 17/04/2009 comentado el codigo de Inma porque calcula la superficie teniendo ---
            '-- en cuenta el calado y se tiene que hacer con la escala
            '--.C1 = calado 
            '---------------------------------------------------------------------------
            .C1 = escala
        End With
        With p1
            .x = 0
            .y = valores_perfil.H1 + valores_perfil.H2
        End With
        With p2
            .x = valores_perfil.A1
            .y = valores_perfil.H1
        End With
        With p3
            .x = valores_perfil.A1 + valores_perfil.A2
            .y = 0
        End With
        With p4
            .x = valores_perfil.A1 + valores_perfil.A2 + valores_perfil.B1
            .y = 0
        End With
        With p5
            .x = valores_perfil.B2 + valores_perfil.A1
            .y = valores_perfil.H1
        End With
        With p6
            .x = valores_perfil.B3
            .y = valores_perfil.H2 + valores_perfil.H1
        End With
        With recta12
            If (p1.y - p2.y) = 0 Then
                .b = (p1.x - p2.x) / 1
                .a = p1.x - .b * p1.y
            Else
                .b = (p1.x - p2.x) / (p1.y - p2.y)
                .a = p1.x - .b * p1.y
            End If
        End With
        With recta23
            If (p2.y - p3.y) = 0 Then
                .b = (p2.x - p3.x) / 1
                .a = p2.x - .b * p2.y
            Else
                .b = (p2.x - p3.x) / (p2.y - p3.y)
                .a = p2.x - .b * p2.y
            End If
        End With
        With recta34
            If (p3.y - p4.y) = 0 Then
                .b = (p3.x - p4.x) / 1
                .a = p3.x - .b * p3.y
            Else
                .b = (p3.x - p4.x) / (p3.y - p4.y)
                .a = p3.x - .b * p3.y
            End If
        End With
        With recta45
            If (p4.y - p5.y) = 0 Then
                .b = (p4.x - p5.x) / 1
                .a = p4.x - .b * p4.y
            Else
                .b = (p4.x - p5.x) / (p4.y - p5.y)
                .a = p4.x - .b * p4.y
            End If
        End With
        With recta56
            If (p5.y - p6.y) = 0 Then
                .b = (p5.x - p6.x) / 1
                .a = p5.x - .b * p5.y
            Else
                .b = (p5.x - p6.x) / (p5.y - p6.y)
                .a = p5.x - .b * p5.y
            End If
        End With
        With p9
            .x = recta12.a + recta12.b * valores_perfil.C1
            .y = valores_perfil.C1
        End With
        With p10
            .x = recta56.a + recta56.b * valores_perfil.C1
            .y = valores_perfil.C1
        End With
    End Sub
    Shared Function areaSiGrande(ByRef p1 As puntos_recta, ByRef p2 As puntos_recta, ByRef p3 As puntos_recta, ByRef p4 As puntos_recta, ByRef p5 As puntos_recta, ByVal p6 As puntos_recta, _
        ByRef p7 As puntos_recta, ByRef p9 As puntos_recta, ByRef p10 As puntos_recta, _
        ByRef area12 As Double, ByRef area23 As Double, ByRef area34 As Double, ByRef area45 As Double, ByRef area56 As Double, ByRef area61 As Double, ByRef area52 As Double, _
        ByRef areaTot2 As Double, ByRef areaCal2 As Double, ByRef area92 As Double, ByRef area73 As Double, ByRef area48 As Double, ByRef area510 As Double, _
        ByRef area109 As Double, ByRef areaTot1 As Double, ByRef areaCal1 As Double, ByRef area93 As Double, ByRef area410 As Double _
        ) As Double

        area12 = (p1.x - p2.x) * (p1.y - p2.y) / 2 + (p1.x - p2.x) * p2.y
        area23 = (p2.x - p3.x) * (p2.y - p3.y) / 2 + (p2.x - p3.x) * p3.y
        area34 = (p3.x - p4.x) * (p3.y - p4.y) / 2 + (p3.x - p4.x) * p4.y
        area45 = (p4.x - p5.x) * (p4.y - p5.y) / 2 + (p4.x - p5.x) * p5.y
        area56 = (p5.x - p6.x) * (p5.y - p6.y) / 2 + (p5.x - p6.x) * p6.y
        area61 = (p6.x - p1.x) * (p6.y - p1.y) / 2 + (p6.x - p1.x) * p1.y
        areaTot1 = area12 + area23 + area34 + area45 + area61
        area92 = (p9.x - p2.x) * (p9.y - p2.y) / 2 + (p9.x - p2.x) * p2.y
        area510 = (p5.x - p10.x) * (p5.y - p10.y) / 2 + (p5.x - p10.x) * p10.y
        area109 = (p10.x - p9.x) * (p10.y - p9.y) / 2 + (p10.x - p9.x) * p9.y
        areaCal1 = area92 + area23 + area34 + area45 + area510 + area109    'en base al punto 92 si es grande estamos por encima de h1
        Return areaCal1
    End Function

    Shared Function areaSiPequena(byref p2 As puntos_recta,byref p3 As puntos_recta,byref p4 As puntos_recta,byref p5 As puntos_recta, _
        byref p9 As puntos_recta,byref p10 As puntos_recta,byref area23 As Double,byref area34 As Double,byref area45 As Double,byref area52 As Double, _
        byref area109 As Double,byref areaTot1 As Double,byref areaCal1 As Double,byref area93 As Double,byref area410 As Double) As Double
        area23 = (p2.x - p3.x) * (p2.y - p3.y) / 2 + (p2.x - p3.x) * p3.y
        area34 = (p3.x - p4.x) * (p3.y - p4.y) / 2 + (p3.x - p4.x) * p4.y
        area45 = (p4.x - p5.x) * (p4.y - p5.y) / 2 + (p4.x - p5.x) * p5.y
        area52 = (p5.x - p2.x) * (p5.y - p2.y) / 2 + (p5.x - p2.x) * p2.y
        areaTot1 = area23 + area34 + area45 + area52
        area93 = (p9.x - p3.x) * (p9.y - p3.y) / 2 + (p9.x - p3.x) * p3.y
        area410 = (p4.x - p10.x) * (p4.y - p10.y) / 2 + (p4.x - p10.x) * p10.y
        area109 = (p10.x - p9.x) * (p10.y - p9.y) / 2 + (p10.x - p9.x) * p9.y
        areaCal1 = area93 + area34 + area410 + area109
        Return areaCal1
    End Function
    Shared Function CalcularSuperficieCircular(ByVal diametro As Decimal, ByVal escala As Decimal) As Decimal
        Dim radio As Double, pi As Double, alturaseca As Double
        diametro = diametro / 1000
        pi = 3.141654
        radio = diametro / 2
        alturaseca = diametro - escala
        If alturaseca < 0 Then
            Return metodo2(0, 0, True)
        Else

        End If
        Return metodo2(radio, alturaseca, False)
    End Function
    Shared Function metodo2(ByVal radio As Double, ByVal alturaseca As Double, ByVal hay_error As Boolean) As Decimal
        Dim x As Double, superficieSeca As Double, superficieTotal As Double, superficieMojada As Double, arcocoseno As Double
        If hay_error = False Then
            x = (radio - alturaseca) / radio
            arcocoseno = Math.Acos((radio - alturaseca) / radio)
            superficieSeca = radio * radio * arcocoseno - (radio - alturaseca) * Math.Sqrt(2 * radio * alturaseca - (alturaseca * alturaseca))
            superficieTotal = PI * radio * radio
            superficieMojada = superficieTotal - superficieSeca
            Return superficieMojada
        Else
            Return -999
        End If
    End Function
    Shared Sub DefinicionesGeometricas(ByVal A1 As Decimal, ByVal A2 As Decimal, ByVal A3 As Decimal, ByVal B1 As Decimal, ByVal B2 As Decimal, ByVal B3 As Decimal, ByVal B4 As Decimal, ByVal H12 As Decimal, ByVal H23 As Decimal, ByVal H34 As Decimal, ByVal altfangos As Decimal, ByVal escala As Decimal, ByVal calado As Decimal, ByVal OFFSET As Decimal, _
    ByRef p1 As puntos_recta, ByRef p2 As puntos_recta, ByRef p3 As puntos_recta, ByRef p4 As puntos_recta, ByRef p5 As puntos_recta, ByRef p6 As puntos_recta, _
    ByRef p7 As puntos_recta, ByRef Valores_perfil As seccion, ByRef p11 As puntos_recta, ByRef p21 As puntos_recta, ByRef p31 As puntos_recta, ByRef p61 As puntos_recta, _
    ByRef p71 As puntos_recta, ByRef p81 As puntos_recta, ByRef p11fangos As puntos_recta, ByRef p21fangos As puntos_recta, ByRef p31fangos As puntos_recta, ByRef p61fangos As puntos_recta, _
    ByRef p71fangos As puntos_recta, ByRef p81fangos As puntos_recta, ByRef htotal As Double, ByRef hfangos As Double)

        Dim p8 As puntos_recta
        Dim recta12 As recta, recta23 As recta, recta34 As recta, recta45 As recta, recta56 As recta
        Dim recta67 As recta, recta78 As recta
        Dim hreal As Double

        '= HojaX.Cells(Fila, columna)
        With Valores_perfil
            .A1 = A1
            .A2 = A2
            .A3 = A3
            .B1 = B1
            .B2 = B2
            .B3 = B3
            .B4 = B4
            .H12 = H12
            .H23 = H23
            .H12 = H12
            .H34 = H34
            .O = OFFSET
            .E = escala
            .C = calado
        End With
        With p1
            .x = 0
            .y = Valores_perfil.H12 + Valores_perfil.H23 + Valores_perfil.H34
        End With
        With p2
            .x = Valores_perfil.A3
            .y = Valores_perfil.H12 + Valores_perfil.H23
        End With
        With p3
            .x = Valores_perfil.A2
            .y = Valores_perfil.H12
        End With
        With p4
            .x = Valores_perfil.A1
            .y = 0
        End With
        With p5
            .x = Valores_perfil.B1 + Valores_perfil.A1
            .y = 0
        End With
        With p6
            .x = Valores_perfil.B2 + Valores_perfil.A2
            .y = Valores_perfil.H12
        End With
        With p7
            .x = Valores_perfil.B3 + Valores_perfil.A3
            .y = Valores_perfil.H12 + Valores_perfil.H23
        End With
        With p8
            .x = Valores_perfil.B4
            .y = Valores_perfil.H12 + Valores_perfil.H23 + Valores_perfil.H34
        End With
        With recta12
            If (p1.y - p2.y) = 0 Then
                .b = (p1.x - p2.x) / 1
                .a = p1.x - .b * p1.y
            Else
                .b = (p1.x - p2.x) / (p1.y - p2.y)
                .a = p1.x - .b * p1.y
            End If
        End With
        With recta23
            If (p2.y - p3.y) = 0 Then
                .b = (p2.x - p3.x) / 1
                .a = p2.x - .b * p2.y
            Else
                .b = (p2.x - p3.x) / (p2.y - p3.y)
                .a = p2.x - .b * p2.y
            End If
        End With
        With recta34
            If (p3.y - p4.y) = 0 Then
                .b = (p3.x - p4.x) / 1
                .a = p3.x - .b * p3.y
            Else
                .b = (p3.x - p4.x) / (p3.y - p4.y)
                .a = p3.x - .b * p3.y
            End If
        End With
        With recta45
            If (p4.y - p5.y) = 0 Then
                .b = (p4.x - p5.x) / 1
                .a = p4.x - .b * p4.y
            Else
                .b = (p4.x - p5.x) / (p4.y - p5.y)
                .a = p4.x - .b * p4.y
            End If
        End With
        With recta56
            If (p5.y - p6.y) = 0 Then
                .b = (p5.x - p6.x) / 1
                .a = p5.x - .b * p5.y
            Else
                .b = (p5.x - p6.x) / (p5.y - p6.y)
                .a = p5.x - .b * p5.y
            End If
        End With
        With recta67
            If (p6.y - p7.y) = 0 Then
                .b = (p6.x - p7.x) / 1
                .a = p6.x - .b * p6.y
            Else
                .b = (p6.x - p7.x) / (p6.y - p7.y)
                .a = p6.x - .b * p6.y
            End If
        End With
        With recta78
            If (p7.y - p8.y) = 0 Then
                .b = (p7.x - p8.x) / 1
                .a = p7.x - .b * p8.y
            Else
                .b = (p7.x - p8.x) / (p7.y - p8.y)
                .a = p7.x - .b * p8.y
            End If
        End With
        htotal = Valores_perfil.E + Valores_perfil.O
        hreal = Valores_perfil.C
        hfangos = htotal - hreal
        With p11
            .x = recta12.a + recta12.b * htotal
            .y = htotal
        End With
        With p21
            .x = recta23.a + recta23.b * htotal
            .y = htotal
        End With
        With p31
            .x = recta34.a + recta34.b * htotal
            .y = htotal
        End With
        With p61
            .x = recta56.a + recta56.b * htotal
            .y = htotal
        End With
        With p71
            .x = recta67.a + recta67.b * htotal
            .y = htotal
        End With
        With p81
            .x = recta78.a + recta78.b * htotal
            .y = htotal
        End With
        With p11fangos
            .x = recta12.a + recta12.b * hfangos
            .y = hfangos
        End With
        With p21fangos
            .x = recta23.a + recta23.b * hfangos
            .y = hfangos
        End With
        With p31fangos
            .x = recta34.a + recta34.b * hfangos
            .y = hfangos
        End With
        With p61fangos
            .x = recta56.a + recta56.b * hfangos
            .y = hfangos
        End With
        With p71fangos
            .x = recta67.a + recta67.b * hfangos
            .y = hfangos
        End With
        With p81fangos
            .x = recta78.a + recta78.b * hfangos
            .y = hfangos
        End With
    End Sub
    Shared Function CalculoArea(ByVal p2 As puntos_recta, ByVal p3 As puntos_recta, ByVal p4 As puntos_recta, ByVal p5 As puntos_recta, ByVal p6 As puntos_recta, _
    ByVal p7 As puntos_recta, ByVal Valores_perfil As seccion, ByVal p11 As puntos_recta, ByVal p21 As puntos_recta, ByVal p31 As puntos_recta, ByVal p61 As puntos_recta, _
    ByVal p71 As puntos_recta, ByVal p81 As puntos_recta, ByVal p11fangos As puntos_recta, ByVal p21fangos As puntos_recta, ByVal p31fangos As puntos_recta, ByVal p61fangos As puntos_recta, _
    ByVal p71fangos As puntos_recta, ByVal p81fangos As puntos_recta, ByVal htotal As Double, ByVal hfangos As Double) As Double

        'Dim htotal As Double, hfangos As Double

        'ncm nuevos valores para calculo area 03/07/2009

        Dim area314 As Double, area561 As Double, area6131 As Double, area213 As Double, area671 As Double, area7121 As Double, area112 As Double, area67 As Double, area781 As Double, area8111 As Double, areaTotal As Double, areaFangos As Double, areaReal As Double
        Dim area45 As Double, area34 As Double, area56 As Double, area23 As Double

        If (htotal <= Valores_perfil.H12) Then
            area314 = (p31.x - p4.x) * (p31.y - p4.y) / 2 + (p31.x - p4.x) * p4.y
            area45 = (p4.x - p5.x) * (p4.y - p5.y) / 2 + (p4.x - p5.x) * p5.y
            area561 = (p5.x - p61.x) * (p5.y - p61.y) / 2 + (p5.x - p61.x) * p61.y
            area6131 = (((p61.x - p31.x) * (p61.y - p31.y)) / 2) + ((p61.x - p31.x) * p31.y)
            areaTotal = area314 + area45 + area561 + area6131
        End If

        If (Valores_perfil.H12 < htotal) And (htotal <= (Valores_perfil.H12 + Valores_perfil.H23)) Then
            area213 = (p21.x - p3.x) * (p21.y - p3.y) / 2 + (p21.x - p3.x) * p3.y
            area34 = (p3.x - p4.x) * (p3.y - p4.y) / 2 + (p3.x - p4.x) * p4.y
            area45 = (p4.x - p5.x) * (p4.y - p5.y) / 2 + (p4.x - p5.x) * p5.y
            area56 = (p5.x - p6.x) * (p5.y - p6.y) / 2 + (p5.x - p6.x) * p6.y
            area671 = (p6.x - p71.x) * (p6.y - p71.y) / 2 + (p6.x - p71.x) * p71.y
            area7121 = (p71.x - p21.x) * (p71.y - p21.y) / 2 + (p71.x - p21.x) * p21.y
            areaTotal = area213 + area34 + area45 + area56 + area671 + area7121
        End If

        If ((Valores_perfil.H12 + Valores_perfil.H23) < htotal) And (htotal <= (Valores_perfil.H12 + Valores_perfil.H23 + Valores_perfil.H34)) Then
            area112 = (p11.x - p2.x) * (p11.y - p2.y) / 2 + (p11.x - p2.x) * p2.y
            area23 = (p2.x - p3.x) * (p2.y - p3.y) / 2 + (p2.x - p3.x) * p3.y
            area34 = (p3.x - p4.x) * (p3.y - p4.y) / 2 + (p3.x - p4.x) * p4.y
            area45 = (p4.x - p5.x) * (p4.y - p5.y) / 2 + (p4.x - p5.x) * p5.y
            area56 = (p5.x - p6.x) * (p5.y - p6.y) / 2 + (p5.x - p6.x) * p6.y
            area67 = (p6.x - p7.x) * (p6.y - p7.y) / 2 + (p6.x - p7.x) * p7.y
            area781 = (p7.x - p81.x) * (p7.y - p81.y) / 2 + (p7.x - p81.x) * p81.y
            area8111 = (p81.x - p11.x) * (p81.y - p11.y) / 2 + (p81.x - p11.x) * p11.y
            areaTotal = area112 + area23 + area34 + area45 + area56 + area67 + area781 + area8111
        End If

        If (hfangos <= Valores_perfil.H12) Then
            area314 = (p31fangos.x - p4.x) * (p31fangos.y - p4.y) / 2 + (p31fangos.x - p4.x) * p4.y
            area45 = (p4.x - p5.x) * (p4.y - p5.y) / 2 + (p4.x - p5.x) * p5.y
            area561 = (p5.x - p61fangos.x) * (p5.y - p61fangos.y) / 2 + (p5.x - p61fangos.x) * p61fangos.y
            area6131 = (((p61fangos.x - p31fangos.x) * (p61fangos.y - p31fangos.y)) / 2) + ((p61fangos.x - p31fangos.x) * p31fangos.y)
            areaFangos = area314 + area45 + area561 + area6131
        End If

        If (Valores_perfil.H12 < hfangos) And (hfangos <= (Valores_perfil.H12 + Valores_perfil.H23)) Then
            area213 = (p21fangos.x - p3.x) * (p21fangos.y - p3.y) / 2 + (p21fangos.x - p3.x) * p3.y
            area34 = (p3.x - p4.x) * (p3.y - p4.y) / 2 + (p3.x - p4.x) * p4.y
            area45 = (p4.x - p5.x) * (p4.y - p5.y) / 2 + (p4.x - p5.x) * p5.y
            area56 = (p5.x - p6.x) * (p5.y - p6.y) / 2 + (p5.x - p6.x) * p6.y
            area671 = (p6.x - p71fangos.x) * (p6.y - p71fangos.y) / 2 + (p6.x - p71fangos.x) * p71fangos.y
            area7121 = (p71fangos.x - p21fangos.x) * (p71fangos.y - p21fangos.y) / 2 + (p71fangos.x - p21fangos.x) * p21fangos.y
            areaFangos = area213 + area34 + area45 + area56 + area671 + area7121
        End If

        If ((Valores_perfil.H12 + Valores_perfil.H23) < hfangos) And (hfangos <= (Valores_perfil.H12 + Valores_perfil.H23 + Valores_perfil.H34)) Then
            area112 = (p11fangos.x - p2.x) * (p11fangos.y - p2.y) / 2 + (p11fangos.x - p2.x) * p2.y
            area23 = (p2.x - p3.x) * (p2.y - p3.y) / 2 + (p2.x - p3.x) * p3.y
            area34 = (p3.x - p4.x) * (p3.y - p4.y) / 2 + (p3.x - p4.x) * p4.y
            area45 = (p4.x - p5.x) * (p4.y - p5.y) / 2 + (p4.x - p5.x) * p5.y
            area56 = (p5.x - p6.x) * (p5.y - p6.y) / 2 + (p5.x - p6.x) * p6.y
            area67 = (p6.x - p7.x) * (p6.y - p7.y) / 2 + (p6.x - p7.x) * p7.y
            area781 = (p7.x - p81fangos.x) * (p7.y - p81fangos.y) / 2 + (p7.x - p81fangos.x) * p81fangos.y
            area8111 = (p81fangos.x - p11fangos.x) * (p81fangos.y - p11fangos.y) / 2 + (p81fangos.x - p11fangos.x) * p11fangos.y
            areaFangos = area112 + area23 + area34 + area45 + area56 + area67 + area781 + area8111
        End If

        areaReal = areaTotal - areaFangos
        Return areaReal

        'Hoja3.Cells(20, 2) = p1.x
        'Hoja3.Cells(20, 4) = p1.y
        'Hoja3.Cells(20, 6) = p11.x
        'Hoja3.Cells(20, 8) = p11.Y
        'Hoja3.Cells(20, 10) = p11fangos.x
        'Hoja3.Cells(20, 12) = p11fangos.Y
        'Hoja3.Cells(21, 2) = p2.x
        'Hoja3.Cells(21, 4) = p2.y
        'Hoja3.Cells(21, 6) = p21.x
        'Hoja3.Cells(21, 8) = p21.Y
        'Hoja3.Cells(21, 10) = p21fangos.x
        'Hoja3.Cells(21, 12) = p21fangos.Y
        'Hoja3.Cells(22, 2) = p3.x
        'Hoja3.Cells(22, 4) = p3.y
        'Hoja3.Cells(22, 6) = p31.x
        'Hoja3.Cells(22, 8) = p31.Y
        'Hoja3.Cells(22, 10) = p31fangos.x
        'Hoja3.Cells(22, 12) = p31fangos.Y
        'Hoja3.Cells(23, 2) = p4.x
        'Hoja3.Cells(23, 4) = p4.y
        'Hoja3.Cells(24, 2) = p5.x
        'Hoja3.Cells(24, 4) = p5.y
        'Hoja3.Cells(25, 2) = p6.x
        'Hoja3.Cells(25, 4) = p6.y
        'Hoja3.Cells(25, 6) = p61.x
        'Hoja3.Cells(25, 8) = p61.Y
        'Hoja3.Cells(25, 10) = p61fangos.x
        'Hoja3.Cells(25, 12) = p61fangos.Y
        'Hoja3.Cells(26, 2) = p7.x
        'Hoja3.Cells(26, 4) = p7.y
        'Hoja3.Cells(26, 6) = p71.x
        'Hoja3.Cells(26, 8) = p71.Y
        'Hoja3.Cells(26, 10) = p71fangos.x
        'Hoja3.Cells(26, 12) = p71fangos.Y
        'Hoja3.Cells(27, 2) = p8.x
        'Hoja3.Cells(27, 4) = p8.y
        'Hoja3.Cells(27, 6) = p81.x
        'Hoja3.Cells(27, 8) = p81.Y
        'Hoja3.Cells(27, 10) = p81fangos.x
        'Hoja3.Cells(27, 12) = p81fangos.Y
        'Hoja3.Cells(28, 2) = recta12.a
        'Hoja3.Cells(29, 2) = recta12.b
        'Hoja3.Cells(30, 2) = recta23.a
        'Hoja3.Cells(31, 2) = recta23.b
        'Hoja3.Cells(32, 2) = recta34.a
        'Hoja3.Cells(33, 2) = recta34.b
        'Hoja3.Cells(34, 2) = recta45.a
        'Hoja3.Cells(35, 2) = recta45.b
        'Hoja3.Cells(36, 2) = recta56.a
        'Hoja3.Cells(37, 2) = recta56.b
        'Hoja3.Cells(38, 2) = recta67.a
        'Hoja3.Cells(39, 2) = recta67.b
        'Hoja3.Cells(40, 2) = recta78.a
        'Hoja3.Cells(41, 2) = recta78.b
        'Hoja3.Cells(42, 2) = area12
        'Hoja3.Cells(43, 2) = area23
        'Hoja3.Cells(44, 2) = area34
        'Hoja3.Cells(45, 2) = area45
        'Hoja3.Cells(46, 2) = area56
        'Hoja3.Cells(47, 2) = area67
        'Hoja3.Cells(48, 2) = area78
        'Hoja3.Cells(49, 2) = area81
        'Hoja3.Cells(50, 2) = area112
        'Hoja3.Cells(51, 2) = area213
        'Hoja3.Cells(52, 2) = area314
        'Hoja3.Cells(53, 2) = area561
        'Hoja3.Cells(54, 2) = area671
        'Hoja3.Cells(55, 2) = area781
        'Hoja3.Cells(56, 2) = area6131
        'Hoja3.Cells(57, 2) = area7121
        'Hoja3.Cells(58, 2) = area8111
        'Hoja3.Cells(59, 2) = areaTotal
        'Hoja3.Cells(60, 2) = areaFangos
        'Hoja3.Cells(61, 2) = areaReal
        'Hoja3.Cells(62, 2) = Htotal
        'Hoja3.Cells(63, 2) = Hfangos
    End Function
    ''------------------- Calculos de superficies---------------------
    '' comentado el 11/10/2009 ya que se cambian los valores de las A_M, B_m y H_M
    'Shared Function calcularSuperficieTeorica(ByVal tipocaudal As String, ByVal B1 As Decimal, ByVal B2 As Decimal, ByVal B3 As Decimal, ByVal A1 As Decimal, ByVal A2 As Decimal, ByVal H1 As Decimal, ByVal H2 As Decimal, ByVal altfangos As Decimal, ByVal escala As Decimal, ByVal calado As Decimal, ByVal diametro As Decimal, ByVal offset As Decimal, ByRef valores_perfil As seccion) As Decimal
    '    Dim p1 As puntos_recta, p2 As puntos_recta, p3 As puntos_recta, p4 As puntos_recta, p5 As puntos_recta, p6 As puntos_recta
    '    Dim p7 As puntos_recta, p8 As puntos_recta, p9 As puntos_recta, p10 As puntos_recta
    '    Dim recta12 As recta, recta23 As recta, recta34 As recta, recta45 As recta, recta56 As recta, recta92 As recta, recta510 As recta
    '    Dim recta73 As recta, recta48 As recta, recta67 As recta, recta78 As recta
    '    Dim area12 As Double, area23 As Double, area34 As Double, area45 As Double, area56 As Double, area61 As Double, area52 As Double
    '    Dim area92 As Double, area73 As Double, area48 As Double, area510 As Double, area109 As Double, areaTot1 As Double, areaCal1 As Double
    '    Dim area93 As Double, area410 As Double
    '    Dim areaTot2 As Double, areaCal2 As Double
    '    'ncm nuevos valores para calculo area 03/07/2009
    '    Dim p11 As puntos_recta, p21 As puntos_recta, p31 As puntos_recta, p61 As puntos_recta, p71 As puntos_recta, p81 As puntos_recta
    '    Dim p11fangos As puntos_recta, p21fangos As puntos_recta, p31fangos As puntos_recta, p61fangos As puntos_recta, p71fangos As puntos_recta, p81fangos As puntos_recta
    '    Dim area314 As Double, area561 As Double, area6131 As Double, area213 As Double, area671 As Double, area7121 As Double, area112 As Double, area67 As Double, area781 As Double, area8111 As Double, areaTotal As Double, areaFangos As Double, areaReal As Double

    '    If utiles.nullACero(diametro) = 0 Then
    '        DefinicionesGeometricas_Grande(B1, B2, B3, A1, A2, H1, H2, altfangos, escala, calado, valores_perfil, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, recta12, recta23, recta34, recta45, recta56, recta92, recta510)
    '        If valores_perfil.C1 > valores_perfil.H1 Then
    '            Return areaSiGrande(p1, p2, p3, p4, p5, p6, p7, p9, p10, area12, area23, area34, area45, area56, area61, area52, areaTot2, areaCal2, area92, area73, area48, area510, area109, areaTot1, areaCal1, area93, area410)
    '        Else
    '            Return areaSiPequena(p2, p3, p4, p5, p9, p10, area23, area34, area45, area52, area109, areaTot1, areaCal1, area93, area410)
    '        End If
    '    Else
    '        'Sección circular
    '        '---------------------------------------------------------------------------
    '        '-- NCM 17/04/2009 comentado el codigo de Inma porque calcula la superficie teniendo ---
    '        '-- en cuenta el calado y se tiene que hacer con la escala
    '        'Return CalcularSuperficieCircular(diametro, calado)
    '        '---------------------------------------------------------------------------
    '        Return CalcularSuperficieCircular(diametro, escala)
    '    End If


    'End Function
    Shared Function calcularSuperficieTeorica(ByVal tipocaudal As String, ByVal B1 As Decimal, ByVal B2 As Decimal, ByVal B3 As Decimal, ByVal A1 As Decimal, ByVal A2 As Decimal, ByVal H12 As Decimal, ByVal H23 As Decimal, ByVal altfangos As Decimal, ByVal escala As Decimal, ByVal calado As Decimal, ByVal diametro As Decimal, ByVal offset As Decimal, ByVal B4 As Decimal, ByVal A3 As Decimal, ByVal H34 As Decimal) As Decimal
        Dim Valores_perfil As seccion
        Dim p1 As puntos_recta, p2 As puntos_recta, p3 As puntos_recta, p4 As puntos_recta, p5 As puntos_recta, p6 As puntos_recta
        Dim p7 As puntos_recta

        'ncm nuevos valores para calculo area 03/07/2009
        Dim p11 As puntos_recta, p21 As puntos_recta, p31 As puntos_recta, p61 As puntos_recta, p71 As puntos_recta, p81 As puntos_recta
        Dim p11fangos As puntos_recta, p21fangos As puntos_recta, p31fangos As puntos_recta, p61fangos As puntos_recta, p71fangos As puntos_recta, p81fangos As puntos_recta

        Dim htotal, hfangos As Double

        '-------------------------------------- INICIO NCM 03/07/2009 ------------------------------------------
        '-------------------------------------- descomentar cuando lo diga JA esto es la incidencia 57----------
        If utiles.nullACero(diametro) = 0 Then
            DefinicionesGeometricas(A1, A2, A3, B1, B2, B3, B4, H12, H23, H34, altfangos, escala, calado, offset, _
            p1, p2, p3, p4, p5, p6, p7, Valores_perfil, p11, p21, p31, p61, p71, p81, p11fangos, p21fangos, p31fangos, p61fangos, p71fangos, p81fangos, htotal, hfangos)
            'DefinicionesGeometricas(2, 0.5, 0.5, 2, 4.5, 5.5, 6, 0.5, 1.25, 0.75, altfangos, 1.4, 0.8, 0.1)
            Return CalculoArea(p2, p3, p4, p5, p6, p7, Valores_perfil, p11, p21, p31, p61, p71, p81, p11fangos, p21fangos, p31fangos, p61fangos, p71fangos, p81fangos, htotal, hfangos)
        Else
            'Sección circular
            '---------------------------------------------------------------------------
            '-- NCM 17/04/2009 comentado el codigo de Inma porque calcula la superficie teniendo ---
            '-- en cuenta el calado y se tiene que hacer con la escala
            'Return CalcularSuperficieCircular(diametro, calado)
            '---------------------------------------------------------------------------
            Return CalcularSuperficieCircular(diametro, escala)
        End If
    End Function
    Shared Function calcularSuperFangos(ByVal tipocaudal As String, ByVal A2 As Decimal, ByVal H1 As Decimal, ByVal hFangos As Decimal, ByVal B1 As Decimal, ByVal A1 As Decimal, ByVal H2 As Decimal) As Decimal
        Dim b2 As Decimal
        If tipocaudal = "F" Then

            ' si el fango superase la altura de H1 deberíamos calcularlo substituyendo  A2 por A1 y H1 por H2

            If hFangos > H1 Then
                If H2 = 0 Then
                    Return 0
                Else
                    b2 = ((utiles.nullACero(A1) / H2) * utiles.nullACero(hFangos) * 2) + utiles.nullACero(B1)
                    Return ((b2 + B1) * hFangos) / 2
                End If
            Else
                If H1 = 0 Then
                    Return 0
                Else
                    b2 = ((utiles.nullACero(A2) / H1) * utiles.nullACero(hFangos) * 2) + utiles.nullACero(B1)
                    Return ((b2 + B1) * hFangos) / 2
                End If

            End If

        ElseIf tipocaudal = "A" Then
            If utiles.nullACero(H1) = 0 Or H1 = 0 Then
                Return 0
            Else
                ' si el fango superase la altura de H1 deberíamos calcularlo substituyendo  A2 por A1 y H1 por H2
                ' pero de momento no se va a calcular.
                If hFangos > H1 Then
                    If H2 = 0 Then
                        Return 0
                    Else
                        b2 = ((utiles.nullACero(A1) / H2) * utiles.nullACero(hFangos) * 2) + utiles.nullACero(B1)
                        Return ((b2 + B1) * hFangos) / 2
                    End If
                Else
                    If H1 = 0 Then
                        Return 0
                    Else
                        b2 = ((utiles.nullACero(A2) / H1) * utiles.nullACero(hFangos) * 2) + utiles.nullACero(B1)
                        Return ((b2 + B1) * hFangos) / 2
                    End If
                End If
            End If
        End If
    End Function
    Shared Function calcularSuperReal(ByVal tipocaudal As String, ByVal STeorica As Decimal, ByVal SFangos As Decimal) As Decimal
        If tipocaudal = "F" Then
            Return utiles.nullACero(STeorica) - utiles.nullACero(SFangos)
        ElseIf tipocaudal = "A" Then
            Return utiles.nullACero(STeorica) - utiles.nullACero(SFangos)
        End If
    End Function
    ''-------------------- Calculos velocidades -----------------------
    ''
    Shared Function calcularvelocidadF(ByVal Tiempo As Decimal, ByVal longitudFlotador As Decimal, ByVal factorFlotador As Decimal) As Decimal
        If utiles.nullACero(Tiempo) > 0 Then
            Return (longitudFlotador / Tiempo) * factorFlotador
        Else
            Return 0
        End If

    End Function
    Shared Function calcularvelocidadA(ByVal calado As Decimal, ByVal B As Decimal, ByVal v11 As Decimal, ByVal v12 As Decimal, ByVal v21 As Decimal, ByVal v22 As Decimal, ByVal v31 As Decimal, ByVal v32 As Decimal) As Decimal
        Dim z As Decimal = 0

        Dim i As Integer = 0, j As Integer
        Dim velocidad(5) As Double
        velocidad(0) = v11
        velocidad(1) = v12
        velocidad(2) = v21
        velocidad(3) = v22
        velocidad(4) = v31
        velocidad(5) = v32

        For j = 0 To 5
            If utiles.nullACero(velocidad(j)) <> 0 Then
                i = i + 1
                z = z + velocidad(j)
            End If
        Next

        If i <> 0 Then
            'NCM comentado porque el cliente quiere tres decimales
            'Return Math.Round(z / i, 2)
            Return (z / i)
        End If


    End Function

    '' ----------------------- calculo colores ------------------------------------
    Shared Function calcular_codigo_colores(Optional ByVal caudalC As Decimal = Nothing, Optional ByVal caudalA As Decimal = Nothing, Optional ByVal caudalF As Decimal = Nothing)
        Dim diferencialCA, diferencialCF, diferencial, resta As Decimal
        If caudalC > 0 And caudalA > 0 And caudalF > 0 Then
            diferencialCA = Math.Abs((caudalC - caudalA) / caudalC)
            diferencialCF = Math.Abs((caudalC - caudalF) / caudalC)
            If diferencialCA >= diferencialCF Then
                diferencial = diferencialCA
            Else
                diferencial = diferencialCF
            End If
        ElseIf caudalC > 0 And caudalA > 0 And utiles.nullACero(caudalF) = 0 Then
            diferencial = Abs((caudalC - caudalA) / (caudalC))
        ElseIf caudalC > 0 And caudalF > 0 And utiles.nullACero(caudalA) = 0 Then
            diferencial = Abs((caudalC - caudalF) / (caudalC))
        ElseIf caudalC > 0 And caudalF = 0 And utiles.nullACero(caudalA) = 0 Then
            Return "B"
        Else
            'devolveremos una N para indicar que no hay caudales y que no de debe marcar nada
            Return "N"
        End If

        If diferencial > 0.4 Then
            Return "R"
        ElseIf diferencial <= 0.4 And diferencial >= 0.2 Then
            Return "A"
        ElseIf diferencial < 0.2 Then
            Return "V"
        Else
            Return "B"
        End If

    End Function
    Shared Function ObtenerMolineteFlotadorAnterior(ByVal cod_fuente_datos As String, ByVal pfecha_medida As String, ByRef tipo As String, _
    ByVal codigoPVYCR As String, ByVal idElemento As String, ByVal dstacequias As DataSet) As String
        Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))
        Dim daAcequias As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)

        If dstacequias.Tables.Contains("TablaCaudalMolinete") Then
            dstacequias.Tables("TablaCaudalMolinete").Rows.Clear()
        End If
        If dstacequias.Tables.Contains("TablaCaudalFlotador") Then
            dstacequias.Tables("TablaCaudalFlotador").Rows.Clear()
        End If



        'obtenemos el codfuentedato

        '1) buscamos el caudal calculado por molinete para los tres días anteriores a nuestra fecha_medida, si lo encontramos será el que devolvamos
        '2) no molinete, hacemos lo mismo para flotador.
        '3) no flotador, devolvemos una N

        daAcequias.SelectCommand.CommandText = "SELECT Caudal_M3S FROM PVYCR_DatosAcequias where tipoobtencioncaudal ='A' and " & _
                        "idElementoMedida = '" & idElemento & "' and CodigoPVYCR = '" & codigoPVYCR & "' And Cod_Fuente_Dato = '" & cod_fuente_datos & "' and " & _
                        "fecha_medida between dateadd(day,-3,'" & pfecha_medida & "') and '" & pfecha_medida & "' order by fecha_medida desc"

        daAcequias.Fill(dstacequias, "TablaCaudalMolinete")
        If dstacequias.Tables("TablaCaudalMolinete").Rows.Count = 0 Then
            daAcequias.SelectCommand.CommandText = "SELECT Caudal_M3S FROM PVYCR_DatosAcequias where tipoobtencioncaudal ='F' and " & _
            "idElementoMedida = '" & idElemento & "' and CodigoPVYCR = '" & codigoPVYCR & "' And Cod_Fuente_Dato = '" & cod_fuente_datos & "' and " & _
                        "fecha_medida between dateadd(day,-3,'" & pfecha_medida & "') and '" & pfecha_medida & "' order by fecha_medida desc"

            daAcequias.Fill(dstacequias, "TablaCaudalFlotador")
            If dstacequias.Tables("TablaCaudalFlotador").Rows.Count = 0 Then
                tipo = "N"
                Return "N"
            Else
                tipo = "F"
                Return dstacequias.Tables("TablaCaudalFlotador").Rows(0).Item("caudal_m3s").ToString
            End If
        Else
            tipo = "A"
            Return dstacequias.Tables("TablaCaudalMolinete").Rows(0).Item("caudal_m3s").ToString
        End If

    End Function
    Shared Function ObtenerCurvaMasProbable(ByVal vcodigoPVYCR As String, ByVal videlemento As String) As String
        Dim conexion As SqlClient.SqlConnection = New System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings("dsnsegura_migracion"))

        Dim daProbabilidadCurvas As SqlClient.SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter("", conexion)
        Dim dstProbabilidad As DataSet = New System.Data.DataSet()
        dstProbabilidad.Clear()
        daProbabilidadCurvas.SelectCommand.CommandText = "SELECT REGIMEN,Cod_Curva,Probabilidad " & _
                                     "FROM PVYCR_CurvasAcequias " & _
                                     "WHERE codigoPVYCR = '" & vcodigoPVYCR & "' and idElementoMedida = '" & videlemento & "' and FECHA_FIN_USO >= getdate() " & _
                                     " and regimen <> 'CT' " & _
                                     "order by Probabilidad DESC "

        daProbabilidadCurvas.Fill(dstProbabilidad, "TablaProbabilidad")

        Return dstProbabilidad.Tables("tablaProbabilidad").Rows(0).Item("cod_curva").ToString

    End Function
End Class
