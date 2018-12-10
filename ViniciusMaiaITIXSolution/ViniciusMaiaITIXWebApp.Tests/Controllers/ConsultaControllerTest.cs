using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using ViniciusMaiaITIXWebApp.Controllers;
using ViniciusMaiaITIXWebApp.Service;
using ViniciusMaiaITIXWebApp.ViewModel;

namespace ViniciusMaiaITIXWebApp.Tests.Controllers
{
    [TestClass]
    public class ConsultaControllerTest
    {
        [TestMethod]
        public void CamposObrigatoriosDevemEstarPreenchidos()
        {
            var consultaService = new Mock<IConsultaService>();
            var pacienteService = new Mock<IPacienteService>();

            var consultaController = new ConsultaController(consultaService.Object, pacienteService.Object);

            var consultaViewModel = new ConsultaViewModel
            {
                Observacoes = "Teste"
            };

            var jsonResult = consultaController.SalvaConsulta(consultaViewModel);

            var jObject = JObject.FromObject(jsonResult.Data);
            var mensagensErro = jObject["mensagensErro"].Value<JArray>();

            Assert.IsTrue(mensagensErro.Count == 4);            
        }
    }
}
