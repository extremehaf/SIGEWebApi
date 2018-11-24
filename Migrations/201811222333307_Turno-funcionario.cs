namespace SIGEWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Turnofuncionario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FUNCIONARIO", "Turno", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FUNCIONARIO", "Turno");
        }
    }
}
