using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Net;
using UserManager.Contracts.Responses;
using UserManager.Model;
using UserManager.Shared.Interface;

namespace UserManager.Shared
{
    public class EmailService : IEmailService
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly IConfiguration _configuration;

        public EmailService(UserManager<CustomUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        private readonly string smtpServer = "smtp.gmail.com";
        private readonly int smtpPort = 587;


        public async Task<ResponseApi> ConfirmEmail(int userid, string token)
        {
            var user = await _userManager.FindByIdAsync(userid.ToString());

            if (user == null)
                return new ResponseApi(false, "Usuário não localizado.");

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                return new ResponseApi(false, "Falha ao confirmar o e-mail do usuário.");
            }

            return new ResponseApi(false, "E-mail confirmado com sucesso.");
        }

        public async Task<ResponseApi> SendAccountConfirmationEmail(int userid, string nome, string token, string email)
        {
            var smtpUsername = _configuration["SmtpSettings:Username"];
            var smtpPassword = _configuration["SmtpSettings:Password"];

            var host = _configuration["EmailSettings:BaseUrl"];

            var uri = $"{host}/account/confirmacao?userid={userid}&token={token}";

            var body = $"Olá {nome}! \nPara confirmar a criação de sua conta, copie a URL abaixo e cole no navegador: \n{uri}";

            try
            {
                using (SmtpClient client = new SmtpClient(smtpServer, smtpPort))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    client.EnableSsl = true;

                    MailMessage mensagem = new MailMessage();
                    mensagem.From = new MailAddress(smtpUsername);
                    mensagem.To.Add(email);
                    mensagem.Subject = "Madenn - Confirmação de criação de conta";
                    mensagem.Body = body;
                    mensagem.IsBodyHtml = false;

                    await client.SendMailAsync(mensagem);
                }
            }
            catch (Exception ex)
            {
                return new ResponseApi(false, "Erro ao enviar email: " + ex.Message + ex.StackTrace);
            }

            return new ResponseApi(true, "Email enviado com sucesso");
        }
    }
}
