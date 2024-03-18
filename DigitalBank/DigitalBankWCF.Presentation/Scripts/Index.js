
function recargarPagina() {
    window.location.reload();
}

function mostrarDetallesUsuario(idUsuario, nombre, fechaNacimiento, idGenero, idEstado) {
    var nombreGenero = obtenerNombre(idGenero, 'genero');
    var nombreEstado = obtenerNombre(idEstado, 'estado'); 

    var detallesUsuarioHTML = `
        <input type="hidden" id="idUsuario" value="${idUsuario}">
        <div class="form-group">
            <label for="nombre">Nombre:</label>
            <input type="text" class="form-control" id="Nombre" value="${nombre}">
        </div>
        <div class="form-group">
            <label for="FechaNacimiento">Fecha de Nacimiento:</label>
            <input type="date" class="form-control" id="FechaNacimiento" value="${fechaNacimiento}">
        </div>
        <div class="form-group">
            <label for="IdGenero">Sexo:</label>
            <select class="form-control" id="IdGenero">
                <option value="${idGenero}">${nombreGenero}</option>
                <!-- Aquí puedes agregar las opciones para los géneros -->
                <option value="1">Femenino</option>
                <option value="2">Masculino</option>
                <option value="3">Trans</option>
                <option value="4">No binario</option>
                <option value="5">No aplica</option>
                <!-- Agrega las opciones restantes según tu lógica -->
            </select>
        </div>
        <div class="form-group">
            <label for="idEstado">Estado:</label>
            <select class="form-control" id="idEstado">
                <option value="${idEstado}">${nombreEstado}</option>
                <!-- Aquí puedes agregar las opciones para los estados -->
                <option value="1">Activo</option>
                <option value="2">Inactivo</option>
                <!-- Agrega las opciones restantes según tu lógica -->
            </select>
        </div>
    `;
    document.getElementById("detallesUsuario").innerHTML = detallesUsuarioHTML;

    $('#editarUsuarioModal').modal('show');
}

function obtenerNombre(id, tipo) {
    switch (tipo) {
        case 'genero':
            switch (id) {
                case 1:
                    return 'Femenino';
                case 2:
                    return 'Masculino';
                case 3:
                    return 'Trans';
                case 4:
                    return 'No binario';
                case 5:
                    return 'No aplica';
                default:
                    return '';
            }
        case 'estado':
            switch (id) {
                case 1:
                    return 'Activo';
                case 2:
                    return 'Inactivo';
                case 3:
                    return 'Eliminado';
                default:
                    return '';
            }
        default:
            return '';
    }
}

function guardarCambiosUsuario() {
    console.log("priuebas")
    var idUsuario = $('#idUsuario').val();
    var nombre = $('#Nombre').val();
    var fechaNacimiento = document.getElementById('FechaNacimiento').value; 
    var idGenero = $('#IdGenero').val();
    var idEstado = $('#idEstado').val();
    console.log(fechaNacimiento);
    var usuario = {
        IdUsuario: idUsuario,
        Nombre: nombre,
        FechaNacimiento: fechaNacimiento,
        IdGenero: idGenero,
        IdEstado: idEstado
    };
   
    $.ajax({
        url: actualizarUsuarioUrl,
        type: 'POST',
        data: JSON.stringify({ usuario: usuario }),
        contentType: 'application/json',
        success: function (response) {           
            if (response.success) {
                mensajeUpdate('success', 'La actualizacion del usuario fue exitosa');
            } else {
                mensajeUpdate('error', 'La actualizacion del usuario fallo');
            }
        },
        error: function (xhr, status, error) {
            mostrarMensaje('error', 'Error al enviar la solicitud: ' + error);
        }
    });
}

function mensajeUpdate(tipo, mensaje) {
    console.log('entro')
    var mensajeElemento = $('#mensajeActualizacion');
    mensajeElemento.text(mensaje);
    if (tipo === 'success') {
        mensajeElemento.removeClass('alert-danger').addClass('alert-success');
    } else {
        mensajeElemento.removeClass('alert-success').addClass('alert-danger');
    }
    mensajeElemento.show();
}

function MensajeAgregado(tipo, mensaje) {
    console.log('entro')
    var mensajeElemento = $('#mensajeAgregado');
    mensajeElemento.text(mensaje);
    if (tipo === 'success') {
        mensajeElemento.removeClass('alert-danger').addClass('alert-success');
    } else {
        mensajeElemento.removeClass('alert-success').addClass('alert-danger');
    }
    mensajeElemento.show();
}



function eliminarUsuario(idUsuario, Nombre) {
    // Establecer el ID del usuario en el elemento HTML correspondiente dentro del modal
    $('#idUsuarioEliminar').text(idUsuario);
    $('#nombreEliminar').text(Nombre);

    // Mostrar el modal de confirmación
    $('#confirmacionEliminarModal').modal('show');

    // Vincular la función para confirmar la eliminación del usuario
    $('#confirmarEliminarBtn').click(function () {
        $.ajax({
            url: eliminarUsuarioUrl,
            type: 'POST',
            data: { idUsuario: idUsuario },
            success: function (response) {
                if (response.success) {
                    mostrarMensaje('success', 'El usuario ha sido eliminado exitosamente.');
                } else {
                    mostrarMensaje('danger', 'Hubo un error al intentar eliminar el usuario.');
                }
            },
            error: function (xhr, status, error) {
                mostrarMensaje('danger', 'Error al eliminar el usuario: ' + error);
            }
        });
    });
}


function mostrarMensaje(tipo, mensaje) {
    var mensajeElemento = $('#mensajeNotificacion');
    mensajeElemento.text(mensaje);
    mensajeElemento.removeClass().addClass('alert').addClass('alert-' + tipo).show();
}

function crearUsuario() {
    var nombre = $('#nombre').val();
    var fechaNacimiento = $('#fechaNacimiento').val();
    var idGenero = $('#idGenero').val();
    var idEstado = $('#idEstado').val();

    var usuario = {
        Nombre: nombre,
        FechaNacimiento: fechaNacimiento,
        IdGenero: idGenero,
        IdEstado: idEstado
    };

    $.ajax({
        url: adicionarUsuarioUrl,
        type: 'POST',
        data: JSON.stringify(usuario),
        contentType: 'application/json',
        success: function (response) {
            if (response.success) {
                $('#modalAgregarUsuario').modal('hide');
                MensajeAgregado('success', 'Usuario creado exitosamente');
            } else {
                MensajeAgregado('danger', 'Error al crear usuario: ' + response.message);
            }
        },
        error: function (xhr, status, error) {
            mostrarMensaje('danger', 'Error en la solicitud: ' + error);
        }
    });
}

$(document).ready(function () {
    var currentPage = 1;
    var rowsPerPage = 10;

    updateTable();
    updatePagination();

    $('#pagination').on('click', '.page-link', function (e) {
        e.preventDefault();
        var pageNum = parseInt($(this).text());
        if (!isNaN(pageNum)) {
            currentPage = pageNum;
            updateTable();
            updatePagination();
        }
    });

    function updateTable() {
        var startIndex = (currentPage - 1) * rowsPerPage;
        var endIndex = startIndex + rowsPerPage;
        $('#miTabla tbody tr').hide().slice(startIndex, endIndex).show();
    }

    function updatePagination() {
        var totalPages = getTotalPages();
        var paginationHtml = '';
        if (totalPages > 0) {
            if (currentPage === 1) {
                paginationHtml += '<li class="page-item disabled" id="prevPage"><a class="page-link" href="#" tabindex="-1" aria-disabled="true">Anterior</a></li>';
            } else {
                paginationHtml += '<li class="page-item" id="prevPage"><a class="page-link" href="#">Anterior</a></li>';
            }

            for (var i = 1; i <= totalPages; i++) {
                if (i === currentPage) {
                    paginationHtml += '<li class="page-item active"><a class="page-link" href="#">' + i + '</a></li>';
                } else {
                    paginationHtml += '<li class="page-item"><a class="page-link" href="#">' + i + '</a></li>';
                }
            }

            if (currentPage === totalPages) {
                paginationHtml += '<li class="page-item disabled" id="nextPage"><a class="page-link" href="#">Siguiente</a></li>';
            } else {
                paginationHtml += '<li class="page-item" id="nextPage"><a class="page-link" href="#">Siguiente</a></li>';
            }
        }
        $('#pagination').html(paginationHtml);
    }

    function getTotalPages() {
        return Math.ceil($('#miTabla tbody tr').length / rowsPerPage);
    }
});

function exportToExcel() {
    var table = document.getElementsByTagName("table")[0];
    var html = table.outerHTML;

    var data = [];
    var rows = table.rows;
    for (var i = 0; i < rows.length; i++) {
        var row = [], cols = rows[i].cells;
        for (var j = 0; j < cols.length; j++) {
            row.push(cols[j].innerText);
        }
        data.push(row);
    }

    var wb = XLSX.utils.book_new();
    var ws = XLSX.utils.aoa_to_sheet(data);
    XLSX.utils.book_append_sheet(wb, ws, "Sheet1");

    XLSX.writeFile(wb, "tabla_usuarios.xlsx");
}

