/*------------------
      SidebarCollapse
   --------------------*/
(function ($) {

    "use strict";

    var fullHeight = function () {

        $('.js-fullheight').css('height', $(window).height());
        $(window).resize(function () {
            $('.js-fullheight').css('height', $(window).height());
        });

    };
    fullHeight();

    $('#sidebarCollape').on('click', function () {
        $('#sidebar').toggleClass('active');
    });

   

})(jQuery);
