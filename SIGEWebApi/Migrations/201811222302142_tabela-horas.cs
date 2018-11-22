namespace SIGEWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tabelahoras : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HorasTrabalhadas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HorasTrabalhadasNoMes = c.String(nullable: false),
                        Mes = c.Int(nullable: false),
                        Funcionario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FUNCIONARIO", t => t.Funcionario_Id)
                .Index(t => t.Funcionario_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HorasTrabalhadas", "Funcionario_Id", "dbo.FUNCIONARIO");
            DropIndex("dbo.HorasTrabalhadas", new[] { "Funcionario_Id" });
            DropTable("dbo.HorasTrabalhadas");
        }
    }
}
