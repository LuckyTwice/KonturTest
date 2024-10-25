using System;
using System.Collections.Generic;

namespace Kontur;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Code> Codes { get; set; } = new List<Code>();
}
