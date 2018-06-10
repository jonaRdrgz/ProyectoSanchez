function getInformacionArbitrosPorTorneoXTorneo() {
    var data = {
        idTorneo: $("#idTorneo").val()
    }
    $.ajax({
        type: "post",
        url: "/ArbitrosPorTorneo/GetInformacionArbitrosPorTorneo",
        data: JSON.stringify(data),
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            var htmlBodyTable = "";

            $.each(data, function (i, informacion) {
                if ((i + 1) % 2 != 0) {
                    htmlBodyTable += '<tr class="even pointer">';
                } else {
                    htmlBodyTable += '<tr class="odd pointer">';
                }
                htmlBodyTable += '<td align="center">' + informacion["nombre"] + '</td><td class=" ">' + informacion["Promedio"] + '</td >';
            });

            $('#previewTableInformacionArbitros').DataTable().clear();
            $('#previewTableInformacionArbitros').dataTable().fnDestroy();

            $("#previewTableInformacionArbitros").append(htmlBodyTable);

            $('#previewTableInformacionArbitros').dataTable({
                responsive: true,
                columnDefs: [
                    { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -2 }
                ],
                "language": {
                    "emptyTable": "No hay arbitros en el Torneo",
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