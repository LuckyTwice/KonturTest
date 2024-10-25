using System;
using System.Collections.Generic;

namespace Kontur;

public partial class Department
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Datum> Data { get; set; } = new List<Datum>();
}
