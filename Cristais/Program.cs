using System;

class Program
{
    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());
        string[] colecaoUm = new string[n];

        string entradaUm = Console.ReadLine();
        string[] numerosUm = entradaUm.Split(' ');

        PreencherRecursivamente(colecaoUm, numerosUm, 0);

        int m = int.Parse(Console.ReadLine());
        string[] colecaoDois = new string[m];

        string entradaDois = Console.ReadLine();
        string[] numerosDois = entradaDois.Split(' ');

        PreencherRecursivamente(colecaoDois, numerosDois, 0);

        JuntarOrdenarEExibir(colecaoUm, colecaoDois);
    }

    static void PreencherRecursivamente(string[] destino, string[] origem, int indice)
    {
        if (indice >= destino.Length) return;

        destino[indice] = origem[indice];
        PreencherRecursivamente(destino, origem, indice + 1);
    }

    static void JuntarOrdenarEExibir(string[] colecaoUm, string[] colecaoDois)
    {
        int tamanhoTotal = colecaoUm.Length + colecaoDois.Length;
        string[] combinado = new string[tamanhoTotal];

        JuntarColecoes(colecaoUm, colecaoDois, combinado, 0, 0);

        OrdenarBubbleRecursivo(combinado, combinado.Length);

        ExibirRecursivamente(combinado, 0);
    }

    static void JuntarColecoes(string[] colecaoUm, string[] colecaoDois, string[] resultado, int indiceUm, int indiceDois)
    {
        if (indiceUm < colecaoUm.Length)
        {
            resultado[indiceUm] = colecaoUm[indiceUm];
            JuntarColecoes(colecaoUm, colecaoDois, resultado, indiceUm + 1, indiceDois);
        }
        else if (indiceDois < colecaoDois.Length)
        {
            resultado[indiceUm + indiceDois] = colecaoDois[indiceDois];
            JuntarColecoes(colecaoUm, colecaoDois, resultado, indiceUm, indiceDois + 1);
        }
    }

    static void OrdenarBubbleRecursivo(string[] vetor, int n)
    {
        if (n == 1) return;

        PassoBubble(vetor, 0, n);

        OrdenarBubbleRecursivo(vetor, n - 1);
    }

    static void PassoBubble(string[] vetor, int i, int n)
    {
        if (i >= n - 1) return;

        if (int.Parse(vetor[i]) > int.Parse(vetor[i + 1]))
        {
            string temp = vetor[i];
            vetor[i] = vetor[i + 1];
            vetor[i + 1] = temp;
        }

        PassoBubble(vetor, i + 1, n);
    }

    static void ExibirRecursivamente(string[] vetor, int indice)
    {
        if (indice >= vetor.Length) return;

        Console.WriteLine(vetor[indice]);
        ExibirRecursivamente(vetor, indice + 1);
    }
}