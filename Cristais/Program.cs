using System;

class Program
{
    static int[] v1;
    static int[] v2;

    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        string[] partes1 = Console.ReadLine().Split(' ');
        v1 = new int[n];
        PreencherVetor(v1, partes1, 0);

        int m = int.Parse(Console.ReadLine());
        string[] partes2 = Console.ReadLine().Split(' ');
        v2 = new int[m];
        PreencherVetor(v2, partes2, 0);

        UnirOrdenado(0, 0);
    }

    static void PreencherVetor(int[] vetor, string[] partes, int i)
    {
        if (i >= partes.Length) return;
        vetor[i] = int.Parse(partes[i]);
        PreencherVetor(vetor, partes, i + 1);
    }

    static void UnirOrdenado(int i, int j)
    {
        if (i >= v1.Length && j >= v2.Length) return;

        if (i < v1.Length && (j >= v2.Length || v1[i] < v2[j]))
        {
            Console.WriteLine(v1[i]);
            UnirOrdenado(i + 1, j);
        }
        else
        {
            Console.WriteLine(v2[j]);
            UnirOrdenado(i, j + 1);
        }
    }
}
