using ViniciusMaiaITIXWebApp.DAO.Interfaces;
using ViniciusMaiaITIXWebApp.Models;

namespace ViniciusMaiaITIXWebApp.Service
{
    public class PacienteService : BaseService<Paciente, IPacienteDAO>, IPacienteService
    {
        public PacienteService(IPacienteDAO dao) : base(dao)
        {
        }
    }
}