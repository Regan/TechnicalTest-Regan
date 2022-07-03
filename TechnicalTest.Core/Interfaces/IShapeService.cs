using TechnicalTest.Core.Models;

namespace TechnicalTest.Core.Interfaces
{
    public interface IShapeService
    {
        Shape ProcessShape(Grid grid, GridValue gridValue);

        GridValue ProcessGridValue(Grid grid, Shape shape);

        bool IsValidShape(Shape shape);
    }
}