var scriptApp = function () {

    document.body.className = "loading";
    function pageInit() {

        initTeamGroups();
        onLoad(0);

        $('#teamGroup').change(function () {

            onLoad($(this).val());
        });
    }

    function initTeamGroups() {

        $.ajax({
            url: "https://localhost:7066/api/TeamsApi/GetTeamsGroupForCombo",
            type: 'GET',
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            success: function (res) {

                var dropdown = $("#teamGroup");
                dropdown.append($("<option />").val(0).text('All Groups'));
                $.each(res, function () {

                    dropdown.append($("<option />").val(this.id).text(this.name));
                    document.body.className = "";
                })
            },
            error: function (e) {

                document.body.className = "";
                errorHandler(e.responseText);
            }
        });
    }

    function onLoad(id){

        $.ajax({
            url: "https://localhost:7066/api/TeamsRankApi/GetTeamsRank/" + id,
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