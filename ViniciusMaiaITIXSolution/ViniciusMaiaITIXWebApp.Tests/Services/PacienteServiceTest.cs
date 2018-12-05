using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViniciusMaiaITIXWebApp.DAO;
using ViniciusMaiaITIXWebApp.Service;
using ViniciusMaiaITIXWebApp.Models;
using NHibernate;

namespace ViniciusMaiaITIXWebApp.Tests.Services
{
    [TestClass]
    public class PacienteServiceTest
    {

        

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void NomeDoPacienteNaoPodeSerNuloOuVazio()
        {
            var session = new Mock<ISession>();
            var pacienteDao = new Mock<PacienteDAO>(session.Object);
            var pacienteService = new PacienteService(pacienteDao.Object);

            var pacienteNomeVazio = new Paciente
            {
                Nome = "            ",
                DataNascimento = DateTime.Now.Date
            };

            pacienteService.Salva(pacienteNomeVazio);

            pacienteDao.Verify(dao => dao.Insere(pacienteNomeVazio));
        }
    }
}
