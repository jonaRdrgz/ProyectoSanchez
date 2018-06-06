function getFechasCalendarioXTorneo() {
    var data = {
        idTorneo : $("#idTorneo").val()
    }


    $.ajax({
        type: "post",
        url: "/Home/GetFechasProgramadasXTorneo",
        data: JSON.stringify(data),
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            var htmlBodyTable = "";

            $.each(data, function (i, fecha) {
                if ((i + 1) % 2 != 0) {
                    htmlBodyTable += '<tr class="even pointer">';
                } else {
                    htmlBodyTable += '<tr class="odd pointer">';
                }
                htmlBodyTable += '<td align="center">' + fecha["FechaProgramada"] + '</td>\
                            <td class=" last">\
                                <a onclick="rediredToEdit('+ fecha["IdFecha"] + "," + fecha["IdTorneo"] + ')"  class="verForm">+Info</a>\
                            </td>\
                        </tr>';
            });

            $('#previewTableFechas').DataTable().clear();
            $('#previewTableFechas').dataTable().fnDestroy();

            $("#previewTableFechas").append(htmlBodyTable);

            $('#previewTableFechas').dataTable({
                responsive: true,
                columnDefs: [
                    { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -2 }
                ],
                "language": {
                    "emptyTable": "No hay Fechas programadas",
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