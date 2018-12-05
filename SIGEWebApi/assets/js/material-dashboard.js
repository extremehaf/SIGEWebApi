/*!

 =========================================================
 * Material Dashboard - v2.1.0
 =========================================================

 * Product Page: https://www.creative-tim.com/product/material-dashboard
 * Copyright 2018 Creative Tim (http://www.creative-tim.com)

 * Designed by www.invisionapp.com Coded by www.creative-tim.com

 =========================================================

 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

 */

(function () {
    isWindows = navigator.platform.indexOf('Win') > -1 ? true : false;

    if (isWindows) {
        // if we are on windows OS we activate the perfectScrollbar function
        $('.sidebar .sidebar-wrapper, .main-panel').perfectScrollbar();

        $('html').addClass('perfect-scrollbar-on');
    } else {
        $('html').addClass('perfect-scrollbar-off');
    }
})();


var breakCards = true;

var searchVisible = 0;
var transparent = true;

var transparentDemo = true;
var fixedTop = false;

var mobile_menu_visible = 0,
    mobile_menu_initialized = false,
    toggle_initialized = false,
    bootstrap_nav_initialized = false;

var seq = 0,
    delays = 80,
    durations = 500;
var seq2 = 0,
    delays2 = 80,
    durations2 = 500;

$(document).ready(function () {

    $('body').bootstrapMaterialDesign();

    $sidebar = $('.sidebar');

    md.initSidebarsCheck();

    window_width = $(window).width();

    // check if there is an image set for the sidebar's background
    md.checkSidebarImage();

    //    Activate bootstrap-select
    if ($(".selectpicker").length != 0) {
        $(".selectpicker").selectpicker();
    }

    //  Activate the tooltips
    $('[rel="tooltip"]').tooltip();

    $('.form-control').on("focus", function () {
        $(this).parent('.input-group').addClass("input-group-focus");
    }).on("blur", function () {
        $(this).parent(".input-group").removeClass("input-group-focus");
    });

    // remove class has-error for checkbox validation
    $('input[type="checkbox"][required="true"], input[type="radio"][required="true"]').on('click', function () {
        if ($(this).hasClass('error')) {
            $(this).closest('div').removeClass('has-error');
        }
    });

    var jqxhr = jQuery.get("http://sigemv.azurewebsites.net/Faturamento", function () {
    }).done(function (data) {
            
        var valorTotal = parseFloat(data);
            //$.each(data, function (i, val) {
            //    var valor = parseFloat(val.valor);
            //    valorTotal += valor;
            //});

            $("#totalVendas").html("R$" + valorTotal.toFixed(1));
        })
        .fail(function (data) {
            console.log(data);
            $("#totalVendas").html("R$ 0,00");
        });

    var jqxhr = jQuery.get("https://sigerh.azurewebsites.net/api/Funcionarios", function () {
    }).done(function (data) {
        $("#bodyFuncionario").html("");
        $.each(data, function (i, val) {
            var tr = $("<tr\>");
            tr.append($("<td>").append(val.Id));
            tr.append($("<td>").append(val.Nome));
            var str = val.Matricula;
            var pad = "0000";
            tr.append($("<td>").append(pad.substring(0, pad.length - str.length) + str));
            tr.append($("<td>").append(val.Cargo));
            tr.append($("<td>").append(val.Turno));
            $("#bodyFuncionario").append(tr);
        });
    }).fail(function (data) {
        console.log(data);
    });

    var jqxhr = jQuery.get("https://trabalhosige.azurewebsites.net/api/Conta_Pagar", function () {
    }).done(function (data) {
        var valorTotal = 0;
        $.each(data, function (i, val) {
            if (val.setor == "Recursos Humanos") {
                $.each(val.ContasPagar, function (j, _val) {
                    var mes = new Date(_val.vencimento).getMonth()
                    if (mes == new Date().getMonth() || mes == (new Date().getMonth() - 1)) {
                        valorTotal += _val.valor;
                    }
                });
            }
        });

        $("#gastosRH").html("R$ " + valorTotal.toFixed(2));
    })
        .fail(function (data) {
            console.log(data);
            $("#gastosRH").html("R$ 0,00");
        });


});

$(document).on('click', '.navbar-toggler', function () {
    $toggle = $(this);

    if (mobile_menu_visible == 1) {
        $('html').removeClass('nav-open');

        $('.close-layer').remove();
        setTimeout(function () {
            $toggle.removeClass('toggled');
        }, 400);

        mobile_menu_visible = 0;
    } else {
        setTimeout(function () {
            $toggle.addClass('toggled');
        }, 430);

        var $layer = $('<div class="close-layer"></div>');

        if ($('body').find('.main-panel').length != 0) {
            $layer.appendTo(".main-panel");

        } else if (($('body').hasClass('off-canvas-sidebar'))) {
            $layer.appendTo(".wrapper-full-page");
        }

        setTimeout(function () {
            $layer.addClass('visible');
        }, 100);

        $layer.click(function () {
            $('html').removeClass('nav-open');
            mobile_menu_visible = 0;

            $layer.removeClass('visible');

            setTimeout(function () {
                $layer.remove();
                $toggle.removeClass('toggled');

            }, 400);
        });

        $('html').addClass('nav-open');
        mobile_menu_visible = 1;

    }

});

// activate collapse right menu when the windows is resized
$(window).resize(function () {
    md.initSidebarsCheck();

    // reset the seq for charts drawing animations
    seq = seq2 = 0;

    setTimeout(function () {
        md.initDashboardPageCharts();
    }, 500);
});

md = {
    misc: {
        navbar_menu_visible: 0,
        active_collapse: true,
        disabled_collapse_init: 0,
    },

    checkSidebarImage: function () {
        $sidebar = $('.sidebar');
        image_src = $sidebar.data('image');

        if (image_src !== undefined) {
            sidebar_container = '<div class="sidebar-background" style="background-image: url(' + image_src + ') "/>';
            $sidebar.append(sidebar_container);
        }
    },

    initFormExtendedDatetimepickers: function () {
        $('.datetimepicker').datetimepicker({
            icons: {
                time: "fa fa-clock-o",
                date: "fa fa-calendar",
                up: "fa fa-chevron-up",
                down: "fa fa-chevron-down",
                previous: 'fa fa-chevron-left',
                next: 'fa fa-chevron-right',
                today: 'fa fa-screenshot',
                clear: 'fa fa-trash',
                close: 'fa fa-remove'
            }
        });

        $('.datepicker').datetimepicker({
            format: 'MM/DD/YYYY',
            icons: {
                time: "fa fa-clock-o",
                date: "fa fa-calendar",
                up: "fa fa-chevron-up",
                down: "fa fa-chevron-down",
                previous: 'fa fa-chevron-left',
                next: 'fa fa-chevron-right',
                today: 'fa fa-screenshot',
                clear: 'fa fa-trash',
                close: 'fa fa-remove'
            }
        });

        $('.timepicker').datetimepicker({
            //          format: 'H:mm',    // use this format if you want the 24hours timepicker
            format: 'h:mm A', //use this format if you want the 12hours timpiecker with AM/PM toggle
            icons: {
                time: "fa fa-clock-o",
                date: "fa fa-calendar",
                up: "fa fa-chevron-up",
                down: "fa fa-chevron-down",
                previous: 'fa fa-chevron-left',
                next: 'fa fa-chevron-right',
                today: 'fa fa-screenshot',
                clear: 'fa fa-trash',
                close: 'fa fa-remove'

            }
        });
    },


    initSliders: function () {
        // Sliders for demo purpose
        var slider = document.getElementById('sliderRegular');

        noUiSlider.create(slider, {
            start: 40,
            connect: [true, false],
            range: {
                min: 0,
                max: 100
            }
        });

        var slider2 = document.getElementById('sliderDouble');

        noUiSlider.create(slider2, {
            start: [20, 60],
            connect: true,
            range: {
                min: 0,
                max: 100
            }
        });
    },

    initSidebarsCheck: function () {
        if ($(window).width() <= 991) {
            if ($sidebar.length != 0) {
                md.initRightMenu();
            }
        }
    },

    initDashboardPageCharts: function () {

        if ($('#dailySalesChart').length != 0 || $('#completedTasksChart').length != 0 || $('#websiteViewsChart').length != 0) {
            /* ----------==========     Daily Sales Chart initialization    ==========---------- */
            var jqxhr = jQuery.get("https://sigemv.azurewebsites.net/api/Vendas", function () {
            }).done(function (data) {
                    var meses = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
                    $.each(data, function (i, val) {
                        var data = new Date(val.data);
                        meses[data.getMonth()] = meses[data.getMonth()] + 1;
                    });
                    dataDailySalesChart = {
                        labels: ['J', 'F', 'M', 'A', 'M', 'J', 'J', 'A', 'S', 'O', 'N', 'D'],
                        series: [meses]
                    };

                    optionsDailySalesChart = {
                        lineSmooth: Chartist.Interpolation.cardinal({
                            tension: 0
                        }),
                        low: 0,
                        high: 10, // creative tim: we recommend you to set the high sa the biggest value + something for a better look
                        chartPadding: {
                            top: 0,
                            right: 0,
                            bottom: 0,
                            left: 0
                        },
                    }

                    var dailySalesChart = new Chartist.Line('#dailySalesChart', dataDailySalesChart, optionsDailySalesChart);

                    md.startAnimationForLineChart(dailySalesChart);

                });

            /* ----------==========     Completed Tasks Chart initialization    ==========---------- */
            var jqxhr = jQuery.get("https://sigepm.azurewebsites.net/getAllProducaoPorMes", function () {
            })
                .done(function (data) {
                    var meses = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
                    $.each(data, function (i, val) {
                        var data = new Date(val.data);
                        switch (val.mes.trim()) {
                            case "Janeiro":
                                meses[0] += meses[0] + parseInt(val.quantidade);
                                break;
                            case "Fevereiro":
                                break;
                                meses[1] += meses[1] + parseInt(val.quantidade);
                            case "Março":
                                meses[2] += meses[2] + parseInt(val.quantidade);
                                break;
                            case "Abril":
                                meses[3] += meses[3] + parseInt(val.quantidade);
                                break;
                            case "Maio":
                                meses[4] += meses[4] + parseInt(val.quantidade);
                                break;
                            case "Junho ":
                                meses[5] += meses[5] + parseInt(val.quantidade);
                                break;
                            case "Julho":
                                meses[6] += meses[6] + parseInt(val.quantidade);
                                break;
                            case "Agosto":
                                meses[7] += meses[7] + parseInt(val.quantidade);
                                break;
                            case "Setembro":
                                meses[8] += meses[8] + parseInt(val.quantidade);
                                break;
                            case "Outubro":
                                meses[9] += meses[9] + parseInt(val.quantidade);
                                break;
                            case "Novembro":
                                meses[10] += meses[10] + parseInt(val.quantidade);
                                break;
                            case "Dezembro":
                                meses[11] += meses[11] + parseInt(val.quantidade);
                                break;

                        }
                        //meses[data.getMonth()] = meses[data.getMonth()] + 1;
                    });
                    dataCompletedTasksChart = {
                        labels: ['J', 'F', 'M', 'A', 'M', 'J', 'J', 'A', 'S', 'O', 'N', 'D'],
                        series: [meses]
                    };

                    optionsCompletedTasksChart = {
                        lineSmooth: Chartist.Interpolation.cardinal({
                            tension: 0
                        }),
                        low: 0,
                        high: 200, // creative tim: we recommend you to set the high sa the biggest value + something for a better look
                        chartPadding: {
                            top: 0,
                            right: 0,
                            bottom: 0,
                            left: 0
                        }
                    }

                    var completedTasksChart = new Chartist.Line('#completedTasksChart', dataCompletedTasksChart, optionsCompletedTasksChart);

                    // start animation for the Completed Tasks Chart - Line Chart
                    md.startAnimationForLineChart(completedTasksChart);

                })
                .fail(function (data) {
                    var meses = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
                    dataCompletedTasksChart = {
                        labels: ['J', 'F', 'M', 'A', 'M', 'J', 'J', 'A', 'S', 'O', 'N', 'D'],
                        series: [meses]
                    };

                    optionsCompletedTasksChart = {
                        lineSmooth: Chartist.Interpolation.cardinal({
                            tension: 0
                        }),
                        low: 0,
                        high: 1000, // creative tim: we recommend you to set the high sa the biggest value + something for a better look
                        chartPadding: {
                            top: 0,
                            right: 0,
                            bottom: 0,
                            left: 0
                        }
                    }

                    var completedTasksChart = new Chartist.Line('#completedTasksChart', dataCompletedTasksChart, optionsCompletedTasksChart);

                    // start animation for the Completed Tasks Chart - Line Chart
                    md.startAnimationForLineChart(completedTasksChart);
                });




            /* ----------==========     Emails Subscription Chart initialization    ==========---------- */
            var jqxhr = jQuery.get("https://sigemv.azurewebsites.net/api/Eventos", function () {
            }).done(function (data) {
                console.log(data);
                var meses = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
                $.each(data, function (i, val) {
                    var data = new Date(val.data);
                    meses[data.getMonth()] = meses[data.getMonth()] + 1;
                });
                var dataWebsiteViewsChart = {
                    labels: ['J', 'F', 'M', 'A', 'M', 'J', 'J', 'A', 'S', 'O', 'N', 'D'],
                    series: [meses]
                };
                var optionsWebsiteViewsChart = {
                    axisX: {
                        showGrid: false
                    },
                    low: 0,
                    high: 10,
                    chartPadding: {
                        top: 0,
                        right: 5,
                        bottom: 0,
                        left: 0
                    }
                };
                var responsiveOptions = [
                    ['screen and (max-width: 640px)', {
                        seriesBarDistance: 5,
                        axisX: {
                            labelInterpolationFnc: function (value) {
                                return value[0];
                            }
                        }
                    }]
                ];
                var websiteViewsChart = Chartist.Bar('#websiteViewsChart', dataWebsiteViewsChart, optionsWebsiteViewsChart, responsiveOptions);

                //start animation for the Emails Subscription Chart
                md.startAnimationForBarChart(websiteViewsChart);
            })
                .fail(function (data) {
                    console.log(data);

                    var dataWebsiteViewsChart = {
                        labels: ['J', 'F', 'M', 'A', 'M', 'J', 'J', 'A', 'S', 'O', 'N', 'D'],
                        series: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
                    };
                    var optionsWebsiteViewsChart = {
                        axisX: {
                            showGrid: false
                        },
                        low: 0,
                        high: 10,
                        chartPadding: {
                            top: 0,
                            right: 5,
                            bottom: 0,
                            left: 0
                        }
                    };
                    var responsiveOptions = [
                        ['screen and (max-width: 640px)', {
                            seriesBarDistance: 5,
                            axisX: {
                                labelInterpolationFnc: function (value) {
                                    return value[0];
                                }
                            }
                        }]
                    ];
                    var websiteViewsChart = Chartist.Bar('#websiteViewsChart', dataWebsiteViewsChart, optionsWebsiteViewsChart, responsiveOptions);

                    //start animation for the Emails Subscription Chart
                    md.startAnimationForBarChart(websiteViewsChart);
                });



            /* ----------==========     Emails Subscription Chart initialization    ==========---------- */
            var jqxhr = jQuery.get("https://sigerh.azurewebsites.net/api/Treinamentos", function () {
            }).done(function (data) {
                console.log(data);
                var meses = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
                $.each(data, function (i, val) {
                    var data = new Date(val.Data);
                    meses[data.getMonth()] = meses[data.getMonth()] + 1;
                });
                var dataWebsiteViewsChart = {
                    labels: ['J', 'F', 'M', 'A', 'M', 'J', 'J', 'A', 'S', 'O', 'N', 'D'],
                    series: [meses]
                };
                var optionsWebsiteViewsChart = {
                    axisX: {
                        showGrid: false
                    },
                    low: 0,
                    high: 10,
                    chartPadding: {
                        top: 0,
                        right: 5,
                        bottom: 0,
                        left: 0
                    }
                };
                var responsiveOptions = [
                    ['screen and (max-width: 640px)', {
                        seriesBarDistance: 5,
                        axisX: {
                            labelInterpolationFnc: function (value) {
                                return value[0];
                            }
                        }
                    }]
                ];
                var websiteViewsChart1 = Chartist.Bar('#trenamentosMes', dataWebsiteViewsChart, optionsWebsiteViewsChart, responsiveOptions);

                //start animation for the Emails Subscription Chart
                md.startAnimationForBarChart(websiteViewsChart1);
            })
                .fail(function (data) {
                    console.log(data);
                    var dataWebsiteViewsChart = {
                        labels: ['J', 'F', 'M', 'A', 'M', 'J', 'J', 'A', 'S', 'O', 'N', 'D'],
                        series: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
                    };
                    var optionsWebsiteViewsChart = {
                        axisX: {
                            showGrid: false
                        },
                        low: 0,
                        high: 10,
                        chartPadding: {
                            top: 0,
                            right: 5,
                            bottom: 0,
                            left: 0
                        }
                    };
                    var responsiveOptions = [
                        ['screen and (max-width: 640px)', {
                            seriesBarDistance: 5,
                            axisX: {
                                labelInterpolationFnc: function (value) {
                                    return value[0];
                                }
                            }
                        }]
                    ];
                    var websiteViewsChart1 = Chartist.Bar('#trenamentosMes', dataWebsiteViewsChart, optionsWebsiteViewsChart, responsiveOptions);

                    //start animation for the Emails Subscription Chart
                    md.startAnimationForBarChart(websiteViewsChart1);
                });

            /* ----------==========     Daily Sales Chart initialization    ==========---------- */
            var jqxhr = jQuery.get("https://sigerh.azurewebsites.net/api/Treinamentos", function () {
            })
                .done(function (data) {
                    var meses = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
                    $.each(data, function (i, val) {
                        var data = new Date(val.Data);
                        meses[data.getMonth()] = parseFloat(val.custo);
                    });
                    dataDailySalesChart = {
                        labels: ['J', 'F', 'M', 'A', 'M', 'J', 'J', 'A', 'S', 'O', 'N', 'D'],
                        series: [meses]
                    };

                    optionsDailySalesChart = {
                        lineSmooth: Chartist.Interpolation.cardinal({
                            tension: 0
                        }),
                        low: 0,
                        high: 11000, // creative tim: we recommend you to set the high sa the biggest value + something for a better look
                        chartPadding: {
                            top: 0,
                            right: 0,
                            bottom: 0,
                            left: 0
                        },
                    }

                    var dailySalesChart = new Chartist.Line('#custoTrenamentosMes', dataDailySalesChart, optionsDailySalesChart);

                    md.startAnimationForLineChart(dailySalesChart);

                })
                .fail(function (data) {
                    console.log(data);
                    dataDailySalesChart = {
                        labels: ['J', 'F', 'M', 'A', 'M', 'J', 'J', 'A', 'S', 'O', 'N', 'D'],
                        series: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
                    };

                    optionsDailySalesChart = {
                        lineSmooth: Chartist.Interpolation.cardinal({
                            tension: 0
                        }),
                        low: 0,
                        high: 50, // creative tim: we recommend you to set the high sa the biggest value + something for a better look
                        chartPadding: {
                            top: 0,
                            right: 0,
                            bottom: 0,
                            left: 0
                        },
                    }

                    var dailySalesChart = new Chartist.Line('#custoTrenamentosMes', dataDailySalesChart, optionsDailySalesChart);

                    md.startAnimationForLineChart(dailySalesChart);
                });
        }
    },

    initMinimizeSidebar: function () {

        $('#minimizeSidebar').click(function () {
            var $btn = $(this);

            if (md.misc.sidebar_mini_active == true) {
                $('body').removeClass('sidebar-mini');
                md.misc.sidebar_mini_active = false;
            } else {
                $('body').addClass('sidebar-mini');
                md.misc.sidebar_mini_active = true;
            }

            // we simulate the window Resize so the charts will get updated in realtime.
            var simulateWindowResize = setInterval(function () {
                window.dispatchEvent(new Event('resize'));
            }, 180);

            // we stop the simulation of Window Resize after the animations are completed
            setTimeout(function () {
                clearInterval(simulateWindowResize);
            }, 1000);
        });
    },

    checkScrollForTransparentNavbar: debounce(function () {
        if ($(document).scrollTop() > 260) {
            if (transparent) {
                transparent = false;
                $('.navbar-color-on-scroll').removeClass('navbar-transparent');
            }
        } else {
            if (!transparent) {
                transparent = true;
                $('.navbar-color-on-scroll').addClass('navbar-transparent');
            }
        }
    }, 17),


    initRightMenu: debounce(function () {
        $sidebar_wrapper = $('.sidebar-wrapper');

        if (!mobile_menu_initialized) {
            $navbar = $('nav').find('.navbar-collapse').children('.navbar-nav');

            mobile_menu_content = '';

            nav_content = $navbar.html();

            nav_content = '<ul class="nav navbar-nav nav-mobile-menu">' + nav_content + '</ul>';

            navbar_form = $('nav').find('.navbar-form').get(0).outerHTML;

            $sidebar_nav = $sidebar_wrapper.find(' > .nav');

            // insert the navbar form before the sidebar list
            $nav_content = $(nav_content);
            $navbar_form = $(navbar_form);
            $nav_content.insertBefore($sidebar_nav);
            $navbar_form.insertBefore($nav_content);

            $(".sidebar-wrapper .dropdown .dropdown-menu > li > a").click(function (event) {
                event.stopPropagation();

            });

            // simulate resize so all the charts/maps will be redrawn
            window.dispatchEvent(new Event('resize'));

            mobile_menu_initialized = true;
        } else {
            if ($(window).width() > 991) {
                // reset all the additions that we made for the sidebar wrapper only if the screen is bigger than 991px
                $sidebar_wrapper.find('.navbar-form').remove();
                $sidebar_wrapper.find('.nav-mobile-menu').remove();

                mobile_menu_initialized = false;
            }
        }
    }, 200),

    startAnimationForLineChart: function (chart) {

        chart.on('draw', function (data) {
            if (data.type === 'line' || data.type === 'area') {
                data.element.animate({
                    d: {
                        begin: 600,
                        dur: 700,
                        from: data.path.clone().scale(1, 0).translate(0, data.chartRect.height()).stringify(),
                        to: data.path.clone().stringify(),
                        easing: Chartist.Svg.Easing.easeOutQuint
                    }
                });
            } else if (data.type === 'point') {
                seq++;
                data.element.animate({
                    opacity: {
                        begin: seq * delays,
                        dur: durations,
                        from: 0,
                        to: 1,
                        easing: 'ease'
                    }
                });
            }
        });

        seq = 0;
    },
    startAnimationForBarChart: function (chart) {

        chart.on('draw', function (data) {
            if (data.type === 'bar') {
                seq2++;
                data.element.animate({
                    opacity: {
                        begin: seq2 * delays2,
                        dur: durations2,
                        from: 0,
                        to: 1,
                        easing: 'ease'
                    }
                });
            }
        });

        seq2 = 0;
    }
}

// Returns a function, that, as long as it continues to be invoked, will not
// be triggered. The function will be called after it stops being called for
// N milliseconds. If `immediate` is passed, trigger the function on the
// leading edge, instead of the trailing.

function debounce(func, wait, immediate) {
    var timeout;
    return function () {
        var context = this,
            args = arguments;
        clearTimeout(timeout);
        timeout = setTimeout(function () {
            timeout = null;
            if (!immediate) func.apply(context, args);
        }, wait);
        if (immediate && !timeout) func.apply(context, args);
    };
};