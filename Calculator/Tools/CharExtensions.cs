namespace Calculator.Tools
{
    public static class CharExtensions
    {
        public static bool IsDigit(this char c)
        {
            return char.IsDigit(c);
        }

        public static bool IsEmpty(this char c)
        {
            return c.Equals((char) 32); // ''
        }

        public static bool IsDot(this char c)
        {
            return c.Equals('.');
        }
    }
}