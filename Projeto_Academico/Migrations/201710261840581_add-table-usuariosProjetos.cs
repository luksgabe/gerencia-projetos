namespace Projeto_Academico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtableusuariosProjetos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjetoUsuario",
                c => new
                    {
                        Projeto_id = c.Long(nullable: false),
                        Usuario_id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Projeto_id, t.Usuario_id })
                .ForeignKey("dbo.Projeto", t => t.Projeto_id)
                .ForeignKey("dbo.Usuario", t => t.Usuario_id)
                .Index(t => t.Projeto_id)
                .Index(t => t.Usuario_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjetoUsuario", "Usuario_id", "dbo.Usuario");
            DropForeignKey("dbo.ProjetoUsuario", "Projeto_id", "dbo.Projeto");
            DropIndex("dbo.ProjetoUsuario", new[] { "Usuario_id" });
            DropIndex("dbo.ProjetoUsuario", new[] { "Projeto_id" });
            DropTable("dbo.ProjetoUsuario");
        }
    }
}
