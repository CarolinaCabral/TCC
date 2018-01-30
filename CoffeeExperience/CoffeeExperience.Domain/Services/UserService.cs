using CoffeeExperience.Domain.Entities;
using CoffeeExperience.Domain.Interfaces.Repositories;
using CoffeeExperience.Domain.Interfaces.Services;
using CoffeeExperience.Domain.TO;
using CoffeeExperience.Infra.CrossCutting.System;
using CoffeeExperience.Infra.CrossCutting.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeExperience.Domain.Services
{
    public class UserService : GenericService<User>, IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
            : base(unitOfWork.UserRepository)
        {
            this.unitOfWork = unitOfWork;
        }

       
        public User Validate(string email, string password)
        {
            try
            {
                if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    throw new Exception("E-mail e senha não podem ser vázios!");
                }
                User usuario = this.unitOfWork.UserRepository.Validate(email, password.ToCriptografa());

                if(usuario == null)
                {
                    throw new Exception("E-mail e/ou senha incorretos!");
                }

                return usuario;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            
        }
        
        public Response Save(User user)
        {
            if (user.Id > 0)
            {
                User usuarioExistente = unitOfWork.UserRepository.Get(x => x.Id != user.Id && (x.Name == user.Name || x.Email == user.Email));

                if(usuarioExistente != null)
                {
                    return new Response(true, "já existe um usuário cadastrado com este nome e/ou e-mail");
                }
                else
                {
                    unitOfWork.UserRepository.Update(user);
                }
            }
            else
            {

                User usuarioExistente = unitOfWork.UserRepository.Get(x => x.Name == user.Name || x.Email == user.Email);
                if (usuarioExistente != null)
                {
                    return new Response(true, "já existe um usuário cadastrado com este nome e/ou e-mail");
                }
                else
                {
                    user.CripgraphyPassword();
                    unitOfWork.UserRepository.Add(user);
                }
            }
            unitOfWork.Commit();

            StringBuilder mensagem = new StringBuilder();
            mensagem.Append("<h1> Bem-Vindo a " + ConfigurationManager.AppSettings["NameApp"].ToString() + "</h1>");
            mensagem.Append("Olá, " + user.Name + "<br>");
            mensagem.Append("<p>Acesse nosso site <a href='www.coffeexps.com'>www.coffeexps.com</a></p>");
            mensagem.Append("<p>Segue seus dados de acesso:</p>");
            mensagem.Append("<p>E-mail: " + user.Email + "</p>");
            mensagem.Append("<p>Senha: " + user.Password.ToDescriptografa() + "</p>");

            EmailSender.EnviarEmail(null, null, user.Name, user.Email, "CE Bem-Vindo", mensagem.ToString(), true);
            return new Response(false, "Usuário cadastrado com sucesso!");
        }
        
        public void SetLastAccess(User user)
        {
            user.SetLastAccess();
            unitOfWork.UserRepository.Update(user);
            unitOfWork.Commit();
        }

        public bool RememberPassword(string email)
        {
            User user = unitOfWork.UserRepository.Get(p => p.Email == email.ToLower());
            if (user != null)
            {
                StringBuilder mensagem = new StringBuilder();
                mensagem.Append("<h1> Lembrar senha " + ConfigurationManager.AppSettings["NameApp"].ToString() + "</h1>");
                mensagem.Append("Olá, " + user.Name + "<br>");
                mensagem.Append("<p>Esqueceu sua senha? Isso acontece com todos!</p>");
                mensagem.Append("<p>Segue seus dados de acesso:</p>");
                mensagem.Append("<p>E-mail: " + user.Email + "</p>");
                mensagem.Append("<p>Senha: " + user.Password.ToDescriptografa() + "</p>");
                EmailSender.EnviarEmail(null,null, user.Name, user.Email, "[Coffee Experience] Lembrar Senha", mensagem.ToString(), true);

                return true;
            }
            return false;
        }
    }
}

