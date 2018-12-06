using System;
using System.Text;
using ViniciusMaiaITIXWebApp.DAO.Interfaces;
using ViniciusMaiaITIXWebApp.Models;

namespace ViniciusMaiaITIXWebApp.Service
{
    public class ConsultaService : BaseService<Consulta, IConsultaDAO>, IConsultaService
    {
        public ConsultaService(IConsultaDAO dao) : base(dao)
        {
        }

        public override int Salva(Consulta consulta)
        {
            if (IsConsultaValida(consulta))
            {
                return base.Salva(consulta);
            }

            return -1;
        }

        private bool IsConsultaValida(Consulta consulta)
        {
            var mensagemErroBuilder = new StringBuilder();

            if (consulta.DataHoraFim.Value.CompareTo(consulta.DataHoraInicio.Value) <= 0)
            {
                mensagemErroBuilder.Append("O horário do final da consulta deve ser maior que o horário do início.");
                mensagemErroBuilder.Append("\n");
            }
            else if (_dao.ExisteAgendamentoNesseHorario(consulta.DataHoraInicio.Value, consulta.DataHoraFim.Value))
            {
                mensagemErroBuilder.Append("O horário da consulta está em conflito com um agendamento já existente.");
                mensagemErroBuilder.Append("\n");
            }

            var mensagemErro = mensagemErroBuilder.ToString();

            if (!string.IsNullOrWhiteSpace(mensagemErro))
            {
                throw new InvalidOperationException(mensagemErro);
            }

            return true;
        }
    }
}