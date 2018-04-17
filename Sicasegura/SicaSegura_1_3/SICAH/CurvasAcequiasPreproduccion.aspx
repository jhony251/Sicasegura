<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CurvasAcequiasPreproduccion.aspx.vb" Inherits="SICAH_CurvasAcequiasPreproduccion" %>
<%@ Register Assembly="dotnetCHARTING" Namespace="dotnetCHARTING" TagPrefix="dotnetCHARTING" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SICA - Sistema Integrado de Control de Aprovechamientos</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

    <asp:Panel ID="pnlGraficaCurvas" runat="server">
                           <dotnetCHARTING:Chart ID="ChartSeriesDeCurvasAcequia" runat="server" Width="600px" Height="400px" Type="Scatter"
                                                        >
                    <DefaultTitleBox>
                        <Label GlowColor="" Type="UseFont">
                        </Label>
                        <HeaderLabel GlowColor="" Type="UseFont">
                        </HeaderLabel>
                        <Background ShadingEffectMode="None" />
                        <HeaderBackground ShadingEffectMode="None" />
                    </DefaultTitleBox>
                    <DefaultLegendBox CornerBottomRight="Cut" Padding="4">
                        <LabelStyle GlowColor="" Type="UseFont" />
                        <Background ShadingEffectMode="None" />
                        <DefaultEntry ShapeType="None">
                            <LabelStyle GlowColor="" Type="UseFont" />
                            <Background ShadingEffectMode="None" />
                        </DefaultEntry>
                        <HeaderLabel GlowColor="" Type="UseFont">
                        </HeaderLabel>
                        <HeaderEntry ShapeType="None" Visible="False">
                            <LabelStyle GlowColor="" Type="UseFont" />
                            <Background ShadingEffectMode="None" />
                        </HeaderEntry>
                        <HeaderBackground ShadingEffectMode="None" />
                    </DefaultLegendBox>
                    <SmartForecast Start="" TimeSpan="00:00:00" />
                    <NoDataLabel GlowColor="" Type="UseFont">
                    </NoDataLabel>
                    <DefaultElement ShapeType="None">
                        <SmartLabel GlowColor="" Type="UseFont">
                        </SmartLabel>
                        <DefaultSubValue Name="">
                        </DefaultSubValue>
                        <LegendEntry ShapeType="None">
                            <LabelStyle GlowColor="" Type="UseFont" />
                            <Background ShadingEffectMode="None" />
                        </LegendEntry>
                    </DefaultElement>
                    <Background ShadingEffectMode="None" />
                    <ChartArea CornerTopLeft="Square" StartDateOfYear="">
                        <Label Font="Tahoma, 8pt" GlowColor="" Type="UseFont">
                        </Label>
                        <DefaultElement ShapeType="None">
                            <SmartLabel GlowColor="" Type="UseFont">
                            </SmartLabel>
                            <DefaultSubValue Name="">
                            </DefaultSubValue>
                            <LegendEntry ShapeType="None">
                                <LabelStyle GlowColor="" Type="UseFont" />
                                <Background ShadingEffectMode="None" />
                            </LegendEntry>
                        </DefaultElement>
                        <Background ShadingEffectMode="None" />
                        <TitleBox Position="Left">
                            <Label Color="Black" GlowColor="" Type="UseFont">
                            </Label>
                            <HeaderLabel GlowColor="" Type="UseFont">
                            </HeaderLabel>
                            <Background ShadingEffectMode="None" />
                            <HeaderBackground ShadingEffectMode="None" />
                        </TitleBox>
                        <LegendBox CornerBottomRight="Cut" Padding="4" Orientation="TopRight">
                            <LabelStyle GlowColor="" Type="UseFont" />
                            <Background ShadingEffectMode="None" />
                            <DefaultEntry ShapeType="None">
                                <LabelStyle GlowColor="" Type="UseFont" />
                                <Background ShadingEffectMode="None" />
                            </DefaultEntry>
                            <HeaderLabel GlowColor="" Type="UseFont">
                            </HeaderLabel>
                            <HeaderEntry Name="Name" ShapeType="None" SortOrder="-1" Value="Value" Visible="False">
                                <LabelStyle GlowColor="" Type="UseFont" Font="Arial, 8pt, style=Bold" />
                                <Background ShadingEffectMode="None" />
                            </HeaderEntry>
                            <HeaderBackground ShadingEffectMode="None" />
                        </LegendBox>
                        <XAxis GaugeLabelMode="Default" GaugeNeedleType="One" SmartScaleBreakLimit="2" TimeInterval="Minutes">
                            <TimeScaleLabels MaximumRangeRows="4">
                            </TimeScaleLabels>
                            <ScaleBreakLine Color="Gray" />
                            <ZeroTick>
                                <Line Length="3" />
                                <Label GlowColor="" Type="UseFont">
                                </Label>
                            </ZeroTick>
                            <MinorTimeIntervalAdvanced Start="" TimeSpan="00:00:00" />
                            <Label Alignment="Center" Font="Arial, 9pt, style=Bold" GlowColor="" LineAlignment="Center"
                                                                    Type="UseFont">
                            </Label>
                            <TimeIntervalAdvanced Start="" TimeSpan="00:00:00" />
                            <AlternateGridBackground ShadingEffectMode="None" />
                            <DefaultTick>
                                <Line Length="3" />
                                <Label GlowColor="" Text="%Value" Type="UseFont">
                                </Label>
                            </DefaultTick>
                        </XAxis>
                        <YAxis GaugeLabelMode="Default" GaugeNeedleType="One" SmartScaleBreakLimit="2" TimeInterval="Minutes">
                            <TimeScaleLabels MaximumRangeRows="4">
                            </TimeScaleLabels>
                            <ScaleBreakLine Color="Gray" />
                            <ZeroTick>
                                <Line Length="3" />
                                <Label GlowColor="" Type="UseFont">
                                </Label>
                            </ZeroTick>
                            <MinorTimeIntervalAdvanced Start="" TimeSpan="00:00:00" />
                            <Label Alignment="Center" Font="Arial, 9pt, style=Bold" GlowColor="" LineAlignment="Center"
                                                                    Type="UseFont">
                            </Label>
                            <TimeIntervalAdvanced Start="" TimeSpan="00:00:00" />
                            <AlternateGridBackground ShadingEffectMode="None" />
                            <DefaultTick>
                                <Line Length="3" />
                                <Label GlowColor="" Text="%Value" Type="UseFont">
                                </Label>
                            </DefaultTick>
                        </YAxis>
                    </ChartArea>
                    <TitleBox Position="Left">
                        <Label Color="Black" GlowColor="" Type="UseFont">
                        </Label>
                        <HeaderLabel GlowColor="" Type="UseFont">
                        </HeaderLabel>
                        <Background ShadingEffectMode="None" />
                        <HeaderBackground ShadingEffectMode="None" />
                    </TitleBox>
                </dotnetCHARTING:Chart>
    </asp:Panel> 
    </div>
    </form>
</body>
</html>
