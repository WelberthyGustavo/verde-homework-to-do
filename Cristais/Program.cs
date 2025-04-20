using System;

class Program
{
    static int[] v1;
    static int[] v2;

    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        string[] linha1 = Console.ReadLine().Split(' ');
        v1 = new int[n];
        Preencher(v1, linha1, 0);

        int m = int.Parse(Console.ReadLine());
        string[] linha2 = Console.ReadLine().Split(' ');
        v2 = new int[m];
        Preencher(v2, linha2, 0);

        Juntar(0, 0);
    }

    static void Preencher(int[] vetor, string[] valores, int i)
    {
        if (i >= valores.Length) return;
        vetor[i] = int.Parse(valores[i]);
        Preencher(vetor, valores, i + 1);
    }

    static void Juntar(int i, int j)
    {
        if (i >= v1.Length && j >= v2.Length) return;

        if (i < v1.Length && (j >= v2.Length || v1[i] < v2[j]))
        {
            Console.WriteLine(v1[i]);
            Juntar(i + 1, j);
        }
        else
        {
            Console.WriteLine(v2[j]);
            Juntar(i, j + 1);
        }
    }
}
