using NHibernate;
using ViniciusMaiaITIXWebApp.DAO.Interfaces;
using ViniciusMaiaITIXWebApp.Models;

namespace ViniciusMaiaITIXWebApp.DAO
{
    public class ConsultaDAO : BaseDAO<Consulta>, IConsultaDAO
    {
        public ConsultaDAO(ISession sesion) : base(sesion)
        {
        }
    }
}