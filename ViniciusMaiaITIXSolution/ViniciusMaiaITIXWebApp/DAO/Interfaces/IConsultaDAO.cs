using System;
using ViniciusMaiaITIXWebApp.Models;

namespace ViniciusMaiaITIXWebApp.DAO.Interfaces
{
    public interface IConsultaDAO : IDAO<Consulta>
    {
        bool ExisteAgendamentoNesseHorario(DateTime dataHoraInicio, DateTime dataHoraFim);
    }
}
