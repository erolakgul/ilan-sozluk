$(document).ready(function () {
    var searchboxID = "#txtSearch";
    $('#srcButton').click(function () {
        $(searchboxID).keyup();
    });
    $(searchboxID).keyup(function (event) {
        if (event.keyCode != 13 && event.keyCode != 37 && event.keyCode != 39) {
            if ($.trim($(searchboxID).val()).length > 2) {  // girilen karakter sayısı 3 ten fazla ise arama yapılıyor

                delay(function () {
                    var options = {};
                    options.url = '/WebHome/Search',   // çalıştırılacak olan controller/method
                    options.type = "POST";
                    options.data = "{searchKey:'" + $("#h4AramaBaslik").html() + "-" + $(searchboxID).val() + "'}", // controller daki Search methoduna searchKey parametresiyle değer gönderilir
                    options.contentType = "application/json; charset=utf-8",
                    options.success = function (result) {  // partial view e gönderilen model
                        $("#searchDiv").html(result);      // sayfa şekliyle searchDiv elementinin arasına gömülür
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
        if ($("#txtSearch").val() == "Bişeyler aramak ister misin ?") {
            $("#txtSearch").val("");
        }
    });
    $("#txtSearch").blur(function () {
        if ($("#txtSearch").val().trim() == "") {
            $("#txtSearch").val("Bişeyler aramak ister misin ?");
        }
    });
});