<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<root type="NineRays.Reporting.DOM.Document" Name="Interriego" Title="pruebas" IsTemplate="True">
  <Pages>
    <item type="NineRays.Reporting.DOM.Page" Size="7677.166; 2480.315" PaperKind="Custom" Name="page1" StyleName="Normal" Location="0; 0" CustomSize="7677.166; 2480.315">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="7677.166; 118.1102" Name="pageHeader1" StyleName="PageHeaderBack" Location="0; 118.1102" CanShrink="True">
          <Controls>
            <item type="NineRays.Reporting.DOM.TextBox" Name="textBox6" StyleName="PageHeaderBack" Size="8740.157; 59.05512" Location="59.05512; 0" />
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.Detail" Size="7677.166; 118.1102" CanShrink="True" Name="title" StyleName="ReportTitle" Location="0; 236.2205" CanGrow="True">
          <Controls>
            <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Font="Arial, 18pt, style=Bold" Name="textBox4" Size="8858.268; 59.05512" Location="0; 0">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.titulo&quot;)" PropertyName="Value" />
              </DataBindings>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand1" StyleName="TodoBorde" DataSource="Contadores.TablaContador" Size="7677.166; 767.7166" Location="0; 413.3858">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="7677.166; 177.1654" RepeatEveryPage="True" Name="header1" StyleName="BordesBlancos" Location="0; 177.1654">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="codigocauce" GrowToBottom="True" Size="354.3307; 118.1102" Location="59.05512; 0" Text="Código Cauce">
                  <Border type="NineRays.Basics.Drawing.Border" RightLine="1 Solid White" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" TextAlign="MiddleLeft" Name="denominacion" GrowToBottom="True" Size="885.8268; 118.1102" Location="413.3858; 0" Text="Denominación">
                  <Border type="NineRays.Basics.Drawing.Border" LeftLine="1 Solid White" RightLine="1 Solid White" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="SuperfifieTotal" GrowToBottom="True" Size="354.3307; 118.1102" Location="1299.213; 0" Text="Superficie Total">
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="textBox9" GrowToBottom="True" Size="236.2205; 118.1102" Location="1771.654; 0" Text="Has">
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="textBox11" GrowToBottom="True" Size="413.3858; 118.1102" Location="2007.874; 0" Text="Código Punto">
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="volumenCaptado" GrowToBottom="True" Size="354.3307; 118.1102" Location="2421.26; 0" Text="Volumen Captado m3">
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="volumenLegal" GrowToBottom="True" Size="413.3858; 118.1102" Location="2775.591; 0" Text="Volumen Legal Total">
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="por_consumido" GrowToBottom="True" Size="354.3307; 118.1102" Location="3188.976; 0" Text="% Consumido">
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="dotacionTeorica1" GrowToBottom="True" Size="354.3307; 118.1102" Location="3543.307; 0" Text="Dotación Teórica">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.verCampo1&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="DodgerBlue" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="caudal1" GrowToBottom="True" Size="354.3307; 118.1102" Location="3897.638; 0" Text="Caudal a">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.verCampo1&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="DodgerBlue" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="por_calculado" GrowToBottom="True" Size="236.2205; 118.1102" Location="4251.969; 0" Text="%">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.verCampo1&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="DodgerBlue" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="volumenA1" GrowToBottom="True" Size="354.3307; 118.1102" Location="4488.189; 0" Text="Volumen a">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.verCampo1&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="DodgerBlue" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="dotacionteorica2" GrowToBottom="True" Size="354.3307; 118.1102" Location="4842.52; 0" Text="Dotación Teórica">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.vercampo2&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Blue" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="caudalA2" GrowToBottom="True" Size="354.3307; 118.1102" Location="5196.851; 0" Text="Caudal a">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.vercampo2&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Blue" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="porcentaje2" GrowToBottom="True" Size="236.2205; 118.1102" Location="5551.181; 0" Text="%">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.vercampo2&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Blue" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="volumenA2" GrowToBottom="True" Size="354.3307; 118.1102" Location="5787.402; 0" Text="Volumen a">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.vercampo2&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Blue" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="Qmedio1" GrowToBottom="True" Size="354.3307; 118.1102" Location="6141.732; 0" Text="Q medio derivado">
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="DarkBlue" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="QmedioC1" GrowToBottom="True" Size="354.3307; 118.1102" Location="6496.063; 0" Text="Q medio consumido">
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="DarkBlue" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="porcentajet" GrowToBottom="True" Size="236.2205; 118.1102" Location="6850.394; 0" Text="%">
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="DarkBlue" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="VolumenNetoconsumido" GrowToBottom="True" Size="354.3307; 118.1102" Location="7086.614; 0" Text="Volumen neto consumido">
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="DarkBlue" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="textBox25" GrowToBottom="True" Size="118.1102; 118.1102" Location="1653.543; 0">
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="7677.166; 118.1102" CanShrink="True" Name="detail2" StyleName="TodoBorde" Location="0; 413.3858" CanGrow="True">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="txtCodigoCauce" GrowToBottom="True" Size="354.3307; 118.1102" Location="59.05512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.codigoCauce&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Silver" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" TextAlign="MiddleLeft" Name="txtDenominacioncauce" GrowToBottom="True" Size="885.8268; 118.1102" Location="413.3858; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.DenominacionCauce&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Silver" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="txttipocalculoG1" GrowToBottom="True" Size="118.1102; 118.1102" Location="1653.543; 0" Text="G">
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="txtCodigoPVYR" GrowToBottom="True" Size="413.3858; 118.1102" Location="2007.874; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.CodigoPVYCR&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" TextAlign="MiddleRight" Name="txtSuperficieTotal" GrowToBottom="True" Size="354.3307; 118.1102" Location="1299.213; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.SuperficieTotal&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Silver" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" TextAlign="MiddleRight" Name="txtSuperficieReal" GrowToBottom="True" Size="236.2205; 118.1102" Location="1771.654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.SuperficieRealAproximada_HAS&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="txtDotacionTG1" GrowToBottom="True" Size="354.3307; 118.1102" Location="3543.307; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.DotacionteoricaG&quot;)" PropertyName="Value" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.verCampo1&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="txtCaudal1" GrowToBottom="True" Size="354.3307; 118.1102" Location="3897.638; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.Caudal_M3s&quot;)" PropertyName="Value" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.verCampo1&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="txtPorc_Calculado1" GrowToBottom="True" Size="236.2205; 118.1102" Location="4251.969; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.porcentaje_calculado&quot;)" PropertyName="Value" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.verCampo1&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="txtdotacionTG2" GrowToBottom="True" Size="354.3307; 118.1102" Location="4842.52; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.DotacionteoricaG2&quot;)" PropertyName="Value" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.vercampo2&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="txtCaudal2" GrowToBottom="True" Size="354.3307; 118.1102" Location="5196.851; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.caudal_m3s2&quot;)" PropertyName="Value" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.vercampo2&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="txtVolumena2" GrowToBottom="True" Size="354.3307; 118.1102" Location="5787.402; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.volumen_a2&quot;)" PropertyName="Value" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.vercampo2&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="txtVolumen1" GrowToBottom="True" Size="354.3307; 118.1102" Location="4488.189; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.volumen_a&quot;)" PropertyName="Value" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.verCampo1&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="txtVolCaptado" GrowToBottom="True" Size="354.3307; 118.1102" Location="2421.26; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.volumen_captado&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="txtVolLegal" GrowToBottom="True" Size="413.3858; 118.1102" Location="2775.591; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.volumen_legal&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="txtPorconsumido" GrowToBottom="True" Size="354.3307; 118.1102" Location="3188.976; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.porcentaje_consumido&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="txtPorc_Calculado2" GrowToBottom="True" Size="236.2205; 118.1102" Location="5551.181; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.verCampo2&quot;)" PropertyName="Visible" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.porcentaje_calculado2&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="txtQMedioDerivado" GrowToBottom="True" Size="354.3307; 118.1102" Location="6141.732; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.qmedioderivado&quot;)" PropertyName="Value" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.vercampo2&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="txtQmedioConsumido" GrowToBottom="True" Size="354.3307; 118.1102" Location="6496.063; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.QmedioConsumido&quot;)" PropertyName="Value" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.vercampo2&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="txtPorc_total" GrowToBottom="True" Size="236.2205; 118.1102" Location="6850.394; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.verCampo2&quot;)" PropertyName="Visible" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.porc_total&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="txtVolumena3" GrowToBottom="True" Size="354.3307; 118.1102" Location="7086.614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.volumen_a3&quot;)" PropertyName="Value" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.vercampo2&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Header" Size="7677.166; 118.1102" Name="header2" StyleName="BordesBlancos" Location="0; 0">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Font="Arial, 10pt, style=Bold" Name="txtperiodo1" GrowToBottom="True" Size="1299.213; 118.1102" Location="3543.307; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.periodo1&quot;)" PropertyName="Value" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.verCampo1&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Font="Arial, 10pt, style=Bold" Name="txtperiodo2" GrowToBottom="True" Size="1299.213; 118.1102" Location="4842.52; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.periodo2&quot;)" PropertyName="Value" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.vercampo2&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Font="Arial, 10pt, style=Bold" Name="txtPeriodoTotal" GrowToBottom="True" Size="1299.213; 118.1102" Location="6141.732; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.periodo3&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="7677.166; 118.1102" CanShrink="True" Name="detail1" StyleName="TodoBorde" Location="0; 590.5512" CanGrow="True">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" TextAlign="MiddleRight" Name="txtSuperficieInscrita" GrowToBottom="True" Size="236.2205; 118.1102" Location="1771.654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.SuperficieInscrita_HAS&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="txtDotacionTM1" GrowToBottom="True" Size="354.3307; 118.1102" Location="3543.307; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.DotacionteoricaM&quot;)" PropertyName="Value" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.verCampo1&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="txtdotacionTM2" GrowToBottom="True" Size="354.3307; 118.1102" Location="4842.52; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.DotacionteoricaM2&quot;)" PropertyName="Value" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.vercampo2&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="txttipocalculoM1" GrowToBottom="True" Size="118.1102; 118.1102" Location="1653.543; 0" Text="M">
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="textBox1" GrowToBottom="True" Size="354.3307; 118.1102" Location="59.05512; 0">
                  <Border type="NineRays.Basics.Drawing.Border" LeftLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" TextAlign="MiddleLeft" Name="textBox2" GrowToBottom="True" Size="885.8268; 118.1102" Location="413.3858; 0">
                  <Border type="NineRays.Basics.Drawing.Border" LeftLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" TextAlign="MiddleRight" Name="textBox3" GrowToBottom="True" Size="354.3307; 118.1102" Location="1299.213; 0">
                  <Border type="NineRays.Basics.Drawing.Border" LeftLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" TextAlign="MiddleRight" Name="textBox5" GrowToBottom="True" Size="413.3858; 118.1102" Location="2007.874; 0">
                  <Border type="NineRays.Basics.Drawing.Border" LeftLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="textBox7" GrowToBottom="True" Size="354.3307; 118.1102" Location="59.05512; 0">
                  <Border type="NineRays.Basics.Drawing.Border" LeftLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="textBox10" GrowToBottom="True" Size="354.3307; 118.1102" Location="2421.26; 0">
                  <Border type="NineRays.Basics.Drawing.Border" LeftLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="textBox12" GrowToBottom="True" Size="413.3858; 118.1102" Location="2775.591; 0">
                  <Border type="NineRays.Basics.Drawing.Border" LeftLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="textBox15" GrowToBottom="True" Size="354.3307; 118.1102" Location="3188.976; 0">
                  <Border type="NineRays.Basics.Drawing.Border" LeftLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="textBox17" GrowToBottom="True" Size="236.2205; 118.1102" Location="4251.969; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.verCampo1&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" LeftLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="textBox19" GrowToBottom="True" Size="354.3307; 118.1102" Location="4488.189; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.verCampo1&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" LeftLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="textBox20" GrowToBottom="True" Size="354.3307; 118.1102" Location="5196.851; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.vercampo2&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" LeftLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="textBox24" GrowToBottom="True" Size="354.3307; 118.1102" Location="5787.402; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.vercampo2&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" LeftLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="txtporc2" GrowToBottom="True" Size="236.2205; 118.1102" Location="5551.181; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.verCampo2&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" LeftLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="textBox8" GrowToBottom="True" Size="354.3307; 118.1102" Location="3897.638; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.vercampo2&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" LeftLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="textBox13" GrowToBottom="True" Size="354.3307; 118.1102" Location="6141.732; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.vercampo2&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" LeftLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="textBox14" GrowToBottom="True" Size="354.3307; 118.1102" Location="6496.063; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.vercampo2&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" LeftLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="textBox16" GrowToBottom="True" Size="236.2205; 118.1102" Location="6850.394; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.vercampo2&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" LeftLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="textBox18" GrowToBottom="True" Size="354.3307; 118.1102" Location="7086.614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.vercampo2&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" LeftLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand2" DataSource="Contadores.TablaTotales" Size="7677.166; 1181.102" Location="0; 1240.157">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="7677.166; 590.5512" CanBreak="True" Name="detail3" StyleName="TodoBorde" Location="0; 59.05512" CanGrow="True">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt, style=Bold" TextAlign="MiddleRight" Name="total_superficie" Size="177.1654; 118.1102" Location="1122.047; 0" Text="Total">
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt, style=Bold" TextAlign="MiddleRight" Name="txtSumSuperficie" Size="354.3307; 118.1102" Location="1299.213; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaTotales.SumSuperficie&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt, style=Bold" TextAlign="MiddleRight" Name="txtSumhas" Size="236.2205; 118.1102" Location="1771.654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaTotales.SumHas&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt, style=Bold" TextAlign="MiddleRight" Name="totalVol1" Size="236.2205; 118.1102" Location="4251.969; -3.051758E-05" Text="m3">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.verCampo1&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt, style=Bold" Name="txtSumVol1" Size="354.3307; 118.1102" Location="4488.189; -3.051758E-05">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaTotales.CaudalTotal_m31&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt, style=Bold" Name="txtsumvolPorDia1" Size="354.3307; 118.1102" Location="4488.189; 118.1102">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaTotales.CaudalporDias1&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt, style=Bold" TextAlign="MiddleRight" Name="totalVolCaptadoSeg" Size="236.2205; 118.1102" Location="4251.969; 236.2205" Text="m3/seg">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.verCampo1&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt, style=Bold" TextAlign="MiddleRight" Name="totalVolCaptadoDia" Size="236.2205; 118.1102" Location="4251.969; 118.1102" Text="m3/dia">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.verCampo1&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt, style=Bold" Name="txtVolPorSeg1" Size="354.3307; 118.1102" Location="4488.189; 236.2205">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaTotales.CaudalPorSeg1&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt, style=Bold" Name="txtVolPorSeg2" Size="354.3307; 118.1102" Location="5787.402; 236.2205">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaTotales.CaudalPorSeg2&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt, style=Bold" Name="txtsumvolPorDia2" Size="354.3307; 118.1102" Location="5787.402; 118.1102">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaTotales.CaudalporDias2&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt, style=Bold" Name="txtSumVol2" Size="354.3307; 118.1102" Location="5787.402; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaTotales.CaudalTotal_m32&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt, style=Bold" TextAlign="MiddleRight" Name="totalVol2" Size="236.2205; 118.1102" Location="5551.181; 0" Text="m3">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.vercampo2&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt, style=Bold" TextAlign="MiddleRight" Name="totalVolCaptadoDia2" Size="236.2205; 118.1102" Location="5551.181; 118.1102" Text="m3/dia">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.vercampo2&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt, style=Bold" TextAlign="MiddleRight" Name="totalVolCaptadoSeg2" Size="236.2205; 118.1102" Location="5551.181; 236.2205" Text="m3/seg">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.vercampo2&quot;)" PropertyName="Visible" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt, style=Bold" Name="txtSumVol3" Size="354.3307; 118.1102" Location="7086.614; 7.629395E-05">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaTotales.CaudalTotal_m33&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt, style=Bold" Name="txtsumvolPorDia3" Size="354.3307; 118.1102" Location="7086.614; 118.1102">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaTotales.CaudalporDias3&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt, style=Bold" Name="txtVolPorSeg3" Size="354.3307; 118.1102" Location="7086.614; 236.2205">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaTotales.CaudalPorSeg3&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt, style=Bold" TextAlign="MiddleRight" Name="totalVol3" Size="236.2205; 118.1102" Location="6850.394; 7.629395E-05" Text="m3">
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt, style=Bold" TextAlign="MiddleRight" Name="totalVolCaptadoDia3" Size="236.2205; 118.1102" Location="6850.394; 118.1102" Text="m3/dia">
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt, style=Bold" TextAlign="MiddleRight" Name="totalVolCaptadoSeg3" Size="236.2205; 118.1102" Location="6850.394; 236.2206" Text="m3/seg">
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt, style=Bold" Name="txttotalQmedioconsumido" Size="354.3307; 118.1102" Location="6496.063; 236.2205">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaTotales.totalQMedioConsumido&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt, style=Bold" Name="txtSumaQmedioConsumido" Size="354.3307; 118.1102" Location="6496.063; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaTotales.sumaQMedioConsumido&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
  </Pages>
  <GraphicsSettings type="NineRays.Reporting.DOM.GraphicsSettings" InterpolationMode="Low" CompositingQuality="HighSpeed" />
  <StyleSheet type="NineRays.Reporting.DOM.StyleSheet" Title="BordesBlancos" Description="Normal without Borders">
    <Styles>
      <item type="NineRays.Reporting.DOM.Style" Name="Normal" Font="Arial, 9.75pt">
        <Border type="NineRays.Basics.Drawing.Border" />
        <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
      </item>
      <item type="NineRays.Reporting.DOM.Style" Name="Hightlight" Font="Arial, 9.75pt">
        <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="SteelBlue" />
        <Fill type="NineRays.Basics.Drawing.SolidFill" Color="GhostWhite" />
      </item>
      <item type="NineRays.Reporting.DOM.Style" Name="HeaderFooter1" Font="Arial, 12pt, style=Bold">
        <TextFill type="NineRays.Basics.Drawing.SolidFill" />
        <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
      </item>
      <item type="NineRays.Reporting.DOM.Style" Name="HeaderFooter2" Font="Arial, 11.25pt, style=Bold">
        <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
      </item>
      <item type="NineRays.Reporting.DOM.Style" Name="HeaderFooter3" Font="Arial, 9.75pt, style=Italic">
        <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
        <Fill type="NineRays.Basics.Drawing.SolidFill" Color="AliceBlue" />
      </item>
      <item type="NineRays.Reporting.DOM.Style" Name="ReportTitle" Font="Arial, 48pt, style=Bold">
        <TextFill type="NineRays.Basics.Drawing.LinearGradientFill" EndColor="LightSkyBlue" Angle="45" StartColor="LightSlateGray" />
      </item>
      <item type="NineRays.Reporting.DOM.Style" Name="HeaderTitle" Font="Arial, 12pt, style=Bold" />
      <item type="NineRays.Reporting.DOM.Style" Name="PageHeaderBack" Font="Arial, 12pt">
        <Border type="NineRays.Basics.Drawing.Border" BottomLine="1 Solid 0, 128, 192" />
      </item>
      <item type="NineRays.Reporting.DOM.Style" Name="PageNumber" Font="Arial, 9pt, style=Italic">
        <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="CornflowerBlue" />
      </item>
      <item type="NineRays.Reporting.DOM.Style" Name="PageFooterBack" Font="Arial, 12pt">
        <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid 0, 128, 192" />
      </item>
      <item type="NineRays.Reporting.DOM.Style" Name="LeftSide" Font="Arial, 12pt" />
      <item type="NineRays.Reporting.DOM.Style" Name="TodoBorde" Font="Arial, 10pt">
        <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
      </item>
      <item type="NineRays.Reporting.DOM.Style" Name="BordeGris" Font="Arial, 10pt">
        <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Silver" />
      </item>
      <item type="NineRays.Reporting.DOM.Style" Name="TresBordes" Font="Arial, 10pt">
        <Border type="NineRays.Basics.Drawing.Border" LeftLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
      </item>
      <item type="NineRays.Reporting.DOM.Style" Name="BordesBlancos" Font="Arial, 10pt">
        <Border type="NineRays.Basics.Drawing.Border" LeftLine="1 Solid White" RightLine="1 Solid White" />
        <TextFill type="NineRays.Basics.Drawing.SolidFill" />
      </item>
    </Styles>
  </StyleSheet>
</root>