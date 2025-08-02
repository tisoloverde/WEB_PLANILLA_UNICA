var archivo = document.getElementById('customFile1');

archivo.addEventListener('change', function (e) {
    var file = e.target.files[0];
    var files = e.target.files || e.dataTransfer.files;
    if (!files.length) {
        return;
    }
    createImage(files[0]);
});

// Esta funcion practicamente se explica sola.
function createImage(file) {
    var exten = file.type;
    var peso = file.size;
    // Si quieres agregar una extención más para validar solo agregala.
    if (exten == 'image/jpeg' || exten == 'image/png' || exten == 'image/svg') {
        // Si quieres cambiar el peso máximo para la imagen solo edita los números.
        if (peso > 2000000) {
            colorInputImagen();
            siPesoExede();
        } else {
            siTodoBien(file.name)
        }
    } else {
        colorInputImagen()
        siFormatoError();
    }
};

// Funciones que se utilizaran para los colores y los mensajes.
function colorInputImagen() {
    document.getElementById('nameFile1').style.cssText = "border: solid 1px red;"
    setTimeout(() => {
        document.getElementById('nameFile1').style.cssText = "border: solid 1px #DFDFDF;"
    }, 3000)
};

function siFormatoError() {
    document.getElementById('msjFile1').style.cssText = "color: red; display: block";
    document.getElementById('msjFilePeso1').style.cssText = "display: none";
    document.getElementById('nameFile1').innerHTML = '';
    document.getElementById('customFile1').value = '';
};

function siPesoExede() {
    document.getElementById('customFile1').value = '';
    document.getElementById('nameFile1').innerHTML = '';
    document.getElementById('msjFile1').style.cssText = "display: none";
    document.getElementById('msjFilePeso1').style.cssText = "display: block; color: red";
};

function siTodoBien(nombre) {
    document.getElementById('nameFile1').innerHTML = nombre;
    document.getElementById('msjFile1').style.cssText = "display: none";
    document.getElementById('msjFilePeso1').style.cssText = "display: none";
}



/* SIN INPUT */
$(function () {
    var countFiles = 1,
        $body = $('body'),
        typeFileArea = ['txt', 'doc', 'docx', 'ods'],
        coutnTypeFiles = typeFileArea.length;

    //create new element
    $body.on('click', '.files-wr label', function () {
        var wrapFiles = $('.files-wr'),
            newFileInput;

        countFiles = wrapFiles.data('count-files') + 1;
        wrapFiles.data('count-files', countFiles);

        newFileInput = '<div class="one-file"><label for="file-' + countFiles + '">Seleccionar archivo(s)</label>' +
            '<input type="file" name="file-' + countFiles + '" id="file-' + countFiles + '"><div class="file-item hide-btn">' +
            '<span class="file-name"></span><span class="btn btn-del-file">x</span></div></div>';
        wrapFiles.prepend(newFileInput);
    });

    //show text file and check type file
    $body.on('change', 'input[type="file"]', function () {
        var $this = $(this),
            valText = $this.val(),
            fileName = valText.split(/(\\|\/)/g).pop(),
            fileItem = $this.siblings('.file-item'),
            beginSlice = fileName.lastIndexOf('.') + 1,
            typeFile = fileName.slice(beginSlice);

        fileItem.find('.file-name').text(fileName);
        if (valText != '') {
            fileItem.removeClass('hide-btn');

            for (var i = 0; i < coutnTypeFiles; i++) {

                if (typeFile == typeFileArea[i]) {
                    $this.parent().addClass('has-mach');
                }
            }
        } else {
            fileItem.addClass('hide-btn');
        }

        if (!$this.parent().hasClass('has-mach')) {
            $this.parent().addClass('error');
        }
    });

    //remove file
    $body.on('click', '.btn-del-file', function () {
        var elem = $(this).closest('.one-file');
        elem.fadeOut(400);
        setTimeout(function () {
            elem.remove();
        }, 400);
    });
});