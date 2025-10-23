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

    //TODO: create a double list from string:Values
    public IList<double> MyValues => throw new NotImplementedException();
}