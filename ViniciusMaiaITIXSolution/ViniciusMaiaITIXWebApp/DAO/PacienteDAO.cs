using NHibernate;
using ViniciusMaiaITIXWebApp.DAO.Interfaces;
using ViniciusMaiaITIXWebApp.Models;

namespace ViniciusMaiaITIXWebApp.DAO
{
    public class PacienteDAO : BaseDAO<Paciente>, IPacienteDAO
    {
        public PacienteDAO(ISession session) : base(session)
        {
        }
    }
}