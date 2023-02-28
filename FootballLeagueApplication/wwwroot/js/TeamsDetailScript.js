var scriptApp = function () {

    function pageInit(options) {

        if (options.teamId != 0) {

            onLoad(options);
        }

        $("#createBtn").click(function () {

            if (options.teamId == 0) {

                //Create
                var data = {
                    Id: 0,
                    Name: $("#name").val(),
                    CreatedBy: 'Admin',
                    CreatedOn: new Date(),
                    UpdatedBy: 'Admin',
                    UpdatedOn: new Date()
                }
                var jsonData = JSON.stringify(data);

                $.ajax({
                    url: "https://localhost:7066/api/TeamsApi/CreateTeam",
                    type: 'POST',
                    dataType: 'json',
                    contentType: "application/json;charset=utf-8",
                    data: jsonData,
                    success: function (data) {
                        window.close()
                        window.open("/Home/Teams", '_parent');
                    },
                    error: function (e) {
                    }
                });
            }
            else {

                //Update
                $.ajax({
                    url: "https://localhost:7066/api/TeamsApi/UpdateTeamById/" + options.teamId+ "/"+ $("#name").val(),
                    type: 'PUT',
                    dataType: 'json',
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        window.close()
                        window.open("/Home/Teams", '_parent');
                    },
                    error: function (e) {
                    }
                });
            }
           
        });

        $("#canceBtn").click(function () {

            window.close()
            window.open("/Home/Teams", '_parent');
        });
    }

    function onLoad(options) {

        debugger;
        $.ajax({
            url: "https://localhost:7066/api/TeamsApi/GetTeamById/" + options.teamId,
            type: 'GET',
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            success: function (data) {

                $("#name").val(data.name);
            },
            error: function (e) {
            }
        });
    }


    return {
        init: function (options) {
            pageInit(options);
        }
    }

}();