class Program
{
    static void Main(string[] args)
    {
        int startingNumber = 6;
        Console.WriteLine(factorial(startingNumber));
    }

    static int factorial(int x)
    {
        Console.WriteLine($"Factorial called with number: {x}");

        if (x == 1)
        {
            return 1;
        }
        else
        {
            Console.WriteLine($"{x} * factorial({x} - 1)");
            return x * factorial(x - 1);
        }
    }
}