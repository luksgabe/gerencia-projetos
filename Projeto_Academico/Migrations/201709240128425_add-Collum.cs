namespace Projeto_Academico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCollum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Empresa", "codEmpresa", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Empresa", "codEmpresa");
        }
    }
}
