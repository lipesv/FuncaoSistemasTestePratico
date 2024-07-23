
$(document).ready(function () {

    if (obj) {

        var modalId = Math.random().toString().replace('.', '');
        var placeholderElement = $('#modal-placeholder');

        obj.Consulta = {
            Tabela: 'BENEFICIARIOS',
            Chave: 'IDCLIENTE',
            Valor: $('#formCadastro #Id').val(),
            Ordenacao: 'Nome ASC'
        };

        $('#formCadastro #Nome').val(obj.Nome);
        $('#formCadastro #Sobrenome').val(obj.Sobrenome);
        $('#formCadastro #Nacionalidade').val(obj.Nacionalidade);
        $('#formCadastro #Cpf').val(obj.Cpf);
        $('#formCadastro #Email').val(obj.Email);
        $('#formCadastro #Telefone').val(obj.Telefone);
        $('#formCadastro #CEP').val(obj.CEP);
        $('#formCadastro #Cidade').val(obj.Cidade);
        $('#formCadastro #Estado').val(obj.Estado);
        $('#formCadastro #Logradouro').val(obj.Logradouro);

        $('.cpf').mask('000.000.000-00', { reverse: true });
        $('.tel').mask('(00) 0000-0000');
        $('.cep').mask('00000-000');

        $('#' + modalId).on('hide.bs.modal', function () {
            $("#formCadastro")[0].reset();
            window.location.href = urlRetorno;
        });

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

        //debugger;

        $('#gridBeneficiarios').jtable({
            title: 'Beneficiários',
            paging: true, //Enable paging
            pageSize: 5, //Set page size (default: 10)
            sorting: true, //Enable sorting
            defaultSorting: 'Nome ASC', //Set default sorting
            actions: {
                listAction: function (postData, jtParams) {
                    return $.Deferred(function ($dfd) {
                        //debugger;
                        $.ajax({
                            url: urlConsultaBeneficiarios,
                            type: 'POST',
                            dataType: 'json',
                            data: {
                                TableName: obj.Consulta.Tabela,
                                ForeignKeyColumn: obj.Consulta.Chave,
                                ForeignKeyValue: obj.Consulta.Valor,
                                SortColumn: obj.Consulta.Ordenacao,
                                PageIndex: jtParams.jtStartIndex / jtParams.jtPageSize + 1,
                                PageSize: jtParams.jtPageSize
                            },
                            success: function (data) {
                                $dfd.resolve({
                                    Result: 'OK',
                                    Records: data.Records,
                                    TotalRecordCount: data.TotalRecordCount
                                });
                            },
                            error: function () {
                                debugger;
                                $dfd.reject();
                            }
                        });
                    });
                }
            },
            fields: {
                ID: {
                    key: true,
                    create: false,
                    edit: false,
                    list: false
                },
                NOME: {
                    title: 'Nome',
                    width: '50%'
                },
                CPF: {
                    title: 'CPF',
                    width: '15%'
                },
                Alterar: {
                    title: '',
                    display: function (data) {
                        debugger;
                        return '<div class="row"><div class="col text-center"><button type="button" class="btn btn-primary btn-sm" data-toggle="modal" data-target="#modal" data-url="' + urlAtualizaBeneficiario + '?id=' + data.record.ID + '" data-title="Atualização Beneficiário">Alterar</button></div></div>';
                    }
                }
            }
        });

        if (document.getElementById("gridBeneficiarios"))
            $('#gridBeneficiarios').jtable('load');

    }

    //function showNotifications(notifications) {

    //    var notificationsPlaceholder = $('#notifications');
    //    notificationsPlaceholder.empty(); // Limpar notificações anteriores
    //    var content = "";

    //    notifications.forEach(function (notification) {

    //        var alertType = 'alert-' + (notification.Type === "error" ? "danger" : "success");

    //        var alertHtml = '<div class="alert ' + alertType + ' alert-dismissible fade show" role="alert">' +
    //            notification.Message + '\n' +
    //            '<button type="button" class="close" data-dismiss="alert" aria-label="Close">' +
    //            '    <span aria-hidden="true">&times;</span>' +
    //            '</button>' +
    //            '</div>';

    //        content += alertHtml;

    //    });

    //    notificationsPlaceholder.html(content);
    //}

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
                    //ModalDialog(modalId, (r.success == true ? "Sucesso" : "Atenção"), (r.data != null ? r.data.message : r.errorMessage))
                    showNotifications(response.Mensagens, false);
                },
            error:
                function (response) {
                    //if (r.status == 400)
                    //    ModalDialog(modalId, "Ocorreu um erro", r.responseJSON);
                    //else if (r.status == 500)
                    //    ModalDialog(modalId, "Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                    showNotifications(response.Mensagens, false);
                }
        });

    })

    $(document).on('click', 'button[data-toggle="modal"]', function (event) {

        debugger;
        var url = $(this).data('url');
        var title = $(this).data('title');

        $.get(url).done(function (data) {
            debugger;
            placeholderElement.find('.modal-body').find('#modalContent').html(data);
            placeholderElement.find('.modal-title').html(title);

            placeholderElement.find('.modal').modal('show');

            $('.cpf').mask('000.000.000-00', { reverse: true });
        });

    });

    placeholderElement.on('click', '[data-save="modal"]', function (event) {

        event.preventDefault();

        var form = $(this).parents('.modal').find('form');
        var method = form.attr('method');
        var actionUrl = form.attr('action');
        var dataToSend = form.serialize();

        $.ajax({
            url: actionUrl,
            type: method,
            data: dataToSend,
            success: function (response) {

                debugger;

                if (response.Sucesso) {
                    placeholderElement.find('.modal').modal('hide');
                    showNotifications(response.Mensagens, false);

                    window.location.reload();
                }
                else {

                    if (response.Html != undefined) {
                        var newBody = $('.modal-body', response.Html);
                        placeholderElement.find('.modal-body').replaceWith(newBody);
                    } else {
                        showNotifications(response.Mensagens, true);
                    }
                }
            },
            error: function (xhr, status, error) {
                debugger;
                //var notifications = [{ message: 'Ocorreu um erro ao processar a solicitação.', type: 'error' }];
                showNotifications(['Ocorreu um erro no processamento da requisição.'], true);
            }
        });

    });

});

function ModalDialog(modalId, titulo, mensagem) {
    var texto = '<div id="' + modalId + '" class="modal fade">                                                               ' +
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