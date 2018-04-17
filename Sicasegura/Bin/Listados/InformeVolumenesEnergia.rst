<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<root type="NineRays.Reporting.DOM.Document" Name="simpleList" Title="Simple List" IsTemplate="True" Description="Demonstrates how to create simple list report." ScriptLanguage="CSharp">
  <Pages>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" TemplateSize="2550; 1.181102E+07" Name="page1" StyleName="Normal" Location="0; 0" CustomSize="0; 1.181102E+07">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="0" Left="177.165359" Top="0" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.DataBand" CanShrink="True" Name="dataBand1" DataSource="DataSet.tablaAlimentacion" Size="2480.315; 708.6614" Location="0; 59.05512" CanGrow="True">
          <Aggregates>
            <item type="NineRays.Reporting.DOM.Aggregate" Expression="ColumnNumber" Name="numcolumn" />
          </Aggregates>
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header1" StyleName="Normal" Location="0; 236.2205">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox2" GrowToBottom="True" Size="236.2205; 118.1102" Location="59.05512; 0" Text="Cod. Fuente Dato">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox3" GrowToBottom="True" Size="118.1102; 118.1102" Location="944.8819; 0" Text="Dif. (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox4" GrowToBottom="True" Size="118.1102; 118.1102" Location="1062.992; 0" Text="Total (Kvar)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox5" GrowToBottom="True" Size="118.1102; 118.1102" Location="1181.102; 0" Text="Fun.">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox7" GrowToBottom="True" Size="236.2205; 118.1102" Location="1299.213; 0" Text="Inc. Eléctrica">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox8" GrowToBottom="True" Size="236.2205; 118.1102" Location="1535.433; 0" Text="Consumo Elec.Adi.">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox12" GrowToBottom="True" Size="236.2205; 118.1102" Location="1771.654; 0" Text="Reinicio Lec. Elec.">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox13" GrowToBottom="True" Size="472.4409; 118.1102" Location="2007.874; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox14" GrowToBottom="True" Size="118.1102; 118.1102" Location="590.5512; 0" Text="Lec. II">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox21" GrowToBottom="True" Size="177.1654; 118.1102" Location="295.2756; 0" Text="Fecha Medida">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox1" GrowToBottom="True" Size="118.1102; 118.1102" Location="472.4409; 0" Text="Lec. I">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox9" GrowToBottom="True" Size="118.1102; 118.1102" Location="708.6614; 0" Text="Lec. III">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox23" GrowToBottom="True" Size="118.1102; 118.1102" Location="826.7717; 0" Text="Total (Kwh)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 118.1102" CanShrink="True" CanBreak="True" Name="detail2" StyleName="Normal" Location="0; 413.3858" CanGrow="True">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1.LineNumber % 2 == 0 ? &quot;HeaderFooter3&quot; : &quot;HeaderFooter2&quot;" PropertyName="StyleName" />
              </DataBindings>
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleRight" Name="textBox6" Size="236.2205; 118.1102" Location="59.05512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Cod_Fuente_Dato&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleRight" Name="textBox10" Size="118.1102; 118.1102" Location="472.4409; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;LecturaI&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleRight" Name="textBox16" Size="118.1102; 118.1102" Location="826.7717; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Total_Kwh&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleRight" Name="textBox19" Size="118.1102; 118.1102" Location="1062.992; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Total_Kvar&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleLeft" Name="textBox15" Size="118.1102; 118.1102" Location="1181.102; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Funciona&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleLeft" Name="textBox17" Size="236.2205; 118.1102" Location="1299.213; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;descIncElec&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleLeft" Name="textBox18" Size="236.2205; 118.1102" Location="1535.433; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;ConsumoElectricoAdicional&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleLeft" Name="textBox20" Size="236.2205; 118.1102" Location="1771.654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;ReinicioLecturaElectrica&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleLeft" Name="textBox22" Size="472.4409; 118.1102" Location="2007.874; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleLeft" Name="txtFecha_Medida0" Size="177.1654; 118.1102" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Fecha_Medida&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleRight" Name="textBox24" Size="118.1102; 118.1102" Location="590.5512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;LecturaII&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleRight" Name="textBox25" Size="118.1102; 118.1102" Location="708.6614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;LecturaIII&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleRight" Name="textBox26" Size="118.1102; 118.1102" Location="944.8819; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Diferencial&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="DarkRed" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header2" Location="0; 59.05512">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10.75pt, style=Bold" Name="textBox11" Size="1003.937; 118.1102" Location="708.6614; 0" Text="LISTADO CAUDALES ENERGÍA" />
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.PageFooter" Size="2480.315; 59.05512" Name="pageFooter1" Location="0; 649.6063">
          <Controls>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7pt" TextAlign="MiddleLeft" Name="textBox29" StyleName="PageNumber" Size="2421.26; 59.05512" Location="59.05512; 0" Text="(*) Los valores que se muestran son valores estimados">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" />
              <TextFormat type="NineRays.Basics.Text.TextFormat" FormatMask="d" UseCultureSettings="True" UseGroupSeparator="True" FormatStyle="Date" />
              <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="DarkRed" />
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