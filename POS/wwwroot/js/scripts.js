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

var foodFlood = function() { // populates the food tab buttons
      $.ajax({
        type: 'GET',
        url: '/food-get',
        success: function (result) {
          $('#food-display').html(result);
        }
      });
}

var foodAdd = function(name, id) { // hopefully tosses said buttons into ticket
    $.ajax({
        type: 'post',
        data: { name: name, id: id },
        url: '/food-add',
        success: function (result) {
          $('.food-orders').html(result);
        }
      });
}

$(function() {
    menuNav();
    menuDisplay();
    foodFlood();

    $(document).on("click", ".food-item", function () {
        var foodName = $(this).children().first().html();
        var foodId = $(this).children().first().siblings().html());
        foodAdd(foodName, foodId);

    });
});
