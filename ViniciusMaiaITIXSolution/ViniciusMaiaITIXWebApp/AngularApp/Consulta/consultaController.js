globalApp.controller('consultaController', function ($scope, $filter, consultaService) {

    if (typeof window.viewModel !== 'undefined') {
        _init(window.viewModel);
    }

    function _init(viewModel) {
        $scope.IdConsulta = viewModel.IdConsulta;
        $scope.Paciente = viewModel.Paciente;

        console.log($scope.Paciente);

        if (typeof viewModel.DataDaConsulta !== 'undefined' && viewModel.DataDaConsulta !== null) {
            $scope.DataDaConsulta = new Date(parseInt(viewModel.DataDaConsulta.slice(6, -2)));
        }

        if (typeof viewModel.HoraInicio !== 'undefined' && viewModel.HoraInicio !== null) {
            $scope.HoraInicio = new Date(parseInt(viewModel.HoraInicio.slice(6, -2)));
        }

        if (typeof viewModel.HoraFim !== 'undefined' && viewModel.HoraFim !== null) {
            $scope.HoraFim = new Date(parseInt(viewModel.HoraFim.slice(6, -2)));
        }

        $scope.Observacoes = viewModel.Observacoes;
    }

    carregarPacientes();
    function carregarPacientes() {
        var listarPacientes = consultaService.getTodosPacientes();

        listarPacientes.then(function (d) {
            $scope.ListaPacientes = d.data;

            if ($scope.Paciente !== null) {
                $scope.Paciente = $scope.ListaPacientes.find(function () {
                    var pacienteSelecionado = null;

                    for (var i = 0; i < $scope.ListaPacientes.length; i++) {
                        if ($scope.Paciente.Id === $scope.ListaPacientes[i].Id) {
                            pacienteSelecionado = $scope.ListaPacientes[i];
                            break;
                        }
                    }

                    return pacienteSelecionado;
                })
            }
        },
        function (d) {
            alert("Falha ao listar todos os pacientes.");
            console.log(d);
        });
    }

    carregarConsultas();
    function carregarConsultas() {
        var listarConsultas = consultaService.getTodasConsultas();

        listarConsultas.then(function (d) {
            $scope.Consultas = d.data;
        },
        function (d) {
            alert("Falha ao listar todos as consultas.");
            console.log(d);
        });
    }

    $scope.salvaConsulta = function () {

        console.log($scope.HoraInicio);
        console.log($scope.HoraFim);

        var viewModel = {
            IdConsulta: $scope.IdConsulta,
            Paciente: $scope.Paciente,
            DataDaConsulta: $scope.DataDaConsulta,
            HoraInicio: $scope.HoraInicio.toLocaleString(),
            HoraFim: $scope.HoraFim.toLocaleString(),
            Observacoes: $scope.Observacoes
        }

        var adicionarInformacoesConsulta = consultaService.salvaConsulta(viewModel);

        adicionarInformacoesConsulta.then(function (d) {
            if (d.data.success === true) {
                limpaDadosConsulta();
                abrePaginaLista();
                alert("Consulta salva com sucesso!");
            }
            else {
                console.log(d.data.mensagensErro);
                $scope.mensagensDeErro = d.data.mensagensErro;
            }
        },
        function (d) {
            $scope.mensagensDeErro = ["Ocorreu um erro inesperado ao salvar a consulta."];
        });
    };

    $scope.removeConsulta = function (consulta) {

        if (window.confirm('Tem certeza que deseja remover essa consulta?')) {
            var resultadoRemocaoConsulta = consultaService.removeConsulta(consulta);

            resultadoRemocaoConsulta.then(function (d) {
                if (d.data.success === true) {
                    abrePaginaLista();
                    alert("Consulta removida com sucesso.");
                }
                else {
                    alert(d.data.errorMessage);
                }
            },
            function (d) {
                alert("Falha ao remover consulta.");
            });
        }
    };


    $scope.redirecionaParaEdicao = function (consulta) {
        window.location.href = '/Consulta/Editar/' + consulta.Id;
    };

    function abrePaginaLista() {
        window.location.href = '/Consulta/List';
    }

    function limpaDadosConsulta() {
        $scope.mensagensDeErro = null;
        $scope.IdConsulta = null;
        $scope.Paciente = null;
        $scope.DataDaConsulta = null;
        $scope.Horainicio = null;
        $scope.HoraFim = null;
        $scope.Observacoes = null;
        $scope.ListaPacientes = null;
    }
});