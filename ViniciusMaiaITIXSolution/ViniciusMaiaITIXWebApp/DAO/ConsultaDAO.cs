using NHibernate;
using System;
using System.Collections.Generic;
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

        public bool ExisteAgendamentoNesseHorario(int? idConsulta, DateTime dataHoraInicio, DateTime dataHoraFim)
        {
            var query = _session.Query<Consulta>().Where(c =>
                                                        (dataHoraInicio >= c.DataHoraInicio && dataHoraInicio < c.DataHoraFim) ||
                                                        (dataHoraFim > c.DataHoraInicio && dataHoraFim <= c.DataHoraFim));

            IList<Consulta> consultasComHorarioEmConflito = null;

            if (idConsulta != null)
            {
                consultasComHorarioEmConflito = query.Where(c => c.Id.Value != idConsulta.Value).ToList();
            }
            else
            {
                consultasComHorarioEmConflito = query.ToList();
            }


            return consultasComHorarioEmConflito != null && consultasComHorarioEmConflito.Count > 0;
        }
    }
}