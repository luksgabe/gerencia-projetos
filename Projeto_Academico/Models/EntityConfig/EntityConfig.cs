using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Projeto_Academico.Models.EntityConfig
{
    public class EmpresaConfiguration : EntityTypeConfiguration<Empresa>
    {
        public EmpresaConfiguration()
        {
            ToTable("Empresa");
            HasKey(e => e.id).Property(e => e.id).HasColumnName("id_empresa").IsOptional().HasColumnAnnotation(
                "Index", new IndexAnnotation(new IndexAttribute("unique_guid_empresa") { IsUnique = true }));
            Property(e => e.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(e => e.nome).HasMaxLength(50);
            Property(e => e.nomeGerente).HasMaxLength(30);

        }
    }
    public class UsuarioConfiguration : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfiguration()
        {
            ToTable("Usuarios");
            HasKey(x => x.id).Property(u => u.id).HasColumnName("id_usuario").IsOptional().HasColumnAnnotation(
                "Index", new IndexAnnotation(new IndexAttribute("unique_guid_usuario") { IsUnique = true }));
            Property(e => e.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(u => u.cargo).HasMaxLength(30);
            HasRequired(u => u.grupoUsuario).WithMany(u => u.listaUsuario).Map(m => m.MapKey("cod_grupoUsuario"));
            HasRequired(u => u.empresa).WithMany(u => u.listaUsuario).Map(m => m.MapKey("cod_empresa"));
            HasRequired(u => u.uploadImage).WithRequiredPrincipal();
            HasMany(u => u.listaProjetosEnvolvidos).WithMany(p => p.listaUsuariosEnvolvidos).
                Map(m =>
                {
                    m.MapLeftKey("cd_projeto");
                    m.MapRightKey("cd_usuario");
                    m.ToTable("UsuarioProjeto");
                });
        }
    }

    public class AtivacaoConfiguration : EntityTypeConfiguration<Ativacao>
    {
        public AtivacaoConfiguration()
        {
            ToTable("Ativacaos");
            HasKey(x => x.id).Property(u => u.id).HasColumnName("id_ativacao"); ;
            HasRequired(x => x.usuario).WithRequiredPrincipal();
        }
    }

    public class GrupoUsuarioConfiguration : EntityTypeConfiguration<GrupoUsuario>
    {
        public GrupoUsuarioConfiguration()
        {
            ToTable("GrupoUsuario");
            HasKey(g => g.id).Property(g => g.id).HasColumnName("id_grupo_usuario").IsOptional().HasColumnAnnotation(
                "Index", new IndexAnnotation(new IndexAttribute("unique_guid_grupo_usuario") { IsUnique = true }));
            Property(g => g.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(g => g.nome).HasMaxLength(50);
            HasMany(g => g.listaPermissoesTela).WithMany(t => t.gruposPermitidos).
                Map(m =>
                {
                    m.MapLeftKey("id_tela");
                    m.MapRightKey("id_grupo_usuario");
                    m.ToTable("Permissao");
                });
            Property(g => g.empresaID).HasColumnName("cod_empresa").IsRequired();
            

        }
    }

    public class TelaConfiguration : EntityTypeConfiguration<Tela>
    {
        public TelaConfiguration()
        {
            ToTable("Tela");

            HasKey(t => t.id);
            Property(u => u.id).HasColumnName("id_tela").IsOptional().HasColumnAnnotation(
                "Index", new IndexAnnotation(new IndexAttribute("unique_guid_tela") { IsUnique = true })); ;
            Property(g => g.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(u => u.nome).HasColumnName("nome").HasMaxLength(60).IsRequired();
            Property(u => u.controller).HasColumnName("controll").HasMaxLength(60).IsRequired();
        }
    }

    public class UploadFileConfiguration : EntityTypeConfiguration<UploadFile>
    {
        public UploadFileConfiguration()
        {
            ToTable("UploadFile");

            HasKey(u => u.id);
            Property(u => u.id).HasColumnName("id_uploadFile").IsOptional().HasColumnAnnotation(
                "Index", new IndexAnnotation(new IndexAttribute("unique_guid_uploadFile") { IsUnique = true })); ;
            Property(u => u.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }

    public class ClienteConfiguration : EntityTypeConfiguration<Cliente>
    {
        public ClienteConfiguration()
        {
            ToTable("Cliente");

            HasKey(c => c.id);
            Property(c => c.id).HasColumnName("id_cliente").IsOptional().HasColumnAnnotation(
                "Index", new IndexAnnotation(new IndexAttribute("unique_guid_cliente") { IsUnique = true })); ;
            Property(c => c.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(e => e.empresa).WithMany(e => e.listaCliente).Map(m => m.MapKey("cod_empresa"));
        }
    }

    public class ProjetoConfiguration : EntityTypeConfiguration<Projeto>
    {
        public ProjetoConfiguration()
        {
            ToTable("Projeto");

            HasKey(p => p.id);
            Property(p => p.id).HasColumnName("id_projeto").IsOptional().HasColumnAnnotation(
                "Index", new IndexAnnotation(new IndexAttribute("unique_guid_projeto") { IsUnique = true }));
            Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(p => p.cliente).WithMany(c => c.listaProjeto).Map(m => m.MapKey("cod_cliente"));
            Property(p => p.responsavelProjeto).HasColumnName("responsavel_projeto").IsRequired();
        }
    }

    public class FilaConfiguration : EntityTypeConfiguration<Fila>
    {
        public FilaConfiguration()
        {
            ToTable("Fila");

            HasKey(f => f.id);
            Property(f => f.id).HasColumnName("id_fila").IsOptional().HasColumnAnnotation(
                "Index", new IndexAnnotation(new IndexAttribute("unique_guid_fila") { IsUnique = true }));
            Property(f => f.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(f => f.projeto).WithMany(p => p.listaFilas).Map(m => m.MapKey("cod_projeto"));

        }
    }

    public class ProcessoConfiguration : EntityTypeConfiguration<Processo>
    {
        public ProcessoConfiguration()
        {
            ToTable("Processo");

            HasKey(p => p.id);
            Property(p => p.id).HasColumnName("id_processo").IsOptional().HasColumnAnnotation(
                "Index", new IndexAnnotation(new IndexAttribute("unique_guid_processo") { IsUnique = true }));
            Property(p => p.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(p => p.projeto).WithMany(p => p.listaProcessos).Map(m => m.MapKey("cod_projeto"));
        }
    }
    public class TarefaConfiguration : EntityTypeConfiguration<Tarefa>
    {
        public TarefaConfiguration()
        {
            ToTable("Tarefa");

            HasKey(t => t.id);
            Property(t => t.id).HasColumnName("id_tarefa").IsRequired().HasColumnAnnotation(
                "Index", new IndexAnnotation(new IndexAttribute("unique_guid_tarefa") { IsUnique = true }));
            HasRequired(t => t.fila).WithMany(f => f.listaTarefas).Map(m => m.MapKey("cod_fila"));
            HasRequired(p => p.processo).WithMany(p => p.listaTarefas).Map(m => m.MapKey("cod_processo"));
            Property(t => t.dataInicio).HasColumnName("data_inicio").IsRequired();
            Property(t => t.dataEntrega).HasColumnName("data_fim").IsRequired();
            Property(t => t.tempoTotal).HasColumnName("tempo_total").IsRequired();
            HasMany(t => t.listaUsuarios).WithMany(u => u.listaTarefas).
                Map(m =>
                {
                    m.MapLeftKey("cod_usuario");
                    m.MapRightKey("cod_tarefa");
                    m.ToTable("UsuarioTarefa");
                });
        }
    }

    public class ObservacoesConfiguration : EntityTypeConfiguration<Observacoes>
    {
        public ObservacoesConfiguration()
        {
            ToTable("Observacoes");
            HasKey(t => t.id).Property(t => t.id).HasColumnName("id_observacao").IsRequired().HasColumnAnnotation(
                "Index", new IndexAnnotation(new IndexAttribute("unique_guid_observacoes") { IsUnique = true }));
            HasRequired(o => o.usuario).WithMany(t => t.listaObservacoes).Map(m => m.MapKey("cod_usuario"));
            HasRequired(o => o.tarefa).WithMany(t => t.listaObservacoes).Map(m => m.MapKey("cod_tarefa"));
            Property(o => o.dataCriacao).HasColumnName("data_criacao").IsRequired();

        }
    }
}