function simpleAlert(titulo, mensaje, tipo) {
    debugger;
    Swal.fire({
        icon: tipo,
        title: titulo,
        text: mensaje,
        confirmButtonText: 'Aceptar',
        confirmButtonColor: '#e60012',
    })
}
