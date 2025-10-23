using Core.Contracts;

namespace Persistence;

using Base.Persistence;

public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
{
    public IExaminationRepository ExaminationRepository { get; }
    public IPatientRepository     PatientRepository     { get; }
    public IDoctorRepository      DoctorRepository      { get; }

    public UnitOfWork(ApplicationDbContext context,
        IExaminationRepository             examinationRepository,
        IPatientRepository                 patientRepository,
        IDoctorRepository                  doctorRepository
    ) : base(context)
    {
        ExaminationRepository = examinationRepository;
        PatientRepository     = patientRepository;
        DoctorRepository      = doctorRepository;
    }
}