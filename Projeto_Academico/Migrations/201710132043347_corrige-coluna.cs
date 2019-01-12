namespace Projeto_Academico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class corrigecoluna : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Usuario", "uploadImage_id", "dbo.UploadFile");
            DropIndex("dbo.Usuario", new[] { "uploadImage_id" });
            DropPrimaryKey("dbo.UploadFile");
            AlterColumn("dbo.Usuario", "uploadImage_id", c => c.Long());
            AlterColumn("dbo.UploadFile", "id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.UploadFile", "id");
            CreateIndex("dbo.Usuario", "uploadImage_id");
            AddForeignKey("dbo.Usuario", "uploadImage_id", "dbo.UploadFile", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuario", "uploadImage_id", "dbo.UploadFile");
            DropIndex("dbo.Usuario", new[] { "uploadImage_id" });
            DropPrimaryKey("dbo.UploadFile");
            AlterColumn("dbo.UploadFile", "id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Usuario", "uploadImage_id", c => c.Int());
            AddPrimaryKey("dbo.UploadFile", "id");
            CreateIndex("dbo.Usuario", "uploadImage_id");
            AddForeignKey("dbo.Usuario", "uploadImage_id", "dbo.UploadFile", "id");
        }
    }
}
