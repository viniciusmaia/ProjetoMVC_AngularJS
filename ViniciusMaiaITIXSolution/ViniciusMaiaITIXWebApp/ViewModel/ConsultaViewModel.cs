using System;
using ViniciusMaiaITIXWebApp.Models;

namespace ViniciusMaiaITIXWebApp.ViewModel
{
    public class ConsultaViewModel
    {
        public int? IdConsulta { get; set; }
        public Paciente Paciente { get; set; }
        public DateTime? DataDaConsulta { get; set; }
        public DateTime? HoraInicio { get; set; }
        public DateTime? HoraFim { get; set; }
        public string Observacoes { get; set; }

        public ConsultaViewModel()
        {

        }

        public ConsultaViewModel(Consulta consulta)
        {
            IdConsulta = consulta.Id;
            Paciente = consulta.Paciente;
            DataDaConsulta = consulta.DataHoraInicio.Value.Date;
            HoraInicio = consulta.DataHoraInicio.Value;
            HoraFim = consulta.DataHoraFim.Value;
            Observacoes = consulta.Observacoes;
        }

        public Consulta ToModel()
        {
            return new Consulta
            {
                Id = IdConsulta,
                DataHoraFim = DataDaConsulta.Value + HoraFim.Value.TimeOfDay,
                DataHoraInicio = DataDaConsulta.Value + HoraInicio.Value.TimeOfDay,
                IdPaciente = Paciente.Id,
                Paciente = Paciente,
                Observacoes = Observacoes
            };
        }
    }
}