using TechnicalTest.Core.Interfaces;
using TechnicalTest.Core.Models;

namespace TechnicalTest.Core.Services
{
    public class ShapeService : IShapeService
    {
        public Shape ProcessTriangle(Grid grid, GridValue gridValue)
        {
            Coordinate topLeftVertex, outerVertex, bottomRightVertex;
            const int adjustPosition = 1;
            if (gridValue.Column % 2 == 0) // checks if column is even, if so is the top-right triangle.
            {
                topLeftVertex = new Coordinate(((gridValue.Column / 2) - adjustPosition) * grid.Size,
                    (gridValue.GetNumericRow() - adjustPosition) * grid.Size);
                outerVertex = new Coordinate((gridValue.Column / 2) * grid.Size,
                    (gridValue.GetNumericRow() - 1) * grid.Size);
                bottomRightVertex = new Coordinate((gridValue.Column / 2) * grid.Size,
                    gridValue.GetNumericRow() * grid.Size);
                return new Triangle(topLeftVertex, outerVertex, bottomRightVertex);
            }

            // bottom left triangle formula
            topLeftVertex = new Coordinate(gridValue.Column / 2 * grid.Size,
                (gridValue.GetNumericRow() - adjustPosition) * grid.Size);
            outerVertex = new Coordinate(gridValue.Column / 2 * grid.Size, gridValue.GetNumericRow() * grid.Size);
            bottomRightVertex = new Coordinate((gridValue.Column / 2 + adjustPosition) * grid.Size,
                gridValue.GetNumericRow() * grid.Size);
            return new Triangle(topLeftVertex, outerVertex, bottomRightVertex);
        }

        public GridValue ProcessGridValueFromTriangularShape(Grid grid, Triangle triangle)
        {

            bool isValid = IsValidShape(triangle);

            if (!isValid)
            {
                return null!;
            }

            // highest y value tell us the row.
            int rowLetterValue =
                Math.Max(Math.Max(triangle.TopLeftVertex.Y, triangle.OuterVertex.Y), triangle.BottomRightVertex.Y) /
                grid.Size;

            int firstValue;
            int secondValue;
            // find matching X values
            if (triangle.TopLeftVertex.X == triangle.OuterVertex.X)
            {
                firstValue = triangle.TopLeftVertex.X;
                secondValue = triangle.BottomRightVertex.X;
            }
            else if (triangle.TopLeftVertex.X == triangle.BottomRightVertex.X)
            {
                firstValue = triangle.TopLeftVertex.X;
                secondValue = triangle.OuterVertex.X;
            }
            else
            {
                firstValue = triangle.OuterVertex.X;
                secondValue = triangle.TopLeftVertex.X;
            }
            var columnValue = firstValue * 2 / grid.Size;
            
            // if second value greater than first + 1, this happens if the column is supposed to be an odd num.
            if (secondValue > firstValue)
            {
                columnValue += 1;
            }

            return new GridValue(rowLetterValue, columnValue);
        }

        public bool IsValidShape(Triangle triangle)
        {
            if (triangle.Coordinates.Count != 3)
            {
                return false;
            }
        
            // calculates the area of the triangle. uses shoelace formula.
            var result = triangle.TopLeftVertex.X * (triangle.OuterVertex.Y - triangle.BottomRightVertex.Y) +
                         triangle.OuterVertex.X * (triangle.BottomRightVertex.Y - triangle.TopLeftVertex.Y) +
                         triangle.BottomRightVertex.X * (triangle.TopLeftVertex.Y - triangle.OuterVertex.Y);

            return result != 0;
        }
    }
}