(function($) {

    var trigger = ".subscriptions-header",
        target = ".subscriptions-list";

    function toggleList(triggerElement, animate) {
        $(triggerElement).next(target).slideToggle(animate);
    }

    $(function() {

        $(trigger).on("click", function (event) {
            toggleList(this, true);

            event.preventDefault();
        });
    });

})(jQuery);