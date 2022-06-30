using TechnicalTest.Core.Models;

namespace TechnicalTest.API.Utility;

public static class Util
{
    public static bool IsValidTriangle(List<Coordinate> triangleCoordinates)
    {
        if (triangleCoordinates.Count != 2)
        {
            return false;
        }
        
        // calculates the area of the triangle. uses shoelace formula.
        var result = triangleCoordinates[0].X * (triangleCoordinates[1].Y - triangleCoordinates[2].Y) +
                triangleCoordinates[1].X * (triangleCoordinates[2].Y - triangleCoordinates[0].Y) +
                triangleCoordinates[2].X * (triangleCoordinates[0].Y - triangleCoordinates[1].Y);

        return result != 0;
    }
}