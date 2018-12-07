using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ViniciusMaiaITIXWebApp.Models;
using ViniciusMaiaITIXWebApp.Service;

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

            return Json(listaConsultas, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Novo()
        {
            return View("Edit", new Consulta());
        }

        [HttpPost]
        public JsonResult AdicionaConsulta(Consulta consulta)
        {
            try
            {
                if (consulta.Paciente != null && consulta.DataHoraInicio != null && consulta.DataHoraFim != null)
                {
                    _consultaService.Salva(consulta);

                    return Json(new { success = true });
                }
                else
                {
                    throw new Exception("Preencha os campos \"Paciente\" e \"Data/Hora Início\" e \"Data/Hora Fim\"");
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
            var consulta = _consultaService.BuscaPorId(id);

            return View("Edit", consulta);
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