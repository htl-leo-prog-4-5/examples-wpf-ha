using Core.Entities;
using System.Globalization;

namespace Core
{
    public class GCodeParser
    {
        public static async Task<Pattern> ParsePatternFromGcodeAsync(string fileName)
        {
            var lines = await File.ReadAllLinesAsync(fileName);
            var codes = lines
                .Select(l => l.Split(' '))
                .Where(columns => !columns.Contains("Z"))
                .Where(columns => columns.Length == 3)
                .Select(line => new
                {
                    Code = line[0],
                    X = double.Parse(line[1].AsSpan(1), CultureInfo.InvariantCulture),
                    Y = double.Parse(line[2].AsSpan(1), CultureInfo.InvariantCulture)
                })
                .ToList();

            var pattern = new Pattern
            {
                Name = Path.GetFileNameWithoutExtension(fileName)
            };
            CutLine cutLine = new CutLine();
            foreach (var code in codes)
            {
                if (code.Code == "G00")
                {
                    if (cutLine.Points.Count > 0)
                    {
                        pattern.Lines.Add(cutLine);
                        cutLine = new CutLine();
                    }
                }
                cutLine.Points.Add(new LinePoint { X = code.X, Y = code.Y });
            }
            if (cutLine.Points.Count > 0)
            {
                pattern.Lines.Add(cutLine);
            }
            return pattern;

        }
    }
}
