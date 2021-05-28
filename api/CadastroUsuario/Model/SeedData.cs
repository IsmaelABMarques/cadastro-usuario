using CadastroUsuario.Model.Entity;
using CadastroUsuario.Model.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CadastroUsuario.Model
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RepositoryBase(
                serviceProvider.GetRequiredService<
                    DbContextOptions<RepositoryBase>>()))
            {
                if (!context.Status.Any())
                {
                    context.Status.AddRange(
                    new Status
                    {
                        Descricao = "Ativo"
                    }
                    );

                    context.Status.AddRange(
                    new Status
                    {
                        Descricao = "Inativo"
                    }
                    );

                    context.SaveChanges();
                }

                if (!context.Usuario.Any())
                {
                    context.Usuario.AddRange(
                    new Usuario
                    {
                        Nome = "teste",
                        Senha= "Teste",
                        StatusId = 1
                    }
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}
