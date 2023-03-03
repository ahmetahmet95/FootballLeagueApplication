var scriptApp = function () {
    var lastVal1, lastVal2, lastVal3, lastVal4, lastVal5, lastVal6;

    document.body.className = "loading";
    function pageInit(options) {

        onLoad(options.teamId);

        $("#createBtn").click(function () {

            if (isChanged()) {

                if ($('#homeTeamId').find(":selected").val() != $('#guestTeamId').find(":selected").val()) {

                    if (options.teamId == 0) {

                        //Create
                        var data = {
                            Id: 0,
                            FirstTeamId: parseInt($('#homeTeamId').find(":selected").val()),
                            FirstTeamScore: parseInt($('#homeTeamPoints').find(":selected").val()),
                            SecondTeamId: parseInt($('#guestTeamId').find(":selected").val()),
                            SecondTeamScore: parseInt($('#guestTeamPoints').find(":selected").val()),
                            FirstTeamGoal: $('#firstTeamGoal').val(),
                            SecondTeamGoal: $('#secondTeamGoal').val()
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
                                window.open("/Teams/PlayedMatches", '_parent');
                                document.body.className = "";
                            },
                            error: function (e) {

                                document.body.className = "";
                                errorHandler(e.responseText);
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
                            FirstTeamGoal: $('#firstTeamGoal').val(),
                            SecondTeamGoal: $('#secondTeamGoal').val()
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
                                window.open("/Teams/PlayedMatches", '_parent');
                                document.body.className = "";
                            },
                            error: function (e) {

                                document.body.className = "";
                                errorHandler(e.responseText);
                            }
                        });
                    }
                }
                else {

                    toastr.warning("Both teams cannot be equal!").css("width", "300px");

                }
            }
        });

        $("#cancelBtn").click(function () {

            window.open("/Teams/PlayedMatches", "_parent");
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
            url: "https://localhost:7066/api/PlayedMatchesApi/GetTeamsByIdForCombo/" + id,
            type: 'GET',
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            success: function (res) {

                $("#homeTeamId").val(res[0].firstTeam.id).trigger('change');
                $("#guestTeamId").val(res[0].secondTeam.id).trigger('change');
                $("#homeTeamPoints").val(res[0].firstTeamScore).change();
                $("#guestTeamPoints").val(res[0].secondTeamScore).change();
                $("#firstTeamGoal").val(res[0].firstTeamGoal);
                $("#secondTeamGoal").val(res[0].secondTeamGoal);

                lastVal1 = $("#homeTeamId").val();
                lastVal2 = $("#guestTeamId").val();
                lastVal3 = $("#homeTeamPoints").val();
                lastVal4 = $("#guestTeamPoints").val();
                lastVal5 = $("#firstTeamGoal").val();
                lastVal6 = $("#secondTeamGoal").val();

                document.body.className = "";
            },
            error: function (e) {

                document.body.className = "";
                errorHandler(e.responseText);
            }
        });
    }

    function isChanged() {

        if (lastVal1 == $("#homeTeamId").val() &&
            lastVal2 == $("#guestTeamId").val() &&
            lastVal3 == $("#homeTeamPoints").val() &&
            lastVal4 == $("#guestTeamPoints").val() &&
            lastVal5 == $("#firstTeamGoal").val() &&
            lastVal6 == $("#secondTeamGoal").val()) {

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