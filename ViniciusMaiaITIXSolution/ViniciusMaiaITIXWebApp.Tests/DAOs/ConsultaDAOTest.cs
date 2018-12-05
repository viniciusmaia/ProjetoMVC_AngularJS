using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViniciusMaiaITIXWebApp.DAO;
using NHibernate;
using ViniciusMaiaITIXWebApp.Models;
using System.Collections.Generic;

namespace ViniciusMaiaITIXWebApp.Tests.DAOs
{
    [TestClass]
    public class ConsultaDAOTest
    {
        private ISession _session;
        private ConsultaDAO _consultaDAO;
        private PacienteDAO _pacienteDAO;

        [TestInitialize]
        public void Init()
        {
            _session = SessionTestFactory.OpenSessionTest();
            _consultaDAO = new ConsultaDAO(_session);
            _pacienteDAO = new PacienteDAO(_session);
        }

        [TestMethod]
        public void InsereConsulta()
        {
            var paciente = new Paciente
            {
                Nome = @"Paciente criado em " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") +
                        " para testar criação de consulta",
                DataNascimento = DateTime.Now.AddDays(-1)
            };

            _pacienteDAO.Insere(paciente);

            var Consulta = new Consulta
            {
                DataHoraInicio = DateTime.Now,
                DataHoraFim = DateTime.Now.AddMinutes(30),
                Observacoes = "Testando inserção de consulta",
                Paciente = paciente,
                IdPaciente = paciente.Id
            };

            int id = _consultaDAO.Insere(Consulta);

            Assert.IsTrue(id > 0);
            Assert.IsNotNull(Consulta.Id);

            var consultaSalvoNoBanco = _session.Get<Consulta>(id);

            Assert.IsNotNull(consultaSalvoNoBanco);
        }

        [TestMethod]
        public void AtualizaConsulta()
        {
            var paciente = new Paciente
            {
                Nome = @"Paciente criado em " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") +
                        " para testar atualização de consulta",
                DataNascimento = DateTime.Now.AddDays(-1)
            };

            _pacienteDAO.Insere(paciente);

            var consulta = new Consulta
            {
                DataHoraInicio = DateTime.Now,
                DataHoraFim = DateTime.Now.AddMinutes(30),
                Observacoes = "Testando Atualização de consulta",
                Paciente = paciente,
                IdPaciente = paciente.Id
            };

            int id = _consultaDAO.Insere(consulta);

            var consultaAtualizada = new Consulta
            {
                Id = id,
                DataHoraInicio = DateTime.Now.AddDays(-15),
                DataHoraFim = DateTime.Now.AddDays(-15),
                Observacoes = "A consulta foi alterada com sucesso!",
                Paciente = paciente,
                IdPaciente = paciente.Id
            };

            int idConsultaAtualizado = _consultaDAO.Atualiza(consultaAtualizada);

            Assert.AreEqual(idConsultaAtualizado, id);
            Assert.IsTrue(consultaAtualizada.DataHoraInicio.Value.Date.CompareTo(DateTime.Now.Date.AddDays(-15)) == 0);
            Assert.IsTrue(consultaAtualizada.DataHoraFim.Value.Date.CompareTo(DateTime.Now.Date.AddDays(-15)) == 0);
        }

        [TestMethod]
        public void BuscaConsultaPorId()
        {
            var paciente = new Paciente
            {
                Nome = @"Paciente criado em " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") +
                        " para testar busca por consulta",
                DataNascimento = DateTime.Now.AddDays(-1)
            };

            _pacienteDAO.Insere(paciente);

            var consulta = new Consulta
            {
                DataHoraInicio = DateTime.Now,
                DataHoraFim = DateTime.Now.AddMinutes(30),
                Observacoes = "Testando busca por consulta",
                Paciente = paciente,
                IdPaciente = paciente.Id
            };

            int id = _consultaDAO.Insere(consulta);

            Consulta consultaSalvoNoBanco = _consultaDAO.BuscaPorId(id);
            Assert.IsNotNull(consultaSalvoNoBanco);
        }

        [TestMethod]
        public void ListaTodasConsultas()
        {
            IList<Consulta> Consultas = _consultaDAO.ListaTodos();
            Assert.IsNotNull(Consultas);
            Assert.IsTrue(Consultas.Count > 0);
        }

        [TestMethod]
        public void RemoveConsulta()
        {
            var paciente = new Paciente
            {
                Nome = @"Paciente criado em " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") +
                        " para testar remoção de consulta",
                DataNascimento = DateTime.Now.AddDays(-1)
            };

            _pacienteDAO.Insere(paciente);

            var consulta = new Consulta
            {
                DataHoraInicio = DateTime.Now,
                DataHoraFim = DateTime.Now.AddMinutes(30),
                Observacoes = "Testando remoção por consulta",
                Paciente = paciente,
                IdPaciente = paciente.Id
            };

            int id = _consultaDAO.Insere(consulta);

            _consultaDAO.Remove(id);

            Consulta ConsultaSalvoNoBanco = _consultaDAO.BuscaPorId(id);
            Assert.IsNull(ConsultaSalvoNoBanco);
        }
    }
}
