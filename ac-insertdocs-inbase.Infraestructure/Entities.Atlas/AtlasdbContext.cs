using System;
using System.Collections.Generic;
using ac_insertdocs_inbase.Domain.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ac_insertdocs_inbase.Infraestructure.Entities.Atlas;

public partial class AtlasdbContext : DbContext
{
    private readonly ConfigurationValues _configurationValues;
    public AtlasdbContext(ConfigurationValues configurationValues)
    {
        _configurationValues = configurationValues;
    }
    public virtual DbSet<Paramscargue> Paramscargues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(_configurationValues.connection_string);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("capturas_bpo", "tablefunc");

        modelBuilder.Entity<Paramscargue>(entity =>
        {
            entity.HasKey(e => e.IdTipoNegocio).HasName("pktiponegocio");

            entity.ToTable("paramscargue", "atlas");

            entity.Property(e => e.IdTipoNegocio).HasColumnName("id_tipo_negocio");
            entity.Property(e => e.Contentfields)
                .HasColumnType("character varying")
                .HasColumnName("contentfields");
            entity.Property(e => e.Contentvalide)
                .HasColumnType("character varying")
                .HasColumnName("contentvalide");
            entity.Property(e => e.Docnamefields)
                .HasMaxLength(50)
                .HasColumnName("docnamefields");
            entity.Property(e => e.Docnamevalide)
                .HasMaxLength(50)
                .HasColumnName("docnamevalide");
            entity.Property(e => e.TipoNegocio)
                .HasMaxLength(100)
                .HasColumnName("tipo_negocio");
        });
    }
}
