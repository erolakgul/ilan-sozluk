$(document).ready(function () {
    $(function () { // mesajlar için numaratör
        var say = 0;
        $("#mesajContent").bind('keydown keyup keypress change', function () {
            var valueLengt = $(this).val().length;
            var saymax = (say) + (valueLengt);
            $(".numarator").html(saymax);

            if (saymax > 140) {
                swal({
                    title: "",
                    text: "140 karakteri geçtiniz..",
                    timer: 1000,
                    type: "warning",
                    showConfirmButton: false
                });
                $(".numarator").css("color", "red");
                $("#mesajGonder").css("display", "none");
                $(".mesajUyari").css({"display":"inline-block","color":"red"});
                $(".mesajUyari").html("Mesajınızı 140 karakter olarak düzenleyiniz");
            } else {
                $(".numarator").css("color", "black");
                $("#mesajGonder").css("display", "inline-block");
                $(".mesajUyari").css({ "display": "none", "color": "red" });
            }
        });
    });

    // search işlemleri
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
    // search işlemleri bitiş

    $("#txtUserName").css("display", "none");
    $("#mesajContent").addClass("form-about-yourself form-control");

    // entry kaydetme
    var basID = $("#h4baslik").html(); // başlık string şeklinde alındı
    var useId = $("#spanUseName").html(); // username adını aldık

    $("#txtBaslikId").css("display", "none");
    $("#txtUserID").css("display", "none");

    $("#txtEntry").addClass("form-about-yourself form-control");
    $("#entryKaydet").addClass("btn btn-info");

    $("#txtBaslikId").val(basID);
    $("#txtUserID").val(useId);
    // entry kaydetme bitiş

    YeniMesajVarMi();//önce mesaj var mı yok mu sonra da bildirim var mı yok mu tetiklenir
})

function ReminderMe() {
    var str = $("#username0").val();
    if (str =="" || str == null) {
        //alert("username alanına kullanıcı adınız yada email adresiniz girilmiş olarak gönderiniz..");
        swal({
            title: "",
            text: "username alanına kullanıcı adınız yada email adresiniz girilmiş olarak gönderiniz..",
            timer: 2000,
            type: "info",
            showConfirmButton: false
        });
    }
    else {
        swal({
            title: "",
            text: "Kullanıcı bilgileriniz gönderilmek üzere..",
            timer: 5000,
            type: "success",
            showConfirmButton: false
        });
        delay(function () {
            var options = {};
            options.url = '/Users/Account/ReminderMe',
            options.type = "POST";
            options.data = "{str:'" + str + "'}",
            options.contentType = "application/json; charset=utf-8",
            options.success = function (result) {  // controller dan dönen partial view html(result) içine kaydedilir
                //alert("Kullanıcı adı ve şifreniz mailinize gönderilmiştir..");
                if (result == "alert('kullanıcı adı geçerli değil')") {
                    swal({
                        title: "",
                        text: "Kullanıcı bilgileriniz gönderilemedi..",
                        timer: 3000,
                        type: "error",
                        showConfirmButton: false
                    });
                } else {
                    swal({
                        title: "",
                        text: "Kullanıcı bilgileriniz gönderildi..",
                        timer: 5000,
                        type: "success",
                        showConfirmButton: false
                    });
                }
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
        }, 200);
    }
}


function YeniMesajVarMi() {
    delay(function () {
        var options = {};
        options.url = '/Users/Message/MesajVarMi',
        options.type = "POST";
        //options.data = "{str:'" + str + "'}",
        options.contentType = "application/json; charset=utf-8",
        options.success = function (result) {  // controller dan dönen partial view html(result) içine kaydedilir
            $(".mesajlar").html(result);
            if (result.length != 8) {
                // mesaj olmadığında dönen değerin 8 olduğu belirlendi
                $("#mesajKutusu").css("color", "red");
            }
            BildirimVarMi();
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
    }, 200);
}

function BildirimVarMi() {
    delay(function () {
        var options = {};
        options.url = '/Users/Message/BildirimVarMi',
        options.type = "POST";
        //options.data = "{entryID:'" + id + "'}",
        options.contentType = "application/json; charset=utf-8",
        options.success = function (result) {
            $(".bildirim").html(result);
            if (result.length != 6) {
                $("#bildirimKutusu").css("color", "red");
            }
            //alert(result);
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

function LikedNotification(id) {
    delay(function () {
        var options = {};
        options.url = '/Users/Message/BildirimBegenme',
        options.type = "POST";
        options.data = "{entryID:'" + id + "'}",
        options.contentType = "application/json; charset=utf-8",
        options.success = function (result) {
            //$(".bildirim").html(result);
            //if (result.length!=6) {
            //    $("#bildirimKutusu").css("color", "red");
            //}
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

function BildirimlerOkundu() {
    delay(function () {
        var options = {};
        options.url = '/Users/Message/BildirimOkundu',
        options.type = "POST";
        //options.data = "{entryID:'" + id + "'}",
        options.contentType = "application/json; charset=utf-8",
        options.success = function (result) {
            $("#bildirimKutusu").css("color", "#9d9d9d");
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

function mesaj() {
    var gonderenName = $("#txtUserName").html();
    var alanName = $("#txtAlanUserName").html();
    var mesaj = $("#mesajContent").val();

    delay(function () {
        var options = {};
        options.url = '/Users/Message/MesajGonder',
        options.type = "POST";
        options.data = "{msg:'" + gonderenName + "-" + alanName + "-" + mesaj + "'}",
        options.contentType = "application/json; charset=utf-8",
        options.success = function (result) {
            $(".mesajlar").html(result);
            $(".close").click();
            swal({
                title: "",
                text: "Mesajınız Gönderildi..",
                timer: 2000,
                type: "success",
                showConfirmButton: false
            });
        };
        options.error = function (err) {
            if (err.status == 500) {
                window.location.href = "/Users/Home/Index";
            } else {
                if (err.status != 0) {
                    //alert(err.statusText);
                    swal({
                        title: "",
                        text: "Mesajınız Gönderilemedi !!",
                        timer: 2000,
                        type: "error",
                        showConfirmButton: false
                    });
                }
            }
        };
        $.ajax(options);
    }, 500);
}

function mesajList(str) {
    delay(function () {
        var options = {};
        options.url = '/Users/Message/MesajListele',
        options.type = "POST";
        options.data = "{str:'" + str + "'}",
        options.contentType = "application/json; charset=utf-8",
        options.success = function (result) {  // controller dan dönen partial view html(result) içine kaydedilir
            $(".xClosedM").hide();
            $("#mesajlarListeler").html(result);
            $("#txtMesajGonder").val("");
            $("#txtMesajGonder").attr("cols", "500");
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
            swal({
                title: "",
                text: "Yorumunuz Silindi..",
                timer: 2000,
                type: "success",
                showConfirmButton: false
            });
        };
        options.error = function (err) {
            if (err.status == 500) {
                window.location.href = "/Users/Home/Index";
            } else {
                if (err.status != 0) {
                    //alert(err.statusText);
                    swal({
                        title: "",
                        text: "Yorumunuz Silinemedi..",
                        timer: 2000,
                        type: "error",
                        showConfirmButton: false
                    });
                }
            }
        };
        $.ajax(options);
    }, 500);
}

function entryLink(id) {
    delay(function () {
        var options = {};
        options.url = '/Users/Home/Listele',
        options.type = "POST";
        options.data = "{id:'" + id + "'}",
        options.contentType = "application/json; charset=utf-8",
        options.success = function (result) {
            $(".xClosed").hide();
            $("#kullaniciEntryListele").html(result);
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
    }, 50);
}
function upd(id) {
    $("#entryContent").css({ "height": "150px" });

    var k = $("#" + id).attr("title"); // o entry nin id si alındı
    $("#h4Duzenle").html(k);

    var e = $("#h5_" + id).html();
    $("#entryContent").html(e);

    var b = $("#" + id).attr("id");
    $("#bid").html(b);
    $("#bid").val(b);

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
    delay(function () {
        var options = {};
        options.url = '/WebHome/Arttir',
        options.type = "POST";
        options.data = "{id:'" + id + "'}",
        options.contentType = "application/json; charset=utf-8",
        options.success = function () {  // controller dan dönen partial view html(result) içine kaydedilir
            //alert("Arttırma işlemi başarılı");
            LikedNotification(id);
            swal({
                title: "",
                text: "Yorum Beğenildi..",
                timer: 2000,
                type: "success",
                showConfirmButton: false
            });
        };
        options.error = function (err) {
            if (err.status == 500) {
                window.location.href = "/Users/Home/Index";
            } else {
                if (err.status != 0) {
                    //alert(err.statusText);
                    swal({
                        title: "",
                        text: "Yorum Beğenilemedi..",
                        timer: 2000,
                        type: "error",
                        showConfirmButton: false
                    });
                }
            }
        };
        $.ajax(options);
    }, 500);
    //
}

function azalt(id) {
    delay(function () {
        var options = {};
        options.url = '/WebHome/Azalt',
        options.type = "POST";
        options.data = "{id:'" + id + "'}",
        options.contentType = "application/json; charset=utf-8",
        options.success = function () {  
            swal({
                title: "",
                text: "Yorum Eksilendi..",
                timer: 2000,
                type: "info",
                showConfirmButton: false
            });
        };
        options.error = function (err) {
            if (err.status == 500) {
                window.location.href = "/Users/Home/Index";
            } else {
                if (err.status != 0) {
                    //alert(err.statusText);
                    swal({
                        title: "",
                        text: "Yorum Oylanamadı..",
                        timer: 2000,
                        type: "error",
                        showConfirmButton: false
                    });
                }
            }
        };
        $.ajax(options);
    }, 500);

}


//function azalt(id) {
//    $.ajax({
//        type: "POST",
//        url: "/WebHome/Azalt/",
//        data: "{ id: '" + id + "'}",
//        succes: function () {   //selector yazıyor
//            //$("#down_" + id).fadeOut; //arka plana gönderme
//            //alert("Azaltma işlemi başarılı");
//            swal({
//                title: "",
//                text: "Yorum Eksilendi..",
//                timer: 2000,
//                type: "info",
//                showConfirmButton: false
//            });
//        },
//        error: function () {
//            //alert("Azaltılamadı !...");
//            swal({
//                title: "",
//                text: "Yorum Oylanamadı..",
//                timer: 2000,
//                type: "info",
//                showConfirmButton: false
//            });
//        }
//    })
//}
