using System;
using System.Collections.Generic;

namespace TocantinsPay.Infrastructure.Scaffold;

public partial class Cofrinho
{
    public Guid Id { get; set; }

    public string Nome { get; set; } = null!;

    public decimal Saldo { get; set; }

    public decimal? Meta { get; set; }

    public Guid CarteiraId { get; set; }

    public virtual Carteira Carteira { get; set; } = null!;

    public virtual ICollection<Transacao> Transacaos { get; set; } = new List<Transacao>();
}
