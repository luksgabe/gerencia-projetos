using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Projeto_Academico.Models;
using Projeto_Academico.Business.Interfaces;
using System.Data.Entity;

namespace Projeto_Academico.Business.Repository
{
    public class TelaRepository : Repository<Tela>, ITelaRepository
    {
        public IEnumerable<Tela> registrosTela()
        {
            return GetAll();
        }

        public IEnumerable<GrupoUsuario> registrosGrupo()
        {
            return Db.GrupoUsuario.ToList();
        }

        public void VerificaNovasTelas(GrupoUsuario perfil)
        {
            Projeto_AcademicoContext db = new Projeto_AcademicoContext();

            var tela = Db.Tela.ToList();
            int pCount = perfil.listaPermissoesTela.Count;
            if (perfil.nome.Equals("Administrador"))
            {
                foreach (var item in tela)
                {
                    var lista = perfil.listaPermissoesTela.Where(p => p.nome.Equals(item.nome) && p.controller.Equals(item.controller)).ToList();

                    if (lista.Count <= 0 || lista == null)
                    {
                        perfil.listaPermissoesTela.Add(item);
                    }
                }
                if (perfil.listaPermissoesTela.Count > pCount)
                {
                    //db.GrupoUsuario.Attach(perfil);
                    db.Entry(perfil).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
        
        public bool VerificaPermissoesTelas(string action, string controller, long pId)
        {
            var usuario = Db.Usuarios.FirstOrDefault(u => u.id == pId);
            if (usuario != null)
            {
                var perfil = registrosGrupo().FirstOrDefault(p => p.id.Equals(usuario.grupoUsuario.id));
                var tela = registrosTela().FirstOrDefault(t => t.nome.Equals(action) && t.controller.Equals(controller));
                var permissoes = tela.gruposPermitidos.FirstOrDefault(p => p.id.Equals(perfil.id));
                return permissoes != null;
            }
            else
            {
                return false;
            }
        }
    }
}