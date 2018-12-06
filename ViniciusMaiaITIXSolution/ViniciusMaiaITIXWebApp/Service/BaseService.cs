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
        protected TDAO _dao;
        public BaseService(TDAO dao)
        {
            _dao = dao;
        }

        public virtual TModel BuscaPorId(int id)
        {
            return _dao.BuscaPorId(id);
        }

        public virtual IList<TModel> ListaTodos()
        {
            return _dao.ListaTodos();
        }

        public virtual void Remove(TModel model)
        {
            _dao.Remove(model.Id.Value);
        }

        /*Eu "divido" o método "Salva" nos métodos "Atualiza" e "Insere" porque algumas regras podem ser
        específicas da inserção, outras da atualização e podem haver regras comuns tanto para atualização 
        quanto para inserção que, nesse caso, deverão ser aplicadas no método "Salva"*/
        public virtual int Salva(TModel model)
        {
            if (model.Id != null)
            {
                return Atualiza(model);
            }
            else
            {
                return Insere(model);
            }
        }

        protected virtual int Atualiza(TModel model)
        {
            return _dao.Atualiza(model);
        }

        protected virtual int Insere(TModel model)
        {
            return _dao.Insere(model);
        }
    }
}