var scriptApp = function () {


    function pageInit(options) {

        debugger;
        if (options.teamId == 0) {

            onCreateLoad();
        }
        else {
            onUpdateLoad(options.teamId);
        }

        $("#createBtn").click(function () {

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

                var $dropdown = $("#homeTeamId");
                $.each(res, function () {
                    $dropdown.append($("<option />").val(this.id).text(this.name));
                });

                var $dropdown = $("#guestTeamId");
                $.each(res, function () {
                    $dropdown.append($("<option />").val(this.id).text(this.name));
                });

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

                var $dropdown = $("#homeTeamId");
                $.each(res, function () {
                    $dropdown.append($("<option />").val(this.firstTeam.id).text(this.firstTeam.name));
                });

                var $dropdown = $("#guestTeamId");
                $.each(res, function () {
                   
                    $dropdown.append($("<option />").val(this.secondTeam.id).text(this.secondTeam.name));
                    
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