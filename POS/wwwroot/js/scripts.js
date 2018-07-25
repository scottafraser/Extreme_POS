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
          $('.food-orders tbody').append(result);
        }
      });
}

var drinksFlood = function() {
      $.ajax({
        type: 'GET',
        url: '/drinks-get',
        success: function (result) {
          $('#drinks-display').html(result);
        }
      });
}

var drinkAdd = function(id) {
    $.ajax({
        type: 'post',
        data: { id: id },
        url: '/drinks-add',
        success: function (result) {
          $('.drink-orders tbody').append(result);
        }
      });
}

$(function() {
    menuNav();
    menuDisplay();
    foodFlood();
    drinksFlood();

    $(document).on("click", ".food-item", function () {
        var foodName = $(this).children().first().html();
        var foodId = $(this).children().first().siblings().html();
        foodAdd(foodName, foodId);
    });

    $(document).on("click", ".drinks-item", function () {
        var drinkId = $(this).children().first().siblings().html();
        drinkAdd(drinkId);
    });

    $(".wide-table").click(function() {
        var tableId = $(this).children().children().text());
        $.ajax({
        type: 'get',
        data: { id: tableId},
        url: '/tickets',
        success: function (result) {
          $('.ticket-display').html(result);
        }
      });
    });

});
