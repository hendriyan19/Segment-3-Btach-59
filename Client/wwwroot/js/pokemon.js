var text = "";
var gen = "";


$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/"
}).done((result) => {
    $.each(result.results, function (key, val) {
        text += `<tr class="text-center">
                    <td>${key + 1}</td>
                    <td>${val.name}</td>
                    <td>
                    <button type="button" class="btn" style="background-color:darkslateblue"  onclick='getDataPokemon("${val.url}")' data-toggle="modal" data-target="#exampleModal">
                    <b style="color:yellow">Detail</b>
                    </button>
                    </td>
                </tr>`;
    });
    $("#listSW").html(text);
}).fail((error) => {
    console.log(error);
});

function getDataPokemon(url) {
    $.ajax({
        url: url
    }).done((result) => {
        var text = "";
        var img = "";
        var ability1 = ""
        var ability2 = ""
        var type1 = ""
        var type2 = ""
        var name = ""
        var weight = ""
        var height = ""
        var base_experience = ""
        var text = "";


        img = result.sprites.other.dream_world.front_default
        ability1 = result.abilities[0].ability.name;
        type1 = result.types[0].type.name;
        name = result.species.name;
        weight = result.weight;
        height = result.height;
        base_experience = result.base_experience;


        text = `

        <div class="text-center">
      
        <img class="rounded-circle " src="${img}" />
        <br/>
        <span class="badge badge-pill badge-danger">${ability1}</span>
        <span class="badge badge-pill badge-warning">${type1}</span>
        <h6>name : ${name} </h6>
        <h6>weight : ${weight}</h6>
        <h6>height : ${height}</h6>
        <h6>base experience : ${base_experience}</h6>
   

</div>

        `


        $('.modal-body').html(text);
    }).fail((error) => {
        console.log(error);
    });


}


/*<td>${val.gender}</td>*/

