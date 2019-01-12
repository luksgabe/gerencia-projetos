namespace Projeto_Academico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcollumfkgrupoUsuario : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GrupoUsuario", "empresa_id", "dbo.Empresa");
            DropIndex("dbo.GrupoUsuario", new[] { "empresa_id" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.GrupoUsuario", "empresa_id");
            AddForeignKey("dbo.GrupoUsuario", "empresa_id", "dbo.Empresa", "id_empresa");
        }
    }
}
