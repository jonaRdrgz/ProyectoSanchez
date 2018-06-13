$().ready(function () {

    var funcion = 1;
    $("#fecha").on("focusin", function () {
        $(this).datetimepicker({
            format: 'DD/MM/YYYY HH:mm'
        });
    });

    $("#botonGoles").click(function () {


        $("#fecha").rules("add", {
            required: true,
            messages: {
                required: "La fecha es obligatoria",
            }
        });

        $("#idEquipoA").rules("add", {
            valorDiferente: "none",
            valorDiferenteA: $("#idEquipoB").val(),
            messages: {
                valorDiferente: "Seleccione un equipo ",
                valorDiferenteA: "Seleccione un equipo diferente",
            }
        });

        $("#idEquipoB").rules("add", {
            valorDiferente: "none",
            valorDiferenteA: $("#idEquipoA").val(),
            messages: {
                valorDiferente: "Seleccione un equipo",
                valorDiferenteA: "Seleccione un equipo diferente",
            }
        });
        funcion = 2;
        $("#formPartidos").valid();
    });

    $("#botonPartidos").click(function () {


        $("#fecha").rules("add", {
            required: true,
            messages: {
                required: "La fecha es obligatoria",
            }
        });

        $("#idEquipoA").rules("add", {
            valorDiferente: "none",
            valorDiferenteA: $("#idEquipoB").val(),
            messages: {
                valorDiferente: "Seleccione un equipo ",
                valorDiferenteA: "Seleccione un equipo diferente",
            }
        });

        $("#idEquipoB").rules("add", {
            valorDiferente: "none",
            valorDiferenteA: $("#idEquipoA").val(),
            messages: {
                valorDiferente: "Seleccione un equipo",
                valorDiferenteA: "Seleccione un equipo diferente",
            }
        });
        funcion = 1;
        $("#formPartidos").valid();
    });

    $("#formPartidos").validate({
        submitHandler: function (form) {
            if (funcion === 1) {
                window.location.replace("/PartidoEquipos/Index?" + "IdEquipoA=" + $("#idEquipoA").val() + "&IdEquipoB=" + $("#idEquipoB").val() + "&Fecha=" + $("#fecha").val());
            }
            else {
                window.location.replace("/GolesPorPartidoPorFecha/Index?" + "IdEquipoA=" + $("#idEquipoA").val() + "&IdEquipoB=" + $("#idEquipoB").val() + "&Fecha=" + $("#fecha").val());
            }
            return false;
        }
    });

    $.validator.addMethod("valorDiferenteA", function (value, element, arg) {
        return arg !== value;
    }, "Value must not equal arg.");


    $.validator.addMethod("valorDiferente", function (value, element, arg) {
        return arg !== value;
    }, "Value must not equal arg.");
});