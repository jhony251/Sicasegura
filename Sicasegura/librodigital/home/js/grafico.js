function graficoHM() {

    jQuery(function() {
        var chart2 = new Highcharts.Chart({
            chart: { backgroundColor: '#ffffff', renderTo: 'chart2' },
            rangeSelector: { selected: 1, inputEnabled: false },
            title: { text: 'VOLÚMENES SUMINISTRADOS EN EL AÑO HIDROLÓGICO' },
            legend: {
                enabled: true
            },

            xAxis: { title: {
                text: 'Año Hidrológico'
            }, type: 'datetime'
            },
            yAxis: {
                title: {
                    text: 'hm3'
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'}]
                },
                plotOptions: {
                    area: {
                        pointStart: 1940,
                        marker: {
                            enabled: false,
                            symbol: 'circle',
                            radius: 0,
                            states: {
                                hover: {
                                    enabled: true
                                }
                            }
                        }
                    },
                    line: {
                        pointStart: 1940,
                        marker: {
                            enabled: false,
                            symbol: 'circle',
                            radius: 0,
                            states: {
                                hover: {
                                    enabled: true
                                }
                            }
                        }
                    }
                },

                series: [{ name: 'Suministrado(hm3)', type: 'area', data: Values, tooltip: { yDecimals: 2} }, { name: 'Concesión (hm3)', data: Values2, type: 'line', tooltip: { yDecimals: 2}}]
            });
        });
    }

    function graficoM3() {

        jQuery(function() {
            var chart2 = new Highcharts.Chart({
                chart: { backgroundColor: '#ffffff', renderTo: 'chart2' },
                rangeSelector: { selected: 1, inputEnabled: false },
                title: { text: 'VOLÚMENES SUMINISTRADOS EN EL AÑO HIDROLÓGICO' },
                legend: {
                    enabled: true
                },

                xAxis: { title: {
                    text: 'Año Hidrológico'
                }, type: 'datetime'
                },
                yAxis: {
                    title: {
                        text: 'm3'
                    },
                    labels: {
                        align: 'left',
                        x: 3,
                        y: 16,
                        format: '{value:.,0f}'
                    },showFirstLabel: false,
                    plotLines: [{
                        value: 0,
                        width: 1,
                        color: '#808080'}]
                    },
                    plotOptions: {
                        area: {
                            pointStart: 1940,
                            marker: {
                                enabled: false,
                                symbol: 'circle',
                                radius: 0,
                                states: {
                                    hover: {
                                        enabled: true
                                    }
                                }
                            }
                        },
                        line: {
                            pointStart: 1940,
                            marker: {
                                enabled: false,
                                symbol: 'circle',
                                radius: 0,
                                states: {
                                    hover: {
                                        enabled: true
                                    }
                                }
                            }
                        }
                    },

                    series: [{ name: 'Suministrado(m3)', type: 'area', data: Values, tooltip: { yDecimals:0} }, { name: 'Concesión (m3)', data: Values2, type: 'line', tooltip: { yDecimals: 0}}]
                });
            });
        }