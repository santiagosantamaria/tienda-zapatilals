

document.getElementById('link-agregar').onclick = function () {
    
    let talle = document.getElementById("talle-zapa").value;

    if(talle == "ingrese-talle") {
        alert("Ingrese un talle");
    } else {
        let codProducto = document.getElementById("cod-producto").value;
        window.location.href = "/Carrito/Add/" + codProducto + "-" + talle;
    }
        
};


$('#talle-zapa').on('change', function() {
    
    let talle = document.getElementById("talle-zapa").value;
    let idProducto  = document.getElementById('id-producto').value;
    let codProducto = document.getElementById("cod-producto").value;
    
    
    $.ajax({
        type: "GET",
        url: '/Productos/Cantidad',
        contentType: "application/json; charset=utf-8",
        data: { 
            id: idProducto ,
            talle: talle ,
            codp: codProducto ,
        
        },
        dataType: "json",
        success: function(resp){ 
            console.log(resp);
            document.getElementById('cantidad-total-stock-item').innerHTML = resp;

        },

        error: console.log("error")
    });
    
    // let totalStockTalle = 9;
    // document.getElementById('cantidad-total-stock-item').innerHTML = totalStockTalle;
    
    // console.log( 'talle: ' + this.value );
    console.log( 'id desde html: ' +  idProducto );
    
    document.getElementById('cantidad-compra-item').innerHTML = 0;
    
    $('#cantidad-sumar').on('click', function() {
    
        let cant = parseInt(document.getElementById('cantidad-compra-item').innerHTML);
        
        if(cant < totalStockTalle) {
            cant ++;
        }
        console.log(cant);
        document.getElementById('cantidad-compra-item').innerHTML = cant;
        
    
    });
    
    $('#cantidad-restar').on('click', function() {
    
        let cant = parseInt(document.getElementById('cantidad-compra-item').innerHTML);
        if(cant > 0) {
            cant --;
        }
        console.log(cant);
        document.getElementById('cantidad-compra-item').innerHTML = cant;
    
    });
    
  });