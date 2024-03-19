using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZScool.Services.IServices
{
    public interface IBaseServices<Model>
    {
        public Task Add(Model model);
        public Task Delete(string Id);
        public Task Update(Model model);
        public Task<IEnumerable<Model>> GetAllAsEnumerable();
        public Task<List<Model>> GetAllAsListAsync();
        public Task<Model> GetById(string Id);


    }
}
