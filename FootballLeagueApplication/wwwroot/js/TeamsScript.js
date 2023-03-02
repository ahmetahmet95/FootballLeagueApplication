var scriptApp = function () {

    document.body.className = "loading";
    renderTable();
    function pageInit() {

        $("#teamCreateBtn").click(function () {

            window.open("/Home/TeamsDetail", "_parent");
        });
    }

    function renderTable() {

        $.ajax({
            url: "https://localhost:7066/api/TeamsApi/GetTeams",
            type: 'GET',
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            success: function (res) {
              
                var trows = "";
                $.each(res, function (i,val) {
                    trows += "<tr id=" + val.id + ">";
                    trows += "<td>" + val.name + "</td>";
                    trows += "<td>" +
                        "<button onclick = 'scriptApp.onEdit(" + val.id + ")' class='btn btn-outline-primary'> <i class='fa fa-pencil-square' aria-hidden='true'></i> Edit</button > " +
                        "<button onclick = 'scriptApp.onDelete(" + val.id + ")' class='btn btn-outline-danger'><i class='fa fa-trash' aria-hidden='true'></i> Delete</button >" +
                        "</td > ";
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
    };

    function onEdit(id) {

        window.open("/Home/TeamsDetail?id=" + id, "_parent");
    }

    function onDelete(id) {

        $.ajax({
            url: "https://localhost:7066/api/TeamsApi/DeleteTeamById/" + id,
            type: 'DELETE',
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            success: function (res) {

                renderTable();
                document.body.className = "";
                toastr.success("Deleted successfully!").css("width", "300px");

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
        onEdit: onEdit,
        onDelete: onDelete,
        renderTable: renderTable
    }

}();