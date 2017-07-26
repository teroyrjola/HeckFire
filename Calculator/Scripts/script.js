TimeWhenBtn

$("#TimeWhenBtn").click(function () {
    $.get("/HeckFire/CurrentQuest/")
        .done(function (obj) {
            ("#output").value = get("/HeckFire/CurrentQuest/")
        });
})