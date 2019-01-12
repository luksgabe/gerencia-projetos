using Projeto_Academico.Business.Interfaces;
using Projeto_Academico.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace Projeto_Academico.Business.Repository
{
    public class UploadFileRepository : Repository<UploadFile>, IUploadFileRepository
    {
        public UploadFile recuperaArquivo(string caminho)
        {
            return GetAll().FirstOrDefault(u => u.caminhoServidor.Equals(caminho));
        }


        public string BuscarArquivoPorCaminho(string caminho = null)
        {
            if (!string.IsNullOrEmpty(caminho))
            {
                var arquivo = recuperaArquivo(caminho);
                if (arquivo != null)
                {
                    return arquivo.caminhoServidor;
                }
                else
                {
                    return "/Content/Imagens/userDefault.png";
                }
            }
            else
            {
                return "/Content/Imagens/userDefault.png";
            }
        }

        public UploadFile SalvaImagemUsuario(string uploadPath, string caminhoArquivo, string caminhoArquivoServidor, string filename, string contentType, int contentLenght, long codUser = 0)
        {
            UploadFile uploadFile = new UploadFile();
            UsuarioRepository repUser = new UsuarioRepository();
            UploadFileRepository repUpd = new UploadFileRepository();

            uploadFile.caminho = caminhoArquivo;
            uploadFile.caminhoServidor = caminhoArquivoServidor;
            uploadFile.nome = Path.GetFileName(filename);
            uploadFile.tipo = Path.GetFileName(contentType);
            uploadFile.tamanho = 0;
            uploadFile.peso = contentLenght;

            var result = repUpd.GetAll().FirstOrDefault(p => p.caminhoServidor.Equals(uploadFile.caminhoServidor));

            if (result == null)
            {
                //var user = repUser.GetById((int)codUser);
                var user = Db.Usuarios.FirstOrDefault(u => u.id == codUser);
                if (user != null)
                {
                    if (user.uploadImage != null)
                    {
                        var imagem = user.uploadImage;

                        user.uploadImage = null;
                        Db.Entry(user).State = EntityState.Modified;
                        Db.SaveChanges();

                        Db.UploadFile.Remove(imagem);
                        Db.SaveChanges();

                    }
                }

                Db.UploadFile.Add(uploadFile);
                Db.SaveChanges();
            }
            else
            {
                uploadFile = result;
            }

            return uploadFile;
        }

    }
}