namespace Core.Contracts;

using Base.Core.Contracts;

public interface IUnitOfWork : IBaseUnitOfWork
{
    public IExaminationRepository ExaminationRepository { get; }
    public IPatientRepository     PatientRepository     { get; }
    public IDoctorRepository      DoctorRepository      { get; }
}