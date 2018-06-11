function getInfoEntrenador() {
    $("#nombreEntrenador").html("");
    $("#fechaInicio").html("");
    $("#fechaNacimiento").html("");
    var data = {
        idEntrenador: $("#idEntrenador").val()
    }
    $.ajax({
        type: "post",
        url: "/Entrenador/GetInformacionEntrenador",
        data: JSON.stringify(data),
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            $.each(data, function (i, player) {
                var name = player["NombreEntrenador"];
                var fecha = player["FechaNacimiento"].replace(/\/Date\((-?\d+)\)\//, '$1');
                var fechaInicial = new Date(parseInt(fecha));
                var month = fechaInicial.getMonth() + 1;
                var day = fechaInicial.getDate();
                var year = fechaInicial.getFullYear();
                var dateF = day + "-" + month + "-" + year;
                var fechaEmpieza = player["FechaInicio"].replace(/\/Date\((-?\d+)\)\//, '$1');
                var fechaEmpieza = new Date(parseInt(fechaEmpieza));
                var month2 = fechaEmpieza.getMonth() + 1;
                var day2 = fechaEmpieza.getDate();
                var year2 = fechaEmpieza.getFullYear();
                var dateE = day2 + "-" + month2 + "-" + year2;
                $("#nombreEntrenador").html(name);
                $("#fechaInicial").html(dateE);
                $("#fechaNacimiento").html(dateF);
                document.getElementById("foto").src = player["Imagen"];
                document.getElementById("botonEquiposEntrenador").setAttribute("onClick", 'rediredToEquiposEntrenador(' + $('#idEntrenador').val() + ')');
            });

        },
        error: function (data) {
            alert("Ha ocurrido un error: " + JSON.stringify(data));
        }
    });
}

function rediredToEquiposEntrenador(idEntrenador) {
    window.location.replace("/EntrenadorPorEquipoPorTorneo/Index?" + "idEntrenador=" + idEntrenador);
}