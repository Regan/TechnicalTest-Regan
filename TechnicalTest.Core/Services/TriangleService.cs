using TechnicalTest.Core.Interfaces;
using TechnicalTest.Core.Models;

namespace TechnicalTest.Core.Services
{
    public class TriangleService : IShapeService
    {
        public Shape ProcessShape(Grid grid, GridValue gridValue)
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

        public GridValue ProcessGridValue(Grid grid, Shape shape)
        {
            var triangle = new Triangle(shape.Coordinates[ListVertexPositions.TopLeftVertexPos],
                shape.Coordinates[ListVertexPositions.OuterVertexPos],
                shape.Coordinates[ListVertexPositions.BottomRightVertex]);

            if (!IsValidShape(triangle))
            {
                return null;
            }

            // bottom right vertex will always give us row.
            int rowLetterValue = triangle.BottomRightVertex.Y / grid.Size;

            int columnValue;
            // if topLeftX == OuterX odd number. 
            if (triangle.TopLeftVertex.X == triangle.OuterVertex.X)
            {
                columnValue = triangle.OuterVertex.X * 2 / grid.Size + 1;
            }
            else
            {
                columnValue = triangle.OuterVertex.X * 2 / grid.Size;
            }

            return new GridValue(rowLetterValue, columnValue);
        }

        public bool IsValidShape(Shape shape)
        {
            var triangle = (Triangle) shape;
            // calculates the area of the triangle. uses shoelace formula.
            var result = triangle.TopLeftVertex.X * (triangle.OuterVertex.Y - triangle.BottomRightVertex.Y) +
                         triangle.OuterVertex.X * (triangle.BottomRightVertex.Y - triangle.TopLeftVertex.Y) +
                         triangle.BottomRightVertex.X * (triangle.TopLeftVertex.Y - triangle.OuterVertex.Y);

            return result != 0;
        }
    }
}