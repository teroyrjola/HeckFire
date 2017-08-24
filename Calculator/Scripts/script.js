function startUp(){
var smallSize;

if ($('#sidebar').width() < 180) {
    $("select").css("font-size", "11.1px");
    smallSize = 1;
} else {
    $("select").css("font-size", "15px");
    smallSize = 0;
}

$(window).resize(function () {
    if ($('#sidebar').width() < 180) {
        if (!smallSize) {
            $("select").css("font-size", "11.1px");
            smallSize = 1;
        }
    } else {
        if (smallSize) {
            $("select").css("font-size", "15px");
            smallSize = 0;
        }
    }
});

var imageButtons = document.getElementsByClassName("imageButton");

for (var i = 0; i < imageButtons.length; i++) {
    imageButtons[i].style.backgroundImage = "url('../../Content/QuestIcons/" + imageButtons[i].id + ".png')";
}

for (i = 0; i < imageButtons.length; i++) {
    imageButtons[i].setAttribute("aria-checked", false);

    imageButtons[i].addEventListener("click",
        function (event) {
            var button = event.target;
            IfAllFalseThenDisableAll(imageButtons);
            if (button.getAttribute("aria-checked") === "true") {
                UnCheck(button);
            } else {
                Check(button);
                IfAllTrueThenEnableAll(imageButtons);
            }
        });
}

var IfAllFalseThenDisableAll = function (buttons) {

    for (i = 0; i < buttons.length; i++) {
        if (buttons[i].getAttribute("aria-checked") === "true") return false;
    }

    for (i = 0; i < buttons.length; i++) {
        Check(buttons[i]);
    }

    return true;
}

var IfAllTrueThenEnableAll = function (buttons) {

    for (i = 0; i < buttons.length; i++) {
        if (buttons[i].getAttribute("aria-checked") === "false") return true;
    }

    for (i = 0; i < buttons.length; i++) {
        UnCheck(buttons[i]);
    }

    return true;
}

var UnCheck = function (button) {
    button.setAttribute("aria-checked", "false");
    button.style.backgroundColor = "";
}

var Check = function (button) {
    button.style.backgroundColor = "rgba(0,0,0,0.6";
    button.setAttribute("aria-checked", "true");
}
}