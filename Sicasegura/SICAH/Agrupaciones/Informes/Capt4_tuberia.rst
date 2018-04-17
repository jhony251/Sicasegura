<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<root type="NineRays.Reporting.DOM.Document" Name="Capt4_tuberia" GridStep="11.8110237" Title="Cuadrante Brigada Anual" IsTemplate="True" DoublePass="True" ImportsString="System.Drawing" Description="Demonstrates how to create simple list report." ScriptLanguage="CSharp">
  <Pages>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Visible="False" Name="page1" StyleName="Normal" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="236.220474" Left="141.732285" Top="236.220474" Right="82.67716" />
      <Controls>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand1" DataSource="DataSet.TablaOcupaciones" Size="2480.315; 2574.803" Location="0; 897.6378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 2338.583" Name="detail1" Location="0; 47.24409" CanGrow="True">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;VisibleVacioLinea&quot;]" PropertyName="Visible" />
              </DataBindings>
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox3" Size="236.2205; 70.86614" Location="814.9606; 47.24409" />
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 25pt" Name="textBox2" Size="2255.906; 1157.48" Location="141.7323; 555.1181">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="&quot;LIBRO DE CONTROL DEL AGUA REALMENTE UTILIZADA EN LAS TOMAS DE UN APROVECHAMIENTO DE AGUA, INSCRITO EN LA SECCIÓN &quot; + dataBand1[&quot;sec&quot;] + &quot; DEL REGISTRO DE AGUAS&quot;" PropertyName="Value" />
                  </DataBindings>
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox6" Size="2078.74; 118.1102" Location="200.7874; 2137.795" Text="ORDEN ARM/1312/2009, DE 20 DE MAYO " />
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 673.2283" Name="pageHeader1" Location="0; 177.1654">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture1" Size="266.9291; 291.7323" Location="141.7323; 188.9764">
              <Image length="3432">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" TextAlign="MiddleLeft" Name="textBox1" Size="1547.244; 70.86614" Location="602.3622; 200.7874" Text="MINISTERIO DEL MEDIO AMBIENTE" />
            <item type="NineRays.Reporting.DOM.TextBox" TextAlign="MiddleLeft" Name="textBox4" Size="1547.244; 70.86614" Location="602.3622; 295.2756" Text="Y MEDIO RURAL MARINO" />
            <item type="NineRays.Reporting.DOM.TextBox" TextAlign="MiddleLeft" Name="textBox5" Size="1547.244; 70.86614" Location="602.3622; 389.7638" Text="Confederación Hidrográfica del Segura" />
          </Controls>
        </item>
      </Controls>
    </item>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="page2" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="141.732285" Top="118.110237" Right="82.67716" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 673.2283" Name="pageHeader2" Location="0; 177.1654">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture2" Size="266.9291; 291.7323" Location="141.7323; 188.9764">
              <Image length="3432">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" TextAlign="MiddleLeft" Name="textBox7" Size="1547.244; 70.86614" Location="602.3622; 200.7874" Text="MINISTERIO DEL MEDIO AMBIENTE" />
            <item type="NineRays.Reporting.DOM.TextBox" TextAlign="MiddleLeft" Name="textBox8" Size="1547.244; 70.86614" Location="602.3622; 295.2756" Text="Y MEDIO RURAL MARINO" />
            <item type="NineRays.Reporting.DOM.TextBox" TextAlign="MiddleLeft" Name="textBox9" Size="1547.244; 70.86614" Location="602.3622; 389.7638" Text="Confederación Hidrográfica del Segura" />
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand2" DataSource="DataSet.TablaOcupaciones" Size="2480.315; 2574.803" Location="0; 897.6378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 2480.315" Name="detail2" Location="0; 47.24409" CanGrow="True">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;VisibleVacioLinea&quot;]" PropertyName="Visible" />
              </DataBindings>
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox10" Size="236.2205; 70.86614" Location="814.9606; 47.24409" />
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 17pt" TextAlign="TopCenter" Name="advancedText1" Size="2255.906; 1015.748" Location="141.7323; 0" Text="&lt;i&gt;&lt;p align=center&gt;&#xD;&#xA;&quot;El agua es un recurso natural escaso, indispensable para la vida y &#xD;&#xA;para el ejercicio de la inmensa mayoría de las actividades &#xD;&#xA;económicas; es irreemplazable, no ampliable por la mera voluntad &#xD;&#xA;del hombre, irregular en su forma de presentarse en el tiempo y en el &#xD;&#xA;espacio, fácilmente vulnerable y susceptible de usos sucesivos...&quot;&lt;/p&gt;&#xD;&#xA;&lt;/i&gt;&lt;br&gt;&#xD;&#xA;&lt;p align=center&gt;&#xD;&#xA;&lt;font face=Arial size=12&gt;Preámbulo de la Ley de Aguas de 1985.&lt;/font&gt;&lt;/p&gt;&#xD;&#xA;&lt;br&gt;&#xD;&#xA;&lt;br&gt;&#xD;&#xA;&lt;p align=justify&gt;&#xD;&#xA;Este libro pretende llegar a ser un reflejo de la imprescindible &#xD;&#xA;colaboración que debe existir entre la administración del agua y los &#xD;&#xA;usuarios de la misma, con el fin de que ambos puedan alcanzar los &#xD;&#xA;objetivos que persiguen referentes a:&lt;/p&gt;" />
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 17pt" TextAlign="TopCenter" Name="advancedText2" Size="1724.409; 377.9528" Location="673.2283; 1039.37" Text="&lt;p&gt;&lt;font face=&quot;Symbol&quot;&gt;·&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&lt;font face=&quot;Arial&quot;&gt;garantizar el respeto a los derechos &#xD;&#xA;preexistentes,&lt;/p&gt;&#xD;&#xA;&lt;p&gt;&lt;font face=&quot;Symbol&quot;&gt;·&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&lt;font face=&quot;Arial&quot;&gt;posibilitar la eficaz planificación de los recursos,&lt;/p&gt;&#xD;&#xA;&lt;p&gt;&lt;font face=&quot;Symbol&quot;&gt;·&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&lt;font face=&quot;Arial&quot;&gt;permitir la correcta administración del agua,&lt;/p&gt;" />
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 17pt" TextAlign="TopCenter" Name="advancedText3" Size="2255.906; 956.6929" Location="141.7323; 1488.189">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="&quot;&lt;p align=justify&gt;&quot; + &#xD;&#xA;&quot;Con este propósito, cada cierto periodo, se rellenará una línea de las &quot; +&#xD;&#xA;&quot;contenidas en las páginas reticuladas siguientes. Una vez al año, se &quot; +&#xD;&#xA;&quot;completará una línea especial (también definida en las páginas &quot; +&#xD;&#xA;&quot;reticuladas) en la que se debe reflejar el volumen de agua total &quot; +&#xD;&#xA;&quot;utilizado durante el año.&lt;/p&gt;&quot; +&#xD;&#xA;&quot;&lt;br&gt;&quot; +&#xD;&#xA;&quot;&lt;br&gt;&quot; +&#xD;&#xA;&quot;&lt;p align=justify&gt;&quot; +&#xD;&#xA;&quot;Atendiendo al artículo 55.4 del texto refundido de la Ley de Aguas &quot; +&#xD;&#xA;&quot;(RDL 1/2001, de 20 de julio), así como a la disposición adicional &quot; + &#xD;&#xA;&quot;duodécima de la Ley del Plan Hidrológico Nacional (Ley 10/2001, de &quot; +&#xD;&#xA;&quot;5 de julio) y a la presente Orden, se abre con fecha &quot; + Now.Day + &quot;/&quot; + Now.Month + &quot;/&quot; + Now.Year + &quot; este &quot; +&#xD;&#xA;&quot;Libro para el control del agua realmente utilizada en el &quot; +&#xD;&#xA;&quot;aprovechamiento cuyas características se indican en la siguiente&lt;/p&gt;&quot;" PropertyName="Text" />
                  </DataBindings>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="page3" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="141.732285" Top="118.110237" Right="82.67716" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 673.2283" Name="pageHeader3" Location="0; 177.1654">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture3" Size="266.9291; 291.7323" Location="141.7323; 188.9764">
              <Image length="3432">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.TextBox" TextAlign="MiddleLeft" Name="textBox11" Size="1547.244; 70.86614" Location="602.3622; 200.7874" Text="MINISTERIO DEL MEDIO AMBIENTE" />
            <item type="NineRays.Reporting.DOM.TextBox" TextAlign="MiddleLeft" Name="textBox12" Size="1547.244; 70.86614" Location="602.3622; 295.2756" Text="Y MEDIO RURAL MARINO" />
            <item type="NineRays.Reporting.DOM.TextBox" TextAlign="MiddleLeft" Name="textBox13" Size="1547.244; 70.86614" Location="602.3622; 389.7638" Text="Confederación Hidrográfica del Segura" />
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand3" DataSource="DataSet.TablaOcupaciones" Size="2480.315; 1606.299" Location="0; 897.6378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 566.9291" Name="detail3" Location="0; 47.24409" CanGrow="True">
              <DataBindings>
                <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand1[&quot;VisibleVacioLinea&quot;]" PropertyName="Visible" />
              </DataBindings>
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox14" Size="236.2205; 70.86614" Location="814.9606; 47.24409" />
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 21pt" TextAlign="TopCenter" Name="advancedText4" Size="2279.528; 566.9291" Location="129.9213; 0" Text="&lt;p align=center&gt;&#xD;&#xA;&lt;b&gt;DATOS DEL APROVECHAMIENTO&lt;/b&gt;&lt;/p&gt;&#xD;&#xA;&lt;p&gt;&#xD;&#xA;Titulares: (Nombre, Apellidos y DNI o CIF de cada uno) &#xD;&#xA;Inscripción en la sección .... del Registro de Aguas (o del &#xD;&#xA;Catálogo de Aguas Privadas con el número de &#xD;&#xA;inscripción ................&lt;/p&gt;" />
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand4" DataSource="Dataset.TablaTomas" Size="2480.315; 897.6378" Location="0; 661.4174">
              <Controls>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 803.1496" Name="detail4" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 21pt" TextAlign="TopCenter" Name="advancedText5" Size="2090.551; 732.2834" Location="248.0315; 0" Text="&lt;p align=center&gt;&#xD;&#xA;&lt;b&gt;TOMA I:&lt;/b&gt;&lt;/p&gt;&#xD;&#xA;&lt;p&gt;&#xD;&#xA;Localización: (provincia, término municipal y paraje, en su caso) &lt;br&gt;&#xD;&#xA;Caracteríscias físicas: (sistema de derivación o diámetro de la perforación y profundidad de la bomba, en su caso)&lt;br&gt;&#xD;&#xA;Croquis de la toma:&#xD;&#xA;&lt;/p&gt;">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="Enero" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader12" Location="0; 165.3543">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture12" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape9" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText41" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText42" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES MEDIANTE &lt;br&gt;&#xD;&#xA;TUBERÍA A PRESIÓN (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand29" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6299">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail37" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox316" Size="767.7166; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox317" Size="602.3622; 141.7323" Location="944.8819; 0" Text="Catálogo de Aguas Privadas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox318" Size="318.8976; 70.86614" Location="1547.244; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox319" Size="318.8976; 70.86614" Location="1547.244; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox320" Size="496.063; 70.86614" Location="1866.142; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand29[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox321" Size="496.063; 70.86614" Location="1866.142; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand29[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox322" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail38" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox323" Size="1370.079; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox324" Size="614.1732; 59.05512" Location="1547.244; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox325" Size="200.7874; 59.05512" Location="2161.417; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox326" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox327" Size="755.9055; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox328" Size="814.9606; 59.05512" Location="1547.244; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox329" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand29[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox330" Size="755.9055; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand29[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox331" Size="814.9606; 59.05512" Location="1547.244; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand29[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox332" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox333" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox334" Size="1311.024; 129.9213" Location="1051.181; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox335" Size="874.0157; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (01/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand30" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header17" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox336" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail39" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox337" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox338" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand30[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox339" Size="1311.024; 59.05512" Location="1051.181; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand30[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox340" Size="425.1968; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand31" DataSource="dataSet.Datos1" Size="2480.315; 437.0079" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header18" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox342" Size="118.1102; 118.1102" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText43" Size="330.7087; 118.1102" Location="295.2756; 0" Text="Lectura&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText44" Size="425.1968; 118.1102" Location="625.9843; 0" Text="Lectura Actual - &lt;br&gt;Lectura Anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox343" Size="496.063; 118.1102" Location="1051.181; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox344" Size="814.9606; 118.1102" Location="1547.244; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail40" Location="0; 212.5984">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox345" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand31[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox347" Size="330.7087; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand31[&quot;lectura&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox348" Size="425.1968; 70.86614" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand31[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox349" Size="496.063; 70.86614" Location="1051.181; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand31[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox350" Size="814.9606; 70.86614" Location="1547.244; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand31[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer7" Location="0; 330.7087">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox351" Size="2185.039; 59.05512" Location="177.1654; 0" Text="(CONTINUACIÓN DEL MES EN HOJA SIGUIENTE)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="Enero2" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader13" Location="0; 165.3543">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture13" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape10" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText45" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText46" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES MEDIANTE &lt;br&gt;&#xD;&#xA;TUBERÍA A PRESIÓN (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand32" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6299">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail41" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox341" Size="767.7166; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox346" Size="602.3622; 141.7323" Location="944.8819; 0" Text="Catálogo de Aguas Privadas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox353" Size="318.8976; 70.86614" Location="1547.244; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox354" Size="318.8976; 70.86614" Location="1547.244; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox355" Size="496.063; 70.86614" Location="1866.142; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand32[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox356" Size="496.063; 70.86614" Location="1866.142; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand32[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox357" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail42" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox358" Size="1370.079; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox359" Size="614.1732; 59.05512" Location="1547.244; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox360" Size="200.7874; 59.05512" Location="2161.417; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox361" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox362" Size="755.9055; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox363" Size="814.9606; 59.05512" Location="1547.244; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox364" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand32[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox365" Size="755.9055; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand32[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox366" Size="814.9606; 59.05512" Location="1547.244; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand32[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox367" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox368" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox369" Size="1311.024; 129.9213" Location="1051.181; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox352" Size="874.0157; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (01/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand33" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header19" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox371" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail43" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox372" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox373" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand33[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox374" Size="1311.024; 59.05512" Location="1051.181; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand33[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox375" Size="425.1968; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand34" DataSource="dataSet.Datos1_2" Size="2480.315; 437.0079" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header20" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox376" Size="118.1102; 118.1102" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText47" Size="330.7087; 118.1102" Location="295.2756; 0" Text="Lectura&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText48" Size="425.1968; 118.1102" Location="625.9843; 0" Text="Lectura Actual - &lt;br&gt;Lectura Anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox377" Size="496.063; 118.1102" Location="1051.181; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox378" Size="814.9606; 118.1102" Location="1547.244; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail44" Location="0; 212.5984">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox379" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand34[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox380" Size="330.7087; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand34[&quot;lectura&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox381" Size="425.1968; 70.86614" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand34[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox382" Size="496.063; 70.86614" Location="1051.181; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand34[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox383" Size="814.9606; 70.86614" Location="1547.244; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand34[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer8" Location="0; 330.7087">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox384" Size="448.8189; 59.05512" Location="177.1654; 0" Text="VOLUMEN MES (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" Name="textBox385" Size="425.1968; 59.05512" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand34[&quot;volumen&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="Febrero" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader14" Location="0; 165.3543">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture14" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape11" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText49" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText50" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES MEDIANTE &lt;br&gt;&#xD;&#xA;TUBERÍA A PRESIÓN (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand35" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6299">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail45" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox370" Size="767.7166; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox386" Size="602.3622; 141.7323" Location="944.8819; 0" Text="Catálogo de Aguas Privadas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox387" Size="318.8976; 70.86614" Location="1547.244; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox388" Size="318.8976; 70.86614" Location="1547.244; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox389" Size="496.063; 70.86614" Location="1866.142; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand35[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox390" Size="496.063; 70.86614" Location="1866.142; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand35[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox391" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail46" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox392" Size="1370.079; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox393" Size="614.1732; 59.05512" Location="1547.244; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox394" Size="200.7874; 59.05512" Location="2161.417; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox395" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox396" Size="755.9055; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox397" Size="814.9606; 59.05512" Location="1547.244; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox398" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand35[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox399" Size="755.9055; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand35[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox400" Size="814.9606; 59.05512" Location="1547.244; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand35[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox401" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox402" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox403" Size="1311.024; 129.9213" Location="1051.181; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox404" Size="874.0157; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (02/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand36" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header21" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox405" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail47" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox406" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox407" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand36[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox408" Size="1311.024; 59.05512" Location="1051.181; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand36[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox409" Size="425.1968; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand37" DataSource="dataSet.Datos2" Size="2480.315; 437.0079" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header22" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox410" Size="118.1102; 118.1102" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText51" Size="330.7087; 118.1102" Location="295.2756; 0" Text="Lectura&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText52" Size="425.1968; 118.1102" Location="625.9843; 0" Text="Lectura Actual - &lt;br&gt;Lectura Anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox411" Size="496.063; 118.1102" Location="1051.181; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox412" Size="814.9606; 118.1102" Location="1547.244; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail48" Location="0; 212.5984">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox413" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand37[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox414" Size="330.7087; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand37[&quot;lectura&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox415" Size="425.1968; 70.86614" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand37[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox416" Size="496.063; 70.86614" Location="1051.181; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand37[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox417" Size="814.9606; 70.86614" Location="1547.244; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand37[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer9" Location="0; 330.7087">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox418" Size="2185.039; 59.05512" Location="177.1654; 0" Text="(CONTINUACIÓN DEL MES EN HOJA SIGUIENTE)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="Febrero2" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader15" Location="0; 165.3543">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture15" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape12" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText53" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText54" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES MEDIANTE &lt;br&gt;&#xD;&#xA;TUBERÍA A PRESIÓN (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand38" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6299">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail49" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox419" Size="767.7166; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox420" Size="602.3622; 141.7323" Location="944.8819; 0" Text="Catálogo de Aguas Privadas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox421" Size="318.8976; 70.86614" Location="1547.244; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox422" Size="318.8976; 70.86614" Location="1547.244; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox423" Size="496.063; 70.86614" Location="1866.142; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand38[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox424" Size="496.063; 70.86614" Location="1866.142; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand38[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox425" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail50" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox426" Size="1370.079; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox427" Size="614.1732; 59.05512" Location="1547.244; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox428" Size="200.7874; 59.05512" Location="2161.417; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox429" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox430" Size="755.9055; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox431" Size="814.9606; 59.05512" Location="1547.244; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox432" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand38[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox433" Size="755.9055; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand38[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox434" Size="814.9606; 59.05512" Location="1547.244; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand38[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox435" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox436" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox437" Size="1311.024; 129.9213" Location="1051.181; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox438" Size="874.0157; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (02/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand39" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header23" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox439" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail51" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox440" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox441" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand39[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox442" Size="1311.024; 59.05512" Location="1051.181; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand39[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox443" Size="425.1968; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand40" DataSource="dataSet.Datos2_2" Size="2480.315; 437.0079" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header24" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox444" Size="118.1102; 118.1102" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText55" Size="330.7087; 118.1102" Location="295.2756; 0" Text="Lectura&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText56" Size="425.1968; 118.1102" Location="625.9843; 0" Text="Lectura Actual - &lt;br&gt;Lectura Anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox445" Size="496.063; 118.1102" Location="1051.181; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox446" Size="814.9606; 118.1102" Location="1547.244; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail52" Location="0; 212.5984">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox447" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand40[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox448" Size="330.7087; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand40[&quot;lectura&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox449" Size="425.1968; 70.86614" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand40[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox450" Size="496.063; 70.86614" Location="1051.181; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand40[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox451" Size="814.9606; 70.86614" Location="1547.244; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand40[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer10" Location="0; 330.7087">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox452" Size="448.8189; 59.05512" Location="177.1654; 0" Text="VOLUMEN MES (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" Name="textBox453" Size="425.1968; 59.05512" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand40[&quot;volumen&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="Marzo" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader16" Location="0; 165.3543">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture16" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape13" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText57" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText58" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES MEDIANTE &lt;br&gt;&#xD;&#xA;TUBERÍA A PRESIÓN (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand41" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6299">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail53" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox454" Size="767.7166; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox455" Size="602.3622; 141.7323" Location="944.8819; 0" Text="Catálogo de Aguas Privadas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox456" Size="318.8976; 70.86614" Location="1547.244; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox457" Size="318.8976; 70.86614" Location="1547.244; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox458" Size="496.063; 70.86614" Location="1866.142; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand41[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox459" Size="496.063; 70.86614" Location="1866.142; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand41[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox460" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail54" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox461" Size="1370.079; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox462" Size="614.1732; 59.05512" Location="1547.244; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox463" Size="200.7874; 59.05512" Location="2161.417; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox464" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox465" Size="755.9055; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox466" Size="814.9606; 59.05512" Location="1547.244; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox467" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand41[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox468" Size="755.9055; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand41[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox469" Size="814.9606; 59.05512" Location="1547.244; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand41[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox470" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox471" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox472" Size="1311.024; 129.9213" Location="1051.181; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox473" Size="874.0157; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (03/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand42" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header25" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox474" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail55" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox475" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox476" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand42[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox477" Size="1311.024; 59.05512" Location="1051.181; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand42[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox478" Size="425.1968; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand43" DataSource="dataSet.Datos3" Size="2480.315; 437.0079" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header26" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox479" Size="118.1102; 118.1102" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText59" Size="330.7087; 118.1102" Location="295.2756; 0" Text="Lectura&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText60" Size="425.1968; 118.1102" Location="625.9843; 0" Text="Lectura Actual - &lt;br&gt;Lectura Anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox480" Size="496.063; 118.1102" Location="1051.181; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox481" Size="814.9606; 118.1102" Location="1547.244; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail56" Location="0; 212.5984">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox482" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand43[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox483" Size="330.7087; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand43[&quot;lectura&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox484" Size="425.1968; 70.86614" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand43[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox485" Size="496.063; 70.86614" Location="1051.181; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand43[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox486" Size="814.9606; 70.86614" Location="1547.244; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand43[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer11" Location="0; 330.7087">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox487" Size="2185.039; 59.05512" Location="177.1654; 0" Text="(CONTINUACIÓN DEL MES EN HOJA SIGUIENTE)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="Marzo2" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader17" Location="0; 165.3543">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture17" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape14" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText61" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText62" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES MEDIANTE &lt;br&gt;&#xD;&#xA;TUBERÍA A PRESIÓN (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand44" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6299">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail57" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox488" Size="767.7166; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox489" Size="602.3622; 141.7323" Location="944.8819; 0" Text="Catálogo de Aguas Privadas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox490" Size="318.8976; 70.86614" Location="1547.244; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox491" Size="318.8976; 70.86614" Location="1547.244; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox492" Size="496.063; 70.86614" Location="1866.142; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand44[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox493" Size="496.063; 70.86614" Location="1866.142; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand44[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox494" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail58" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox495" Size="1370.079; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox496" Size="614.1732; 59.05512" Location="1547.244; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox497" Size="200.7874; 59.05512" Location="2161.417; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox498" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox499" Size="755.9055; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox500" Size="814.9606; 59.05512" Location="1547.244; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox501" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand44[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox502" Size="755.9055; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand44[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox503" Size="814.9606; 59.05512" Location="1547.244; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand44[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox504" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox505" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox506" Size="1311.024; 129.9213" Location="1051.181; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox507" Size="874.0157; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (03/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand45" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header27" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox508" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail59" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox509" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox510" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand45[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox511" Size="1311.024; 59.05512" Location="1051.181; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand45[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox512" Size="425.1968; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand46" DataSource="dataSet.Datos3_2" Size="2480.315; 437.0079" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header28" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox513" Size="118.1102; 118.1102" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText63" Size="330.7087; 118.1102" Location="295.2756; 0" Text="Lectura&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText64" Size="425.1968; 118.1102" Location="625.9843; 0" Text="Lectura Actual - &lt;br&gt;Lectura Anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox514" Size="496.063; 118.1102" Location="1051.181; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox515" Size="814.9606; 118.1102" Location="1547.244; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail60" Location="0; 212.5984">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox516" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand46[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox517" Size="330.7087; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand46[&quot;lectura&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox518" Size="425.1968; 70.86614" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand46[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox519" Size="496.063; 70.86614" Location="1051.181; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand46[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox520" Size="814.9606; 70.86614" Location="1547.244; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand46[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer12" Location="0; 330.7087">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox521" Size="448.8189; 59.05512" Location="177.1654; 0" Text="VOLUMEN MES (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" Name="textBox522" Size="425.1968; 59.05512" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand46[&quot;volumen&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="Abril" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader18" Location="0; 165.3543">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture18" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape15" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText65" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText66" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES MEDIANTE &lt;br&gt;&#xD;&#xA;TUBERÍA A PRESIÓN (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand47" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6299">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail61" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox523" Size="767.7166; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox524" Size="602.3622; 141.7323" Location="944.8819; 0" Text="Catálogo de Aguas Privadas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox525" Size="318.8976; 70.86614" Location="1547.244; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox526" Size="318.8976; 70.86614" Location="1547.244; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox527" Size="496.063; 70.86614" Location="1866.142; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand47[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox528" Size="496.063; 70.86614" Location="1866.142; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand47[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox529" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail62" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox530" Size="1370.079; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox531" Size="614.1732; 59.05512" Location="1547.244; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox532" Size="200.7874; 59.05512" Location="2161.417; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox533" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox534" Size="755.9055; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox535" Size="814.9606; 59.05512" Location="1547.244; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox536" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand47[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox537" Size="755.9055; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand47[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox538" Size="814.9606; 59.05512" Location="1547.244; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand47[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox539" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox540" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox541" Size="1311.024; 129.9213" Location="1051.181; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox542" Size="874.0157; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (04/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand48" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header29" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox543" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail63" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox544" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox545" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand48[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox546" Size="1311.024; 59.05512" Location="1051.181; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand48[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox547" Size="425.1968; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand49" DataSource="dataSet.Datos4" Size="2480.315; 437.0079" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header30" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox548" Size="118.1102; 118.1102" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText67" Size="330.7087; 118.1102" Location="295.2756; 0" Text="Lectura&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText68" Size="425.1968; 118.1102" Location="625.9843; 0" Text="Lectura Actual - &lt;br&gt;Lectura Anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox549" Size="496.063; 118.1102" Location="1051.181; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox550" Size="814.9606; 118.1102" Location="1547.244; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail64" Location="0; 212.5984">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox551" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand49[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox552" Size="330.7087; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand49[&quot;lectura&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox553" Size="425.1968; 70.86614" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand49[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox554" Size="496.063; 70.86614" Location="1051.181; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand49[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox555" Size="814.9606; 70.86614" Location="1547.244; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand49[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer13" Location="0; 330.7087">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox556" Size="2185.039; 59.05512" Location="177.1654; 0" Text="(CONTINUACIÓN DEL MES EN HOJA SIGUIENTE)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="Abril2" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader19" Location="0; 165.3543">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture19" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape16" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText69" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText70" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES MEDIANTE &lt;br&gt;&#xD;&#xA;TUBERÍA A PRESIÓN (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand50" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6299">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail65" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox557" Size="767.7166; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox558" Size="602.3622; 141.7323" Location="944.8819; 0" Text="Catálogo de Aguas Privadas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox559" Size="318.8976; 70.86614" Location="1547.244; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox560" Size="318.8976; 70.86614" Location="1547.244; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox561" Size="496.063; 70.86614" Location="1866.142; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand50[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox562" Size="496.063; 70.86614" Location="1866.142; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand50[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox563" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail66" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox564" Size="1370.079; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox565" Size="614.1732; 59.05512" Location="1547.244; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox566" Size="200.7874; 59.05512" Location="2161.417; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox567" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox568" Size="755.9055; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox569" Size="814.9606; 59.05512" Location="1547.244; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox570" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand50[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox571" Size="755.9055; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand50[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox572" Size="814.9606; 59.05512" Location="1547.244; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand50[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox573" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox574" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox575" Size="1311.024; 129.9213" Location="1051.181; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox576" Size="874.0157; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (04/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand51" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header31" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox577" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail67" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox578" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox579" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand51[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox580" Size="1311.024; 59.05512" Location="1051.181; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand51[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox581" Size="425.1968; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand52" DataSource="dataSet.Datos4_2" Size="2480.315; 437.0079" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header32" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox582" Size="118.1102; 118.1102" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText71" Size="330.7087; 118.1102" Location="295.2756; 0" Text="Lectura&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText72" Size="425.1968; 118.1102" Location="625.9843; 0" Text="Lectura Actual - &lt;br&gt;Lectura Anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox583" Size="496.063; 118.1102" Location="1051.181; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox584" Size="814.9606; 118.1102" Location="1547.244; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail68" Location="0; 212.5984">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox585" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand52[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox586" Size="330.7087; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand52[&quot;lectura&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox587" Size="425.1968; 70.86614" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand52[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox588" Size="496.063; 70.86614" Location="1051.181; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand52[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox589" Size="814.9606; 70.86614" Location="1547.244; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand52[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer14" Location="0; 330.7087">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox590" Size="448.8189; 59.05512" Location="177.1654; 0" Text="VOLUMEN MES (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" Name="textBox591" Size="425.1968; 59.05512" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand52[&quot;volumen&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="Mayo" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader20" Location="0; 165.3543">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture20" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape17" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText73" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText74" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES MEDIANTE &lt;br&gt;&#xD;&#xA;TUBERÍA A PRESIÓN (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand53" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6299">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail69" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox592" Size="767.7166; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox593" Size="602.3622; 141.7323" Location="944.8819; 0" Text="Catálogo de Aguas Privadas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox594" Size="318.8976; 70.86614" Location="1547.244; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox595" Size="318.8976; 70.86614" Location="1547.244; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox596" Size="496.063; 70.86614" Location="1866.142; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand53[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox597" Size="496.063; 70.86614" Location="1866.142; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand53[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox598" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail70" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox599" Size="1370.079; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox600" Size="614.1732; 59.05512" Location="1547.244; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox601" Size="200.7874; 59.05512" Location="2161.417; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox602" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox603" Size="755.9055; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox604" Size="814.9606; 59.05512" Location="1547.244; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox605" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand53[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox606" Size="755.9055; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand53[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox607" Size="814.9606; 59.05512" Location="1547.244; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand53[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox608" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox609" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox610" Size="1311.024; 129.9213" Location="1051.181; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox611" Size="874.0157; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (05/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand54" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header33" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox612" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail71" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox613" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox614" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand54[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox615" Size="1311.024; 59.05512" Location="1051.181; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand54[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox616" Size="425.1968; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand55" DataSource="dataSet.Datos5" Size="2480.315; 437.0079" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header34" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox617" Size="118.1102; 118.1102" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText75" Size="330.7087; 118.1102" Location="295.2756; 0" Text="Lectura&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText76" Size="425.1968; 118.1102" Location="625.9843; 0" Text="Lectura Actual - &lt;br&gt;Lectura Anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox618" Size="496.063; 118.1102" Location="1051.181; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox619" Size="814.9606; 118.1102" Location="1547.244; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail72" Location="0; 212.5984">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox620" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand55[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox621" Size="330.7087; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand55[&quot;lectura&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox622" Size="425.1968; 70.86614" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand55[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox623" Size="496.063; 70.86614" Location="1051.181; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand55[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox624" Size="814.9606; 70.86614" Location="1547.244; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand55[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer15" Location="0; 330.7087">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox625" Size="2185.039; 59.05512" Location="177.1654; 0" Text="(CONTINUACIÓN DEL MES EN HOJA SIGUIENTE)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="Mayo2" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader21" Location="0; 165.3543">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture21" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape18" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText77" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText78" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES MEDIANTE &lt;br&gt;&#xD;&#xA;TUBERÍA A PRESIÓN (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand56" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6299">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail73" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox626" Size="767.7166; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox627" Size="602.3622; 141.7323" Location="944.8819; 0" Text="Catálogo de Aguas Privadas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox628" Size="318.8976; 70.86614" Location="1547.244; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox629" Size="318.8976; 70.86614" Location="1547.244; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox630" Size="496.063; 70.86614" Location="1866.142; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand56[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox631" Size="496.063; 70.86614" Location="1866.142; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand56[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox632" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail74" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox633" Size="1370.079; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox634" Size="614.1732; 59.05512" Location="1547.244; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox635" Size="200.7874; 59.05512" Location="2161.417; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox636" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox637" Size="755.9055; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox638" Size="814.9606; 59.05512" Location="1547.244; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox639" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand56[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox640" Size="755.9055; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand56[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox641" Size="814.9606; 59.05512" Location="1547.244; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand56[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox642" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox643" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox644" Size="1311.024; 129.9213" Location="1051.181; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox645" Size="874.0157; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (05/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand57" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header35" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox646" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail75" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox647" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox648" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand57[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox649" Size="1311.024; 59.05512" Location="1051.181; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand57[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox650" Size="425.1968; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand58" DataSource="dataSet.Datos5_2" Size="2480.315; 437.0079" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header36" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox651" Size="118.1102; 118.1102" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText79" Size="330.7087; 118.1102" Location="295.2756; 0" Text="Lectura&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText80" Size="425.1968; 118.1102" Location="625.9843; 0" Text="Lectura Actual - &lt;br&gt;Lectura Anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox652" Size="496.063; 118.1102" Location="1051.181; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox653" Size="814.9606; 118.1102" Location="1547.244; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail76" Location="0; 212.5984">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox654" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand58[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox655" Size="330.7087; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand58[&quot;lectura&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox656" Size="425.1968; 70.86614" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand58[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox657" Size="496.063; 70.86614" Location="1051.181; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand58[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox658" Size="814.9606; 70.86614" Location="1547.244; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand58[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer16" Location="0; 330.7087">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox659" Size="448.8189; 59.05512" Location="177.1654; 0" Text="VOLUMEN MES (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" Name="textBox660" Size="425.1968; 59.05512" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand58[&quot;volumen&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="Junio" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader22" Location="0; 165.3543">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture22" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">/9j/4AAQSkZJRgABAQEAYABgAAD/2wBDAAgGBgcGBQgHBwcJCQgKDBQNDAsLDBkSEw8UHRofHh0aHBwgJC4nICIsIxwcKDcpLDAxNDQ0Hyc5PTgyPC4zNDL/2wBDAQkJCQwLDBgNDRgyIRwhMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjL/wAARCABLAEwDASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwD3yiiigBrusS75GVUHUlsCszUvEOl6VAJbm7jwxwqowZiR1wM1yGpJaaz4z1Gz8QSTyW1rs+wWIyiSkx5Yk9zz6iseS0sPC/jezv8AS9AURSxmORV3MLZhwZcY7A4PP5UAaXirxLZ6tJpsenzyB4phLIm4IUPG3OTweuD2rpY/HmhSXcUK3PEjFRNx5eQM/ezXKeJdQs76WKWZ7RpJrV7dZYf49+Cuefu8dASfSp/E/iC3HhqXT0tLC7SSHbDbJwV2ck4BzhcegoA9DS6t5H2xzxM/TCuCeKmryyy8LeF7Pw5El7BJbTW8SGTUbZnDGRec465z/s11ngPUr7U/C0U2oTSTXCTSR+dIgUyBWwGwOPagDp6KKKAIbm5gs4GmnkWOJcZY+5wP1rItPF+h399JZw3jCZJFj2yRPGCxAIwWABzuFSeJ43k8PXflqshRN5jP/LQKc46Hk44rzjw1pYfV7OSx0e7tjcOH+03TXDJ8vzZ/eggtgYFAHoXiHw8uqxPcWnlw6moASYrkEDPB9uTXnB03XIHmtrpbK3nE4KySzqdiqPlwuTIRk8Y7dRmvV3UzarJEZHEawKwCOV5LMCePpXK6pf8AheTXLeOaGW5mO0G9inYCLJIALBgf4DQBxEPhXVRDj7TYcyecwjtrxl34HH3OnH196m/sTVYLuFjd6ZLCJBJNHIs0bFtxOAZFC4G44yfrmvTprK28m0mtrm7Mc0yYZbyXDKef73euWv8AWPCk9pdTvBq2oJBN5TRLdSyHcf4hG0n3c8Z20AUfDXhzxBeXIe+jtorASOS24MXAckKgX5cHPJx/DxXojy6foOmruaKzs4htUHgL1OBXJeGtdjNnYabPYz2t1N5qottLmEKgyX+/lVG4DnvV7xRFc3XhK1mWFrraI3liGfMkJAAKlRndk9qANTSvE+ka0xWxu9zh2QLJE8ZJUkHAYDPQ9K2a8t8GW1xB4mjs/Kv4jGhuG+3b/lUlhhQyAZLE89fvV6lQBi+I9TtbHT2t5nbzrkFYY0GWcjGf8muPtfG4sNP0yP8AsPUZRaMiOVMIyzoQoAMmec1seOdAutRS3vbKZUMJAulLFfNhBzjj0Ncve2WhaV4d0u403S7CXUGcPOO5Xa2dxByO1AHb6HrcXiKW4uLaKe2JtYxiVRuQkuR0PauK+x6i8V3LHYRsEceUPvfaSMtxn+7k5J4+brXQ3Nxo3huyuJJVm063u7OPdIPMkWJmLADOfl6+1ZtzqulQPeSS6oUOmopmU6YwWBQTtB54HzUAb66nY6VoOhrf30MLSPG0fmP2OSAM88A4zXF+LdKv4b59Umn0y0LXI+ymFSWuk64Izt6Hkn+7XnnhpGuba61zxDoya4dUspfstzcuz4lVyiRKNpw/ynCg5xtxXT/Da0gTUb/w5r1nd3C6YsVxZxjfIbdZU3NGSACOSpx60Adh4ISS7ubG4tLKZYLe1uYrq6kkUq8kjo4Cgnd2/ugAcVak8dW/9n2tlb6VfzuoiJI2KPkw5+8w7Crnh+2t9AbU2X7bP9qmaRf9Dddg5O0gccE9gKztUj0rSYNJQaTbbLhYpJpvIDuVDLvGTz0PNAElr4usLnxm93PHc2kYtBCfNj3DcryZ5QkY5616CMkcDI9a8x1nw1Z6v4l8jQvs1sktrG7y2rbEwXk3HC8Enamf92vR4bWC2t4oI48RxoEQDoFHAoA434ipqE0WmW1vJepY3MxhuWtOqZGQzf7PGD6VF4V8KadYW2k3h8+6nuGLM124k2/IxwoxwMgGun8R3sVnoswlljjMuIkaRwoyTjkkgYHfmuX03WTYNpVvca1ot1bW77D9ndVk5UqD/rWzyegFADfiv/yKmq/9cYP/AEaa5nxP/qPiN/17L/6EK6D4sSlPDuoRtHhJIISkm7glZTkfgGBrn/E/+o+I/wD17L/6EKAOcsvCOsxarN4f0RNMu9Mtb2G9t01TcxhkIGcbRyhP3l77av2OnR6Kl3cyXNlrGuakZXumkViUYOwJQf3SAMZrptEuRbeN78n+IwfebGFGzJ59M1Je2KQeH9RsCRLe207zRWt06hyGfmXI+8Np4xQBH4eFs/iKwutOm04Ru8qzRRuxIkKEnGfUZx7V3dzbxXPhi1iljVkKQDntkqOPzrnls4E1nT3SePyNOjeWcW7LtgYpyrkct83TvWvrN7/Zvh6yhaW3guZPKVTdOqohXBOckZxjpmgDjdf0a50jWXh0a7v/ALR9nRkIf95I7vINpcDhflGOK9VTOxd3LY5NcLYasv8AwlkN7d6xpFx58ItcW0qL5ZG5gSPMYnJOK7ugDlfGPhu/8RNYrZXsNqsDOXeRGY8gAY2kH9a5L/hV2vojf8Ty2kQnLRCAIJM9c8Efmpr1nAowKAMbXvD1h4k0ZtL1CNmgO0hlbDIw6MDjqK5DUvBV/cPrcVzeq0OrxhJp4rVmKAEHhQ2cmvSMCjAoA881DwncQ3H2vS7S3vZruRY5JbtSGiQhQWAPTG3IwP4q1L3RRe6f5Oo2VteXO04hFs7KFPRRJkY49TXYYFGKAOZ0vw75LK88dtBEkglW0s4dibx0ZySSxHUYwPrVXxh4XvvEN5p0llcwW/2ffuklXd1xwAMNzjswrr8CjAoA8pj+GfiC33tHr1oSecGA8nvk554/vBq9UAG0A9QMU7AowKAP/9k=</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape19" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText81" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText82" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES MEDIANTE &lt;br&gt;&#xD;&#xA;TUBERÍA A PRESIÓN (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand59" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6299">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail77" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox661" Size="767.7166; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox662" Size="602.3622; 141.7323" Location="944.8819; 0" Text="Catálogo de Aguas Privadas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox663" Size="318.8976; 70.86614" Location="1547.244; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox664" Size="318.8976; 70.86614" Location="1547.244; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox665" Size="496.063; 70.86614" Location="1866.142; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand59[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox666" Size="496.063; 70.86614" Location="1866.142; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand59[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox667" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail78" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox668" Size="1370.079; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox669" Size="614.1732; 59.05512" Location="1547.244; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox670" Size="200.7874; 59.05512" Location="2161.417; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox671" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox672" Size="755.9055; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox673" Size="814.9606; 59.05512" Location="1547.244; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox674" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand59[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox675" Size="755.9055; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand59[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox676" Size="814.9606; 59.05512" Location="1547.244; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand59[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox677" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox678" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox679" Size="1311.024; 129.9213" Location="1051.181; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox680" Size="874.0157; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (06/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand60" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header37" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox681" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail79" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox682" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox683" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand60[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox684" Size="1311.024; 59.05512" Location="1051.181; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand60[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox685" Size="425.1968; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand61" DataSource="dataSet.Datos6" Size="2480.315; 437.0079" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header38" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox686" Size="118.1102; 118.1102" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText83" Size="330.7087; 118.1102" Location="295.2756; 0" Text="Lectura&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText84" Size="425.1968; 118.1102" Location="625.9843; 0" Text="Lectura Actual - &lt;br&gt;Lectura Anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox687" Size="496.063; 118.1102" Location="1051.181; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox688" Size="814.9606; 118.1102" Location="1547.244; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail80" Location="0; 212.5984">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox689" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand61[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox690" Size="330.7087; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand61[&quot;lectura&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox691" Size="425.1968; 70.86614" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand61[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox692" Size="496.063; 70.86614" Location="1051.181; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand61[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox693" Size="814.9606; 70.86614" Location="1547.244; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand61[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer17" Location="0; 330.7087">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox694" Size="2185.039; 59.05512" Location="177.1654; 0" Text="(CONTINUACIÓN DEL MES EN HOJA SIGUIENTE)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="Junio2" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader23" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture23" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">/9j/4AAQSkZJRgABAQEAYABgAAD/2wBDAAgGBgcGBQgHBwcJCQgKDBQNDAsLDBkSEw8UHRofHh0aHBwgJC4nICIsIxwcKDcpLDAxNDQ0Hyc5PTgyPC4zNDL/2wBDAQkJCQwLDBgNDRgyIRwhMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjL/wAARCABLAEwDASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwD3yiiigBrusS75GVUHUlsCszUvEOl6VAJbm7jwxwqowZiR1wM1yGpJaaz4z1Gz8QSTyW1rs+wWIyiSkx5Yk9zz6iseS0sPC/jezv8AS9AURSxmORV3MLZhwZcY7A4PP5UAaXirxLZ6tJpsenzyB4phLIm4IUPG3OTweuD2rpY/HmhSXcUK3PEjFRNx5eQM/ezXKeJdQs76WKWZ7RpJrV7dZYf49+Cuefu8dASfSp/E/iC3HhqXT0tLC7SSHbDbJwV2ck4BzhcegoA9DS6t5H2xzxM/TCuCeKmryyy8LeF7Pw5El7BJbTW8SGTUbZnDGRec465z/s11ngPUr7U/C0U2oTSTXCTSR+dIgUyBWwGwOPagDp6KKKAIbm5gs4GmnkWOJcZY+5wP1rItPF+h399JZw3jCZJFj2yRPGCxAIwWABzuFSeJ43k8PXflqshRN5jP/LQKc46Hk44rzjw1pYfV7OSx0e7tjcOH+03TXDJ8vzZ/eggtgYFAHoXiHw8uqxPcWnlw6moASYrkEDPB9uTXnB03XIHmtrpbK3nE4KySzqdiqPlwuTIRk8Y7dRmvV3UzarJEZHEawKwCOV5LMCePpXK6pf8AheTXLeOaGW5mO0G9inYCLJIALBgf4DQBxEPhXVRDj7TYcyecwjtrxl34HH3OnH196m/sTVYLuFjd6ZLCJBJNHIs0bFtxOAZFC4G44yfrmvTprK28m0mtrm7Mc0yYZbyXDKef73euWv8AWPCk9pdTvBq2oJBN5TRLdSyHcf4hG0n3c8Z20AUfDXhzxBeXIe+jtorASOS24MXAckKgX5cHPJx/DxXojy6foOmruaKzs4htUHgL1OBXJeGtdjNnYabPYz2t1N5qottLmEKgyX+/lVG4DnvV7xRFc3XhK1mWFrraI3liGfMkJAAKlRndk9qANTSvE+ka0xWxu9zh2QLJE8ZJUkHAYDPQ9K2a8t8GW1xB4mjs/Kv4jGhuG+3b/lUlhhQyAZLE89fvV6lQBi+I9TtbHT2t5nbzrkFYY0GWcjGf8muPtfG4sNP0yP8AsPUZRaMiOVMIyzoQoAMmec1seOdAutRS3vbKZUMJAulLFfNhBzjj0Ncve2WhaV4d0u403S7CXUGcPOO5Xa2dxByO1AHb6HrcXiKW4uLaKe2JtYxiVRuQkuR0PauK+x6i8V3LHYRsEceUPvfaSMtxn+7k5J4+brXQ3Nxo3huyuJJVm063u7OPdIPMkWJmLADOfl6+1ZtzqulQPeSS6oUOmopmU6YwWBQTtB54HzUAb66nY6VoOhrf30MLSPG0fmP2OSAM88A4zXF+LdKv4b59Umn0y0LXI+ymFSWuk64Izt6Hkn+7XnnhpGuba61zxDoya4dUspfstzcuz4lVyiRKNpw/ynCg5xtxXT/Da0gTUb/w5r1nd3C6YsVxZxjfIbdZU3NGSACOSpx60Adh4ISS7ubG4tLKZYLe1uYrq6kkUq8kjo4Cgnd2/ugAcVak8dW/9n2tlb6VfzuoiJI2KPkw5+8w7Crnh+2t9AbU2X7bP9qmaRf9Dddg5O0gccE9gKztUj0rSYNJQaTbbLhYpJpvIDuVDLvGTz0PNAElr4usLnxm93PHc2kYtBCfNj3DcryZ5QkY5616CMkcDI9a8x1nw1Z6v4l8jQvs1sktrG7y2rbEwXk3HC8Enamf92vR4bWC2t4oI48RxoEQDoFHAoA434ipqE0WmW1vJepY3MxhuWtOqZGQzf7PGD6VF4V8KadYW2k3h8+6nuGLM124k2/IxwoxwMgGun8R3sVnoswlljjMuIkaRwoyTjkkgYHfmuX03WTYNpVvca1ot1bW77D9ndVk5UqD/rWzyegFADfiv/yKmq/9cYP/AEaa5nxP/qPiN/17L/6EK6D4sSlPDuoRtHhJIISkm7glZTkfgGBrn/E/+o+I/wD17L/6EKAOcsvCOsxarN4f0RNMu9Mtb2G9t01TcxhkIGcbRyhP3l77av2OnR6Kl3cyXNlrGuakZXumkViUYOwJQf3SAMZrptEuRbeN78n+IwfebGFGzJ59M1Je2KQeH9RsCRLe207zRWt06hyGfmXI+8Np4xQBH4eFs/iKwutOm04Ru8qzRRuxIkKEnGfUZx7V3dzbxXPhi1iljVkKQDntkqOPzrnls4E1nT3SePyNOjeWcW7LtgYpyrkct83TvWvrN7/Zvh6yhaW3guZPKVTdOqohXBOckZxjpmgDjdf0a50jWXh0a7v/ALR9nRkIf95I7vINpcDhflGOK9VTOxd3LY5NcLYasv8AwlkN7d6xpFx58ItcW0qL5ZG5gSPMYnJOK7ugDlfGPhu/8RNYrZXsNqsDOXeRGY8gAY2kH9a5L/hV2vojf8Ty2kQnLRCAIJM9c8Efmpr1nAowKAMbXvD1h4k0ZtL1CNmgO0hlbDIw6MDjqK5DUvBV/cPrcVzeq0OrxhJp4rVmKAEHhQ2cmvSMCjAoA881DwncQ3H2vS7S3vZruRY5JbtSGiQhQWAPTG3IwP4q1L3RRe6f5Oo2VteXO04hFs7KFPRRJkY49TXYYFGKAOZ0vw75LK88dtBEkglW0s4dibx0ZySSxHUYwPrVXxh4XvvEN5p0llcwW/2ffuklXd1xwAMNzjswrr8CjAoA8pj+GfiC33tHr1oSecGA8nvk554/vBq9UAG0A9QMU7AowKAP/9k=</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape20" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText85" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText86" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES MEDIANTE &lt;br&gt;&#xD;&#xA;TUBERÍA A PRESIÓN (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand62" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail81" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox695" Size="767.7166; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox696" Size="602.3622; 141.7323" Location="944.8819; 0" Text="Catálogo de Aguas Privadas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox697" Size="318.8976; 70.86614" Location="1547.244; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox698" Size="318.8976; 70.86614" Location="1547.244; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox699" Size="496.063; 70.86614" Location="1866.142; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand62[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox700" Size="496.063; 70.86614" Location="1866.142; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand62[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox701" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail82" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox702" Size="1370.079; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox703" Size="614.1732; 59.05512" Location="1547.244; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox704" Size="200.7874; 59.05512" Location="2161.417; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox705" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox706" Size="755.9055; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox707" Size="814.9606; 59.05512" Location="1547.244; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox708" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand62[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox709" Size="755.9055; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand62[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox710" Size="814.9606; 59.05512" Location="1547.244; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand62[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox711" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox712" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox713" Size="1311.024; 129.9213" Location="1051.181; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox714" Size="874.0157; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (06/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand63" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header39" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox715" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail83" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox716" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox717" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand63[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox718" Size="1311.024; 59.05512" Location="1051.181; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand63[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox719" Size="425.1968; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand64" DataSource="dataSet.Datos6_2" Size="2480.315; 437.0079" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header40" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox720" Size="118.1102; 118.1102" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText87" Size="330.7087; 118.1102" Location="295.2756; 0" Text="Lectura&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText88" Size="425.1968; 118.1102" Location="625.9843; 0" Text="Lectura Actual - &lt;br&gt;Lectura Anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox721" Size="496.063; 118.1102" Location="1051.181; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox722" Size="814.9606; 118.1102" Location="1547.244; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail84" Location="0; 212.5984">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox723" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand64[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox724" Size="330.7087; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand64[&quot;lectura&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox725" Size="425.1968; 70.86614" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand64[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox726" Size="496.063; 70.86614" Location="1051.181; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand64[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox727" Size="814.9606; 70.86614" Location="1547.244; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand64[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer18" Location="0; 330.7087">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox728" Size="448.8189; 59.05512" Location="177.1654; 0" Text="VOLUMEN MES (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" Name="textBox729" Size="425.1968; 59.05512" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand64[&quot;volumen&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="Julio" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader24" Location="0; 165.3543">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture24" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">/9j/4AAQSkZJRgABAQEAYABgAAD/2wBDAAgGBgcGBQgHBwcJCQgKDBQNDAsLDBkSEw8UHRofHh0aHBwgJC4nICIsIxwcKDcpLDAxNDQ0Hyc5PTgyPC4zNDL/2wBDAQkJCQwLDBgNDRgyIRwhMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjL/wAARCABLAEwDASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwD3yiiigBrusS75GVUHUlsCszUvEOl6VAJbm7jwxwqowZiR1wM1yGpJaaz4z1Gz8QSTyW1rs+wWIyiSkx5Yk9zz6iseS0sPC/jezv8AS9AURSxmORV3MLZhwZcY7A4PP5UAaXirxLZ6tJpsenzyB4phLIm4IUPG3OTweuD2rpY/HmhSXcUK3PEjFRNx5eQM/ezXKeJdQs76WKWZ7RpJrV7dZYf49+Cuefu8dASfSp/E/iC3HhqXT0tLC7SSHbDbJwV2ck4BzhcegoA9DS6t5H2xzxM/TCuCeKmryyy8LeF7Pw5El7BJbTW8SGTUbZnDGRec465z/s11ngPUr7U/C0U2oTSTXCTSR+dIgUyBWwGwOPagDp6KKKAIbm5gs4GmnkWOJcZY+5wP1rItPF+h399JZw3jCZJFj2yRPGCxAIwWABzuFSeJ43k8PXflqshRN5jP/LQKc46Hk44rzjw1pYfV7OSx0e7tjcOH+03TXDJ8vzZ/eggtgYFAHoXiHw8uqxPcWnlw6moASYrkEDPB9uTXnB03XIHmtrpbK3nE4KySzqdiqPlwuTIRk8Y7dRmvV3UzarJEZHEawKwCOV5LMCePpXK6pf8AheTXLeOaGW5mO0G9inYCLJIALBgf4DQBxEPhXVRDj7TYcyecwjtrxl34HH3OnH196m/sTVYLuFjd6ZLCJBJNHIs0bFtxOAZFC4G44yfrmvTprK28m0mtrm7Mc0yYZbyXDKef73euWv8AWPCk9pdTvBq2oJBN5TRLdSyHcf4hG0n3c8Z20AUfDXhzxBeXIe+jtorASOS24MXAckKgX5cHPJx/DxXojy6foOmruaKzs4htUHgL1OBXJeGtdjNnYabPYz2t1N5qottLmEKgyX+/lVG4DnvV7xRFc3XhK1mWFrraI3liGfMkJAAKlRndk9qANTSvE+ka0xWxu9zh2QLJE8ZJUkHAYDPQ9K2a8t8GW1xB4mjs/Kv4jGhuG+3b/lUlhhQyAZLE89fvV6lQBi+I9TtbHT2t5nbzrkFYY0GWcjGf8muPtfG4sNP0yP8AsPUZRaMiOVMIyzoQoAMmec1seOdAutRS3vbKZUMJAulLFfNhBzjj0Ncve2WhaV4d0u403S7CXUGcPOO5Xa2dxByO1AHb6HrcXiKW4uLaKe2JtYxiVRuQkuR0PauK+x6i8V3LHYRsEceUPvfaSMtxn+7k5J4+brXQ3Nxo3huyuJJVm063u7OPdIPMkWJmLADOfl6+1ZtzqulQPeSS6oUOmopmU6YwWBQTtB54HzUAb66nY6VoOhrf30MLSPG0fmP2OSAM88A4zXF+LdKv4b59Umn0y0LXI+ymFSWuk64Izt6Hkn+7XnnhpGuba61zxDoya4dUspfstzcuz4lVyiRKNpw/ynCg5xtxXT/Da0gTUb/w5r1nd3C6YsVxZxjfIbdZU3NGSACOSpx60Adh4ISS7ubG4tLKZYLe1uYrq6kkUq8kjo4Cgnd2/ugAcVak8dW/9n2tlb6VfzuoiJI2KPkw5+8w7Crnh+2t9AbU2X7bP9qmaRf9Dddg5O0gccE9gKztUj0rSYNJQaTbbLhYpJpvIDuVDLvGTz0PNAElr4usLnxm93PHc2kYtBCfNj3DcryZ5QkY5616CMkcDI9a8x1nw1Z6v4l8jQvs1sktrG7y2rbEwXk3HC8Enamf92vR4bWC2t4oI48RxoEQDoFHAoA434ipqE0WmW1vJepY3MxhuWtOqZGQzf7PGD6VF4V8KadYW2k3h8+6nuGLM124k2/IxwoxwMgGun8R3sVnoswlljjMuIkaRwoyTjkkgYHfmuX03WTYNpVvca1ot1bW77D9ndVk5UqD/rWzyegFADfiv/yKmq/9cYP/AEaa5nxP/qPiN/17L/6EK6D4sSlPDuoRtHhJIISkm7glZTkfgGBrn/E/+o+I/wD17L/6EKAOcsvCOsxarN4f0RNMu9Mtb2G9t01TcxhkIGcbRyhP3l77av2OnR6Kl3cyXNlrGuakZXumkViUYOwJQf3SAMZrptEuRbeN78n+IwfebGFGzJ59M1Je2KQeH9RsCRLe207zRWt06hyGfmXI+8Np4xQBH4eFs/iKwutOm04Ru8qzRRuxIkKEnGfUZx7V3dzbxXPhi1iljVkKQDntkqOPzrnls4E1nT3SePyNOjeWcW7LtgYpyrkct83TvWvrN7/Zvh6yhaW3guZPKVTdOqohXBOckZxjpmgDjdf0a50jWXh0a7v/ALR9nRkIf95I7vINpcDhflGOK9VTOxd3LY5NcLYasv8AwlkN7d6xpFx58ItcW0qL5ZG5gSPMYnJOK7ugDlfGPhu/8RNYrZXsNqsDOXeRGY8gAY2kH9a5L/hV2vojf8Ty2kQnLRCAIJM9c8Efmpr1nAowKAMbXvD1h4k0ZtL1CNmgO0hlbDIw6MDjqK5DUvBV/cPrcVzeq0OrxhJp4rVmKAEHhQ2cmvSMCjAoA881DwncQ3H2vS7S3vZruRY5JbtSGiQhQWAPTG3IwP4q1L3RRe6f5Oo2VteXO04hFs7KFPRRJkY49TXYYFGKAOZ0vw75LK88dtBEkglW0s4dibx0ZySSxHUYwPrVXxh4XvvEN5p0llcwW/2ffuklXd1xwAMNzjswrr8CjAoA8pj+GfiC33tHr1oSecGA8nvk554/vBq9UAG0A9QMU7AowKAP/9k=</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape21" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText89" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText90" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES MEDIANTE &lt;br&gt;&#xD;&#xA;TUBERÍA A PRESIÓN (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand65" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6299">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail85" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox730" Size="767.7166; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox731" Size="602.3622; 141.7323" Location="944.8819; 0" Text="Catálogo de Aguas Privadas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox732" Size="318.8976; 70.86614" Location="1547.244; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox733" Size="318.8976; 70.86614" Location="1547.244; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox734" Size="496.063; 70.86614" Location="1866.142; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand65[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox735" Size="496.063; 70.86614" Location="1866.142; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand65[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox736" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail86" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox737" Size="1370.079; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox738" Size="614.1732; 59.05512" Location="1547.244; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox739" Size="200.7874; 59.05512" Location="2161.417; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox740" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox741" Size="755.9055; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox742" Size="814.9606; 59.05512" Location="1547.244; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox743" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand65[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox744" Size="755.9055; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand65[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox745" Size="814.9606; 59.05512" Location="1547.244; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand65[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox746" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox747" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox748" Size="1311.024; 129.9213" Location="1051.181; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox749" Size="874.0157; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (07/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand66" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header41" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox750" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail87" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox751" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox752" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand66[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox753" Size="1311.024; 59.05512" Location="1051.181; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand66[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox754" Size="425.1968; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand67" DataSource="dataSet.Datos7" Size="2480.315; 437.0079" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header42" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox755" Size="118.1102; 118.1102" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText91" Size="330.7087; 118.1102" Location="295.2756; 0" Text="Lectura&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText92" Size="425.1968; 118.1102" Location="625.9843; 0" Text="Lectura Actual - &lt;br&gt;Lectura Anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox756" Size="496.063; 118.1102" Location="1051.181; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox757" Size="814.9606; 118.1102" Location="1547.244; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail88" Location="0; 212.5984">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox758" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand67[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox759" Size="330.7087; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand67[&quot;lectura&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox760" Size="425.1968; 70.86614" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand67[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox761" Size="496.063; 70.86614" Location="1051.181; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand67[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox762" Size="814.9606; 70.86614" Location="1547.244; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand67[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer19" Location="0; 330.7087">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox763" Size="2185.039; 59.05512" Location="177.1654; 0" Text="(CONTINUACIÓN DEL MES EN HOJA SIGUIENTE)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="Julio2" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader25" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture25" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape22" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText93" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText94" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES MEDIANTE &lt;br&gt;&#xD;&#xA;TUBERÍA A PRESIÓN (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand68" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail89" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox764" Size="767.7166; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox765" Size="602.3622; 141.7323" Location="944.8819; 0" Text="Catálogo de Aguas Privadas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox766" Size="318.8976; 70.86614" Location="1547.244; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox767" Size="318.8976; 70.86614" Location="1547.244; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox768" Size="496.063; 70.86614" Location="1866.142; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand68[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox769" Size="496.063; 70.86614" Location="1866.142; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand68[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox770" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail90" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox771" Size="1370.079; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox772" Size="614.1732; 59.05512" Location="1547.244; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox773" Size="200.7874; 59.05512" Location="2161.417; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox774" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox775" Size="755.9055; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox776" Size="814.9606; 59.05512" Location="1547.244; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox777" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand68[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox778" Size="755.9055; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand68[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox779" Size="814.9606; 59.05512" Location="1547.244; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand68[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox780" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox781" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox782" Size="1311.024; 129.9213" Location="1051.181; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox783" Size="874.0157; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (07/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand69" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header43" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox784" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail91" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox785" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox786" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand69[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox787" Size="1311.024; 59.05512" Location="1051.181; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand69[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox788" Size="425.1968; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand70" DataSource="dataSet.Datos7_2" Size="2480.315; 437.0079" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header44" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox789" Size="118.1102; 118.1102" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText95" Size="330.7087; 118.1102" Location="295.2756; 0" Text="Lectura&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText96" Size="425.1968; 118.1102" Location="625.9843; 0" Text="Lectura Actual - &lt;br&gt;Lectura Anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox790" Size="496.063; 118.1102" Location="1051.181; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox791" Size="814.9606; 118.1102" Location="1547.244; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail92" Location="0; 212.5984">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox792" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand70[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox793" Size="330.7087; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand70[&quot;lectura&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox794" Size="425.1968; 70.86614" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand70[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox795" Size="496.063; 70.86614" Location="1051.181; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand70[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox796" Size="814.9606; 70.86614" Location="1547.244; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand70[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer20" Location="0; 330.7087">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox797" Size="448.8189; 59.05512" Location="177.1654; 0" Text="VOLUMEN MES (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" Name="textBox798" Size="425.1968; 59.05512" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand70[&quot;volumen&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="Agosto" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader26" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture26" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape23" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText97" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText98" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES MEDIANTE &lt;br&gt;&#xD;&#xA;TUBERÍA A PRESIÓN (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand71" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail93" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox799" Size="767.7166; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox800" Size="602.3622; 141.7323" Location="944.8819; 0" Text="Catálogo de Aguas Privadas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox801" Size="318.8976; 70.86614" Location="1547.244; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox802" Size="318.8976; 70.86614" Location="1547.244; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox803" Size="496.063; 70.86614" Location="1866.142; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand71[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox804" Size="496.063; 70.86614" Location="1866.142; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand71[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox805" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail94" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox806" Size="1370.079; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox807" Size="614.1732; 59.05512" Location="1547.244; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox808" Size="200.7874; 59.05512" Location="2161.417; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox809" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox810" Size="755.9055; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox811" Size="814.9606; 59.05512" Location="1547.244; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox812" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand71[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox813" Size="755.9055; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand71[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox814" Size="814.9606; 59.05512" Location="1547.244; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand71[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox815" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox816" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox817" Size="1311.024; 129.9213" Location="1051.181; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox818" Size="874.0157; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (08/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand72" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header45" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox819" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail95" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox820" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox821" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand72[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox822" Size="1311.024; 59.05512" Location="1051.181; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand72[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox823" Size="425.1968; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand73" DataSource="dataSet.Datos8" Size="2480.315; 437.0079" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header46" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox824" Size="118.1102; 118.1102" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText99" Size="330.7087; 118.1102" Location="295.2756; 0" Text="Lectura&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText100" Size="425.1968; 118.1102" Location="625.9843; 0" Text="Lectura Actual - &lt;br&gt;Lectura Anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox825" Size="496.063; 118.1102" Location="1051.181; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox826" Size="814.9606; 118.1102" Location="1547.244; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail96" Location="0; 212.5984">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox827" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand73[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox828" Size="330.7087; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand73[&quot;lectura&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox829" Size="425.1968; 70.86614" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand73[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox830" Size="496.063; 70.86614" Location="1051.181; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand73[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox831" Size="814.9606; 70.86614" Location="1547.244; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand73[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer21" Location="0; 330.7087">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox832" Size="2185.039; 59.05512" Location="177.1654; 0" Text="(CONTINUACIÓN DEL MES EN HOJA SIGUIENTE)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="Agosto2" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader27" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture27" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape24" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText101" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText102" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES MEDIANTE &lt;br&gt;&#xD;&#xA;TUBERÍA A PRESIÓN (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand74" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail97" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox833" Size="767.7166; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox834" Size="602.3622; 141.7323" Location="944.8819; 0" Text="Catálogo de Aguas Privadas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox835" Size="318.8976; 70.86614" Location="1547.244; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox836" Size="318.8976; 70.86614" Location="1547.244; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox837" Size="496.063; 70.86614" Location="1866.142; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand74[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox838" Size="496.063; 70.86614" Location="1866.142; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand74[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox839" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail98" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox840" Size="1370.079; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox841" Size="614.1732; 59.05512" Location="1547.244; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox842" Size="200.7874; 59.05512" Location="2161.417; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox843" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox844" Size="755.9055; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox845" Size="814.9606; 59.05512" Location="1547.244; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox846" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand74[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox847" Size="755.9055; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand74[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox848" Size="814.9606; 59.05512" Location="1547.244; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand74[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox849" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox850" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox851" Size="1311.024; 129.9213" Location="1051.181; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox852" Size="874.0157; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (08/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand75" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header47" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox853" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail99" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox854" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox855" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand75[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox856" Size="1311.024; 59.05512" Location="1051.181; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand75[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox857" Size="425.1968; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand76" DataSource="dataSet.Datos8_2" Size="2480.315; 437.0079" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header48" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox858" Size="118.1102; 118.1102" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText103" Size="330.7087; 118.1102" Location="295.2756; 0" Text="Lectura&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText104" Size="425.1968; 118.1102" Location="625.9843; 0" Text="Lectura Actual - &lt;br&gt;Lectura Anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox859" Size="496.063; 118.1102" Location="1051.181; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox860" Size="814.9606; 118.1102" Location="1547.244; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail100" Location="0; 212.5984">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox861" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand76[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox862" Size="330.7087; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand76[&quot;lectura&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox863" Size="425.1968; 70.86614" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand76[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox864" Size="496.063; 70.86614" Location="1051.181; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand76[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox865" Size="814.9606; 70.86614" Location="1547.244; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand76[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer22" Location="0; 330.7087">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox866" Size="448.8189; 59.05512" Location="177.1654; 0" Text="VOLUMEN MES (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" Name="textBox867" Size="425.1968; 59.05512" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand76[&quot;volumen&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="Septiembre" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader28" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture28" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape25" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText105" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText106" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES MEDIANTE &lt;br&gt;&#xD;&#xA;TUBERÍA A PRESIÓN (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand77" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail101" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox868" Size="767.7166; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox869" Size="602.3622; 141.7323" Location="944.8819; 0" Text="Catálogo de Aguas Privadas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox870" Size="318.8976; 70.86614" Location="1547.244; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox871" Size="318.8976; 70.86614" Location="1547.244; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox872" Size="496.063; 70.86614" Location="1866.142; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand77[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox873" Size="496.063; 70.86614" Location="1866.142; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand77[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox874" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail102" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox875" Size="1370.079; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox876" Size="614.1732; 59.05512" Location="1547.244; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox877" Size="200.7874; 59.05512" Location="2161.417; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox878" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox879" Size="755.9055; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox880" Size="814.9606; 59.05512" Location="1547.244; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox881" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand77[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox882" Size="755.9055; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand77[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox883" Size="814.9606; 59.05512" Location="1547.244; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand77[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox884" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox885" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox886" Size="1311.024; 129.9213" Location="1051.181; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox887" Size="874.0157; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (09/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand78" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header49" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox888" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail103" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox889" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox890" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand78[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox891" Size="1311.024; 59.05512" Location="1051.181; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand78[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox892" Size="425.1968; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand79" DataSource="dataSet.Datos9" Size="2480.315; 437.0079" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header50" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox893" Size="118.1102; 118.1102" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText107" Size="330.7087; 118.1102" Location="295.2756; 0" Text="Lectura&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText108" Size="425.1968; 118.1102" Location="625.9843; 0" Text="Lectura Actual - &lt;br&gt;Lectura Anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox894" Size="496.063; 118.1102" Location="1051.181; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox895" Size="814.9606; 118.1102" Location="1547.244; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail104" Location="0; 212.5984">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox896" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand79[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox897" Size="330.7087; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand79[&quot;lectura&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox898" Size="425.1968; 70.86614" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand79[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox899" Size="496.063; 70.86614" Location="1051.181; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand79[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox900" Size="814.9606; 70.86614" Location="1547.244; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand79[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer23" Location="0; 330.7087">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox901" Size="2185.039; 59.05512" Location="177.1654; 0" Text="(CONTINUACIÓN DEL MES EN HOJA SIGUIENTE)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="Septiembre2" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader29" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture29" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape26" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText109" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText110" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES MEDIANTE &lt;br&gt;&#xD;&#xA;TUBERÍA A PRESIÓN (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand80" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail105" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox902" Size="767.7166; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox903" Size="602.3622; 141.7323" Location="944.8819; 0" Text="Catálogo de Aguas Privadas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox904" Size="318.8976; 70.86614" Location="1547.244; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox905" Size="318.8976; 70.86614" Location="1547.244; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox906" Size="496.063; 70.86614" Location="1866.142; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand80[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox907" Size="496.063; 70.86614" Location="1866.142; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand80[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox908" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail106" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox909" Size="1370.079; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox910" Size="614.1732; 59.05512" Location="1547.244; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox911" Size="200.7874; 59.05512" Location="2161.417; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox912" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox913" Size="755.9055; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox914" Size="814.9606; 59.05512" Location="1547.244; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox915" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand81[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox916" Size="755.9055; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand81[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox917" Size="814.9606; 59.05512" Location="1547.244; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand81[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox918" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox919" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox920" Size="1311.024; 129.9213" Location="1051.181; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox921" Size="874.0157; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (09/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand81" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header51" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox922" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail107" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox923" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox924" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand81[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox925" Size="1311.024; 59.05512" Location="1051.181; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand81[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox926" Size="425.1968; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand82" DataSource="dataSet.Datos9_2" Size="2480.315; 437.0079" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header52" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox927" Size="118.1102; 118.1102" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText111" Size="330.7087; 118.1102" Location="295.2756; 0" Text="Lectura&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText112" Size="425.1968; 118.1102" Location="625.9843; 0" Text="Lectura Actual - &lt;br&gt;Lectura Anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox928" Size="496.063; 118.1102" Location="1051.181; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox929" Size="814.9606; 118.1102" Location="1547.244; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail108" Location="0; 212.5984">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox930" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand82[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox931" Size="330.7087; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand82[&quot;lectura&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox932" Size="425.1968; 70.86614" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand82[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox933" Size="496.063; 70.86614" Location="1051.181; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand82[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox934" Size="814.9606; 70.86614" Location="1547.244; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand82[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer24" Location="0; 330.7087">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox935" Size="448.8189; 59.05512" Location="177.1654; 0" Text="VOLUMEN MES (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" Name="textBox936" Size="425.1968; 59.05512" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand82[&quot;volumenMes&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="Octubre" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader30" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture30" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape27" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText113" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText114" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES MEDIANTE &lt;br&gt;&#xD;&#xA;TUBERÍA A PRESIÓN (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand83" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail109" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox937" Size="767.7166; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox938" Size="602.3622; 141.7323" Location="944.8819; 0" Text="Catálogo de Aguas Privadas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox939" Size="318.8976; 70.86614" Location="1547.244; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox940" Size="318.8976; 70.86614" Location="1547.244; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox941" Size="496.063; 70.86614" Location="1866.142; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand83[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox942" Size="496.063; 70.86614" Location="1866.142; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand83[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox943" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail110" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox944" Size="1370.079; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox945" Size="614.1732; 59.05512" Location="1547.244; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox946" Size="200.7874; 59.05512" Location="2161.417; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox947" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox948" Size="755.9055; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox949" Size="814.9606; 59.05512" Location="1547.244; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox950" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand83[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox951" Size="755.9055; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand83[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox952" Size="814.9606; 59.05512" Location="1547.244; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand83[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox953" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox954" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox955" Size="1311.024; 129.9213" Location="1051.181; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox956" Size="874.0157; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (10/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand84" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header53" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox957" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail111" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox958" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox959" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand84[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox960" Size="1311.024; 59.05512" Location="1051.181; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand84[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox961" Size="425.1968; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand85" DataSource="dataSet.Datos10" Size="2480.315; 437.0079" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header54" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox962" Size="118.1102; 118.1102" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText115" Size="330.7087; 118.1102" Location="295.2756; 0" Text="Lectura&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText116" Size="425.1968; 118.1102" Location="625.9843; 0" Text="Lectura Actual - &lt;br&gt;Lectura Anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox963" Size="496.063; 118.1102" Location="1051.181; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox964" Size="814.9606; 118.1102" Location="1547.244; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail112" Location="0; 212.5984">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox965" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand85[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox966" Size="330.7087; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand85[&quot;lectura&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox967" Size="425.1968; 70.86614" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand85[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox968" Size="496.063; 70.86614" Location="1051.181; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand85[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox969" Size="814.9606; 70.86614" Location="1547.244; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand85[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer25" Location="0; 330.7087">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox970" Size="2185.039; 59.05512" Location="177.1654; 0" Text="(CONTINUACIÓN DEL MES EN HOJA SIGUIENTE)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="Octubre2" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader31" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture31" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape28" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText117" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText118" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES MEDIANTE &lt;br&gt;&#xD;&#xA;TUBERÍA A PRESIÓN (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand86" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail113" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox971" Size="767.7166; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox972" Size="602.3622; 141.7323" Location="944.8819; 0" Text="Catálogo de Aguas Privadas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox973" Size="318.8976; 70.86614" Location="1547.244; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox974" Size="318.8976; 70.86614" Location="1547.244; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox975" Size="496.063; 70.86614" Location="1866.142; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand86[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox976" Size="496.063; 70.86614" Location="1866.142; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand86[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox977" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail114" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox978" Size="1370.079; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox979" Size="614.1732; 59.05512" Location="1547.244; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox980" Size="200.7874; 59.05512" Location="2161.417; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox981" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox982" Size="755.9055; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox983" Size="814.9606; 59.05512" Location="1547.244; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox984" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand86[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox985" Size="755.9055; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand86[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox986" Size="814.9606; 59.05512" Location="1547.244; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand86[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox987" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox988" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox989" Size="1311.024; 129.9213" Location="1051.181; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox990" Size="874.0157; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (10/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand87" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header55" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox991" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail115" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox992" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox993" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand87[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox994" Size="1311.024; 59.05512" Location="1051.181; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand87[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox995" Size="425.1968; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand88" DataSource="dataSet.Datos10_2" Size="2480.315; 437.0079" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header56" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox996" Size="118.1102; 118.1102" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText119" Size="330.7087; 118.1102" Location="295.2756; 0" Text="Lectura&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText120" Size="425.1968; 118.1102" Location="625.9843; 0" Text="Lectura Actual - &lt;br&gt;Lectura Anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox997" Size="496.063; 118.1102" Location="1051.181; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox998" Size="814.9606; 118.1102" Location="1547.244; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail116" Location="0; 212.5984">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox999" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand88[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1000" Size="330.7087; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand88[&quot;lectura&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1001" Size="425.1968; 70.86614" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand88[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1002" Size="496.063; 70.86614" Location="1051.181; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand88[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1003" Size="814.9606; 70.86614" Location="1547.244; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand88[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer26" Location="0; 330.7087">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1004" Size="448.8189; 59.05512" Location="177.1654; 0" Text="VOLUMEN MES (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" Name="textBox1005" Size="425.1968; 59.05512" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand88[&quot;volumenMes&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="Noviembre" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader32" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture32" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape29" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText121" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText122" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES MEDIANTE &lt;br&gt;&#xD;&#xA;TUBERÍA A PRESIÓN (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand89" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail117" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1006" Size="767.7166; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1007" Size="602.3622; 141.7323" Location="944.8819; 0" Text="Catálogo de Aguas Privadas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1008" Size="318.8976; 70.86614" Location="1547.244; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1009" Size="318.8976; 70.86614" Location="1547.244; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1010" Size="496.063; 70.86614" Location="1866.142; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand89[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1011" Size="496.063; 70.86614" Location="1866.142; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand89[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1012" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail118" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1013" Size="1370.079; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1014" Size="614.1732; 59.05512" Location="1547.244; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1015" Size="200.7874; 59.05512" Location="2161.417; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1016" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1017" Size="755.9055; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1018" Size="814.9606; 59.05512" Location="1547.244; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1019" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand89[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1020" Size="755.9055; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand89[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1021" Size="814.9606; 59.05512" Location="1547.244; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand89[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1022" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1023" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1024" Size="1311.024; 129.9213" Location="1051.181; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1025" Size="874.0157; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (11/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand90" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header57" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1026" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail119" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1027" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1028" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand90[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1029" Size="1311.024; 59.05512" Location="1051.181; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand90[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1030" Size="425.1968; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand91" DataSource="dataSet.Datos11" Size="2480.315; 437.0079" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header58" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1031" Size="118.1102; 118.1102" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText123" Size="330.7087; 118.1102" Location="295.2756; 0" Text="Lectura&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText124" Size="425.1968; 118.1102" Location="625.9843; 0" Text="Lectura Actual - &lt;br&gt;Lectura Anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1032" Size="496.063; 118.1102" Location="1051.181; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1033" Size="814.9606; 118.1102" Location="1547.244; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail120" Location="0; 212.5984">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1034" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand91[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1035" Size="330.7087; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand91[&quot;lectura&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1036" Size="425.1968; 70.86614" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand91[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1037" Size="496.063; 70.86614" Location="1051.181; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand91[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1038" Size="814.9606; 70.86614" Location="1547.244; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand91[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer27" Location="0; 330.7087">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1039" Size="2185.039; 59.05512" Location="177.1654; 0" Text="(CONTINUACIÓN DEL MES EN HOJA SIGUIENTE)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="Noviembre2" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader33" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture33" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">/9j/4AAQSkZJRgABAQEAYABgAAD/2wBDAAgGBgcGBQgHBwcJCQgKDBQNDAsLDBkSEw8UHRofHh0aHBwgJC4nICIsIxwcKDcpLDAxNDQ0Hyc5PTgyPC4zNDL/2wBDAQkJCQwLDBgNDRgyIRwhMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjL/wAARCABLAEwDASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwD3yiiigBrusS75GVUHUlsCszUvEOl6VAJbm7jwxwqowZiR1wM1yGpJaaz4z1Gz8QSTyW1rs+wWIyiSkx5Yk9zz6iseS0sPC/jezv8AS9AURSxmORV3MLZhwZcY7A4PP5UAaXirxLZ6tJpsenzyB4phLIm4IUPG3OTweuD2rpY/HmhSXcUK3PEjFRNx5eQM/ezXKeJdQs76WKWZ7RpJrV7dZYf49+Cuefu8dASfSp/E/iC3HhqXT0tLC7SSHbDbJwV2ck4BzhcegoA9DS6t5H2xzxM/TCuCeKmryyy8LeF7Pw5El7BJbTW8SGTUbZnDGRec465z/s11ngPUr7U/C0U2oTSTXCTSR+dIgUyBWwGwOPagDp6KKKAIbm5gs4GmnkWOJcZY+5wP1rItPF+h399JZw3jCZJFj2yRPGCxAIwWABzuFSeJ43k8PXflqshRN5jP/LQKc46Hk44rzjw1pYfV7OSx0e7tjcOH+03TXDJ8vzZ/eggtgYFAHoXiHw8uqxPcWnlw6moASYrkEDPB9uTXnB03XIHmtrpbK3nE4KySzqdiqPlwuTIRk8Y7dRmvV3UzarJEZHEawKwCOV5LMCePpXK6pf8AheTXLeOaGW5mO0G9inYCLJIALBgf4DQBxEPhXVRDj7TYcyecwjtrxl34HH3OnH196m/sTVYLuFjd6ZLCJBJNHIs0bFtxOAZFC4G44yfrmvTprK28m0mtrm7Mc0yYZbyXDKef73euWv8AWPCk9pdTvBq2oJBN5TRLdSyHcf4hG0n3c8Z20AUfDXhzxBeXIe+jtorASOS24MXAckKgX5cHPJx/DxXojy6foOmruaKzs4htUHgL1OBXJeGtdjNnYabPYz2t1N5qottLmEKgyX+/lVG4DnvV7xRFc3XhK1mWFrraI3liGfMkJAAKlRndk9qANTSvE+ka0xWxu9zh2QLJE8ZJUkHAYDPQ9K2a8t8GW1xB4mjs/Kv4jGhuG+3b/lUlhhQyAZLE89fvV6lQBi+I9TtbHT2t5nbzrkFYY0GWcjGf8muPtfG4sNP0yP8AsPUZRaMiOVMIyzoQoAMmec1seOdAutRS3vbKZUMJAulLFfNhBzjj0Ncve2WhaV4d0u403S7CXUGcPOO5Xa2dxByO1AHb6HrcXiKW4uLaKe2JtYxiVRuQkuR0PauK+x6i8V3LHYRsEceUPvfaSMtxn+7k5J4+brXQ3Nxo3huyuJJVm063u7OPdIPMkWJmLADOfl6+1ZtzqulQPeSS6oUOmopmU6YwWBQTtB54HzUAb66nY6VoOhrf30MLSPG0fmP2OSAM88A4zXF+LdKv4b59Umn0y0LXI+ymFSWuk64Izt6Hkn+7XnnhpGuba61zxDoya4dUspfstzcuz4lVyiRKNpw/ynCg5xtxXT/Da0gTUb/w5r1nd3C6YsVxZxjfIbdZU3NGSACOSpx60Adh4ISS7ubG4tLKZYLe1uYrq6kkUq8kjo4Cgnd2/ugAcVak8dW/9n2tlb6VfzuoiJI2KPkw5+8w7Crnh+2t9AbU2X7bP9qmaRf9Dddg5O0gccE9gKztUj0rSYNJQaTbbLhYpJpvIDuVDLvGTz0PNAElr4usLnxm93PHc2kYtBCfNj3DcryZ5QkY5616CMkcDI9a8x1nw1Z6v4l8jQvs1sktrG7y2rbEwXk3HC8Enamf92vR4bWC2t4oI48RxoEQDoFHAoA434ipqE0WmW1vJepY3MxhuWtOqZGQzf7PGD6VF4V8KadYW2k3h8+6nuGLM124k2/IxwoxwMgGun8R3sVnoswlljjMuIkaRwoyTjkkgYHfmuX03WTYNpVvca1ot1bW77D9ndVk5UqD/rWzyegFADfiv/yKmq/9cYP/AEaa5nxP/qPiN/17L/6EK6D4sSlPDuoRtHhJIISkm7glZTkfgGBrn/E/+o+I/wD17L/6EKAOcsvCOsxarN4f0RNMu9Mtb2G9t01TcxhkIGcbRyhP3l77av2OnR6Kl3cyXNlrGuakZXumkViUYOwJQf3SAMZrptEuRbeN78n+IwfebGFGzJ59M1Je2KQeH9RsCRLe207zRWt06hyGfmXI+8Np4xQBH4eFs/iKwutOm04Ru8qzRRuxIkKEnGfUZx7V3dzbxXPhi1iljVkKQDntkqOPzrnls4E1nT3SePyNOjeWcW7LtgYpyrkct83TvWvrN7/Zvh6yhaW3guZPKVTdOqohXBOckZxjpmgDjdf0a50jWXh0a7v/ALR9nRkIf95I7vINpcDhflGOK9VTOxd3LY5NcLYasv8AwlkN7d6xpFx58ItcW0qL5ZG5gSPMYnJOK7ugDlfGPhu/8RNYrZXsNqsDOXeRGY8gAY2kH9a5L/hV2vojf8Ty2kQnLRCAIJM9c8Efmpr1nAowKAMbXvD1h4k0ZtL1CNmgO0hlbDIw6MDjqK5DUvBV/cPrcVzeq0OrxhJp4rVmKAEHhQ2cmvSMCjAoA881DwncQ3H2vS7S3vZruRY5JbtSGiQhQWAPTG3IwP4q1L3RRe6f5Oo2VteXO04hFs7KFPRRJkY49TXYYFGKAOZ0vw75LK88dtBEkglW0s4dibx0ZySSxHUYwPrVXxh4XvvEN5p0llcwW/2ffuklXd1xwAMNzjswrr8CjAoA8pj+GfiC33tHr1oSecGA8nvk554/vBq9UAG0A9QMU7AowKAP/9k=</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape30" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText125" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText126" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES MEDIANTE &lt;br&gt;&#xD;&#xA;TUBERÍA A PRESIÓN (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand92" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail121" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1040" Size="767.7166; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1041" Size="602.3622; 141.7323" Location="944.8819; 0" Text="Catálogo de Aguas Privadas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1042" Size="318.8976; 70.86614" Location="1547.244; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1043" Size="318.8976; 70.86614" Location="1547.244; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1044" Size="496.063; 70.86614" Location="1866.142; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand92[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1045" Size="496.063; 70.86614" Location="1866.142; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand92[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1046" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail122" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1047" Size="1370.079; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1048" Size="614.1732; 59.05512" Location="1547.244; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1049" Size="200.7874; 59.05512" Location="2161.417; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1050" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1051" Size="755.9055; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1052" Size="814.9606; 59.05512" Location="1547.244; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1053" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand92[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1054" Size="755.9055; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand92[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1055" Size="814.9606; 59.05512" Location="1547.244; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand92[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1056" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1057" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1058" Size="1311.024; 129.9213" Location="1051.181; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1059" Size="874.0157; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (11/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand93" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header59" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1060" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail123" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1061" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1062" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand93[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1063" Size="1311.024; 59.05512" Location="1051.181; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand93[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1064" Size="425.1968; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand94" DataSource="dataSet.Datos11_2" Size="2480.315; 437.0079" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header60" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1065" Size="118.1102; 118.1102" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText127" Size="330.7087; 118.1102" Location="295.2756; 0" Text="Lectura&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText128" Size="425.1968; 118.1102" Location="625.9843; 0" Text="Lectura Actual - &lt;br&gt;Lectura Anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1066" Size="496.063; 118.1102" Location="1051.181; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1067" Size="814.9606; 118.1102" Location="1547.244; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail124" Location="0; 212.5984">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1068" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand94[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1069" Size="330.7087; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand94[&quot;lectura&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1070" Size="425.1968; 70.86614" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand94[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1071" Size="496.063; 70.86614" Location="1051.181; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand94[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1072" Size="814.9606; 70.86614" Location="1547.244; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand94[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer28" Location="0; 330.7087">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1073" Size="448.8189; 59.05512" Location="177.1654; 0" Text="VOLUMEN MES (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" Name="textBox1074" Size="425.1968; 59.05512" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand94[&quot;volumenMes&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="Diciembre" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader34" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture34" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape31" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText129" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText130" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES MEDIANTE &lt;br&gt;&#xD;&#xA;TUBERÍA A PRESIÓN (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand95" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail125" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1075" Size="767.7166; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1076" Size="602.3622; 141.7323" Location="944.8819; 0" Text="Catálogo de Aguas Privadas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1077" Size="318.8976; 70.86614" Location="1547.244; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1078" Size="318.8976; 70.86614" Location="1547.244; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1079" Size="496.063; 70.86614" Location="1866.142; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand95[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1080" Size="496.063; 70.86614" Location="1866.142; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand95[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1081" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail126" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1082" Size="1370.079; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1083" Size="614.1732; 59.05512" Location="1547.244; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1084" Size="200.7874; 59.05512" Location="2161.417; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1085" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1086" Size="755.9055; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1087" Size="814.9606; 59.05512" Location="1547.244; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1088" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand95[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1089" Size="755.9055; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand95[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1090" Size="814.9606; 59.05512" Location="1547.244; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand95[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1091" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1092" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1093" Size="1311.024; 129.9213" Location="1051.181; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1094" Size="874.0157; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (12/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand96" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header61" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1095" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail127" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1096" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1097" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand96[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1098" Size="1311.024; 59.05512" Location="1051.181; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand96[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1099" Size="425.1968; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand97" DataSource="dataSet.Datos12" Size="2480.315; 437.0079" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header62" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1100" Size="118.1102; 118.1102" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText131" Size="330.7087; 118.1102" Location="295.2756; 0" Text="Lectura&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText132" Size="425.1968; 118.1102" Location="625.9843; 0" Text="Lectura Actual - &lt;br&gt;Lectura Anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1101" Size="496.063; 118.1102" Location="1051.181; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1102" Size="814.9606; 118.1102" Location="1547.244; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail128" Location="0; 212.5984">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1103" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand97[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1104" Size="330.7087; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand97[&quot;lectura&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1105" Size="425.1968; 70.86614" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand97[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1106" Size="496.063; 70.86614" Location="1051.181; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand97[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1107" Size="814.9606; 70.86614" Location="1547.244; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand97[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer29" Location="0; 330.7087">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1108" Size="2185.039; 59.05512" Location="177.1654; 0" Text="(CONTINUACIÓN DEL MES EN HOJA SIGUIENTE)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
      </Controls>
    </item>
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="Diciembre2" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader35" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture35" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape32" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText133" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText134" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES MEDIANTE &lt;br&gt;&#xD;&#xA;TUBERÍA A PRESIÓN (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand98" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail129" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1109" Size="767.7166; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1110" Size="602.3622; 141.7323" Location="944.8819; 0" Text="Catálogo de Aguas Privadas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1111" Size="318.8976; 70.86614" Location="1547.244; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1112" Size="318.8976; 70.86614" Location="1547.244; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1113" Size="496.063; 70.86614" Location="1866.142; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand98[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1114" Size="496.063; 70.86614" Location="1866.142; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand98[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1115" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail130" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1116" Size="1370.079; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1117" Size="614.1732; 59.05512" Location="1547.244; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1118" Size="200.7874; 59.05512" Location="2161.417; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1119" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1120" Size="755.9055; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1121" Size="814.9606; 59.05512" Location="1547.244; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1122" Size="614.1732; 59.05512" Location="177.1654; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand98[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1123" Size="755.9055; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand98[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1124" Size="814.9606; 59.05512" Location="1547.244; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand98[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1125" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1126" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1127" Size="1311.024; 129.9213" Location="1051.181; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1128" Size="874.0157; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (12/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand99" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header63" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1129" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail131" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1130" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1131" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand99[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1132" Size="1311.024; 59.05512" Location="1051.181; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand99[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1133" Size="425.1968; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand100" DataSource="dataSet.Datos12_2" Size="2480.315; 496.063" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header64" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1134" Size="118.1102; 118.1102" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText135" Size="330.7087; 118.1102" Location="295.2756; 0" Text="Lectura&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText136" Size="425.1968; 118.1102" Location="625.9843; 0" Text="Lectura Actual - &lt;br&gt;Lectura Anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1135" Size="496.063; 118.1102" Location="1051.181; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1136" Size="814.9606; 118.1102" Location="1547.244; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail132" Location="0; 212.5984">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1137" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand100[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1138" Size="330.7087; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand100[&quot;lectura&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1139" Size="425.1968; 70.86614" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand100[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1140" Size="496.063; 70.86614" Location="1051.181; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand100[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1141" Size="814.9606; 70.86614" Location="1547.244; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand100[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 118.1102" Name="footer30" Location="0; 330.7087">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1142" Size="448.8189; 59.05512" Location="177.1654; 0" Text="VOLUMEN MES (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" Name="textBox1143" Size="425.1968; 59.05512" Location="625.9843; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand100[&quot;volumenMes&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1144" Size="448.8189; 59.05512" Location="177.1653; 59.05512" Text="VOLUMEN AÑO (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" Name="textBox1145" Size="425.1968; 59.05512" Location="625.9843; 59.05512">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand100[&quot;volumenAnyo&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
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
      <item type="NineRays.Reporting.DOM.Style" Name="Cabecera" Font="Arial, 11pt">
        <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
        <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
      </item>
      <item type="NineRays.Reporting.DOM.Style" Name="Titulo" Font="Arial, 9pt, style=Bold">
        <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
        <Fill type="NineRays.Basics.Drawing.SolidFill" Color="204, 204, 204" />
      </item>
      <item type="NineRays.Reporting.DOM.Style" Name="Nocturno" Font="Arial, 8.25pt">
        <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
        <Fill type="NineRays.Basics.Drawing.SolidFill" Color="136, 136, 136" />
      </item>
      <item type="NineRays.Reporting.DOM.Style" Name="Diurno" Font="Arial, 8.25pt">
        <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
      </item>
      <item type="NineRays.Reporting.DOM.Style" Name="Vacio" Font="Arial, 12pt">
        <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
      </item>
      <item type="NineRays.Reporting.DOM.Style" Name="Negrita" Font="Arial, 8.25pt, style=Bold">
        <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
      </item>
      <item type="NineRays.Reporting.DOM.Style" Name="Subrayado" Font="Arial, 8.25pt, style=Underline">
        <TextFill type="NineRays.Basics.Drawing.SolidFill" Color="Black" />
      </item>
    </Styles>
  </StyleSheet>
</root>