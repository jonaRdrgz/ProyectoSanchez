function GetEquipoJugador() {
    $.ajax({
        type: "post",
        url: "/JugadorPorEquipoPorTorneo/GetInformacionJugador",
        data: JSON.stringify(data),
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            var htmlBodyTable = "";

            $.each(data, function (i, jugador) {
                if ((i + 1) % 2 != 0) {
                    htmlBodyTable += '<tr class="even pointer">';
                } else {
                    htmlBodyTable += '<tr class="odd pointer">';
                }

                htmlBodyTable += '<td align="center">' + jugador["NombreEquipo"] + '</td><td class=" ">' + jugador["Posicion"] + '</td >\
                            <td class=" ">'+ jugador["Anno"] + '</td>\
                            <td class=" ">'+ jugador["Periodo"] + '</td>\
                            <td class=" ">'+ jugador["Evaluacion"] + '</td>\ </tr>';
            });

            $('#previewInfoJugador').DataTable().clear();
            $('#previewInfoJugador').dataTable().fnDestroy();

            $("#previewInfoJugador").append(htmlBodyTable);

            $('#previewInfoJugador').dataTable({
                responsive: true,
                columnDefs: [
                    { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -2 }
                ],
                "language": {
                    "emptyTable": "No hay jugadores registrados",
                    "paginate": {
                        "previous": "Siguiente",
                        "next": "Anterior"
                    },
                    "info": "Mostrando página _PAGE_ de _PAGES_",
                    "search": "Buscar:",
                    "lengthMenu": "Mostrar _MENU_ entradas",
                }
            });

        },
        error: function (data) {
            alert("Ha ocurrido un error: " + JSON.stringify(data));
        }
    });
}

$().ready(function () {
    GetEquipoJugador();
});