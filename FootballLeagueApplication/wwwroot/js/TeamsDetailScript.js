var scriptApp = function () {
    var globalData;
    function pageInit(options) {

        if (options.teamId != 0) {

            onLoad(options);
        }

        $("#createBtn").click(function () {

            if (options.teamId == 0) {

                //Create
                var data = {
                    Id: 0,
                    Name: $("#name").val()
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
                        alert("Team alredy exist!");
                    }
                });
            }
            else {
                //Update
                var data = {
                    Id: globalData.id,
                    Name: $("#name").val()
                }

                var jsonData = JSON.stringify(data);
              
                $.ajax({
                    url: "https://localhost:7066/api/TeamsApi/UpdateTeam",
                    type: 'PUT',
                    dataType: 'json',
                    data: jsonData,
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        window.close()
                        window.open("/Home/Teams", '_parent');
                    },
                    error: function (e) {
                        alert("Team alredy exist!");
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

        $.ajax({
            url: "https://localhost:7066/api/TeamsApi/GetTeamById/" + options.teamId,
            type: 'GET',
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                debugger;
                globalData = data;
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