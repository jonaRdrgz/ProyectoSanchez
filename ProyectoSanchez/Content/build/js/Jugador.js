function getInfoJugador() {
    $("#nombreJugador").html("");
    $("#edad").html("");
    $("#peso").html("");
    $("estatura").html("");
    var data = {
        idJugador: $("#idJugador").val()
    }
    $.ajax({
        type: "post",
        url: "/Jugador/GetInformacionJugador",
        data: JSON.stringify(data),
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            $.each(data, function (i, player) {
                var name = player["Nombre"];
                var fecha = player["FechaNacimiento"].replace(/\/Date\((-?\d+)\)\//, '$1');
                var fechaInicial = new Date(parseInt(fecha));
                var month = fechaInicial.getMonth() + 1;
                var day = fechaInicial.getDate();
                var year = fechaInicial.getFullYear();
                var dateF = day + "-" + month + "-" + year;
                var peso = new String(player["PesoKilos"]).toLocaleString();
                var altura = new String(player["AlturaMetros"]).toLocaleString();
                $("#nombreJugador").html(name);
                $("#edad").html(dateF);
                $("#peso").html(peso);
                $("#estatura").html(altura);
                $("#foto").attr("src", 'player["Imagen"]');
                document.getElementById("botonEquiposJugador").setAttribute("onClick",'rediredToEquiposJugador('+$('#idJugador').val() +')');
            });
            
        },
        error: function (data) {
            alert("Ha ocurrido un error: " + JSON.stringify(data));
        }
    });
}

function rediredToEquiposJugador(idJugador) {
    //Redirigir a paritdo
    window.location.replace("/JugadorPorEquipoPorTorneo/Index?" + "idJugador=" + idJugador);
}