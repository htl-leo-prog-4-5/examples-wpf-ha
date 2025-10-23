namespace Core.Contracts;

using System;
using System.Threading.Tasks;

public interface IImportService
{
    Task<int> ImportRaceAsync(string driver, string competition, string car, DateTime raceStartTime, TimeOnly raceTime, string moves);
}