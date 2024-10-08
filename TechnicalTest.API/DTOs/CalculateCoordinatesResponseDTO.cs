﻿namespace TechnicalTest.API.DTOs
{
    public class CalculateCoordinatesResponseDTO
    {
        public List<Coordinate> Coordinates { get; set; }
        
        public CalculateCoordinatesResponseDTO(List<Coordinate> coordinates)
        {
            Coordinates = coordinates;
        }
        public class Coordinate
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Coordinate(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }
}
