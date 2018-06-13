function getGolPorPartidoPorFecha() {
    $.ajax({
        type: "post",
        url: "/GolesPorPartidoPorFecha/GetGolesPorPartidoPorFecha",
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            var htmlBodyTable = "";

            $.each(data, function (i, goles) {
                if ((i + 1) % 2 != 0) {
                    htmlBodyTable += '<tr class="even pointer">';
                } else {
                    htmlBodyTable += '<tr class="odd pointer">';
                }

                htmlBodyTable += '<td >' + goles["nombre"] + '</td><td class=" ">' + goles["CantidadGoles"] + '</td >';
            });

            $('#previewInfoGolesPorPartido').DataTable().clear();
            $('#previewInfoGolesPorPartido').dataTable().fnDestroy();

            $("#previewInfoGolesPorPartido").append(htmlBodyTable);

            $('#previewInfoGolesPorPartido').dataTable({
                responsive: true,
                columnDefs: [
                    { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -2 }
                ],
                "language": {
                    "emptyTable": "No hay goles registrados",
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
    getGolPorPartidoPorFecha();
});