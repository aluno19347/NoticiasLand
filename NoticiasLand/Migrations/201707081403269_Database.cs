namespace NoticiasLand.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        CategoriaID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 30),
                        UrlSlug = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.CategoriaID)
                .Index(t => t.UrlSlug, unique: true);
            
            CreateTable(
                "dbo.Noticias",
                c => new
                    {
                        NoticiaID = c.Int(nullable: false, identity: true),
                        UtilizadorFK = c.Int(nullable: false),
                        UrlSlug = c.String(nullable: false, maxLength: 30),
                        Meta = c.String(nullable: false),
                        Titulo = c.String(nullable: false),
                        Subtitulo = c.String(),
                        Data = c.DateTime(nullable: false),
                        CategoriaFK = c.Int(nullable: false),
                        Texto = c.String(nullable: false),
                        Likes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NoticiaID)
                .ForeignKey("dbo.Categorias", t => t.CategoriaFK)
                .ForeignKey("dbo.Utilizadores", t => t.UtilizadorFK)
                .Index(t => t.UtilizadorFK)
                .Index(t => t.UrlSlug, unique: true)
                .Index(t => t.CategoriaFK);
            
            CreateTable(
                "dbo.Fotos",
                c => new
                    {
                        FotoID = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 30),
                        FotoUrl = c.String(nullable: false),
                        NoticiaFK = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FotoID)
                .ForeignKey("dbo.Noticias", t => t.NoticiaFK)
                .Index(t => t.NoticiaFK);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 30),
                        UrlSlug = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.TagID)
                .Index(t => t.UrlSlug, unique: true);
            
            CreateTable(
                "dbo.Utilizadores",
                c => new
                    {
                        UtilizadorID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false),
                        Foto = c.String(),
                    })
                .PrimaryKey(t => t.UtilizadorID);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 16),
                        UtilizadorFK = c.Int(nullable: false),
                        PasswordSalt = c.String(nullable: false),
                        PasswordHash = c.String(nullable: false),
                        Nivel = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Username)
                .ForeignKey("dbo.Utilizadores", t => t.UtilizadorFK)
                .Index(t => t.UtilizadorFK);
            
            CreateTable(
                "dbo.TagsNoticias",
                c => new
                    {
                        Tags_TagID = c.Int(nullable: false),
                        Noticias_NoticiaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tags_TagID, t.Noticias_NoticiaID })
                .ForeignKey("dbo.Tags", t => t.Tags_TagID)
                .ForeignKey("dbo.Noticias", t => t.Noticias_NoticiaID)
                .Index(t => t.Tags_TagID)
                .Index(t => t.Noticias_NoticiaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logins", "UtilizadorFK", "dbo.Utilizadores");
            DropForeignKey("dbo.Noticias", "UtilizadorFK", "dbo.Utilizadores");
            DropForeignKey("dbo.TagsNoticias", "Noticias_NoticiaID", "dbo.Noticias");
            DropForeignKey("dbo.TagsNoticias", "Tags_TagID", "dbo.Tags");
            DropForeignKey("dbo.Fotos", "NoticiaFK", "dbo.Noticias");
            DropForeignKey("dbo.Noticias", "CategoriaFK", "dbo.Categorias");
            DropIndex("dbo.TagsNoticias", new[] { "Noticias_NoticiaID" });
            DropIndex("dbo.TagsNoticias", new[] { "Tags_TagID" });
            DropIndex("dbo.Logins", new[] { "UtilizadorFK" });
            DropIndex("dbo.Tags", new[] { "UrlSlug" });
            DropIndex("dbo.Fotos", new[] { "NoticiaFK" });
            DropIndex("dbo.Noticias", new[] { "CategoriaFK" });
            DropIndex("dbo.Noticias", new[] { "UrlSlug" });
            DropIndex("dbo.Noticias", new[] { "UtilizadorFK" });
            DropIndex("dbo.Categorias", new[] { "UrlSlug" });
            DropTable("dbo.TagsNoticias");
            DropTable("dbo.Logins");
            DropTable("dbo.Utilizadores");
            DropTable("dbo.Tags");
            DropTable("dbo.Fotos");
            DropTable("dbo.Noticias");
            DropTable("dbo.Categorias");
        }
    }
}
