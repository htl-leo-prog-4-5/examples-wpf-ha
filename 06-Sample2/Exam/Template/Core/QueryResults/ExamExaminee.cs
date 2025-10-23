namespace Core.QueryResults;

using System;

public class ExamExaminee
{
    //TODO Dto for all Exam/Examinee (=CsvFile) loaded from the Database
    // with this dto you can verify, it a Csv File is already imported 
    public int      ExamId       { get; set; }
    public string   ExamName     { get; set; } = string.Empty;
    public DateOnly ExamDate     { get; set; }
    public int      ExamineeId   { get; set; }
    public string   ExamineeName { get; set; } = string.Empty;
}