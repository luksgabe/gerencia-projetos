namespace Projeto_Academico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcollumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Observacoes", "data_criacao", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Observacoes", "data_criacao");
        }
    }
}
