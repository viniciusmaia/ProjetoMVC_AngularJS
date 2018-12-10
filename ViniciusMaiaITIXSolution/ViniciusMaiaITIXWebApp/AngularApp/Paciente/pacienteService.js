globalApp.service('pacienteService', function ($http) {
    this.getTodosPacientes = function () {
        return $http.get("/Paciente/ListarPacientes");
    }

    this.salvaPaciente = function (paciente) {
        var request = $http({
            method: 'POST',
            url: '/Paciente/SalvaPaciente',
            data: paciente
        });

        return request;
    }

    this.removePaciente = function (paciente) {
        var request = $http({
            url: '/Paciente/RemovePaciente',
            method: 'POST',
            data: paciente
        });

        return request;
    }
});