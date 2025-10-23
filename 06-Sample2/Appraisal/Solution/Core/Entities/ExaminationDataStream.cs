using Base.Core.Entities;

namespace Core.Entities;

using System.Globalization;

public class ExaminationDataStream : EntityObject
{
    public required string Name { get; set; }

    public double Period { get; set; }

    public required string Values { get; set; }

    public int          ExaminationId { get; set; }
    public Examination? Examination   { get; set; }

    public IList<double> MyValues => string.IsNullOrEmpty(Values)
        ? new List<double>()
        : Values.Split(',')
            .Select(((string val, int idx) =>
                double.Parse(val, CultureInfo.InvariantCulture)))
            .ToList();
}