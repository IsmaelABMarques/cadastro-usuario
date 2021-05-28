using AutoMapper;
using CadastroUsuario.Model.Entity;
using CadastroUsuario.Model.Model;
using CadastroUsuario.Model.Repository;
using CadastroUsuario.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroUsuario.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [EnableCors("Policy2")]
    public class UsuarioController
    {
        private readonly IRepositoryService<Usuario> _repository;
        private readonly IRepositoryService<Status> _statusRepository;
        private UsuarioValidator _validator;
        private readonly IMapper _mapper;

        public UsuarioController(UsuarioRepository repositorio, StatusRepository status, UsuarioValidator validator, IMapper mapper)
        {
            _repository = repositorio;
            _statusRepository = status;
            _validator = validator;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Usuario/Status")]
        public async Task<IEnumerable<Status>> BuscarStatus()
        {
            var result = await _statusRepository.GetAll(null);

            return result;
        }

        [HttpPost]
        [Route("Usuario/Buscar")]
        public IEnumerable<UsuarioModel> Buscar([Bind("Id,Nome,Status")]  UsuarioModel usuario)
        { 
            var result =  _repository.GetAll(_mapper.Map<Usuario>(usuario)).Result;

            return _mapper.Map<IEnumerable<UsuarioModel>>(result);
        }

        [HttpGet]
        [Route("Usuario/Buscar")]
        public async Task<UsuarioModel> Buscar(int id)
        {
            var result = await _repository.Get(id);

            return _mapper.Map<UsuarioModel>(result);
        }

        [HttpPost]
        [Route("Usuario/Gravar")]
        public async Task<int> Gravar([Bind("Id,Nome,Senha,Status")]  UsuarioModel usuarioModel)
        {
            usuarioModel.Validar();
            var usuario = _mapper.Map<Usuario>(usuarioModel);

            if (usuario.Id == 0)
                usuario.Id = await _repository.Insert(usuario);
            else
                await _repository.Update(usuario);

            return usuario.Id;
        }

        [HttpGet]
        [Route("Usuario/Excluir")]
        public async Task<bool> Excluir(int id)
        {
            await _repository.Delete(id);

            return true;
        }
    }
}
