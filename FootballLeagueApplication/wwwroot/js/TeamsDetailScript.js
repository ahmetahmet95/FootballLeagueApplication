var scriptApp = function () {

    function pageInit() {



        $("#canceBtn").click(function () {

            window.close()  

        });

        $("#createBtn").click(function () {

            var obj = {
                Name: $("#fname").val()
            }

            $.ajax({
                url: "https://localhost:7066/api/TeamsApi/CreateTeams",
                type: 'POST',
                dataType: 'json',
                contentType: "application/json;charset=utf-8",
                data: obj,
                success: function (res) {

                },
                error: function (e) {
                }
            });

        });
        
    }




    return {
        init: function () {
            pageInit();
        }
    }

}();