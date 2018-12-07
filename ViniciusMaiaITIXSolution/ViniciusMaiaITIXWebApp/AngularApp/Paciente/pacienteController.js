globalApp.controller('pacienteController', function ($scope, pacienteService) {

    if (typeof window.paciente !== 'undefined') {
        _init(window.paciente);
    }

    function _init(paciente) {
        $scope.Id = paciente.Id;
        $scope.Nome = paciente.Nome;
        $scope.DataNascimento = new Date(parseInt(paciente.DataNascimento.slice(6, -2)));
    }

    carregarPacientes();
    function carregarPacientes() {
        var listarPacientes = pacienteService.getTodosPacientes();

        listarPacientes.then(function (d) {
            $scope.Pacientes = d.data;
        },
        function (d) {
            alert("Falha ao listar todos os pacientes.");
            console.log(d);
        });
    }

    $scope.salvaPaciente = function () {
        var paciente = {
            Id: $scope.Id,
            Nome: $scope.Nome,
            DataNascimento: $scope.DataNascimento
        }
        var adicionarInformacoesPaciente = pacienteService.salvaPaciente(paciente);

        adicionarInformacoesPaciente.then(function (d) {
            if (d.data.success === true) {
                limpaDadosPaciente();
                abrePaginaLista();
                alert("Paciente salvo com sucesso!");
            }
            else {
                alert(d.data.errorMessage);
            }
        },
        function (d) {
            alert("Falha ao adicionar paciente.");
        });
    };

    $scope.removePaciente = function (paciente) {

        if (window.confirm('Tem certeza que deseja remover esse paciente?')) {
            var resultadoRemocaoPaciente = pacienteService.removePaciente(paciente);

            resultadoRemocaoPaciente.then(function (d) {
                if (d.data.success === true) {
                    abrePaginaLista();
                    alert("Paciente removido com sucesso.");
                }
                else {
                    alert(d.data.errorMessage);
                }
            },
            function (d) {
                alert("Falha ao remover paciente.");
            });
        }
    };


    $scope.redirecionaParaEdicao = function (paciente) {
        window.location.href = '/Paciente/Editar/' + paciente.Id;
    };

    function abrePaginaLista() {
        window.location.href = '/Paciente/List';
    }

    function limpaDadosPaciente(){
        $scope.Id = null;
        $scope.Nome = null;
        $scope.DataNascimento = null;
    }
});