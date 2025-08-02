function CommaFormatted(event, idInput) {
    if (event.which >= 37 && event.which <= 40) return;
    valor = $(idInput).val();
    if (valor) {
        formated = valor.replace(/\D/g, "").replace(/([0-9])([0-9]{3})$/, '$1.$2')
        $(idInput).val(formated);
    }
}
function numberCheck(args) {
    if (args.key === 'e' || args.key === '+' || args.key === '-') {
        return false;
    } else {
        return true;
    }
}
function soloNumerosLetras(evt) {

    let code = (evt.which) ? evt.which : evt.keyCode;
    if ((code == 8)) {
        //backspace
        return true;
    } else if ((code >= 48 && code <= 57) || (code >= 65 && code <= 90) || (code >= 97 && code <= 122)) {
        //is a number

        return true;
    } else {
        return false;
    }
}
function encriptar(json) {
    var enc1 = btoa(JSON.stringify(json));
    var key = CryptoJS.enc.Utf8.parse('863F952818EBD9BF');
    var iv = CryptoJS.enc.Utf8.parse('3721A49117CC7D62');
    var encryptedlogin = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(enc1), key,
        { keySize: 128 / 8, iv: iv, mode: CryptoJS.mode.CBC, padding: CryptoJS.pad.Pkcs7 });
    var enc = encryptedlogin;
    return enc;
}
function validaCorreo(mail) {

    var regex = /[\w-\.]{2,}@([\w-]{2,}\.)*([\w-]{2,}\.)[\w-]{2,4}/;
    if (regex.test(mail.trim())) {
        return true;

    } else {
        return false;
    }
}
function soloNumeros(evt) {
    let code = (evt.which) ? evt.which : evt.keyCode;
    if (code == 8) {
        //backspace
        return true;
    } else if (code >= 48 && code <= 57) {
        //is a number
        return true;
    } else {
        return false;
    }
}
function soloLetras(evt) {
    let code = (evt.which) ? evt.which : evt.keyCode;
    if ((code == 8 || code == 32)) {
        //backspace
        return true;
    } else if ((code >= 65 && code <= 90) || (code >= 97 && code <= 122)) {
        //is a number
        return true;
    } else {
        return false;
    }
}
function VerificaRut() {



    var rut = $('#rut').val();
    console.log('rut= ' + rut)
    //debugger;

    if (rut == "") {
        $('#pMostrarErrorRut').show();
        $('#pMostrarErrorRut').text('Debe ingresar RUT Correctamente');
        return;
    } else {

        if (rut.length >= '8') {

            $('#pMostrarErrorRut').text('');
            var rutValidador = new RutValidador(rut)
            if (rutValidador.esValido) {
                $('#pMostrarErrorRut').hide();
                return;
            }
            else {
                $('#pMostrarErrorRut').show();
                $('#pMostrarErrorRut').text('Rut Invalido!');
                return;
            }
        }
        else {
            $('#pMostrarErrorRut').show();
            $('#pMostrarErrorRut').text('Rut Invalido!');
            return;
        }
    }




}

function VerificaRut2() {
    var rut = $('#rut').val();
    console.log('rut= ' + rut)

    if (rut.length >= '8') {
        $('#resultado').text('');
        var rutValidador = new RutValidador(rut)
        if (rutValidador.esValido) {

            $('#resultadoError').text('');
            $('#resultadoMin').text('');
            $('#resultadoOK').text('Rut valido!');

            return;
        }
        else {
            $('#resultadoOK').text('');
            $('#resultadoMin').text('');
            $('#resultadoError').text('Rut Invalido!');
            return;
        }
    }
    else {
        $('#resultadoOK').text('');
        $('#resultadoError').text('Debe ingresar un rut');
        $('#resultadoMin').text('');
        return;
    }
}



class RutValidador {
    constructor(rut) {

        this.rut = rut;
        //obtenemos ultimo caracter del rut ingresado
        this.dv = this.rut.substring(this.rut.length - 1);
        //limpiar rut dejando solo numeros
        this.rut = this.rut.substring(0, this.rut.length - 1).replace(/\D/g, '');
        this.esValido = this.validarRut();
    }
    validarRut() {

        let numerosArray = this.rut.split('').reverse()
        let acumulador = 0;
        let multiplicador = 2;
        for (let numero of numerosArray) {
            acumulador += parseInt(numero) * multiplicador;
            multiplicador++;
            //reset vueeeltaaaa
            if (multiplicador == 8) {
                multiplicador = 2;
            }
        }
        let dv = 11 - (acumulador % 11);

        if (dv == 11) {
            dv = '0'
        }
        if (dv == 10) {
            dv = 'k'
        }
        return dv == this.dv.toLowerCase();
    }

    formateado() {
        if (!this.esValido) {
            return '';
        }
        else {
            return this.rut.toString().replace(/\B(?=(\d{3})+(?!\d))/g, '.') + '-' + this.dv;
        }
    }
}

function validaCorreo(mail) {

    var regex = /[\w-\.]{2,}@([\w-]{2,}\.)*([\w-]{2,}\.)[\w-]{2,4}/;
    if (regex.test(mail.trim())) {
        return true;

    } else {
        return false;
    }
}

function soloLetras2(val) {

    valor = event.target.value;

    if (!/^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]+$/g.test(valor)) {

        valorSub = valor.substring(0, valor.length - 1);
        $("#" + event.target.id).val(valorSub);
    }

}
