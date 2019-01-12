namespace Projeto_Academico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _29082017 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Ativacaos", "cod_usuario");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ativacaos", "cod_usuario", c => c.Long());
        }
    }
}
