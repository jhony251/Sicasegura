<%@ Page Language="VB" AutoEventWireup="false" CodeFile="perfil_acequia.aspx.vb" Inherits="SICAH_perfil_acequia" %>

<%@ Register Assembly="dotnetCHARTING" Namespace="dotnetCHARTING" TagPrefix="dotnetCHARTING" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>C.H. Segura</title>
           <script language="javascript">
    function redimensiona(){
        
        //window.resizeTo(document.getElementById('grafico').offsetWidth,  document.getElementById('grafico').offsetHeight+50);        
           }
    </script>

</head>
<body onload="redimensiona();">
    <form id="form1" runat="server">
    <div>
        <dotnetcharting:chart id="grafico" runat="server" backcolor="DarkKhaki" borderstyle="Outset"
            borderwidth="2px" type="Scatter">
<DefaultTitleBox>
<Label GlowColor="" Type="UseFont"></Label>

<HeaderLabel GlowColor="" Type="UseFont"></HeaderLabel>

<Background ShadingEffectMode="None"></Background>

<HeaderBackground ShadingEffectMode="None"></HeaderBackground>
</DefaultTitleBox>

<DefaultLegendBox CornerBottomRight="Cut" Padding="4">
<LabelStyle GlowColor="" Type="UseFont"></LabelStyle>

<Background ShadingEffectMode="None"></Background>

<DefaultEntry ShapeType="None">
<LabelStyle GlowColor="" Type="UseFont"></LabelStyle>

<Background ShadingEffectMode="None"></Background>
</DefaultEntry>

<HeaderLabel GlowColor="" Type="UseFont"></HeaderLabel>

<HeaderEntry ShapeType="None" Visible="False">
<LabelStyle GlowColor="" Type="UseFont"></LabelStyle>

<Background ShadingEffectMode="None"></Background>
</HeaderEntry>

<HeaderBackground ShadingEffectMode="None"></HeaderBackground>
</DefaultLegendBox>
<SeriesCollection>
<dotnetCHARTING:Series GaugeType="Circular" GaugeBorderShape="Default" GaugeLinearStyle="Normal">
<Background ShadingEffectMode="None"></Background>

<GaugeBorderBox>
<Label GlowColor="" Type="UseFont"></Label>

<HeaderLabel GlowColor="" Type="UseFont"></HeaderLabel>

<Background ShadingEffectMode="None"></Background>

<HeaderBackground ShadingEffectMode="None"></HeaderBackground>
</GaugeBorderBox>

<LegendEntry ShapeType="None">
<LabelStyle GlowColor="" Type="UseFont"></LabelStyle>

<Background ShadingEffectMode="None"></Background>
</LegendEntry>

<EmptyElement>
<SmartLabel Type="UseFont" GlowColor=""></SmartLabel>
</EmptyElement>
</dotnetCHARTING:Series>
</SeriesCollection>

<SmartForecast Start="" TimeSpan="00:00:00"></SmartForecast>

<NoDataLabel GlowColor="" Type="UseFont"></NoDataLabel>

<DefaultElement ShapeType="None">
<SmartLabel Type="UseFont" GlowColor=""></SmartLabel>

<DefaultSubValue Name=""></DefaultSubValue>

<LegendEntry ShapeType="None">
<LabelStyle GlowColor="" Type="UseFont"></LabelStyle>

<Background ShadingEffectMode="None"></Background>
</LegendEntry>
</DefaultElement>

<Background ShadingEffectMode="None"></Background>

<ChartArea StartDateOfYear="" CornerTopLeft="Square">
<Label GlowColor="" Type="UseFont" Font="Tahoma, 8pt"></Label>

<DefaultElement ShapeType="None">
<SmartLabel Type="UseFont" GlowColor=""></SmartLabel>

<DefaultSubValue Name="">
<Line Length="4"></Line>
</DefaultSubValue>

<LegendEntry ShapeType="None">
<LabelStyle GlowColor="" Type="UseFont"></LabelStyle>

<Background ShadingEffectMode="None"></Background>
</LegendEntry>
</DefaultElement>

<Background ShadingEffectMode="None"></Background>

<TitleBox Position="Left">
<Label GlowColor="" Color="Black" Type="UseFont"></Label>

<HeaderLabel GlowColor="" Type="UseFont"></HeaderLabel>

<Background ShadingEffectMode="None"></Background>

<HeaderBackground ShadingEffectMode="None"></HeaderBackground>
</TitleBox>

<LegendBox CornerBottomRight="Cut" Padding="4" Orientation="TopRight">
<LabelStyle GlowColor="" Type="UseFont"></LabelStyle>

<Background ShadingEffectMode="None"></Background>

<DefaultEntry ShapeType="None">
<LabelStyle GlowColor="" Type="UseFont"></LabelStyle>

<Background ShadingEffectMode="None"></Background>
</DefaultEntry>

<HeaderLabel GlowColor="" Type="UseFont"></HeaderLabel>

<HeaderEntry Value="Value" ShapeType="None" Visible="False" Name="Name" SortOrder="-1">
<LabelStyle GlowColor="" Type="UseFont" Font="Arial, 8pt, style=Bold"></LabelStyle>

<Background ShadingEffectMode="None"></Background>
</HeaderEntry>

<HeaderBackground ShadingEffectMode="None"></HeaderBackground>
</LegendBox>

<XAxis SmartScaleBreakLimit="2" GaugeNeedleType="One" GaugeLabelMode="Default" TimeInterval="Minutes">
<TimeScaleLabels MaximumRangeRows="4"></TimeScaleLabels>

<ScaleBreakLine Color="Gray"></ScaleBreakLine>

<ZeroTick>
<Line Length="3"></Line>

<Label GlowColor="" Type="UseFont"></Label>
</ZeroTick>

<MinorTimeIntervalAdvanced Start="" TimeSpan="00:00:00"></MinorTimeIntervalAdvanced>

<Label GlowColor="" Type="UseFont" LineAlignment="Center" Alignment="Center" Font="Arial, 9pt, style=Bold"></Label>

<TimeIntervalAdvanced Start="" TimeSpan="00:00:00"></TimeIntervalAdvanced>

<AlternateGridBackground ShadingEffectMode="None"></AlternateGridBackground>

<DefaultTick>
<Line Length="3"></Line>

<Label GlowColor="" Type="UseFont" Text="%Value"></Label>
</DefaultTick>
</XAxis>

<YAxis SmartScaleBreakLimit="2" GaugeNeedleType="One" GaugeLabelMode="Default" TimeInterval="Minutes">
<TimeScaleLabels MaximumRangeRows="4"></TimeScaleLabels>

<ScaleBreakLine Color="Gray"></ScaleBreakLine>

<ZeroTick>
<Line Length="3"></Line>

<Label GlowColor="" Type="UseFont"></Label>
</ZeroTick>

<MinorTimeIntervalAdvanced Start="" TimeSpan="00:00:00"></MinorTimeIntervalAdvanced>

<Label GlowColor="" Type="UseFont" LineAlignment="Center" Alignment="Center" Font="Arial, 9pt, style=Bold"></Label>

<TimeIntervalAdvanced Start="" TimeSpan="00:00:00"></TimeIntervalAdvanced>

<AlternateGridBackground ShadingEffectMode="None"></AlternateGridBackground>

<DefaultTick>
<Line Length="3"></Line>

<Label GlowColor="" Type="UseFont" Text="%Value"></Label>
</DefaultTick>
</YAxis>
</ChartArea>

<TitleBox Position="Left">
<Label GlowColor="" Color="Black" Type="UseFont"></Label>

<HeaderLabel GlowColor="" Type="UseFont"></HeaderLabel>

<Background ShadingEffectMode="None"></Background>

<HeaderBackground ShadingEffectMode="None"></HeaderBackground>
</TitleBox>
</dotnetcharting:chart>
    
    </div>
    </form>
</body>
</html>

