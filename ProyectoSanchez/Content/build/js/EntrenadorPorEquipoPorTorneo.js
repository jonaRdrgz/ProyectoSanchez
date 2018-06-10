function getEquipoEntrenador() {
    $.ajax({
        type: "post",
        url: "/EntrenadorPorEquipoPorTorneo/GetInformacionEntrenador",
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            var htmlBodyTable = "";

            $.each(data, function (i, entrenador) {
                if ((i + 1) % 2 != 0) {
                    htmlBodyTable += '<tr class="even pointer">';
                } else {
                    htmlBodyTable += '<tr class="odd pointer">';
                }

                htmlBodyTable += '<td >' + entrenador["NombreEquipo"] + '</td><td class=" ">' + entrenador["NombreTorneo"] + '</td >\
                            <td class=" ">'+ entrenador["Tipo"] + '</td>\
                            <td class=" ">'+ entrenador["Posicion"] + '</td>\
                            <td class=" ">'+ entrenador["Sinopsis"] + '</td>\
                            <td class=" ">'+ entrenador["Anno"] + '</td>\ </tr>';
            });

            $('#previewInfoEntrenador').DataTable().clear();
            $('#previewInfoEntrenador').dataTable().fnDestroy();

            $("#previewInfoEntrenador").append(htmlBodyTable);

            $('#previewInfoEntrenador').dataTable({
                responsive: true,
                columnDefs: [
                    { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 2, targets: -2 }
                ],
                "language": {
                    "emptyTable": "No hay entrenadores registrados",
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
    getEquipoEntrenador();
});