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

            if (consulta.DataHoraInicio.Value < DateTime.Now || consulta.DataHoraFim < DateTime.Now)
            {
                mensagemErroBuilder.Append("A data e o horário da consulta não podem ser menores que a data e o horário atual.");
                mensagemErroBuilder.Append("\n");
            }
            if (consulta.DataHoraFim.Value.CompareTo(consulta.DataHoraInicio.Value) <= 0)
            {
                mensagemErroBuilder.Append("A hora fim da consulta deve ser maior que o hora início.");
                mensagemErroBuilder.Append("\n");
            }
            if (_dao.ExisteAgendamentoNesseHorario(consulta.Id, consulta.DataHoraInicio.Value, consulta.DataHoraFim.Value))
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