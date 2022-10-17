using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MozaeekCore.Domain
{
    public interface IFileRepository
    {
        Task<MosaikFile> Find(long id);
        Task Create(MosaikFile mosaikFile);
        void Delete(MosaikFile file);
    }
}
