﻿'------------------------------------------------------------------------------
' <auto-generated>
'     Este código fue generado por una herramienta.
'     Versión de runtime:4.0.30319.42000
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

<Assembly: Global.System.Data.Objects.DataClasses.EdmSchemaAttribute("f3275ae2-c93f-41be-8092-c8280db1b70d")> 

'Nombre de archivo original:
'Fecha de generación: 17/04/2018 13:20:33
Namespace DBSICAModel
    '''<summary>
    '''No hay ningún comentario para DBSICAModel.SICA_Visitas_a_campo en el esquema.
    '''</summary>
    '''<KeyProperties>
    '''FechaID
    '''EXP_ISM
    '''</KeyProperties>
    <Global.System.Data.Objects.DataClasses.EdmEntityTypeAttribute(NamespaceName:="DBSICAModel", Name:="SICA_Visitas_a_campo"),  _
     Global.System.Runtime.Serialization.DataContractAttribute(IsReference:=true),  _
     Global.System.Serializable()>  _
    Partial Public Class SICA_Visitas_a_campo
        Inherits Global.System.Data.Objects.DataClasses.EntityObject
        '''<summary>
        '''Crear un nuevo objeto SICA_Visitas_a_campo.
        '''</summary>
        '''<param name="fechaID">Valor inicial de FechaID.</param>
        '''<param name="eXP_ISM">Valor inicial de EXP_ISM.</param>
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Public Shared Function CreateSICA_Visitas_a_campo(ByVal fechaID As Date, ByVal eXP_ISM As String) As SICA_Visitas_a_campo
            Dim sICA_Visitas_a_campo As SICA_Visitas_a_campo = New SICA_Visitas_a_campo()
            sICA_Visitas_a_campo.FechaID = fechaID
            sICA_Visitas_a_campo.EXP_ISM = eXP_ISM
            Return sICA_Visitas_a_campo
        End Function
        '''<summary>
        '''No hay ningún comentario para la propiedad FechaID en el esquema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty:=true, IsNullable:=false),  _
         Global.System.Runtime.Serialization.DataMemberAttribute(),  _
         Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Public Property FechaID() As Date
            Get
                Return Me._FechaID
            End Get
            Set
                Me.OnFechaIDChanging(value)
                Me.ReportPropertyChanging("FechaID")
                Me._FechaID = Global.System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value)
                Me.ReportPropertyChanged("FechaID")
                Me.OnFechaIDChanged
            End Set
        End Property
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Private _FechaID As Date
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Partial Private Sub OnFechaIDChanging(ByVal value As Date)
        End Sub
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Partial Private Sub OnFechaIDChanged()
        End Sub
        '''<summary>
        '''No hay ningún comentario para la propiedad EXP_ISM en el esquema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(EntityKeyProperty:=true, IsNullable:=false),  _
         Global.System.Runtime.Serialization.DataMemberAttribute(),  _
         Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Public Property EXP_ISM() As String
            Get
                Return Me._EXP_ISM
            End Get
            Set
                Me.OnEXP_ISMChanging(value)
                Me.ReportPropertyChanging("EXP_ISM")
                Me._EXP_ISM = Global.System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, false)
                Me.ReportPropertyChanged("EXP_ISM")
                Me.OnEXP_ISMChanged
            End Set
        End Property
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Private _EXP_ISM As String
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Partial Private Sub OnEXP_ISMChanging(ByVal value As String)
        End Sub
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Partial Private Sub OnEXP_ISMChanged()
        End Sub
        '''<summary>
        '''No hay ningún comentario para la propiedad Tipo en el esquema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(),  _
         Global.System.Runtime.Serialization.DataMemberAttribute(),  _
         Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Public Property Tipo() As Global.System.Nullable(Of Integer)
            Get
                Return Me._Tipo
            End Get
            Set
                Me.OnTipoChanging(value)
                Me.ReportPropertyChanging("Tipo")
                Me._Tipo = Global.System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value)
                Me.ReportPropertyChanged("Tipo")
                Me.OnTipoChanged
            End Set
        End Property
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Private _Tipo As Global.System.Nullable(Of Integer)
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Partial Private Sub OnTipoChanging(ByVal value As Global.System.Nullable(Of Integer))
        End Sub
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Partial Private Sub OnTipoChanged()
        End Sub
        '''<summary>
        '''No hay ningún comentario para la propiedad Contacto en el esquema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(),  _
         Global.System.Runtime.Serialization.DataMemberAttribute(),  _
         Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Public Property Contacto() As String
            Get
                Return Me._Contacto
            End Get
            Set
                Me.OnContactoChanging(value)
                Me.ReportPropertyChanging("Contacto")
                Me._Contacto = Global.System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true)
                Me.ReportPropertyChanged("Contacto")
                Me.OnContactoChanged
            End Set
        End Property
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Private _Contacto As String
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Partial Private Sub OnContactoChanging(ByVal value As String)
        End Sub
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Partial Private Sub OnContactoChanged()
        End Sub
        '''<summary>
        '''No hay ningún comentario para la propiedad Fecha_visita en el esquema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(),  _
         Global.System.Runtime.Serialization.DataMemberAttribute(),  _
         Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Public Property Fecha_visita() As Global.System.Nullable(Of Date)
            Get
                Return Me._Fecha_visita
            End Get
            Set
                Me.OnFecha_visitaChanging(value)
                Me.ReportPropertyChanging("Fecha_visita")
                Me._Fecha_visita = Global.System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value)
                Me.ReportPropertyChanged("Fecha_visita")
                Me.OnFecha_visitaChanged
            End Set
        End Property
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Private _Fecha_visita As Global.System.Nullable(Of Date)
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Partial Private Sub OnFecha_visitaChanging(ByVal value As Global.System.Nullable(Of Date))
        End Sub
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Partial Private Sub OnFecha_visitaChanged()
        End Sub
        '''<summary>
        '''No hay ningún comentario para la propiedad Personal_SICA en el esquema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(),  _
         Global.System.Runtime.Serialization.DataMemberAttribute(),  _
         Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Public Property Personal_SICA() As String
            Get
                Return Me._Personal_SICA
            End Get
            Set
                Me.OnPersonal_SICAChanging(value)
                Me.ReportPropertyChanging("Personal_SICA")
                Me._Personal_SICA = Global.System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true)
                Me.ReportPropertyChanged("Personal_SICA")
                Me.OnPersonal_SICAChanged
            End Set
        End Property
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Private _Personal_SICA As String
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Partial Private Sub OnPersonal_SICAChanging(ByVal value As String)
        End Sub
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Partial Private Sub OnPersonal_SICAChanged()
        End Sub
        '''<summary>
        '''No hay ningún comentario para la propiedad Personal_CHS en el esquema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(),  _
         Global.System.Runtime.Serialization.DataMemberAttribute(),  _
         Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Public Property Personal_CHS() As String
            Get
                Return Me._Personal_CHS
            End Get
            Set
                Me.OnPersonal_CHSChanging(value)
                Me.ReportPropertyChanging("Personal_CHS")
                Me._Personal_CHS = Global.System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true)
                Me.ReportPropertyChanged("Personal_CHS")
                Me.OnPersonal_CHSChanged
            End Set
        End Property
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Private _Personal_CHS As String
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Partial Private Sub OnPersonal_CHSChanging(ByVal value As String)
        End Sub
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Partial Private Sub OnPersonal_CHSChanged()
        End Sub
        '''<summary>
        '''No hay ningún comentario para la propiedad Observaciones en el esquema.
        '''</summary>
        <Global.System.Data.Objects.DataClasses.EdmScalarPropertyAttribute(),  _
         Global.System.Runtime.Serialization.DataMemberAttribute(),  _
         Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Public Property Observaciones() As String
            Get
                Return Me._Observaciones
            End Get
            Set
                Me.OnObservacionesChanging(value)
                Me.ReportPropertyChanging("Observaciones")
                Me._Observaciones = Global.System.Data.Objects.DataClasses.StructuralObject.SetValidValue(value, true)
                Me.ReportPropertyChanged("Observaciones")
                Me.OnObservacionesChanged
            End Set
        End Property
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Private _Observaciones As String
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Partial Private Sub OnObservacionesChanging(ByVal value As String)
        End Sub
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Partial Private Sub OnObservacionesChanged()
        End Sub
    End Class
    '''<summary>
    '''No hay ningún comentario para DBSICAEntities en el esquema.
    '''</summary>
    Partial Public Class DBSICAEntities
        Inherits Global.System.Data.Objects.ObjectContext
        '''<summary>
        '''Inicializa un nuevo objeto DBSICAEntities usando la cadena de conexión encontrada en la sección 'DBSICAEntities' del archivo de configuración de la aplicación.
        '''</summary>
        Public Sub New()
            MyBase.New("name=DBSICAEntities", "DBSICAEntities")
            Me.OnContextCreated
        End Sub
        '''<summary>
        '''Inicializar un nuevo objeto DBSICAEntities.
        '''</summary>
        Public Sub New(ByVal connectionString As String)
            MyBase.New(connectionString, "DBSICAEntities")
            Me.OnContextCreated
        End Sub
        '''<summary>
        '''Inicializar un nuevo objeto DBSICAEntities.
        '''</summary>
        Public Sub New(ByVal connection As Global.System.Data.EntityClient.EntityConnection)
            MyBase.New(connection, "DBSICAEntities")
            Me.OnContextCreated
        End Sub
        Partial Private Sub OnContextCreated()
        End Sub
        '''<summary>
        '''No hay ningún comentario para SICA_Visitas_a_campo en el esquema.
        '''</summary>
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Public ReadOnly Property SICA_Visitas_a_campo() As Global.System.Data.Objects.ObjectQuery(Of SICA_Visitas_a_campo)
            Get
                If (Me._SICA_Visitas_a_campo Is Nothing) Then
                    Me._SICA_Visitas_a_campo = MyBase.CreateQuery(Of SICA_Visitas_a_campo)("[SICA_Visitas_a_campo]")
                End If
                Return Me._SICA_Visitas_a_campo
            End Get
        End Property
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Private _SICA_Visitas_a_campo As Global.System.Data.Objects.ObjectQuery(Of SICA_Visitas_a_campo)
        '''<summary>
        '''No hay ningún comentario para SICA_Visitas_a_campo en el esquema.
        '''</summary>
        <Global.System.CodeDom.Compiler.GeneratedCode("System.Data.Entity.Design.EntityClassGenerator", "4.0.0.0")>  _
        Public Sub AddToSICA_Visitas_a_campo(ByVal sICA_Visitas_a_campo As SICA_Visitas_a_campo)
            MyBase.AddObject("SICA_Visitas_a_campo", sICA_Visitas_a_campo)
        End Sub
    End Class
End Namespace
