using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViniciusMaiaITIXWebApp.DAO;
using NHibernate;
using ViniciusMaiaITIXWebApp.Models;
using System.Collections.Generic;

namespace ViniciusMaiaITIXWebApp.Tests.DAOs
{
    [TestClass]
    public class PacienteDAOTest
    {
        private ISession _session;
        private PacienteDAO _pacienteDAO;

        [TestInitialize]
        public void Init()
        {
            _session = SessionTestFactory.OpenSessionTest();
            _pacienteDAO = new PacienteDAO(_session);
        }

        [TestMethod]
        public void InserePaciente()
        {
            var paciente = new Paciente
            {
                Nome = "Vinícius Teste",
                DataNascimento = DateTime.Now.Date
            };

            int id = _pacienteDAO.Insere(paciente);

            Assert.IsTrue(id > 0);
            Assert.IsNotNull(paciente.Id);

            var pacienteSalvoNoBanco = _session.Get<Paciente>(id);

            Assert.IsNotNull(pacienteSalvoNoBanco);
        }

        [TestMethod]
        public void AtualizaPaciente()
        {
            var paciente = new Paciente
            {
                Nome = "Novo Paciente em " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"),
                DataNascimento = DateTime.Now.Date
            };

            int id = _pacienteDAO.Insere(paciente);

            var pacienteAtualizado = new Paciente
            {
                Id = id,
                Nome = "Paciente Atualizado em " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"),
                DataNascimento = DateTime.Now.Date.AddDays(-15)
            };

            int idPacienteAtualizado = _pacienteDAO.Atualiza(pacienteAtualizado);

            Assert.AreEqual(idPacienteAtualizado, id);
            Assert.IsTrue(pacienteAtualizado.DataNascimento.Value.Date.CompareTo(DateTime.Now.Date.AddDays(-15)) == 0);
        }

        [TestMethod]
        public void BuscaPacientePorId()
        {
            var paciente = new Paciente
            {
                Nome = "Paciente criado para testar busca em" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"),
                DataNascimento = DateTime.Now.Date
            };

            int id = _pacienteDAO.Insere(paciente);

            Paciente pacienteSalvoNoBanco = _pacienteDAO.BuscaPorId(id);
            Assert.IsNotNull(pacienteSalvoNoBanco);
        }

        [TestMethod]
        public void ListaTodosPacientes()
        {
            IList<Paciente> pacientes = _pacienteDAO.ListaTodos();
            Assert.IsNotNull(pacientes);
            Assert.IsTrue(pacientes.Count > 0);
        }

        [TestMethod]
        public void RemovePaciente()
        {
            var paciente = new Paciente
            {
                Nome = "Paciente criado para testar remoção em" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"),
                DataNascimento = DateTime.Now.Date
            };

            int id = _pacienteDAO.Insere(paciente);

            _pacienteDAO.Remove(id);

            Paciente pacienteSalvoNoBanco = _pacienteDAO.BuscaPorId(id);
            Assert.IsNull(pacienteSalvoNoBanco);
        }
    }
}
