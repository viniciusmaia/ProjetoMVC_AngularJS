using System.Collections.Generic;
using ViniciusMaiaITIXWebApp.DAO;
using ViniciusMaiaITIXWebApp.Models;

namespace ViniciusMaiaITIXWebApp.Service
{
    public interface IService<TModel, TDAO> where TModel : BaseModel where TDAO : IDAO<TModel>
    {
        IList<TModel> ListaTodos();
        TModel BuscaPorId(int id);
        int Salva(TModel model);
        void Remove(TModel model);
    }
}
