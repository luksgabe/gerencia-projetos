namespace Projeto_Academico.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.UploadFile", name: "id", newName: "id_uploadFile");
            CreateIndex("dbo.UploadFile", "id_uploadFile", unique: true, name: "unique_guid_uploadTela");
        }
        
        public override void Down()
        {
            DropIndex("dbo.UploadFile", "unique_guid_uploadTela");
            RenameColumn(table: "dbo.UploadFile", name: "id_uploadFile", newName: "id");
        }
    }
}
