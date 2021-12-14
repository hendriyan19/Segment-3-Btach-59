function Insert() {
    var obj = new Object();
    obj.nik = $("#nik").val();
    obj.firstName = $("#firstName").val();
    obj.lastName = $("#lastName").val();
    obj.email = $("#email").val();
    obj.phone = $("#phone").val();
    obj.password = $("#password").val();
    var dateTemp = $("#birthDate").val();
    dateNew = new Date(dateTemp);
    obj.birthDate = dateNew.getFullYear()+"/"+ dateNew.getMonth()+"/"+ dateNew.getDay() + "T00:00:00";
    obj.gender = $("#gender").val();
    obj.salary = $("#salary").val();
    obj.universityId = $("#universityId").val();
    obj.degree = $("#degree").val();
    obj.gpa = $("#gpa").val();

    console.log(obj);
    $.ajax({

        headers: {
            /*'Accept': 'application/json',*/
            /*'Content-Type': 'application/json'*/
        },
        type: "POST",
        url: "/Employees/Register",
        dataType: 'json',
        data: obj
    }).done((result) => {


        Swal.fire(
            'Sippp!',
            'Berhasil Di Insert!',
            'success'
        )
    }).fail((error) => {

     

        Swal.fire({
            icon: 'error',
            title: 'Waduhhh',
            text: 'Something went wrong!'
        })



    })
}
