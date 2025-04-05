using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.EntityMappings;

public class ProdutoMap : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("produtos");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasColumnName("id")
               .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(x => x.Nome)
               .IsRequired()
               .HasColumnName("nome")
               .HasMaxLength(255);

        builder.Property(x => x.Valor)
               .IsRequired()
               .HasColumnName("valor")
               .HasPrecision(10, 2);

        builder.Property(x => x.QtdEstoque)
               .IsRequired()
               .HasColumnName("qtd_estoque");

        builder.Property(x => x.UrlImagem)
               .HasColumnName("url_imagem")
               .HasMaxLength(255);

        builder.Property(x => x.DataCadastro)
               .IsRequired()
               .HasColumnName("data_cadastro")
               .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(x => x.DataAlteracao)
               .HasColumnName("data_alteracao");

        builder.Property(x => x.Ativo)
               .IsRequired()
               .HasColumnName("ativo")
               .HasDefaultValue(true);

        builder.Property(x => x.IdCategoria)
               .IsRequired()
               .HasColumnName("id_categoria");

        builder.HasOne(x => x.Categoria)
               .WithMany()
               .HasForeignKey(x => x.IdCategoria);
    }
}
