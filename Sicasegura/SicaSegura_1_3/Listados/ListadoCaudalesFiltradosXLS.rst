<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<root type="NineRays.Reporting.DOM.Document" Name="simpleList" Title="Simple List" IsTemplate="True" DoublePass="True" ImportsString="System.Drawing" Description="Demonstrates how to create simple list report." ScriptLanguage="CSharp">
  <Pages>
    <item type="NineRays.Reporting.DOM.Page" Size="2550; 4200" PaperKind="Legal" Name="page1" StyleName="Normal" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="0" Left="177.165359" Top="0" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand1" DataSource="DataSet.listado" Size="2550; 649.6063" Location="0; 708.6614">
          <Aggregates>
            <item type="NineRays.Reporting.DOM.Aggregate" Expression="ColumnNumber" Name="numcolumn" />
          </Aggregates>
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2550; 118.1102" RepeatEveryPage="True" Name="header1" StyleName="Normal" GenerateScript="if (PageNumber==1){&#xD;&#xA;   header1.Visible=false;  &#xD;&#xA;}else{&#xD;&#xA;   header1.Visible=true;&#xD;&#xA;}&#xD;&#xA;" Location="0; 354.3307" CanGrow="True">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox21" GrowToBottom="True" Size="354.3307; 118.1102" Location="59.05512; 0" Text="Intervalo">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox22" GrowToBottom="True" Size="413.3858; 118.1102" Location="413.3858; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Var1Titulo&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox23" GrowToBottom="True" Size="354.3307; 118.1102" Location="826.7717; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Var2Titulo&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox24" GrowToBottom="True" Size="413.3858; 118.1102" Location="1181.102; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Var3Titulo&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox25" GrowToBottom="True" Size="413.3858; 118.1102" Location="1594.488; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Var4Titulo&quot;]" PropertyName="Value" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Var4Visible&quot;]" PropertyName="Visible" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox26" GrowToBottom="True" Size="472.4409; 118.1102" Location="2007.874; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Var5Titulo&quot;]" PropertyName="Value" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Var5Visible&quot;]" PropertyName="Visible" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2550; 59.05512" CanShrink="True" Name="detail2" StyleName="Normal" Location="0; 531.4961" CanGrow="True">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1.LineNumber % 2 == 0 ? &quot;HeaderFooter3&quot; : &quot;HeaderFooter2&quot;" PropertyName="StyleName" />
              </DataBindings>
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleLeft" Name="txtFecha_Medida0" Size="354.3307; 59.05512" Location="59.05512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Fecha_MedidaF&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleRight" Name="textBox9" Size="413.3858; 59.05512" Location="413.3858; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Var1Valor&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleRight" Name="textBox6" Size="354.3307; 59.05512" Location="826.7717; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Var2Valor&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleRight" Name="textBox10" Size="413.3858; 59.05512" Location="1181.102; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Var3Valor&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleRight" Name="textBox16" Size="413.3858; 59.05512" Location="1594.488; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Var4Valor&quot;]" PropertyName="Value" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="(String)dataBand1[&quot;DescTipoElem&quot;] == &quot;DATOS CONTADOR ENERGÍA&quot; ? new SolidFill(Color.DarkRed) : null" PropertyName="TextFill" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" TextAlign="MiddleRight" Name="textBox19" Size="472.4409; 59.05512" Location="2007.874; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Var5Valor&quot;]" PropertyName="Value" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="(String)dataBand1[&quot;DescTipoElem&quot;] == &quot;DATOS CONTADOR ENERGÍA&quot; ? new SolidFill(Color.DarkRed) : null" PropertyName="TextFill" />
                  </DataBindings>
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.GroupHeader" CanShrink="True" Size="2550; 236.2205" Name="groupHeader1" CanBreak="True" Location="0; 59.05512" CanGrow="True">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Punto&quot;]" PropertyName="Group" />
              </DataBindings>
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" TextAlign="MiddleLeft" Name="textBox2" GrowToBottom="True" Size="2421.26; 59.05512" Location="59.05512; 177.1654">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Rama&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="lblFechaMedida1" GrowToBottom="True" Size="354.3307; 118.1102" Location="59.05512; 59.05512" Text="Intervalo">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox4" GrowToBottom="True" Size="413.3858; 118.1102" Location="413.3858; 59.05512">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Var1Titulo&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox3" GrowToBottom="True" Size="354.3307; 118.1102" Location="826.7717; 59.05512">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Var2Titulo&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox8" GrowToBottom="True" Size="413.3858; 118.1102" Location="1181.102; 59.05512">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Var3Titulo&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox18" GrowToBottom="True" Size="413.3858; 118.1102" Location="1594.488; 59.05512">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Var4Titulo&quot;]" PropertyName="Value" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Var4Visible&quot;]" PropertyName="Visible" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8.25pt, style=Bold" Name="textBox20" GrowToBottom="True" Size="472.4409; 118.1102" Location="2007.874; 59.05512">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Var5Titulo&quot;]" PropertyName="Value" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Var5Visible&quot;]" PropertyName="Visible" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand2" DataSource="DataSet.listadoGlobales" Size="2550; 590.5512" Location="0; 59.05512">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2550; 472.4409" CanShrink="True" CanBreak="True" Name="detail1" Location="0; 59.05512" CanGrow="True">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Font="Arial, 18pt, style=Bold" TextAlign="MiddleLeft" Name="textBox11" Size="1240.157; 118.1102" Location="59.05512; 0" Text="Comparativa Caudales Acequias" />
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Font="Arial, 16pt, style=Bold" TextAlign="TopLeft" Name="textBox13" GrowToBottom="True" Size="1181.102; 236.2205" Location="59.05512; 118.1102">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;Nodo&quot;]" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.75pt, style=Bold" TextAlign="TopLeft" Name="textBox14" Size="1240.157; 59.05512" Location="1240.156; 0" Text="Filtro:">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8pt" TextAlign="TopLeft" Name="textBox15" Size="649.6063; 295.2756" Location="1240.157; 59.05512" StringTrimming="None">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;Filtro1&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanShrink="True" Font="Arial, 8pt" TextAlign="TopLeft" Name="textBox17" Size="590.5512; 295.2756" Location="1889.764; 59.05512">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;Filtro2&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.75pt, style=Bold" TextAlign="TopLeft" Name="textBox27" StyleName="Normal" Size="708.6614; 59.05512" Location="59.05512; 236.2205">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;DescTipoElem&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="LightSlateGray" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.PageFooter" Size="2550; 59.05512" Name="pageFooter1" Location="0; 1417.323">
          <DataBindings>
            <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="(String)dataBand1[&quot;DescTipoElem&quot;] == &quot;DATOS CONTADOR ENERGÍA&quot; ? true : false" PropertyName="Visible" />
          </DataBindings>
          <Controls>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7pt" TextAlign="MiddleLeft" Name="textBox29" StyleName="PageNumber" Size="2421.26; 59.05512" Location="59.05512; 0" Text="(*) Los valores que se muestran son valores estimados">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="(String)dataBand1[&quot;DescTipoElem&quot;] == &quot;DATOS CONTADOR ENERGÍA&quot; ? true : false" PropertyName="Visible" />
              </DataBindings>
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