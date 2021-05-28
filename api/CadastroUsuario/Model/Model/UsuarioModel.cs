using CadastroUsuario.Model.Entity;
using FluentValidation;
using System;

namespace CadastroUsuario.Model.Model
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public Status Status { get; set; }

        public bool Validar()
        {
            if (this.Nome == null || this.Nome == "" || this.Nome == " ")
                throw new Exception("Campo Nome é obrigatório.");
            if (this.Senha == null || this.Senha == "" || this.Senha == " ")
                throw new Exception("Campo Senha é obrigatório.");
            if (this.Status.Id == null || this.Status.Id == 0)
                throw new Exception("Campo Status é obrigatório.");

            return true;
        }

    }

    public class UsuarioValidator : AbstractValidator<UsuarioModel>
    {
        public UsuarioValidator()
        {
            RuleFor(p => p.Nome).NotNull().WithMessage("Campo Nome é obrigatório.");
            RuleFor(p => p.Nome).NotEmpty().WithMessage("Campo Nome Booking é obrigatório.");
            RuleFor(p => p.Senha).NotNull().WithMessage("Campo Senha Container é obrigatório.");
            RuleFor(p => p.Senha).NotEmpty().WithMessage("Campo Senha Container é obrigatório.");
            RuleFor(p => p.Status.Id).NotNull().WithMessage("Campo Status é obrigatório.");
        }
    }
}

