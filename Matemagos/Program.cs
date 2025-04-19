using System;

class Program
{
    static int[] nums;

    static void Main()
    {
        int quant = int.Parse(Console.ReadLine());
        nums = new int[quant];
        LerEntradas(0, quant);
        ImprimirResultados(0, quant);
    }

    static void LerEntradas(int idx, int total)
    {
        if (idx >= total) return;
        nums[idx] = int.Parse(Console.ReadLine());
        LerEntradas(idx + 1, total);
    }

    static void ImprimirResultados(int idx, int total)
    {
        if (idx >= total) return;
        if (nums[idx] == 0)
        {
            Console.WriteLine("0");
        }
        else
        {
            ConverterParaBinario(nums[idx]);
            Console.WriteLine();
        }
        ImprimirResultados(idx + 1, total);
    }

    static void ConverterParaBinario(int num)
    {
        if (num == 0) return;
        ConverterParaBinario(num / 2);
        Console.Write(num % 2);
    }
}
