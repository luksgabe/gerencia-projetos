using System;
using System.Collections.Generic;
using System.Data.Entity;
using Projeto_Academico.Models.EntityConfig;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Projeto_Academico.Models
{
    public class Projeto_AcademicoContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public Projeto_AcademicoContext() : base("name=Projeto_AcademicoContext")
        {
            //Configuration.LazyLoadingEnabled = false;
            //Configuration.ProxyCreationEnabled = false;

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Properties<decimal>().Configure(config => config.HasPrecision(10, 2));

            modelBuilder.Configurations.Add(new EmpresaConfiguration());
            modelBuilder.Configurations.Add(new GrupoUsuarioConfiguration());
            modelBuilder.Configurations.Add(new TelaConfiguration());
            modelBuilder.Configurations.Add(new UploadFileConfiguration());
            modelBuilder.Configurations.Add(new ClienteConfiguration());
            modelBuilder.Configurations.Add(new ProjetoConfiguration());
            modelBuilder.Configurations.Add(new FilaConfiguration());
            modelBuilder.Configurations.Add(new ProcessoConfiguration());
            modelBuilder.Configurations.Add(new TarefaConfiguration());
            modelBuilder.Configurations.Add(new ObservacoesConfiguration());
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Ativacao> Ativacao { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<GrupoUsuario> GrupoUsuario { get; set; }
        public DbSet<Tela> Tela { get; set; }
        public DbSet<UploadFile> UploadFile { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Projeto> Projeto { get; set; }
        public DbSet<Fila> Fila { get; set; }
        public DbSet<Processo> Processo { get; set; }
        public DbSet<Tarefa> Tarefa { get; set; }
        public DbSet<Observacoes> Observacoes { get; set; }
    }
}
