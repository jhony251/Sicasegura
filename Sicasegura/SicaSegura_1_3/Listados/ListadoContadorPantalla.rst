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
            <item type="NineRays.Reporting.DOM.TextBox" TextAlign="MiddleLeft" Name="textBox3" StyleName="PageNumber" Size="1830.709; 59.05512" Location="59.05512; 0">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="Document.Title" PropertyName="Value" />
              </DataBindings>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.Detail" Size="3507.874; 118.1102" CanShrink="True" Name="title" StyleName="ReportTitle" Location="0; 236.2205" CanGrow="True">
          <Controls>
            <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Font="Arial, 18pt, style=Bold" Name="textBox4" Size="3248.031; 59.05512" Location="177.1654; 0" Text="CONTADORES" />
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand1" DataSource="Contadores.TablaContador" Size="3507.874; 354.3307" Location="0; 413.3858">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="3507.874; 59.05512" RepeatEveryPage="True" Name="header1" StyleName="HeaderFooter1" Location="0; 59.05512" CanGrow="True">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="textBox2" GrowToBottom="True" Size="531.4961; 59.05512" Location="59.05512; 0" Text="Código" />
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="textBox10" GrowToBottom="True" Size="295.2756; 59.05512" Location="590.5512; 0" Text="F. Revisión" />
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="textBox1" GrowToBottom="True" Size="354.3307; 59.05512" Location="885.8268; 0" Text="F. Inicio" />
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="textBox8" GrowToBottom="True" Size="295.2756; 59.05512" Location="1240.157; 0" Text="F. Fin" />
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="textBox9" GrowToBottom="True" Size="649.6063; 59.05512" Location="1535.433; 0" Text="Factor Fabricación" />
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="textBox11" GrowToBottom="True" Size="236.2205; 59.05512" Location="2185.039; 0" Text="Tipo" />
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="textBox12" GrowToBottom="True" Size="354.3307; 59.05512" Location="2421.26; 0" Text="Acceso Cont." />
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="textBox13" GrowToBottom="True" Size="236.2205; 59.05512" Location="2775.591; 0" Text="Aforable" />
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 10pt, style=Bold" Name="textBox20" GrowToBottom="True" Size="354.3307; 59.05512" Location="3011.811; 0" Text="Dist. Aforo (KM)" />
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="3507.874; 118.1102" CanShrink="True" Name="detail2" Location="0; 177.1654" CanGrow="True">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1.LineNumber%2==0?&quot;Normal&quot;:&quot;Hightlight&quot;" PropertyName="StyleName" />
              </DataBindings>
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" TextAlign="MiddleLeft" Name="txtIdContador" GrowToBottom="True" Size="531.4961; 118.1102" Location="59.05512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.codigoPVYCR&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="texFechaRevision" GrowToBottom="True" Size="295.2756; 118.1102" Location="590.5512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.FechaRevision&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="textFechaInicio" GrowToBottom="True" Size="354.3307; 118.1102" Location="885.8268; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.FechaInicial&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="textBox15" GrowToBottom="True" Size="295.2756; 118.1102" Location="1240.157; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.FechaFin&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="textBox16" GrowToBottom="True" Size="649.6063; 118.1102" Location="1535.433; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="(string) GetData(&quot;Contadores.TablaContador.tipocontador&quot;)==&quot;V&quot;?GetData(&quot;Contadores.TablaContador.V_ffcontvolum&quot;):&#xD;&#xA;(string) GetData(&quot;Contadores.TablaContador.tipocontador&quot;)==&quot;E&quot;?GetData(&quot;Contadores.TablaContador.E_ffcontActiva&quot;):&#xD;&#xA;(string) GetData(&quot;Contadores.TablaContador.tipocontador&quot;)==&quot;H&quot;?GetData(&quot;Contadores.TablaContador.H_ffconthora&quot;):&#xD;&#xA;(string) GetData(&quot;Contadores.TablaContador.tipocontador&quot;)==&quot;Q&quot;?GetData(&quot;Contadores.TablaContador.Q_ffcontCaudal&quot;): &quot;&quot;" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="textBox17" GrowToBottom="True" Size="236.2205; 118.1102" Location="2185.039; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.TipoContador&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="textBox18" GrowToBottom="True" Size="354.3307; 118.1102" Location="2421.26; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.AccesoAContador&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="textBox19" GrowToBottom="True" Size="236.2205; 118.1102" Location="2775.591; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.Aforable&quot;)" PropertyName="Value" />
                  </DataBindings>
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" CanShrink="True" Name="textBox21" GrowToBottom="True" Size="354.3307; 118.1102" Location="3011.811; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="GetData(&quot;Contadores.TablaContador.DistanciaPtoAforo_Km&quot;)" PropertyName="Value" />
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