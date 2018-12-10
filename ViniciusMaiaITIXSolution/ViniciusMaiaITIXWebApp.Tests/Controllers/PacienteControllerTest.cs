using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViniciusMaiaITIXWebApp.Service;
using ViniciusMaiaITIXWebApp.Controllers;
using Moq;
using ViniciusMaiaITIXWebApp.Models;
using Newtonsoft.Json.Linq;

namespace ViniciusMaiaITIXWebApp.Tests.Controllers
{
    [TestClass]
    public class PacienteControllerTest
    {
        [TestClass]
        public class ConsultaControllerTest
        {
            [TestMethod]
            public void CamposObrigatoriosDevemEstarPreenchidos()
            {
                var pacienteService = new Mock<IPacienteService>();

                var pacienteController = new PacienteController(pacienteService.Object);

                var paciente = new Paciente();

                var jsonResult = pacienteController.SalvaPaciente(paciente);

                var jObject = JObject.FromObject(jsonResult.Data);
                var mensagensErro = jObject["mensagensErro"].Value<JArray>();

                Assert.IsTrue(mensagensErro.Count == 2);
            }
        }
    }
}
