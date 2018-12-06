using NHibernate;
using System;
using System.Linq;
using ViniciusMaiaITIXWebApp.DAO.Interfaces;
using ViniciusMaiaITIXWebApp.Models;

namespace ViniciusMaiaITIXWebApp.DAO
{
    public class ConsultaDAO : BaseDAO<Consulta>, IConsultaDAO
    {
        public ConsultaDAO(ISession sesion) : base(sesion)
        {
        }

        public bool ExisteAgendamentoNesseHorario(DateTime dataHoraInicio, DateTime dataHoraFim)
        {
            var consultasComHorarioEmConflito = _session.Query<Consulta>().Where(c =>
                                                        (dataHoraInicio >= c.DataHoraInicio && dataHoraInicio < c.DataHoraFim) ||
                                                        (dataHoraFim > c.DataHoraInicio && dataHoraFim <= c.DataHoraFim)).ToList();

            return consultasComHorarioEmConflito != null && consultasComHorarioEmConflito.Count > 0;
        }
    }
}