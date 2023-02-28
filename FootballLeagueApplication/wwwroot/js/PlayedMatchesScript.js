var scriptApp = function () {

    

    function pageInit() {

        $("#playedMatchesCreateBtn").click(function () {

            window.open("/Home/PlayedMatchesDetail", "_parent");
        });

 

    }

    function onLoad() {

        $.ajax({
            url: "https://localhost:7066/api/TeamsApi/GetTeams",
            type: 'GET',
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            success: function (res) {
                debugger;
                var $dropdown = $("#dropdown");
                $.each(res, function () {
                    $dropdown.append($("<option />").val(this.id).text(this.name));
                });

                $("#table1 tbody").html(trows);
            },
            error: function (e) {
            }
        });
       
    }

    return {
        init: function () {
            pageInit();
        },
        onLoad: onLoad
    }

}();
