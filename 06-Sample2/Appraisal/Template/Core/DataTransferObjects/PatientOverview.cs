namespace Core.DataTransferObjects;

public record PatientOverview(
    int       Id,
    string    SvNumber,
    string?   FirstName,
    string?   LastName,
    int       ExaminationCount,
    DateTime? LastExamination
);