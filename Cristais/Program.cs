using System;

class Program
{
    static int[] v1;
    static int[] v2;

    static void Main(){
        int n = int.Parse(Console.ReadLine());
        v1 = new int[n];
        LerVetor(v1, 0, n);

        int m = int.Parse(Console.ReadLine());
        v2 = new int[m];
        LerVetor(v2, 0, m);

        UnirOrdenado(0, 0, n, m);
    }

    static void LerVetor(int[] vetor, int i, int tamanho){
        if (i >= tamanho) return;
        string[] partes = Console.ReadLine().Split(' ');
        PreencherVetor(vetor, partes, 0);
    }

    static void PreencherVetor(int[] vetor, string[] partes, int i){
        if (i >= partes.Length) return;
        vetor[i] = int.Parse(partes[i]);
        PreencherVetor(vetor, partes, i + 1);
    }

    static void UnirOrdenado(int i, int j, int n, int m){
        if (i >= n && j >= m) return;

        if (i < n && (j >= m || v1[i] < v2[j])){
            Console.WriteLine(v1[i]);
            UnirOrdenado(i + 1, j, n, m);
        }
        else{
            Console.WriteLine(v2[j]);
            UnirOrdenado(i, j + 1, n, m);
        }
    }
}
