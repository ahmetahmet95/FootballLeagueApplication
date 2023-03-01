var scriptApp = function () {
    var comboData;

    function pageInit(options) {

        if (options.teamId == 0) {

            onCreateLoad();
        }
        else {
            onUpdateLoad(options.teamId);
        }

        $("#createBtn").click(function () {

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
                //$.ajax({
                //    url: "https://localhost:7066/api/TeamsApi/UpdateTeamById/" + options.teamId + "/" + $("#name").val(),
                //    type: 'PUT',
                //    dataType: 'json',
                //    contentType: "application/json;charset=utf-8",
                //    success: function (data) {
                //        window.close()
                //        window.open("/Home/Teams", '_parent');
                //    },
                //    error: function (e) {
                //    }
                //});
            }
        });


        $("#canceBtn").click(function () {

            window.open("/Home/PlayedMatches", "_parent");
        });
    }

    function onCreateLoad() {

        $.ajax({
            url: "https://localhost:7066/api/PlayedMatchesApi/GetTeamsForCombo",
            type: 'GET',
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            success: function (res) {
                comboData = res;

                $.each(res, function () {
                    var dropdown1 = $("#homeTeamId");
                    dropdown1.append($("<option />").val(this.id).text(this.name));

                    var dropdown2 = $("#guestTeamId");
                    dropdown2.append($("<option />").val(this.id).text(this.name));
                });
            },
            error: function (e) {
      
            }
        });
    }

    function onUpdateLoad(id) {
        onCreateLoad();
        $.ajax({
            url: "https://localhost:7066/api/PlayedMatchesApi/GetTeamsByIdForCombo/" + id,
            type: 'GET',
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            success: function (res) {
     
         
                $.each(res, function () {
                    debugger;

                    //to do

                    //$("#homeTeamId").val(this.firstTeam.id).text(this.firstTeam.name).trigger('change');
                    //$("#guestTeamId").val(this.secondTeam.id).text(this.secondTeam.name).trigger('change');

                    //var dropdown1 = $("#homeTeamId");
                    //dropdown1.select($("<option />").val(this.firstTeam.id).text(this.firstTeam.name));

                    //var dropdown2 = $("#guestTeamId");
                    //dropdown2.select($("<option />").val(this.secondTeam.id).text(this.secondTeam.name));

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
        onCreateLoad: onCreateLoad,
        onUpdateLoad: onUpdateLoad
    }

}();