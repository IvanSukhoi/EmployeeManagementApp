(function () {
    var self = this;

    $(function () {
        $(".link", "li").click(function () {
            var $buttonClicked = $(this);
            var employeeType = $buttonClicked.attr('id');

            var Type;
            if (employeeType == 'developer') {
                self.TeamDetailPostBackURL = '/Admin/Employee/EditDeveloper';
            }
            if (employeeType == 'manager') {
                self.TeamDetailPostBackURL = '/Admin/Employee/EditManager';
            }
            if (employeeType == 'serviceWorker') {
                self.TeamDetailPostBackURL = '/Admin/Employee/EditServiceWorker';
            }

            var id = $buttonClicked.attr('data-id');
            var options = { "backdrop": "success", keyboard: false };
            $.ajax({
                type: "GET",
                url: TeamDetailPostBackURL,
                contentType: "application/json; charset=utf-8",
                data: { "id": id },
                datatype: "json",
                success: function (data) {
                    var template = $('#popup').html();
                    var context =
                        {
                            "Method": $.trim(data["ActionMethod"]),
                            "FirstName": $.trim(data["FirstName"]),
                            "MiddleName": $.trim(data["MiddleName"]),
                            "LastName": $.trim(data["LastName"]),
                            "ManagerID": data["ManagerID"],
                            "Position": data["Position"],
                            "ID": data["ID"],
                            "Profession": data["TypeOfWorker"],
                            "Department": data["DepartmentID"],
                            "DepartmentList": data["DepartmentModelList"],
                            "Sex": data["Sex"],
                            "TypeOfWorker": data["TypeOfWorker"],
                            "SexParameters": [
                                { "name": "Female", "value": "0" },
                                { "name": "Male", "value": "1" }
                            ],
                            "PositionParameters": [
                                { "name": "Intern", "value": "0" },
                                { "name": "Junior", "value": "1" },
                                { "name": "Middle", "value": "2" },
                                { "name": "Senior", "value": "3" }
                            ],
                            "ProfessionParameters": [
                                { "name": "SystemAdministrator", "value": "0"},
                                { "name": "BusinessAnalyst", "value": "1"},
                                { "name": "Designer", "value": "2"},
                                { "name": "Recruiter", "value": "3"},
                                { "name": "Bookkeeper", "value": "4" },
                                { "name": "Developer", "value": "5" },
                                { "name": "Manager", "value": "6"}
                            ],
                            "Title": "Edit"
                        }

                    self.DepartmentList = context["DepartmentList"];
                    self.EmployeeId = context["ID"];

                    Handlebars.registerHelper('isSelectedDepartment', function (departmentId) {
                        return context.Department == departmentId ? 'selected' : '';
                    });

                    Handlebars.registerHelper('isSelectedSex', function (sexId) {
                        return context.Sex == sexId ? 'selected' : '';
                    })

                    Handlebars.registerHelper('isSelectedPosition', function (positionId) {
                        return context.Position == positionId ? 'selected' : '';
                    })

                    Handlebars.registerHelper('isSelectedProfession', function (professionId) {
                        return context.Profession == professionId ? 'selected' : '';
                    })

                    Handlebars.registerHelper('if_eq', function (parametr, opts) {
                        if (typeof parametr != 'undefined') {
                            return opts.fn(this);
                        }
                        else {
                            return opts.inverse(this);
                        }
                    })

                    Handlebars.registerHelper('GetProfession', function () {
                        if (employeeType == 'manager')
                            return 'Manager';
                        if (employeeType == 'developer')
                            return 'Developer'
                        else {
                            return data['Profession'];
                        }
                    })

                    var templateScript = Handlebars.compile(template);

                    var html = templateScript(context);

                    $('#myModalContent').html(html);
                    $('#myModal').modal(options);
                    $('#myModal').modal('show');
                },
                error: function () {
                    alert("Dynamic content load failed.");
                }
            });
        });
    });

    $("#closebtn").click(function () {
        $('#myModal').modal('hide')
    });

    $("[data-toggle='save']").click(function () {
        var departmentId = $('#DepartmentID').val();
        $.ajax({
            type: "POST",
            url: self.TeamDetailPostBackURL,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(
                {
                    "FirstName": $('#FirstName').val(),
                    "MiddleName": $('#MiddleName').val(),
                    "LastName": $('#LastName').val(),
                    "ManagerID": $('#ManagerID').val(),
                    "Position": $('#Position').val(),
                    "DepartmentID": departmentId,
                    "Sex": $('#Sex').val(),
                    "TypeOfWorker": $('#TypeOfWorker').val(),
                    "ActionMethod": "Edit",
                    "ID": $('#ID').val()
                }),
            datatype: "json",

            success: function (response) {
                $("td[id =" + self.EmployeeId + "]").text(self.DepartmentList[departmentId - 1].Name);
                $('#myModal').modal('hide');
            },

            error: function () {
                alert("Error");
            }
        });
    });

    $("li[id= 'developer']").click(function () {
        var options = { "backdrop": "success", keyboard: false };
        var template = $('#popup').html();

        $.ajax({
            type: "GET",
            url: '/Admin/Employee/CreateDeveloper',
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (data) {
                var template = $('#popup').html();
                var context =
                    {
                        "ID": data["ID"],
                        "DepartmentList": data["DepartmentModelList"],
                        "SexParameters": [
                            { "name": "Female", "value": "0" },
                            { "name": "Male", "value": "1" }
                        ],
                        "PositionParameters": [
                            { "name": "Intern", "value": "0" },
                            { "name": "Junior", "value": "1" },
                            { "name": "Middle", "value": "2" },
                            { "name": "Senior", "value": "3" }
                        ],
                        "ProfessionParameters": [
                            { "name": "SystemAdministrator", "value": "0" },
                            { "name": "BusinessAnalyst", "value": "1" },
                            { "name": "Designer", "value": "2" },
                            { "name": "Recruiter", "value": "3" },
                            { "name": "Bookkeeper", "value": "4" },
                            { "name": "Developer", "value": "5" },
                            { "name": "Manager", "value": "6" }
                        ],
                        "Sex": data["Sex"],
                        "Position": data["Position"],
                        "Title": "Create"
                    }

                self.TeamDetailPostBackURL = '/Admin/Employee/EditDeveloper';
                self.DepartmentList = contest["DepartmentList"];

                Handlebars.registerHelper('isSelectedDepartment', function (departmentId) {
                    return context.Department == departmentId ? 'selected' : '';
                });

                Handlebars.registerHelper('isSelectedSex', function (sexId) {
                    return context.Sex == sexId ? 'selected' : '';
                })

                Handlebars.registerHelper('isSelectedPosition', function (positionId) {
                    return context.Position == positionId ? 'selected' : '';
                })

                Handlebars.registerHelper('isSelectedProfession', function (professionId) {
                    return context.Profession == professionId ? 'selected' : '';
                })

                Handlebars.registerHelper('if_eq', function (parametr, opts) {
                    if (typeof parametr != 'undefined') {
                        return opts.fn(this);
                    }
                    else {
                        return opts.inverse(this);
                    }
                })

                Handlebars.registerHelper('GetProfession', function () {
                    return "Developer";
                })

                var templateScript = Handlebars.compile(template);

                var html = templateScript(context);

                $('#myModalContent').html(html);
                $('#myModal').modal(options);
                $('#myModal').modal('show');
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        })
    })
}());