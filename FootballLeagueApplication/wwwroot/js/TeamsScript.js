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
    
    return {
        init: function () {
            pageInit();
        }
    }

}();