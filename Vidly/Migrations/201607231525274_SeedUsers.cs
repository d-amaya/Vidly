namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
             INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'a733f87d-9cec-4d15-9feb-3ce4da309a51', N'admin@vidly.com', 0, N'AKWLLWqbFqpsPM1250ButSriaz2IICVbYPBQjlRD4KSCRbaX3pjPuf3pksq4dzhYSA==', N'66b89884-5109-464a-b95c-6d1ea61ae329', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
             INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'feb68e1e-d268-42e5-95ef-d538a01c2667', N'guest@vidly.com', 0, N'AIb61lDOusqdI2tFtv08eCGlVE0ry819CmowN4hMOJ83aMZGZ7rR4JGU7XG0i7xtWA==', N'fe549d60-9fe1-45ac-8974-fcfc002ec318', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')

             INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1e6235f3-875c-422b-acb6-4de53cac2223', N'CanManageMovies')

             INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a733f87d-9cec-4d15-9feb-3ce4da309a51', N'1e6235f3-875c-422b-acb6-4de53cac2223')
            ");
        }
        
        public override void Down()
        {
        }
    }
}
