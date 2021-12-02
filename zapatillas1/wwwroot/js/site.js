$('#talle-zapa').on('change', function() {

    let talle = document.getElementById("talle-zapa").value;
    let idProducto = document.getElementById('id-producto').value;
    let codProducto = document.getElementById("cod-producto").value;


    $.ajax({
        type: "GET",
        url: '/Productos/Cantidad',
        contentType: "application/json; charset=utf-8",
        data: {
            id: idProducto,
            talle: talle,
            codp: codProducto,

        },

        dataType: "json",
        success: function(resp) {
            console.log(resp);
            var idProducto = resp[0];
            var codProducto = resp[1];
            var cantidadStock = resp[2];

            document.getElementById('id-producto').value = idProducto;
            // document.getElementById('cantidad-total-stock-item').innerHTML = cantidadStock;

            let stringSelect1 = '<select name="cant-items" id="cant-items">';
            let stringSelect2 = '';
            let stringSelect3 = '</select>';


            for (i = 1; i <= cantidadStock; i++) {
                stringSelect2 += '<option value=' + i + '>' + i + '</option>'
            }

            let stringFinal = stringSelect1 + stringSelect2 + stringSelect3;

            document.getElementById('cant-zapadiv').innerHTML = stringFinal;
        },

        error: console.log("error")
    });

    console.log('id desde html: ' + idProducto);

    document.getElementById('link-agregar').onclick = function() {

        let idProducto = document.getElementById('id-producto').value;
        let cantidadItemsCompra = document.getElementById('cant-items').value;

        // select para la cantidad = cant-zapadiv
        // id-producto .value tiene el id

        $.ajax({
            type: "GET",
            url: '/Carrito/Add',
            contentType: "application/json; charset=utf-8",
            data: {
                id: idProducto,
                cantidad: cantidadItemsCompra,
            },
            dataType: "json",
            success: function(resp) {
                console.log(resp);
                window.location.href = "/Carrito/Ver/";
            },

            error: console.log("error")
        });


    };


});


// buscar historial modelo talle

document.getElementById('buscar-historial-modelo').onclick = function() {

    let idProducto = document.getElementById('select-producto-id').value;
    console.log(idProducto);
    if (idProducto != "-1") {
        window.location.href = "/Ventas/Zapatilla?id=" + idProducto;
    } else {
        alert("Elegi un modelo");
    }

};