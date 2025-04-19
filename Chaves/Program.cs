using System;
using System.Collections.Generic;

namespace EnigmaDasTresChaves
{
    class Program
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            List<string> expressoes = LerExpressoes(n, new List<string>());
            ValidarEExibir(expressoes, 0);
        }

        static List<string> LerExpressoes(int restante, List<string> acumuladas)
        {
            if (restante == 0) return acumuladas;

            string expressao = Console.ReadLine();
            acumuladas.Add(expressao);
            return LerExpressoes(restante - 1, acumuladas);
        }

        static void ValidarEExibir(List<string> expressoes, int indice)
        {
            if (indice == expressoes.Count) return;

            string expressao = expressoes[indice];
            string resultado = VerificarExpressao(expressao, new List<char>(), 0) ? "Expressão válida" : "Expressão inválida";
            Console.WriteLine(resultado);

            ValidarEExibir(expressoes, indice + 1);
        }

        static bool VerificarExpressao(string expressao, List<char> pilha, int indice)
        {
            if (indice == expressao.Length)
                return pilha.Count == 0;

            char atual = expressao[indice];

            if (EhAbertura(atual))
            {
                pilha.Add(atual);
            }
            else if (EhFechamento(atual))
            {
                if (pilha.Count == 0) return false;

                char topo = pilha[pilha.Count - 1];
                if (!Combina(topo, atual)) return false;

                pilha.RemoveAt(pilha.Count - 1); 
            }

            return VerificarExpressao(expressao, pilha, indice + 1);
        }

        static bool EhAbertura(char c) => c == '(' || c == '[' || c == '{';
        static bool EhFechamento(char c) => c == ')' || c == ']' || c == '}';
        static bool Combina(char abre, char fecha)
        {
            return (abre == '(' && fecha == ')') ||
                   (abre == '[' && fecha == ']') ||
                   (abre == '{' && fecha == '}');
        }
    }
}
