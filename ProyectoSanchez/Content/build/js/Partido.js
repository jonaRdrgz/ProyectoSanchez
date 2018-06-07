function GetPartidos() {

    $.ajax({
        type: "post",
        url: "/Partido/GetPartidosPorFechaYTorneo",
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            var htmlBodyTable = "";

            $.each(data, function (i, partido) {
                if ((i + 1) % 2 != 0) {
                    htmlBodyTable += '<tr class="even pointer">';
                } else {
                    htmlBodyTable += '<tr class="odd pointer">';
                }

                htmlBodyTable += '<td align="center">' + partido["NombreLocal"] + '</td><td class=" ">' + partido["NombreVisita"] + '</td >\
                            <td class=" ">'+ partido["GolLocal"] + '</td>\
                            <td class=" ">'+ partido["GolVisita"] + '</td>\
                            <td class=" last">\
                                <a onclick="getInformacionPartido('+ partido["IdPartido"] + "," + partido["IdEquipoLocal"] + "," + partido["IdEquipoVisita"] +
                    "," + partido["GolLocal"] + "," + partido["GolVisita"] + ')"  class="verpartido">+Info</a>\
                            </td>\
                        </tr>';
            });

            $('#previewTablePartidos').DataTable().clear();
            $('#previewTablePartidos').dataTable().fnDestroy();

            $("#previewTablePartidos").append(htmlBodyTable);

            $('#previewTablePartidos').dataTable({
                responsive: true,
                columnDefs: [
                    { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -2 }
                ],
                "language": {
                    "emptyTable": "No hay partidos registrados",
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

function getInformacionPartido(idPartido, idEquipoLocal, idEquipovisita, golLocal, golVisita) { 
    $("#golcasa").val(golLocal);
    $("#golvisita").val(golVisita);
    $("#modalEditarPartido").modal();
}

$().ready(function () {

    GetPartidos();
});