var scriptApp = function () {

    document.body.className = "loading";
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
                document.body.className = "";
            },
            error: function (e) {

                document.body.className = "";
                errorHandler(e.responseText);
            }
        });
    }

    function errorHandler(responseText) {

        toastr.warning(responseText).css("width", "300px");
    }

    return {
        init: function () {
            pageInit();
        },
        onLoad: onLoad
    }

}();