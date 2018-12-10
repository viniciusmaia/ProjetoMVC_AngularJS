using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ViniciusMaiaITIXWebApp.Models;
using ViniciusMaiaITIXWebApp.Service;

namespace ViniciusMaiaITIXWebApp.Controllers
{
    public class PacienteController : Controller
    {
        private IPacienteService _service;

        public PacienteController(IPacienteService pacienteService)
        {
            _service = pacienteService;
        }

        public ActionResult List()
        {
            return View();
        }

        public JsonResult ListarPacientes()
        {
            IList<Paciente> listaPacientes = _service.ListaTodos();

            return Json(listaPacientes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Novo()
        {
            return View("Edit", new Paciente());
        }

        [HttpPost]
        public JsonResult SalvaPaciente(Paciente paciente)
        {
            try
            {
                IList<string> mensagensDeErro = ValidaPaciente(paciente);

                if (mensagensDeErro.Count == 0)
                {
                    _service.Salva(paciente);

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

        private IList<string> ValidaPaciente(Paciente paciente)
        {
            IList<string> mensagensDeErro = new List<string>();

            if (string.IsNullOrWhiteSpace(paciente.Nome))
            {
                mensagensDeErro.Add("Informe o nome do paciente.");
            }

            if (paciente.DataNascimento == null)
            {
                mensagensDeErro.Add("Informe a data de nascimento.");
            }

            return mensagensDeErro;
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            var paciente = _service.BuscaPorId(id);

            return View("Edit", paciente);
        }

        [HttpPost]
        public JsonResult RemovePaciente(Paciente paciente)
        {
            try
            {
                _service.Remove(paciente);

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