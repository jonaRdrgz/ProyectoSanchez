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
                    "," + partido["GolLocal"] + "," + partido["GolVisita"] + "," + partido["IdTorneo"] + ')"  class="verpartido">+Info</a>\
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

function getInformacionPartido(idPartido, idEquipoLocal, idEquipovisita, golLocal, golVisita, IdTorneo) { 
    $("#golcasa").val(golLocal);
    $("#golvisita").val(golVisita);
    getEquiposXTorneo(IdTorneo, $("#local")); 
    getEquiposXTorneo(IdTorneo, $("#visita"));
    $("#modalEditarPartido").modal();
}

function getEquiposXTorneo(IdTorneo, tag) {
    var data = {
        IdTorneo: IdTorneo
    }
    tag.html("<option value='none'>Seleccione una Opción</option>");

    $.ajax({
        type: "post",
        url: "/Partido/GetEquiposXTorneo",
        data: JSON.stringify(data),
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            var htmlSelect = "";

            $.each(data, function (i, equipo) {
                htmlSelect += '<option value="' + equipo["IdEquipo"] + ' " > ' + equipo["Nombre"] + '</option>'; 
                
            });
            tag.append(htmlSelect);

        },
        error: function (data) {
            alert("Ha ocurrido un error: " + JSON.stringify(data));
        }
    });
}


$().ready(function () {

    GetPartidos();

    //----------------------------- Validación para Guardar Formulario --------------
    $("#botonGuardar").click(function () {


        $("#local").rules("add", {
            valorDiferenteA: "none",
            valorDiferenteA: $("#visita").val(),
            messages: {
                valorDiferenteA: "Seleccione un equipo local",
                valorDiferenteA: "Seleccione un equipo diferente",
            }
        });

        $("#visita").rules("add", {
            valorDiferenteA: "none",
            valorDiferenteA: $("#local").val(),
            messages: {
                valorDiferenteA: "Seleccione un equipo visitante",
                valorDiferenteA: "Seleccione un equipo diferente",
            }
        });

        $('#golcasa').rules("add", {
            required: true,
            number: true,
            messages: {
                required: "La cantidad es obligatoria",
                number: "Ingrese solamente números"
            }
        });

        $('#golvisita').rules("add", {
            required: true,
            number: true,
            messages: {
                required: "La cantidad es obligatoria",
                number: "Ingrese solamente números"
            }
        });


        tipoGuardado = 2;

        $("#formPartido").valid();
    });

    $("#formPartido").validate({
        submitHandler: function (form) {
           
            var partido = {
                equipoLocal: $("#local").val(),
                equipoVisita: $("#visita").val(),
                golCasa: $("#golcasa").val(),
                golVisita: $("#golvisita").val()
            }
            console.log(JSON.stringify(partido));

            //$.ajax({
            //    type: "post",
            //    url: "/Partido/" + funcion,
            //    data: JSON.stringify(partido),
            //    dataType: "json",
            //    contentType: "application/json",
            //    success: function (data) {
            //        var code = data["CODE"]
            //        if (code === "PARTIDO_GUARDADO" ) {
            //            window.location.replace("../Home/Index");
            //        } else {
            //            alert("Hubo un error enviando el formulario. Si el problema persiste, contacte a soporte.");
            //        }
            //    },
            //    error: function (data) {
            //        alert("Ha ocurrido un error: " + JSON.stringify(data));
            //    }
            //});
            return false;
        }
    });

    $.validator.addMethod("valorDiferenteA", function (value, element, arg) {
        return arg !== value;
    }, "Value must not equal arg.");

    $.validator.addMethod("justificacionObligatoriaConRespuesta", function (value, element, arg) {
        var idPregunta = element.id.replace('justPreg', '');
        var respuesta = $("input[name='pregunta" + idPregunta + "']:checked").data('name');
        if (respuesta === arg && value === "") {
            return false;
        } else {
            return true;
        }
        return false;
    }, 'La justificación es obligatoria.');
});