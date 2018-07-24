var menuDisplay = function() {
    $(".menu button").click(function() {
        var target = $(this).attr("id");
        $(".display").hide();
        $("#" + target + "-display").show();
    });
}

var menuNav = function() {
    $(".menu button").click(function() {
        $(this).parent().siblings().removeClass("selected");
        $(this).parent().addClass("selected");
    });
}

var testAjax = function() {
    $('.menu button').click(function () {
      $.ajax({
        type: 'GET',
        url: '/Mockup/TestPull',
        success: function (result) {
          $('#food-display').html(result);
        }
      });
    });
}

var menu

$(function() {
    menuNav();
    menuDisplay();
    testAjax();


});
