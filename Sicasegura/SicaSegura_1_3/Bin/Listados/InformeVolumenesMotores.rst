<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<root type="NineRays.Reporting.DOM.Document" Name="simpleList" IsTemplate="True" Title="Simple List" Description="Demonstrates how to create simple list report." ScriptLanguage="CSharp">
  <StyleSheet type="NineRays.Reporting.DOM.StyleSheet" Title="Standard Stylesheet" Description="Normal without Borders">
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
        <TextFill type="NineRays.Basics.Drawing.LinearGradientFill" Angle="45" StartColor="LightSlateGray" EndColor="LightSkyBlue" />
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
    </Styles>
  </StyleSheet>
  <Pages>
    <item type="NineRays.Reporting.DOM.Page" Location="0; 0" StyleName="Normal" TemplateSize="2550; 1.181102E+07" PaperKind="Legal" Name="page1" Size="2550; 4200" CustomSize="0; 1.181102E+07">
      <Margins type="NineRays.Reporting.DOM.Margins" Top="0" Left="177.165359" Bottom="0" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.DataBand" Location="0; 59.05512" CanShrink="True" DataSource="DataSet.tablaMotores" CanGrow="True" Name="dataBand1" Size="2550; 590.5512">
          <Aggregates>
            <item type="NineRays.Reporting.DOM.Aggregate" Expression="ColumnNumber" Name="numcolumn" />
          </Aggregates>
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Location="0; 236.2205" StyleName="Normal" Name="header1" Size="2550; 118.1102">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Location="59.05512; 0" Text="Cod. Fuente Dato" Font="Arial, 8.25pt, style=Bold" CanGrow="True" GrowToBottom="True" Name="textBox2" Size="236.2205; 118.1102">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="1062.992; 0" Text="Funciona" Font="Arial, 8.25pt, style=Bold" CanGrow="True" GrowToBottom="True" Name="textBox3" Size="177.1654; 118.1102">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="1240.157; 0" Text="Justificación" Font="Arial, 8.25pt, style=Bold" CanGrow="True" GrowToBottom="True" Name="textBox4" Size="236.2205; 118.1102">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="1476.378; 0" Text="Inc. Volumétrica" Font="Arial, 8.25pt, style=Bold" CanGrow="True" GrowToBottom="True" Name="textBox5" Size="236.2205; 118.1102">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="1712.598; 0" Text="Consumo Vol.Adi." Font="Arial, 8.25pt, style=Bold" CanGrow="True" GrowToBottom="True" Name="textBox7" Size="177.1654; 118.1102">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="1889.764; 0" Text="Reinicio Lec. Vol." Font="Arial, 8.25pt, style=Bold" CanGrow="True" GrowToBottom="True" Name="textBox8" Size="177.1654; 118.1102">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="2066.929; 0" Text="Observaciones" Font="Arial, 8.25pt, style=Bold" CanGrow="True" GrowToBottom="True" Name="textBox13" Size="413.3858; 118.1102">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="826.7717; 0" Text="Diferencial (m3)" Font="Arial, 8.25pt, style=Bold" CanGrow="True" GrowToBottom="True" Name="textBox14" Size="236.2205; 118.1102">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="295.2756; 0" Text="Fecha Medida" Font="Arial, 8.25pt, style=Bold" CanGrow="True" GrowToBottom="True" Name="textBox21" Size="236.2205; 118.1102">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="531.4961; 0" Text="Lectura Contador (m3)" Font="Arial, 8.25pt, style=Bold" GrowToBottom="True" Name="textBox1" Size="295.2756; 118.1102">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" CanShrink="True" CanBreak="True" CanGrow="True" Location="0; 413.3858" StyleName="Normal" Name="detail2" Size="2550; 118.1102">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Location="59.05512; 0" TextAlign="MiddleRight" Font="Arial, 8.25pt" Name="textBox6" Size="236.2205; 118.1102">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Cod_Fuente_Dato&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="826.7717; 0" TextAlign="MiddleRight" Font="Arial, 8.25pt" Name="textBox10" Size="236.2205; 118.1102">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Diferencial&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="1062.992; 0" TextAlign="MiddleRight" Font="Arial, 8.25pt" Name="textBox16" Size="177.1654; 118.1102">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Funciona&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="1240.157; 0" TextAlign="MiddleRight" Font="Arial, 8.25pt" Name="textBox19" Size="236.2205; 118.1102">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Justificacion&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="1476.378; 0" TextAlign="MiddleLeft" Font="Arial, 8.25pt" Name="textBox15" Size="236.2205; 118.1102">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;DescIncVol&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="1712.598; 0" TextAlign="MiddleLeft" Font="Arial, 8.25pt" Name="textBox17" Size="177.1654; 118.1102">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;ConsumoVolumetricoAdicional&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="1889.764; 0" TextAlign="MiddleLeft" Font="Arial, 8.25pt" Name="textBox18" Size="177.1654; 118.1102">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;ReinicioLecturaVolumetrica&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="2066.929; 0" TextAlign="MiddleLeft" Font="Arial, 8.25pt" Name="textBox22" Size="413.3858; 118.1102">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="295.2756; 0" TextAlign="MiddleLeft" Font="Arial, 8.25pt" Name="txtFecha_Medida0" Size="236.2205; 118.1102">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Fecha_Medida&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="531.4961; 0" TextAlign="MiddleRight" Font="Arial, 8.25pt" Name="textBox9" Size="295.2756; 118.1102">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;LecturaContador_M3&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
              </Controls>
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1.LineNumber % 2 == 0 ? &quot;HeaderFooter3&quot; : &quot;HeaderFooter2&quot;" PropertyName="StyleName" />
              </DataBindings>
            </item>
            <item type="NineRays.Reporting.DOM.Header" Location="0; 59.05512" Name="header2" Size="2550; 118.1102">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Location="708.6614; 0" Text="LISTADO CAUDALES MOTORES" Font="Arial, 10.75pt, style=Bold" Name="textBox11" Size="1003.937; 118.1102" />
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
  </Pages>
</root>