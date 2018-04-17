<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<root type="NineRays.Reporting.DOM.Document" Name="simpleList" Title="Simple List" IsTemplate="True" Description="Demonstrates how to create simple list report." ScriptLanguage="CSharp">
  <Pages>
    <item type="NineRays.Reporting.DOM.Page" Size="2550; 4200" PaperKind="Legal" Name="page1" StyleName="Normal" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageFooter" Size="2550; 118.1102" Name="pageFooter1" StyleName="PageFooterBack" Location="0; 1122.047">
          <Controls>
            <item type="NineRays.Reporting.DOM.TextBox" Name="textBox5" Size="2244.094; 59.05512" Location="177.1654; 59.05512" />
            <item type="NineRays.Reporting.DOM.TextBox" TextAlign="MiddleLeft" Name="textBox14" StyleName="PageNumber" Size="1181.102; 59.05512" Location="59.05512; 59.05512">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="Now" PropertyName="Value" />
              </DataBindings>
              <TextFormat type="NineRays.Basics.Text.TextFormat" FormatMask="d" UseCultureSettings="True" UseGroupSeparator="True" FormatStyle="Date" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Name="textBox7" StyleName="PageNumber" Size="590.5512; 59.05512" Location="3484.252; 59.05512">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="&quot;Page &quot;+PageNumber.ToString()" PropertyName="Value" />
              </DataBindings>
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" TextAlign="MiddleRight" Name="textBox1" StyleName="PageNumber" Size="472.4409; 59.05512" Location="1948.819; 59.05512">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="&quot;Page &quot;+PageNumber.ToString()" PropertyName="Value" />
              </DataBindings>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand1" DataSource="DataSet.Listados" Size="2550; 767.7166" Location="0; 59.05512">
          <Aggregates>
            <item type="NineRays.Reporting.DOM.Aggregate" Expression="ColumnNumber" Name="numcolumn" />
          </Aggregates>
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2550; 354.3307" Name="header2" Location="0; 59.05512">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.75pt" Name="txtPeriodo1" Size="1062.992; 59.05512" Location="1417.323; 236.2205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="IndianRed" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Font="Arial, 25pt, style=Bold" Name="textBox4" Size="2244.094; 118.1102" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="&quot;SISTEMA &quot; + GetData(&quot;DataSet.Listados.Sistema&quot;) " PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.75pt" Name="textBox2" Size="1358.268; 59.05512" Location="59.05512; 236.2205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="IndianRed" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Header" Size="2550; 59.05512" RepeatEveryPage="True" Name="header1" StyleName="HeaderFooter1" Location="0; 472.4409" CanGrow="True">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 9pt, style=Bold" Name="lblFechaMedida1" GrowToBottom="True" Size="649.6063; 118.1102" Location="59.05512; 0" Text="Punto de control">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 9pt, style=Bold" Name="lbldiferencial1" GrowToBottom="True" Size="413.3858; 118.1102" Location="1535.433; 0" Text="Volumen Diferencial">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 9pt, style=Bold" Name="lblCaudal1" GrowToBottom="True" Size="177.1654; 118.1102" Location="1299.213; 0" Text="Factor">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 9pt, style=Bold" Name="textBox3" GrowToBottom="True" Size="236.2205; 118.1102" Location="1003.937; 0" Text="Elemento de Medida">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 9pt, style=Bold" Name="textBox8" GrowToBottom="True" Size="177.1654; 118.1102" Location="767.7166; 0" Text="Tipo">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 9pt, style=Bold" Name="textBox11" GrowToBottom="True" Size="413.3858; 118.1102" Location="2066.929; 0" Text="Acumulado">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2550; 59.05512" CanShrink="True" Name="detail2" Location="0; 649.6063" CanGrow="True">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1" PropertyName="StyleName" />
              </DataBindings>
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleLeft" Name="txtFecha_Medida0" Size="649.6063; 59.05512" Location="59.05512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;CodigoPVYCR&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleRight" Name="textBox13" Size="236.2205; 59.05512" Location="1003.937; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;IdElementoMedida&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleRight" Name="textBox16" Size="413.3858; 59.05512" Location="1535.433; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;VolumenDif&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleRight" Name="textBox6" Size="177.1654; 59.05512" Location="767.7166; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Tipo&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleRight" Name="textBox9" Size="177.1654; 59.05512" Location="1299.213; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Factor&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleRight" Name="textBox10" Size="413.3858; 59.05512" Location="2066.929; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;VxF&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand2" DataSource="DataSet.Subtotales" Size="2550; 177.1654" Location="0; 885.8268">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2550; 59.05512" Name="detail1" Location="0; 59.05512">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" TextAlign="MiddleRight" Name="textBox12" Size="472.4409; 59.05512" Location="2007.874; 5.268595">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;SubtotalStr&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" TextAlign="MiddleRight" Name="textBox15" Size="708.6614; 59.05512" Location="1240.157; 7.902892" Text="Total Acumulado" />
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