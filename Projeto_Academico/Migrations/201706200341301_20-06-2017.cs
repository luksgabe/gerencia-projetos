namespace Projeto_Academico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20062017 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "apelido", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Usuarios", "nome", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Usuarios", "email", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Usuarios", "senha", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuarios", "senha", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Usuarios", "email", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Usuarios", "nome", c => c.String(nullable: false, maxLength: 30));
            DropColumn("dbo.Usuarios", "apelido");
        }
    }
}
