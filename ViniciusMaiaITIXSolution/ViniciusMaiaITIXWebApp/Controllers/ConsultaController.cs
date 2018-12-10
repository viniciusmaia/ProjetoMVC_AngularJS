using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ViniciusMaiaITIXWebApp.Models;
using ViniciusMaiaITIXWebApp.Service;
using ViniciusMaiaITIXWebApp.ViewModel;

namespace ViniciusMaiaITIXWebApp.Controllers
{
    public class ConsultaController : Controller
    {
        private IConsultaService _consultaService;
        private IPacienteService _pacienteService;

        public ConsultaController(IConsultaService consultaService, IPacienteService pacienteService)
        {
            _consultaService = consultaService;
            _pacienteService = pacienteService;
        }

        public ActionResult List()
        {
            return View();
        }

        public JsonResult ListarPacientes()
        {
            IList<Paciente> listaPacientes = _pacienteService.ListaTodos();

            return Json(listaPacientes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarConsultas()
        {
            IList<Consulta> listaConsultas = _consultaService.ListaTodos();

            foreach (var consulta in listaConsultas)
            {
                consulta.Paciente = _pacienteService.BuscaPorId(consulta.IdPaciente.Value);
            }

            return Json(listaConsultas, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Novo()
        {
            return View("Edit", new ConsultaViewModel());
        }

        [HttpPost]
        public JsonResult SalvaConsulta(ConsultaViewModel viewModel)
        {
            try
            {
                IList<string> mensagensDeErro = ValidaViewModel(viewModel);

                if (mensagensDeErro.Count == 0)
                {
                   
                    var consulta = viewModel.ToModel();
                    _consultaService.Salva(consulta);

                    return Json(new { success = true });
                }

                return Json(new
                {
                    success = false,
                    mensagensErro = mensagensDeErro
                });
            }
            catch (Exception e)
            {
                string[] erros = e.Message.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                return Json(new
                {
                    success = false,
                    mensagensErro = erros
                });
            }
        }

        private IList<string> ValidaViewModel(ConsultaViewModel viewModel)
        {
            IList<string> mensagensDeErro = new List<string>();

            if (viewModel.Paciente == null)
            {
                mensagensDeErro.Add("Selecione o paciente.");
            }

            if (viewModel.DataDaConsulta == null)
            {
                mensagensDeErro.Add("Informe a data da consulta.");
            }

            if (viewModel.HoraInicio == null)
            {
                mensagensDeErro.Add("Informe a hora do início da consulta.");
            }

            if (viewModel.HoraFim == null)
            {
                mensagensDeErro.Add("Informe a hora final da consulta.");
            }

            return mensagensDeErro;
        }
        
        [HttpGet]
        public ActionResult Editar(int id)
        {
            Consulta consulta = _consultaService.BuscaPorId(id);
            var viewModel = new ConsultaViewModel(consulta);
            viewModel.Paciente = _pacienteService.BuscaPorId(consulta.IdPaciente.Value);

            return View("Edit", viewModel);
        }

        [HttpPost]
        public JsonResult RemoveConsulta(Consulta consulta)
        {
            try
            {
                _consultaService.Remove(consulta);

                return Json(new { success = true } );
            }
            catch (Exception e)
            {
                return Json(new
                {
                    success = false,
                    errorMessage = e.Message
                });
            }
        }
    }
}