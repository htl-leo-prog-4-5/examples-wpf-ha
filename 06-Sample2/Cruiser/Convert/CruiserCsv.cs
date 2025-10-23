using Base.Tools.CsvImport;

namespace Convert;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

internal class CruiserCsv
{
    public string Name { get; set; }
    public string BJ { get; set; }

    [CsvImportFormat(Culture = "de")]
    public decimal? BRZ { get; set; }

    [CsvImportFormat(Culture = "de")]
    public decimal? Laenge { get; set; }
    
    [CsvImportFormat(Culture = "de")]
    public decimal? Kab { get; set; }
    
    [CsvImportFormat(Culture = "de")]
    public decimal? Pass { get; set; }
    
    [CsvImportFormat(Culture = "de")]
    public decimal? Bes { get; set; }
    public string Reederei { get; set; }
    public string Bauklasse { get; set; }
    public string Bemerkungen { get; set; }
}