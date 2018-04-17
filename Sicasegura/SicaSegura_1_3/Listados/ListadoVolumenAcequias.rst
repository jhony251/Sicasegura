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
    <item type="NineRays.Reporting.DOM.Page" Location="0; 0" StyleName="Normal" PaperKind="Legal" Orientation="Landscape" Name="page1" Size="4200; 2550">
      <Margins type="NineRays.Reporting.DOM.Margins" Top="118.110237" Left="177.165359" Bottom="141.732285" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Location="0; 59.05512" Size="4200; 118.1102" StyleName="PageHeaderBack" Name="pageHeader1">
          <Controls>
            <item type="NineRays.Reporting.DOM.TextBox" Location="177.1654; 0" StyleName="PageHeaderBack" Name="textBox6" Size="3897.638; 59.05512" />
            <item type="NineRays.Reporting.DOM.TextBox" Location="3484.252; 0" StyleName="PageNumber" Name="textBox7" Size="590.5512; 59.05512">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="&quot;Page &quot;+PageNumber.ToString()" PropertyName="Value" />
              </DataBindings>
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Location="118.1102; 0" Text="text" TextAlign="MiddleLeft" Name="txtcodigoPVYCR" Size="531.4961; 59.05512">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;dataSource.CodigoPVYCR0&quot;)" PropertyName="Value" />
              </DataBindings>
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Location="649.6063; 0" Text="-" Name="textBox3" Size="59.05512; 59.05512" />
            <item type="NineRays.Reporting.DOM.TextBox" Location="767.7166; 0" Text="text" Name="txtidElementomedida" Size="236.2205; 59.05512">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;dataSource.idelementoMedida0&quot;)" PropertyName="Value" />
              </DataBindings>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.PageFooter" Location="0; 944.8819" Size="4200; 118.1102" StyleName="PageFooterBack" Name="pageFooter1">
          <Controls>
            <item type="NineRays.Reporting.DOM.TextBox" Location="177.1654; 59.05512" Name="textBox5" Size="3897.638; 59.05512" />
            <item type="NineRays.Reporting.DOM.TextBox" Location="177.1654; 590.5512" TextAlign="MiddleLeft" StyleName="PageNumber" Name="textBox14" Size="3188.976; 59.05512">
              <TextFormat type="NineRays.Basics.Text.TextFormat" UseCultureSettings="True" FormatStyle="Date" UseGroupSeparator="True" FormatMask="d" />
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="Now" PropertyName="Value" />
              </DataBindings>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.Detail" CanShrink="True" CanGrow="True" Location="0; 236.2205" StyleName="ReportTitle" Name="title" Size="4200; 118.1102">
          <Controls>
            <item type="NineRays.Reporting.DOM.TextBox" CanShrink="True" Location="177.1654; 0" Text="Comparativa Caudales Acequias" Font="Arial, 25pt, style=Bold" CanGrow="True" Name="textBox4" Size="3188.976; 118.1102" />
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Location="0; 413.3858" DataSource="dataSource" Name="dataBand1" Size="4200; 472.4409">
          <Aggregates>
            <item type="NineRays.Reporting.DOM.Aggregate" Expression="ColumnNumber" Name="numcolumn" />
          </Aggregates>
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Location="0; 236.2205" CanGrow="True" StyleName="HeaderFooter1" Name="header1" Size="4200; 59.05512" RepeatEveryPage="True">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Location="177.1654; 0" Text="Fecha" TextAlign="MiddleLeft" Font="Arial, 9pt, style=Bold" CanGrow="True" GrowToBottom="True" Name="lblFechaMedida1" Size="354.3307; 59.05512">
                  <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="767.7166; 0" Text="Diferencial" Font="Arial, 9pt, style=Bold" CanGrow="True" GrowToBottom="True" Name="lbldiferencial1" Size="295.2756; 59.05512">
                  <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" BottomLine="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="531.4961; 0" Text="Caudal" Font="Arial, 9pt, style=Bold" CanGrow="True" GrowToBottom="True" Name="lblCaudal1" Size="236.2205; 59.05512">
                  <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" BottomLine="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="1062.992; 0" Text="Diferencial Acum." Font="Arial, 9pt, style=Bold" CanGrow="True" GrowToBottom="True" Name="lblDiferencialAcum1" Size="413.3858; 59.05512">
                  <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="1476.378; 0" Text="Fecha" TextAlign="MiddleLeft" Font="Arial, 9pt, style=Bold" CanGrow="True" GrowToBottom="True" Name="lblfechamedida2" Size="354.3307; 59.05512">
                  <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="1830.709; 0" Text="Caudal" Font="Arial, 9pt, style=Bold" CanGrow="True" GrowToBottom="True" Name="lblcaudal2" Size="236.2205; 59.05512">
                  <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" BottomLine="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="2066.929; 0" Text="Diferencial" Font="Arial, 9pt, style=Bold" CanGrow="True" GrowToBottom="True" Name="lbldiferencial2" Size="295.2756; 59.05512">
                  <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" BottomLine="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="2362.205; 0" Text="Diferencial Acum." Font="Arial, 9pt, style=Bold" CanGrow="True" GrowToBottom="True" Name="lblDiferencialacum2" Size="413.3858; 59.05512">
                  <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="2775.591; 0" Text="Fecha" TextAlign="MiddleLeft" Font="Arial, 9pt, style=Bold" CanGrow="True" GrowToBottom="True" Name="lblfechamedida3" Size="354.3307; 59.05512">
                  <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="3129.921; 0" Text="Caudal" Font="Arial, 9pt, style=Bold" CanGrow="True" GrowToBottom="True" Name="lblcaudal3" Size="236.2205; 59.05512">
                  <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" BottomLine="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="3366.142; 0" Text="Diferencial" Font="Arial, 9pt, style=Bold" CanGrow="True" GrowToBottom="True" Name="lbldiferencial3" Size="295.2756; 59.05512">
                  <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" BottomLine="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="3661.417; 0" Text="Diferencial Acum." Font="Arial, 9pt, style=Bold" CanGrow="True" GrowToBottom="True" Name="lbldiferencialacum3" Size="413.3858; 59.05512">
                  <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" CanShrink="True" CanGrow="True" Location="0; 354.3307" Name="detail2" Size="4200; 59.05512">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Location="177.1654; 0" Font="Arial, 8pt" Name="txtFecha_Medida0" Size="354.3307; 59.05512">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;dataSource.Fecha_Medida0&quot;)" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="531.4961; 0" Font="Arial, 8pt" Name="txtCaudal0" Size="236.2205; 59.05512">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;dataSource.Caudal_m3s0&quot;)" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="1062.992; 0" Font="Arial, 8pt" Name="txtDiferencial_acum0" Size="413.3858; 59.05512">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;dataSource.Diferencial_acum0&quot;)" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="767.7166; 0" Font="Arial, 8pt" Name="txtDiferencial0" Size="295.2756; 59.05512">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;dataSource.Diferencial0&quot;)" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="1476.378; 0" Font="Arial, 8pt" Name="txtFecha_medida1" Size="354.3307; 59.05512">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;dataSource.Fecha_Medida1&quot;)" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="1830.709; 0" Font="Arial, 8pt" Name="txtcaudal1" Size="236.2205; 59.05512">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;dataSource.Caudal_m3s1&quot;)" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="2066.929; 0" Font="Arial, 8pt" Name="txtDiferencial1" Size="295.2756; 59.05512">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;dataSource.Diferencial_acum1&quot;)" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="2362.205; 0" Font="Arial, 8pt" Name="txtDiferencial_acum1" Size="413.3858; 59.05512">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;dataSource.Diferencial_acum1&quot;)" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="2775.591; 0" Font="Arial, 8pt" Name="textBox2" Size="354.3307; 59.05512">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;dataSource.Fecha_Medida2&quot;)" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="3129.921; 0" Font="Arial, 8pt" Name="textBox10" Size="236.2205; 59.05512">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;dataSource.Caudal_m3s2&quot;)" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="3366.142; 0" Font="Arial, 8pt" Name="textBox11" Size="295.2756; 59.05512">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;dataSource.Diferencial2&quot;)" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="3661.417; 0" Font="Arial, 8pt" Name="textBox12" Size="413.3858; 59.05512">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;dataSource.Diferencial_acum2&quot;)" PropertyName="Value" />
                  </DataBindings>
                </item>
              </Controls>
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1" PropertyName="StyleName" />
              </DataBindings>
            </item>
            <item type="NineRays.Reporting.DOM.Header" Location="0; 59.05512" Name="header2" Size="4200; 118.1102">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Location="177.1654; 0" Text="2005-2006" Font="Arial, 12pt" Name="txtPeriodo1" Size="1299.213; 118.1102">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="IndianRed" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="1476.378; 0" Text="2006-2007" Font="Arial, 12pt" Name="textBox8" Size="1299.213; 118.1102">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="IndianRed" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Location="2775.591; 0" Text="2007-2008" Font="Arial, 12pt" Name="textBox9" Size="1299.213; 118.1102">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="IndianRed" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
  </Pages>
</root>