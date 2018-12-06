using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViniciusMaiaITIXWebApp.DAO.Interfaces;
using ViniciusMaiaITIXWebApp.Service;
using ViniciusMaiaITIXWebApp.Models;

namespace ViniciusMaiaITIXWebApp.Tests.Services
{
    [TestClass]
    public class ConsultaServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DataHorarioFinalNaoPodeSerMenorQueInicial()
        {
            var consultaDao = new Mock<IConsultaDAO>();
            var consultaService = new ConsultaService(consultaDao.Object);

            var novaConsulta = new Consulta
            {
                DataHoraFim = new DateTime(2018, 12, 5, 15, 0, 0),
                DataHoraInicio = new DateTime(2018, 12, 5, 15, 30, 0),
                IdPaciente = 1,
            };

            consultaService.Salva(novaConsulta);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NaoPodeCriarConsultaComHorarioEmConflitoComOutroAgendamento()
        {
            var consultaDao = new Mock<IConsultaDAO>();
            var consultaService = new ConsultaService(consultaDao.Object);

            var dataHoraInicio = new DateTime(2018, 1, 2, 15, 0, 0);
            var dataHoraFim = new DateTime(2018, 1, 2, 15, 30, 0);

            consultaDao.Setup(dao => dao.ExisteAgendamentoNesseHorario(dataHoraInicio, dataHoraFim)).Returns(true);

            var consulta = new Consulta
            {
                DataHoraFim = dataHoraFim,
                DataHoraInicio = dataHoraInicio,
                IdPaciente = 1
            };

            consultaService.Salva(consulta);
        }
    }
}
