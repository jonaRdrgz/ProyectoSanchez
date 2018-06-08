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
                                <button  id="buttonPlus" \
                                onclick="getInformacionPartido('+ partido["IdPartido"] + ", " + partido["IdEquipoLocal"] + ", " + partido["IdEquipoVisita"] +
                                    "," + partido["GolLocal"] + "," + partido["GolVisita"] + "," + partido["IdTorneo"] + ');" class="btn-link col-md-12"><i class="fa fa-plus-circle">\
                                 </i></button>\
                            </td>\
                        </tr>';
            });
            //<a onclick="getInformacionPartido('+ partido[" IdPartido"] + ", " + partido["IdEquipoLocal"] + ", " + partido["IdEquipoVisita"] +
            //"," + partido["GolLocal"] + "," + partido["GolVisita"] + "," + partido["IdTorneo"] + ')"  class="verpartido">+Info</a>
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
    $("#idElementos").attr("idPartido", idPartido);
    $("#idElementos").attr("idTorneo", IdTorneo);
    getEquiposXTorneo(IdTorneo, $("#local"), idEquipoLocal);
    getEquiposXTorneo(IdTorneo, $("#visita"), idEquipovisita);
    $("#modalEditarPartido").modal();
}

function getEquiposXTorneo(IdTorneo, tag, idEquipo) {
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

            // Se verifica si el Id del equipo actual es igual al parámetro idEquipo y se selecciona
            $.each(data, function (i, equipo) {
                htmlSelect += '<option value="' + equipo["IdEquipo"] + '" ' + ((equipo["IdEquipo"] == idEquipo) ? "selected" : "") + ' > ' + equipo["Nombre"] + ' </option>'; 
                
            });
            tag.append(htmlSelect);

        },
        error: function (data) {
            alert("Ha ocurrido un error: " + JSON.stringify(data));
        }
    });
}

function agregarPartido() {
    $("#modalAgregarPartidorr").modal();
    
}


$().ready(function () {

    GetPartidos();

    //----------------------------- Validación para Guardar Formulario --------------
    $("#botonGuardar").click(function () {


        $("#local").rules("add", {
            valorDiferente: "none",
            valorDiferenteA: $("#visita").val(),
            messages: {
                valorDiferente: "Seleccione un equipo local",
                valorDiferenteA: "Seleccione un equipo diferente",
            }
        });

        $("#visita").rules("add", {
            valorDiferente: "none",
            valorDiferenteA: $("#local").val(),
            messages: {
                valorDiferente: "Seleccione un equipo visitante",
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
                IdPartido : $("#idElementos").attr('idPartido'),
                IdEquipoLocal: $("#local").val(),
                IdEquipoVisita: $("#visita").val(),
                GolLocal: $("#golcasa").val(),
                GolVisita: $("#golvisita").val(),
                IdTorneo: $("#idElementos").attr('idTorneo')
            }
            console.log(JSON.stringify(partido));

            $.ajax({
                type: "post",
                url: "/Partido/ActualizarPartido",
                data: JSON.stringify(partido),
                dataType: "json",
                contentType: "application/json",
                success: function (data) {
                    var code = data["CODE"]
                    if (code === "PARTIDO_GUARDADO") {
                        $('#modalEditarPartido').modal('hide');
                        location.reload();
                    } else {
                        alert("Hubo un error enviando el formulario. Si el problema persiste, contacte a soporte.");
                    }
                },
                error: function (data) {
                    alert("Ha ocurrido un error: " + JSON.stringify(data));
                }
            });
            return false;
        }
    });

    $.validator.addMethod("valorDiferenteA", function (value, element, arg) {
        return arg !== value;
    }, "Value must not equal arg.");

    $.validator.addMethod("valorDiferente", function (value, element, arg) {
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

