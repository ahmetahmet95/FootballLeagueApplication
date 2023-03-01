var scriptApp = function () {

    function pageInit(options) {

        onLoad(options.teamId);

        $("#createBtn").click(function () {

            if ($('#homeTeamId').find(":selected").val() != $('#guestTeamId').find(":selected").val()) {

                if (options.teamId == 0) {

                    //Create
                    var data = {
                        Id: 0,
                        FirstTeamId: parseInt($('#homeTeamId').find(":selected").val()),
                        FirstTeamScore: parseInt($('#homeTeamPoints').find(":selected").val()),
                        SecondTeamId: parseInt($('#guestTeamId').find(":selected").val()),
                        SecondTeamScore: parseInt($('#guestTeamPoints').find(":selected").val()),
                        Year: new Date().getFullYear(),
                        CreatedBy: 'Admin',
                        CreatedOn: new Date(),
                        UpdatedBy: 'Admin',
                        UpdatedOn: new Date()
                    }
                    var jsonData = JSON.stringify(data);

                    $.ajax({
                        url: "https://localhost:7066/api/PlayedMatchesApi/CreatePlayedMatches",
                        type: 'POST',
                        dataType: 'json',
                        contentType: "application/json;charset=utf-8",
                        data: jsonData,
                        success: function (data) {
                            window.close()
                            window.open("/Home/PlayedMatches", '_parent');
                        },
                        error: function (e) {
                        }
                    });

                }
                else {

                    //Update
                    var data = {
                        Id: options.teamId,
                        FirstTeamId: parseInt($('#homeTeamId').find(":selected").val()),
                        FirstTeamScore: parseInt($('#homeTeamPoints').find(":selected").val()),
                        SecondTeamId: parseInt($('#guestTeamId').find(":selected").val()),
                        SecondTeamScore: parseInt($('#guestTeamPoints').find(":selected").val()),
                        Year: new Date().getFullYear(),
                        CreatedBy: 'Admin',
                        CreatedOn: new Date(),
                        UpdatedBy: 'Admin',
                        UpdatedOn: new Date()
                    }
                    var jsonData = JSON.stringify(data);

                    $.ajax({
                        url: "https://localhost:7066/api/PlayedMatchesApi/UpdatePlayedMatches",
                        type: 'PUT',
                        data: jsonData,
                        dataType: 'json',
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            window.close()
                            window.open("/Home/PlayedMatches", '_parent');
                        },
                        error: function (e) {
                        }
                    });
                }
            }
            else {
                alert("Both teams cannot be equal!");
            }
        });

        $("#canceBtn").click(function () {

            window.open("/Home/PlayedMatches", "_parent");
        });
    }

    function onLoad(id) {

        $.ajax({
            url: "https://localhost:7066/api/PlayedMatchesApi/GetTeamsForCombo",
            type: 'GET',
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            success: function (res) {

                $.each(res, function () {
                    var dropdown1 = $("#homeTeamId");
                    dropdown1.append($("<option />").val(this.id).text(this.name));

                    var dropdown2 = $("#guestTeamId");
                    dropdown2.append($("<option />").val(this.id).text(this.name));
                });

                if (id != 0) {

                    onUpdateLoad(id);
                }

            },
            error: function (e) {

            }
        });
    }

    function onUpdateLoad(id) {

        $.ajax({
            url: "https://localhost:7066/api/PlayedMatchesApi/GetTeamsByIdForCombo/" + id,
            type: 'GET',
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            success: function (res) {

                $.each(res, function () {

                    $("#homeTeamId").val(this.firstTeam.id).trigger('change');
                    $("#guestTeamId").val(this.secondTeam.id).trigger('change');
                    $("#homeTeamPoints").val(this.firstTeamScore).change();
                    $("#guestTeamPoints").val(this.secondTeamScore).change();
                });

            },
            error: function (e) {

            }
        });
    }

    return {
        init: function (options) {
            pageInit(options);
        },
        onLoad: onLoad,
        onUpdateLoad: onUpdateLoad
    }

}();