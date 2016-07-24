namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhoneNumberToUserRegistration : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE [dbo].[AspNetUsers] SET PhoneNumber = N''");
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String());
            Sql("UPDATE [dbo].[AspNetUsers] SET PhoneNumber = NULL");
        }
    }
}
