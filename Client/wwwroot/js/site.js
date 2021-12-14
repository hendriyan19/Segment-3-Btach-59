
//$.ajax({
//    url: "https://localhost:4321/API/Employees"
//}).done((result) => {
//    var text = "";
//    console.log(result);
//    $.each(result, function (key, val) {
        
//        text += `<tr class="text-center">
//                    <td>${key + 1}</td>
//                    <td>${val.nik}</td>
//                    <td>${val.firstName + " " + val.lastName }</td>
//                    <td>${"+"+val.phone}</td>
//                    <td>${(val.birthDate).substring(0,10)}</td>
//                    <td>${"Rp. " + val.salary + ",00"}</td>
//                    <td>${val.email}</td>
//                    <td>${val.gender}</td>
//                     <td>
//                    <button type="button" class="btn text-light" style="background-color:#191970"  onclick='' data-toggle="modal" data-target="#exampleModal">
//                    <b style="color:midnight-blue">Insert Data</b>
//                    </button>
//                    </td>
//                </tr>`;
//    });
//    $("#listEmployee").html(text);
//}).fail((error) => {
//    console.log(error);
//});

//$(document).ready(function () {
//    $('#tabelEmployee').DataTable({
//        dom: 'Bfrtip',
//        buttons: [
//            'copy', 'csv', 'excel', 'pdf', 'print'
//        ]
//    });
    
//});



$(document).ready(function () {
    
    $("#tabelEmployee").DataTable({
        "ajax": {
            "url": "/Employees/GetAll",
            "dataSrc": ""
        },
        "columns": [

            {
                "data": null,
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { "data": "nik" },

            {
                "data": null,
                "render": function (data, type, row) {
                    return row['firstName'] + ' ' + row['lastName'];
                }
            },
            { "data": "phone" },
            { "data": "birthDate" },
            { "data": "salary" },
            { "data": "email" },
            { "data": "gender" },
            {
                "data": null,
                "render": function (data, type, row, meta) {
                    return '<button type="button" class="btn btn-primary" onclick="Delete(' + row['nik'] + ')"><i class="fas fa-user-minus"></i></button >'
                        + ' <button type="button" class="btn btn-primary" onclick="getDataNIK(' + row['nik'] + ')" data-toggle="modal" data-target="#exampleModal2"><i class="fas fa-user-edit"></i></button >';
                }
            }

        ],
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'copyHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7]
                }
            },
            {
                extend: 'csv',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7]
                }
            },
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7]
                }
            },
            {
                extend: 'pdfHtml5',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7]
                }
            },
            {
                extend: 'print',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7]
                }
            }

        ]
    });
});

function getDataNIK(nik) {
    $.ajax({
        url: "/Employees/get/"+nik,
        success: function (result) {
            var data = result;
            const birthArr = data.birthDate.split("T00:00:00")
            $("#nik2").attr("value", data.nik)
            $("#firstName2").attr("value", data.firstName)
            $("#lastName2").attr("value", data.lastName)
            $("#email2").attr("value", data.email)
            $("#phone2").attr("value", data.phone)
            $("#salary2").attr("value", data.salary)
            $("#gender2").attr("value", data.gender)
            $("#birthDate2").attr("value", birthArr[0])
            document.getElementById('nik2').disabled = true;
            document.getElementById('gender2').disabled = true;
            document.getElementById('birthDate2').disabled = true;
        },
        error: function (error) {
            console.log(error);
        }
    })
}




function Insert() {
    var obj = new Object();

    obj.nik = $("#nik").val();
    obj.firstName = $("#firstName").val();
    obj.lastName = $("#lastName").val();
    obj.email = $("#email").val();
    obj.phone = $("#phone").val();
    obj.birthDate = $("#birthDate").val()+"T00:00:00" ;
    obj.salary = $("#salary").val();
    obj.gender = $("#gender").val();
    console.log(obj);
    $.ajax({

        headers: {
            /*'Accept': 'application/json',*/
            /*'Content-Type': 'application/json'*/
        },
        type: "POST",
        url: "/Employees/post",
        dataType: 'json',
        data: obj
    }).done((result) => {
    
        $('#tabelEmployee').DataTable().ajax.reload()
        
        
        Swal.fire(
            'Sippp!',
            'Berhasil Di Insert!',
            'success'
        )
    }).fail((error) => {
     
        $('#tabelEmployee').DataTable().ajax.reload()
        
        Swal.fire({
            icon: 'error',
            title: 'Waduhhh',
            text: 'Something went wrong!'
        })

        
     
    })
}

function Delete(nik) {

    $.ajax({
        headers: {
            /*'Accept': 'application/json',*/
           /* 'Content-Type': 'application/json'*/
        },
        type: "DELETE",
        url: "/Employees/delete/"+nik,
        dataType: 'json'
    }).done((result) => {
        $('#tabelEmployee').DataTable().ajax.reload()
        Swal.fire(
            'Sippp!',
            'Berhasil Di Delete!',
            'success'
        )
    }).fail((error) => {
        $('#tabelEmployee').DataTable().ajax.reload()
        Swal.fire({
            icon: 'error',
            title: 'Waduhhh',
            text: 'Something went wrong!'
        })

    })
}

function Update() {
    var obj = new Object();
    obj.nik = $("#nik2").val();
    obj.firstName = $("#firstName2").val();
    obj.lastName = $("#lastName2").val();
    obj.email = $("#email2").val();
    obj.phone = $("#phone2").val();
    obj.birthDate = $("#birthDate2").val()+"T00:00:00";
    obj.salary = $("#salary2").val();
    obj.gender = $("#gender2").val();
    $.ajax({

        headers: {
            /*'Accept': 'application/json',*/
           /* 'Content-Type': 'application/json'*/
        },
        type: "PUT",
        url: "/Employees/put",
        dataType: 'json',
        data:obj
    }).done((result) => {
        $('#tabelEmployee').DataTable().ajax.reload()
        Swal.fire(
            'Sippp!',
            'Berhasil Di Update!',
            'success'
        )
    }).fail((error) => {
        $('#tabelEmployee').DataTable().ajax.reload()
        Swal.fire({
            icon: 'error',
            title: 'Waduhhh',
            text: 'Gagal Di Update!'
        })

    })
}
$(document).ready(function () {
    $("#FormEmployee").validate({
        rules: {
            "firstName": { required: true },
            "lastName": { required: true },
            "email": { required: true, email: true },
            "gender": { required: true },
            "phone": { required: true },
            "birthDate": { required: true },
            "salary": { required: true },
            "nik": { required: true }
        },
        errorPacement: function (error, element) { }, highlight: function (element) {
            $(element).closest('.form-control').addClass('is-invalid');
        },
        unhighlight: function (element) {
            $(element).closest(' .form-control').removeClass('is-invalid');
        }
    });
});

