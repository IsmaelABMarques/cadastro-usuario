
using CadastroUsuario.Model.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroUsuario.Services
{
    public interface IRepositoryService<EntidadeIdentificavel>
    {
        public RepositoryBase _repositorio { get; set; }

        public Task<EntidadeIdentificavel> Get(int id);
        public Task<IEnumerable<EntidadeIdentificavel>> GetAll(EntidadeIdentificavel entity);
        public Task<int> Insert(EntidadeIdentificavel entity);
        public Task Update(EntidadeIdentificavel entity);
        public Task Delete(int id);
    }
}
