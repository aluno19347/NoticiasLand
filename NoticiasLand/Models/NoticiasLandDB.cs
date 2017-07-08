using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace NoticiasLand.Models
{
    public class NoticiasLandDB : DbContext
    {
        public virtual DbSet<Utilizadores> Utilizadores { get; set; }
        public virtual DbSet<Logins> Logins { get; set; }
        public virtual DbSet<Noticias> Noticias { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<Fotos> Fotos { get; set; }
        public virtual DbSet<Categorias> Categorias { get; set; }

        // especificar ONDE será criada a Base de Dados
        public NoticiasLandDB() : base("DBConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // não podemos usar a chave seguinte, nesta geração de tabelas
            // por causa das tabelas do Identity (gestão de utilizadores)
            // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}