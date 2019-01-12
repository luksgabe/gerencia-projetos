namespace Projeto_Academico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20112017 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Observacoes", new[] { "tarefa_id" });
            DropIndex("dbo.Observacoes", new[] { "usuario_id" });
            RenameColumn(table: "dbo.Observacoes", name: "tarefa_id", newName: "cod_tarefa");
            RenameColumn(table: "dbo.Observacoes", name: "usuario_id", newName: "cod_usuario");
            AlterColumn("dbo.Observacoes", "cod_tarefa", c => c.Long(nullable: false));
            AlterColumn("dbo.Observacoes", "cod_usuario", c => c.Long(nullable: false));
            CreateIndex("dbo.Observacoes", "cod_tarefa");
            CreateIndex("dbo.Observacoes", "cod_usuario");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Observacoes", new[] { "cod_usuario" });
            DropIndex("dbo.Observacoes", new[] { "cod_tarefa" });
            AlterColumn("dbo.Observacoes", "cod_usuario", c => c.Long());
            AlterColumn("dbo.Observacoes", "cod_tarefa", c => c.Long());
            RenameColumn(table: "dbo.Observacoes", name: "cod_usuario", newName: "usuario_id");
            RenameColumn(table: "dbo.Observacoes", name: "cod_tarefa", newName: "tarefa_id");
            CreateIndex("dbo.Observacoes", "usuario_id");
            CreateIndex("dbo.Observacoes", "tarefa_id");
        }
    }
}
