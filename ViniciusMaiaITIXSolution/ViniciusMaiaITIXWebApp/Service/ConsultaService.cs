using ViniciusMaiaITIXWebApp.DAO.Interfaces;
using ViniciusMaiaITIXWebApp.Models;

namespace ViniciusMaiaITIXWebApp.Service
{
    public class ConsultaService : BaseService<Consulta, IConsultaDAO>, IConsultaService
    {
        public ConsultaService(IConsultaDAO dao) : base(dao)
        {
        }
    }
}