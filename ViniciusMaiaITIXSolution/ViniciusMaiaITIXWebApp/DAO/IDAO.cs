using System.Collections.Generic;
using ViniciusMaiaITIXWebApp.Models;

namespace ViniciusMaiaITIXWebApp.DAO
{
    public interface IDAO<TModel> where TModel : BaseModel
    {
        int Insere(TModel model);
        int Atualiza(TModel model);
        void Remove(int id);
        IList<TModel> ListaTodos();
        TModel BuscaPorId(int id);
    }
}
