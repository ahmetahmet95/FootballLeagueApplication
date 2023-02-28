var scriptApp = function () {

    function pageInit() {

        $("#playedMatchesCreateBtn").click(function () {

            window.open("/Home/PlayedMatchesDetail", "_parent");
        });
    }


    return {
        init: function () {
            pageInit();
        }
    }

}();
