using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace OnmpApp.Models.Database;

[Table("Catalogs")]
public class Catalog
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    // Название карты 
    [NotNull, Indexed(Name = "name_idx", Order = 1)]
    public string Name { get; set; }

    public string Text { get; set; }
}

public class CatalogName
{
    public string Name { get; set; }
}
