globalApp.service('pacienteService', function ($http) {
    this.getTodosPacientes = function () {
        return $http.get("/Paciente/ListarPacientes");
    }

    this.salvaPaciente = function (paciente) {
        var request = $http({
            method: 'POST',
            url: '/Paciente/AdicionaPaciente',
            data: paciente
        });

        return request;
    }

    //this.adicionaPaciente = function (paciente) {
    //    var request = $http({
    //        method: 'post',
    //        url: '/Paciente/AdicionaPaciente',
    //        data: paciente
    //    });

    //    return request;
    //}
});