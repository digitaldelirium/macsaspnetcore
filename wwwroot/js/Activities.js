var gotJan = false;
var gotFeb = false;
var gotMar = false;
var gotApr = false;
var gotMay = false;
var gotJun = false;
var gotJul = false;
var gotAug = false;
var gotSep = false;
var gotOct = false;
var gotNov = false;
var gotDec = false;
var gotSched = false;


function hideMonths() {
    $("#monthJan").hide();
    $("#monthFeb").hide();
    $("#monthMar").hide();
    $("#monthApr").hide();
    $("#monthMay").hide();
    $("#monthJun").hide();
    $("#monthJul").hide();
    $("#monthAug").hide();
    $("#monthSep").hide();
    $("#monthOct").hide();
    $("#monthNov").hide();
    $("#monthDec").hide();
    $("#allActivities").hide();
}

hideMonths();

function removeActiveClasses() {
    $("#tabJan").removeClass("active");
    $("#monthJan").removeClass("active");
    $("#tabFeb").removeClass("active");
    $("#monthFeb").removeClass("active");
    $("#tabMar").removeClass("active");
    $("#monthMar").removeClass("active");
    $("#tabApr").removeClass("active");
    $("#monthApr").removeClass("active");
    $("#tabMay").removeClass("active");
    $("#monthMay").removeClass("active");
    $("#tabJun").removeClass("active");
    $("#monthJun").removeClass("active");
    $("#tabJul").removeClass("active");
    $("#monthJul").removeClass("active");
    $("#tabAug").removeClass("active");
    $("#monthAug").removeClass("active");
    $("#tabSep").removeClass("active");
    $("#monthSep").removeClass("active");
    $("#tabOct").removeClass("active");
    $("#monthOct").removeClass("active");
    $("#tabNov").removeClass("active");
    $("#monthNov").removeClass("active");
    $("#tabDec").removeClass("active");
    $("#monthDec").removeClass("active");
}

removeActiveClasses();

function renderData(data) {
    var eventTime = moment(data.startTime).format("dddd, MMMM Do, h:mm A");
    //console.debug(eventTime);
    //var d = new Date(data.startTime);
    var dataBlob = "<div class=\"panel panel-primary\">" +
        "<div class=\"panel-heading\">" + eventTime + " &nbsp;&nbsp;&nbsp;&nbsp; <b><em>" + data.activityTitle + "</em></b></div>" +
        "<div class=\"panel-body\">" + data.activityDescription + "</div></div>";
    return dataBlob;
}

var monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

function getUpcomingMonthEvents(target, month) {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth(); //January is 0!
    var yyyy = today.getFullYear();
    //today = yyyy + "-" +mm + "-" + dd;
    var startDate = new Date(yyyy, mm, dd).toUTCString();

    var day;
    switch (month) {
        case 3:
        case 5:
        case 8:
        case 10:
            day = 30;
            break;
        case 0:
        case 2:
        case 4:
        case 6:
        case 7:
        case 9:
        case 11:
            day = 31;
            break;
        case 1:
            if (yyyy % 4 === 0) {
                day = 29;
            } else {
                day = 28;
            }
            break;
    }

    var endDate = new Date(yyyy, mm, day).toUTCString();

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/api/activities/month",
        data: { starting: startDate, ending: endDate },
        success: function(data) {
            if (data.length > 0) {
                for (var i = 0; i < data.length; ++i) {
                    $(target).append(renderData(data[i]));
                }
            } else {
                var noEvents = "<div class=\"panel panel-info\">" +
                    "<div class=\"panel-header\">No Events for " + monthNames[month] + "</div>" +
                    "<div class=\"panel-body\">There are no events this month!</div></div>";
                $(target).append(noEvents);
            }
        },
        error: function() {
            var errString = "<div class=\"panel panel-warning\">" +
                "<div class=\"panel-header\">An Error Has Occurred</div>" +
                "<div class=\"panel-body\">Could not retrieve " + monthNames[month] + " data</div></div>";
            return errString;
        }

    });


}

var today = new Date();
var year = today.getFullYear;

// Hide all months as default
hideMonths();
(function() {
    function pageInit() {
        // hide _Layout items
        $("#topLeft").hide();
        $("#topCenter").hide();
        $("#topRight").hide();
        $("#title").hide();

        //Get month and set as active on calendar
        var d = new Date();
        var n = d.getMonth();
        var target;
        switch (n) {
            case 0:
                target = "#monthJan";
                removeActiveClasses();
                hideMonths();
                getUpcomingMonthEvents(target, n);
                $("#monthJan").addClass("active");
                $("#tabJan").addClass("active");
                $("#monthJan").show();
                gotJan = true;
                break;
            case 1:
                target = "#monthFeb";
                removeActiveClasses();
                hideMonths();
                getUpcomingMonthEvents(target, n);
                $("#monthFeb").addClass("active");
                $("#tabFeb").addClass("active");
                $("#monthFeb").show();
                gotFeb = true;
                break;
            case 2:
                target = "#monthMar";
                removeActiveClasses();
                hideMonths();
                getUpcomingMonthEvents(target, n);
                $("#monthMar").addClass("active");
                $("#tabMar").addClass("active");
                $("#monthMar").show();
                gotMar = true;
                break;
            case 3:
                target = "#monthApr";
                removeActiveClasses();
                hideMonths();
                getUpcomingMonthEvents(target, n);
                gotApr = true;
                $("#monthApr").addClass("active");
                $("#tabApr").addClass("active");
                $("#monthApr").show();
                break;
            case 4:
                target = "#monthMay";
                removeActiveClasses();
                hideMonths();
                getUpcomingMonthEvents(target, n);
                $("#monthMay").addClass("active");
                $("#tabMay").addClass("active");
                $("#monthMay").show();
                gotMay = true;
                break;
            case 5:
                target = "#monthJun";
                removeActiveClasses();
                hideMonths();
                getUpcomingMonthEvents(target, n);
                $("#monthJun").addClass("active");
                $("#tabJun").addClass("active");
                $("#monthJun").show();
                gotJun = true;
                break;
            case 6:
                target = "#monthJul";
                removeActiveClasses();
                hideMonths();
                getUpcomingMonthEvents(target, n);
                $("#monthJul").addClass("active");
                $("#tabJul").addClass("active");
                $("#monthJul").show();
                gotJul = true;
                break;
            case 7:
                target = "#monthAug";
                removeActiveClasses();
                hideMonths();
                getUpcomingMonthEvents(target, n);
                $("#monthAug").addClass("active");
                $("#tabAug").addClass("active");
                $("#monthAug").show();
                gotAug = true;
                break;
            case 8:
                target = "#monthSep";
                removeActiveClasses();
                hideMonths();
                getUpcomingMonthEvents(target, n);
                $("#monthSep").addClass("active");
                $("#tabSep").addClass("active");
                $("#monthSep").show();
                gotSept = true;
                break;
            case 9:
                target = "#monthOct";
                removeActiveClasses();
                hideMonths();
                getUpcomingMonthEvents(target, n);
                $("#monthOct").addClass("active");
                $("#tabOct").addClass("active");
                $("#monthOct").show();
                gotOct = true;
                break;
            case 10:
                target = "#monthNov";
                removeActiveClasses();
                hideMonths();
                getUpcomingMonthEvents(target, n);
                $("#monthNov").addClass("active");
                $("#tabNov").addClass("active");
                $("#monthNov").show();
                gotNov = true;
                break;
            case 11:
                target = "#monthDec";
                removeActiveClasses();
                hideMonths();
                getUpcomingMonthEvents(target, n);
                $("#monthDec").addClass("active");
                $("#tabDec").addClass("active");
                $("#monthDec").show();
                gotDec = true;
                break;
            default:
                break;
        }
    }

    pageInit();
})();

function getJanuary() {
    $("#tabJan").click(function() {
        hideMonths();
        removeActiveClasses();
        var startDate = new Date(2018, 0, 1).toUTCString();
        var endDate = new Date(2018, 0, 31).toUTCString();
        if (!gotJan) {
            $.ajax({
                type: "GET",
                dataType: "json",
                url: "/api/activities/month",
                data: { starting: startDate, ending: endDate },
                success: function(data) {
                    if (data.length > 0) {
                        for (var i = 0; i < data.length; ++i) {
                            $("#monthJan").append(renderData(data[i]));
                        }
                    } else {
                        $("#monthJan").append("<div class='panel panel-info'>" +
                            "<div class='panel-header'>No Events for January</div>" +
                            "<div class='panel-body'>There are no events this month!</div></div>"
                        );
                    }
                },
                error: function() {
                    $("#monthJan").append("<div class='panel panel-warning'>" +
                        "<div class='panel-header'>An Error Has Occurred</div>" +
                        "<div class='panel-body'>Could not retrieve January data</div></div>");
                }
            });
            gotJan = true;
        }
        $("#monthJan").show();
        $("#tabJan").addClass("active");
        $("#monthJan").addClass("active");

    });
}

function getFebruary() {
    $("#tabFeb").click(function() {
        hideMonths();
        removeActiveClasses();
        getUpcomingMonthEvents();
        var startDate = new Date(2018, 1, 1).toUTCString();
        var endDate = new Date(2018, 1, 29).toUTCString();
        if (!gotFeb) {
            $.ajax({
                type: "GET",
                dataType: "json",
                url: "/api/activities/month",
                data: { starting: startDate, ending: endDate },
                success: function(data) {
                    if (data.length > 0) {
                        for (var i = 0; i < data.length; ++i) {
                            $("#monthFeb").append(renderData(data[i]));
                        }
                    } else {
                        $("#monthFeb").append("<div class='panel panel-info'>" +
                            "<div class='panel-header'>No Events for February</div>" +
                            "<div class='panel-body'>There are no events this month!</div></div>"
                        );
                    }

                },
                error: function() {
                    $("#monthFeb").append("<div class='panel panel-warning'>" +
                        "<div class='panel-header'>An Error Has Occurred</div>" +
                        "<div class='panel-body'>Could not retrieve February data</div></div>");
                }
            });
            gotFeb = true;
        }

        $("#monthFeb").show();
        $("#tabFeb").addClass("active");
        $("#monthFeb").addClass("active");
    });
}

function getMarch() {
    $("#tabMar").click(function() {
        hideMonths();
        removeActiveClasses();
        var startDate = new Date(2018, 2, 1).toUTCString();
        var endDate = new Date(2018, 2, 31).toUTCString();
        if (!gotMar) {
            $.ajax({
                type: "GET",
                dataType: "json",
                url: "/api/activities/month",
                data: { starting: startDate, ending: endDate },
                success: function(data) {
                    if (data.length > 0) {
                        if (data.length > 0) {
                            for (var i = 0; i < data.length; ++i) {
                                $("#monthMar").append(renderData(data[i]));
                            }
                        } else {
                            $("#monthMar").append("<div class='panel panel-info'>" +
                                "<div class='panel-header'>No Events for March</div>" +
                                "<div class='panel-body'>There are no events this month!</div></div>"
                            );
                        }
                    } else {
                        $("#monthMar").append("<div class='panel panel-info'>" +
                            "<div class='panel-header'>No Events for March</div>" +
                            "<div class='panel-body'>There are no events this month!</div></div>"
                        );
                    }
                },
                error: function() {
                    $("#monthMar").append("<div class='panel panel-warning'>" +
                        "<div class='panel-header'>An Error Has Occurred</div>" +
                        "<div class='panel-body'>Could not retrieve March data</div></div>");
                }
            });
            gotMar = true;
        }

        $("#monthMar").show();
        $("#tabMar").addClass("active");
        $("#monthMar").addClass("active");
    });
}

function getApril() {
    $("#tabApr").click(function() {
        hideMonths();
        removeActiveClasses();
        var startDate = new Date(2018, 3, 1).toUTCString();
        var endDate = new Date(2018, 3, 30).toUTCString();
        if (!gotApr) {
            $.ajax({
                type: "GET",
                dataType: "json",
                url: "/api/activities/month",
                data: { starting: startDate, ending: endDate },
                success: function(data) {
                    if (data.length > 0) {
                        if (data.length > 0) {
                            for (var i = 0; i < data.length; ++i) {
                                $("#monthApr").append(renderData(data[i]));
                            }
                        } else {
                            $("#monthApr").append("<div class='panel panel-info'>" +
                                "<div class='panel-header'>No Events for April</div>" +
                                "<div class='panel-body'>There are no events this month!</div></div>"
                            );
                        }
                    } else {
                        $("#monthApr").append("<div class='panel panel-info'>" +
                            "<div class='panel-header'>No Events for April</div>" +
                            "<div class='panel-body'>There are no events this month!</div></div>"
                        );
                    }
                },
                error: function() {
                    $("#monthApr").append("<div class='panel panel-warning'>" +
                        "<div class='panel-header'>An Error Has Occurred</div>" +
                        "<div class='panel-body'>Could not retrieve April data</div></div>");
                }
            });
            gotApr = true;
        }

        $("#monthApr").show();
        $("#tabApr").addClass("active");
        $("#monthApr").addClass("active");
    });
}

function getMay() {
    $("#tabMay").click(function() {
        hideMonths();
        removeActiveClasses();
        var startDate = new Date(2018, 4, 1).toUTCString();
        var endDate = new Date(2018, 4, 31).toUTCString();
        if (!gotMay) {
            $.ajax({
                type: "GET",
                dataType: "json",
                url: "/api/activities/month",
                data: { starting: startDate, ending: endDate },
                success: function(data) {
                    if (data.length > 0) {
                        for (var i = 0; i < data.length; ++i) {
                            $("#monthMay").append(renderData(data[i]));
                        }
                    } else {
                        var noEvents = "<div class='panel panel-info'>" +
                            "<div class='panel-header'>No Events for May </div>" +
                            "<div class='panel-body'>There are no events this month!</div></div>";
                        $("#monthMay").append(noEvents);
                    }

                },
                error: function() {
                    $("#monthMay").append("<div class='panel panel-warning'>" +
                        "<div class='panel-header'>An Error Has Occurred</div>" +
                        "<div class='panel-body'>Could not retrieve May data</div></div>");
                }
            });
        }
        gotMay = true;
        $("#monthMay").show();
        $("#tabMay").addClass("active");
        $("#monthMay").addClass("active");
    });
}

function getJune() {
    $("#tabJun").click(function() {
        hideMonths();
        removeActiveClasses();
        var startDate = new Date(2018, 5, 1).toUTCString();
        var endDate = new Date(2018, 5, 30).toUTCString();
        if (!gotJun) {
            $.ajax({
                type: "GET",
                dataType: "json",
                url: "/api/activities/month",
                data: { starting: startDate, ending: endDate },
                success: function(data) {
                    if (data.length > 0) {
                        for (var i = 0; i < data.length; ++i) {
                            $("#monthJun").append(renderData(data[i]));
                        }
                    } else {
                        $("#monthJun").append("<div class='panel panel-info'>" +
                            "<div class='panel-header'>No Events for June</div>" +
                            "<div class='panel-body'>There are no events this month!</div></div>"
                        );
                    }
                },
                error: function() {
                    $("#monthJun").append("<div class='panel panel-warning'>" +
                        "<div class='panel-header'>An Error Has Occurred</div>" +
                        "<div class='panel-body'>Could not retrieve June data</div></div>");
                }
            });
            gotJun = true;
        }
        $("#monthJun").show();
        $("#tabJun").addClass("active");
        $("#monthJun").addClass("active");
    });
}

function getJuly() {
    $("#tabJul").click(function() {
        hideMonths();
        removeActiveClasses();
        var startDate = new Date(2018, 6, 1).toUTCString();
        var endDate = new Date(2018, 6, 31).toUTCString();
        if (!gotJul) {
            $.ajax({
                type: "GET",
                dataType: "json",
                url: "/api/activities/month",
                data: { starting: startDate, ending: endDate },
                success: function(data) {

                    if (data.length > 0) {
                        for (var i = 0; i < data.length; ++i) {
                            $("#monthJul").append(renderData(data[i]));
                        }
                    } else {
                        $("#monthJul").append("<div class='panel panel-info'>" +
                            "<div class='panel-header'>No Events for July</div>" +
                            "<div class='panel-body'>There are no events this month!</div></div>"
                        );
                    }
                },

                error: function() {
                    $("#monthJul").append("<div class='panel panel-warning'>" +
                        "<div class='panel-header'>An Error Has Occurred</div>" +
                        "<div class='panel-body'>Could not retrieve July data</div></div>");
                }
            });
            gotJul = true;
        }
        $("#monthJul").show();
        $("#tabJul").addClass("active");
        $("#monthJul").addClass("active");
    });
}

function getAugust() {

    $("#tabAug").click(function() {
        hideMonths();
        removeActiveClasses();
        var startDate = new Date(2018, 7, 1).toUTCString();
        var endDate = new Date(2018, 7, 31).toUTCString();
        if (!gotAug) {
            $.ajax({
                type: "GET",
                dataType: "json",
                url: "/api/activities/month",
                data: { starting: startDate, ending: endDate },
                success: function(data) {

                    if (data.length > 0) {
                        for (var i = 0; i < data.length; ++i) {
                            $("#monthAug").append(renderData(data[i]));
                        }
                    } else {
                        $("#monthAug").append("<div class='panel panel-info'>" +
                            "<div class='panel-header'>No Events for August</div>" +
                            "<div class='panel-body'>There are no events this month!</div></div>"
                        );
                    }
                },
                error: function() {
                    $("#monthAug").append("<div class='panel panel-warning'>" +
                        "<div class='panel-header'>An Error Has Occurred</div>" +
                        "<div class='panel-body'>Could not retrieve August data</div></div>");
                }
            });
            gotAug = true;
        }

        $("#monthAug").show();
        $("#tabAug").addClass("active");
        $("#monthAug").addClass("active");
    });
}

function getSeptember() {

    $("#tabSep").click(function() {
        hideMonths();
        removeActiveClasses();
        var startDate = new Date(2018, 8, 1).toUTCString();
        var endDate = new Date(2018, 8, 30).toUTCString();
        if (!gotSep) {
            $.ajax({
                type: "GET",
                dataType: "json",
                url: "/api/activities/month",
                data: { starting: startDate, ending: endDate },
                success: function(data) {
                    if (data.length > 0) {
                        for (var i = 0; i < data.length; ++i) {
                            $("#monthSep").append(renderData(data[i]));
                        }
                    } else {
                        $("#monthSep").append("<div class='panel panel-info'>" +
                            "<div class='panel-header'>No Events for September</div>" +
                            "<div class='panel-body'>There are no events this month!</div></div>"
                        );
                    }
                },
                error: function() {
                    $("#monthSep").append("<div class='panel panel-warning'>" +
                        "<div class='panel-header'>An Error Has Occurred</div>" +
                        "<div class='panel-body'>Could not retrieve September data</div></div>");
                }
            });
            gotSep = true;
        }
        $("#monthSep").show();
        $("#tabSep").addClass("active");
        $("#monthSep").addClass("active");
    });
}

function getOctober() {

    $("#tabOct").click(function() {
        hideMonths();
        removeActiveClasses();
        var startDate = new Date(2018, 9, 1).toUTCString();
        var endDate = new Date(2018, 9, 31).toUTCString();
        if (!gotOct) {
            $.ajax({
                type: "GET",
                dataType: "json",
                url: "/api/activities/month",
                data: { starting: startDate, ending: endDate },
                success: function(data) {
                    if (data.length > 0) {
                        for (var i = 0; i < data.length; ++i) {
                            $("#monthOct").append(renderData(data[i]));
                        }
                    } else {
                        $("#monthOct").append("<div class='panel panel-info'>" +
                            "<div class='panel-header'>No Events for October</div>" +
                            "<div class='panel-body'>There are no events this month!</div></div>"
                        );
                    }
                },
                error: function() {
                    $("#monthOct").append("<div class='panel panel-warning'>" +
                        "<div class='panel-header'>An Error Has Occurred</div>" +
                        "<div class='panel-body'>Could not retrieve October data</div></div>");
                }
            });
            gotOct = true;
        }
        $("#monthOct").show();
        $("#tabOct").addClass("active");
        $("#monthOct").addClass("active");
    });
}

function getNovember() {

    $("#tabNov").click(function() {
        hideMonths();
        removeActiveClasses();
        var startDate = new Date(2018, 10, 1).toUTCString();
        var endDate = new Date(2018, 10, 30).toUTCString();
        if (!gotNov) {
            $.ajax({
                type: "GET",
                dataType: "json",
                url: "/api/activities/month",
                data: { starting: startDate, ending: endDate },
                success: function(data) {
                    if (data.length > 0 && gotNov === false) {
                        for (var i = 0; i < data.length; ++i) {
                            $("#monthNov").append(renderData(data[i]));
                        }
                    } else {
                        $("#monthNov").append("<div class='panel panel-info'>" +
                            "<div class='panel-header'>No Events for November</div>" +
                            "<div class='panel-body'>There are no events this month!</div></div>"
                        );
                    }
                    self.gotNov = true;
                },
                error: function() {
                    $("#monthNov").append("<div class='panel panel-warning'>" +
                        "<div class='panel-header'>An Error Has Occurred</div>" +
                        "<div class='panel-body'>Could not retrieve November data</div></div>");
                }
            });
            gotNov = true;
        }
        $("#monthNov").show();
        $("#tabNov").addClass("active");
        $("#monthNov").addClass("active");
    });
}

function getDecember() {

    $("#tabDec").click(function() {
        hideMonths();
        removeActiveClasses();
        var startDate = new Date(2018, 11, 1).toUTCString();
        var endDate = new Date(2018, 11, 31).toUTCString();
        if (!gotDec) {
            $.ajax({
                type: "GET",
                dataType: "json",
                url: "/api/activities/month",
                data: { starting: startDate, ending: endDate },
                success: function(data) {
                    if (data.length > 0 && gotDec === false) {
                        for (var i = 0; i < data.length; ++i) {
                            $("#monthDec").append(renderData(data[i]));
                        }
                    } else {
                        $("#monthDec").append("<div class='panel panel-info'>" +
                            "<div class='panel-header'>No Events for December</div>" +
                            "<div class='panel-body'>There are no events this month!</div></div>"
                        );
                    }

                    gotDec = true;
                },
                error: function() {
                    $("#monthDec").append("<div class='panel panel-warning'>" +
                        "<div class='panel-header'>An Error Has Occurred</div>" +
                        "<div class='panel-body'>Could not retrieve December data</div></div>");
                }
            });
            gotDec = false;
        }
        $("#monthDec").show();
        $("#tabDec").addClass("active");
        $("#monthDec").addClass("active");
    });

    function getFullSchedule() {
        hideMonths();
        $("#allActivities").show();
        $("#allActivities").addClass("active");
        if (!gotSched) {
            $.ajax({
                type: "GET",
                dataType: "json",
                url: "/api/activities",
                data: {},
                sucess: function(data) {
                    if (data.length > 0 && gotSched === false) {
                        for (var i = 0; i < data.length; ++i) {
                            $("#allActivities").append(renderData(data[i]));
                        }
                        gotSched = true;
                    } else {
                        $("#allActivities").append("<div class='panel panel-warning'>" +
                            "<div class='panel panel-header'>An Error Has Occurred Retrieving Calendar'</div>" +
                            "<div class='panel panel-body'> There are no events this year.  Either none have been entered, or there's an error talking to the database (probable)</div></div>"
                        );
                    }
                },
                error: function() {
                    $("#allActivities").append("<div class='panel panel-warning'>" +
                        "<div class='panel panel-header'>An Error Has Occurred Retrieving Calendar'</div>" +
                        "<div class='panel panel-body'> There are no events this year.  Either none have been entered, or there's an error talking to the database (probable)</div></div>");
                }

            });
        }
    }
}