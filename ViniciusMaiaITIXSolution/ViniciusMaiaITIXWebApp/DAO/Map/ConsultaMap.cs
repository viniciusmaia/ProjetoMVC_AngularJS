using FluentNHibernate.Mapping;
using ViniciusMaiaITIXWebApp.Models;

namespace ViniciusMaiaITIXWebApp.DAO.Map
{
    public class ConsultaMap : ClassMap<Consulta>
    {
        public ConsultaMap()
        {
            Id(c => c.Id).GeneratedBy.Identity();
            Map(c => c.DataHoraInicio).Not.Nullable();
            Map(c => c.DataHoraFim).Not.Nullable();
            Map(c => c.Observacoes).Length(500);
            Map(c => c.IdPaciente).Not.Nullable();
        }
    }
}