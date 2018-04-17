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
      <Margins type="NineRays.Reporting.DOM.Margins" Top="0" Left="177.165359" Bottom="141.732285" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Location="0; 59.05512" Size="2480.315; 236.2205" StyleName="PageHeaderBack" Name="pageHeader1">
          <Controls>
            <item type="NineRays.Reporting.DOM.TextBox" Location="413.3858; 59.05512" Text="LISTADO CAUDALES ACEQUIAS" Font="Arial Narrow, 18.75pt, style=Bold, Underline" StyleName="Normal" Name="textBox17" Size="1476.378; 118.1102">
              <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.PageFooter" Location="0; 708.6614" Size="2480.315; 118.1102" StyleName="PageFooterBack" Name="pageFooter1">
          <Controls>
            <item type="NineRays.Reporting.DOM.TextBox" Location="177.1654; 59.05512" Name="textBox5" Size="2185.039; 59.05512" />
            <item type="NineRays.Reporting.DOM.TextBox" StringTrimming="None" Location="177.1654; 59.05512" TextAlign="MiddleLeft" Font="Arial Narrow, 9pt, style=Italic" StyleName="PageNumber" Name="textBox14" Size="2539.37; 59.05512">
              <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="CornflowerBlue" />
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="&quot;Page &quot;+PageNumber.ToString()" PropertyName="Value" />
              </DataBindings>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Location="0; 354.3307" DataSource="DataSet.TablaAcequias" Name="dataBand1" Size="2480.315; 295.2756">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Location="0; 59.05512" CanGrow="True" StyleName="HeaderFooter1" Name="header1" Size="2480.315; 59.05512" RepeatEveryPage="True">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Location="118.1102; 0" Text="Código" Font="Arial Narrow, 9.75pt" CanGrow="True" StyleName="Normal" GrowToBottom="True" Name="textBox8" Size="236.2205; 59.05512">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="354.3307; 0" Text="IdElementoMedida" Font="Arial Narrow, 9.75pt" CanGrow="True" StyleName="Normal" GrowToBottom="True" Name="textBox9" Size="354.3307; 59.05512">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="708.6614; 0" Text="Fecha Lectura" Font="Arial Narrow, 9.75pt" CanGrow="True" StyleName="Normal" GrowToBottom="True" Name="textBox10" Size="590.5512; 59.05512">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="1299.213; 0" Text="Caudal" Font="Arial Narrow, 9.75pt" CanGrow="True" StyleName="Normal" GrowToBottom="True" Name="textBox19" Size="236.2205; 59.05512">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="1535.433; 0" Text="Diferencial" Font="Arial Narrow, 9.75pt" CanGrow="True" StyleName="Normal" GrowToBottom="True" Name="textBox20" Size="236.2205; 59.05512">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="1771.654; 0" Text="Diferencial Acumulado" Font="Arial Narrow, 9.75pt" CanGrow="True" StyleName="Normal" GrowToBottom="True" Name="textBox21" Size="413.3858; 59.05512">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" CanShrink="True" CanGrow="True" Location="0; 177.1655" Name="detail2" Size="2480.315; 59.05512">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" CanShrink="True" Location="708.6614; 0" Font="Arial Narrow, 9.75pt" CanGrow="True" GrowToBottom="True" Name="textBox11" Size="590.5512; 59.05512">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;fecha_Medida&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanShrink="True" Location="118.1102; 0" Font="Arial Narrow, 9.75pt" CanGrow="True" GrowToBottom="True" Name="textBox4" Size="236.2205; 59.05512">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;codigoPVYCR&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanShrink="True" Location="354.3307; 0" Font="Arial Narrow, 9.75pt" CanGrow="True" GrowToBottom="True" Name="textBox12" Size="354.3307; 59.05512">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;idElementoMedida&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanShrink="True" Location="1299.213; 0" Font="Arial Narrow, 9.75pt" CanGrow="True" GrowToBottom="True" Name="textBox1" Size="236.2205; 59.05512">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Caudal_M3S&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanShrink="True" Location="1535.433; 0" Font="Arial Narrow, 9.75pt" CanGrow="True" GrowToBottom="True" Name="textBox2" Size="236.2205; 59.05512">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Diferencial&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanShrink="True" Location="1771.654; 0" Font="Arial Narrow, 9.75pt" CanGrow="True" GrowToBottom="True" Name="textBox3" Size="413.3858; 59.05512">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Diferencial_acum&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
  </Pages>
</root>