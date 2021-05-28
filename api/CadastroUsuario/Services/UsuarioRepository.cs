using CadastroUsuario.Model.Entity;
using CadastroUsuario.Model.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroUsuario.Services
{
    public class UsuarioRepository : IRepositoryService<Usuario>
    {
        public UsuarioRepository(RepositoryBase repositorio)
        {
            this._repositorio = repositorio;
        }
        public RepositoryBase _repositorio { get; set; }

        public async Task<Usuario> Get(int id)
        {
            var usuario = await _repositorio.Usuario
                .FirstOrDefaultAsync(u => u.Id == id );

            if (usuario == null || usuario.Id < 1)
                throw new Exception("Usuário não encontrado");

            usuario.Status = await _repositorio.Status.FindAsync(usuario.StatusId);

            return usuario;
        }

        public async Task<IEnumerable<Usuario>> GetAll(Usuario usuario)
        {
            var usuarios =  _repositorio.Usuario
                .Where(u =>
                        usuario == null
                        ||
                        (
                                (usuario.Id == 0 || u.Id == usuario.Id)
                            && (usuario.Nome == null || usuario.Nome == "" || usuario.Nome == null || u.Nome == usuario.Nome)
                            && (usuario.Status == null || usuario.Status.Id == 0 || u.Status.Id == usuario.Status.Id)
                        )
               );

            if (usuarios == null || usuarios.Count() < 1)
                throw new Exception("Usuário não encontrado");

            usuarios.ToList().ForEach(u => u.Status = _repositorio.Status.Find(u.StatusId) );

            return usuarios.ToList();
        }

        public async Task<int> Insert([Bind("Id,Nome,Senha,StatusId")] Usuario usuario)
        {
            usuario.Status = null;
            var u = _repositorio.Add(usuario);
            await _repositorio.SaveChangesAsync();

            return u.Entity.Id;
        }

        public async Task Update([Bind("Id,Nome,Senha,StatusId")] Usuario usuario)
        {
            usuario.Status = null;
            var u = _repositorio.Update(usuario);
            await _repositorio.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var u = await _repositorio.Usuario.FindAsync(id);

            if (u == null || u.Id == 0)
               throw new System.Exception("Usuário não encontrado");

            _repositorio.Usuario.Remove(u);
            await _repositorio.SaveChangesAsync();
        }
    }
}
