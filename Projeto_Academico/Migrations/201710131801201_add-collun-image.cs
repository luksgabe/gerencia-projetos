namespace Projeto_Academico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcollunimage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UploadFile",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(),
                        tamanho = c.Int(nullable: false),
                        peso = c.Int(nullable: false),
                        tipo = c.String(),
                        caminho = c.String(),
                        caminhoServidor = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Usuario", "uploadImage_id", c => c.Int());
            CreateIndex("dbo.Usuario", "uploadImage_id");
            AddForeignKey("dbo.Usuario", "uploadImage_id", "dbo.UploadFile", "id");
            DropColumn("dbo.Usuario", "imagemPerfil");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Usuario", "imagemPerfil", c => c.Binary());
            DropForeignKey("dbo.Usuario", "uploadImage_id", "dbo.UploadFile");
            DropIndex("dbo.Usuario", new[] { "uploadImage_id" });
            DropColumn("dbo.Usuario", "uploadImage_id");
            DropTable("dbo.UploadFile");
        }
    }
}
