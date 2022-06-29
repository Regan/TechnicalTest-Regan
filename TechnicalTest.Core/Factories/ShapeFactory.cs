using TechnicalTest.Core.Interfaces;
using TechnicalTest.Core.Models;

namespace TechnicalTest.Core.Factories
{
    public class ShapeFactory : IShapeFactory
    {
	    private readonly IShapeService _shapeService;

        public ShapeFactory(IShapeService shapeService)
        {
	        _shapeService = shapeService;
        }

        public Shape? CalculateCoordinates(ShapeEnum shapeEnum, Grid grid, GridValue gridValue)
        {
            // using pattern matching for shapeEnum values, returns shape from service.
            return shapeEnum switch
            {
                ShapeEnum.Triangle =>
                    _shapeService.ProcessTriangle(grid, gridValue),
                ShapeEnum.Other => null, // not yet implemented
                ShapeEnum.None => null, // not yet implemented
                _ => throw new ArgumentOutOfRangeException(nameof(shapeEnum), shapeEnum, "Out of range, shape not supported.")
            };
        }

        public GridValue? CalculateGridValue(ShapeEnum shapeEnum, Grid grid, Shape shape)
        {
            switch (shapeEnum)
            {
                case ShapeEnum.Triangle:
                    if (shape.Coordinates.Count != 3) return null;
                    var triangle = new Triangle(shape.Coordinates[ListVertexPositions.TopLeftVertexPos], shape.Coordinates[ListVertexPositions.OuterVertexPos], shape.Coordinates[ListVertexPositions.BottomRightVertex]);
                    return _shapeService.ProcessGridValueFromTriangularShape(grid, triangle);
                case ShapeEnum.Other:
                    return null; // not yet implemented
                case ShapeEnum.None:
                    return null; // not yet implemented
                default: throw new ArgumentOutOfRangeException(nameof(shapeEnum), shapeEnum, "Out of range, shape not supported.");
            }
        }
    }
}
