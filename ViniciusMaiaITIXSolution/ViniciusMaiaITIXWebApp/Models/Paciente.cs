using System;

namespace ViniciusMaiaITIXWebApp.Models
{
    public class Paciente : BaseModel
    {
        public virtual string Nome { get; set; }
        public virtual DateTime? DataNascimento { get; set; }

        public override string ToString()
        {
            return Nome;
        }
    }
}