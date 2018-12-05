using FluentNHibernate.Mapping;
using ViniciusMaiaITIXWebApp.Models;

namespace ViniciusMaiaITIXWebApp.DAO.Map
{
    public class PacienteMap : ClassMap<Paciente>
    {
        public PacienteMap()
        {
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Nome).Length(100).Not.Nullable();
            Map(p => p.DataNascimento).Not.Nullable();
        }
    }
}