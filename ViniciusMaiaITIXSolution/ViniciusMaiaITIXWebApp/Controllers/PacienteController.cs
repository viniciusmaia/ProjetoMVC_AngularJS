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
        public JsonResult AdicionaPaciente(Paciente paciente)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(paciente.Nome) && paciente.DataNascimento != null)
                {
                    _service.Salva(paciente);

                    return Json(new { success = true });
                }
                else
                {
                    throw new Exception("Preencha os campos \"Nome\" e \"Data de Nascimento\"");
                }
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