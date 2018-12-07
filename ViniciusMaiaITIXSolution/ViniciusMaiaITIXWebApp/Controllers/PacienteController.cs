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
        public ActionResult Salva(Paciente paciente)
        {
            _service.Salva(paciente);
            return List();
        }

        [HttpPost]
        public JsonResult AdicionaPaciente(Paciente paciente)
        {
            try
            {
                _service.Salva(paciente);

                return Json(new { success = true });
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