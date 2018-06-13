function GetEncuentros() {
    $.ajax({
        type: "post",
        url: "/PartidoHistorico/GetInformacionEncuentros",
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
                var fecha = partido["FechaJuego"].replace(/\/Date\((-?\d+)\)\//, '$1');
                var fechaInicial = new Date(parseInt(fecha));
                htmlBodyTable += '<td align="center">' + partido["NombreLocal"] +
                    '</td><td class=" ">' + partido["NombreVisita"] + '</td >\
                            <td class=" ">'+ partido["GolLocal"] + '</td>\
                            <td class=" ">'+ partido["GolVisita"] + '</td>\
                            <td class=" ">'+ fechaInicial + '</td>\
                            <td class=" ">'+ partido["NombreTorneo"] + '</td>\ </tr>';
            });

            $('#previewEncuentros').DataTable().clear();
            $('#previewEncuentros').dataTable().fnDestroy();

            $("#previewEncuentros").append(htmlBodyTable);

            $('#previewEncuentros').dataTable({
                responsive: true,
                columnDefs: [
                    { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -2 }
                ],
                "language": {
                    "emptyTable": "No hay encuentros registrados",
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
    GetEncuentros();
});