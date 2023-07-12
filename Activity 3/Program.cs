class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter and integer. I will do some math and arrive at 1");
        int input = int.Parse(Console.ReadLine());
        int output = CountToOne(input);
    }

    private static int CountToOne(int input)
    {
        Console.WriteLine($"Number is {input}");
        if (input == 1)
        {
            return 1;
        }
        else
        {
            if (input % 2 == 0)
            {
                Console.WriteLine($"{input} is even, divide by 2");
                return CountToOne(input / 2);
            }
            else
            {
                Console.WriteLine($"{input} is odd, add 1");
                return CountToOne(input + 1);
            }
        }
    }
}