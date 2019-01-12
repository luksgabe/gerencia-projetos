using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Projeto_Academico;
using Projeto_Academico.Models;
using System.Data.Entity;

namespace Projeto_Academico.App_Start
{
    public class AppConfig
    {

        public static void StartaTelas()
        {
            Projeto_AcademicoContext db = new Projeto_AcademicoContext();
            Tela tela = null;

            string perfilNome = "Administrador";
            var grupo = db.GrupoUsuario.FirstOrDefault(g => g.nome.Equals(perfilNome));

            var telas = db.Tela.ToList();


            foreach (var item in telas)
            {
                var result = item.gruposPermitidos.Where(t => t.id == grupo.id).ToList();
                if (result.Count <= 0 || result == null)
                {
                    item.gruposPermitidos.Add(grupo);
                    tela = item;
                }
            }
            if (tela != null)
            {
                db.Entry(tela).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}