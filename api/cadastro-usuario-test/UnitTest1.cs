using CadastroUsuario.Model.Entity;
using CadastroUsuario.Model.Repository;
using CadastroUsuario.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace cadastro_usuario_test
{
    [TestClass]
    public class UsuarioTest
    {
        private UsuarioRepository _repository;
        public async Task Initialize()
        {
            var context = new DbContextOptionsBuilder<RepositoryBase>();
            context.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=UsuarioProd;Trusted_Connection=True;MultipleActiveResultSets=true");
            _repository = new UsuarioRepository(new RepositoryBase(context.Options));

        }
        [TestMethod]
        public async Task Create()
        {
            await Initialize();
            Usuario usuario = new Usuario();
            Usuario usuarioBuscado = new Usuario();

            usuario = new Usuario { StatusId = 1, Nome = "Teste", Senha = "Teste" };
            usuario.Id = await _repository.Insert(usuario);
            usuarioBuscado = await _repository.Get(usuario.Id);

            await _repository.Delete(usuario.Id);

            Assert.IsFalse(
                usuario == null || usuario.Id != usuarioBuscado.Id || usuario.StatusId != usuarioBuscado.StatusId
                || usuario.Senha != usuarioBuscado.Senha || usuario.Nome != usuarioBuscado.Nome,
                "O Usuário não foi inserido corretamente."
            );
        }
        [TestMethod]
        public async Task Edit()
        {
            await Initialize();
            Usuario usuario = new Usuario();
            Usuario usuarioBuscado = new Usuario();

            usuario = new Usuario { StatusId = 1, Nome = "Teste", Senha = "Teste" };
            usuario.Id = await _repository.Insert(usuario);
            usuario.Nome = "Alterado";
            usuario.Senha = "Alterado";
            await _repository.Update(usuario);
            usuarioBuscado = await _repository.Get(usuario.Id);

            await _repository.Delete(usuario.Id);

            Assert.IsFalse(
                usuario == null || usuario.Id != usuarioBuscado.Id || usuario.StatusId != usuarioBuscado.StatusId
                || usuario.Senha != usuarioBuscado.Senha || usuario.Nome != usuarioBuscado.Nome,
                "O Usuário não foi inserido corretamente."
            );
        }
    }
}
