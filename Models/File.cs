using System;
using System.Collections.Generic;

namespace Compare.Models;

public partial class File
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Path { get; set; } = null!;

    public virtual ModelFile? ModelFile { get; set; }
}
