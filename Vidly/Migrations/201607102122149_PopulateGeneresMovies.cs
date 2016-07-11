namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGeneresMovies : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO GenreMovies(Name) VALUES ('Comedy')");
            Sql("INSERT INTO GenreMovies(Name) VALUES ('Action')");
            Sql("INSERT INTO GenreMovies(Name) VALUES ('Family')");
            Sql("INSERT INTO GenreMovies(Name) VALUES ('Romance')");
        }
        
        public override void Down()
        {
        }
    }
}
