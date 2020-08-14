(function ($) {
    $(document).ready(function () {
        $('.menuWindovs').on('click', function () {
            var element = $(this).parent('li');
            if (element.hasClass('open')) {
                element.removeClass('open');
                element.find('ul').slideUp();
            }
            else {
                element.addClass('open');
                element.children('ul').slideDown();
                element.siblings('li').removeClass('open');
            }
        });
        $('#mainMenu>ul>li.has-sub>a').append('<span class="holder"></span>');
    });
})(jQuery);