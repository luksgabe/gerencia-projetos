namespace Projeto_Academico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class criacaodb070517 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 30),
                        email = c.String(nullable: false, maxLength: 30),
                        senha = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Usuarios");
        }
    }
}
