using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TocantinsPay.Infrastructure.Scaffold;

public partial class TocantinspayContext : DbContext
{
    public TocantinspayContext()
    {
    }

    public TocantinspayContext(DbContextOptions<TocantinspayContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carteira> Carteiras { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Cofrinho> Cofrinhos { get; set; }

    public virtual DbSet<Endereco> Enderecos { get; set; }

    public virtual DbSet<Transacao> Transacaos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5490;Database=tocantinspay;Username=admin;Password=admin");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carteira>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("carteira_pkey");

            entity.ToTable("carteira");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Agencia)
                .HasMaxLength(10)
                .HasColumnName("agencia");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.Conta)
                .HasMaxLength(50)
                .HasColumnName("conta");
            entity.Property(e => e.Saldo)
                .HasPrecision(15, 2)
                .HasColumnName("saldo");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Carteiras)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("carteira_cliente_id_fkey");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cliente_pkey");

            entity.ToTable("cliente");

            entity.HasIndex(e => e.Cpf, "cliente_cpf_key").IsUnique();

            entity.HasIndex(e => e.Email, "cliente_email_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Cpf)
                .HasMaxLength(14)
                .HasColumnName("cpf");
            entity.Property(e => e.DataNascimento).HasColumnName("data_nascimento");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.NomeCompleto)
                .HasMaxLength(150)
                .HasColumnName("nome_completo");
            entity.Property(e => e.Senha)
                .HasMaxLength(255)
                .HasColumnName("senha");
            entity.Property(e => e.Situacao).HasColumnName("situacao");
            entity.Property(e => e.Telefone)
                .HasMaxLength(20)
                .HasColumnName("telefone");
        });

        modelBuilder.Entity<Cofrinho>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cofrinho_pkey");

            entity.ToTable("cofrinho");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CarteiraId).HasColumnName("carteira_id");
            entity.Property(e => e.Meta)
                .HasPrecision(15, 2)
                .HasColumnName("meta");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
            entity.Property(e => e.Saldo)
                .HasPrecision(15, 2)
                .HasColumnName("saldo");

            entity.HasOne(d => d.Carteira).WithMany(p => p.Cofrinhos)
                .HasForeignKey(d => d.CarteiraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cofrinho_carteira_id_fkey");
        });

        modelBuilder.Entity<Endereco>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("endereco_pkey");

            entity.ToTable("endereco");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Bairro)
                .HasMaxLength(100)
                .HasColumnName("bairro");
            entity.Property(e => e.Cep)
                .HasMaxLength(9)
                .IsFixedLength()
                .HasColumnName("cep");
            entity.Property(e => e.Cidade)
                .HasMaxLength(100)
                .HasColumnName("cidade");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.Logradouro)
                .HasMaxLength(200)
                .HasColumnName("logradouro");
            entity.Property(e => e.Uf)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("uf");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Enderecos)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("endereco_cliente_id_fkey");
        });

        modelBuilder.Entity<Transacao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("transacao_pkey");

            entity.ToTable("transacao");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CarteiraId).HasColumnName("carteira_id");
            entity.Property(e => e.CofrinhoId).HasColumnName("cofrinho_id");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.Descricao)
                .HasMaxLength(150)
                .HasColumnName("descricao");
            entity.Property(e => e.SaldoResultante)
                .HasPrecision(15, 2)
                .HasColumnName("saldo_resultante");
            entity.Property(e => e.Tipo).HasColumnName("tipo");
            entity.Property(e => e.Valor)
                .HasPrecision(15, 2)
                .HasColumnName("valor");

            entity.HasOne(d => d.Carteira).WithMany(p => p.Transacaos)
                .HasForeignKey(d => d.CarteiraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transacao_carteira_id_fkey");

            entity.HasOne(d => d.Cofrinho).WithMany(p => p.Transacaos)
                .HasForeignKey(d => d.CofrinhoId)
                .HasConstraintName("transacao_cofrinho_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
