using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Net.Mail;
using Projeto_Academico.Business.Repository;
using Projeto_Academico.Controllers;
using Projeto_Academico.Models;
using System.Web.Mvc;
using System.Net;
using System.Web.Mvc.Filters;
using System.Drawing;
using System.IO;

namespace Projeto_Academico.Business
{
    public static class Util
    {
        public static Int32 gerarNumeroRandom(Int32 tamanho)
        {
            Int32 numero = 0;

            Random rand = new Random();
            if (tamanho > 0)
            {
                numero = rand.Next(0, tamanho);
            }
            return numero;
        }

        public static Guid gerarChave()
        {
            Guid chave = Guid.NewGuid();
            return chave;
        }

        public static byte[] imageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

    }
    public class Hash
    {
        private HashAlgorithm _algoritimo;

        public Hash(HashAlgorithm algoritimo)
        {
            _algoritimo = algoritimo;
        }

        public string GerarHash(string senha)
        {

            var valorCodificado = Encoding.UTF8.GetBytes(senha);
            var senhaCifrada = _algoritimo.ComputeHash(valorCodificado);
            var sb = new StringBuilder();
            foreach (var caractere in senhaCifrada)
            {
                sb.Append(caractere.ToString("X2"));
            }
            return sb.ToString();
        }

        public bool VerificarHash(string senhaDigitada, string senhaCadastrada)
        {
            if (string.IsNullOrEmpty(senhaCadastrada))
                throw new NullReferenceException("Cadastre uma senha.");

            var senhaCifrada = _algoritimo.ComputeHash(Encoding.UTF8.GetBytes(senhaDigitada));
            var sb = new StringBuilder();
            foreach (var caractere in senhaCifrada)
            {
                sb.Append(caractere.ToString("X2"));
            }
            return sb.ToString() == senhaCadastrada;
        }

    }
    public class Servico_Email
    {


        public Servico_Email()
        {
        }

        public static void Envio_Email(string pDestinatario, string pCC = null, string pAssunto = null, string tipoEmail = null)
        {
            UsuarioRepository userRep = new UsuarioRepository();
            var user = userRep.recuperaUsuarioEmSessao();

            string nomeRemetente = user.nome;
            string emailRemetente = user.email;
            string emailSMTP = "llucasgabriel@hotmail.com";
            string senhaSMTP = "Izachi@1994";
            string destinatario = pDestinatario;
            string smtp = "smtp-mail.outlook.com";
            string assunto = pAssunto;

            MailMessage email = new MailMessage();

            email.From = new MailAddress(nomeRemetente + "<" + emailSMTP + ">");
            email.To.Add(destinatario);
            //email.CC.Add(string.Empty);
            //email.Bcc.Add(string.Empty);
            email.Priority = MailPriority.Normal;
            email.IsBodyHtml = true;
            email.Subject = assunto;
            email.ReplyTo = new MailAddress(emailSMTP);
            email.Body = corpo_email(tipoEmail, nomeRemetente);
            email.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            email.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            SmtpClient objSmtp = new SmtpClient();

            objSmtp.Credentials = new System.Net.NetworkCredential(emailSMTP, senhaSMTP);
            objSmtp.EnableSsl = true;
            //objSmtp.TargetName = "smtp.gmail.com";
            objSmtp.Host = smtp;
            objSmtp.Port = 587;
            objSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                objSmtp.Send(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine("<script type='Javascript/Text'>alert('Ocorreram problemas no e-mail; Erro = " + ex.Message + "');</script>");
            }
            finally
            {
                email.Dispose();
            }
        }


        public void Email_Autentic(Usuario usuario)
        {
            string nomeRemetente = "Lucas Gabriel";
            string emailRemetente = "llucasgabriel@hotmail.com";
            string senhaRemetente = "Izachi@1994";
            string destinatario = usuario.email;
            string smtp = "smtp-mail.outlook.com";
            string assunto = "Gerencia de Projeto - Gerencia de Projetos";

            MailMessage email = new MailMessage();

            email.From = new MailAddress(nomeRemetente + "<" + emailRemetente + ">");
            email.To.Add(destinatario);
            //email.CC.Add(string.Empty);
            //email.Bcc.Add(string.Empty);
            email.Priority = MailPriority.Normal;
            email.IsBodyHtml = true;
            email.Subject = assunto;
            email.ReplyTo = new MailAddress(emailRemetente);
            email.Body = formulario_email(usuario);
            email.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            email.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");

            SmtpClient objSmtp = new SmtpClient();

            objSmtp.Credentials = new System.Net.NetworkCredential(emailRemetente, senhaRemetente);
            objSmtp.EnableSsl = true;
            //objSmtp.TargetName = "smtp.gmail.com";
            objSmtp.Host = smtp;
            objSmtp.Port = 587;
            objSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                objSmtp.Send(email);
                Console.WriteLine("<script type='Javascript/Text'>alert('E-mail enviado com sucesso!');</script>");
            }
            catch (Exception ex)
            {
                Console.WriteLine("<script type='Javascript/Text'>alert('Ocorreram problemas no e-mail; Erro = " + ex.Message + "');</script>");
            }
            finally
            {
                email.Dispose();
            }
        }

        public string formulario_email(Usuario usuario)
        {
            string codigo = string.Empty;
            UsuarioRepository repUser = new UsuarioRepository();
            codigo = repUser.AtivarContaUsuario(usuario);

            string body = string.Empty;
            string endereco = string.Empty;
            if (!string.IsNullOrEmpty(codigo))
            {
                endereco = "http://localhost:1356/Inicio/Confirmar?cod=" + codigo;
                body = @"
                        <h3>Por fins de segurança, clique no link abaixo para validar sua conta</h3>
                        <br />
                        <br />
                        " + endereco;
            }

            return body;
        }

        public static string corpo_email(string tipoEmail, string remetente = null)
        {
            string body = string.Empty;

            if (tipoEmail.Equals("Comum"))
            {

            }
            else if (tipoEmail.Equals("Convidar"))
            {
                UsuarioRepository repUser = new UsuarioRepository();

                var user = repUser.recuperaUsuarioEmSessao();

                string codigo = user.empresa.codEmpresa.ToString();

                string endereço = "http://localhost:1356/Inicio/Cadastro?codEmp=" + codigo;

                body = @"
                    <h2>" + remetente + @" convidou você para participar do SGP</h2>
                    <br />
                    <h4>Clique no link abaixo para se registrar</h4>
                    <br />" + endereço;
            }

            return body;
        }
    }

    #region class Session
    public class SessionBase
    {
        protected static T Ler<T>(string nomeSession)
        {
            return (T)HttpContext.Current.Session[nomeSession];
        }

        protected static T LerComDefault<T>(string nomeSession)
        {
            object valor = HttpContext.Current.Session[nomeSession];
            return valor == null ? default(T) : ((T)valor);
        }

        protected static void Atualizar(string nomeSession, object valor)
        {
            HttpContext.Current.Session[nomeSession] = valor;
        }

        protected static void Abandon()
        {
            HttpContext.Current.Session.Abandon();
        }

        protected static void RemoveAll()
        {
            HttpContext.Current.Session.RemoveAll();
        }
    }

    public class SessionControl : SessionBase
    {
        private const string UserSessionName = "Session do Nome de Usuario";
        private const string UserSessionCargo = "Session do cargo de Usuario";
        private const string UserSessionId = "Session do Id de Usuario";
        private const string ProjectSessionId = "Session do Id do projeto atual";
        private const string TaskSessionId = "Session do Id da tarefa atual";

        public static string UserName
        {
            get
            {
                return LerComDefault<string>(UserSessionName);
            }
            set
            {
                Atualizar(UserSessionName, value);
            }
        }
        public static long Id
        {
            get
            {
                return LerComDefault<Int64>(UserSessionId);
            }
            set
            {
                Atualizar(UserSessionId, value);
            }
        }
        public static string Cargo
        {
            get
            {
                return LerComDefault<string>(UserSessionCargo);
            }
            set
            {
                Atualizar(UserSessionCargo, value);
            }
        }

        public static string ProjetoId
        {
            get
            {
                return LerComDefault<string>(ProjectSessionId);
            }
            set
            {
                Atualizar(ProjectSessionId, value);
            }
        }

        public static string TarefaId
        {
            get
            {
                return LerComDefault<string>(TaskSessionId);
            }
            set
            {
                Atualizar(TaskSessionId, value);
            }
        }

        public static void AtualizaSession(string pNomeSession, object pValor)
        {
            Atualizar(pNomeSession, pValor);
        }

        public static void PopulaSessao(Usuario usuario)
        {
            SessionControl.Id = Convert.ToInt64(usuario.id);
            SessionControl.UserName = usuario.apelido;
            SessionControl.Cargo = usuario.cargo;
        }

        public static void FimSession()
        {
            RemoveAll();
        }

        public static bool VerificaSession(string chave)
        {
            if (!string.IsNullOrEmpty(chave))
                return true;
            else
                return false;
        }

        public static bool VerificaSession(HttpContextBase contexto)
        {
            if (contexto.Session["UserSessionName"] != null)
                return true;
            else
                return false;
        }

        public static bool VerificaPermissao(string nome, string controller, long id)
        {
            TelaRepository repTela = new TelaRepository();
            return repTela.VerificaPermissoesTelas(nome, controller, id);
        }

    }
    public class MyAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (SessionControl.VerificaSession(SessionControl.UserName) == true)
                return true;
            else
                return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (SessionControl.VerificaSession(SessionControl.UserName) == false)
            {
                filterContext.Result = new RedirectToRouteResult(
                        new System.Web.Routing.RouteValueDictionary
                    {
                            {"action", "Index" },
                            {"controller", "Login" }
                    });
            }
            else
                base.HandleUnauthorizedRequest(filterContext);
        }
    }
    #endregion

    public class Cenario
    {
        public int id { get; set; }
        public string nome { get; set; }

        public static List<Cenario> listCenario()
        {
            var lista = new List<Cenario>
            {
                new Cenario(){ id = 0, nome = "Certeza" },
                new Cenario(){ id = 1, nome = "Incerteza"}
            };
            return lista; 
        }

    }





}