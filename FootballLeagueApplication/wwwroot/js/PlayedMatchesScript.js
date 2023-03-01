var scriptApp = function () {

    
    renderTable();
    function pageInit() {

        $("#playedMatchesCreateBtn").click(function () {

            window.open("/Home/PlayedMatchesDetail", "_parent");
        });

 

    }

    function renderTable() {
   
        $.ajax({
            url: "https://localhost:7066/api/PlayedMatchesApi/GetPlayedMatches",
            type: 'GET',
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            success: function (res) {

                var trows = "";
                $.each(res, function (i, val) {
         
                    trows += "<tr id=" + val.id + ">";
                    trows += "<td>" + val.firstTeam.name + "</td>";
                    trows += "<td>" + val.secondTeam.name + "</td>";
                    trows += "<td>" + val.firstTeamScore + "</td>";
                    trows += "<td>" + val.secondTeamScore + "</td>";
                    trows += "<td>" + val.year + "</td>";
                    trows += "<td>" +
                        "<button onclick = 'scriptApp.onEdit(" + val.id + ")' class='btn btn-outline-primary'> Edit</button > " +
                        "<button onclick = 'scriptApp.onDelete(" + val.id + ")' class='btn btn-outline-danger'> Delete</button >" +
                        "</td > ";
                    trows += "</tr>";
                });

                $("#table1 tbody").html(trows);
            },
            error: function (e) {
            }
        });

    };

    function onEdit(id) {
        debugger;
        window.open("/Home/PlayedMatchesDetail?id=" + id, "_parent");

    }


    function onDelete(id) {

        $.ajax({
            url: "https://localhost:7066/api/PlayedMatchesApi/DeletePlayedMatchesById/" + id,
            type: 'DELETE',
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            success: function (res) {

                renderTable();
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
        onDelete: onDelete,
        renderTable: renderTable
    }

}();
