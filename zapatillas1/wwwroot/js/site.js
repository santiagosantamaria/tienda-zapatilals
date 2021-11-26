

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

            let stringSelect1 = '<select name="talle-zapa" id="talle - zapa">';
            let stringSelect2 = '';
            let stringSelect3 = '</select>';
            

            for (i = 1; i <= resp; i++) {
                stringSelect2 += '<option value=' + i + '>' + i + '</option>'
            }

            let stringFinal = stringSelect1 + stringSelect2 + stringSelect3;

            document.getElementById('cant-zapadiv').innerHTML = stringFinal;
        },

        error: console.log("error")
    });
    
    // let totalStockTalle = 9;
    // document.getElementById('cantidad-total-stock-item').innerHTML = totalStockTalle;
    
    // console.log( 'talle: ' + this.value );
    console.log( 'id desde html: ' +  idProducto );
    

    
    document.getElementById('link-agregar').onclick = function () {

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
            success: function (resp) {
                console.log(resp);
                document.getElementById('cantidad-total-stock-item').innerHTML = resp;

               


               
            },

            error: console.log("error")
        });


    };

   




  });