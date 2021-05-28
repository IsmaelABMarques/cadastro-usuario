using CadastroUsuario.Model.Entity;
using CadastroUsuario.Model.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroUsuario.Services
{
    public class StatusRepository : IRepositoryService<Status>
    {
        public StatusRepository(RepositoryBase repositorio)
        {
            this._repositorio = repositorio;
        }
        public RepositoryBase _repositorio { get; set; }

        public async Task<Status> Get(int id)
        {
            var status = await _repositorio.Status
                .FirstOrDefaultAsync(s => s.Id == id);

            if (status == null || status.Id == 0)
                throw new Exception("Status não encontrado");

            return status;
        }

        public async Task<IEnumerable<Status>> GetAll(Status status)
        {
            var statusLista = _repositorio.Status
                .Where(s =>
                        status == null 
                        ||
                        (
                            (status.Id == 0 || s.Id == status.Id)
                            && (status.Descricao == null || status.Descricao == "" || status.Descricao == null || s.Descricao == status.Descricao)
                        )
               );

            if (statusLista == null || statusLista.Count() < 1)
                throw new Exception("Status não encontrado");

            return statusLista;
        }

        public async Task<int> Insert(Status status)
        {
            var s = _repositorio.Add(status);
            await _repositorio.SaveChangesAsync();

            return s.Entity.Id;
        }

        public async Task Update(Status status)
        {
            var u = _repositorio.Update(status);
            await _repositorio.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var s = await _repositorio.Status.FindAsync(id);

            if (s == null || s.Id == 0)
                throw new Exception("Status não encontrado");

            _repositorio.Status.Remove(s);
            await _repositorio.SaveChangesAsync();
        }
    }
}
