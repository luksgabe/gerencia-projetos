namespace Projeto_Academico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfilaprocessotarefa : DbMigration
    {
        public override void Up()
        {
            RenameIndex(table: "dbo.Cliente", name: "unique_guid_uploadTela", newName: "unique_guid_cliente");
            RenameIndex(table: "dbo.Projeto", name: "unique_guid_uploadTela", newName: "unique_guid_projeto");
            RenameIndex(table: "dbo.UploadFile", name: "unique_guid_uploadTela", newName: "unique_guid_uploadFile");
            CreateTable(
                "dbo.Fila",
                c => new
                    {
                        id_fila = c.Long(nullable: false, identity: true),
                        nome = c.String(),
                        ativo = c.Boolean(nullable: false),
                        cod_projeto = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.id_fila)
                .ForeignKey("dbo.Projeto", t => t.cod_projeto)
                .Index(t => t.id_fila, unique: true, name: "unique_guid_fila")
                .Index(t => t.cod_projeto);
            
            CreateTable(
                "dbo.Tarefa",
                c => new
                    {
                        id_tarefa = c.Long(nullable: false, identity: true),
                        nome = c.String(),
                        descricao = c.String(),
                        data_inicio = c.DateTime(nullable: false),
                        data_fim = c.DateTime(nullable: false),
                        tempo_total = c.String(nullable: false),
                        ativo = c.Boolean(nullable: false),
                        cod_fila = c.Long(nullable: false),
                        cod_processo = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.id_tarefa)
                .ForeignKey("dbo.Fila", t => t.cod_fila)
                .ForeignKey("dbo.Processo", t => t.cod_processo)
                .Index(t => t.id_tarefa, unique: true, name: "unique_guid_tarefa")
                .Index(t => t.cod_fila)
                .Index(t => t.cod_processo);
            
            CreateTable(
                "dbo.Processo",
                c => new
                    {
                        id_processo = c.Long(nullable: false, identity: true),
                        nome = c.String(),
                        ativo = c.Boolean(nullable: false),
                        cod_projeto = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.id_processo)
                .ForeignKey("dbo.Projeto", t => t.cod_projeto)
                .Index(t => t.id_processo, unique: true, name: "unique_guid_processo")
                .Index(t => t.cod_projeto);
            
            AddColumn("dbo.Usuario", "Tarefa_id", c => c.Long());
            AddColumn("dbo.UploadFile", "Tarefa_id", c => c.Long());
            CreateIndex("dbo.Usuario", "Tarefa_id");
            CreateIndex("dbo.UploadFile", "Tarefa_id");
            AddForeignKey("dbo.UploadFile", "Tarefa_id", "dbo.Tarefa", "id_tarefa");
            AddForeignKey("dbo.Usuario", "Tarefa_id", "dbo.Tarefa", "id_tarefa");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fila", "cod_projeto", "dbo.Projeto");
            DropForeignKey("dbo.Tarefa", "cod_processo", "dbo.Processo");
            DropForeignKey("dbo.Processo", "cod_projeto", "dbo.Projeto");
            DropForeignKey("dbo.Usuario", "Tarefa_id", "dbo.Tarefa");
            DropForeignKey("dbo.UploadFile", "Tarefa_id", "dbo.Tarefa");
            DropForeignKey("dbo.Tarefa", "cod_fila", "dbo.Fila");
            DropIndex("dbo.Processo", new[] { "cod_projeto" });
            DropIndex("dbo.Processo", "unique_guid_processo");
            DropIndex("dbo.UploadFile", new[] { "Tarefa_id" });
            DropIndex("dbo.Tarefa", new[] { "cod_processo" });
            DropIndex("dbo.Tarefa", new[] { "cod_fila" });
            DropIndex("dbo.Tarefa", "unique_guid_tarefa");
            DropIndex("dbo.Fila", new[] { "cod_projeto" });
            DropIndex("dbo.Fila", "unique_guid_fila");
            DropIndex("dbo.Usuario", new[] { "Tarefa_id" });
            DropColumn("dbo.UploadFile", "Tarefa_id");
            DropColumn("dbo.Usuario", "Tarefa_id");
            DropTable("dbo.Processo");
            DropTable("dbo.Tarefa");
            DropTable("dbo.Fila");
            RenameIndex(table: "dbo.UploadFile", name: "unique_guid_uploadFile", newName: "unique_guid_uploadTela");
            RenameIndex(table: "dbo.Projeto", name: "unique_guid_projeto", newName: "unique_guid_uploadTela");
            RenameIndex(table: "dbo.Cliente", name: "unique_guid_cliente", newName: "unique_guid_uploadTela");
        }
    }
}
