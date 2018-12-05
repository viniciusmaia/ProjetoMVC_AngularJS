using System;

namespace ViniciusMaiaITIXWebApp.Models
{
    public class Consulta : BaseModel
    {
        public virtual int? IdPaciente { get; set; }
        public virtual Paciente Paciente { get; set; }
        public virtual DateTime? DataHoraInicio { get; set; }
        public virtual DateTime? DataHoraFim { get; set; }
        public virtual string Observacoes { get; set; }
    }
}