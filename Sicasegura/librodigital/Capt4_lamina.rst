<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<root type="NineRays.Reporting.DOM.Document" Name="Capt4_lamina" GridStep="11.8110237" Title="Cuadrante Brigada Anual" IsTemplate="True" DoublePass="True" ImportsString="System.Drawing" Description="Demonstrates how to create simple list report." ScriptLanguage="CSharp">
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
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader36" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture36" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape33" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText137" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText138" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES DE AGUA&lt;br&gt;&#xD;&#xA;EN LÁMINA LIBRE (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand101" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail133" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1146" Size="696.8504; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1147" Size="543.3071; 141.7323" Location="874.0157; 0" Text="Registro de Aguas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1148" Size="318.8976; 70.86614" Location="1417.323; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1149" Size="318.8976; 70.86614" Location="1417.323; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1150" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand101[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1151" Size="625.9843; 70.86614" Location="1736.22; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand101[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1152" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail134" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1153" Size="1240.157; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1154" Size="696.8504; 59.05512" Location="1417.323; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1155" Size="248.0315; 59.05512" Location="2114.173; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1156" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1157" Size="625.9843; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1158" Size="944.8819; 59.05512" Location="1417.323; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1159" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand101[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1160" Size="625.9843; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand101[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1161" Size="944.8819; 59.05512" Location="1417.323; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand101[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1162" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1163" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1164" Size="1570.866; 129.9213" Location="791.3386; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1165" Size="614.1732; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (01/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand102" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header65" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1166" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail135" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1167" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1168" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand102[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1169" Size="1275.591; 59.05512" Location="1086.614; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand102[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1170" Size="460.6299; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand103" DataSource="dataSet.Datos1" Size="2480.315; 484.252" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 165.3543" Name="header66" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1171" Size="118.1102; 165.3543" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText139" Size="295.2756; 165.3543" Location="295.2756; 0" Text="Nivel en la escala&lt;br&gt;( m )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText140" Size="295.2756; 165.3543" Location="791.3386; 0" Text="Volumen utilizado&lt;br&gt;desde la lectura&lt;br&gt;anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1172" Size="649.6063; 165.3543" Location="1086.614; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1173" Size="625.9843; 165.3543" Location="1736.22; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText141" Size="200.7874; 165.3543" Location="590.5512; 0" Text="Caudal&lt;br&gt;estimado o&lt;br&gt;calculado&lt;br&gt;( l/s )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail136" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1174" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand103[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1175" Size="200.7874; 70.86614" Location="590.5512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand103[&quot;caudal_ls&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1176" Size="295.2756; 70.86614" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand103[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1177" Size="649.6063; 70.86614" Location="1086.614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand103[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1178" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand103[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1180" Size="295.2756; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand103[&quot;calado&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer31" Location="0; 377.9528">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1179" Size="2185.039; 59.05512" Location="177.1654; 0" Text="(CONTINUACIÓN DEL MES EN HOJA SIGUIENTE)">
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
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader37" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture37" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape34" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText142" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText143" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES DE AGUA&lt;br&gt;&#xD;&#xA;EN LÁMINA LIBRE (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand104" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail137" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1181" Size="696.8504; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1182" Size="543.3071; 141.7323" Location="874.0157; 0" Text="Registro de Aguas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1183" Size="318.8976; 70.86614" Location="1417.323; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1184" Size="318.8976; 70.86614" Location="1417.323; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1185" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand104[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1186" Size="625.9843; 70.86614" Location="1736.22; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand104[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1187" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail138" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1188" Size="1240.157; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1189" Size="696.8504; 59.05512" Location="1417.323; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1190" Size="248.0315; 59.05512" Location="2114.173; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1191" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1192" Size="625.9843; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1193" Size="944.8819; 59.05512" Location="1417.323; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1194" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand104[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1195" Size="625.9843; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand104[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1196" Size="944.8819; 59.05512" Location="1417.323; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand104[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1197" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1198" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1199" Size="1570.866; 129.9213" Location="791.3386; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1200" Size="614.1732; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (01/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand105" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header67" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1201" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail139" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1202" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1203" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand105[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1204" Size="1275.591; 59.05512" Location="1086.614; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand105[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1205" Size="460.6299; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand106" DataSource="dataSet.Datos1_2" Size="2480.315; 484.252" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 165.3543" Name="header68" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1206" Size="118.1102; 165.3543" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText144" Size="295.2756; 165.3543" Location="295.2756; 0" Text="Nivel en la escala&lt;br&gt;( m )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText145" Size="295.2756; 165.3543" Location="791.3386; 0" Text="Volumen utilizado&lt;br&gt;desde la lectura&lt;br&gt;anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1207" Size="649.6063; 165.3543" Location="1086.614; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1208" Size="625.9843; 165.3543" Location="1736.22; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText146" Size="200.7874; 165.3543" Location="590.5512; 0" Text="Caudal&lt;br&gt;estimado o&lt;br&gt;calculado&lt;br&gt;( l/s )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail140" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1209" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand106[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1210" Size="200.7874; 70.86614" Location="590.5512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand106[&quot;caudal_ls&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1211" Size="295.2756; 70.86614" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand106[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1212" Size="649.6063; 70.86614" Location="1086.614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand106[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1213" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand106[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1214" Size="295.2756; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand106[&quot;calado&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer32" Location="0; 377.9528">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1215" Size="614.1732; 59.05512" Location="177.1654; 0" Text="VOLUMEN MES (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" Name="textBox1216" Size="295.2756; 59.05512" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand106[&quot;volumenMes&quot;]" PropertyName="Value" />
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
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader38" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture38" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape35" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText147" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText148" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES DE AGUA&lt;br&gt;&#xD;&#xA;EN LÁMINA LIBRE (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand107" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail141" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1217" Size="696.8504; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1218" Size="543.3071; 141.7323" Location="874.0157; 0" Text="Registro de Aguas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1219" Size="318.8976; 70.86614" Location="1417.323; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1220" Size="318.8976; 70.86614" Location="1417.323; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1221" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand107[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1222" Size="625.9843; 70.86614" Location="1736.22; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand107[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1223" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail142" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1224" Size="1240.157; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1225" Size="696.8504; 59.05512" Location="1417.323; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1226" Size="248.0315; 59.05512" Location="2114.173; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1227" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1228" Size="625.9843; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1229" Size="944.8819; 59.05512" Location="1417.323; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1230" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand107[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1231" Size="625.9843; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand107[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1232" Size="944.8819; 59.05512" Location="1417.323; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand107[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1233" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1234" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1235" Size="1570.866; 129.9213" Location="791.3386; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1236" Size="614.1732; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (02/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand108" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header69" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1237" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail143" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1238" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1239" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand108[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1240" Size="1275.591; 59.05512" Location="1086.614; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand108[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1241" Size="460.6299; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand109" DataSource="dataSet.Datos2" Size="2480.315; 484.252" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 165.3543" Name="header70" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1242" Size="118.1102; 165.3543" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText149" Size="295.2756; 165.3543" Location="295.2756; 0" Text="Nivel en la escala&lt;br&gt;( m )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText150" Size="295.2756; 165.3543" Location="791.3386; 0" Text="Volumen utilizado&lt;br&gt;desde la lectura&lt;br&gt;anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1243" Size="649.6063; 165.3543" Location="1086.614; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1244" Size="625.9843; 165.3543" Location="1736.22; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText151" Size="200.7874; 165.3543" Location="590.5512; 0" Text="Caudal&lt;br&gt;estimado o&lt;br&gt;calculado&lt;br&gt;( l/s )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail144" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1245" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand109[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1246" Size="200.7874; 70.86614" Location="590.5512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand109[&quot;caudal_ls&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1247" Size="295.2756; 70.86614" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand109[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1248" Size="649.6063; 70.86614" Location="1086.614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand109[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1249" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand109[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1250" Size="295.2756; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand109[&quot;calado&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer33" Location="0; 377.9528">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1251" Size="2185.039; 59.05512" Location="177.1654; 0" Text="(CONTINUACIÓN DEL MES EN HOJA SIGUIENTE)">
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
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader39" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture39" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape36" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText152" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText153" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES DE AGUA&lt;br&gt;&#xD;&#xA;EN LÁMINA LIBRE (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand110" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail145" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1253" Size="696.8504; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1254" Size="543.3071; 141.7323" Location="874.0157; 0" Text="Registro de Aguas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1255" Size="318.8976; 70.86614" Location="1417.323; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1256" Size="318.8976; 70.86614" Location="1417.323; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1257" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand110[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1258" Size="625.9843; 70.86614" Location="1736.22; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand110[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1259" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail146" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1260" Size="1240.157; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1261" Size="696.8504; 59.05512" Location="1417.323; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1262" Size="248.0315; 59.05512" Location="2114.173; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1263" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1264" Size="625.9843; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1265" Size="944.8819; 59.05512" Location="1417.323; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1266" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand110[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1267" Size="625.9843; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand110[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1268" Size="944.8819; 59.05512" Location="1417.323; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand110[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1269" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1270" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1271" Size="1570.866; 129.9213" Location="791.3386; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1272" Size="614.1732; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (02/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand111" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header71" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1273" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail147" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1274" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1275" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand111[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1276" Size="1275.591; 59.05512" Location="1086.614; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand111[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1277" Size="460.6299; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand112" DataSource="dataSet.Datos2_2" Size="2480.315; 484.252" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 165.3543" Name="header72" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1278" Size="118.1102; 165.3543" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText154" Size="295.2756; 165.3543" Location="295.2756; 0" Text="Nivel en la escala&lt;br&gt;( m )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText155" Size="295.2756; 165.3543" Location="791.3386; 0" Text="Volumen utilizado&lt;br&gt;desde la lectura&lt;br&gt;anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1279" Size="649.6063; 165.3543" Location="1086.614; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1280" Size="625.9843; 165.3543" Location="1736.22; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText156" Size="200.7874; 165.3543" Location="590.5512; 0" Text="Caudal&lt;br&gt;estimado o&lt;br&gt;calculado&lt;br&gt;( l/s )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail148" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1281" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand112[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1282" Size="200.7874; 70.86614" Location="590.5512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand112[&quot;caudal_ls&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1283" Size="295.2756; 70.86614" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand112[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1284" Size="649.6063; 70.86614" Location="1086.614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand112[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1285" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand112[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1286" Size="295.2756; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand112[&quot;calado&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer34" Location="0; 377.9528">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1287" Size="614.1732; 59.05512" Location="177.1654; 0" Text="VOLUMEN MES (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" Name="textBox1288" Size="295.2756; 59.05512" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand112[&quot;volumenMes&quot;]" PropertyName="Value" />
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
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader40" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture40" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape37" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText157" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText158" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES DE AGUA&lt;br&gt;&#xD;&#xA;EN LÁMINA LIBRE (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand113" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail149" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1289" Size="696.8504; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1290" Size="543.3071; 141.7323" Location="874.0157; 0" Text="Registro de Aguas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1291" Size="318.8976; 70.86614" Location="1417.323; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1292" Size="318.8976; 70.86614" Location="1417.323; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1293" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand113[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1294" Size="625.9843; 70.86614" Location="1736.22; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand113[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1295" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail150" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1296" Size="1240.157; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1297" Size="696.8504; 59.05512" Location="1417.323; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1298" Size="248.0315; 59.05512" Location="2114.173; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1299" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1300" Size="625.9843; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1301" Size="944.8819; 59.05512" Location="1417.323; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1302" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand113[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1303" Size="625.9843; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand113[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1304" Size="944.8819; 59.05512" Location="1417.323; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand113[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1305" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1306" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1307" Size="1570.866; 129.9213" Location="791.3386; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1308" Size="614.1732; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (03/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand114" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header73" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1309" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail151" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1310" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1311" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand114[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1312" Size="1275.591; 59.05512" Location="1086.614; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand114[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1313" Size="460.6299; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand115" DataSource="dataSet.Datos3" Size="2480.315; 484.252" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 165.3543" Name="header74" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1314" Size="118.1102; 165.3543" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText159" Size="295.2756; 165.3543" Location="295.2756; 0" Text="Nivel en la escala&lt;br&gt;( m )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText160" Size="295.2756; 165.3543" Location="791.3386; 0" Text="Volumen utilizado&lt;br&gt;desde la lectura&lt;br&gt;anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1315" Size="649.6063; 165.3543" Location="1086.614; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1316" Size="625.9843; 165.3543" Location="1736.22; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText161" Size="200.7874; 165.3543" Location="590.5512; 0" Text="Caudal&lt;br&gt;estimado o&lt;br&gt;calculado&lt;br&gt;( l/s )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail152" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1317" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand115[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1318" Size="200.7874; 70.86614" Location="590.5512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand115[&quot;caudal_ls&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1319" Size="295.2756; 70.86614" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand115[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1320" Size="649.6063; 70.86614" Location="1086.614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand115[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1321" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand115[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1322" Size="295.2756; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand115[&quot;calado&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer35" Location="0; 377.9528">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1252" Size="2185.039; 59.05512" Location="177.1654; 0" Text="(CONTINUACIÓN DEL MES EN HOJA SIGUIENTE)">
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
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader41" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture41" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape38" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText162" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText163" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES DE AGUA&lt;br&gt;&#xD;&#xA;EN LÁMINA LIBRE (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand116" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail153" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1325" Size="696.8504; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1326" Size="543.3071; 141.7323" Location="874.0157; 0" Text="Registro de Aguas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1327" Size="318.8976; 70.86614" Location="1417.323; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1328" Size="318.8976; 70.86614" Location="1417.323; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1329" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand116[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1330" Size="625.9843; 70.86614" Location="1736.22; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand116[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1331" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail154" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1332" Size="1240.157; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1333" Size="696.8504; 59.05512" Location="1417.323; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1334" Size="248.0315; 59.05512" Location="2114.173; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1335" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1336" Size="625.9843; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1337" Size="944.8819; 59.05512" Location="1417.323; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1338" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand116[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1339" Size="625.9843; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand116[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1340" Size="944.8819; 59.05512" Location="1417.323; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand116[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1341" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1342" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1343" Size="1570.866; 129.9213" Location="791.3386; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1344" Size="614.1732; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (03/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand117" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header75" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1345" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail155" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1346" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1347" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand117[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1348" Size="1275.591; 59.05512" Location="1086.614; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand117[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1349" Size="460.6299; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand118" DataSource="dataSet.Datos3_2" Size="2480.315; 484.252" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 165.3543" Name="header76" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1350" Size="118.1102; 165.3543" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText164" Size="295.2756; 165.3543" Location="295.2756; 0" Text="Nivel en la escala&lt;br&gt;( m )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText165" Size="295.2756; 165.3543" Location="791.3386; 0" Text="Volumen utilizado&lt;br&gt;desde la lectura&lt;br&gt;anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1351" Size="649.6063; 165.3543" Location="1086.614; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1352" Size="625.9843; 165.3543" Location="1736.22; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText166" Size="200.7874; 165.3543" Location="590.5512; 0" Text="Caudal&lt;br&gt;estimado o&lt;br&gt;calculado&lt;br&gt;( l/s )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail156" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1353" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand118[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1354" Size="200.7874; 70.86614" Location="590.5512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand118[&quot;caudal_ls&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1355" Size="295.2756; 70.86614" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand118[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1356" Size="649.6063; 70.86614" Location="1086.614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand118[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1357" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand118[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1358" Size="295.2756; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand118[&quot;calado&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer36" Location="0; 377.9528">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1359" Size="614.1732; 59.05512" Location="177.1654; 0" Text="VOLUMEN MES (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" Name="textBox1360" Size="295.2756; 59.05512" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand118[&quot;volumenMes&quot;]" PropertyName="Value" />
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
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader42" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture42" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape39" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText167" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText168" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES DE AGUA&lt;br&gt;&#xD;&#xA;EN LÁMINA LIBRE (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand119" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail157" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1361" Size="696.8504; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1362" Size="543.3071; 141.7323" Location="874.0157; 0" Text="Registro de Aguas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1363" Size="318.8976; 70.86614" Location="1417.323; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1364" Size="318.8976; 70.86614" Location="1417.323; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1365" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand119[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1366" Size="625.9843; 70.86614" Location="1736.22; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand119[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1367" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail158" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1368" Size="1240.157; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1369" Size="696.8504; 59.05512" Location="1417.323; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1370" Size="248.0315; 59.05512" Location="2114.173; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1371" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1372" Size="625.9843; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1373" Size="944.8819; 59.05512" Location="1417.323; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1374" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand119[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1375" Size="625.9843; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand119[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1376" Size="944.8819; 59.05512" Location="1417.323; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand119[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1377" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1378" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1379" Size="1570.866; 129.9213" Location="791.3386; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1380" Size="614.1732; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (04/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand120" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header77" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1381" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail159" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1382" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1383" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand120[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1384" Size="1275.591; 59.05512" Location="1086.614; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand120[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1385" Size="460.6299; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand121" DataSource="dataSet.LDatos4" Size="2480.315; 484.252" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 165.3543" Name="header78" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1386" Size="118.1102; 165.3543" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText169" Size="295.2756; 165.3543" Location="295.2756; 0" Text="Nivel en la escala&lt;br&gt;( m )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText170" Size="295.2756; 165.3543" Location="791.3386; 0" Text="Volumen utilizado&lt;br&gt;desde la lectura&lt;br&gt;anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1387" Size="649.6063; 165.3543" Location="1086.614; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1388" Size="625.9843; 165.3543" Location="1736.22; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText171" Size="200.7874; 165.3543" Location="590.5512; 0" Text="Caudal&lt;br&gt;estimado o&lt;br&gt;calculado&lt;br&gt;( l/s )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail160" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1389" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand121[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1390" Size="200.7874; 70.86614" Location="590.5512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand121[&quot;caudal_ls&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1391" Size="295.2756; 70.86614" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand121[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1392" Size="649.6063; 70.86614" Location="1086.614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand121[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1393" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand121[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1394" Size="295.2756; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand121[&quot;calado&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer37" Location="0; 377.9528">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1323" Size="2185.039; 59.05512" Location="177.1654; 0" Text="(CONTINUACIÓN DEL MES EN HOJA SIGUIENTE)">
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
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader43" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture43" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape40" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText172" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText173" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES DE AGUA&lt;br&gt;&#xD;&#xA;EN LÁMINA LIBRE (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand122" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail161" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1397" Size="696.8504; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1398" Size="543.3071; 141.7323" Location="874.0157; 0" Text="Registro de Aguas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1399" Size="318.8976; 70.86614" Location="1417.323; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1400" Size="318.8976; 70.86614" Location="1417.323; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1401" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand122[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1402" Size="625.9843; 70.86614" Location="1736.22; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand122[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1403" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail162" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1404" Size="1240.157; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1405" Size="696.8504; 59.05512" Location="1417.323; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1406" Size="248.0315; 59.05512" Location="2114.173; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1407" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1408" Size="625.9843; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1409" Size="944.8819; 59.05512" Location="1417.323; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1410" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand122[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1411" Size="625.9843; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand122[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1412" Size="944.8819; 59.05512" Location="1417.323; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand122[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1413" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1414" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1415" Size="1570.866; 129.9213" Location="791.3386; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1416" Size="614.1732; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (04/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand123" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header79" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1417" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail163" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1418" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1419" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand123[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1420" Size="1275.591; 59.05512" Location="1086.614; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand123[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1421" Size="460.6299; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand124" DataSource="dataSet.Datos4_2" Size="2480.315; 484.252" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 165.3543" Name="header80" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1422" Size="118.1102; 165.3543" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText174" Size="295.2756; 165.3543" Location="295.2756; 0" Text="Nivel en la escala&lt;br&gt;( m )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText175" Size="295.2756; 165.3543" Location="791.3386; 0" Text="Volumen utilizado&lt;br&gt;desde la lectura&lt;br&gt;anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1423" Size="649.6063; 165.3543" Location="1086.614; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1424" Size="625.9843; 165.3543" Location="1736.22; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText176" Size="200.7874; 165.3543" Location="590.5512; 0" Text="Caudal&lt;br&gt;estimado o&lt;br&gt;calculado&lt;br&gt;( l/s )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail164" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1425" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand124[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1426" Size="200.7874; 70.86614" Location="590.5512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand124[&quot;caudal_ls&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1427" Size="295.2756; 70.86614" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand124[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1428" Size="649.6063; 70.86614" Location="1086.614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand124[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1429" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand124[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1430" Size="295.2756; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand124[&quot;calado&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer38" Location="0; 377.9528">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1431" Size="614.1732; 59.05512" Location="177.1654; 0" Text="VOLUMEN MES (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" Name="textBox1432" Size="295.2756; 59.05512" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand124[&quot;volumenMes&quot;]" PropertyName="Value" />
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
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader44" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture44" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape41" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText177" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText178" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES DE AGUA&lt;br&gt;&#xD;&#xA;EN LÁMINA LIBRE (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand125" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail165" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1433" Size="696.8504; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1434" Size="543.3071; 141.7323" Location="874.0157; 0" Text="Registro de Aguas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1435" Size="318.8976; 70.86614" Location="1417.323; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1436" Size="318.8976; 70.86614" Location="1417.323; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1437" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand125[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1438" Size="625.9843; 70.86614" Location="1736.22; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand125[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1439" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail166" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1440" Size="1240.157; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1441" Size="696.8504; 59.05512" Location="1417.323; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1442" Size="248.0315; 59.05512" Location="2114.173; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1443" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1444" Size="625.9843; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1445" Size="944.8819; 59.05512" Location="1417.323; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1446" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand125[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1447" Size="625.9843; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand125[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1448" Size="944.8819; 59.05512" Location="1417.323; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand125[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1449" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1450" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1451" Size="1570.866; 129.9213" Location="791.3386; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1452" Size="614.1732; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (05/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand126" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header81" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1453" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail167" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1454" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1455" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand126[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1456" Size="1275.591; 59.05512" Location="1086.614; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand126[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1457" Size="460.6299; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand127" DataSource="dataSet.Datos5" Size="2480.315; 484.252" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 165.3543" Name="header82" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1458" Size="118.1102; 165.3543" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText179" Size="295.2756; 165.3543" Location="295.2756; 0" Text="Nivel en la escala&lt;br&gt;( m )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText180" Size="295.2756; 165.3543" Location="791.3386; 0" Text="Volumen utilizado&lt;br&gt;desde la lectura&lt;br&gt;anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1459" Size="649.6063; 165.3543" Location="1086.614; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1460" Size="625.9843; 165.3543" Location="1736.22; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText181" Size="200.7874; 165.3543" Location="590.5512; 0" Text="Caudal&lt;br&gt;estimado o&lt;br&gt;calculado&lt;br&gt;( l/s )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail168" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1461" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand127[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1462" Size="200.7874; 70.86614" Location="590.5512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand127[&quot;caudal_ls&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1463" Size="295.2756; 70.86614" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand127[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1464" Size="649.6063; 70.86614" Location="1086.614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand127[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1465" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand127[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1466" Size="295.2756; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand127[&quot;calado&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer39" Location="0; 377.9528">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1324" Size="2185.039; 59.05512" Location="177.1654; 0" Text="(CONTINUACIÓN DEL MES EN HOJA SIGUIENTE)">
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
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader45" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture45" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape42" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText182" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText183" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES DE AGUA&lt;br&gt;&#xD;&#xA;EN LÁMINA LIBRE (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand128" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail169" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1469" Size="696.8504; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1470" Size="543.3071; 141.7323" Location="874.0157; 0" Text="Registro de Aguas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1471" Size="318.8976; 70.86614" Location="1417.323; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1472" Size="318.8976; 70.86614" Location="1417.323; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1473" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand128[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1474" Size="625.9843; 70.86614" Location="1736.22; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand128[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1475" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail170" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1476" Size="1240.157; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1477" Size="696.8504; 59.05512" Location="1417.323; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1478" Size="248.0315; 59.05512" Location="2114.173; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1479" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1480" Size="625.9843; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1481" Size="944.8819; 59.05512" Location="1417.323; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1482" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand128[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1483" Size="625.9843; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand128[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1484" Size="944.8819; 59.05512" Location="1417.323; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand128[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1485" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1486" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1487" Size="1570.866; 129.9213" Location="791.3386; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1488" Size="614.1732; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (05/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand129" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header83" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1489" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail171" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1490" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1491" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand129[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1492" Size="1275.591; 59.05512" Location="1086.614; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand129[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1493" Size="460.6299; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand130" DataSource="dataSet.Datos5_2" Size="2480.315; 484.252" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 165.3543" Name="header84" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1494" Size="118.1102; 165.3543" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText184" Size="295.2756; 165.3543" Location="295.2756; 0" Text="Nivel en la escala&lt;br&gt;( m )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText185" Size="295.2756; 165.3543" Location="791.3386; 0" Text="Volumen utilizado&lt;br&gt;desde la lectura&lt;br&gt;anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1495" Size="649.6063; 165.3543" Location="1086.614; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1496" Size="625.9843; 165.3543" Location="1736.22; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText186" Size="200.7874; 165.3543" Location="590.5512; 0" Text="Caudal&lt;br&gt;estimado o&lt;br&gt;calculado&lt;br&gt;( l/s )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail172" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1497" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand130[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1498" Size="200.7874; 70.86614" Location="590.5512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand130[&quot;caudal_ls&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1499" Size="295.2756; 70.86614" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand130[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1500" Size="649.6063; 70.86614" Location="1086.614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand130[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1501" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand130[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1502" Size="295.2756; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand130[&quot;caladado&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer40" Location="0; 377.9528">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1503" Size="614.1732; 59.05512" Location="177.1654; 0" Text="VOLUMEN MES (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" Name="textBox1504" Size="295.2756; 59.05512" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand130[&quot;volumenMes&quot;]" PropertyName="Value" />
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
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader46" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture46" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape43" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText187" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText188" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES DE AGUA&lt;br&gt;&#xD;&#xA;EN LÁMINA LIBRE (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand131" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail173" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1505" Size="696.8504; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1506" Size="543.3071; 141.7323" Location="874.0157; 0" Text="Registro de Aguas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1507" Size="318.8976; 70.86614" Location="1417.323; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1508" Size="318.8976; 70.86614" Location="1417.323; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1509" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand131[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1510" Size="625.9843; 70.86614" Location="1736.22; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand131[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1511" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail174" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1512" Size="1240.157; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1513" Size="696.8504; 59.05512" Location="1417.323; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1514" Size="248.0315; 59.05512" Location="2114.173; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1515" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1516" Size="625.9843; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1517" Size="944.8819; 59.05512" Location="1417.323; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1518" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand131[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1519" Size="625.9843; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand131[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1520" Size="944.8819; 59.05512" Location="1417.323; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand131[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1521" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1522" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1523" Size="1570.866; 129.9213" Location="791.3386; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1524" Size="614.1732; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (06/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand132" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header85" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1525" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail175" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1526" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1527" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand132[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1528" Size="1275.591; 59.05512" Location="1086.614; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand132[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1529" Size="460.6299; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand133" DataSource="dataSet.Datos6" Size="2480.315; 484.252" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 165.3543" Name="header86" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1530" Size="118.1102; 165.3543" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText189" Size="295.2756; 165.3543" Location="295.2756; 0" Text="Nivel en la escala&lt;br&gt;( m )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText190" Size="295.2756; 165.3543" Location="791.3386; 0" Text="Volumen utilizado&lt;br&gt;desde la lectura&lt;br&gt;anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1531" Size="649.6063; 165.3543" Location="1086.614; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1532" Size="625.9843; 165.3543" Location="1736.22; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText191" Size="200.7874; 165.3543" Location="590.5512; 0" Text="Caudal&lt;br&gt;estimado o&lt;br&gt;calculado&lt;br&gt;( l/s )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail176" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1533" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand133[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1534" Size="200.7874; 70.86614" Location="590.5512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand133[&quot;caudal_ls&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1535" Size="295.2756; 70.86614" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand133[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1536" Size="649.6063; 70.86614" Location="1086.614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand133[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1537" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand133[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1538" Size="295.2756; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand133[&quot;calado&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer41" Location="0; 377.9528">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1395" Size="2185.039; 59.05512" Location="177.1654; 0" Text="(CONTINUACIÓN DEL MES EN HOJA SIGUIENTE)">
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
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader47" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture47" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape44" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText192" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText193" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES DE AGUA&lt;br&gt;&#xD;&#xA;EN LÁMINA LIBRE (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand134" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail177" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1541" Size="696.8504; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1542" Size="543.3071; 141.7323" Location="874.0157; 0" Text="Registro de Aguas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1543" Size="318.8976; 70.86614" Location="1417.323; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1544" Size="318.8976; 70.86614" Location="1417.323; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1545" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand134[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1546" Size="625.9843; 70.86614" Location="1736.22; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand134[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1547" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail178" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1548" Size="1240.157; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1549" Size="696.8504; 59.05512" Location="1417.323; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1550" Size="248.0315; 59.05512" Location="2114.173; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1551" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1552" Size="625.9843; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1553" Size="944.8819; 59.05512" Location="1417.323; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1554" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand134[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1555" Size="625.9843; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand134[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1556" Size="944.8819; 59.05512" Location="1417.323; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand134[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1557" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1558" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1559" Size="1570.866; 129.9213" Location="791.3386; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1560" Size="614.1732; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (06/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand135" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header87" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1561" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail179" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1562" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1563" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand135[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1564" Size="1275.591; 59.05512" Location="1086.614; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand135[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1565" Size="460.6299; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand136" DataSource="dataSet.Datos6_2" Size="2480.315; 484.252" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 165.3543" Name="header88" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1566" Size="118.1102; 165.3543" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText194" Size="295.2756; 165.3543" Location="295.2756; 0" Text="Nivel en la escala&lt;br&gt;( m )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText195" Size="295.2756; 165.3543" Location="791.3386; 0" Text="Volumen utilizado&lt;br&gt;desde la lectura&lt;br&gt;anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1567" Size="649.6063; 165.3543" Location="1086.614; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1568" Size="625.9843; 165.3543" Location="1736.22; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText196" Size="200.7874; 165.3543" Location="590.5512; 0" Text="Caudal&lt;br&gt;estimado o&lt;br&gt;calculado&lt;br&gt;( l/s )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail180" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1569" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand136[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1570" Size="200.7874; 70.86614" Location="590.5512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand136[&quot;caudal_ls&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1571" Size="295.2756; 70.86614" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand136[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1572" Size="649.6063; 70.86614" Location="1086.614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand136[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1573" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand136[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1574" Size="295.2756; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand136[&quot;calado&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer42" Location="0; 377.9528">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1575" Size="614.1732; 59.05512" Location="177.1654; 0" Text="VOLUMEN MES (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" Name="textBox1576" Size="295.2756; 59.05512" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand136[&quot;volumenMes&quot;]" PropertyName="Value" />
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
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader48" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture48" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape45" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText197" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText198" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES DE AGUA&lt;br&gt;&#xD;&#xA;EN LÁMINA LIBRE (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand137" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail181" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1577" Size="696.8504; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1578" Size="543.3071; 141.7323" Location="874.0157; 0" Text="Registro de Aguas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1579" Size="318.8976; 70.86614" Location="1417.323; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1580" Size="318.8976; 70.86614" Location="1417.323; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1581" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand137[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1582" Size="625.9843; 70.86614" Location="1736.22; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand137[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1583" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail182" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1584" Size="1240.157; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1585" Size="696.8504; 59.05512" Location="1417.323; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1586" Size="248.0315; 59.05512" Location="2114.173; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1587" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1588" Size="625.9843; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1589" Size="944.8819; 59.05512" Location="1417.323; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1590" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand137[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1591" Size="625.9843; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand137[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1592" Size="944.8819; 59.05512" Location="1417.323; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand137[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1593" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1594" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1595" Size="1570.866; 129.9213" Location="791.3386; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1596" Size="614.1732; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (07/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand138" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header89" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1597" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail183" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1598" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1599" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand138[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1600" Size="1275.591; 59.05512" Location="1086.614; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand138[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1601" Size="460.6299; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand139" DataSource="dataSet.Datos7" Size="2480.315; 484.252" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 165.3543" Name="header90" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1602" Size="118.1102; 165.3543" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText199" Size="295.2756; 165.3543" Location="295.2756; 0" Text="Nivel en la escala&lt;br&gt;( m )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText200" Size="295.2756; 165.3543" Location="791.3386; 0" Text="Volumen utilizado&lt;br&gt;desde la lectura&lt;br&gt;anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1603" Size="649.6063; 165.3543" Location="1086.614; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1604" Size="625.9843; 165.3543" Location="1736.22; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText201" Size="200.7874; 165.3543" Location="590.5512; 0" Text="Caudal&lt;br&gt;estimado o&lt;br&gt;calculado&lt;br&gt;( l/s )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail184" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1605" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand139[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1606" Size="200.7874; 70.86614" Location="590.5512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand139[&quot;caudal_ls&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1607" Size="295.2756; 70.86614" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand139[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1608" Size="649.6063; 70.86614" Location="1086.614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand139[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1609" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand139[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1610" Size="295.2756; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand139[&quot;calado&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer43" Location="0; 377.9528">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1396" Size="2185.039; 59.05512" Location="177.1654; 0" Text="(CONTINUACIÓN DEL MES EN HOJA SIGUIENTE)">
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
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader49" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture49" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape46" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText202" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText203" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES DE AGUA&lt;br&gt;&#xD;&#xA;EN LÁMINA LIBRE (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand140" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail185" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1613" Size="696.8504; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1614" Size="543.3071; 141.7323" Location="874.0157; 0" Text="Registro de Aguas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1615" Size="318.8976; 70.86614" Location="1417.323; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1616" Size="318.8976; 70.86614" Location="1417.323; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1617" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand140[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1618" Size="625.9843; 70.86614" Location="1736.22; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand140[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1619" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail186" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1620" Size="1240.157; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1621" Size="696.8504; 59.05512" Location="1417.323; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1622" Size="248.0315; 59.05512" Location="2114.173; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1623" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1624" Size="625.9843; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1625" Size="944.8819; 59.05512" Location="1417.323; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1626" Size="614.1732; 59.05512" Location="177.1654; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand140[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1627" Size="625.9843; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand140[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1628" Size="944.8819; 59.05512" Location="1417.323; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand140[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1629" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1630" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1631" Size="1570.866; 129.9213" Location="791.3386; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1632" Size="614.1732; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (07/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand141" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header91" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1633" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail187" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1634" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1635" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand141[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1636" Size="1275.591; 59.05512" Location="1086.614; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand141[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1637" Size="460.6299; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand142" DataSource="dataSet.Datos7_2" Size="2480.315; 484.252" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 165.3543" Name="header92" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1638" Size="118.1102; 165.3543" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText204" Size="295.2756; 165.3543" Location="295.2756; 0" Text="Nivel en la escala&lt;br&gt;( m )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText205" Size="295.2756; 165.3543" Location="791.3386; 0" Text="Volumen utilizado&lt;br&gt;desde la lectura&lt;br&gt;anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1639" Size="649.6063; 165.3543" Location="1086.614; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1640" Size="625.9843; 165.3543" Location="1736.22; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText206" Size="200.7874; 165.3543" Location="590.5512; 0" Text="Caudal&lt;br&gt;estimado o&lt;br&gt;calculado&lt;br&gt;( l/s )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail188" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1641" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand142[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1642" Size="200.7874; 70.86614" Location="590.5512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand142[&quot;caudal_ls&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1643" Size="295.2756; 70.86614" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand142[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1644" Size="649.6063; 70.86614" Location="1086.614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand142[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1645" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand142[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1646" Size="295.2756; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand142[&quot;calado&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer44" Location="0; 377.9528">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1647" Size="614.1732; 59.05512" Location="177.1654; 0" Text="VOLUMEN MES (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" Name="textBox1648" Size="295.2756; 59.05512" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand142[&quot;volumenMes&quot;]" PropertyName="Value" />
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
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader50" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture50" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape47" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText207" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText208" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES DE AGUA&lt;br&gt;&#xD;&#xA;EN LÁMINA LIBRE (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand143" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail189" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1649" Size="696.8504; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1650" Size="543.3071; 141.7323" Location="874.0157; 0" Text="Registro de Aguas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1651" Size="318.8976; 70.86614" Location="1417.323; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1652" Size="318.8976; 70.86614" Location="1417.323; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1653" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand143[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1654" Size="625.9843; 70.86614" Location="1736.22; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand143[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1655" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail190" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1656" Size="1240.157; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1657" Size="696.8504; 59.05512" Location="1417.323; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1658" Size="248.0315; 59.05512" Location="2114.173; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1659" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1660" Size="625.9843; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1661" Size="944.8819; 59.05512" Location="1417.323; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1662" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand143[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1663" Size="625.9843; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand143[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1664" Size="944.8819; 59.05512" Location="1417.323; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand143[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1665" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1666" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1667" Size="1570.866; 129.9213" Location="791.3386; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1668" Size="614.1732; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (08/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand144" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header93" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1669" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail191" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1670" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1671" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand144[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1672" Size="1275.591; 59.05512" Location="1086.614; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand144[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1673" Size="460.6299; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand145" DataSource="dataSet.Datos8" Size="2480.315; 484.252" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 165.3543" Name="header94" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1674" Size="118.1102; 165.3543" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText209" Size="295.2756; 165.3543" Location="295.2756; 0" Text="Nivel en la escala&lt;br&gt;( m )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText210" Size="295.2756; 165.3543" Location="791.3386; 0" Text="Volumen utilizado&lt;br&gt;desde la lectura&lt;br&gt;anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1675" Size="649.6063; 165.3543" Location="1086.614; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1676" Size="625.9843; 165.3543" Location="1736.22; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText211" Size="200.7874; 165.3543" Location="590.5512; 0" Text="Caudal&lt;br&gt;estimado o&lt;br&gt;calculado&lt;br&gt;( l/s )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail192" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1677" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand145[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1678" Size="200.7874; 70.86614" Location="590.5512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand145[&quot;caudal_ls&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1679" Size="295.2756; 70.86614" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand145[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1680" Size="649.6063; 70.86614" Location="1086.614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand145[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1681" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand145[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1682" Size="295.2756; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand145[&quot;calado&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer45" Location="0; 377.9528">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1467" Size="2185.039; 59.05512" Location="177.1654; 0" Text="(CONTINUACIÓN DEL MES EN HOJA SIGUIENTE)">
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
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader51" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture51" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape48" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText212" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText213" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES DE AGUA&lt;br&gt;&#xD;&#xA;EN LÁMINA LIBRE (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand146" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail193" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1685" Size="696.8504; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1686" Size="543.3071; 141.7323" Location="874.0157; 0" Text="Registro de Aguas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1687" Size="318.8976; 70.86614" Location="1417.323; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1688" Size="318.8976; 70.86614" Location="1417.323; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1689" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand146[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1690" Size="625.9843; 70.86614" Location="1736.22; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand146[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1691" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail194" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1692" Size="1240.157; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1693" Size="696.8504; 59.05512" Location="1417.323; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1694" Size="248.0315; 59.05512" Location="2114.173; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1695" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1696" Size="625.9843; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1697" Size="944.8819; 59.05512" Location="1417.323; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1698" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand146[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1699" Size="625.9843; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand146[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1700" Size="944.8819; 59.05512" Location="1417.323; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand146[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1701" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1702" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1703" Size="1570.866; 129.9213" Location="791.3386; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1704" Size="614.1732; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (08/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand147" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header95" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1705" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail195" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1706" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1707" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand147[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1708" Size="1275.591; 59.05512" Location="1086.614; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand147[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1709" Size="460.6299; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand148" DataSource="dataSet.Datos8_2" Size="2480.315; 484.252" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 165.3543" Name="header96" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1710" Size="118.1102; 165.3543" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText214" Size="295.2756; 165.3543" Location="295.2756; 0" Text="Nivel en la escala&lt;br&gt;( m )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText215" Size="295.2756; 165.3543" Location="791.3386; 0" Text="Volumen utilizado&lt;br&gt;desde la lectura&lt;br&gt;anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1711" Size="649.6063; 165.3543" Location="1086.614; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1712" Size="625.9843; 165.3543" Location="1736.22; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText216" Size="200.7874; 165.3543" Location="590.5512; 0" Text="Caudal&lt;br&gt;estimado o&lt;br&gt;calculado&lt;br&gt;( l/s )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail196" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1713" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand148[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1714" Size="200.7874; 70.86614" Location="590.5512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand148[&quot;caudal_ls&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1715" Size="295.2756; 70.86614" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand148[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1716" Size="649.6063; 70.86614" Location="1086.614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand148[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1717" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand148[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1718" Size="295.2756; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand148[&quot;calado&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer46" Location="0; 377.9528">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1719" Size="614.1732; 59.05512" Location="177.1654; 0" Text="VOLUMEN MES (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" Name="textBox1720" Size="295.2756; 59.05512" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand148[&quot;volumenMes&quot;]" PropertyName="Value" />
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
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader52" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture52" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape49" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText217" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText218" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES DE AGUA&lt;br&gt;&#xD;&#xA;EN LÁMINA LIBRE (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand149" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail197" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1721" Size="696.8504; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1722" Size="543.3071; 141.7323" Location="874.0157; 0" Text="Registro de Aguas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1723" Size="318.8976; 70.86614" Location="1417.323; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1724" Size="318.8976; 70.86614" Location="1417.323; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1725" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand149[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1726" Size="625.9843; 70.86614" Location="1736.22; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand149[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1727" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail198" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1728" Size="1240.157; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1729" Size="696.8504; 59.05512" Location="1417.323; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1730" Size="248.0315; 59.05512" Location="2114.173; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1731" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1732" Size="625.9843; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1733" Size="944.8819; 59.05512" Location="1417.323; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1734" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand149[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1735" Size="625.9843; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand149[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1736" Size="944.8819; 59.05512" Location="1417.323; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand149[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1737" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1738" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1739" Size="1570.866; 129.9213" Location="791.3386; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1740" Size="614.1732; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (09/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand150" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header97" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1741" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail199" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1742" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1743" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand150[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1744" Size="1275.591; 59.05512" Location="1086.614; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand150[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1745" Size="460.6299; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand151" DataSource="dataSet.Datos9" Size="2480.315; 484.252" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 165.3543" Name="header98" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1746" Size="118.1102; 165.3543" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText219" Size="295.2756; 165.3543" Location="295.2756; 0" Text="Nivel en la escala&lt;br&gt;( m )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText220" Size="295.2756; 165.3543" Location="791.3386; 0" Text="Volumen utilizado&lt;br&gt;desde la lectura&lt;br&gt;anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1747" Size="649.6063; 165.3543" Location="1086.614; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1748" Size="625.9843; 165.3543" Location="1736.22; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText221" Size="200.7874; 165.3543" Location="590.5512; 0" Text="Caudal&lt;br&gt;estimado o&lt;br&gt;calculado&lt;br&gt;( l/s )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail200" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1749" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand151[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1750" Size="200.7874; 70.86614" Location="590.5512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand151[&quot;caudal_ls&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1751" Size="295.2756; 70.86614" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand151[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1752" Size="649.6063; 70.86614" Location="1086.614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand151[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1753" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand151[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1754" Size="295.2756; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand151[&quot;calado&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer47" Location="0; 377.9528">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1468" Size="2185.039; 59.05512" Location="177.1654; 0" Text="(CONTINUACIÓN DEL MES EN HOJA SIGUIENTE)">
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
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader53" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture53" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape50" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText222" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText223" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES DE AGUA&lt;br&gt;&#xD;&#xA;EN LÁMINA LIBRE (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand152" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail201" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1757" Size="696.8504; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1758" Size="543.3071; 141.7323" Location="874.0157; 0" Text="Registro de Aguas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1759" Size="318.8976; 70.86614" Location="1417.323; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1760" Size="318.8976; 70.86614" Location="1417.323; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1761" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand152[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1762" Size="625.9843; 70.86614" Location="1736.22; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand152[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1763" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail202" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1764" Size="1240.157; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1765" Size="696.8504; 59.05512" Location="1417.323; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1766" Size="248.0315; 59.05512" Location="2114.173; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1767" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1768" Size="625.9843; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1769" Size="944.8819; 59.05512" Location="1417.323; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1770" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand152[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1771" Size="625.9843; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand152[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1772" Size="944.8819; 59.05512" Location="1417.323; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand152[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1773" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1774" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1775" Size="1570.866; 129.9213" Location="791.3386; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1776" Size="614.1732; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (09/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand153" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header99" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1777" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail203" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1778" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1779" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand153[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1780" Size="1275.591; 59.05512" Location="1086.614; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand153[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1781" Size="460.6299; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand154" DataSource="dataSet.Datos9_2" Size="2480.315; 484.252" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 165.3543" Name="header100" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1782" Size="118.1102; 165.3543" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText224" Size="295.2756; 165.3543" Location="295.2756; 0" Text="Nivel en la escala&lt;br&gt;( m )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText225" Size="295.2756; 165.3543" Location="791.3386; 0" Text="Volumen utilizado&lt;br&gt;desde la lectura&lt;br&gt;anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1783" Size="649.6063; 165.3543" Location="1086.614; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1784" Size="625.9843; 165.3543" Location="1736.22; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText226" Size="200.7874; 165.3543" Location="590.5512; 0" Text="Caudal&lt;br&gt;estimado o&lt;br&gt;calculado&lt;br&gt;( l/s )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail204" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1785" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand154[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1786" Size="200.7874; 70.86614" Location="590.5512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand154[&quot;caudal_ls&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1787" Size="295.2756; 70.86614" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand154[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1788" Size="649.6063; 70.86614" Location="1086.614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand154[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1789" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand154[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1790" Size="295.2756; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand154[&quot;calado&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer48" Location="0; 377.9528">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1791" Size="614.1732; 59.05512" Location="177.1654; 0" Text="VOLUMEN MES (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" Name="textBox1792" Size="295.2756; 59.05512" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand154[&quot;volumenMes&quot;]" PropertyName="Value" />
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
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader54" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture54" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape51" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText227" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText228" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES DE AGUA&lt;br&gt;&#xD;&#xA;EN LÁMINA LIBRE (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand155" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail205" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1793" Size="696.8504; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1794" Size="543.3071; 141.7323" Location="874.0157; 0" Text="Registro de Aguas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1795" Size="318.8976; 70.86614" Location="1417.323; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1796" Size="318.8976; 70.86614" Location="1417.323; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1797" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand155[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1798" Size="625.9843; 70.86614" Location="1736.22; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand155[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1799" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail206" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1800" Size="1240.157; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1801" Size="696.8504; 59.05512" Location="1417.323; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1802" Size="248.0315; 59.05512" Location="2114.173; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1803" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1804" Size="625.9843; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1805" Size="944.8819; 59.05512" Location="1417.323; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1806" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand155[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1807" Size="625.9843; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand155[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1808" Size="944.8819; 59.05512" Location="1417.323; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand155[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1809" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1810" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1811" Size="1570.866; 129.9213" Location="791.3386; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1812" Size="614.1732; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (10/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand156" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header101" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1813" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail207" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1814" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1815" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand156[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1816" Size="1275.591; 59.05512" Location="1086.614; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand156[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1817" Size="460.6299; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand157" DataSource="dataSet.Datos10" Size="2480.315; 484.252" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 165.3543" Name="header102" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1818" Size="118.1102; 165.3543" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText229" Size="295.2756; 165.3543" Location="295.2756; 0" Text="Nivel en la escala&lt;br&gt;( m )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText230" Size="295.2756; 165.3543" Location="791.3386; 0" Text="Volumen utilizado&lt;br&gt;desde la lectura&lt;br&gt;anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1819" Size="649.6063; 165.3543" Location="1086.614; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1820" Size="625.9843; 165.3543" Location="1736.22; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText231" Size="200.7874; 165.3543" Location="590.5512; 0" Text="Caudal&lt;br&gt;estimado o&lt;br&gt;calculado&lt;br&gt;( l/s )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail208" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1821" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand157[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1822" Size="200.7874; 70.86614" Location="590.5512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand157[&quot;caudal_ls&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1823" Size="295.2756; 70.86614" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand157[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1824" Size="649.6063; 70.86614" Location="1086.614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand157[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1825" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand157[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1826" Size="295.2756; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand157[&quot;calado&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer49" Location="0; 377.9528">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1539" Size="2185.039; 59.05512" Location="177.1654; 0" Text="(CONTINUACIÓN DEL MES EN HOJA SIGUIENTE)">
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
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader55" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture55" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape52" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText232" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText233" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES DE AGUA&lt;br&gt;&#xD;&#xA;EN LÁMINA LIBRE (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand158" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail209" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1829" Size="696.8504; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1830" Size="543.3071; 141.7323" Location="874.0157; 0" Text="Registro de Aguas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1831" Size="318.8976; 70.86614" Location="1417.323; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1832" Size="318.8976; 70.86614" Location="1417.323; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1833" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand158[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1834" Size="625.9843; 70.86614" Location="1736.22; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand158[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1835" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail210" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1836" Size="1240.157; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1837" Size="696.8504; 59.05512" Location="1417.323; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1838" Size="248.0315; 59.05512" Location="2114.173; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1839" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1840" Size="625.9843; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1841" Size="944.8819; 59.05512" Location="1417.323; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1842" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand158[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1843" Size="625.9843; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand158[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1844" Size="944.8819; 59.05512" Location="1417.323; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand158[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1845" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1846" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1847" Size="1570.866; 129.9213" Location="791.3386; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1848" Size="614.1732; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (10/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand159" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header103" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1849" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail211" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1850" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1851" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand159[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1852" Size="1275.591; 59.05512" Location="1086.614; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand159[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1853" Size="460.6299; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand160" DataSource="dataSet.Datos10_2" Size="2480.315; 484.252" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 165.3543" Name="header104" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1854" Size="118.1102; 165.3543" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText234" Size="295.2756; 165.3543" Location="295.2756; 0" Text="Nivel en la escala&lt;br&gt;( m )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText235" Size="295.2756; 165.3543" Location="791.3386; 0" Text="Volumen utilizado&lt;br&gt;desde la lectura&lt;br&gt;anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1855" Size="649.6063; 165.3543" Location="1086.614; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1856" Size="625.9843; 165.3543" Location="1736.22; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText236" Size="200.7874; 165.3543" Location="590.5512; 0" Text="Caudal&lt;br&gt;estimado o&lt;br&gt;calculado&lt;br&gt;( l/s )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail212" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1857" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand160[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1858" Size="200.7874; 70.86614" Location="590.5512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand160[&quot;caudal_ls&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1859" Size="295.2756; 70.86614" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand160[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1860" Size="649.6063; 70.86614" Location="1086.614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand160[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1861" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand160[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1862" Size="295.2756; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand160[&quot;calado&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer50" Location="0; 377.9528">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1863" Size="614.1732; 59.05512" Location="177.1654; 0" Text="VOLUMEN MES (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" Name="textBox1864" Size="295.2756; 59.05512" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand160[&quot;volumenMes&quot;]" PropertyName="Value" />
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
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader56" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture56" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape53" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText237" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText238" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES DE AGUA&lt;br&gt;&#xD;&#xA;EN LÁMINA LIBRE (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand161" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail213" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1865" Size="696.8504; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1866" Size="543.3071; 141.7323" Location="874.0157; 0" Text="Registro de Aguas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1867" Size="318.8976; 70.86614" Location="1417.323; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1868" Size="318.8976; 70.86614" Location="1417.323; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1869" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand161[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1870" Size="625.9843; 70.86614" Location="1736.22; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand161[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1871" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail214" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1872" Size="1240.157; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1873" Size="696.8504; 59.05512" Location="1417.323; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1874" Size="248.0315; 59.05512" Location="2114.173; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1875" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1876" Size="625.9843; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1877" Size="944.8819; 59.05512" Location="1417.323; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1878" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand161[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1879" Size="625.9843; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand161[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1880" Size="944.8819; 59.05512" Location="1417.323; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand161[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1881" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1882" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1883" Size="1570.866; 129.9213" Location="791.3386; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1884" Size="614.1732; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (11/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand162" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header105" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1885" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail215" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1886" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1887" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand162[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1888" Size="1275.591; 59.05512" Location="1086.614; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand162[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1889" Size="460.6299; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand163" DataSource="dataSet.Datos11" Size="2480.315; 484.252" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 165.3543" Name="header106" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1890" Size="118.1102; 165.3543" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText239" Size="295.2756; 165.3543" Location="295.2756; 0" Text="Nivel en la escala&lt;br&gt;( m )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText240" Size="295.2756; 165.3543" Location="791.3386; 0" Text="Volumen utilizado&lt;br&gt;desde la lectura&lt;br&gt;anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1891" Size="649.6063; 165.3543" Location="1086.614; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1892" Size="625.9843; 165.3543" Location="1736.22; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText241" Size="200.7874; 165.3543" Location="590.5512; 0" Text="Caudal&lt;br&gt;estimado o&lt;br&gt;calculado&lt;br&gt;( l/s )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail216" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1893" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand163[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1894" Size="200.7874; 70.86614" Location="590.5512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand163[&quot;caudal_ls&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1895" Size="295.2756; 70.86614" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand163[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1896" Size="649.6063; 70.86614" Location="1086.614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand163[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1897" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand163[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1898" Size="295.2756; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand163[&quot;calado&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer51" Location="0; 377.9528">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1540" Size="2185.039; 59.05512" Location="177.1654; 0" Text="(CONTINUACIÓN DEL MES EN HOJA SIGUIENTE)">
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
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader57" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture57" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape54" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText242" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText243" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES DE AGUA&lt;br&gt;&#xD;&#xA;EN LÁMINA LIBRE (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand164" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail217" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1901" Size="696.8504; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1902" Size="543.3071; 141.7323" Location="874.0157; 0" Text="Registro de Aguas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1903" Size="318.8976; 70.86614" Location="1417.323; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1904" Size="318.8976; 70.86614" Location="1417.323; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1905" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand164[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1906" Size="625.9843; 70.86614" Location="1736.22; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand164[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1907" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail218" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1908" Size="1240.157; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1909" Size="696.8504; 59.05512" Location="1417.323; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1910" Size="248.0315; 59.05512" Location="2114.173; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1911" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1912" Size="625.9843; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1913" Size="944.8819; 59.05512" Location="1417.323; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1914" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand164[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1915" Size="625.9843; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand164[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1916" Size="944.8819; 59.05512" Location="1417.323; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand164[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1917" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1918" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1919" Size="1570.866; 129.9213" Location="791.3386; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1920" Size="614.1732; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (11/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand165" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header107" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1921" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail219" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1922" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1923" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand165[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1924" Size="1275.591; 59.05512" Location="1086.614; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand165[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1925" Size="460.6299; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand166" DataSource="dataSet.Datos11_2" Size="2480.315; 484.252" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 165.3543" Name="header108" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1926" Size="118.1102; 165.3543" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText244" Size="295.2756; 165.3543" Location="295.2756; 0" Text="Nivel en la escala&lt;br&gt;( m )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText245" Size="295.2756; 165.3543" Location="791.3386; 0" Text="Volumen utilizado&lt;br&gt;desde la lectura&lt;br&gt;anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1927" Size="649.6063; 165.3543" Location="1086.614; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1928" Size="625.9843; 165.3543" Location="1736.22; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText246" Size="200.7874; 165.3543" Location="590.5512; 0" Text="Caudal&lt;br&gt;estimado o&lt;br&gt;calculado&lt;br&gt;( l/s )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail220" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1929" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand166[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1930" Size="200.7874; 70.86614" Location="590.5512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand166[&quot;caudal_ls&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1931" Size="295.2756; 70.86614" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand166[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1932" Size="649.6063; 70.86614" Location="1086.614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand166[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1933" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand166[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1934" Size="295.2756; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand166[&quot;calado&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer52" Location="0; 377.9528">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1935" Size="614.1732; 59.05512" Location="177.1654; 0" Text="VOLUMEN MES (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" Name="textBox1936" Size="295.2756; 59.05512" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand166[&quot;volumenMes&quot;]" PropertyName="Value" />
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
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader58" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture58" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape55" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText247" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText248" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES DE AGUA&lt;br&gt;&#xD;&#xA;EN LÁMINA LIBRE (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand167" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail221" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1937" Size="696.8504; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1938" Size="543.3071; 141.7323" Location="874.0157; 0" Text="Registro de Aguas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1939" Size="318.8976; 70.86614" Location="1417.323; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1940" Size="318.8976; 70.86614" Location="1417.323; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1941" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand167[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1942" Size="625.9843; 70.86614" Location="1736.22; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand167[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1943" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail222" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1944" Size="1240.157; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1945" Size="696.8504; 59.05512" Location="1417.323; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1946" Size="248.0315; 59.05512" Location="2114.173; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1947" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1948" Size="625.9843; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1949" Size="944.8819; 59.05512" Location="1417.323; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1950" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand167[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1951" Size="625.9843; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand167[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1952" Size="944.8819; 59.05512" Location="1417.323; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand167[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1953" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1954" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1955" Size="1570.866; 129.9213" Location="791.3386; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1956" Size="614.1732; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (12/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand168" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header109" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1957" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail223" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1958" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1959" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand168[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1960" Size="1275.591; 59.05512" Location="1086.614; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand168[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1961" Size="460.6299; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand169" DataSource="dataSet.Datos12" Size="2480.315; 484.252" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 165.3543" Name="header110" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1962" Size="118.1102; 165.3543" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText249" Size="295.2756; 165.3543" Location="295.2756; 0" Text="Nivel en la escala&lt;br&gt;( m )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText250" Size="295.2756; 165.3543" Location="791.3386; 0" Text="Volumen utilizado&lt;br&gt;desde la lectura&lt;br&gt;anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1963" Size="649.6063; 165.3543" Location="1086.614; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1964" Size="625.9843; 165.3543" Location="1736.22; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText251" Size="200.7874; 165.3543" Location="590.5512; 0" Text="Caudal&lt;br&gt;estimado o&lt;br&gt;calculado&lt;br&gt;( l/s )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail224" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1965" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand169[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1966" Size="200.7874; 70.86614" Location="590.5512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand169[&quot;caudal_ls&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1967" Size="295.2756; 70.86614" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand169[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1968" Size="649.6063; 70.86614" Location="1086.614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand169[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1969" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand169[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1970" Size="295.2756; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand169[&quot;calado&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer53" Location="0; 377.9528">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1611" Size="2185.039; 59.05512" Location="177.1654; 0" Text="(CONTINUACIÓN DEL MES EN HOJA SIGUIENTE)">
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
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader59" Location="0; 165.3542">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture59" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape56" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText252" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText253" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES DE AGUA&lt;br&gt;&#xD;&#xA;EN LÁMINA LIBRE (Qmax &gt;= 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand170" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 968.504" Location="0; 460.6298">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail225" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1973" Size="696.8504; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1974" Size="543.3071; 141.7323" Location="874.0157; 0" Text="Registro de Aguas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1975" Size="318.8976; 70.86614" Location="1417.323; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1976" Size="318.8976; 70.86614" Location="1417.323; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1977" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand170[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1978" Size="625.9843; 70.86614" Location="1736.22; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand170[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1979" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 354.3307" Name="detail226" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1980" Size="1240.157; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1981" Size="696.8504; 59.05512" Location="1417.323; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1982" Size="248.0315; 59.05512" Location="2114.173; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1983" Size="614.1732; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1984" Size="625.9843; 59.05512" Location="791.3386; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1985" Size="944.8819; 59.05512" Location="1417.323; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1986" Size="614.1732; 59.05512" Location="177.1653; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand170[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1987" Size="625.9843; 59.05512" Location="791.3386; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand170[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1988" Size="944.8819; 59.05512" Location="1417.323; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand170[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1989" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox1990" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox1991" Size="1570.866; 129.9213" Location="791.3386; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1992" Size="614.1732; 129.9213" Location="177.1654; 224.4095" Text="MES/AÑO (12/201_)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand171" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header111" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox1993" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail227" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1994" Size="118.1102; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1995" Size="330.7087; 59.05512" Location="295.2756; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand171[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1996" Size="1275.591; 59.05512" Location="1086.614; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand171[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox1997" Size="460.6299; 59.05512" Location="625.9843; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand172" DataSource="dataSet.Datos12_2" Size="2480.315; 543.3071" Location="0; 1476.378">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 165.3543" Name="header112" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1998" Size="118.1102; 165.3543" Location="177.1654; 0" Text="Día">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText254" Size="295.2756; 165.3543" Location="295.2756; 0" Text="Nivel en la escala&lt;br&gt;( m )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText255" Size="295.2756; 165.3543" Location="791.3386; 0" Text="Volumen utilizado&lt;br&gt;desde la lectura&lt;br&gt;anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox1999" Size="649.6063; 165.3543" Location="1086.614; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox2000" Size="625.9843; 165.3543" Location="1736.22; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText256" Size="200.7874; 165.3543" Location="590.5512; 0" Text="Caudal&lt;br&gt;estimado o&lt;br&gt;calculado&lt;br&gt;( l/s )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 70.86614" Name="detail228" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox2001" Size="118.1102; 70.86614" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand172[&quot;dia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox2002" Size="200.7874; 70.86614" Location="590.5512; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand172[&quot;caudal_ls&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox2003" Size="295.2756; 70.86614" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand172[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox2004" Size="649.6063; 70.86614" Location="1086.614; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand172[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox2005" Size="625.9843; 70.86614" Location="1736.22; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand172[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox2006" Size="295.2756; 70.86614" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand172[&quot;calado&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 118.1102" Name="footer54" Location="0; 377.9528">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox2007" Size="614.1732; 59.05512" Location="177.1654; 0" Text="VOLUMEN MES (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" Name="textBox2008" Size="295.2756; 59.05512" Location="791.3386; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand172[&quot;volumenMes&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" Name="textBox1612" Size="295.2756; 59.05512" Location="791.3386; 59.05512">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand172[&quot;volumenAnyo&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox1683" Size="614.1732; 59.05512" Location="177.1653; 59.05512" Text="VOLUMEN AÑO (m3)">
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