using Projeto_Academico.Business.Repositorys;
using Projeto_Academico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Academico.Business.Interfaces
{
    public interface IUploadFileRepository : IRepository<UploadFile>
    {

        UploadFile recuperaArquivo(string caminho);

    }
}
