using Base.Core.Entities;

namespace Core.Entities;

public class Examination : EntityObject
{
    public DateTime ExaminationDate { get; set; }

    public DateTime? MedicalFindingsDate { get; set; }

    public string? MedicalFindings { get; set; }

    public int? PatientId { get; set; }

    public Patient? Patient { get; set; }

    public int? DoctorId { get; set; }

    public Doctor? Doctor { get; set; }

    public IList<ExaminationDataStream>? DataStreams { get; set; }
}