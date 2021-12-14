var text = "";
var gen = "";


$.ajax({
    url: "https://swapi.dev/api/people/"
}).done((result) => {
    console.log(result.results);
    var gen = "";
    $.each(result.results, function (key, val) {
        gen = ``;
        if (val.gender == `male`) {

            gen = `<td><i class="fa fa-mars" aria-hidden="true"></i></td>`;

        } else if (val.gender == `female`) {

            gen = `<td><i class="fa fa-venus" aria-hidden="true"></i></td>`;

        } else {

            gen = `<td><i class="fa fa-genderless" aria-hidden="true"></i></td>`;

        }
        console.log(gen);
        text += `<tr class="text-center">
                    <td>${key + 1}</td>
                    <td>${val.name}</td>
                    <td>
                    <a href="${val.url}">
                    <button type="button" class="btn btn-dark"><h6 style="color:yellow">Goto URL</h6></button>
                    </a>
                    </td>
                    <td>${val.height + " Cm"}</td>
                    <td>${val.mass + " Kg"}</td>
                    ${gen}
                    <td>${val.hair_color}</td>
                    <td>${val.skin_color}</td>
                    <td>${val.eye_color}</td>
                </tr>`;
    });
    $("#listSW").html(text);
}).fail((error) => {
    console.log(error);
});



/*<td>${val.gender}</td>*/

