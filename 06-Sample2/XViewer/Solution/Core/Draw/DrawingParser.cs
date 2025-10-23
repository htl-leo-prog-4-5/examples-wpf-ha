using System.Globalization;
using System.IO;
using System.Windows.Media.Media3D;
using Core.Entities;

namespace Core.Draw
{
    public class DrawingParser
    {
        private static IEnumerable<(double x, double y)> ToPoints(string str)
        {
            var firstValue = 0.0;
            int count = 0;

            var coordinates = str.Split([' ', ',']).ToArray();
            if (coordinates.Length % 2 != 0 || coordinates.Any(string.IsNullOrEmpty))
            {
                throw new InvalidDataException("invalid number of point coordinates");
            }

            foreach (var item in coordinates)
            {
                count++;
                var val = double.Parse(item, CultureInfo.InvariantCulture);
                if (count % 2 == 1)
                {
                    firstValue = val;
                }
                else
                {
                    yield return new(firstValue, val);
                }
            }
        }

        private static (double x, double y) ToPoint(string val)
        {
            var cols = val.Split([' ', ',']);
            return (double.Parse(cols[0], CultureInfo.InvariantCulture),
                double.Parse(cols[1], CultureInfo.InvariantCulture));
        }

        public static async Task<MyDrawing> ParseDrawingAsync(string fileName)
        {
            var lines = await File.ReadAllLinesAsync(fileName);

            return new MyDrawing()
            {
                Name = Path.GetFileName(fileName),
                Shapes =
                    lines.Select(line =>
                    {
                        var columns = line.Split(';');
                        var valuesDict = columns.Skip(1)
                            .ToDictionary(v => v.Split('=').First(), v => v.Split('=').Last());

                        return (Shape) (columns[0] switch
                        {
                            "Line" => new Line()
                            {
                                ColorIdx = int.Parse(valuesDict["ColorIdx"]),
                                StartPoint = ToPoint(valuesDict["StartPoint"]),
                                EndPoint = ToPoint(valuesDict["EndPoint"])
                            },
                            "PolyLine" => new Polyline()
                            {
                                ColorIdx = int.Parse(valuesDict["ColorIdx"]),
                                StartPoint = ToPoint(valuesDict["StartPoint"]),
                                Points = ToPoints(valuesDict["Points"]).ToList()
                            },
                            "Rectangle" => new Rectangle()
                            {
                                ColorIdx = int.Parse(valuesDict["ColorIdx"]),
                                StartPoint = ToPoint(valuesDict["StartPoint"]),
                                EndPoint = ToPoint(valuesDict["EndPoint"])
                            },
                            _ => throw new Exception("illegal shape type, illegal drawing")
                        });
                    }).ToList()
            };
        }
    }
}