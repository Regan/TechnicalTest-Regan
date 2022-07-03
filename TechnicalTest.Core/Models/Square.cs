namespace TechnicalTest.Core.Models;

public class Square : Shape
{
    public Square(Coordinate topLeftVertex, Coordinate topRightVertex, Coordinate bottomLeftVertex, Coordinate bottomRightVertex)
    {
        Coordinates.AddRange(new List<Coordinate>{topLeftVertex, topRightVertex, bottomLeftVertex, bottomRightVertex});
        TopLeftVertex = topLeftVertex;
        TopRightVertex = topRightVertex;
        BottomLeftVertex = bottomLeftVertex;
        BottomRightVertex = bottomRightVertex;
    }

    public Coordinate TopLeftVertex { get; set; }
    public Coordinate TopRightVertex { get; set; }
    public Coordinate BottomLeftVertex { get; set; }
    public Coordinate BottomRightVertex { get; set; }
}