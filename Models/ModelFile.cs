using System;
using System.Collections.Generic;

namespace Compare.Models;

public partial class ModelFile
{
    public int FileId { get; set; }

    public int Id { get; set; }

    public string ModelName { get; set; } = null!;

    public int ModelId { get; set; }

    public virtual File File { get; set; } = null!;
}
