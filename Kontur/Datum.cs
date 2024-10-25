using System;
using System.Collections.Generic;

namespace Kontur;

public partial class Datum
{
    public int Id { get; set; }

    public string CodeId { get; set; } = null!;

    public int DepId { get; set; }

    public virtual Code Code { get; set; } = null!;

    public virtual Department Dep { get; set; } = null!;
}
