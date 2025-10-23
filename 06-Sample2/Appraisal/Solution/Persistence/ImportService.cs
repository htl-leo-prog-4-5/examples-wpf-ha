namespace Persistence;

using System.Globalization;

using Base.Tools.CsvImport;

using Core.Contracts;
using Core.Entities;

using Persistence.ImportData;

using System.Threading.Tasks;

using Core.Tools;

public class ImportService : IImportService
{
    private IUnitOfWork _uow;

    public ImportService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<int> ImportDoctorsAsync(string fileName)
    {
        var doctorsCsv = await new CsvImport<DoctorCsv>().ReadAsync(fileName);

        var doctors = doctorsCsv.Select(d => new Doctor()
        {
            Name = d.Name
        }).ToList();

        await _uow.DoctorRepository.AddRangeAsync(doctors);

        await _uow.SaveChangesAsync();

        return doctors.Count;
    }

    public async Task<int> ImportAsync(string directory)
    {
        int newCount    = 0;
        int existsCount = 0;

        foreach (var fileName in Directory.GetFiles(directory, "*.txt"))
        {
            if (await ImportFileAsync(fileName))
            {
                newCount++;
            }
            else
            {
                existsCount++;
            }
        }

        return newCount;
    }

    public async Task<bool> ImportFileAsync(string fileName)
    {
        var csvExamination = (await (new CsvImport<ExaminationCsv>().ReadAsync(fileName))).ToDictionary(c => c.Key, c => c.Value);

        var patientSv       = csvExamination["Patient"];
        var examinationDate = DateTime.ParseExact(csvExamination["Date"], "yyyy.MM.dd HH:mm", CultureInfo.InvariantCulture);

        if (!SVNumber.IsSvNumberValid(patientSv))
        {
            // This is a invalid SvNumber!
            Console.WriteLine($"Invalid sv number found in file '{fileName}': {patientSv}");
            return false;
        }

        if (string.IsNullOrEmpty(patientSv) ||
            (await _uow.ExaminationRepository.GetExaminationAsync(patientSv, examinationDate)) != null)
        {
            // already imported!
            Console.WriteLine($"Examination exists (file '{fileName}'): {patientSv}:{examinationDate}");
            return false;
        }

        var patient = await _uow.PatientRepository.GetBySVNumberAsync(patientSv);

        if (patient == null)
        {
            patient = new Patient()
            {
                SVNumber = csvExamination["Patient"]
            };

            if (csvExamination.TryGetValue("PatientName", out var name))
            {
                var names = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (names.Length > 0)
                {
                    patient.LastName = names[0];
                    if (names.Length > 1)
                    {
                        patient.FirstName = names[1];
                    }
                }
            }
        }

        var examination = new Examination()
        {
            Patient         = patient,
            ExaminationDate = examinationDate,
            DataStreams = csvExamination
                .GetValueOrDefault("DataStreams", "").Split(',')
                .Select(name =>
                {
                    var dataValues =
                        string.Join(',', csvExamination
                            .Where(x => x.Key.StartsWith(name + "Data"))
                            .OrderBy(x => x.Key)
                            .SelectMany(x => x.Value
                                .Split([' ', ','], StringSplitOptions.RemoveEmptyEntries)
                                .Select(v => double.Parse(v, CultureInfo.InvariantCulture)))
                            .Select(x => x.ToString(CultureInfo.InvariantCulture)));
                    return new ExaminationDataStream()
                    {
                        Period = double.Parse(csvExamination[name + "Period"], CultureInfo.InvariantCulture),
                        Name   = csvExamination[name + "Name"],
                        Values = dataValues
                    };
                }).ToList()
        };


        await _uow.ExaminationRepository.AddAsync(examination);

        await _uow.SaveChangesAsync();

        return true;
    }
}