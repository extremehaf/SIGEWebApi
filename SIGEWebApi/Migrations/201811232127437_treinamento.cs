namespace SIGEWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class treinamento : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Treinamento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricação = c.String(),
                        Data = c.DateTime(nullable: false),
                        custo = c.Double(nullable: false),
                        AreaEnvolvida = c.String(),
                        AreaRequisitante = c.String(),
                        Motivo = c.String(),
                        obs = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Treinamento");
        }
    }
}
