// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.getElementById('link-agregar').onclick = function () {
    
    talle = document.getElementById("talle-zapa").value;

    if(talle == "ingrese-talle") {
        alert("Ingrese un talle");
    } else {
        var codProducto = document.getElementById("cod-producto").value;
        window.location.href = "/Carrito/Add/" + codProducto + "-" + talle;

        console.log("PROD: "+codProducto + "- TALLE:  " + talle)
    }
        
};
