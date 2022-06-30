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
                if (gridValue.Column == 10) // number 10 needs to use a different formula.
                { 
                    topLeftVertex = new Coordinate(((gridValue.Column / 2)) * grid.Size, (gridValue.GetNumericRow() - adjustPosition) * grid.Size);
                    outerVertex = new Coordinate((gridValue.Column / 2) * grid.Size, gridValue.GetNumericRow() * grid.Size);
                    bottomRightVertex = new Coordinate((gridValue.Column / 2 + adjustPosition) * grid.Size, gridValue.GetNumericRow() * grid.Size);
                    return new Triangle(topLeftVertex, outerVertex, bottomRightVertex);
                }
                topLeftVertex = new Coordinate(((gridValue.Column / 2) - adjustPosition) * grid.Size, (gridValue.GetNumericRow() - adjustPosition) * grid.Size);
                outerVertex = new Coordinate((gridValue.Column / 2) * grid.Size, (gridValue.GetNumericRow() - 1) * grid.Size);
                bottomRightVertex = new Coordinate((gridValue.Column / 2) * grid.Size, gridValue.GetNumericRow() * grid.Size);
                return new Triangle(topLeftVertex, outerVertex, bottomRightVertex);
            }
            // bottom left triangle formula
            topLeftVertex = new Coordinate(gridValue.Column / 2 * grid.Size, (gridValue.GetNumericRow() - adjustPosition) * grid.Size); 
            outerVertex = new Coordinate(gridValue.Column / 2 * grid.Size, gridValue.GetNumericRow() * grid.Size); 
            bottomRightVertex = new Coordinate((gridValue.Column / 2 + adjustPosition) * grid.Size, gridValue.GetNumericRow() * grid.Size);
            return new Triangle(topLeftVertex, outerVertex, bottomRightVertex);
        }

        public GridValue ProcessGridValueFromTriangularShape(Grid grid, Triangle triangle)
        {
            // TODO: Calculate the grid value.
            return new GridValue(0, 0);
        }
    }
}