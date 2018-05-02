(function () {
    var self = this;

    $(function () {
        $(".edit").click(function () {
            var $buttonClicked = $(this);
            var TeamDetailPostBackURL = '/Admin/Department/Edit';
            var id = $buttonClicked.attr('data-id')
            var options = { "backdrop": "success", keyboard: false };
            $.ajax({
                type: "Get",
                url: TeamDetailPostBackURL,
                contentType: "application/json; charset=utf-8",
                data: { "id": id },
                datatype: "json",
                success: function (data) {
                    var template = $('#popup').html();
                    var templateScript = Handlebars.compile(template);

                    data.Title = "Edit";
                    self.departmentId = data["Id"];
                    var html = templateScript(data);

                    $('#myModalContent').html(html);
                    $('#myModal').modal(options);
                    $('#myModal').modal('show');
                },
                error: function () {
                    alert("Dynamic content load failed.");
                }
            })
        })
    })

    $(function () {
        $("#create").click(function () {
            var options = { "backdrop": "success", keyboard: false };
            var template = $('#popup').html();

            var templateScript = Handlebars.compile(template);
            var data = { "Title": "Create" };

            var html = templateScript(data);

            $('#myModalContent').html(html);
            $('#myModal').modal(options);
            $('#myModal').modal('show');
        })
    })

    $(function () {
        $(".delete").click(function () {
            var $buttonClicked = $(this);
            var html = $("#delete-popup").html();
            var options = { "backdrop": "success", keyboard: false };

            self.Id = $buttonClicked.attr('data-id');

            var data = { "id": self };

            $('#myModalContent').html(html);
            $('#myModal').modal(options);
            $('#myModal').modal('show');
        });
    });

    $("button[data-toggle='delete']").click(function () {
        var TeamDetailPostBackURL = '/Admin/Department/Delete';

        var Id = self.Id;
        $.ajax({
            type: "POST",
            url: TeamDetailPostBackURL,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ "id": Id }),
            datatype: "json",

            success: function (response) {
                alert(response.message);
                $("[name =" + Id + "]").remove();
                $('#myModal').modal('hide');
            },

            error: function () {
                alert("Error");
            }
        });

        self.Id = undefined;
    });

    $("button[data-toggle ='save']").click(function () {
        var departmentName = $('#Name').val();
        var id = $('#Id').val();
        $.ajax({
            type: "POST",
            url: "/Admin/Department/Edit",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(
                {
                    "Name": departmentName,
                    "Id": $('#Id').val()
                }),
            datatype: "json",

            success: function () {
                $('#myModal').modal('hide');
            },

            error: function () {
                alert("Error");
            }
        })
    });

    $("#closebtn").click(function () {
        $('#myModal').modal('hide')
    });
} ());
