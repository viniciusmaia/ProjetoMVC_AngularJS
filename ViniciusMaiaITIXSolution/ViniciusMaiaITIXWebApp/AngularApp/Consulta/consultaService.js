globalApp.service('consultaService', function ($http) {
    this.getTodosPacientes = function () {
        return $http.get("/Paciente/ListarPacientes");
    }

    this.getTodasConsultas = function () {
        return $http.get("/Consulta/ListarConsultas");
    }

    this.salvaConsulta = function (viewModel) {
        var request = $http({
            method: 'POST',
            url: '/Consulta/SalvaConsulta',
            data: viewModel
        });

        return request;
    }

    this.removeConsulta = function (consulta) {
        var request = $http({
            url: '/Consulta/RemoveConsulta',
            method: 'POST',
            data: consulta
        });

        return request;
    }
});