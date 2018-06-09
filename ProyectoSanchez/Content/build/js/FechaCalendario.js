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
                var milli = fecha["FechaProgramada"].replace(/\/Date\((-?\d+)\)\//, '$1');

                var fechaInicial = new Date(parseInt(milli)).toLocaleString();
                htmlBodyTable += '<td align="center">' + fechaInicial + '</td>\
                            <td class=" last">\
                                <a onclick="rediredToPartido('+ fecha["IdFecha"] + "," + fecha["IdTorneo"] + ')"  class="">+Info</a>\
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

function rediredToPartido(idFecha, idTorneo) {
    //Redirigir a paritdo
    window.location.replace("/Partido/Index?" + "IdFecha=" + idFecha + "&IdTorneo=" + idTorneo);
}