var scriptApp = function () {

    var globalData;
    document.body.className = "loading";

    function pageInit(options) {

        if (options.teamId != 0) {

            onLoad(options);
        }

        $("#createBtn").click(function () {

            if ($("#name").val().length <= 0) {

                toastr.warning("Team name is required!").css("width", "300px");
                return false;
            }

            if (options.teamId == 0) {

                //Create
                var data = {
                    Id: 0,
                    Name: $("#name").val()
                }
                var jsonData = JSON.stringify(data);

                $.ajax({
                    url: "https://localhost:7066/api/TeamsGroupApi/CreateTeamsGroup",
                    type: 'POST',
                    dataType: 'json',
                    contentType: "application/json;charset=utf-8",
                    data: jsonData,
                    success: function (data) {

                        window.close();
                        window.open("/Teams/TeamsGroup", '_parent');
                        document.body.className = "";
                    },
                    error: function (e) {

                        document.body.className = "";
                        errorHandler(e.responseText);
                    }
                });
            }
            else {

                if (globalData.name == $("#name").val()) {

                    window.close()
                    window.open("/Teams/TeamsGroup", '_parent');
                }

                //Update
                var data = {
                    Id: globalData.id,
                    Name: $("#name").val()
                }

                var jsonData = JSON.stringify(data);

                $.ajax({
                    url: "https://localhost:7066/api/TeamsGroupApi/UpdateTeamsGroup",
                    type: 'PUT',
                    dataType: 'json',
                    data: jsonData,
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {

                        window.close()
                        window.open("/Teams/TeamsGroup", '_parent');
                        document.body.className = "";
                    },
                    error: function (e) {

                        document.body.className = "";
                        errorHandler(e.responseText);
                    }
                });
            }

        });

        $("#cancelBtn").click(function () {

            window.close()
            window.open("/Teams/TeamsGroup", '_parent');
        });
        document.body.className = "";
    }

    function onLoad(options) {

        $.ajax({
            url: "https://localhost:7066/api/TeamsGroupApi/GetTeamsGroupById/" + options.teamId,
            type: 'GET',
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            success: function (data) {

                globalData = data;
                $("#name").val(data.name);
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
        init: function (options) {
            pageInit(options);
        }
    }

}();