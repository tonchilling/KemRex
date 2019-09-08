var BarOption = {
    title: {
        x: 'center',
        text: '',
        subtext: '',
        link: ''
    },
    tooltip: {
        trigger: 'item'
    },
    toolbox: {
        show: true,
        feature: {
            dataView: { show: false, readOnly: false },
            restore: { show: true },
            saveAsImage: { show: true }
        }
    },
    calculable: true,
    grid: {
        borderWidth: 0,
        y: 80,
        y2: 60
    },
    xAxis: [
        {
            type: 'category',
            show: true,
            data: null,
            axisLabel: {
                show: true,
                margin: 2,
                formatter: function (params) {
                    return (params != null && params.length > 10) ? params.substring(0, 10) + '..' : params;
                }
            }
        }
    ],
    yAxis: [
        {
            type: 'value',
            show: true,
            axisLabel: {
                show: true,
                margin: 2,
                formatter: function (params) {
                    return numeral(params).format('0.0a');  
                }
            },
            splitLine: {
                show: true,
                shadowColor: 'rgba(0, 0, 0, 0.5)',
                shadowBlur: 10,
                lineStyle: {
                    color: '#ccc',
                    width: 2
                },
                shadowBlur: 10,
            },
            splitArea: {
                show: true,
                color: ['rgba(250,250,250,0.8)', 'rgba(200,200,200,0.8)'],
                shadowColor: '#F8FFFF',
                shadowBlur: 10,
            }

        }
    ],
    series: [
        {
            name: '',
            type: 'bar',
            itemStyle: {
                normal: {
                    color: function (params) {
                        var colorList = [
                            '#C1232B', '#B5C334', '#FCCE10', '#E87C25', '#27727B',
                            '#FE8463', '#9BCA63', '#FAD860', '#F3A43B', '#60C0DD',
                            '#D7504B', '#C6E579', '#F4E001', '#F0805A', '#26C0C0'
                        ];
                        return colorList[params.dataIndex]
                    },
                    label: {
                        show: true,
                        position: 'top',
                        formatter: function (params) {
                            return numeral(params.value).format('0.0a');
                        } 
                    }
                }
            },
            data: null,
            markPoint: {
                tooltip: {
                    trigger: 'item',
                    backgroundColor: 'rgba(0,0,0,0)',
                    formatter: function (params) {
                        return '';
                    }
                },

            }
        }
    ]
};

var Bar2Option = {
    title: {
        x: 'center',
        text: '',
        subtext: '',
        link: ''
    },
    legend: {
        data:null
    },
    tooltip: {
        trigger: 'item'
    },
    toolbox: {
        show: true,
        feature: {
            dataView: { show: false, readOnly: false },
            restore: { show: true },
            saveAsImage: { show: true }
        }
    },
    calculable: true,
    grid: {
        borderWidth: 0,
        y: 80,
        y2: 60 },
    xAxis: [
        {
            type: 'category',
            show: true,
            data: null,
            axisLabel: {
                show: true,
                margin: 2,
                formatter: function (params) {
                    return (params != null && params.length > 10) ? params.substring(0, 10) + '..' : params;
                }
            }
        }
    ],
    yAxis: [
        {
            type: 'value',
            show: true,
            axisLabel: {
                show: true,
                margin: 2,
                formatter: function (params) {
                    return numeral(params).format('0.0a');
                }
            },
            splitLine: {
                show: true,
                shadowColor: 'rgba(0, 0, 0, 0.5)',
                shadowBlur: 10,
                lineStyle: {
                    color: '#ccc',
                    width: 2
                },
                shadowBlur: 10,
            },
            splitArea: {
                show: true,
                color: ['rgba(250,250,250,0.8)', 'rgba(200,200,200,0.8)'],
                shadowColor: '#F8FFFF',
                shadowBlur: 10,
            }

        }
    ],
    series: [
        {
            name: '',
            type: 'bar',
            itemStyle: {
                normal: {
                
                    color: '#FCCE10',
                    
                   
                    label: {
                        show: true,
                        position: 'top',
                        formatter: function (params) {
                            return numeral(params.value).format('0.0a');
                        }
                    }
                },
                emphasis: {
                    color: '#9BCA63'
                }
            },
            data: null,
            markPoint: {
                tooltip: {
                    trigger: 'item',
                    backgroundColor: 'rgba(0,0,0,0)',
                    formatter: function (params) {
                        return '';
                    }
                },

            }
        },
        {
            name: '',
            type: 'bar',
            itemStyle: {
                normal: {
                    color: '#1e90ff',
                    
                    label: {
                        show: true,
                        position: 'top',
                        formatter: function (params) {
                            return numeral(params.value).format('0.0a');
                        }
                    }
                },emphasis: {
                    color: '#9BCA63'
                },
            },
            data: null,
            markPoint: {
                tooltip: {
                    trigger: 'item',
                    backgroundColor: 'rgba(55,0,0,0)',
                    formatter: function (params) {
                        return '';
                    }
                },

            }
        }
    ]
};
var BarHorizentalOption = {
    title: {
        x: 'center',
        text: '',
        subtext: '',
        link: ''
    },
    tooltip: {
        trigger: 'item'
    },
    toolbox: {
        show: true,
        feature: {
            dataView: { show: true, readOnly: false },
            restore: { show: true },
            saveAsImage: { show: true }
        }
    },
    calculable: true,
    grid: {
        borderWidth: 0,
        y: 80,
        y2: 60
    },
    xAxis: [
        {
            type: 'value',
            show: true,
            axisLabel: {
                show: true,
                margin: 2,
                formatter: function (params) {
                    return numeral(params).format('0.0a'); 
                }
            }
        }
    ],
    yAxis: [
        {
            type: 'category',
            show: true,
            data: null,
            axisLabel: {
                show: true,
                margin: 2,
                formatter: function (params) {
                    return (params != null && params.length > 10) ? params.substring(0, 10) + '..' : params;
                }
            },
            splitLine: {
                show: true,
                shadowColor: 'rgba(0, 0, 0, 0.5)',
                shadowBlur: 10,
                lineStyle: {
                    color: '#ccc',
                    width: 2
                },
                shadowBlur: 10,
            },
            splitArea: {
                show: true,
                color: ['rgba(250,250,250,0.8)', 'rgba(200,200,200,0.8)'],
                shadowColor: '#F8FFFF',
                shadowBlur: 10,
            }

        }
    ],
    series: [
        {
            name: '',
            type: 'bar',
            itemStyle: {
                normal: {
                    color: function (params) {
                        var colorList = [
                            '#C1232B', '#B5C334', '#FCCE10', '#E87C25', '#27727B',
                            '#FE8463', '#9BCA63', '#FAD860', '#F3A43B', '#60C0DD',
                            '#D7504B', '#C6E579', '#F4E001', '#F0805A', '#26C0C0'
                        ];
                        return colorList[params.dataIndex]
                    },
                    label: {
                        show: false,
                        position: 'insideRight',
                        formatter: function (params) {
                            return numeral(params.value).format('0.0a');
                        } 
                    }
                }
            },
            data: null,
            markPoint: {
                tooltip: {
                    trigger: 'item',
                    backgroundColor: 'rgba(0,0,0,0)',
                    formatter: function (params) {
                        return '';
                    }
                },

            }
        }
    ]
};

PieOption = {
    title: {
        text: '',
        subtext: '',
        x: 'center'
    },
    tooltip: {
        trigger: 'item',
        formatter: "{a} <br/>{b} : {c} ({d}%)"
    },
    legend: {
        orient: 'vertical',
        x: 'left',
        show: true,
        itemWidth: 10,
        data: null
    },
    toolbox: {
        show: false,
        feature: {
            mark: { show: true },
            dataView: { show: true, readOnly: false },
            magicType: {
                show: true,
                type: ['pie', 'funnel'],
                option: {
                    funnel: {
                        x: '25%',
                        width: '50%',
                        funnelAlign: 'left',
                        max: 1548
                    }
                }
            },
            restore: { show: true },
            saveAsImage: { show: true }
        }
    },
    calculable: true,
    series: [
        {
            name: '',
            type: 'pie',
            radius: '55%',
            center: ['50%', '60%'],
            data: null,
            itemStyle: {
                normal: {
                    label: {
                        show: false
                    }
                }
            }
        }
    ],
    scaleLabel:
        function (label) { return numeral(label.value).format('0.0a');  }
};


var CardOption = {
    title: {
        x: 'center',
        text: '',
        subtext: '',
        link: ''
    },
    xAxisData: [],
    yAxisData:[],
    ConvertToHTML: function (display,value,width,bgclass) {
        var html = ' <div class="col-xs-' + width+'">';
        if (bgclass == null) {
            html += '<div class="card  bg-info">';
        } else {
            html += '<div class="card  ' + bgclass+'">';
        }
        html += '<div class="card-body">';
        html += '<div class="row align-items-center">';
        html += '<div class="col-xs-9">';
        html += ' <h2 class="card-title ">';
        html += display
        html += '</h2>'
        html += '<span class="h2 mb-0">'
        html += value;
        html += '</span>';
        html += '</div>';
        html += '<div class="col-xs-3">';
        html += '<i class=" fa fa-television fa-3x"></i>';
        html += ' </div>';
        html += '</div>';
        html += '</div>';
        html += '</div>';
        html += '</div>';
        return html;
    }

}

var ChartType = 

    [
        { ID: 1,Name:'Bar Chart', Option: BarOption },       
        { ID: 2, Name: 'Bar Chart(Horizontal)', Option: BarHorizentalOption }, 
        { ID: 3, Name: 'Pie Chart', Option: PieOption },
        { ID: 4, Name: 'CardOption', Option: CardOption },
        { ID: 5, Name: 'Bar Chart2', Option: Bar2Option },       
        ]

