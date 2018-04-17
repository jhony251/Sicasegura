<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<root type="NineRays.Reporting.DOM.Document" Name="simpleList" Title="Simple List" IsTemplate="True" Description="Demonstrates how to create simple list report." ScriptLanguage="CSharp">
  <Pages>
    <item type="NineRays.Reporting.DOM.Page" Size="3507.874; 2480.315" Name="page1" StyleName="Normal" Orientation="Landscape" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="3507.874; 118.1102" Name="pageHeader1" StyleName="PageHeaderBack" Location="0; 118.1102">
          <Controls>
            <item type="NineRays.Reporting.DOM.TextBox" Name="textBox6" StyleName="PageHeaderBack" Size="3307.087; 59.05512" Location="59.05512; 0" />
            <item type="NineRays.Reporting.DOM.TextBox" Name="textBox7" StyleName="PageNumber" Size="472.4409; 59.05512" Location="2893.701; 0">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="&quot;Página &quot;+PageNumber.ToString()" PropertyName="Value" />
              </DataBindings>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.Detail" Size="3507.874; 118.1102" CanShrink="True" Name="title" StyleName="ReportTitle" Location="0; 236.2205" CanGrow="True">
          <Controls>
            <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Font="Arial, 18pt, style=Bold" Name="textBox4" Size="3248.031; 59.05512" Location="177.1654; 0" Text="MOTOBOMBAS" />
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand1" DataSource="Contadores.TablaContador" Size="3507.874; 354.3307" Location="0; 413.3858">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="3507.874; 59.05512" RepeatEveryPage="True" Name="header1" StyleName="HeaderFooter1" Location="0; 59.05512" CanGrow="True">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" TextAlign="MiddleLeft" Name="textBox2" GrowToBottom="True" Size="531.4961; 59.05512" Location="59.05512; 0" Text="Código Motobomba" />
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" TextAlign="MiddleLeft" Name="textBox10" GrowToBottom="True" Size="295.2756; 59.05512" Location="1003.937; 0" Text="F. Revisión" />
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" TextAlign="MiddleLeft" Name="textBox9" GrowToBottom="True" Size="590.5512; 59.05512" Location="1889.764; 0" Text="Descripción" />
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" TextAlign="MiddleLeft" Name="textBox11" GrowToBottom="True" Size="354.3307; 59.05512" Location="2775.591; 0" Text="Tipo" />
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" TextAlign="MiddleLeft" Name="textBox12" GrowToBottom="True" Size="295.2756; 59.05512" Location="2480.315; 0" Text="Marca" />
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" TextAlign="MiddleLeft" Name="textBox13" GrowToBottom="True" Size="236.2205; 59.05512" Location="3129.921; 0" Text="Nº Serie" />
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" TextAlign="MiddleLeft" Name="textBox1" GrowToBottom="True" Size="413.3858; 59.05512" Location="590.5512; 0" Text="Código PVYCR" />
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" TextAlign="MiddleLeft" Name="textBox3" GrowToBottom="True" Size="295.2756; 59.05512" Location="1299.213; 0" Text="F. Inicio" />
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" TextAlign="MiddleLeft" Name="textBox14" GrowToBottom="True" Size="295.2756; 59.05512" Location="1594.488; 0" Text="F. Fin" />
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="3507.874; 118.1102" CanShrink="True" Name="detail2" Location="0; 177.1654" CanGrow="True">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1.LineNumber%2==0?&quot;Normal&quot;:&quot;Hightlight&quot;" PropertyName="StyleName" />
              </DataBindings>
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Font="Arial, 9.25pt" TextAlign="MiddleLeft" Name="txtIdContador" GrowToBottom="True" Size="531.4961; 118.1102" Location="59.05487; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.codigoMotobomba&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Font="Arial, 9.25pt" TextAlign="MiddleLeft" Name="texFechaRevision" GrowToBottom="True" Size="295.2756; 118.1102" Location="1003.937; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.FechaRevision&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Font="Arial, 9.25pt" TextAlign="MiddleLeft" Name="textBox16" GrowToBottom="True" Size="590.5512; 118.1102" Location="1889.764; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.Descripcion&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Font="Arial, 9.25pt" TextAlign="MiddleLeft" Name="textBox17" GrowToBottom="True" Size="295.2756; 118.1102" Location="2480.315; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.MarcaBomba&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Font="Arial, 9.25pt" TextAlign="MiddleLeft" Name="textBox18" GrowToBottom="True" Size="354.3307; 118.1102" Location="2775.591; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.TipoBomba&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Font="Arial, 9.25pt" TextAlign="MiddleLeft" Name="textBox19" GrowToBottom="True" Size="236.2205; 118.1102" Location="3129.921; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.NumSerieBomba&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Font="Arial, 9.25pt" TextAlign="MiddleLeft" Name="textBox5" GrowToBottom="True" Size="413.3858; 118.1102" Location="590.5512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.codigoPVYCR&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Font="Arial, 9.25pt" TextAlign="MiddleLeft" Name="textBox8" GrowToBottom="True" Size="295.2756; 118.1102" Location="1299.213; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.FechaInicial&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFormat type="NineRays.Basics.Text.TextFormat" FormatMask="d" UseCultureSettings="True" UseGroupSeparator="True" FormatStyle="Date" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Font="Arial, 9.25pt" TextAlign="MiddleLeft" Name="textBox15" GrowToBottom="True" Size="295.2756; 118.1102" Location="1594.488; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.FechaFin&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFormat type="NineRays.Basics.Text.TextFormat" FormatMask="d" UseCultureSettings="True" UseGroupSeparator="True" FormatStyle="Date" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
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