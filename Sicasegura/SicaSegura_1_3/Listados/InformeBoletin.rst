<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<root type="NineRays.Reporting.DOM.Document" Name="simpleList" Title="Simple List" IsTemplate="True" DoublePass="True" Description="Demonstrates how to create simple list report." ScriptLanguage="CSharp">
  <Pages>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="Pagina1" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 2598.425" Name="detail2" Location="0; 531.4961">
          <Controls>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox30" Size="413.3858; 59.05512" Location="1240.159; 413.3858" Text="Provincia:">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox31" Size="708.6614; 59.05512" Location="1653.544; 413.3856">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;Provincia&quot;]" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox32" Size="708.6614; 59.05512" Location="1653.544; 354.3303">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;TerminoMunicipal&quot;]" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox33" Size="413.3858; 59.05512" Location="1240.158; 354.3306" Text="Término Municipal:">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox34" Size="413.3858; 59.05512" Location="1240.157; 295.2754" Text="Paraje:">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox35" Size="708.6614; 59.05512" Location="1653.543; 295.2754">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;Paraje&quot;]" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox36" Size="708.6614; 59.05512" Location="1653.543; 236.2203">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;UTM_Y&quot;]" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox37" Size="708.6614; 59.05512" Location="1653.542; 177.1649">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;UTM_X&quot;]" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox38" Size="413.3858; 118.1102" Location="1240.156; 177.1648" Text="U. T. M.">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox39" Size="413.3858; 59.05512" Location="1240.156; 118.1097" Text="Zona de guardería:">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox40" Size="708.6614; 59.05512" Location="1653.542; 118.1097">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;ZonaGuarderia&quot;]" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox41" Size="767.7166; 59.05512" Location="472.4382; 118.1095">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;NumRef&quot;]" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox42" Size="767.7166; 59.05512" Location="472.4398; 177.1651">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;SRef&quot;]" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox43" Size="767.7166; 59.05512" Location="472.4389; 236.2202">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;PVYCRRef&quot;]" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox44" Size="767.7166; 59.05512" Location="472.4398; 295.2754">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;FechaCorta&quot;]" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox45" Size="767.7166; 59.05512" Location="472.442; 354.3308">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;Tipo&quot;]" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox46" Size="295.2756; 59.05512" Location="177.1655; 354.3309" Text="TIPO:">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox47" Size="295.2756; 59.05512" Location="177.1654; 295.2756" Text="FECHA:">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox48" Size="295.2756; 59.05512" Location="177.1657; 236.2206" Text="PVYCR / REF:">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox49" Size="295.2756; 59.05512" Location="177.1657; 177.1656" Text="S / REF:">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox50" Size="295.2756; 59.05512" Location="177.1622; 118.1097" Text="Nº / REF:">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" TextAlign="MiddleLeft" Name="textBox51" Size="1062.992; 59.05512" Location="177.1646; 59.05466" Text="BOLETÍN DE:" />
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" TextAlign="MiddleLeft" Name="textBox52" Size="1062.992; 59.05512" Location="1240.155; 59.05468" Text="LOCALIZACIÓN" />
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 14pt, style=Bold" TextAlign="MiddleLeft" Name="textBox53" Size="1062.992; 59.05512" Location="177.1654; 636.2602" Text="IDENTIFICACIÓN DE AUTORES" />
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox55" Size="826.7717; 59.05512" Location="413.3858; 708.6614">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;NombreAutor&quot;]" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightGray" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox54" Size="236.2205; 59.05512" Location="177.1654; 708.6614" Text="Autor:">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightGray" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox56" Size="236.2205; 59.05512" Location="177.1654; 767.7166" Text="Domicilio:">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox57" Size="826.7717; 59.05512" Location="413.3858; 767.7166">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;DomicilioAutor&quot;]&#xD;&#xA;" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox58" Size="236.2205; 59.05512" Location="177.1654; 826.7717" Text="Población:">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox59" Size="826.7717; 59.05512" Location="413.3858; 826.7717">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;PoblacionAutor&quot;]&#xD;&#xA;" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox60" Size="885.8268; 59.05512" Location="1476.378; 708.6614">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;NIFAutor&quot;]" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox61" Size="236.2205; 59.05512" Location="1240.157; 708.6614" Text="NIF / CIF:  ">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox62" Size="236.2205; 59.05512" Location="1240.157; 767.7166" Text="TELF:">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox63" Size="885.8268; 59.05512" Location="1476.378; 767.7166">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;TLFAutor&quot;]" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox64" Size="236.2205; 59.05512" Location="1240.157; 826.7717" Text="Provincia:">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox65" Size="590.5512; 59.05512" Location="1476.378; 826.7717">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;ProvinciaAutor&quot;]&#xD;&#xA;" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox66" Size="118.1102; 59.05512" Location="2066.929; 826.7717" Text="C.P.">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox67" Size="177.1654; 59.05512" Location="2185.039; 826.7717">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;CPAutor&quot;]&#xD;&#xA;" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox68" Size="354.3307; 59.05512" Location="177.1654; 885.8268" Text="Representante:">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightGray" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox69" Size="708.6614; 59.05512" Location="531.496; 944.8817">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;TitularTerreno&quot;]" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox70" Size="236.2205; 59.05512" Location="177.1654; 1003.937" Text="Domicilio:">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox71" Size="826.7717; 59.05512" Location="413.3858; 1003.937">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;DomicilioRepresentante&quot;]&#xD;&#xA;" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox72" Size="236.2205; 59.05512" Location="177.1654; 1062.992" Text="Población:">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox73" Size="826.7717; 59.05512" Location="413.3858; 1062.992">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;PoblacionRepresentante&quot;]&#xD;&#xA;" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox74" Size="236.2205; 59.05512" Location="1240.157; 944.8819" Text="NIF / CIF:  ">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox75" Size="236.2205; 59.05512" Location="1240.157; 1003.937" Text="TELF:">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox76" Size="236.2205; 59.05512" Location="1240.157; 1062.992" Text="Provincia:">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox77" Size="885.8268; 59.05512" Location="1476.378; 944.8819">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;NIFRepresentante&quot;]" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox78" Size="590.5512; 59.05512" Location="1476.378; 1062.992">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;ProvinciaRepresentante&quot;]&#xD;&#xA;" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox79" Size="885.8268; 59.05512" Location="1476.378; 1003.937">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;TLFRepresentante&quot;]" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox80" Size="118.1102; 59.05512" Location="2066.929; 1062.992" Text="C.P.">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox81" Size="177.1654; 59.05512" Location="2185.039; 1062.992">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;CPRepresentante&quot;]&#xD;&#xA;" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox82" Size="354.3307; 59.05512" Location="177.1654; 944.8819" Text="Titular Terreno:">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox83" Size="1830.709; 59.05512" Location="531.4961; 885.8268">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;DomicilioRepresentante&quot;]&#xD;&#xA;" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightGray" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox84" Size="472.4409; 59.05512" Location="177.1654; 1122.047" Text="Empresa que ejecuta:">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightGray" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox85" Size="590.5512; 59.05512" Location="649.6063; 1122.047">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;NombreEmpresa&quot;]" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightGray" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox86" Size="236.2205; 59.05512" Location="177.1654; 1181.102" Text="Domicilio:">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox87" Size="826.7717; 59.05512" Location="413.3858; 1181.102">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;DomicilioEmpresa&quot;]&#xD;&#xA;" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox88" Size="236.2205; 59.05512" Location="177.1654; 1240.158" Text="Población:">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox89" Size="826.7717; 59.05512" Location="413.3858; 1240.158">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;PoblacionAEmpresa&quot;]&#xD;&#xA;" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox90" Size="236.2205; 59.05512" Location="1240.157; 1122.047" Text="NIF / CIF:  ">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox91" Size="236.2205; 59.05512" Location="1240.157; 1181.102" Text="TELF:">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox92" Size="236.2205; 59.05512" Location="1240.157; 1240.158" Text="Provincia:">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox93" Size="885.8268; 59.05512" Location="1476.378; 1181.102">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;TLFEmpresa&quot;]" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox94" Size="885.8268; 59.05512" Location="1476.378; 1122.047">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;NIFEmpresa&quot;]" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox95" Size="590.5512; 59.05512" Location="1476.378; 1240.158">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;ProvinciaEmpresa&quot;]&#xD;&#xA;" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox96" Size="118.1102; 59.05512" Location="2066.929; 1240.158" Text="C.P.">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox97" Size="177.1654; 59.05512" Location="2185.039; 1240.158">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;CPEmpresa&quot;]&#xD;&#xA;" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox98" Size="472.4409; 59.05512" Location="177.1654; 1299.213" Text="Director de Obra:">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightGray" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox99" Size="590.5512; 59.05512" Location="649.6063; 1299.213">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;NombreDirectorObra&quot;]" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="LightGray" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox100" Size="236.2205; 59.05512" Location="177.1654; 1358.268" Text="Domicilio:">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox101" Size="826.7717; 59.05512" Location="413.3858; 1358.268">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;DomicilioDirectorObra&quot;]&#xD;&#xA;" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox102" Size="236.2205; 59.05512" Location="177.1654; 1417.323" Text="Población:">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox103" Size="826.7717; 59.05512" Location="413.3858; 1417.323">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;PoblacionDirectorObra&quot;]&#xD;&#xA;" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox104" Size="236.2205; 59.05512" Location="1240.157; 1299.213" Text="NIF / CIF:  ">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox105" Size="236.2205; 59.05512" Location="1240.157; 1358.268" Text="TELF:">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox106" Size="236.2205; 59.05512" Location="1240.157; 1417.323" Text="Provincia:">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox107" Size="885.8268; 59.05512" Location="1476.378; 1358.268">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;TLFDirectorObra&quot;]" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox108" Size="885.8268; 59.05512" Location="1476.378; 1299.213">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;NIFDirectorObra&quot;]" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox109" Size="590.5512; 59.05512" Location="1476.378; 1417.323">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;ProvinciaDirectorObra&quot;]&#xD;&#xA;" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox110" Size="118.1102; 59.05512" Location="2066.929; 1417.323" Text="C.P.">
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" LeftLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox111" Size="177.1654; 59.05512" Location="2185.039; 1417.323">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;CPDirectorObra&quot;]&#xD;&#xA;" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox112" Size="472.4409; 59.05512" Location="649.6063; 1476.378" Text="2ª Oficio Tipo Denuncia:">
              <Margins type="NineRays.Reporting.DOM.Margins" Bottom="0" Left="11.8110237" Top="0" Right="11.8110237" />
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox113" Size="472.4409; 59.05512" Location="177.1654; 1476.378" Text="Documentos Adjuntos:">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox114" Size="118.1102; 59.05512" Location="1122.047; 1476.378">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;TipoDenunciaTXT&quot;]&#xD;&#xA;" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox115" Size="236.2205; 59.05512" Location="1240.157; 1476.378" Text="3ª Fotos:">
              <Margins type="NineRays.Reporting.DOM.Margins" Bottom="0" Left="11.8110237" Top="0" Right="11.8110237" />
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox116" Size="118.1102; 59.05512" Location="1476.378; 1476.378">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;FotosTXT&quot;]&#xD;&#xA;" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox117" Size="236.2205; 59.05512" Location="2007.874; 1476.378" Text="4ª Croquis:">
              <Margins type="NineRays.Reporting.DOM.Margins" Bottom="0" Left="11.8110237" Top="0" Right="11.8110237" />
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox118" Size="118.1102; 59.05512" Location="2244.095; 1476.378">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;CroquisTXT&quot;]&#xD;&#xA;" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox119" Size="295.2756; 59.05512" Location="1594.488; 1476.378" Text="5ª Planos: ">
              <Margins type="NineRays.Reporting.DOM.Margins" Bottom="0" Left="11.8110237" Top="0" Right="11.8110237" />
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox120" Size="118.1102; 59.05512" Location="1889.764; 1476.378">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;PlanosTXT&quot;]&#xD;&#xA;" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" TopLine="1 Solid Black" RightLine="1 Solid Black" BottomLine="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox121" Size="1062.992; 59.05512" Location="177.1654; 1653.543" Text="HECHOS">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 9.5pt" TextAlign="TopLeft" Name="textBox122" Size="1062.992; 885.8268" Location="177.1654; 1712.598">
              <Margins type="NineRays.Reporting.DOM.Margins" Bottom="11.8110237" Left="11.8110237" Top="11.8110237" Right="11.8110237" />
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;Hechos&quot;]" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 9.5pt" TextAlign="TopLeft" Name="textBox123" Size="1122.047; 885.8268" Location="1240.157; 1712.598" Text="Por el personal de PVYCR / Servicio Apoyo Guardería Fluvial&#xD;&#xA;&#xD;&#xA;&#xD;&#xA;&#xD;&#xA;&#xD;&#xA;&#xD;&#xA;&#xD;&#xA;Fdo:&#xD;&#xA;Por el Servicio de Guardería y Policía Fluvial&#xD;&#xA;Encargado de la Zona&#xD;&#xA;&#xD;&#xA;&#xD;&#xA;&#xD;&#xA;&#xD;&#xA;&#xD;&#xA;&#xD;&#xA;Fdo:">
              <Margins type="NineRays.Reporting.DOM.Margins" Bottom="11.8110237" Left="11.8110237" Top="11.8110237" Right="11.8110237" />
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox127" Size="413.3858; 59.05512" Location="1240.157; 472.4409" Text="Mapa 1:25000">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox128" Size="708.6614; 59.05512" Location="1653.542; 472.4407">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;Hoja&quot;]" PropertyName="Value" />
              </DataBindings>
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox130" Size="1122.047; 59.05512" Location="1240.157; 1653.543">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 236.2205" Name="pageHeader2" Location="0; 177.1654">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture1" Size="708.6614; 177.1654" Location="177.1654; 33.31002" SizeMode="Normal">
              <Image length="2809">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8pt" TextAlign="MiddleLeft" Name="textBox126" GrowToBottom="True" Size="885.8268; 118.1102" Location="1358.268; 61.49542" Text="CONFEDERACIÓN HIDROGRÁFICA DEL SEGURA.&#xD;&#xA;COMISARÍA DE AGUAS.&#xD;&#xA;SERVICIO DE GUARDERÍA Y POLICÍA FLUVIAL.&#xD;&#xA;" />
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.PageFooter" Size="2480.315; 177.1654" Name="pageFooter1" Location="0; 3188.976">
          <Controls>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8pt" TextAlign="TopLeft" Name="textBox124" Size="472.4409; 177.1654" Location="1948.819; 0" Text="C/ Mahonesas , nº 2&#xD;&#xA;30004 MURCIA&#xD;&#xA;Tel:  968 35 88 90&#xD;&#xA;Fax: 968 35 88 95 " />
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8pt" TextAlign="TopLeft" Name="textBox5" Size="1653.543; 177.1654" Location="177.1654; 0" Text="SR. COMISARIO DE AGUAS- CONFEDERACIÓN HIDROGRÁFICA DEL SEGURA&#xD;&#xA;&#xD;&#xA;CORREO ELECTRÓNICO comisaria@chsegura.es&#xD;&#xA;" />
          </Controls>
        </item>
      </Controls>
    </item>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="Pagina2" StyleName="Normal" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand1" DataSource="DataSet.TablaBoletinImages" Size="2480.315; 2007.874" Location="0; 1122.047">
          <Aggregates>
            <item type="NineRays.Reporting.DOM.Aggregate" Expression="ColumnNumber" Name="numcolumn" />
          </Aggregates>
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 1889.764" Name="detail1" StyleName="Normal" Location="0; 59.05512">
              <Controls>
                <item type="NineRays.Reporting.DOM.Picture" Name="Foto1" Size="1092.52; 826.7717" Location="177.1654; 118.1102">
                  <Margins type="NineRays.Reporting.DOM.Margins" Bottom="59.05512" Left="59.05512" Top="59.05512" Right="59.05512" />
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Foto1&quot;]" PropertyName="Image" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1" PropertyName="Border" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.Picture" Name="Foto2" Size="1092.52; 826.7717" Location="1269.685; 118.1102">
                  <Margins type="NineRays.Reporting.DOM.Margins" Bottom="59.05512" Left="59.05512" Top="59.05512" Right="59.05512" />
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Foto2&quot;]" PropertyName="Image" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1" PropertyName="Border" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.Picture" Name="Foto3" Size="1092.52; 826.7717" Location="177.1654; 1003.937">
                  <Margins type="NineRays.Reporting.DOM.Margins" Bottom="59.05512" Left="59.05512" Top="59.05512" Right="59.05512" />
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Foto3&quot;]" PropertyName="Image" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1" PropertyName="Border" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.Picture" Name="Foto4" Size="1092.52; 826.7717" Location="1269.685; 1003.937">
                  <Margins type="NineRays.Reporting.DOM.Margins" Bottom="59.05512" Left="59.05512" Top="59.05512" Right="59.05512" />
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;Foto4&quot;]" PropertyName="Image" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1" PropertyName="Border" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.75pt, style=Bold" Name="textBox1" Size="1092.52; 59.05512" Location="177.1654; 59.05512" Text="FOTO 1">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.75pt, style=Bold" Name="textBox2" Size="1092.52; 59.05512" Location="1269.685; 59.05512" Text="FOTO 2">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.75pt, style=Bold" Name="textBox3" Size="1092.52; 59.05512" Location="177.1648; 944.882" Text="FOTO 3">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.75pt, style=Bold" Name="textBox4" Size="1092.52; 59.05512" Location="1269.685; 944.8818" Text="FOTO 4">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand2" DataSource="DataSet.TablaBoletin" Size="2480.315; 590.5512" Location="0; 472.4409">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 472.4409" Name="detail3" Location="0; 59.05512">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox23" Size="413.3858; 59.05512" Location="1240.159; 413.3858" Text="Provincia:">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox29" Size="708.6614; 59.05512" Location="1653.544; 413.3856">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;Provincia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox28" Size="708.6614; 59.05512" Location="1653.544; 354.3303">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;TerminoMunicipal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox27" Size="413.3858; 59.05512" Location="1240.158; 354.3306" Text="Término Municipal:">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox25" Size="413.3858; 59.05512" Location="1240.157; 295.2754" Text="Paraje:">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox26" Size="708.6614; 59.05512" Location="1653.543; 295.2756">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;Paraje&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox24" Size="708.6614; 59.05512" Location="1653.543; 236.2205">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;UTM_Y&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox22" Size="708.6614; 59.05512" Location="1653.542; 177.1651">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;UTM_X&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox21" Size="413.3858; 118.1102" Location="1240.157; 177.1653" Text="U. T. M.">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox19" Size="413.3858; 59.05512" Location="1240.156; 118.1099" Text="Zona de guardería:">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox20" Size="708.6614; 59.05512" Location="1653.542; 118.1099">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;ZonaGuarderia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox8" Size="767.7166; 59.05512" Location="472.4386; 118.1097">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;NumRef&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox15" Size="767.7166; 59.05512" Location="472.4402; 177.1653">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;SRef&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox17" Size="767.7166; 59.05512" Location="472.4393; 236.2204">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;PVYCRRef&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox16" Size="767.7166; 59.05512" Location="472.4402; 295.2756">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;FechaCorta&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt" TextAlign="MiddleLeft" Name="textBox18" Size="767.7166; 59.05512" Location="472.4424; 354.331">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand2[&quot;Tipo&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox14" Size="295.2756; 59.05512" Location="177.1659; 354.3311" Text="TIPO:">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox13" Size="295.2756; 59.05512" Location="177.1658; 295.2758" Text="FECHA:">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox12" Size="295.2756; 59.05512" Location="177.1661; 236.2208" Text="PVYCR / REF:">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox10" Size="295.2756; 59.05512" Location="177.1661; 177.1658" Text="S / REF:">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 9.5pt, style=Bold" TextAlign="MiddleLeft" Name="textBox9" Size="295.2756; 59.05512" Location="177.1626; 118.1099" Text="Nº / REF:">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" TextAlign="MiddleLeft" Name="textBox11" Size="1062.992; 59.05512" Location="177.1646; 59.05466" Text="BOLETÍN DE:">
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" TextAlign="MiddleLeft" Name="textBox7" Size="1062.992; 59.05512" Location="1240.155; 59.05489" Text="LOCALIZACIÓN">
                  <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 236.2205" Name="pageHeader1" Location="0; 177.1654">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture4" Size="708.6614; 177.1654" Location="177.1654; 30.74771" SizeMode="Normal">
              <Image length="2809">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" CanGrow="True" Font="Arial, 8pt" TextAlign="MiddleLeft" Name="textBox129" GrowToBottom="True" Size="885.8268; 177.1654" Location="1358.268; 33.43203" Text="CONFEDERACIÓN HIDROGRÁFICA DEL SEGURA.&#xD;&#xA;COMISARÍA DE AGUAS.&#xD;&#xA;SERVICIO DE GUARDERÍA Y POLICÍA FLUVIAL.&#xD;&#xA;">
              <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.PageFooter" Size="2480.315; 295.2756" Name="pageFooter2" Location="0; 3188.976">
          <Controls>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8pt" TextAlign="TopLeft" Name="textBox6" Size="472.4409; 177.1654" Location="1948.819; 0" Text="C/ Mahonesas , nº 2&#xD;&#xA;30004 MURCIA&#xD;&#xA;Tel:  968 35 88 90&#xD;&#xA;Fax: 968 35 88 95 ">
              <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8pt" TextAlign="TopLeft" Name="textBox125" Size="1653.543; 177.1654" Location="177.1654; 0" Text="SR. COMISARIO DE AGUAS- CONFEDERACIÓN HIDROGRÁFICA DEL SEGURA&#xD;&#xA;&#xD;&#xA;CORREO ELECTRÓNICO comisaria@chsegura.es&#xD;&#xA;">
              <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
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