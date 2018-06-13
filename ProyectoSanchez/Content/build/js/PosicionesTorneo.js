function getPosicionesXTorneo() {
    var data = {
        idTorneo: $("#idTorneo").val()
    }
    $.ajax({
        type: "post",
        url: "/Torneo/GetPosicionesTorneo",
        data: JSON.stringify(data),
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            var htmlBodyTable = "";

            $.each(data, function (i, posicion) {
                if ((i + 1) % 2 != 0) {
                    htmlBodyTable += '<tr class="even pointer">';
                } else {
                    htmlBodyTable += '<tr class="odd pointer">';
                }
                htmlBodyTable += '<td align="center">' + posicion["nombre"] + '</td><td class=" ">' + posicion["Jugados"] + '</td >\
                            <td class=" ">'+ posicion["Ganados"] + '</td>\
                            <td class=" ">'+ posicion["Empatados"] + '</td>\
                            <td class=" ">'+ posicion["Perdidos"] + '</td>\
                            <td class=" ">'+ posicion["GolesAFavor"] + '</td>\
                            <td class=" ">'+ posicion["GolesEnContra"] + '</td>\
                            <td class=" ">'+ posicion["Puntos"] + '</td>';
            });

            $('#previewTablePosiciones').DataTable().clear();
            $('#previewTablePosiciones').dataTable().fnDestroy();

            $("#previewTablePosiciones").append(htmlBodyTable);

            $('#previewTablePosiciones').dataTable({
                responsive: true,
                columnDefs: [
                    { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -2 }
                ],
                "language": {
                    "emptyTable": "No hay equipos en el torneo",
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

