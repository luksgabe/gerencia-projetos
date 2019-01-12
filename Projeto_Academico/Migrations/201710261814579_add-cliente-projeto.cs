namespace Projeto_Academico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addclienteprojeto : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.GrupoUsuario", name: "empresa_id", newName: "cod_empresa");
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        id_cliente = c.Long(nullable: false, identity: true),
                        nome = c.String(),
                        areaAtuacao = c.String(),
                        cod_empresa = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.id_cliente)
                .ForeignKey("dbo.Empresa", t => t.cod_empresa)
                .Index(t => t.id_cliente, unique: true, name: "unique_guid_uploadTela")
                .Index(t => t.cod_empresa);
            
            CreateTable(
                "dbo.Projeto",
                c => new
                    {
                        id_projeto = c.Long(nullable: false, identity: true),
                        nome = c.String(),
                        responsavel_projeto = c.String(nullable: false),
                        cod_cliente = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.id_projeto)
                .ForeignKey("dbo.Cliente", t => t.cod_cliente)
                .Index(t => t.id_projeto, unique: true, name: "unique_guid_uploadTela")
                .Index(t => t.cod_cliente);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projeto", "cod_cliente", "dbo.Cliente");
            DropForeignKey("dbo.Cliente", "cod_empresa", "dbo.Empresa");
            DropIndex("dbo.Projeto", new[] { "cod_cliente" });
            DropIndex("dbo.Projeto", "unique_guid_uploadTela");
            DropIndex("dbo.Cliente", new[] { "cod_empresa" });
            DropIndex("dbo.Cliente", "unique_guid_uploadTela");
            DropTable("dbo.Projeto");
            DropTable("dbo.Cliente");
            RenameColumn(table: "dbo.GrupoUsuario", name: "cod_empresa", newName: "empresa_id");
        }
    }
}
