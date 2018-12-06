﻿using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using ViniciusMaiaITIXWebApp.Models;

namespace ViniciusMaiaITIXWebApp.DAO
{
    public abstract class BaseDAO<TModel> : IDAO<TModel> where TModel : BaseModel
    {
        protected ISession _session;

        public BaseDAO(ISession session)
        {
            _session = session;
        }

        public virtual int Atualiza(TModel model)
        {
            using (ITransaction transaction = _session.BeginTransaction())
            {
                _session.Merge(model);
                transaction.Commit();
            }

            return model.Id.Value;
        }

        public virtual TModel BuscaPorId(int id)
        {
            var model = _session.Get<TModel>(id);
            return model;
        }

        public virtual int Insere(TModel model)
        {
            using (ITransaction transaction = _session.BeginTransaction())
            {
                _session.Save(model);
                transaction.Commit();
            }

            return model.Id.Value;
        }

        public virtual IList<TModel> ListaTodos()
        {
            return _session.Query<TModel>().ToList();
        }

        public virtual void Remove(int id)
        {
            using (ITransaction transaction = _session.BeginTransaction())
            {
                var model = BuscaPorId(id);
                _session.Delete(model);
                transaction.Commit();
            }
        }
    }
}