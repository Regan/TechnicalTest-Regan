namespace TechnicalTest.Core.Models
{
    public class GridValue
    {
        public GridValue(string gridValue)
        {
            // originally was max length = 2. e.g. could only do A1-A9
            if (string.IsNullOrEmpty(gridValue) || gridValue.Length > 3) return;

            Row = gridValue[..1];
            Column = int.Parse(gridValue[1..]);
            /*
             * range of values.
             * Row = First string value.
             * Column = everything after first string value.
             */
        }


        public GridValue(int row, int column)
        {
            var numericValueOfCharacter = (char)64 + row;
            Row = ((char)numericValueOfCharacter).ToString();
            Column = column;
        }

        public string? Row { get; set; }

        public int Column { get; set; }

        public int GetNumericRow() => Row != null ? char.ToUpper(char.Parse(Row)) - 64 : 0; // converts letter to numeric value. e.g. A = 1, B = 2. etc
    }
}
