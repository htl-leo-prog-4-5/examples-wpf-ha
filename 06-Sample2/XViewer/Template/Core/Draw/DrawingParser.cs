using System.Globalization;
using System.IO;
using System.Windows.Media.Media3D;
using Core.Entities;

namespace Core.Draw
{
    public class DrawingParser
    {
        public static async Task<MyDrawing> ParseDrawingAsync(string fileName)
        {
            var lines = await File.ReadAllLinesAsync(fileName);

            throw new NotImplementedException();
        }
    }
}