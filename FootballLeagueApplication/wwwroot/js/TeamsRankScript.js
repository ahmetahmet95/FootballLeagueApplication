var scriptApp = function () {

    function pageInit() {
        onLoad();

    }

    function onLoad(){

        $.ajax({
            url: "https://localhost:7066/api/TeamsRankApi/GetTeamsRank",
            type: 'GET',
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            success: function (res) {

                var trows = "";
                $.each(res, function (i, val) {
                    trows += "<tr id=" + val.id + ">";
                    trows += "<td>" + val.teams.name + "</td>";
                    trows += "<td>" + val.totalPoint + "</td>";
                    trows += "<td>" + val.year + "</td>";;
                    trows += "</tr>";
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