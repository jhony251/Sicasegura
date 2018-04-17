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
    <item type="NineRays.Reporting.DOM.Page" Location="0; 0" StyleName="Normal" Name="page1" Size="2480.315; 3507.874">
      <Margins type="NineRays.Reporting.DOM.Margins" Top="118.110237" Left="177.165359" Bottom="141.732285" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Location="0; 118.1102" Size="2480.315; 118.1102" StyleName="PageHeaderBack" Name="pageHeader1">
          <Controls>
            <item type="NineRays.Reporting.DOM.TextBox" Location="177.1654; 0" StyleName="PageHeaderBack" Name="textBox6" Size="2185.039; 59.05512" />
            <item type="NineRays.Reporting.DOM.TextBox" Location="1889.764; 0" StyleName="PageNumber" Name="textBox7" Size="472.4409; 59.05512">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="&quot;Página &quot;+PageNumber.ToString()" PropertyName="Value" />
              </DataBindings>
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Location="177.1654; 0" TextAlign="MiddleLeft" StyleName="PageNumber" Name="textBox3" Size="1712.598; 59.05512">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="Document.Title" PropertyName="Value" />
              </DataBindings>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.PageFooter" Location="0; 3248.031" Size="2480.315; 118.1102" StyleName="PageFooterBack" Name="pageFooter1">
          <Controls>
            <item type="NineRays.Reporting.DOM.TextBox" Location="177.1654; 59.05512" Name="textBox5" Size="2185.039; 59.05512" />
            <item type="NineRays.Reporting.DOM.TextBox" Location="177.1654; 59.05512" TextAlign="MiddleLeft" StyleName="PageNumber" Name="textBox14" Size="1712.598; 59.05512">
              <TextFormat type="NineRays.Basics.Text.TextFormat" UseCultureSettings="True" FormatStyle="Date" UseGroupSeparator="True" FormatMask="d" />
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="Now" PropertyName="Value" />
              </DataBindings>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.Detail" CanShrink="True" CanGrow="True" Location="0; 236.2205" StyleName="ReportTitle" Name="title" Size="2480.315; 118.1102">
          <Controls>
            <item type="NineRays.Reporting.DOM.TextBox" CanShrink="True" Location="177.1654; 0" Text="Puntos Motores con diferencial negativo" Font="Arial, 18pt, style=Bold" CanGrow="True" Name="textBox4" Size="2185.039; 59.05512" />
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Location="0; 413.3858" DataSource="Contadores.TablaContador" Name="dataBand1" Size="2480.315; 354.3307">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Location="0; 59.05512" CanGrow="True" StyleName="HeaderFooter1" Name="header1" Size="2480.315; 59.05512" RepeatEveryPage="True">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Location="177.1654; 0" Text="Código PVYCR" TextAlign="MiddleLeft" CanGrow="True" GrowToBottom="True" Name="textBox2" Size="708.6614; 59.05512" />
                <item type="NineRays.Reporting.DOM.TextBox" Location="1712.598; 0" Text="Diferencial" CanGrow="True" GrowToBottom="True" Name="textBox10" Size="649.6063; 59.05512" />
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" CanShrink="True" CanGrow="True" Location="0; 177.1654" Name="detail2" Size="2480.315; 118.1102">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" CanShrink="True" Location="177.1654; 0" TextAlign="TopLeft" CanGrow="True" GrowToBottom="True" Name="txtcodigoPVYCR" Size="708.6614; 118.1102">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.codigoPVYCR&quot;)" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanShrink="True" Location="1771.654; 0" TextAlign="TopLeft" CanGrow="True" GrowToBottom="True" Name="textBox13" Size="590.5512; 118.1102">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.diferencial&quot;)" PropertyName="Value" />
                  </DataBindings>
                </item>
              </Controls>
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1.LineNumber%2==0?&quot;Normal&quot;:&quot;Hightlight&quot;" PropertyName="StyleName" />
              </DataBindings>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
  </Pages>
</root>