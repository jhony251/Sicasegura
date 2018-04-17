﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.3603
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Data.Linq
Imports System.Data.Linq.Mapping
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Reflection


<System.Data.Linq.Mapping.DatabaseAttribute(Name:="DataSource")>  _
Partial Public Class datosDataContext
	Inherits System.Data.Linq.DataContext
	
	Private Shared mappingSource As System.Data.Linq.Mapping.MappingSource = New AttributeMappingSource
	
  #Region "Extensibility Method Definitions"
  Partial Private Sub OnCreated()
  End Sub
  Partial Private Sub InsertTPuntos_PVY(instance As TPuntos_PVY)
    End Sub
  Partial Private Sub UpdateTPuntos_PVY(instance As TPuntos_PVY)
    End Sub
  Partial Private Sub DeleteTPuntos_PVY(instance As TPuntos_PVY)
    End Sub
  #End Region
	
	Public Sub New()
		MyBase.New(Global.System.Configuration.ConfigurationManager.ConnectionStrings("DataSourceConnectionString").ConnectionString, mappingSource)
		OnCreated
	End Sub
	
	Public Sub New(ByVal connection As String)
		MyBase.New(connection, mappingSource)
		OnCreated
	End Sub
	
	Public Sub New(ByVal connection As System.Data.IDbConnection)
		MyBase.New(connection, mappingSource)
		OnCreated
	End Sub
	
	Public Sub New(ByVal connection As String, ByVal mappingSource As System.Data.Linq.Mapping.MappingSource)
		MyBase.New(connection, mappingSource)
		OnCreated
	End Sub
	
	Public Sub New(ByVal connection As System.Data.IDbConnection, ByVal mappingSource As System.Data.Linq.Mapping.MappingSource)
		MyBase.New(connection, mappingSource)
		OnCreated
	End Sub
	
	Public ReadOnly Property TCauces_PVY() As System.Data.Linq.Table(Of TCauces_PVY)
		Get
			Return Me.GetTable(Of TCauces_PVY)
		End Get
	End Property
	
	Public ReadOnly Property PVYCR_DatosAcequias_NIVUS() As System.Data.Linq.Table(Of PVYCR_DatosAcequias_NIVUS)
		Get
			Return Me.GetTable(Of PVYCR_DatosAcequias_NIVUS)
		End Get
	End Property
	
	Public ReadOnly Property TPuntos_PVY() As System.Data.Linq.Table(Of TPuntos_PVY)
		Get
			Return Me.GetTable(Of TPuntos_PVY)
		End Get
	End Property
	
	Public ReadOnly Property TablaAcequias() As System.Data.Linq.Table(Of TablaAcequias)
		Get
			Return Me.GetTable(Of TablaAcequias)
		End Get
	End Property
	
	Public ReadOnly Property TablaListado() As System.Data.Linq.Table(Of TablaListado)
		Get
			Return Me.GetTable(Of TablaListado)
		End Get
	End Property
	
	Public ReadOnly Property TiposVariables() As System.Data.Linq.Table(Of TiposVariables)
		Get
			Return Me.GetTable(Of TiposVariables)
		End Get
	End Property
End Class

<Table(Name:="dbo.TCauces_PVY")>  _
Partial Public Class TCauces_PVY
	
	Private _CodigoCauce As String
	
	Private _DenominacionCauce As String
	
	Public Sub New()
		MyBase.New
	End Sub
	
	<Column(Storage:="_CodigoCauce", DbType:="VarChar(20) NOT NULL", CanBeNull:=false)>  _
	Public Property CodigoCauce() As String
		Get
			Return Me._CodigoCauce
		End Get
		Set
			If (String.Equals(Me._CodigoCauce, value) = false) Then
				Me._CodigoCauce = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_DenominacionCauce", DbType:="VarChar(255)")>  _
	Public Property DenominacionCauce() As String
		Get
			Return Me._DenominacionCauce
		End Get
		Set
			If (String.Equals(Me._DenominacionCauce, value) = false) Then
				Me._DenominacionCauce = value
			End If
		End Set
	End Property
End Class

<Table(Name:="dbo.PVYCR_DatosAcequias_NIVUS")>  _
Partial Public Class PVYCR_DatosAcequias_NIVUS
	
	Private _CodigoPVYCR As String
	
	Private _Fecha As Date
	
	Private _Hora As Date
	
	Private _Calado_M As System.Nullable(Of Decimal)
	
	Private _Caudal_M3S As System.Nullable(Of Decimal)
	
	Private _Velocidad_MS As System.Nullable(Of Decimal)
	
	Public Sub New()
		MyBase.New
	End Sub
	
	<Column(Storage:="_CodigoPVYCR", DbType:="VarChar(20) NOT NULL", CanBeNull:=false)>  _
	Public Property CodigoPVYCR() As String
		Get
			Return Me._CodigoPVYCR
		End Get
		Set
			If (String.Equals(Me._CodigoPVYCR, value) = false) Then
				Me._CodigoPVYCR = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Fecha", DbType:="DateTime NOT NULL")>  _
	Public Property Fecha() As Date
		Get
			Return Me._Fecha
		End Get
		Set
			If ((Me._Fecha = value)  _
						= false) Then
				Me._Fecha = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Hora", DbType:="DateTime NOT NULL")>  _
	Public Property Hora() As Date
		Get
			Return Me._Hora
		End Get
		Set
			If ((Me._Hora = value)  _
						= false) Then
				Me._Hora = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Calado_M", DbType:="Decimal(14,3)")>  _
	Public Property Calado_M() As System.Nullable(Of Decimal)
		Get
			Return Me._Calado_M
		End Get
		Set
			If (Me._Calado_M.Equals(value) = false) Then
				Me._Calado_M = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Caudal_M3S", DbType:="Decimal(14,3)")>  _
	Public Property Caudal_M3S() As System.Nullable(Of Decimal)
		Get
			Return Me._Caudal_M3S
		End Get
		Set
			If (Me._Caudal_M3S.Equals(value) = false) Then
				Me._Caudal_M3S = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Velocidad_MS", DbType:="Decimal(14,3)")>  _
	Public Property Velocidad_MS() As System.Nullable(Of Decimal)
		Get
			Return Me._Velocidad_MS
		End Get
		Set
			If (Me._Velocidad_MS.Equals(value) = false) Then
				Me._Velocidad_MS = value
			End If
		End Set
	End Property
End Class

<Table(Name:="dbo.TPuntos_PVY")>  _
Partial Public Class TPuntos_PVY
	Implements System.ComponentModel.INotifyPropertyChanging, System.ComponentModel.INotifyPropertyChanged
	
	Private Shared emptyChangingEventArgs As PropertyChangingEventArgs = New PropertyChangingEventArgs(String.Empty)
	
	Private _CodigoPVYCR As String
	
	Private _CodigoCauce As String
	
	Private _DenominacionPunto As String
	
	Private _Observaciones As String
	
	Private _TipoSensor As System.Nullable(Of Char)
	
	Private _Acceso As String
	
	Private _X_UTM As System.Nullable(Of Integer)
	
	Private _Y_UTM As System.Nullable(Of Integer)
	
	Private _PorcentajeRegable As System.Nullable(Of Single)
	
	Private _B As System.Nullable(Of Decimal)
	
	Private _B2 As System.Nullable(Of Decimal)
	
	Private _H As System.Nullable(Of Decimal)
	
	Private _Ti As System.Nullable(Of Single)
	
	Private _Td As System.Nullable(Of Single)
	
	Private _Diametro_mm As System.Nullable(Of Short)
	
	Private _FactorFlotador As System.Nullable(Of Single)
	
	Private _TipoPunto As System.Nullable(Of Char)
	
	Private _CodigoDataLogger As String
	
	Private _UsadoEnParteOficial As Boolean
	
	Private _PKS As System.Nullable(Of Decimal)
	
	Private _PKA As System.Nullable(Of Decimal)
	
	Private _RIO As String
	
	Private _Longitud As System.Nullable(Of Integer)
	
	Private _DN_PK As Integer
	
	Private _DN_X As System.Nullable(Of Integer)
	
	Private _DN_Y As System.Nullable(Of Integer)
	
    #Region "Extensibility Method Definitions"
    Partial Private Sub OnLoaded()
    End Sub
    Partial Private Sub OnValidate(action As System.Data.Linq.ChangeAction)
    End Sub
    Partial Private Sub OnCreated()
    End Sub
    Partial Private Sub OnCodigoPVYCRChanging(value As String)
    End Sub
    Partial Private Sub OnCodigoPVYCRChanged()
    End Sub
    Partial Private Sub OnCodigoCauceChanging(value As String)
    End Sub
    Partial Private Sub OnCodigoCauceChanged()
    End Sub
    Partial Private Sub OnDenominacionPuntoChanging(value As String)
    End Sub
    Partial Private Sub OnDenominacionPuntoChanged()
    End Sub
    Partial Private Sub OnObservacionesChanging(value As String)
    End Sub
    Partial Private Sub OnObservacionesChanged()
    End Sub
    Partial Private Sub OnTipoSensorChanging(value As System.Nullable(Of Char))
    End Sub
    Partial Private Sub OnTipoSensorChanged()
    End Sub
    Partial Private Sub OnAccesoChanging(value As String)
    End Sub
    Partial Private Sub OnAccesoChanged()
    End Sub
    Partial Private Sub OnX_UTMChanging(value As System.Nullable(Of Integer))
    End Sub
    Partial Private Sub OnX_UTMChanged()
    End Sub
    Partial Private Sub OnY_UTMChanging(value As System.Nullable(Of Integer))
    End Sub
    Partial Private Sub OnY_UTMChanged()
    End Sub
    Partial Private Sub OnPorcentajeRegableChanging(value As System.Nullable(Of Single))
    End Sub
    Partial Private Sub OnPorcentajeRegableChanged()
    End Sub
    Partial Private Sub OnBChanging(value As System.Nullable(Of Decimal))
    End Sub
    Partial Private Sub OnBChanged()
    End Sub
    Partial Private Sub OnB2Changing(value As System.Nullable(Of Decimal))
    End Sub
    Partial Private Sub OnB2Changed()
    End Sub
    Partial Private Sub OnHChanging(value As System.Nullable(Of Decimal))
    End Sub
    Partial Private Sub OnHChanged()
    End Sub
    Partial Private Sub OnTiChanging(value As System.Nullable(Of Single))
    End Sub
    Partial Private Sub OnTiChanged()
    End Sub
    Partial Private Sub OnTdChanging(value As System.Nullable(Of Single))
    End Sub
    Partial Private Sub OnTdChanged()
    End Sub
    Partial Private Sub OnDiametro_mmChanging(value As System.Nullable(Of Short))
    End Sub
    Partial Private Sub OnDiametro_mmChanged()
    End Sub
    Partial Private Sub OnFactorFlotadorChanging(value As System.Nullable(Of Single))
    End Sub
    Partial Private Sub OnFactorFlotadorChanged()
    End Sub
    Partial Private Sub OnTipoPuntoChanging(value As System.Nullable(Of Char))
    End Sub
    Partial Private Sub OnTipoPuntoChanged()
    End Sub
    Partial Private Sub OnCodigoDataLoggerChanging(value As String)
    End Sub
    Partial Private Sub OnCodigoDataLoggerChanged()
    End Sub
    Partial Private Sub OnUsadoEnParteOficialChanging(value As Boolean)
    End Sub
    Partial Private Sub OnUsadoEnParteOficialChanged()
    End Sub
    Partial Private Sub OnPKSChanging(value As System.Nullable(Of Decimal))
    End Sub
    Partial Private Sub OnPKSChanged()
    End Sub
    Partial Private Sub OnPKAChanging(value As System.Nullable(Of Decimal))
    End Sub
    Partial Private Sub OnPKAChanged()
    End Sub
    Partial Private Sub OnRIOChanging(value As String)
    End Sub
    Partial Private Sub OnRIOChanged()
    End Sub
    Partial Private Sub OnLongitudChanging(value As System.Nullable(Of Integer))
    End Sub
    Partial Private Sub OnLongitudChanged()
    End Sub
    Partial Private Sub OnDN_PKChanging(value As Integer)
    End Sub
    Partial Private Sub OnDN_PKChanged()
    End Sub
    Partial Private Sub OnDN_XChanging(value As System.Nullable(Of Integer))
    End Sub
    Partial Private Sub OnDN_XChanged()
    End Sub
    Partial Private Sub OnDN_YChanging(value As System.Nullable(Of Integer))
    End Sub
    Partial Private Sub OnDN_YChanged()
    End Sub
    #End Region
	
	Public Sub New()
		MyBase.New
		OnCreated
	End Sub
	
	<Column(Storage:="_CodigoPVYCR", DbType:="VarChar(20) NOT NULL", CanBeNull:=false)>  _
	Public Property CodigoPVYCR() As String
		Get
			Return Me._CodigoPVYCR
		End Get
		Set
			If (String.Equals(Me._CodigoPVYCR, value) = false) Then
				Me.OnCodigoPVYCRChanging(value)
				Me.SendPropertyChanging
				Me._CodigoPVYCR = value
				Me.SendPropertyChanged("CodigoPVYCR")
				Me.OnCodigoPVYCRChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_CodigoCauce", DbType:="VarChar(20) NOT NULL", CanBeNull:=false)>  _
	Public Property CodigoCauce() As String
		Get
			Return Me._CodigoCauce
		End Get
		Set
			If (String.Equals(Me._CodigoCauce, value) = false) Then
				Me.OnCodigoCauceChanging(value)
				Me.SendPropertyChanging
				Me._CodigoCauce = value
				Me.SendPropertyChanged("CodigoCauce")
				Me.OnCodigoCauceChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_DenominacionPunto", DbType:="VarChar(255)")>  _
	Public Property DenominacionPunto() As String
		Get
			Return Me._DenominacionPunto
		End Get
		Set
			If (String.Equals(Me._DenominacionPunto, value) = false) Then
				Me.OnDenominacionPuntoChanging(value)
				Me.SendPropertyChanging
				Me._DenominacionPunto = value
				Me.SendPropertyChanged("DenominacionPunto")
				Me.OnDenominacionPuntoChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Observaciones", DbType:="NVarChar(255)")>  _
	Public Property Observaciones() As String
		Get
			Return Me._Observaciones
		End Get
		Set
			If (String.Equals(Me._Observaciones, value) = false) Then
				Me.OnObservacionesChanging(value)
				Me.SendPropertyChanging
				Me._Observaciones = value
				Me.SendPropertyChanged("Observaciones")
				Me.OnObservacionesChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_TipoSensor", DbType:="VarChar(1)")>  _
	Public Property TipoSensor() As System.Nullable(Of Char)
		Get
			Return Me._TipoSensor
		End Get
		Set
			If (Me._TipoSensor.Equals(value) = false) Then
				Me.OnTipoSensorChanging(value)
				Me.SendPropertyChanging
				Me._TipoSensor = value
				Me.SendPropertyChanged("TipoSensor")
				Me.OnTipoSensorChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Acceso", DbType:="Text", UpdateCheck:=UpdateCheck.Never)>  _
	Public Property Acceso() As String
		Get
			Return Me._Acceso
		End Get
		Set
			If (String.Equals(Me._Acceso, value) = false) Then
				Me.OnAccesoChanging(value)
				Me.SendPropertyChanging
				Me._Acceso = value
				Me.SendPropertyChanged("Acceso")
				Me.OnAccesoChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_X_UTM", DbType:="Int")>  _
	Public Property X_UTM() As System.Nullable(Of Integer)
		Get
			Return Me._X_UTM
		End Get
		Set
			If (Me._X_UTM.Equals(value) = false) Then
				Me.OnX_UTMChanging(value)
				Me.SendPropertyChanging
				Me._X_UTM = value
				Me.SendPropertyChanged("X_UTM")
				Me.OnX_UTMChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Y_UTM", DbType:="Int")>  _
	Public Property Y_UTM() As System.Nullable(Of Integer)
		Get
			Return Me._Y_UTM
		End Get
		Set
			If (Me._Y_UTM.Equals(value) = false) Then
				Me.OnY_UTMChanging(value)
				Me.SendPropertyChanging
				Me._Y_UTM = value
				Me.SendPropertyChanged("Y_UTM")
				Me.OnY_UTMChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_PorcentajeRegable", DbType:="Real")>  _
	Public Property PorcentajeRegable() As System.Nullable(Of Single)
		Get
			Return Me._PorcentajeRegable
		End Get
		Set
			If (Me._PorcentajeRegable.Equals(value) = false) Then
				Me.OnPorcentajeRegableChanging(value)
				Me.SendPropertyChanging
				Me._PorcentajeRegable = value
				Me.SendPropertyChanged("PorcentajeRegable")
				Me.OnPorcentajeRegableChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_B", DbType:="Decimal(14,2)")>  _
	Public Property B() As System.Nullable(Of Decimal)
		Get
			Return Me._B
		End Get
		Set
			If (Me._B.Equals(value) = false) Then
				Me.OnBChanging(value)
				Me.SendPropertyChanging
				Me._B = value
				Me.SendPropertyChanged("B")
				Me.OnBChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_B2", DbType:="Decimal(14,2)")>  _
	Public Property B2() As System.Nullable(Of Decimal)
		Get
			Return Me._B2
		End Get
		Set
			If (Me._B2.Equals(value) = false) Then
				Me.OnB2Changing(value)
				Me.SendPropertyChanging
				Me._B2 = value
				Me.SendPropertyChanged("B2")
				Me.OnB2Changed
			End If
		End Set
	End Property
	
	<Column(Storage:="_H", DbType:="Decimal(14,2)")>  _
	Public Property H() As System.Nullable(Of Decimal)
		Get
			Return Me._H
		End Get
		Set
			If (Me._H.Equals(value) = false) Then
				Me.OnHChanging(value)
				Me.SendPropertyChanging
				Me._H = value
				Me.SendPropertyChanged("H")
				Me.OnHChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Ti", DbType:="Real")>  _
	Public Property Ti() As System.Nullable(Of Single)
		Get
			Return Me._Ti
		End Get
		Set
			If (Me._Ti.Equals(value) = false) Then
				Me.OnTiChanging(value)
				Me.SendPropertyChanging
				Me._Ti = value
				Me.SendPropertyChanged("Ti")
				Me.OnTiChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Td", DbType:="Real")>  _
	Public Property Td() As System.Nullable(Of Single)
		Get
			Return Me._Td
		End Get
		Set
			If (Me._Td.Equals(value) = false) Then
				Me.OnTdChanging(value)
				Me.SendPropertyChanging
				Me._Td = value
				Me.SendPropertyChanged("Td")
				Me.OnTdChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Diametro_mm", DbType:="SmallInt")>  _
	Public Property Diametro_mm() As System.Nullable(Of Short)
		Get
			Return Me._Diametro_mm
		End Get
		Set
			If (Me._Diametro_mm.Equals(value) = false) Then
				Me.OnDiametro_mmChanging(value)
				Me.SendPropertyChanging
				Me._Diametro_mm = value
				Me.SendPropertyChanged("Diametro_mm")
				Me.OnDiametro_mmChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_FactorFlotador", DbType:="Real")>  _
	Public Property FactorFlotador() As System.Nullable(Of Single)
		Get
			Return Me._FactorFlotador
		End Get
		Set
			If (Me._FactorFlotador.Equals(value) = false) Then
				Me.OnFactorFlotadorChanging(value)
				Me.SendPropertyChanging
				Me._FactorFlotador = value
				Me.SendPropertyChanged("FactorFlotador")
				Me.OnFactorFlotadorChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_TipoPunto", DbType:="VarChar(1)")>  _
	Public Property TipoPunto() As System.Nullable(Of Char)
		Get
			Return Me._TipoPunto
		End Get
		Set
			If (Me._TipoPunto.Equals(value) = false) Then
				Me.OnTipoPuntoChanging(value)
				Me.SendPropertyChanging
				Me._TipoPunto = value
				Me.SendPropertyChanged("TipoPunto")
				Me.OnTipoPuntoChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_CodigoDataLogger", DbType:="VarChar(5)")>  _
	Public Property CodigoDataLogger() As String
		Get
			Return Me._CodigoDataLogger
		End Get
		Set
			If (String.Equals(Me._CodigoDataLogger, value) = false) Then
				Me.OnCodigoDataLoggerChanging(value)
				Me.SendPropertyChanging
				Me._CodigoDataLogger = value
				Me.SendPropertyChanged("CodigoDataLogger")
				Me.OnCodigoDataLoggerChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_UsadoEnParteOficial", DbType:="Bit NOT NULL")>  _
	Public Property UsadoEnParteOficial() As Boolean
		Get
			Return Me._UsadoEnParteOficial
		End Get
		Set
			If ((Me._UsadoEnParteOficial = value)  _
						= false) Then
				Me.OnUsadoEnParteOficialChanging(value)
				Me.SendPropertyChanging
				Me._UsadoEnParteOficial = value
				Me.SendPropertyChanged("UsadoEnParteOficial")
				Me.OnUsadoEnParteOficialChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_PKS", DbType:="Decimal(18,3)")>  _
	Public Property PKS() As System.Nullable(Of Decimal)
		Get
			Return Me._PKS
		End Get
		Set
			If (Me._PKS.Equals(value) = false) Then
				Me.OnPKSChanging(value)
				Me.SendPropertyChanging
				Me._PKS = value
				Me.SendPropertyChanged("PKS")
				Me.OnPKSChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_PKA", DbType:="Decimal(18,3)")>  _
	Public Property PKA() As System.Nullable(Of Decimal)
		Get
			Return Me._PKA
		End Get
		Set
			If (Me._PKA.Equals(value) = false) Then
				Me.OnPKAChanging(value)
				Me.SendPropertyChanging
				Me._PKA = value
				Me.SendPropertyChanged("PKA")
				Me.OnPKAChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_RIO", DbType:="NVarChar(25)")>  _
	Public Property RIO() As String
		Get
			Return Me._RIO
		End Get
		Set
			If (String.Equals(Me._RIO, value) = false) Then
				Me.OnRIOChanging(value)
				Me.SendPropertyChanging
				Me._RIO = value
				Me.SendPropertyChanged("RIO")
				Me.OnRIOChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_Longitud", DbType:="Int")>  _
	Public Property Longitud() As System.Nullable(Of Integer)
		Get
			Return Me._Longitud
		End Get
		Set
			If (Me._Longitud.Equals(value) = false) Then
				Me.OnLongitudChanging(value)
				Me.SendPropertyChanging
				Me._Longitud = value
				Me.SendPropertyChanged("Longitud")
				Me.OnLongitudChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_DN_PK", AutoSync:=AutoSync.OnInsert, DbType:="Int NOT NULL IDENTITY", IsPrimaryKey:=true, IsDbGenerated:=true)>  _
	Public Property DN_PK() As Integer
		Get
			Return Me._DN_PK
		End Get
		Set
			If ((Me._DN_PK = value)  _
						= false) Then
				Me.OnDN_PKChanging(value)
				Me.SendPropertyChanging
				Me._DN_PK = value
				Me.SendPropertyChanged("DN_PK")
				Me.OnDN_PKChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_DN_X", DbType:="Int")>  _
	Public Property DN_X() As System.Nullable(Of Integer)
		Get
			Return Me._DN_X
		End Get
		Set
			If (Me._DN_X.Equals(value) = false) Then
				Me.OnDN_XChanging(value)
				Me.SendPropertyChanging
				Me._DN_X = value
				Me.SendPropertyChanged("DN_X")
				Me.OnDN_XChanged
			End If
		End Set
	End Property
	
	<Column(Storage:="_DN_Y", DbType:="Int")>  _
	Public Property DN_Y() As System.Nullable(Of Integer)
		Get
			Return Me._DN_Y
		End Get
		Set
			If (Me._DN_Y.Equals(value) = false) Then
				Me.OnDN_YChanging(value)
				Me.SendPropertyChanging
				Me._DN_Y = value
				Me.SendPropertyChanged("DN_Y")
				Me.OnDN_YChanged
			End If
		End Set
	End Property
	
	Public Event PropertyChanging As PropertyChangingEventHandler Implements System.ComponentModel.INotifyPropertyChanging.PropertyChanging
	
	Public Event PropertyChanged As PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
	
	Protected Overridable Sub SendPropertyChanging()
		If ((Me.PropertyChangingEvent Is Nothing)  _
					= false) Then
			RaiseEvent PropertyChanging(Me, emptyChangingEventArgs)
		End If
	End Sub
	
	Protected Overridable Sub SendPropertyChanged(ByVal propertyName As [String])
		If ((Me.PropertyChangedEvent Is Nothing)  _
					= false) Then
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
		End If
	End Sub
End Class

<Table(Name:="")>  _
Partial Public Class TablaAcequias
	
	Private _CodigoPVYCR As String
	
	Private _idElementoMedida As String
	
	Private _Cod_Fuente_Dato As String
	
	Private _Fecha_Medida As String
	
	Private _Escala_M As String
	
	Private _Calado_M As String
	
	Private _TipoObtencionCaudal As String
	
	Private _RegimenCurva As String
	
	Private _Caudal_M3S As String
	
	Private _DUDA_CALIDAD As String
	
	Private _DescFuenteDato As String
	
	Private _Diferencial As String
	
	Private _Diferencial_acum As String
	
	Public Sub New()
		MyBase.New
	End Sub
	
	<Column(Storage:="_CodigoPVYCR")>  _
	Public Property CodigoPVYCR() As String
		Get
			Return Me._CodigoPVYCR
		End Get
		Set
			If (String.Equals(Me._CodigoPVYCR, value) = false) Then
				Me._CodigoPVYCR = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_idElementoMedida")>  _
	Public Property idElementoMedida() As String
		Get
			Return Me._idElementoMedida
		End Get
		Set
			If (String.Equals(Me._idElementoMedida, value) = false) Then
				Me._idElementoMedida = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Cod_Fuente_Dato")>  _
	Public Property Cod_Fuente_Dato() As String
		Get
			Return Me._Cod_Fuente_Dato
		End Get
		Set
			If (String.Equals(Me._Cod_Fuente_Dato, value) = false) Then
				Me._Cod_Fuente_Dato = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Fecha_Medida")>  _
	Public Property Fecha_Medida() As String
		Get
			Return Me._Fecha_Medida
		End Get
		Set
			If (String.Equals(Me._Fecha_Medida, value) = false) Then
				Me._Fecha_Medida = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Escala_M")>  _
	Public Property Escala_M() As String
		Get
			Return Me._Escala_M
		End Get
		Set
			If (String.Equals(Me._Escala_M, value) = false) Then
				Me._Escala_M = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Calado_M")>  _
	Public Property Calado_M() As String
		Get
			Return Me._Calado_M
		End Get
		Set
			If (String.Equals(Me._Calado_M, value) = false) Then
				Me._Calado_M = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_TipoObtencionCaudal")>  _
	Public Property TipoObtencionCaudal() As String
		Get
			Return Me._TipoObtencionCaudal
		End Get
		Set
			If (String.Equals(Me._TipoObtencionCaudal, value) = false) Then
				Me._TipoObtencionCaudal = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_RegimenCurva")>  _
	Public Property RegimenCurva() As String
		Get
			Return Me._RegimenCurva
		End Get
		Set
			If (String.Equals(Me._RegimenCurva, value) = false) Then
				Me._RegimenCurva = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Caudal_M3S")>  _
	Public Property Caudal_M3S() As String
		Get
			Return Me._Caudal_M3S
		End Get
		Set
			If (String.Equals(Me._Caudal_M3S, value) = false) Then
				Me._Caudal_M3S = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_DUDA_CALIDAD")>  _
	Public Property DUDA_CALIDAD() As String
		Get
			Return Me._DUDA_CALIDAD
		End Get
		Set
			If (String.Equals(Me._DUDA_CALIDAD, value) = false) Then
				Me._DUDA_CALIDAD = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_DescFuenteDato")>  _
	Public Property DescFuenteDato() As String
		Get
			Return Me._DescFuenteDato
		End Get
		Set
			If (String.Equals(Me._DescFuenteDato, value) = false) Then
				Me._DescFuenteDato = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Diferencial")>  _
	Public Property Diferencial() As String
		Get
			Return Me._Diferencial
		End Get
		Set
			If (String.Equals(Me._Diferencial, value) = false) Then
				Me._Diferencial = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Diferencial_acum")>  _
	Public Property Diferencial_acum() As String
		Get
			Return Me._Diferencial_acum
		End Get
		Set
			If (String.Equals(Me._Diferencial_acum, value) = false) Then
				Me._Diferencial_acum = value
			End If
		End Set
	End Property
End Class

<Table(Name:="")>  _
Partial Public Class TablaListado
	
	Private _Rama As String
	
	Private _DescTipoElem As String
	
	Private _Punto As String
	
	Private _Fecha_Medida As String
	
	Private _Fecha_MedidaF As String
	
	Private _Var1Titulo As String
	
	Private _Var1Valor As String
	
	Private _Var2Titulo As String
	
	Private _Var2Valor As String
	
	Private _Var3Titulo As String
	
	Private _Var3Valor As String
	
	Private _Var4Titulo As String
	
	Private _Var4Valor As String
	
	Private _Var4Visible As String
	
	Private _Var5Titulo As String
	
	Private _Var5Valor As String
	
	Private _Var5Visible As String
	
	Private _Filtro1 As String
	
	Private _Filtro2 As String
	
	Private _numLecturas As String
	
	Private _ElementoMedida As String
	
	Public Sub New()
		MyBase.New
	End Sub
	
	<Column(Storage:="_Rama")>  _
	Public Property Rama() As String
		Get
			Return Me._Rama
		End Get
		Set
			If (String.Equals(Me._Rama, value) = false) Then
				Me._Rama = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_DescTipoElem")>  _
	Public Property DescTipoElem() As String
		Get
			Return Me._DescTipoElem
		End Get
		Set
			If (String.Equals(Me._DescTipoElem, value) = false) Then
				Me._DescTipoElem = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Punto", CanBeNull:=false)>  _
	Public Property Punto() As String
		Get
			Return Me._Punto
		End Get
		Set
			If (String.Equals(Me._Punto, value) = false) Then
				Me._Punto = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Fecha_Medida", CanBeNull:=false)>  _
	Public Property Fecha_Medida() As String
		Get
			Return Me._Fecha_Medida
		End Get
		Set
			If (String.Equals(Me._Fecha_Medida, value) = false) Then
				Me._Fecha_Medida = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Fecha_MedidaF", CanBeNull:=false)>  _
	Public Property Fecha_MedidaF() As String
		Get
			Return Me._Fecha_MedidaF
		End Get
		Set
			If (String.Equals(Me._Fecha_MedidaF, value) = false) Then
				Me._Fecha_MedidaF = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Var1Titulo", CanBeNull:=false)>  _
	Public Property Var1Titulo() As String
		Get
			Return Me._Var1Titulo
		End Get
		Set
			If (String.Equals(Me._Var1Titulo, value) = false) Then
				Me._Var1Titulo = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Var1Valor")>  _
	Public Property Var1Valor() As String
		Get
			Return Me._Var1Valor
		End Get
		Set
			If (String.Equals(Me._Var1Valor, value) = false) Then
				Me._Var1Valor = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Var2Titulo", CanBeNull:=false)>  _
	Public Property Var2Titulo() As String
		Get
			Return Me._Var2Titulo
		End Get
		Set
			If (String.Equals(Me._Var2Titulo, value) = false) Then
				Me._Var2Titulo = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Var2Valor")>  _
	Public Property Var2Valor() As String
		Get
			Return Me._Var2Valor
		End Get
		Set
			If (String.Equals(Me._Var2Valor, value) = false) Then
				Me._Var2Valor = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Var3Titulo", CanBeNull:=false)>  _
	Public Property Var3Titulo() As String
		Get
			Return Me._Var3Titulo
		End Get
		Set
			If (String.Equals(Me._Var3Titulo, value) = false) Then
				Me._Var3Titulo = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Var3Valor")>  _
	Public Property Var3Valor() As String
		Get
			Return Me._Var3Valor
		End Get
		Set
			If (String.Equals(Me._Var3Valor, value) = false) Then
				Me._Var3Valor = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Var4Titulo")>  _
	Public Property Var4Titulo() As String
		Get
			Return Me._Var4Titulo
		End Get
		Set
			If (String.Equals(Me._Var4Titulo, value) = false) Then
				Me._Var4Titulo = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Var4Valor")>  _
	Public Property Var4Valor() As String
		Get
			Return Me._Var4Valor
		End Get
		Set
			If (String.Equals(Me._Var4Valor, value) = false) Then
				Me._Var4Valor = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Var4Visible")>  _
	Public Property Var4Visible() As String
		Get
			Return Me._Var4Visible
		End Get
		Set
			If (String.Equals(Me._Var4Visible, value) = false) Then
				Me._Var4Visible = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Var5Titulo")>  _
	Public Property Var5Titulo() As String
		Get
			Return Me._Var5Titulo
		End Get
		Set
			If (String.Equals(Me._Var5Titulo, value) = false) Then
				Me._Var5Titulo = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Var5Valor")>  _
	Public Property Var5Valor() As String
		Get
			Return Me._Var5Valor
		End Get
		Set
			If (String.Equals(Me._Var5Valor, value) = false) Then
				Me._Var5Valor = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Var5Visible")>  _
	Public Property Var5Visible() As String
		Get
			Return Me._Var5Visible
		End Get
		Set
			If (String.Equals(Me._Var5Visible, value) = false) Then
				Me._Var5Visible = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Filtro1")>  _
	Public Property Filtro1() As String
		Get
			Return Me._Filtro1
		End Get
		Set
			If (String.Equals(Me._Filtro1, value) = false) Then
				Me._Filtro1 = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Filtro2")>  _
	Public Property Filtro2() As String
		Get
			Return Me._Filtro2
		End Get
		Set
			If (String.Equals(Me._Filtro2, value) = false) Then
				Me._Filtro2 = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_numLecturas")>  _
	Public Property numLecturas() As String
		Get
			Return Me._numLecturas
		End Get
		Set
			If (String.Equals(Me._numLecturas, value) = false) Then
				Me._numLecturas = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_ElementoMedida")>  _
	Public Property ElementoMedida() As String
		Get
			Return Me._ElementoMedida
		End Get
		Set
			If (String.Equals(Me._ElementoMedida, value) = false) Then
				Me._ElementoMedida = value
			End If
		End Set
	End Property
End Class

<Table(Name:="")>  _
Partial Public Class TiposVariables
	
	Private _TipoVariable As String
	
	Private _Variable As String
	
	Private _Valor As Integer
	
	Public Sub New()
		MyBase.New
	End Sub
	
	<Column(Storage:="_TipoVariable", CanBeNull:=false)>  _
	Public Property TipoVariable() As String
		Get
			Return Me._TipoVariable
		End Get
		Set
			If (String.Equals(Me._TipoVariable, value) = false) Then
				Me._TipoVariable = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Variable", CanBeNull:=false)>  _
	Public Property Variable() As String
		Get
			Return Me._Variable
		End Get
		Set
			If (String.Equals(Me._Variable, value) = false) Then
				Me._Variable = value
			End If
		End Set
	End Property
	
	<Column(Storage:="_Valor")>  _
	Public Property Valor() As Integer
		Get
			Return Me._Valor
		End Get
		Set
			If ((Me._Valor = value)  _
						= false) Then
				Me._Valor = value
			End If
		End Set
	End Property
End Class
