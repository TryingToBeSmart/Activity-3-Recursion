class Program
{
    static void Main(string[] args)
    {
        int x = 400, y = 85;
        Console.WriteLine($"The Greatest Common Divisor of {x} and {y} is {GreatestCommonDivisor(x, y)}");
    }

    private static int GreatestCommonDivisor(int x, int y)
    {
        Console.WriteLine($"first number: {x}, second number: {y}");
        if (y == 0)
            return x;
        else
        {
            Console.WriteLine($"Remainder of {x} and {y} is {x % y}");
            return GreatestCommonDivisor(y, x % y);
        }
    }
}