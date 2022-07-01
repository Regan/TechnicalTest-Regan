using TechnicalTest.Core.Models;
using TechnicalTest.Core.Services;
using Xunit;

namespace TechnicalTest.Core.Tests.Services
{
    public class ShapeServiceTests
    {
        private readonly ShapeService _shapeService = new();

        [Fact]
        public void GivenGridValueA1WhenProcessingLeftTriangleThenNumberOfCoordinatesIs3()
        {
            var gridValue = new GridValue("A1");
            var grid = new Grid(10);

            var shape = _shapeService.ProcessTriangle(grid, gridValue);

            Assert.NotNull(shape);
            Assert.Equal(3, shape.Coordinates.Count);
        }

        [Fact]
        public void GivenGridValueA1AndGridSize10WhenProcessingLeftTriangleThenCoordinatesAreValid()
        {
            var gridValue = new GridValue("A1");
            var grid = new Grid(10);

            var shape = _shapeService.ProcessTriangle(grid, gridValue);

            Assert.NotNull(shape);
            Assert.Contains(shape.Coordinates, (c) => c.X == 0 && c.Y == 0);
            Assert.Contains(shape.Coordinates, (c) => c.X == 0 && c.Y == 10);
            Assert.Contains(shape.Coordinates, (c) => c.X == 10 && c.Y == 10);
        }

        [Fact]
        public void GivenGridValueA2AndGridSize10WhenProcessingRightTriangleThenCoordinatesAreValid()
        {
            var gridValue = new GridValue("A2");
            var grid = new Grid(10);

            var shape = _shapeService.ProcessTriangle(grid, gridValue);

            Assert.NotNull(shape);
            Assert.Contains(shape.Coordinates, (c) => c.X == 0 && c.Y == 0);
            Assert.Contains(shape.Coordinates, (c) => c.X == 10 && c.Y == 10);
            Assert.Contains(shape.Coordinates, (c) => c.X == 10 && c.Y == 10);
        }

        [Fact]
        public void GivenGridValueD5AndGridSize10WhenProcessingLeftTriangleThenCoordinatesAreValid()
        {
            var gridValue = new GridValue("D5");
            var grid = new Grid(10);

            var shape = _shapeService.ProcessTriangle(grid, gridValue);

            Assert.NotNull(shape);
            Assert.Contains(shape.Coordinates, (c) => c.X == 20 && c.Y == 30);
            Assert.Contains(shape.Coordinates, (c) => c.X == 20 && c.Y == 40); // fixed test - was using same value as topleftVert
            Assert.Contains(shape.Coordinates, (c) => c.X == 30 && c.Y == 40);
        }

        [Fact]
        public void GivenGridValueD6AndGridSize10WhenProcessingRightTriangleThenCoordinatesAreValid()
        {
            var gridValue = new GridValue("D6");
            var grid = new Grid(10);

            var shape = _shapeService.ProcessTriangle(grid, gridValue);

            Assert.NotNull(shape);
            Assert.Contains(shape.Coordinates, (c) => c.X == 20 && c.Y == 30);
            Assert.Contains(shape.Coordinates, (c) => c.X == 30 && c.Y == 30);
            Assert.Contains(shape.Coordinates, (c) => c.X == 30 && c.Y == 40);
        }
        [Fact]
        public void GivenGridValueF8AndGridSize10WhenProcessingRightTriangleThenCoordinatesAreValid()
        {
            var gridValue = new GridValue("F8");
            var grid = new Grid(10);

            var shape = _shapeService.ProcessTriangle(grid, gridValue);

            Assert.NotNull(shape);
            Assert.Contains(shape.Coordinates, (c) => c.X == 30 && c.Y == 50);
            Assert.Contains(shape.Coordinates, (c) => c.X == 40 && c.Y == 50);
            Assert.Contains(shape.Coordinates, (c) => c.X == 40 && c.Y == 60);
        }
        [Fact]
        public void GivenGridValueF9AndGridSize10WhenProcessingRightTriangleThenCoordinatesAreValid()
        {
            var gridValue = new GridValue("F9");
            var grid = new Grid(10);

            var shape = _shapeService.ProcessTriangle(grid, gridValue);

            Assert.NotNull(shape);
            Assert.Contains(shape.Coordinates, (c) => c.X == 40 && c.Y == 50);
            Assert.Contains(shape.Coordinates, (c) => c.X == 40 && c.Y == 60);
            Assert.Contains(shape.Coordinates, (c) => c.X == 50 && c.Y == 60);
        }
        
        [Fact]
        public void GivenGridValueF10AndGridSize10WhenProcessingRightTriangleThenCoordinatesAreValid()
        {
            var gridValue = new GridValue("F10");
            var grid = new Grid(10);

            var shape = _shapeService.ProcessTriangle(grid, gridValue);

            Assert.NotNull(shape);
            Assert.Contains(shape.Coordinates, (c) => c.X == 40 && c.Y == 50);
            Assert.Contains(shape.Coordinates, (c) => c.X == 50 && c.Y == 50);
            Assert.Contains(shape.Coordinates, (c) => c.X == 50 && c.Y == 60);
        }
        
        [Fact]
        public void GivenGridValueF11AndGridSize10WhenProcessingRightTriangleThenCoordinatesAreValid()
        {
            var gridValue = new GridValue("F11");
            var grid = new Grid(10);

            var shape = _shapeService.ProcessTriangle(grid, gridValue);

            Assert.NotNull(shape);
            Assert.Contains(shape.Coordinates, (c) => c.X == 50 && c.Y == 50);
            Assert.Contains(shape.Coordinates, (c) => c.X == 50 && c.Y == 60);
            Assert.Contains(shape.Coordinates, (c) => c.X == 60 && c.Y == 60);
        }

        [Fact]
        public void GivenGridValueF12AndGridSize10WhenProcessingRightTriangleThenCoordinatesAreValid()
        {
            var gridValue = new GridValue("F12");
            var grid = new Grid(10);

            var shape = _shapeService.ProcessTriangle(grid, gridValue);

            Assert.NotNull(shape);
            Assert.Contains(shape.Coordinates, (c) => c.X == 50 && c.Y == 50);
            Assert.Contains(shape.Coordinates, (c) => c.X == 60 && c.Y == 50);
            Assert.Contains(shape.Coordinates, (c) => c.X == 60 && c.Y == 60);
        }
        
        [Fact]
        public void GivenD5TriangleCoordinatesWhenProcessingGridValueThenGridValueIsD5()
        {
            var grid = new Grid(10);
            var triangle = new Triangle(new Coordinate(20, 30), new Coordinate(20, 40), new Coordinate(30, 40)); // was missing type

            var gridValue = _shapeService.ProcessGridValueFromTriangularShape(grid, triangle);

            Assert.NotNull(gridValue);
            Assert.Equal(5, gridValue.Column);
            Assert.Equal("D", gridValue.Row);
        }

        [Fact]
        public void GivenD6TriangleCoordinatesWhenProcessingGridValueThenGridValueIsD6()
        {
            var grid = new Grid(10);
            var triangle = new Triangle(new Coordinate(20, 30), new Coordinate(30, 30), new Coordinate(30, 40)); // was missing type

            var gridValue = _shapeService.ProcessGridValueFromTriangularShape(grid, triangle);

            Assert.NotNull(gridValue);
            Assert.Equal(6, gridValue.Column);
            Assert.Equal("D", gridValue.Row);
        }

        [Fact]
        public void GivenA1TriangleCoordinatesWhenProcessingGridValueThenGridValueIsA1()
        {
            var grid = new Grid(10);
            var triangle = new Triangle(new Coordinate(0, 0), new Coordinate(0, 10), new Coordinate(10, 10)); 

            var gridValue = _shapeService.ProcessGridValueFromTriangularShape(grid, triangle);

            Assert.NotNull(gridValue);
            Assert.Equal(1, gridValue.Column);
            Assert.Equal("A", gridValue.Row);
        }

        [Fact]
        public void GivenF8TriangleCoordinatesWhenProcessingGridValueThenGridValueIsF8()
        {
            var grid = new Grid(10);
            var triangle = new Triangle(new Coordinate(30, 50), new Coordinate(40, 50), new Coordinate(40, 60));

            var gridValue = _shapeService.ProcessGridValueFromTriangularShape(grid, triangle);

            Assert.NotNull(gridValue);
            Assert.Equal(8, gridValue.Column);
            Assert.Equal("F", gridValue.Row);
        }
        
        [Fact]
        public void GivenF9TriangleCoordinatesWhenProcessingGridValueThenGridValueIsF9()
        {
            var grid = new Grid(10);
            var triangle = new Triangle(new Coordinate(40, 50), new Coordinate(40, 60), new Coordinate(50, 60));

            var gridValue = _shapeService.ProcessGridValueFromTriangularShape(grid, triangle);

            Assert.NotNull(gridValue);
            Assert.Equal(9, gridValue.Column);
            Assert.Equal("F", gridValue.Row);
        }
        [Fact]
        public void GivenF10TriangleCoordinatesWhenProcessingGridValueThenGridValueIsF10()
        {
            var grid = new Grid(10);
            var triangle = new Triangle(new Coordinate(40, 50), new Coordinate(50, 50), new Coordinate(50, 60));

            var gridValue = _shapeService.ProcessGridValueFromTriangularShape(grid, triangle);

            Assert.NotNull(gridValue);
            Assert.Equal(10, gridValue.Column);
            Assert.Equal("F", gridValue.Row);
        }

        [Fact]
        public void GivenF11TriangleCoordinatesWhenProcessingGridValueThenGridValueIsF11()
        {
            var grid = new Grid(10);
            var triangle = new Triangle(new Coordinate(50, 50), new Coordinate(50, 60), new Coordinate(60, 60));

            var gridValue = _shapeService.ProcessGridValueFromTriangularShape(grid, triangle);

            Assert.NotNull(gridValue);
            Assert.Equal(11, gridValue.Column);
            Assert.Equal("F", gridValue.Row);
        }
        
        [Fact]
        public void GivenF12TriangleCoordinatesWhenProcessingGridValueThenGridValueIsF12()
        {
            var grid = new Grid(10);
            var triangle = new Triangle(new Coordinate(50, 50), new Coordinate(60, 50), new Coordinate(60, 60));

            var gridValue = _shapeService.ProcessGridValueFromTriangularShape(grid, triangle);

            Assert.NotNull(gridValue);
            Assert.Equal(12, gridValue.Column);
            Assert.Equal("F", gridValue.Row);
        }
        
        
        [Fact]
        public void GivenDotCoordinatesWhenCheckingIsValidShapeThenFalse()
        {
            var grid = new Grid(10);
            var triangle = new Triangle(new Coordinate(0, 0), new Coordinate(0, 0), new Coordinate(0, 0));

            var gridValue = _shapeService.IsValidShape(triangle);

            Assert.False(gridValue);
        }
        
        [Fact]
        public void GivenXLineCoordinatesWhenCheckingIsValidShapeThenFalse()
        {
            var grid = new Grid(10);
            var triangle = new Triangle(new Coordinate(0, 0), new Coordinate(10, 0), new Coordinate(20, 0));

            var gridValue = _shapeService.IsValidShape(triangle);

            Assert.False(gridValue);
        }
        
        [Fact]
        public void GivenA1CoordinatesWhenCheckingIsValidShapeThenTrue()
        {
            var grid = new Grid(10);
            var triangle = new Triangle(new Coordinate(0, 0), new Coordinate(0, 10), new Coordinate(10, 10)); 

            var gridValue = _shapeService.IsValidShape(triangle);

            Assert.True(gridValue);
        }
        
        [Fact]
        public void GivenD5CoordinatesWhenCheckingIsValidShapeThenTrue()
        {
            var grid = new Grid(10);
            var triangle = new Triangle(new Coordinate(20, 30), new Coordinate(20, 40), new Coordinate(30, 40));

            var gridValue = _shapeService.IsValidShape(triangle);

            Assert.True(gridValue);
        }
        
        [Fact]
        public void GivenD6CoordinatesWhenCheckingIsValidShapeThenTrue()
        {
            var grid = new Grid(10);
            var triangle = new Triangle(new Coordinate(20, 30), new Coordinate(30, 30), new Coordinate(30, 40));

            var gridValue = _shapeService.IsValidShape(triangle);

            Assert.True(gridValue);
        }
        
        [Fact]
        public void GivenF12CoordinatesWhenCheckingIsValidShapeThenTrue()
        {
            var grid = new Grid(10);
            var triangle = new Triangle(new Coordinate(50, 50), new Coordinate(60, 50), new Coordinate(60, 60));

            var gridValue = _shapeService.IsValidShape(triangle);

            Assert.True(gridValue);
        }
        
        [Fact]
        public void GivenDotCoordinatesWhenProcessingGridValueThenGridValueIsInvalid()
        {
            var grid = new Grid(10);
            var triangle = new Triangle(new Coordinate(0, 0), new Coordinate(0, 0), new Coordinate(0, 0));

            var gridValue = _shapeService.ProcessGridValueFromTriangularShape(grid, triangle);

            Assert.Null(gridValue);
        }
        
        [Fact]
        public void GivenStraightXLineCoordinatesWhenProcessingGridValueThenGridValueIsInvalid()
        {
            var grid = new Grid(10);
            var triangle = new Triangle(new Coordinate(0, 0), new Coordinate(10, 0), new Coordinate(20, 0));

            var gridValue = _shapeService.ProcessGridValueFromTriangularShape(grid, triangle);

            Assert.Null(gridValue);
        }
        
        [Fact]
        public void GivenStraightYLineCoordinatesWhenProcessingGridValueThenGridValueIsInvalid()
        {
            var grid = new Grid(10);
            var triangle = new Triangle(new Coordinate(0, 10), new Coordinate(0, 10), new Coordinate(20, 0));

            var gridValue = _shapeService.ProcessGridValueFromTriangularShape(grid, triangle);

            Assert.Null(gridValue);
        }
        
        
        
    }
}