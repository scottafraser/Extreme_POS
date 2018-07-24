var menuNav = function() {
    $(".menu button").click(function() {
        var target = $(this).attr("id");
        $(".display").hide();
        $("#" + target + "-display").show();
    });
}

$(function() {
    menuNav();

    $('.menu button').click(function () {
      $.ajax({
        type: 'GET',
        url: '/Mockup/TestPull',
        success: function (result) {
          $('#food-display').append(result);
        }
      });
    });

});
