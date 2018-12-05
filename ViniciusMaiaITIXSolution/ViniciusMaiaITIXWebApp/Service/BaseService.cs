using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ViniciusMaiaITIXWebApp.DAO;
using ViniciusMaiaITIXWebApp.Models;

namespace ViniciusMaiaITIXWebApp.Service
{
    public abstract class BaseService<TModel, TDAO> : IService<TModel, TDAO> where TModel : BaseModel where TDAO : IDAO<TModel>
    {
        private TDAO _dao;
        public BaseService(TDAO dao)
        {
            _dao = dao;
        }

        public TModel BuscaPorId(int id)
        {
            return _dao.BuscaPorId(id);
        }

        public IList<TModel> ListaTodos()
        {
            return _dao.ListaTodos();
        }

        public void Remove(TModel model)
        {
            _dao.Remove(model.Id.Value);
        }

        public int Salva(TModel model)
        {
            if (model.Id != null)
            {
                return _dao.Atualiza(model);
            }
            else
            {
                return _dao.Insere(model);
            }
        }
    }
}