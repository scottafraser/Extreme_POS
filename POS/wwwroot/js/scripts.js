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
        url: '/foods',
        success: function (result) {
          $('#food-display').html(result);
        }
      });
    });
}

var testAjax2 = function() {
    $('.menu button').click(function () {
      $.ajax({
        type: 'GET',
        url: '/table/map',
        success: function (result) {
          $('#tables-display').html(result);
        }
      });
    });
}

$(function() {
    menuNav();
    menuDisplay();
    testAjax();
    testAjax2();

});
