using System;
using System.Collections.Generic;

namespace TocantinsPay.Infrastructure.Scaffold;

public partial class Cliente
{
    public Guid Id { get; set; }

    public string NomeCompleto { get; set; } = null!;

    public short Situacao { get; set; }

    public string Email { get; set; } = null!;

    public string Cpf { get; set; } = null!;

    public DateOnly DataNascimento { get; set; }

    public string Telefone { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public virtual ICollection<Carteira> Carteiras { get; set; } = new List<Carteira>();

    public virtual ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();
}
