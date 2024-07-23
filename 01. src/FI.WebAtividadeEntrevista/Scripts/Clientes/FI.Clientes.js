
$(document).ready(function () {

    $('.cpf').mask('000.000.000-00', { reverse: true });
    $('.tel').mask('(00) 0000-0000');
    $('.cep').mask('00000-000');

    $('.cep').on('blur', function () {

        var cep = $(this).val().replace(/\D/g, '');

        if (cep.length === 8 && $('#Estado').val() == "" && $('#Cidade').val() == "" && $('#Logradouro').val() == "") {

            $.getJSON('https://viacep.com.br/ws/' + cep + '/json/', function (data) {
                if (!("erro" in data)) {
                    $('#Estado').val(data.uf);
                    $('#Cidade').val(data.localidade);
                    $('#Logradouro').val(data.logradouro);
                } else {
                    alert('CEP não encontrado.');
                }
            }).fail(function () {
                alert('Erro ao consultar o CEP.');
            });

        } else {
            $('#Estado').val("");
            $('#Cidade').val("");
            $('#Logradouro').val("");
        }
    });

    $('#formCadastro').submit(function (e) {

        e.preventDefault();

        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                "NOME": $(this).find("#Nome").val(),
                "Sobrenome": $(this).find("#Sobrenome").val(),
                "Nacionalidade": $(this).find("#Nacionalidade").val(),
                "Cpf": $(this).find("#Cpf").val(),
                "Email": $(this).find("#Email").val(),
                "Telefone": $(this).find("#Telefone").val(),
                "CEP": $(this).find("#CEP").val(),
                "Cidade": $(this).find("#Cidade").val(),
                "Estado": $(this).find("#Estado").val(),
                "Logradouro": $(this).find("#Logradouro").val()
            },
            success:
                function (response) {
                    debugger;
                    //ModalDialog((r.success == true ? "Sucesso" : "Atenção"), (r.data != null ? r.data.message : r.errorMessage));
                    showNotifications(response.Mensagens, false);
                    $("#formCadastro")[0].reset();
                },
            error:
                function (response) {
                    debugger;
                    //if (r.status == 400)
                    //    ModalDialog("Ocorreu um erro", r.responseJSON);
                    //else if (r.status == 500)
                    //    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                    showNotifications(['Ocorreu um erro no processamento da requisição.'], false);
                }
        });
    });

});

function ModalDialog(titulo, mensagem) {

    var random = Math.random().toString().replace('.', '');

    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + mensagem + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}

function showNotifications(notifications, isModal) {

    var container = isModal ? '#modalNotifications' : '#mainNotifications';
    var alertClass = isModal ? 'alert-danger' : 'alert-success';

    var html = '<div class="alert ' + alertClass + ' alert-dismissible fade show" role="alert">';

    notifications.forEach(function (message) {
        html += '<p>' + message + '</p>';
    });

    html += '<button type="button" class="close" data-dismiss="alert" aria-label="Close">';
    html += '<span aria-hidden="true">&times;</span>';
    html += '</button>';
    html += '</div>';

    $(container).html(html);

    setTimeout(function () {
        $(container).find('.alert').alert('close');
    }, 5000);
}