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
                            <td class="last"> <div class="col-md-2 col-sm-2 col-xs-2">' + partido["GolLocal"]  + '</div> <button id="buttonPlus" \
                                        onclick="asignarGol('+ partido["IdPartido"] + ", " + partido["IdEquipoLocal"] + ", " + partido["GolLocal"] +');" class="btn-link col-md-4 col-sm-4 col-xs-4"><i class="fa fa-sign-out">\
                                 </i></button></td>\
                            <td class="last"> <div class="col-md-2 col-sm-2 col-xs-2">' + partido["GolVisita"] + '</div> <button id="buttonPlus" \
                                        onclick="asignarGol('+ partido["IdPartido"] + ", " + partido["IdEquipoVisita"] + ", " + partido["GolVisita"] + ');" class="btn-link col-md-4 col-sm-4 col-xs-4"><i class="fa fa-sign-out">\
                                 </i></button></td>\
                            <td class=" last">\
                                <button  id="buttonPlus" \
                                onclick="getInformacionPartido('+ partido["IdPartido"] + ", " + partido["IdEquipoLocal"] + ", " + partido["IdEquipoVisita"] +
                                           "," + partido["GolLocal"] + "," + partido["GolVisita"] + "," + partido["IdTorneo"] + ');" class="btn-link col-md-4 col-sm-4 col-xs-4"><i class="fa fa-edit">\
                                 </i></button>\
                                 <button  id="buttonMinus" \
                                onclick="deletePartido('+ partido["IdPartido"] + ');" class="btn-link col-md-4 col-sm-4 col-xs-4"><i class="fa fa-minus-circle" style="color:#800000;">\
                                 </i></button>\
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


function asignarGol(idPartido, idEquipo, goles) {
    $("#idElementosGol").attr("idPartido", idPartido);
    $("#idElementosGol").attr("idEquipo", idEquipo);
    var data = {
        idPartido: idPartido,
        idEquipo: idEquipo,
        goles: goles
    }
    $.ajax({
        type: "post",
        url: "/Partido/VerificarGolRegistrado",
        data: JSON.stringify(data),
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            if (data === false) {         // Si no esta guardado, se procede a guardar
                $("#elementosDinamicos").html("");
                for (gol = 1; gol <= goles; gol++) {
                    var jugador = '<h4 class="headerGol"> Gol ' + gol + '</h4><div class="item form-group">\
                    <label class="control-label col-md-3 col-sm-3 col-xs-12" for= "jugador '+ gol +'">\
                        Jugador que anotó:\
                        <span class="required">*</span>\
                    </label>\
                    <div class="col-md-6 col-sm-6 col-xs-12">\
                        <select class="form-control jugadadorSelect" id="jugador'+ gol +'" name="jugador' + gol +'">\
                            <option value="none">Seleccione una Opción</option>\
                        </select>\
                    </div>\
                 </div >';
                    var minuto = '<div class="item form-group">\
                        <label class="control-label col-md-3 col-sm-3 col-xs-12" for="minuto'+gol +'">Minuto:</label>\
                        <div class="col-md-6 col-sm-6 col-xs-12">\
                            <input type="number" class="form-control col-md-7 col-xs-12 minutoInput" id="minuto'+ gol +'" min="1" step="1" name="minuto" value="1"/>\
                        </div>\
                    </div>';
                    $("#elementosDinamicos").append('<div id=golAdicional' + gol + '>' + jugador + '</div>');
                    $("#elementosDinamicos").append('<div id=golAdicional' + gol + '>' + minuto + '</div>');
                }
                $(".guardar").removeClass("editar");
                $("#.guardar").addClass("guardar");
                $('#modalGol').modal();
            } else {
                $("#elementosDinamicos").html("");
                for (gol = 1; gol <= goles; gol++)
                {
                    var jugador = '<div class="item form-group">\
                    <label class="control-label col-md-3 col-sm-3 col-xs-12" for= "jugador">\
                        Jugador que anotó:\
                        <span class="required">*</span>\
                    </label>\
                    <div class="col-md-6 col-sm-6 col-xs-12">\
                        <select class="form-control jugadadorSelect" id="jugador" name="jugador">\
                            <option value="none">Seleccione una Opción</option>\
                        </select>\
                    </div>\
                 </div >';
                    var minuto = '<div class="item form-group">\
                        <label class="control-label col-md-3 col-sm-3 col-xs-12" for="minuto">Minuto:</label>\
                        <div class="col-md-6 col-sm-6 col-xs-12">\
                            <input type="number" class="form-control col-md-7 col-xs-12 minutoInput" id="minuto" min="1" step="1" name="minuto" value="1"/>\
                        </div>\
                    </div>';
                    $("#elementosDinamicos").append('<div id=golAdicional' + gol + '>' + jugador + '</div>');
                }
            }
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
    getEquiposXTorneo($("#local"), idEquipoLocal);
    getEquiposXTorneo($("#visita"), idEquipovisita);
    $("#modalEditarPartido").modal();
}

function deletePartido(idPartido) {
    var respuesta = confirm("¿Seguro que desea eliminar el partido?");
    if (respuesta == false) {
        return false;
    }
    var data = {
        idPartido: idPartido
    }
    $.ajax({
        type: "post",
        url: "/Partido/DeletePartido",
        data: JSON.stringify(data),
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            var code = data["CODE"]
            if (code === "PARTIDO_ELIMINADO") {
                GetPartidos();
            } else {
                alert("Hubo un error enviando el form. Si el problema persiste, contacte a soporte.");
            }

        },
        error: function (data) {
            alert("Ha ocurrido un error: " + JSON.stringify(data));
        }
    });
}

function getEquiposXTorneo(tag, idEquipo) {
    tag.html("<option value='none'>Seleccione una Opción</option>");
    $.ajax({
        type: "post",
        url: "/Partido/GetEquiposXTorneo",
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
    getEquiposXTorneo($("#equipolocal"), -1);
    getEquiposXTorneo($("#equipovisita"), -1);
    $("#modalAgregarPartido").modal();
    
}


$().ready(function () {

    GetPartidos();

    //----------------------------- Validación para editar partido --------------
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
                        GetPartidos();
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

    //----------------------------- Validación para agregar partido --------------
    $("#botonAgregar").click(function () {


        $("#equipolocal").rules("add", {
            valorDiferenteA: "none",
            valorDiferente: $("#equipovisita").val(),
            messages: {
                valorDiferenteA: "Seleccione un equipo local",
                valorDiferente: "Seleccione un equipo diferente",
            }
        });

        $("#equipovisita").rules("add", {
            valorDiferenteA: "none",
            valorDiferente: $("#equipolocal").val(),
            messages: {
                valorDiferenteA: "Seleccione un equipo visitante",
                valorDiferente: "Seleccione un equipo diferente",
            }
        });

        $('#agregarGolCasa').rules("add", {
            required: true,
            number: true,
            messages: {
                required: "La cantidad es obligatoria",
                number: "Ingrese solamente números"
            }
        });

        $('#agregarGolVisita').rules("add", {
            required: true,
            number: true,
            messages: {
                required: "La cantidad es obligatoria",
                number: "Ingrese solamente números"
            }
        });
        $("#jugado").rules("add", {
            valorDiferente: "none",
            messages: {
                valorDiferente: "Seleccione una opción",
            }
        });

        $("#formAgregarPartido").valid();
    });

    $("#formAgregarPartido").validate({
        submitHandler: function (form) {

            var partido = {
                IdPartido: 0, //ID para que este valido en el dataAnnotation
                IdEquipoLocal: $("#equipolocal").val(),
                IdEquipoVisita: $("#equipovisita").val(),
                GolLocal: $("#agregarGolCasa").val(),
                GolVisita: $("#agregarGolVisita").val(),
                Jugado: $("#jugado").val()
            }
            console.log(JSON.stringify(partido));

            $.ajax({
                type: "post",
                url: "/Partido/AgregarPartido",
                data: JSON.stringify(partido),
                dataType: "json",
                contentType: "application/json",
                success: function (data) {
                    var code = data["CODE"]
                    if (code === "PARTIDO_GUARDADO") {
                        $('#modalAgregarPartido').modal('hide');
                        GetPartidos();
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

    //---------------------------------- Validacion para agregar un gol ---------------
    $("#botonGuardarGol").click(function () {
        $(".jugadadorSelect").each(function () {

            $(this).rules("add", {
                valorDiferenteA: "none",
                messages: {
                    valorDiferenteA: "Seleccione un jugador"
                }
            });
        });
        $(".minutoInput").each(function () {

            $(this).rules("add", {
                required: true,
                number: true,
                messages: {
                    required: "La cantidad es obligatoria",
                    number: "Ingrese solamente números"
                }
            });
        });

        $("#formGol").valid();
    });

    $("#formGol").validate({
        submitHandler: function (form) {

            //var partido = {
            //    IdPartido: 0, //ID para que este valido en el dataAnnotation
            //    IdEquipoLocal: $("#equipolocal").val(),
            //    IdEquipoVisita: $("#equipovisita").val(),
            //    GolLocal: $("#agregarGolCasa").val(),
            //    GolVisita: $("#agregarGolVisita").val(),
            //    Jugado: $("#jugado").val()
            //}
            //console.log(JSON.stringify(partido));

            //$.ajax({
            //    type: "post",
            //    url: "/Partido/AgregarPartido",
            //    data: JSON.stringify(partido),
            //    dataType: "json",
            //    contentType: "application/json",
            //    success: function (data) {
            //        var code = data["CODE"]
            //        if (code === "PARTIDO_GUARDADO") {
            //            $('#modalAgregarPartido').modal('hide');
            //            GetPartidos();
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

    $.validator.addMethod("valorDiferenteB", function (value, element, arg) {
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

