// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.getElementById('link-agregar').onclick = function () {
    
    let talle = document.getElementById("talle-zapa").value;

    if(talle == "ingrese-talle") {
        alert("Ingrese un talle");
    } else {
        var codProducto = document.getElementById("cod-producto").value;
        window.location.href = "/Carrito/Add/" + codProducto + "-" + talle;

        console.log("PROD: "+codProducto + "- TALLE:  " + talle)
    }
        
};


$('#talle-zapa').on('change', function() {
    
    let idProducto  = document.getElementById('id-producto').value;
    
    // ----- Falta todo ajax que busque la info y la traiga
    // stock del talle va a venir de ajax !! 
    // total va a venir de ajax y al front end 
    // request ajax and update cantidad-single-item 
    // can cada onChange se va a buscar el id talle y la cantidad en stock de ese id
    // esa cantidad esta en el objeto Producto
    
    let totalStockTalle = 9;
    document.getElementById('cantidad-total-stock-item').innerHTML = totalStockTalle;
    
    console.log( 'talle: ' + this.value );
    console.log( 'id: ' +  idProducto );
    
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