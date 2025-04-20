﻿using System;

class Program
{
    struct Cliente
    {
        public int Chegada;
        public int Idade;
        public bool Gravida;
        public Cliente(int chegada, int idade, bool gravida)
        {
            Chegada = chegada;
            Idade = idade;
            Gravida = gravida;
        }
    }

    static void Main()
    {
        Cliente[] clientes = LerClientes(0, 0);

        bool[] atendidos = new bool[clientes.Length];
        int[] ordem = AtenderTodos(clientes, atendidos, 0, 0, 0);

        ImprimirIdades(ordem, 0);
    }

    static Cliente[] LerClientes(int tempoAcumulado, int ultimaChegada)
    {
        string linha = Console.ReadLine();
        if (linha == null || linha == "-1")
            return new Cliente[0];

        var partes = linha.Split();
        int tempo = int.Parse(partes[0]);
        int idade = int.Parse(partes[1]);
        bool gravida = (partes.Length == 3 && partes[2] == "G");

        int chegada = ultimaChegada + tempo;
        Cliente[] resto = LerClientes(tempoAcumulado + tempo, chegada);

        Cliente[] todos = new Cliente[resto.Length + 1];
        todos[0] = new Cliente(chegada, idade, gravida);
        Array.Copy(resto, 0, todos, 1, resto.Length);
        return todos;
    }

    static int[] AtenderTodos(Cliente[] clientes, bool[] atendidos, int tempoAtual, int proximaCategoria, int atendidosCount)
    {
        if (atendidosCount == clientes.Length)
            return new int[0];

        int idx;
        int i80 = Buscar80Mais(clientes, atendidos, tempoAtual, 0);
        int novaProximaCategoria = proximaCategoria;
        int fim;

        if (i80 != -1)
        {
            idx = i80;
            fim = Math.Max(tempoAtual, clientes[idx].Chegada) + 10;
        }
        else
        {
            int chegada80 = BuscarProximaChegada80Mais(clientes, atendidos, tempoAtual, 0, int.MaxValue);
            if (chegada80 <= tempoAtual + 10)
            {
                idx = BuscarIndicePorChegada80Mais(clientes, atendidos, chegada80, 0);
                fim = chegada80 + 10;
            }
            else
            {
                int encontrado = BuscarPorCategoria(clientes, atendidos, tempoAtual, proximaCategoria, 0);
                if (encontrado != -1)
                {
                    idx = encontrado;
                    fim = Math.Max(tempoAtual, clientes[idx].Chegada) + 10;
                    novaProximaCategoria = (proximaCategoria + 1) % 3;
                }
                else
                {
                    int proximaChegada = BuscarProximaChegada(clientes, atendidos, tempoAtual, 0, int.MaxValue);
                    return AtenderTodos(clientes, atendidos, proximaChegada, proximaCategoria, atendidosCount);
                }
            }
        }

        atendidos[idx] = true;
        int[] resto = AtenderTodos(clientes, atendidos, fim, novaProximaCategoria, atendidosCount + 1);
        int[] resultado = new int[resto.Length + 1];
        resultado[0] = clientes[idx].Idade;
        Array.Copy(resto, 0, resultado, 1, resto.Length);
        return resultado;
    }

    static int Buscar80Mais(Cliente[] clientes, bool[] atendidos, int tempo, int i)
    {
        if (i >= clientes.Length) return -1;
        if (!atendidos[i] && clientes[i].Idade >= 80 && clientes[i].Chegada <= tempo) return i;
        return Buscar80Mais(clientes, atendidos, tempo, i + 1);
    }

    static int BuscarProximaChegada80Mais(Cliente[] clientes, bool[] atendidos, int tempo, int i, int menor)
    {
        if (i >= clientes.Length) return menor;
        if (!atendidos[i] && clientes[i].Idade >= 80 && clientes[i].Chegada > tempo && clientes[i].Chegada < menor)
            menor = clientes[i].Chegada;
        return BuscarProximaChegada80Mais(clientes, atendidos, tempo, i + 1, menor);
    }

    static int BuscarIndicePorChegada80Mais(Cliente[] clientes, bool[] atendidos, int chegada, int i)
    {
        if (i >= clientes.Length) return -1;
        if (!atendidos[i] && clientes[i].Idade >= 80 && clientes[i].Chegada == chegada) return i;
        return BuscarIndicePorChegada80Mais(clientes, atendidos, chegada, i + 1);
    }

    static int BuscarPorCategoria(Cliente[] clientes, bool[] atendidos, int tempo, int categoria, int k)
    {
        if (k >= 3) return -1;
        int desejado = (categoria + k) % 3;
        int idx = BuscarEmUmaCategoria(clientes, atendidos, tempo, desejado, 0);
        if (idx != -1) return idx;
        return BuscarPorCategoria(clientes, atendidos, tempo, categoria, k + 1);
    }

    static int BuscarEmUmaCategoria(Cliente[] clientes, bool[] atendidos, int tempo, int categoria, int i)
    {
        if (i >= clientes.Length) return -1;
        if (!atendidos[i] && clientes[i].Chegada <= tempo &&
            ((categoria == 0 && clientes[i].Idade >= 60 && clientes[i].Idade < 80) ||
             (categoria == 1 && clientes[i].Gravida) ||
             (categoria == 2 && clientes[i].Idade < 60 && !clientes[i].Gravida)))
            return i;
        return BuscarEmUmaCategoria(clientes, atendidos, tempo, categoria, i + 1);
    }

    static int BuscarProximaChegada(Cliente[] clientes, bool[] atendidos, int tempo, int i, int menor)
    {
        if (i >= clientes.Length) return menor;
        if (!atendidos[i] && clientes[i].Chegada > tempo && clientes[i].Chegada < menor)
            menor = clientes[i].Chegada;
        return BuscarProximaChegada(clientes, atendidos, tempo, i + 1, menor);
    }

    static void ImprimirIdades(int[] idades, int i)
    {
        if (i >= idades.Length) return;
        Console.WriteLine(idades[i]);
        ImprimirIdades(idades, i + 1);
    }
}
