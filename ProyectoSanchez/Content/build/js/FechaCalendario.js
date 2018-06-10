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
                                <button  id="buttonView" \
                                onclick="rediredToPartido('+ fecha["IdFecha"] + "," + fecha["IdTorneo"] + ');" class="btn-link col-md-2 col-sm-2 col-xs-2"><i class="fa fa-eye">\
                                 </i></button>\
                                <button  id="buttonEdit" \
                                onclick="editFecha('+ fecha["IdFecha"] + ');" class="btn-link col-md-2 col-sm-2 col-xs-2"><i class="fa fa-edit">\
                                 </i></button>\
                                <button  id="buttonDelete" \
                                onclick="deleteFecha('+ fecha["IdFecha"] + ');" class="btn-link col-md-2 col-sm-2 col-xs-2"><i class="fa fa-minus-circle" style="color:#800000;">\
                                 </i></button>\
                            </td>\
                        </tr>';
            });
            // <a onclick="rediredToPartido('+ fecha["IdFecha"] + "," + fecha["IdTorneo"] + ')"  class="">+Info</a>

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
    // Se revisa si no ha seleccionado un torneo para desabilitar botón.
    if ($('#idTorneo').val() === '-1') {
        $('#botonAgregar').prop('disabled', true);
        $('#botonBorrarTorneo').prop('disabled', true);
    }
    else {
        $('#botonAgregar').prop('disabled', false);
        $('#botonBorrarTorneo').prop('disabled', false);
    }
}
function getListaTorneos() {
    $("#idTorneo").html("<option value='-1'>Seleccione una Opción</option>")
    $.ajax({
        type: "post",
        url: "/Home/GetListaTorneos",
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            var htmlSelect = "";

            $.each(data, function (i, torneo) {
                htmlSelect += '<option value="' + torneo["IdTorneo"] + '" >' + torneo["Nombre"] + '</option>';
            });
            $("#idTorneo").append(htmlSelect);
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
function borrrarTorneo() {
    var respuesta = confirm("¿Seguro que desea eliminar el torneo?");
    if (respuesta == false) {
        return false;
    }
    var data = {
        idTorneo: $("#idTorneo").val()
    }
    console.log(JSON.stringify(data));
    $.ajax({
        type: "post",
        url: "/Home/DeleteTorneo",
        data: JSON.stringify(data),
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            var code = data["CODE"]
            if (code === "TORNEO_ELIMINADO") {
                getListaTorneos();
            } else {
                alert("Hubo un error enviando el form. Si el problema persiste, contacte a soporte.");
            }

        },
        error: function (data) {
            alert("Ha ocurrido un error: " + JSON.stringify(data));
        }
    });
}

function deleteFecha(idFecha)
{
    var respuesta = confirm("¿Seguro que desea eliminar la fecha del torneo?");
    if (respuesta == false) {
        return false;
    }
    var data = {
        idFechaCalendario: idFecha
    }
    $.ajax({
        type: "post",
        url: "/Home/DeleteFechaCalendario",
        data: JSON.stringify(data),
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            var code = data["CODE"]
            if (code === "FECHA_CALENDARIO_ELIMINADA") {
                getFechasCalendarioXTorneo();
            } else {
                alert("Hubo un error enviando el form. Si el problema persiste, contacte a soporte.");
            }

        },
        error: function (data) {
            alert("Ha ocurrido un error: " + JSON.stringify(data));
        }
    });

}

function editFecha(idFecha) {
    $("#idElementos").attr("idFechaCalendario", idFecha);
    $("#modalEditarFechaCalendario").modal();
}

function agregarFecha() {
    $("#modalAgregarFechaCalendario").modal();
}

$().ready(function () {

    $("#fecha").on("focusin", function () {
        $(this).datetimepicker({
            format: 'DD/MM/YYYY HH:mm'
        });
    });

    $("#editarFecha").on("focusin", function () {
        $(this).datetimepicker({
            format: 'DD/MM/YYYY HH:mm'
        });
    });
    $("#botonGuardar").click(function () {


        $("#fecha").rules("add", {
            required: true,
            messages: {
                required: "La fecha es obligatoria",
            }
        });

        $("#formFechaCalendario").valid();
    });

    $("#botonEditar").click(function () {


        $("#editarFecha").rules("add", {
            required: true,
            messages: {
                required: "La fecha es obligatoria",
            }
        });

        $("#formEditarFechaCalendario").valid();
    });

    $("#formFechaCalendario").validate({
        submitHandler: function (form) {

            var fecha = {
                FechaProgramada: $("#fecha").val(),
                IdTorneo: $("#idTorneo").val()
            }
            console.log(JSON.stringify(fecha));

            $.ajax({
                type: "post",
                url: "/Home/AgregarFechaCalendario",
                data: JSON.stringify(fecha),
                dataType: "json",
                contentType: "application/json",
                success: function (data) {
                    var code = data["CODE"]
                    if (code === "FECHA_GUARDADA") {
                        $('#modalAgregarFechaCalendario').modal('hide');
                       getFechasCalendarioXTorneo();
                    } else {
                        alert("Hubo un error enviando el form. Si el problema persiste, contacte a soporte.");
                    }
                },
                error: function (data) {
                    alert("Ha ocurrido un error: " + JSON.stringify(data));
                }
            });
            return false;
        }
    });
    $("#formEditarFechaCalendario").validate({
        submitHandler: function (form) {

            var fecha = {
                IdFecha: $("#idElementos").attr('idFechaCalendario'),
                FechaProgramada: $("#editarFecha").val(),
                IdTorneo: $("#idTorneo").val()
            }
            console.log(JSON.stringify(fecha));

            $.ajax({
                type: "post",
                url: "/Home/ActualizarFechaCalendario",
                data: JSON.stringify(fecha),
                dataType: "json",
                contentType: "application/json",
                success: function (data) {
                    var code = data["CODE"]
                    if (code === "FECHA_ACTUALIZADA") {
                        $('#modalEditarFechaCalendario').modal('hide');
                        getFechasCalendarioXTorneo();
                    } else {
                        alert("Hubo un error enviando el form. Si el problema persiste, contacte a soporte.");
                    }
                },
                error: function (data) {
                    alert("Ha ocurrido un error: " + JSON.stringify(data));
                }
            });
            return false;
        }
    });
});