var scriptApp = function () {
    var globalData = [];


    function pageInit() {

        $.ajax({
            url: "https://localhost:7066/api/TeamsApi/GetTeams",
            type: 'GET',
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            success: function (res) {

                globalData = res;

                var trows = "";
                $.each(res, function (i, val) {
                    trows += "<tr id=" + val.id + ">";
                    trows += "<td>" + val.name + "</td>";
                    trows += "<td>" +
                        "<button onclick = 'scriptApp.onEdit(" + val.id + ")' class='btn btn-outline-primary'> Edit</button > " +
                        "<button onclick = 'onDelete(" + val.id + ")' class='btn btn-outline-danger'> Delete</button >" +
                        "</td > ";
                    trows += "</tr>";
                });

                $("#table1 tbody").html(trows);
            },
            error: function (e) {
            }
        });

        $("#teamCreateBtn").click(function () {

            window.open("/Home/TeamsDetail");
        });
    }

    function onEdit(id){

        window.open("/Home/TeamsDetail?id=" + id);
    }

    function onDelete() {

        $.ajax({
            url: "https://localhost:7066/api/TeamsApi/CreateTeam",
            type: 'POST',
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            data: jsonData,
            success: function (res) {
                window.close();
                window.open("/Home/Teams");
            },
            error: function (e) {
            }
        });
    }

    return {
        init: function () {
            pageInit();
        },
        onEdit: onEdit,
        onDelete: onDelete
    }

}();