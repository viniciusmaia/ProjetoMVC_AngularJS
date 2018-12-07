globalApp.service('consultaService', function ($http) {
    this.getTodosPacientes = function () {
        return $http.get("/Paciente/ListarPacientes");
    }

    this.getTodasConsultas = function () {
        return $http.get("/Consulta/ListarConsultas");
    }

    this.salvaConsulta = function (consulta) {
        var request = $http({
            method: 'POST',
            url: '/Consulta/AdicionaConsulta',
            data: consulta
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