namespace Projeto_Academico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _61117 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Usuario", "Tarefa_id", "dbo.Tarefa");
            DropIndex("dbo.Usuario", new[] { "Tarefa_id" });
            CreateTable(
                "dbo.UsuarioTarefa",
                c => new
                    {
                        cod_usuario = c.Long(nullable: false),
                        cod_tarefa = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.cod_usuario, t.cod_tarefa })
                .ForeignKey("dbo.Tarefa", t => t.cod_usuario)
                .ForeignKey("dbo.Usuario", t => t.cod_tarefa)
                .Index(t => t.cod_usuario)
                .Index(t => t.cod_tarefa);
            
            DropColumn("dbo.Usuario", "Tarefa_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Usuario", "Tarefa_id", c => c.Long());
            DropForeignKey("dbo.UsuarioTarefa", "cod_tarefa", "dbo.Usuario");
            DropForeignKey("dbo.UsuarioTarefa", "cod_usuario", "dbo.Tarefa");
            DropIndex("dbo.UsuarioTarefa", new[] { "cod_tarefa" });
            DropIndex("dbo.UsuarioTarefa", new[] { "cod_usuario" });
            DropTable("dbo.UsuarioTarefa");
            CreateIndex("dbo.Usuario", "Tarefa_id");
            AddForeignKey("dbo.Usuario", "Tarefa_id", "dbo.Tarefa", "id_tarefa");
        }
    }
}
