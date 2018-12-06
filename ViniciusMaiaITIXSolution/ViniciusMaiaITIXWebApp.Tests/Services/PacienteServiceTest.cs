using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViniciusMaiaITIXWebApp.DAO;
using ViniciusMaiaITIXWebApp.Service;
using ViniciusMaiaITIXWebApp.Models;
using NHibernate;
using ViniciusMaiaITIXWebApp.DAO.Interfaces;

namespace ViniciusMaiaITIXWebApp.Tests.Services
{
    [TestClass]
    public class PacienteServiceTest
    {

        

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NomeDoPacienteNaoPodeSerNuloOuVazio()
        {
            var pacienteDao = new Mock<IPacienteDAO>();
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
