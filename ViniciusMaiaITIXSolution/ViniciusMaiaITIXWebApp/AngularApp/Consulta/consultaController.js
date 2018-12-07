globalApp.controller('consultaController', function ($scope, consultaService) {

    if (typeof window.consulta !== 'undefined') {
        _init(window.consulta);
    }

    function _init(consulta) {
        $scope.Id = consulta.Id;
        $scope.Paciente = consulta.Paciente;
        $scope.IdPaciente = consulta.IdPaciente

        if (typeof consulta.DataHorainicio !== 'undefined' && consulta.DataHorainicio !== null) {
            $scope.DataHorainicio = new Date(parseInt(consulta.DataHorainicio.slice(6, -2)));
        }

        if (typeof consulta.DataHoraFim !== 'undefined' && consulta.DataHoraFim !== null) {
            $scope.DataHoraFim = new Date(parseInt(consulta.DataHoraFim.slice(6, -2)));
        }

        $scope.Observacoes = consulta.Observacoes;
    }

    carregarPacientes();
    function carregarPacientes() {
        var listarPacientes = consultaService.getTodosPacientes();

        listarPacientes.then(function (d) {
            $scope.ListaPacientes = d.data;
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

        console.log("Data Inicio: " + $scope.DataHorainicio);
        console.log("Data Fim: " + $scope.DataHoraFim);

        var consulta = {
            Id: $scope.Id,
            Paciente: $scope.Paciente,
            DataHorainicio: $scope.DataHorainicio,
            DataHoraFim: $scope.DataHoraFim,
            Observacoes: $scope.Observacoes
        }
        var adicionarInformacoesConsulta = consultaService.salvaConsulta(consulta);

        adicionarInformacoesConsulta.then(function (d) {
            if (d.data.success === true) {
                limpaDadosConsulta();
                abrePaginaLista();
                alert("Consulta salva com sucesso!");
            }
            else {
                alert(d.data.errorMessage);
            }
        },
        function (d) {
            alert("Falha ao adicionar consulta.");
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

    function limpaDadosConsulta(){
        $scope.Id = null;
        $scope.Nome = null;
        $scope.DataNascimento = null;
    }
});