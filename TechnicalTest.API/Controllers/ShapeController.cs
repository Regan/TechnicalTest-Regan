using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using TechnicalTest.API.DTOs;
using TechnicalTest.Core;
using TechnicalTest.Core.Interfaces;
using TechnicalTest.Core.Models;

namespace TechnicalTest.API.Controllers
{
    /// <summary>
    /// Shape Controller which is responsible for calculating coordinates and grid value.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ShapeController : ControllerBase
    {
        private readonly IShapeFactory _shapeFactory;

        /// <summary>
        /// Constructor of the Shape Controller.
        /// </summary>
        /// <param name="shapeFactory"></param>
        public ShapeController(IShapeFactory shapeFactory)
        {
            _shapeFactory = shapeFactory;
        }

        /// <summary>
        /// Calculates the Coordinates of a shape given the Grid Value.
        /// </summary>
        /// <param name="calculateCoordinatesRequest"></param>   
        /// <returns>A Coordinates response with a list of coordinates.</returns>
        /// <response code="200">Returns the Coordinates response model.</response>
        /// <response code="400">If an error occurred while calculating the Coordinates.</response>   
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Shape))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CalculateCoordinates")]
        [HttpPost]
        public IActionResult CalculateCoordinates([FromBody] CalculateCoordinatesDTO calculateCoordinatesRequest)
        {
            // Get the ShapeEnum and if it is default (ShapeEnum.None) or not triangle, return BadRequest as only Triangle is implemented yet.
            var shapeEnum = (ShapeEnum) calculateCoordinatesRequest.ShapeType;
            if (!shapeEnum.Equals(ShapeEnum.Triangle))
            {
                if (shapeEnum.Equals(ShapeEnum.None) || shapeEnum.Equals(ShapeEnum.Other))
                {
                    return BadRequest("Not yet implemented, only Triangle is supported at the moment.");
                }

                return BadRequest("Only Triangle, None or Other is supported.");
            }

            /*
             checks for supported row and columns.
             ensures that the first value is a-F and then in range of 1-12. e.g. A1, F11 valid. 11F, B13, A1A invalid.
             */
            if (!Regex.IsMatch(calculateCoordinatesRequest.GridValue, "^[a-fA-F]{1}([1-9]|1[012])$"))
            {
                return BadRequest("Only a-f, A-F rows & 1-12 columns are supported.");
            }

            if (calculateCoordinatesRequest.Grid.Size != 10)
            {
                return BadRequest("Only Grid Size 10 is currently supported.");
            }

            //  calls the Calculate function in the shape factory.
            var shape = _shapeFactory.CalculateCoordinates(shapeEnum, new Grid(calculateCoordinatesRequest.Grid.Size),
                new GridValue(calculateCoordinatesRequest.GridValue));

            //  returns BadRequest with error message if the calculate result is null
            if (shape == null)
            {
                return BadRequest("Calculation Result was null");
            }

            //  uses linq query to get coordinates from shape, and then creates new DTOs to a list.
            var coordinateList = shape.Coordinates.Select(coordinate =>
                new CalculateCoordinatesResponseDTO.Coordinate(coordinate.X, coordinate.Y)).ToList();

            return Ok(new CalculateCoordinatesResponseDTO(coordinateList));
        }

        /// <summary>
        /// Calculates the Grid Value of a shape given the Coordinates.
        /// </summary>
        /// <remarks>
        /// A Triangle Shape must have 3 vertices, in this order: Top Left Vertex, Outer Vertex, Bottom Right Vertex.
        /// </remarks>
        /// <param name="gridValueRequest"></param>   
        /// <returns>A Grid Value response with a Row and a Column.</returns>
        /// <response code="200">Returns the Grid Value response model.</response>
        /// <response code="400">If an error occurred while calculating the Grid Value.</response>   
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GridValue))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CalculateGridValue")]
        [HttpPost]
        public IActionResult CalculateGridValue([FromBody] CalculateGridValueDTO gridValueRequest)
        {
            // Get the ShapeEnum and if it is default (ShapeEnum.None) or not triangle, return BadRequest as only Triangle is implemented yet.
            var shapeEnum = (ShapeEnum) gridValueRequest.ShapeType;
            if (!shapeEnum.Equals(ShapeEnum.Triangle))
            {
                if (shapeEnum.Equals(ShapeEnum.None) || shapeEnum.Equals(ShapeEnum.Other))
                {
                    return BadRequest("Not yet implemented, only Triangle is supported at the moment.");
                }

                return BadRequest("Only Triangle, None or Other is supported.");
            }

            if (gridValueRequest.Grid.Size != 10)
            {
                return BadRequest("Only Grid Size 10 is currently supported.");
            }

            // only 0-60 pixels are supported.
            var isNotSupportedPixelRange =
                gridValueRequest.Vertices.Any(vertices => vertices.x is > 60 or < 0 || vertices.y is > 60 or < 0);

            if (isNotSupportedPixelRange)
            {
                return BadRequest("Only 0-60 vertices (pixels) are currently supported.");
            }

            // LINQ expression to get vertices and create new coordinate objects and add them to a list.
            var coordinateList = gridValueRequest.Vertices.Select(vertices => new Coordinate(vertices.x, vertices.y))
                .ToList();

            // Call the function in the shape factory to calculate grid value.
            var gridValue = _shapeFactory.CalculateGridValue(shapeEnum, new Grid(gridValueRequest.Grid.Size),
                new Shape(coordinateList));

            // If the GridValue result is null then return BadRequest with an error message.

            if (gridValue == null)
            {
                return BadRequest("Calculation Result was null.");
            }

            return Ok(new CalculateGridValueResponseDTO(gridValue.Row, gridValue.Column));
        }
    }
}