var menuDisplay = function() {
    $(".menu button").click(function() {
        var target = $(this).attr("id");
        $(".display").hide();
        $("#" + target + "-display").show();
    });

    $("#tables").click();
}

var menuNav = function() {
    $(".menu button").click(function() {
        $(this).parent().siblings().removeClass("nav-selected");
        $(this).parent().addClass("nav-selected");
    });
    $(".permissions button").click(function() {
        $(this).parent().siblings().removeClass("nav-selected");
        $(this).parent().addClass("nav-selected");
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

var testAjax1 = function() {
    $('.menu button').click(function () {
      $.ajax({
        type: 'GET',
        url: '/drinks',
        success: function (result) {
          $('#drinks-display').html(result);
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

    $.ajax({
        type: 'GET',
        url: '/tickets',
        success: function (result) {
          $('.ticket-display').html(result);
        }
    });
}



$(function() {
    menuNav();
    menuDisplay();



});
