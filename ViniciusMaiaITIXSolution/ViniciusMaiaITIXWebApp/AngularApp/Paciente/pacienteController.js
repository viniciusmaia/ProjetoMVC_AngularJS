globalApp.controller('pacienteController', function ($scope, pacienteService) {
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
            if (d.data.success = true) {
                limpaDadosPaciente();
                abrePaginaLista();
                alert("Paciente adicionado com sucesso!");
            }
            else {
                alert("Falha ao adicionar paciente.");
            }
        },
        function () {
            alert("Falha ao adicionar paciente.");
        });
    }

    function abrePaginaLista() {
        window.location.href = '/Paciente/List';
    }

    function limpaDadosPaciente(){
        $scope.Id = null;
        $scope.Nome = null;
        $scope.DataNascimento = null;
    }
});