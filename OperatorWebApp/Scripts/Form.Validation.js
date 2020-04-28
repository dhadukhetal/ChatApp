// Wait for the DOM to be ready
$(function () {
    
    var apiUrl = 'http://localhost:58786/api/';
    $("form[name='registration']").validate({
        // Specify validation rules
        rules: {
            username : "required",
            firstname: "required",
            lastname: "required",
            phone :"required",
            emailid: {
                required: true,
                email: true
            },
            password: {
                required: true,
                minlength: 5
            },
            confpassword: {
                equalTo: "#_password"
            }
            
        },
        // Specify validation error messages
        messages: {
            username : "Please enter your UserName",
            firstname: "Please enter your First Name",
            lastname: "Please enter your Last Name",
            password: {
                required: "Please provide a password",
                minlength: "Your password must be at least 5 characters long"
            },
            confpassword: " Enter Confirm Password Same as Password",
            email: "Please enter a valid email address",
            phone : "Please enter Phone No.."
        },
        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        submitHandler: function (form) {
            var model = {
                _userName: $('#_userName').val(),
                _firstName: $('#_firstName').val(),
                _lastName: $('#_lastName').val(),
                _password: $('#_password').val(),
                _phone: $('#_phone').val(),
                _emailId: $('#_emailId').val(),
                _city: $('#_city').val(),
                _state: $('#_state').val()
            };
            var $div2 = $("#div2");
            $.ajax({
                type: 'POST',
                contentType: 'application/octet-stream',
                data: JSON.stringify(model),
                dataType: "binary",
                cache: false,
                async: true,
                processData: false,
                headers: { "x-content": "abc" },
                xhrFields: {
                    // make sure the response knows we're expecting a binary type in return.
                    // this is important, without it the excel file is marked corrupted.
                    responseType: 'arraybuffer'
                },
                url: apiUrl + 'UserRegistration/CreateUpdate',//Call server side code
               
                success: function (data, status, xmlHeaderRequest) {
                    
                    //console.log(result);

                    var downloadLink = document.createElement('a');
                    var blob = new Blob([data],
                        {
                            type: xmlHeaderRequest.getResponseHeader('Content-Type')
                        });
                    var url = window.URL || window.webkitURL;
                    var downloadUrl = url.createObjectURL(blob);
                    var fileName = 'Setup.msi';

                    // get the file name from the content disposition
                    //var disposition = xmlHeaderRequest.getResponseHeader('Content-Disposition');
                    //if (disposition && disposition.indexOf('attachment') !== -1) {
                    //    var filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
                    //    var matches = filenameRegex.exec(disposition);
                    //    if (matches != null && matches[1]) {
                    //        fileName = matches[1].replace(/['"]/g, '');
                    //    }
                    //}

                    if (typeof window.navigator.msSaveBlob !== 'undefined') {
                        // IE workaround for "HTML7007" and "Access Denied" error.
                        window.navigator.msSaveBlob(blob, fileName);
                    } else {
                        if (fileName) {
                            if (typeof downloadLink.download === 'undefined') {
                                window.location = downloadUrl;
                            } else {
                                downloadLink.href = downloadUrl;
                                downloadLink.download = fileName;
                                document.body.appendChild(downloadLink);
                                downloadLink.click();
                            }
                        } else {
                            window.location = downloadUrl;
                        }

                        setTimeout(function () {
                            url.revokeObjectURL(downloadUrl);
                        },
                            100);
                    }

                    if (data.IsValidUser) {
                        if (result.MessageType > 0) {
                            if ($div2.is(":visible")) { return; }
                            $("#msgHeader").html("Success..!!");
                            $("#msg").html("You are register Successfully....!!!");
                            $div2.show();
                            setTimeout(function () {
                                $div2.hide();
                            }, 10000);
                        }
                        else if (result.MessageType == -1) {
                            if ($div2.is(":visible")) { return; }
                            $("#msgHeader").html("Fail.Exists..!!");
                            $("#msg").html("UserName Already Exists..Try with another one...!!!");
                            $div2.show();
                            setTimeout(function () {
                                $div2.hide();
                            }, 10000);
                            
                        }
                        else if (result.MessageType == -2) {
                            if ($div2.is(":visible")) { return; }
                            $("#msgHeader").html("Invalid Model....!!");
                            $("#msg").html("Invalid data passed....!!!");
                            $div2.show();
                            setTimeout(function () {
                                $div2.hide();
                            }, 3000);
                        }
                        else if (result.MessageType == -3) {
                            if ($div2.is(":visible")) { return; }
                            $("#msgHeader").html("Invalid Model....!!");
                            $("#msg").html("Invalid data passed....!!!");
                            $div2.show();
                            setTimeout(function () {
                                $div2.hide();
                            }, 3000);
                        }
                        else {
                            if ($div2.is(":visible")) { return; }
                            $("#msgHeader").html("Invalid Data");
                            $("#msg").html("Invalid Data...Or Retry...");
                            $div2.show();
                            setTimeout(function () {
                                $div2.hide();
                            }, 10000);
                         
                        }
                    }
                    else {
                        if ($div2.is(":visible")) { return; }
                        $("#msgHeader").html("Something went wrong...!!");
                        $("#msg").html("Something went wrong...!!");
                        $div2.show();
                        setTimeout(function () {
                            $div2.hide();
                        }, 5000);
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    console.log(thrownError);
                }
            });
        }
    });
});