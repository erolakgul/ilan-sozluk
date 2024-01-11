$(document).ready(function () {
    var searchboxID = "#txtSearch";
    $('#srcButton').click(function () {
        $(searchboxID).keyup();
    });
    $(searchboxID).keyup(function (event) {
        if (event.keyCode != 13 && event.keyCode != 37 && event.keyCode != 39) {
            if ($.trim($(searchboxID).val()).length > 2) {

                delay(function () {
                    var options = {};
                    options.url = '/WebHome/Search',
                    options.type = "POST";
                    options.data = "{searchKey:'" + $("#h4AramaBaslik").html() + "-" + $(searchboxID).val() + "'}",
                    options.contentType = "application/json; charset=utf-8",
                    options.success = function (result) {
                        $("#searchDiv").html(result);
                        $("#searchDiv").show();
                    };
                    options.error = function (err) {
                        if (err.status == 500) {
                            window.location.href = "/WebHome/Index";
                        } else {
                            if (err.status != 0) {
                                alert(err.statusText);
                            }
                        }
                    };
                    $.ajax(options);
                }, 500);
            } else {
                $("#searchDiv").hide();
            }
        }
    });
});

var delay = (function () {
    var timer = 0;
    return function (callback, ms) {
        clearTimeout(timer);
        timer = setTimeout(callback, ms);
    };
})();

$(function () {
    $("#txtSearch").click(function () {
        if ($("#txtSearch").val() == "Detaylı ara?") {
            $("#txtSearch").val("");
        }
    });
    $("#txtSearch").blur(function () {
        if ($("#txtSearch").val().trim() == "") {
            $("#txtSearch").val("Detaylı ara?");
        }
    });
});