var scriptApp = function () {
    var lastVal1, lastVal2;

    var globalData;
    document.body.className = "loading";

    function pageInit(options) {

        onLoad(options.teamId);

        $("#createBtn").click(function () {

            if ($("#name").val().length <= 0) {

                toastr.warning("Team name is required!").css("width", "300px");
                return false;
            }

            if (isChanged()) {

                if (options.teamId == 0) {

                    //Create
                    var data = {
                        Id: 0,
                        Name: $("#name").val(),
                        TeamsGroupId: parseInt($('#teamGroupId').find(":selected").val())

                    }
                    var jsonData = JSON.stringify(data);

                    $.ajax({
                        url: "https://localhost:7066/api/TeamsApi/CreateTeam",
                        type: 'POST',
                        dataType: 'json',
                        contentType: "application/json;charset=utf-8",
                        data: jsonData,
                        success: function (data) {

                            window.close();
                            window.open("/Teams/Teams", '_parent');
                            document.body.className = "";
                        },
                        error: function (e) {

                            document.body.className = "";
                            errorHandler(e.responseText);
                        }
                    });
                }
                else {

                    if (globalData[0].name == $("#name").val()) {

                        window.close()
                        window.open("/Teams/Teams", '_parent');
                    }

                    //Update
                    var data = {
                        Id: globalData[0].id,
                        Name: $("#name").val(),
                        TeamsGroupId: parseInt($('#teamGroupId').find(":selected").val())
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
                            window.open("/Teams/Teams", '_parent');
                            document.body.className = "";
                        },
                        error: function (e) {

                            document.body.className = "";
                            errorHandler(e.responseText);
                        }
                    });
                }
            }
           
        });

        $("#cancelBtn").click(function () {

            window.close()
            window.open("/Teams/Teams", '_parent');
        });
        document.body.className = "";
    }

    function onLoad(id) {
;
        $.ajax({
            url: "https://localhost:7066/api/TeamsApi/GetTeamsGroupForCombo",
            type: 'GET',
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            success: function (res) {

               
                $.each(res, function () {
                    var dropdown = $("#teamGroupId");
                    dropdown.append($("<option />").val(this.id).text(this.name));
                });
      
                if (id != 0) {

                    onUpdateLoad(id);
                }
                document.body.className = "";
            },
            error: function (e) {

                document.body.className = "";
                errorHandler(e.responseText);
            }
        });
    }

    function onUpdateLoad(id) {

        $.ajax({
            url: "https://localhost:7066/api/TeamsApi/GetTeamsGroupByIdForCombo/" + id,
            type: 'GET',
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            success: function (res) {

                globalData = res;
                $("#teamGroupId").val(res[0].teamsGroup.id).trigger('change');
                $("#name").val(res[0].name);

                lastVal1 = $("#teamGroupId").val();
                lastVal2 = $("#name").val();


                document.body.className = "";
            },
            error: function (e) {

                document.body.className = "";
                errorHandler(e.responseText);
            }
        });
    }

    function isChanged() {

        if (lastVal1 == $("#teamGroupId").val() &&
            lastVal2 == $("#name").val()) {

            toastr.warning("Nothing changed!").css("width", "300px");
            return false;
        }
        else {

            return true;
        }
    }

    function errorHandler(responseText) {

        toastr.warning(responseText).css("width", "300px");
    }

    return {
        init: function (options) {
            pageInit(options);
        },
        onLoad: onLoad,
        onUpdateLoad: onUpdateLoad
    }

}();