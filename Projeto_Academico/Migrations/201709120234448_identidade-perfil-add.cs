namespace Projeto_Academico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class identidadeperfiladd : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Ativacaos", newName: "Ativacao");
            RenameTable(name: "dbo.Usuarios", newName: "Usuario");
            CreateTable(
                "dbo.Empresa",
                c => new
                    {
                        id_empresa = c.Long(nullable: false, identity: true),
                        nome = c.String(maxLength: 50),
                        nomeGerente = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.id_empresa)
                .Index(t => t.id_empresa, unique: true, name: "unique_guid_empresa");
            
            CreateTable(
                "dbo.GrupoUsuario",
                c => new
                    {
                        id_grupo_usuario = c.Long(nullable: false, identity: true),
                        nome = c.String(maxLength: 50),
                        ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id_grupo_usuario)
                .Index(t => t.id_grupo_usuario, unique: true, name: "unique_guid_grupo_usuario");
            
            CreateTable(
                "dbo.Tela",
                c => new
                    {
                        id_tela = c.Long(nullable: false, identity: true),
                        controll = c.String(nullable: false, maxLength: 60),
                        nome = c.String(nullable: false, maxLength: 60),
                    })
                .PrimaryKey(t => t.id_tela)
                .Index(t => t.id_tela, unique: true, name: "unique_guid_tela");
            
            CreateTable(
                "dbo.Permissao",
                c => new
                    {
                        id_tela = c.Long(nullable: false),
                        id_grupo_usuario = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.id_tela, t.id_grupo_usuario })
                .ForeignKey("dbo.GrupoUsuario", t => t.id_tela)
                .ForeignKey("dbo.Tela", t => t.id_grupo_usuario)
                .Index(t => t.id_tela)
                .Index(t => t.id_grupo_usuario);
            
            AddColumn("dbo.Usuario", "ativo", c => c.Boolean(nullable: false));
            AddColumn("dbo.Usuario", "empresa_id", c => c.Long());
            AddColumn("dbo.Usuario", "grupoUsuario_id", c => c.Long());
            CreateIndex("dbo.Usuario", "empresa_id");
            CreateIndex("dbo.Usuario", "grupoUsuario_id");
            AddForeignKey("dbo.Usuario", "empresa_id", "dbo.Empresa", "id_empresa");
            AddForeignKey("dbo.Usuario", "grupoUsuario_id", "dbo.GrupoUsuario", "id_grupo_usuario");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuario", "grupoUsuario_id", "dbo.GrupoUsuario");
            DropForeignKey("dbo.Permissao", "id_grupo_usuario", "dbo.Tela");
            DropForeignKey("dbo.Permissao", "id_tela", "dbo.GrupoUsuario");
            DropForeignKey("dbo.Usuario", "empresa_id", "dbo.Empresa");
            DropIndex("dbo.Permissao", new[] { "id_grupo_usuario" });
            DropIndex("dbo.Permissao", new[] { "id_tela" });
            DropIndex("dbo.Tela", "unique_guid_tela");
            DropIndex("dbo.GrupoUsuario", "unique_guid_grupo_usuario");
            DropIndex("dbo.Empresa", "unique_guid_empresa");
            DropIndex("dbo.Usuario", new[] { "grupoUsuario_id" });
            DropIndex("dbo.Usuario", new[] { "empresa_id" });
            DropColumn("dbo.Usuario", "grupoUsuario_id");
            DropColumn("dbo.Usuario", "empresa_id");
            DropColumn("dbo.Usuario", "ativo");
            DropTable("dbo.Permissao");
            DropTable("dbo.Tela");
            DropTable("dbo.GrupoUsuario");
            DropTable("dbo.Empresa");
            RenameTable(name: "dbo.Usuario", newName: "Usuarios");
            RenameTable(name: "dbo.Ativacao", newName: "Ativacaos");
        }
    }
}
