<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<root type="NineRays.Reporting.DOM.Document" Name="Capt3_tuberia" GridStep="11.8110237" Title="Cuadrante Brigada Anual" IsTemplate="True" DoublePass="True" ImportsString="System.Drawing" Description="Demonstrates how to create simple list report." ScriptLanguage="CSharp">
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
              <Image length="3432">iVBORw0KGgoAAAANSUhEUgAAAEAAAABGCAYAAAB8MJLDAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAAlwSFlzAAALBwAACwcBtnMLwAAADNFJREFUeF7l2z+oHdUWBnBjNCFoNH8NaMSAJoiISRHSJI1iI4IIShrFUsukeJUkYCERUkgKbSWQVGlCQsBOJWIr1uZBihSKPjDCQ0017/7m+R323XfPOXPOuSZFNgxzz56ZPWt9+1t/9tpzN3Qr7YH7uQHgfm4P3M/K9+y/lwAcP36827FjR3f16tV7JsY9BeC5557rTp482b3++uv3JwCnTp3qnnnmme7mzZv3HwA//fRTt3Xr1h4Ax71qd80Ebty40Z0/f7777LPPuq+//rq7fPlyt3PnTiG427ZtWweQzz//vPvkk0+677//vvvtt9/uCib/OACUYeuPPPJIr2zr2Lx5c7dp06bO2XVnoBw9erS7devWPwrEugJgdt9///3eqb377rv98fjjj/fKm20KHTt2rHv++ed7+geMXH/ppZf66wcPHuwB2LJlS28e5ZjegT3r1dYFgA8//LBX0izWM0wJQNSNGeTePXv2dH/++eeqW5jAkSNHBsd88sknu4sXLy6Nw9IAvPbaaz1lH3vsse6FF17oZ/+dd97pZ5uCzq321VdfTQAYcoJAkScAFojGfvvttzvKYxRwT5w4sRQISwHw1ltvdQ8++GD37LPPrqElhwYA9E0zq2fPnu089+qrr04AAJ77AAeYsr344ou9onWTPzAv165cubIwCAsDwDmhvePTTz9dJcAvv/zSifEA4ACZCAWdXdM++OCDCQD8wa+//tr3ywopl/sxwDuYzF9//bXqPVjhHcuE0YUBQL2HHnqopz6lLly40M8g4c0yZ8W2CWjG63b69OmJ10fnVjKE6t5x+PDhfnxjewfFsenjjz/uxwfQosnUwgBQwMt59EOHDvUzVDfe2j0tirJvtv3ee+/1QLUaRbGjzgk8iyHMIwAsmjcsDID4Hq/PW7caIQmIJYSOp//55597hybcYQ8Kl+sByrhHKHzllVf6BKluxkoozfiLOIKFASBAKC4S1A1AYjoA3EdJyugXGTg71NaHTQ79MkKsAo5nPB/Kl+/wfsxjIsZYtC0MgBey871793YAYJtmPHTHiqHMj2OkPCWBQ3EhlOL+dg0DjJlIEj8CKIwA7htvvDFJo+8JAGaBc3r55ZdXxXSKAMAMxk5LMChmpj3rPn9zYgmN+inKOboOMGBl3IwFrGWzwqUYENR549C1FM51Tq5mQtJbCvmbcmaYovW9xga0e8trnvvxxx8XnfjJczMBGOtdzVipAGqbvTLnHzKJaf2SHeYRgAEFDCxZjzYTAAuUsQ0d2e8iitbPAJCyHF2uUZzDYzJjG7Oa1poAyOIoIs10mEVOhwef1az7JTDLgpBKUSKN8TChXjS15CE/eaXo5CdPwKufXwOAdHXDhg1NBR5++OFZ+vfXpcY1CBQSMZwJJnK8+eabk8PM5tquXbsm97iP8sYbMwHyCkq3JkDGyCeVbQ0AMjqru40bN64axKKHAmNmwAvKmSMMZszTKFsqwVGOaeQDJHlrEOhVy9E0ATE41ZkMMpSuElRSA7jySKqc52V1cYycJYoKY+VBcI7TbMvuSgVSSsu7nB1DrKijkrFa964BQE2OUITgBwjKERGaQ6wHcd0ipT4slvTlPHQPhVvPDj2nv7xGrrJZiGGLfnInsaKPfvpNNYFpBQYzXK/XCb9ME+dnNe8Ymum64OLeWsaMr7/Wbw0DxNfU4xIFckbh2oamAYbGfEmroBGhZgHw+++/T+RJcYWdB5AagFSba9n9pledOTZ9wFAeX9OHEtMASCTg9YdaABDbgct2KSh+ozMAs+q08DFBlGF6WqvkllVo7QRbtckmAF5OeF7fWVgyGIHqVpuAmSF0Zt5zBPZbPlG3AOA5Csr/NWCbsVY4E6bz3gBRjiuBwlayOxKRWltwTQBqpVJ5aRU96nulzkNxmDBDAGCSyCN8ZeWXgkoJAoXF87QWA8hUypoKdMtfrTsAhK5DWBTAqNqZlT7g9u3bPd3NsLPEK89m1nly980CoHSEcwNQ23UY0PKuNaqz0uCasrUTRP0W7bEKuwBQrgVaJjDEgJa/mosBYwEYSqX1TwNAajzkwAKUc1mBmgeApU2gBUCNqsLGEABsvF7JnTlzZkJnix3Puo/j4owzVhQVIZjYNBMgUylrNmFGA1AqxX7MDFpyTlZa+lInaA3Kmw/ReMgJ6jduXeDMDhPnmM2TRIoyDAI2iQ6wyHru3Lne52RdMQqA7MkJQV7Umk0K6meXrdBSryMCRishqn2AdUianKB8PxCkuKUj9ZvD5HvqBVw9CcCsCzwTH3Dnzp1JkTLV3DFr+lYYGjKBEgBUVtyUdFnolKtMCVEy0lKG1oJsltMtnyeXycOOvG8CgGytFlwRgdDZ23eGuPBU22ZJ7VkAWCkSIrHa2e/U/1vxnyLJBMt3BQDvBFC21P1tW01/XU/0TLbTJgCovQWtrJzMTLkFVSYXhCR06ZHZmBcGAALzH/ntjMZCHZv1rAIFM/Jbv8Pv7DgDHSMiW12i4ziluKVspRM0biuyMHFtAoBP1mJDECvb2EzQi0vKhfIlAJSmkApQaOhs6c22KUNoY+U655a0uLUcb6XnJSDlVnyYFIc4AcCqKzkzgVsAoE29TVV6Vnab/NuZWbmePrOZhY6ZS2XXWYITUOwb1M39nF29Gq19UDZrpgGQBdcqBviBKvHwLQCgF9vJ9Xk+UAhVE9oAY1s8dizum+FWZGGKlKpLcmUi5JqxTGALgOQYQEprZoL1sjEmEPqUycw8BREKuh+LAI3WAMAMv9n/vB9OlgxAdb6DCZfb5TGB8mONqQDUSuVjBwDU6Co7xdHF6SWiWEa7FlalfOZDBzOVLW6O0DMWORhlqy0VY/t/jrKCnN/OJSNNVByt8eIv5s4EA4CZCqWiPGdVttybxMg1fe4PPbM85sFRM34kX3iU7xNdPKf2gNKU+uOPP/rfMTcbsKns1D6gDOdkIn8+1xmVCXqp8KZ4UcfzVkWoBUCiQYQLAIku2c4GgJwiJpf7zSqzkBYLz0zF71SWyZZ0uLUYqqNRIhPnOnVjhOJlkhM6HThwoJ/RaavBFgNqAFIlNg768yVZIPltVt0jO8wOMsXdB6Q4z5JdQwURYzHPcn8gTrB03BMnaOFQxmtAJHcekweMMYEyRwACwJMIsdcshrLYwjgAOIsCztjhU7mEylkVIWGz3q8ka0LwBAAvDQB1GBpTEBkDwN+J1woO/19ZlokQhTLbwJeTYIM+Z5THFkAwjVkm0FoOl9WlRLJVi6HkzEM1wXlNoHaCJQCUkRW6B+AUZuNAibKETEE2NYLUA1JfHGLANADKzZRVeQAADFwDoGgBvWlF0ZIBcULTAEianOWwd+a9+aAS3bNGYdNZymZnqKwH1JFpCADp/iAABmFnQzXBsQAMhcGSAQEgFSFCYYBn4+3rpbB+ztI5ew3zMMDYddRYt5rgvD6gZkAJgJy//rw+H2MDgIkkpI6pCS6cCIVW6xUFWgyICXCKUYbtS3xKBmQJHdpn72AoD2itBUYlQsnkWouh9XSCtQlwdunL3/XSWr/GL+Tvf4QB2YLOOXuFLQCSxY1JhFoMkM9rFkps2yJGmEPxVIackx1ih/sSqlv7fdYursdZ5vM7/XWba3NUKatuywKQmRTesoObNT/Kuh6nLFkCTllbbH2InTVGXdNs3dv8RIZnTVgKivGg9VfZQPG93iJOUDYmF8As46YSHZCtDtG9rOQCBAj6AFKvT4zTkp8eWFWzeA0A09b3Hm5dz8eQmZkxYdDsEFQlKosjgEiIePlkihTOAi3hMWlszKdk5awCTR021wBgRlqzn0JFq1zlpWMYUFOSsg5ZXrbezSpFhcJ8qqdclv8kyxqfX2h9XEG+krWlP6NDXVRdBYBYO+vLToK0ko+U0Mt6QL0aHNpn4BzlAakTYJq/mVfWAma9VL71AaQsdtq/53m/6+U2/SoAxm6ItD6UoHj+VS5l6DoVZq/o7h8orYiq1itNsaEvQdk902iVtkrgh4BOf2nGqwAYu8vSMoPYIeFRVs7Nlv1vENRRneCc1Iog+1aOcyvH+ZXjXyvH6ZXji927d/dOLf8nmH/BwSQMyf7BmlD0d0dZL5gGQsngNQCwPc4M1VPJSZ/zo48+2vw/wFooZSxIc1QpfuSejz76CAiXV447K8d/V47/rBz/LjdGAeU5zxtnzAeaGFf+F2rk11cegwCgdrnxkH9UKtNKfUOfoQ3NDFZQYvv27d3+/fv7/yHYt29fD+ZTTz01SXn1Pf30090TTzzRYcM8H0V7N9nKr8DUDr23/lqk/NK8mQgNKbJs/7Vr17offvih+/bbb7vvvvuu++abb/oh2ba+69ev9+cvv/yyu3Tp0rKvG/X8XQVglER3+ab/AZ0aP6pkDQ1QAAAAAElFTkSuQmCC</Image>
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
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="page4" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader8" Location="0; 165.3543">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture8" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">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</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape5" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText23" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText24" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES MEDIANTE &lt;br&gt;&#xD;&#xA;TUBERÍA A PRESIÓN (100 l/s &lt;= Qmax &lt; 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand19" DataSource="dataSet.Datos1" Size="2480.315; 425.1968" Location="0; 1429.134">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header10" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox185" Size="177.1654; 118.1102" Location="519.6851; 0" Text="Día que se realiza la anotación">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox186" Size="118.1102; 118.1102" Location="177.1654; 0" Text="Mes">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText25" Size="259.8425; 118.1102" Location="696.8504; 0" Text="Lectura&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText26" Size="283.4646; 118.1102" Location="956.6929; 0" Text="Lectura Actual - &lt;br&gt;Lectura Anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox187" Size="425.1968; 118.1102" Location="1240.157; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox188" Size="696.8504; 118.1102" Location="1665.354; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox199" Size="224.4095; 118.1102" Location="295.2756; 0" Text="Semana">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail24" Location="0; 212.5984">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox189" Size="118.1102; 59.05512" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand19[&quot;mes&quot;]" PropertyName="Value" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand19[&quot;tamanyo&quot;]" PropertyName="Size" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox190" Size="224.4095; 59.05512" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand19[&quot;semana&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox191" Size="177.1654; 59.05512" Location="519.6851; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand19[&quot;fecha_medidaS&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox192" Size="283.4646; 59.05512" Location="956.6929; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand19[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox193" Size="425.1968; 59.05512" Location="1240.157; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand19[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox194" Size="696.8504; 59.05512" Location="1665.354; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand19[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox196" Size="259.8425; 59.05512" Location="696.8504; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand19[&quot;lectura&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer3" Location="0; 318.8976">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox195" Size="2185.039; 59.05512" Location="177.1654; 0" Text="(CONTINUACIÓN DEL AÑO EN HOJA SIGUIENTE)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand17" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 921.2598" Location="0; 460.6299">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail21" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox160" Size="1062.992; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox161" Size="472.4409; 141.7323" Location="1240.157; 0" Text="Catálogo de Aguas Privadas">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand17[&quot;tipo&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox162" Size="330.7087; 70.86614" Location="1712.598; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox163" Size="330.7087; 70.86614" Location="1712.598; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox164" Size="318.8976; 70.86614" Location="2043.307; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand17[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox165" Size="318.8976; 70.86614" Location="2043.307; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand17[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox166" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 307.0866" Name="detail22" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox167" Size="1535.433; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox168" Size="531.4961; 59.05512" Location="1712.598; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox169" Size="118.1102; 59.05512" Location="2244.094; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox170" Size="791.3386; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox171" Size="744.0945; 59.05512" Location="968.504; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox172" Size="649.6063; 59.05512" Location="1712.598; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox173" Size="791.3386; 59.05512" Location="177.1654; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand17[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox174" Size="744.0945; 59.05512" Location="968.504; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand17[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox175" Size="649.6063; 59.05512" Location="1712.598; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand17[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox176" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox177" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox178" Size="1122.047; 82.67717" Location="1240.157; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox179" Size="1062.992; 82.67717" Location="177.1654; 224.4095" Text="AÑO 201_">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand18" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header9" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox180" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail23" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox181" Size="165.3543; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox182" Size="248.0315; 59.05512" Location="342.5197; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand18[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox183" Size="1122.047; 59.05512" Location="1240.157; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand18[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox184" Size="649.6063; 59.05512" Location="590.5512; 0" Text="Nombre y apellidos">
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
    <item type="NineRays.Reporting.DOM.Page" Size="2480.315; 3507.874" Name="page5" Location="0; 0">
      <Margins type="NineRays.Reporting.DOM.Margins" Bottom="141.732285" Left="177.165359" Top="118.110237" Right="118.110237" />
      <Controls>
        <item type="NineRays.Reporting.DOM.PageHeader" Size="2480.315; 248.0315" Name="pageHeader9" Location="0; 165.3543">
          <Controls>
            <item type="NineRays.Reporting.DOM.Picture" Name="picture9" Size="237.4016; 233.8583" Location="177.1654; 11.81102">
              <Image length="2639">/9j/4AAQSkZJRgABAQEAYABgAAD/2wBDAAgGBgcGBQgHBwcJCQgKDBQNDAsLDBkSEw8UHRofHh0aHBwgJC4nICIsIxwcKDcpLDAxNDQ0Hyc5PTgyPC4zNDL/2wBDAQkJCQwLDBgNDRgyIRwhMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjIyMjL/wAARCABLAEwDASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwD3yiiigBrusS75GVUHUlsCszUvEOl6VAJbm7jwxwqowZiR1wM1yGpJaaz4z1Gz8QSTyW1rs+wWIyiSkx5Yk9zz6iseS0sPC/jezv8AS9AURSxmORV3MLZhwZcY7A4PP5UAaXirxLZ6tJpsenzyB4phLIm4IUPG3OTweuD2rpY/HmhSXcUK3PEjFRNx5eQM/ezXKeJdQs76WKWZ7RpJrV7dZYf49+Cuefu8dASfSp/E/iC3HhqXT0tLC7SSHbDbJwV2ck4BzhcegoA9DS6t5H2xzxM/TCuCeKmryyy8LeF7Pw5El7BJbTW8SGTUbZnDGRec465z/s11ngPUr7U/C0U2oTSTXCTSR+dIgUyBWwGwOPagDp6KKKAIbm5gs4GmnkWOJcZY+5wP1rItPF+h399JZw3jCZJFj2yRPGCxAIwWABzuFSeJ43k8PXflqshRN5jP/LQKc46Hk44rzjw1pYfV7OSx0e7tjcOH+03TXDJ8vzZ/eggtgYFAHoXiHw8uqxPcWnlw6moASYrkEDPB9uTXnB03XIHmtrpbK3nE4KySzqdiqPlwuTIRk8Y7dRmvV3UzarJEZHEawKwCOV5LMCePpXK6pf8AheTXLeOaGW5mO0G9inYCLJIALBgf4DQBxEPhXVRDj7TYcyecwjtrxl34HH3OnH196m/sTVYLuFjd6ZLCJBJNHIs0bFtxOAZFC4G44yfrmvTprK28m0mtrm7Mc0yYZbyXDKef73euWv8AWPCk9pdTvBq2oJBN5TRLdSyHcf4hG0n3c8Z20AUfDXhzxBeXIe+jtorASOS24MXAckKgX5cHPJx/DxXojy6foOmruaKzs4htUHgL1OBXJeGtdjNnYabPYz2t1N5qottLmEKgyX+/lVG4DnvV7xRFc3XhK1mWFrraI3liGfMkJAAKlRndk9qANTSvE+ka0xWxu9zh2QLJE8ZJUkHAYDPQ9K2a8t8GW1xB4mjs/Kv4jGhuG+3b/lUlhhQyAZLE89fvV6lQBi+I9TtbHT2t5nbzrkFYY0GWcjGf8muPtfG4sNP0yP8AsPUZRaMiOVMIyzoQoAMmec1seOdAutRS3vbKZUMJAulLFfNhBzjj0Ncve2WhaV4d0u403S7CXUGcPOO5Xa2dxByO1AHb6HrcXiKW4uLaKe2JtYxiVRuQkuR0PauK+x6i8V3LHYRsEceUPvfaSMtxn+7k5J4+brXQ3Nxo3huyuJJVm063u7OPdIPMkWJmLADOfl6+1ZtzqulQPeSS6oUOmopmU6YwWBQTtB54HzUAb66nY6VoOhrf30MLSPG0fmP2OSAM88A4zXF+LdKv4b59Umn0y0LXI+ymFSWuk64Izt6Hkn+7XnnhpGuba61zxDoya4dUspfstzcuz4lVyiRKNpw/ynCg5xtxXT/Da0gTUb/w5r1nd3C6YsVxZxjfIbdZU3NGSACOSpx60Adh4ISS7ubG4tLKZYLe1uYrq6kkUq8kjo4Cgnd2/ugAcVak8dW/9n2tlb6VfzuoiJI2KPkw5+8w7Crnh+2t9AbU2X7bP9qmaRf9Dddg5O0gccE9gKztUj0rSYNJQaTbbLhYpJpvIDuVDLvGTz0PNAElr4usLnxm93PHc2kYtBCfNj3DcryZ5QkY5616CMkcDI9a8x1nw1Z6v4l8jQvs1sktrG7y2rbEwXk3HC8Enamf92vR4bWC2t4oI48RxoEQDoFHAoA434ipqE0WmW1vJepY3MxhuWtOqZGQzf7PGD6VF4V8KadYW2k3h8+6nuGLM124k2/IxwoxwMgGun8R3sVnoswlljjMuIkaRwoyTjkkgYHfmuX03WTYNpVvca1ot1bW77D9ndVk5UqD/rWzyegFADfiv/yKmq/9cYP/AEaa5nxP/qPiN/17L/6EK6D4sSlPDuoRtHhJIISkm7glZTkfgGBrn/E/+o+I/wD17L/6EKAOcsvCOsxarN4f0RNMu9Mtb2G9t01TcxhkIGcbRyhP3l77av2OnR6Kl3cyXNlrGuakZXumkViUYOwJQf3SAMZrptEuRbeN78n+IwfebGFGzJ59M1Je2KQeH9RsCRLe207zRWt06hyGfmXI+8Np4xQBH4eFs/iKwutOm04Ru8qzRRuxIkKEnGfUZx7V3dzbxXPhi1iljVkKQDntkqOPzrnls4E1nT3SePyNOjeWcW7LtgYpyrkct83TvWvrN7/Zvh6yhaW3guZPKVTdOqohXBOckZxjpmgDjdf0a50jWXh0a7v/ALR9nRkIf95I7vINpcDhflGOK9VTOxd3LY5NcLYasv8AwlkN7d6xpFx58ItcW0qL5ZG5gSPMYnJOK7ugDlfGPhu/8RNYrZXsNqsDOXeRGY8gAY2kH9a5L/hV2vojf8Ty2kQnLRCAIJM9c8Efmpr1nAowKAMbXvD1h4k0ZtL1CNmgO0hlbDIw6MDjqK5DUvBV/cPrcVzeq0OrxhJp4rVmKAEHhQ2cmvSMCjAoA881DwncQ3H2vS7S3vZruRY5JbtSGiQhQWAPTG3IwP4q1L3RRe6f5Oo2VteXO04hFs7KFPRRJkY49TXYYFGKAOZ0vw75LK88dtBEkglW0s4dibx0ZySSxHUYwPrVXxh4XvvEN5p0llcwW/2ffuklXd1xwAMNzjswrr8CjAoA8pj+GfiC33tHr1oSecGA8nvk554/vBq9UAG0A9QMU7AowKAP/9k=</Image>
            </item>
            <item type="NineRays.Reporting.DOM.Shape" Line="0 Solid Silver" Name="shape6" Size="897.6378; 35.43307" Location="448.8189; 11.81102">
              <ShapeStyle type="NineRays.Basics.Drawing.RectangleShape" />
              <ShadowFill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
              <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Silver" />
            </item>
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 10pt" TextAlign="TopLeft" Name="advancedText27" Size="838.5827; 177.1654" Location="460.6299; 82.67717" Text="MINISTERIO DE MEDIO AMBIENTE, &lt;br&gt;&#xD;&#xA;Y MEDIO RURAL MARINO &lt;br&gt;&#xD;&#xA;Confederación Hidrográfica del Segura" />
            <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 9.75pt, style=Bold" Name="advancedText28" Size="944.8819; 224.4095" Location="1417.323; 11.81102" Text="MODELO PARA CAPTACIONES MEDIANTE &lt;br&gt;&#xD;&#xA;TUBERÍA A PRESIÓN (100 l/s &lt;= Qmax &lt; 300 l/s)">
              <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand20" DataSource="dataSet.TablaAprovechamientos" Size="2480.315; 921.2598" Location="0; 460.6299">
          <Controls>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 165.3543" Name="detail25" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox197" Size="1062.992; 141.7323" Location="177.1654; 0" Text="Datos de inscripción del aprovechamiento">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox198" Size="472.4409; 141.7323" Location="1240.157; 0" Text="Catálogo de Aguas Privadas">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox200" Size="330.7087; 70.86614" Location="1712.598; 0" Text="Sección">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox201" Size="330.7087; 70.86614" Location="1712.598; 70.86614" Text="Nº inscripción">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox202" Size="318.8976; 70.86614" Location="2043.307; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand20[&quot;seccion&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox203" Size="318.8976; 70.86614" Location="2043.307; 70.86614">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand20[&quot;numpal&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox204" Size="2185.039; 23.62205" Location="177.1654; 141.7323">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 307.0866" Name="detail26" Location="0; 566.9291">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox205" Size="1535.433; 59.05512" Location="177.1654; 23.62205" Text="Datos identificativos de la toma">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox206" Size="531.4961; 59.05512" Location="1712.598; 23.62205" Text="Nº de toma (I, II, III,...)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox207" Size="118.1102; 59.05512" Location="2244.094; 23.62205">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox208" Size="791.3386; 59.05512" Location="177.1654; 82.67717" Text="Provincia">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox209" Size="744.0945; 59.05512" Location="968.504; 82.67717" Text="Término municipal">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox210" Size="649.6063; 59.05512" Location="1712.598; 82.67717" Text="Paraje">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox211" Size="791.3386; 59.05512" Location="177.1654; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand20[&quot;provi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox212" Size="744.0945; 59.05512" Location="968.504; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand20[&quot;termi&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox213" Size="649.6063; 59.05512" Location="1712.598; 141.7323">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand20[&quot;lugar&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox214" Size="2185.039; 23.62205" Location="177.1654; 0">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Name="textBox215" Size="2185.039; 23.62205" Location="177.1654; 200.7874">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 6.25pt" TextAlign="TopCenter" Name="textBox216" Size="1122.047; 82.67717" Location="1240.157; 224.4095" Text="Espacio reservado para fecha y sello de inspección de la Administración">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                  <Fill type="NineRays.Basics.Drawing.SolidFill" Color="Gainsboro" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox217" Size="1062.992; 82.67717" Location="177.1654; 224.4095" Text="AÑO 201_">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand21" DataSource="dataSet.TablaTitulares" Size="2480.315; 259.8425" Location="0; 259.8425">
              <Controls>
                <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 59.05512" Name="header11" Location="0; 47.24409">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 12pt, style=Bold" Name="textBox218" Size="2185.039; 59.05512" Location="177.1654; 0" Text="Titulares (NIF o CIF, Nombre y apellidos)">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
                <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail27" Location="0; 153.5433">
                  <Controls>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox219" Size="165.3543; 59.05512" Location="177.1654; 0" Text="DNI">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox220" Size="248.0315; 59.05512" Location="342.5197; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand21[&quot;CIF&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox221" Size="1122.047; 59.05512" Location="1240.157; 0">
                      <DataBindings>
                        <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand21[&quot;NombreYApellidos&quot;]" PropertyName="Value" />
                      </DataBindings>
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                    <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 10pt" Name="textBox222" Size="649.6063; 59.05512" Location="590.5512; 0" Text="Nombre y apellidos">
                      <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                    </item>
                  </Controls>
                </item>
              </Controls>
            </item>
          </Controls>
        </item>
        <item type="NineRays.Reporting.DOM.DataBand" Name="dataBand22" DataSource="dataSet.Datos2" Size="2480.315; 425.1968" Location="0; 1429.134">
          <Controls>
            <item type="NineRays.Reporting.DOM.Header" Size="2480.315; 118.1102" Name="header12" Location="0; 47.24409">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox223" Size="177.1654; 118.1102" Location="519.6851; 0" Text="Día que se realiza la anotación">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox224" Size="118.1102; 118.1102" Location="177.1654; 0" Text="Mes">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText29" Size="259.8425; 118.1102" Location="696.8504; 0" Text="Lectura&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.AdvancedText" Font="Arial, 7.25pt" Name="advancedText30" Size="283.4646; 118.1102" Location="956.6929; 0" Text="Lectura Actual - &lt;br&gt;Lectura Anterior&lt;br&gt;( m3 )">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox225" Size="425.1968; 118.1102" Location="1240.157; 0" Text="Persona que hace la lectura">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox226" Size="696.8504; 118.1102" Location="1665.354; 0" Text="Observaciones">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox227" Size="224.4095; 118.1102" Location="295.2756; 0" Text="Semana">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Detail" Size="2480.315; 59.05512" Name="detail28" Location="0; 212.5984">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox228" Size="118.1102; 59.05512" Location="177.1654; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand22[&quot;mes&quot;]" PropertyName="Value" />
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand22[&quot;tamanyo&quot;]" PropertyName="Size" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox229" Size="224.4095; 59.05512" Location="295.2756; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand22[&quot;semana&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox230" Size="177.1654; 59.05512" Location="519.6851; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand22[&quot;fecha_medidaS&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox231" Size="283.4646; 59.05512" Location="956.6929; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand22[&quot;diferencia&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox232" Size="425.1968; 59.05512" Location="1240.157; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand22[&quot;usuario&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox233" Size="696.8504; 59.05512" Location="1665.354; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand22[&quot;observaciones&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 7.25pt" Name="textBox234" Size="259.8425; 59.05512" Location="696.8504; 0">
                  <DataBindings>
                    <item type="NineRays.Reporting.DOM.ReportDataBinding" Expression="dataBand22[&quot;lectura&quot;]" PropertyName="Value" />
                  </DataBindings>
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
              </Controls>
            </item>
            <item type="NineRays.Reporting.DOM.Footer" Size="2480.315; 59.05512" Name="footer4" Location="0; 318.8976">
              <Controls>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt, style=Bold" Name="textBox235" Size="779.5276; 59.05512" Location="177.1654; 0" Text="VOLUMEN AÑO (m3)">
                  <Border type="NineRays.Basics.Drawing.Border" All="1 Solid Black" />
                </item>
                <item type="NineRays.Reporting.DOM.TextBox" Font="Arial, 8.25pt" Name="textBox236" Size="283.4646; 59.05512" Location="956.6929; 0">
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