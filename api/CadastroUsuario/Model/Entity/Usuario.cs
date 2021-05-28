using System;
using System.Collections.Generic;

namespace CadastroUsuario.Model.Entity
{
    public class Usuario : EntidadeIdentificavel
    {
        public int StatusId { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public DateTime DataCriacao { get; set; }
        public Status Status { get; set; }
    }

    public class Teste
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public DateTime DataCriacao { get; set; }
        public Status Status { get; set; }

    }

    public class Status : EntidadeIdentificavel
    { 
        public string Descricao { get; set; }
        public IEnumerable<Usuario> Usuarios { get; set; }
    }
}
