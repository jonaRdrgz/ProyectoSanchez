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
                var fecha = player["FechaNacimiento"].replace(/\/Date\((-?\d+)\)\//, '$1');
                var fechaInicial = new Date(parseInt(fecha));
                var month = fechaInicial.getMonth() + 1;
                var day = fechaInicial.getDate();
                var year = fechaInicial.getFullYear();
                var dateF = day + "-" + month + "-" + year;
                var peso = new String(player["PesoKilos"]).toLocaleString();
                var altura = new String(player["AlturaMetros"]).toLocaleString();
                document.getElementById("NombreJugador").innerText = name;
                document.getElementById("EdadJugador").innerText = dateF;
                document.getElementById("PesoJugador").innerText = peso;
                document.getElementById("AlturaJugador").innerText = altura;
                document.getElementById("botonEquiposJugador").setAttribute("onClick",'rediredToEquiposJugador('+$('#idJugador').val() +')');
            });
            
        },
        error: function (data) {
            $('label[id*=NombreJugador]').innerText = "Nombre:";
            $('label[id*=EdadJugador]').innerText = "Fecha de Nacimiento:";
            $('label[id*=PesoJugador]').innerText = "Peso en kilogramos:";
            $('label[id*=AlturaJugador]').innerText = "Altura en metros:";
            alert("Ha ocurrido un error: " + JSON.stringify(data));
        }
    });
}

function rediredToEquiposJugador(idJugador) {
    //Redirigir a paritdo
    window.location.replace("/JugadorPorEquipoPorTorneo/Index?" + "idJugador=" + idJugador);
}