using System;
using System.Collections.Generic;

namespace TocantinsPay.Infrastructure.Scaffold;

public partial class Transacao
{
    public Guid Id { get; set; }

    public short Tipo { get; set; }

    public DateTime Data { get; set; }

    public decimal Valor { get; set; }

    public decimal SaldoResultante { get; set; }

    public string? Descricao { get; set; }

    public Guid CarteiraId { get; set; }

    public Guid? CofrinhoId { get; set; }

    public virtual Carteira Carteira { get; set; } = null!;

    public virtual Cofrinho? Cofrinho { get; set; }
}
