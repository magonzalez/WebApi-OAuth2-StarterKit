using System;
using System.Collections.Generic;

namespace StarterKit.Framework.Data
{
    public interface IDataModelRepository<TDataModel> where TDataModel : IDataModel
    {
        IEnumerable<TDataModel> GetAll();

        TDataModel Get(Guid id);

        Guid Insert(TDataModel entity);

        void Update(TDataModel entity);
        
        void Delete(Guid id);

        void DeleteAll();
    }
}
