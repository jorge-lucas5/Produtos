using System;
using System.Linq;
using Estudos.App.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace Estudos.App.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //registrando os Mapping
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            foreach (var relacionamento in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relacionamento.DeleteBehavior = DeleteBehavior.NoAction;
            }

           


            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                        .Where(p => p.ClrType == typeof(string))))
            {
                Console.WriteLine(property.GetColumnType());
                if (property.GetColumnType() == "varchar(max)" || property.GetColumnType() == "nvarchar(max)")
                    property.SetColumnType("varchar(100)");
            }


            base.OnModelCreating(modelBuilder);
        }
    }
}