using System;
using System.Collections.Generic;

class Program
{
    class Pessoa
    {
        public int TempoAbsoluto;
        public int Idade;
        public bool Gravida;

        public Pessoa(int tempoAbsoluto, int idade, bool gravida)
        {
            TempoAbsoluto = tempoAbsoluto;
            Idade = idade;
            Gravida = gravida;
        }
    }

    static void Main()
    {
        var fila80Mais = new Queue<Pessoa>();
        var filaIdosos = new Queue<Pessoa>();
        var filaGravidas = new Queue<Pessoa>();
        var filaComum = new Queue<Pessoa>();
        var entradas = new List<Pessoa>();

        // Leitura da entrada
        int tempoRelativo = 0;
        while (true)
        {
            string linha = Console.ReadLine();
            if (linha == null || linha == "-1") break;

            string[] partes = linha.Split();
            int tempo = int.Parse(partes[0]);
            int idade = int.Parse(partes[1]);
            bool gravida = partes.Length == 3 && partes[2] == "G";

            tempoRelativo += tempo;
            entradas.Add(new Pessoa(tempoRelativo, idade, gravida));
        }

        int tempoAtual = 0;
        int indiceEntrada = 0;
        int contadorCircular = 0; 

        while (indiceEntrada < entradas.Count || 
               fila80Mais.Count > 0 || filaIdosos.Count > 0 || 
               filaGravidas.Count > 0 || filaComum.Count > 0)
        {
          
            while (indiceEntrada < entradas.Count && entradas[indiceEntrada].TempoAbsoluto <= tempoAtual)
            {
                var pessoa = entradas[indiceEntrada];

                if (pessoa.Idade >= 80)
                    fila80Mais.Enqueue(pessoa);
                else if (pessoa.Idade >= 60)
                    filaIdosos.Enqueue(pessoa);
                else if (pessoa.Gravida)
                    filaGravidas.Enqueue(pessoa);
                else
                    filaComum.Enqueue(pessoa);

                indiceEntrada++;
            }

            Pessoa atendida = null;

            
            if (fila80Mais.Count > 0)
            {
                atendida = fila80Mais.Dequeue();
            }
            else
            {
           
                for (int i = 0; i < 3; i++)
                {
                    int atual = (contadorCircular + i) % 3;

                    if (atual == 0 && filaIdosos.Count > 0)
                    {
                        atendida = filaIdosos.Dequeue();
                        contadorCircular = (atual + 1) % 3;
                        break;
                    }
                    if (atual == 1 && filaGravidas.Count > 0)
                    {
                        atendida = filaGravidas.Dequeue();
                        contadorCircular = (atual + 1) % 3;
                        break;
                    }
                    if (atual == 2 && filaComum.Count > 0)
                    {
                        atendida = filaComum.Dequeue();
                        contadorCircular = (atual + 1) % 3;
                        break;
                    }
                }
            }

            if (atendida != null)
            {
                Console.WriteLine(atendida.Idade);
                tempoAtual += 10;
            }
            else
            {
                
                if (indiceEntrada < entradas.Count)
                    tempoAtual = entradas[indiceEntrada].TempoAbsoluto;
            }
        }
    }
}
