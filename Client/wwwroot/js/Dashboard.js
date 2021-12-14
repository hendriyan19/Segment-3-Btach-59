$.ajax({
	url: "https://localhost:4321/API/Employees",
	success: function (result) {
		var male = 0
		var female = 0

		for (var i = 0; i < result.length; i++) {
			if (result[i].gender == 0) {
				male++
			}
			if (result[i].gender == 1) {
				female++
			}

		}
		var options = {
			chart: {
				width: 380,
                type: "donut",
                toolbar: {
                    show: true,
                    offsetX: 120,
                    offsetY: 0,
                    tools: {
                        download: true,
                        selection: true,
                        zoom: true,
                        zoomin: true,
                        zoomout: true,
                        pan: true,
                        reset: true | '<img src="/static/icons/reset.png" width="20">',
                        customIcons: []
                    },
                    export: {
                        csv: {
                            filename: undefined,
                            columnDelimiter: ',',
                            headerCategory: 'category',
                            headerValue: 'value',
                            dateFormatter(timestamp) {
                                return new Date(timestamp).toDateString()
                            }
                        },
                        svg: {
                            filename: undefined,
                        },
                        png: {
                            filename: undefined,
                        }
                    },
                    autoSelected: 'zoom'
                }
			},
			dataLabels: {
				enabled: false
			},
			series: [male, female],
			labels: ["Male", "Female"]
		};

		var chart = new ApexCharts(document.querySelector("#chart"), options);
		chart.render();


	}

});


$.ajax({
    url: "https://localhost:4321/API/University/GetUniversity",
    success: function (result) {
        var valUniv = [];
        var nameUniv = [];

        for (var i = 0; i < result.result.length; i++) {
            valUniv[i] = result.result[i].val;
            nameUniv[i] = result.result[i].name;
        }

        var options = {
            series: [
                {
                    data: valUniv
                }
            ],
            chart: {
                type: "bar",
                height: 350
            },
            plotOptions: {
                bar: {
                    horizontal: true,
                    distributed: true,
                    startingShape: "rounded",
                    endingShape: "rounded",
                    colors: {
                        backgroundBarColors: ["#eee"],
                        backgroundBarOpacity: 1,
                        backgroundBarRadius: 9
                    }
                }
            },
            dataLabels: {
                enabled: false
            },
            grid: {
                yaxis: {
                    lines: {
                        show: false
                    }
                }
            },
            xaxis: {
                axisBorder: {
                    show: false
                },
                categories: nameUniv
            },
            legend: {
                show: false
            }
        };

        var chart = new ApexCharts(document.querySelector("#chart2"), options);
        chart.render();
    }
});

$.ajax({
    url: "https://localhost:4321/API/Employees",
    success: function (result) {
        var yearsOld = [];
        var yearTemp = [];
        var age = [];
        var salary = [];
        for (var i = 0; i < result.length; i++) {
            salary[i] = result[i].salary;
        }
        for (var i = 0; i < result.length; i++) {
            yearsOld[i] = result[i].birthDate;
        }
        for (var i = 0; i < result.length; i++) {
            yearTemp[i] = new Date(yearsOld[i]);
        }
        for (var i = 0; i < result.length; i++) {
            age[i] = 2021 - (yearTemp[i].getFullYear());
        }
        var options = {
            chart: {
                height: 350,
                type: 'line',
                zoom: {
                    enabled: false
                }
            },
            series: [{
                name: "salary",
                data: salary
            }],
            dataLabels: {
                enabled: false
            },
            stroke: {
                curve: 'straight'
            },
            title: {
                text: 'Chart Salary by Age',
                align: 'left'
            },
            grid: {
                row: {
                    colors: ['#f3f3f3', 'transparent'], // takes an array which will be repeated on columns
                    opacity: 0.5
                },
            },
            tooltip: {
                shared: false,
                intersect: true
            },
            markers: {
                size: 6,
                onClick: function (e) {
                    var seriesIndex = e.target.getAttribute('index')
                    var dataPointIndex = e.target.getAttribute('j')
                    console.log(new Date(chart.w.globals.seriesX[seriesIndex][dataPointIndex]))
                },
                onDblClick: function (e) {
                    console.log(e)
                }
            },
            labels: age,

            xaxis: {
                type: 'text',
            }
        }

        var chart = new ApexCharts(
            document.querySelector("#chart3"),
            options
        );

        chart.render();
        
        



    }

});

