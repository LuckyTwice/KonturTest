using System;
using System.Collections.Generic;

namespace Kontur;

public partial class Code
{
    public string Code1 { get; set; } = null!;

    public string CodeName { get; set; } = null!;

    public int CatId { get; set; }

    public virtual Category Cat { get; set; } = null!;

    public virtual ICollection<Datum> Data { get; set; } = new List<Datum>();
}
