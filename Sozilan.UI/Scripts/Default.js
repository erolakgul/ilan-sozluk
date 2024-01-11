$(document).ready(function () {

    $("#btnKariyer").click(function () {
        var icerik = $("#btnKariyer").html();
        $("#h4AramaBaslik").html(icerik);
    });
    $("#btnSecret").click(function () {
        var icerik = $("#btnSecret").html();
        $("#h4AramaBaslik").html(icerik);
    });
    $("#btnYeni").click(function () {
        var icerik = $("#btnYeni").html();
        $("#h4AramaBaslik").html(icerik);
    });
    $("#btnEleman").click(function () {
        var icerik = $("#btnEleman").html();
        $("#h4AramaBaslik").html(icerik);
    });
    $("#btnLinked").click(function () {
        var icerik = $("#btnLinked").html();
        $("#h4AramaBaslik").html(icerik);
    });
    $("#btnFirsat").click(function () {
        var icerik = $("#btnFirsat").html();
        $("#h4AramaBaslik").html(icerik);
    });

    $("#txtSearch").css("max-width", "100%");
})

function sil(id) {
    var fg = $("#titleID").attr("title");
    delay(function () {
        var options = {};
        options.url = '/Users/Home/Sil',
        options.type = "POST";
        options.data = "{id:'" + id + "-" + fg + "'}",
        options.contentType = "application/json; charset=utf-8",
        options.success = function (result) {  // controller dan dönen partial view html(result) içine kaydedilir
            $(".xClosed").hide();
            $("#kullaniciEntryListelePartial").html(result);
        };
        options.error = function (err) {
            if (err.status == 500) {
                window.location.href = "/Users/Home/Index";
            } else {
                if (err.status != 0) {
                    alert(err.statusText);
                }
            }
        };
        $.ajax(options);
    }, 500);
}

function entryLink(id) {
    delay(function () {
        var options = {};
        options.url = '/WebHome/Listele',
        options.type = "POST";
        options.data = "{id:'" + id + "'}",
        options.contentType = "application/json; charset=utf-8",
        options.success = function (result) {
            $(".xClosed").hide();
            $("#kullaniciEntryListele").html(result);
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
    }, 50);
}


function upd(id) {
    $("#entryContent2").css({ "max-width": "100%", "height": "150px" });

    var k = $("#" + id).attr("title"); // title içeriği alındı
    $("#h4Duzenle2").html(k);

    var e = $("#h52_" + id).html();    //entry içeriği alındı
    $("#entryContent2").html(e);

    var b = $("#" + id).attr("id");  //entry id si alındı
    $("#bid2").html(b);
    $("#bid2").val(b);
}

function arttir(id) {
    $.ajax({
        type: "POST",
        url: "/WebHome/Arttir/" + id,
        data: "{ id: '" + id + "'}",
        //success: function () {   
        //    alert("Arttırma işlemi başarılı");
        //},
        success: function () {
            swal({
                title: "",
                text: "Yorum Beğenildi",
                timer: 2000,
                type:"success",
                showConfirmButton: false
            });
        },
        error: function () {
            swal({
                title: "",
                text: "Bir terslik oluştu!!.",
                timer: 3000,
                type: "error",
                showConfirmButton: false
            });
        }
    })
}

function azalt(id) {
    $.ajax({
        type: "POST",
        url: "/WebHome/Azalt/" + id,
        data: "{ id: '" + id + "'}",
        success: function () {  
                swal({
                    title: "",
                    text: "Eksi oy verildi!",
                    timer: 2000,
                    type: "warning",
                    showConfirmButton: false
                });
        },
        error: function () {
            swal({
                title: "",
                text: "Bir terslik oluştu!!.",
                timer: 3000,
                type: "error",
                showConfirmButton: false
            });
        }
    })
}