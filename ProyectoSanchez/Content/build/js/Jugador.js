function getInfoJugador() {
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
            $('label[id*=NombreJugador]').empty();
            $('label[id*=EdadJugador]').empty();
            $('label[id*=PesoJugador]').empty();
            $('label[id*=AlturaJugador]').empty();
            $.each(data, function (i, player) {
                var name = player["Nombre"];
                var fechaN = player["FechaNacimiento"];
                var fechaInicial = new Date(parseInt(fechaN)).toLocaleString();
                var peso = new String(player["PesoKilos"]).toLocaleString();
                var altura = new String(player["AlturaMetros"]).toLocaleString();
                document.getElementById("NombreJugador").innerText = name;
                document.getElementById("EdadJugador").innerText = fechaInicial;
                document.getElementById("PesoJugador").innerText = peso;
                document.getElementById("AlturaJugador").innerText = altura;
            });
        },
        error: function (data) {
            alert("Ha ocurrido un error: " + JSON.stringify(data));
        }
    });
}