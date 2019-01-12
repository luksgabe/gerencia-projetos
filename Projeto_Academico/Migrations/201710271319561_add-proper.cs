namespace Projeto_Academico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addproper : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cliente", "ativo", c => c.Boolean(nullable: false));
            AddColumn("dbo.Projeto", "ativo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projeto", "ativo");
            DropColumn("dbo.Cliente", "ativo");
        }
    }
}
