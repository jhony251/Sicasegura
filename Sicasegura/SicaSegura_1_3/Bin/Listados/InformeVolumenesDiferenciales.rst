<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<root type="NineRays.Reporting.DOM.Document" Name="simpleList" Title="Simple List" IsTemplate="True" Description="Demonstrates how to create simple list report." ScriptLanguage="CSharp">
  <Pages>
    <item type="NineRays.Reporting.DOM.Page" Size="2550; 4200" PaperKind="Legal" TemplateSize="2550; 1.181102E+07" Name="page1" StyleName="Normal" Location="0; 0" CustomSize="0; 1.181102E+07">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="0" Left="177.165359" Top="0" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.DataBand" CanShrink="True" Name="dataBand1" DataSource="DataSet.tablaDiferencial" Size="2550; 590.5512" Location="0; 59.05512" CanGrow="True">
          <Aggregates>
            <item type="NineRays.Reporting.DOM.Aggregate" Expression="ColumnNumber" Name="numcolumn" />
          </Aggregates>
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2550; 118.1102" Name="header1" StyleName="Normal" Location="0; 236.2205">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox2" GrowToBottom="True" Size="236.2205; 118.1102" Location="118.1102; 0" Text="Cod. Fuente Dato">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox13" GrowToBottom="True" Size="826.7717; 118.1102" Location="1476.378; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox14" GrowToBottom="True" Size="354.3307; 118.1102" Location="1122.047; 0" Text="Diferencial (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox21" GrowToBottom="True" Size="354.3307; 118.1102" Location="354.3307; 0" Text="Fecha Medida">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1" GrowToBottom="True" Size="413.3858; 118.1102" Location="708.6614; 0" Text="Suministro Mensual (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2550; 118.1102" CanShrink="True" CanBreak="True" Name="detail2" StyleName="Normal" Location="0; 413.3858" CanGrow="True">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1.LineNumber % 2 == 0 ? &quot;HeaderFooter3&quot; : &quot;HeaderFooter2&quot;" PropertyName="StyleName" />
              </DataBindings>
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleRight" Name="textBox6" Size="236.2205; 118.1102" Location="118.1102; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Cod_Fuente_Dato&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleRight" Name="textBox10" Size="354.3307; 118.1102" Location="1122.047; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Diferencial&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleLeft" Name="textBox22" Size="826.7717; 118.1102" Location="1476.378; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleLeft" Name="txtFecha_Medida0" Size="354.3307; 118.1102" Location="354.3307; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Fecha_Medida&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleRight" Name="textBox9" Size="413.3858; 118.1102" Location="708.6614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;SuministroMensualM3&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <TextFormat type="NineRays.Basics.Text.TextFormat" UseCultureSettings="False" UseGroupSeparator="True" GroupSeparator=" ." DecimalPlaces="0" FormatStyle="Number" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Header" Size="2550; 118.1102" Name="header2" Location="0; 59.05512">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10.75pt, style=Bold" Name="textBox11" Size="1003.937; 118.1102" Location="708.6614; 0" Text="LISTADO CAUDALES DIFERENCIALES" />
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
  </Pages>
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
    </Styles>
  </StyleSheet>
</root>