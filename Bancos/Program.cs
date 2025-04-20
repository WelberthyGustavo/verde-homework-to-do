﻿using System;

class Program
{
    struct Client
    {
        public int Arrival;
        public int Age;
        public bool Gravida;
        public Client(int a, int age, bool g) { Arrival = a; Age = age; Gravida = g; }
    }

    static void Main()
    {
        // 1) Leitura recursiva
        Client[] clients = ReadClients(0, 0);

        // prepara vetor de flags e dispara agendamento
        bool[] served = new bool[clients.Length];
        int[] order = ServeAll(clients, served, 0, 0, 0);

        // 3) Impressão recursiva
        PrintAges(order, 0);
    }

    // ========== Leitura ==========
    static Client[] ReadClients(int tempoAccum, int lastArrival)
    {
        string line = Console.ReadLine();
        if (line == null || line == "-1")
            return new Client[0];

        var p = line.Split();
        int T = int.Parse(p[0]);
        int age = int.Parse(p[1]);
        bool g = (p.Length == 3 && p[2] == "G");

        int arrival = lastArrival + T;
        Client[] tail = ReadClients(tempoAccum + T, arrival);

        Client[] all = new Client[tail.Length + 1];
        all[0] = new Client(arrival, age, g);
        Array.Copy(tail, 0, all, 1, tail.Length);
        return all;
    }

    // ========== Agendamento ==========
    static int[] ServeAll(Client[] C, bool[] S, int curTime, int nextCat, int servedCount)
    {
        if (servedCount == C.Length)
            return new int[0];

        int idx;
        int i80 = Find80(C, S, curTime, 0);
        int newNextCat = nextCat;
        int finish;

        if (i80 != -1)
        {
            // atende 80+ já disponível
            idx = i80;
            finish = Math.Max(curTime, C[idx].Arrival) + 10;
        }
        else
        {
            // preempção: se um 80+ chegar durante os próximos 10 minutos, aguarda e atende ele
            int next80Arrival = FindNext80Arrival(C, S, curTime, 0, int.MaxValue);
            if (next80Arrival <= curTime + 10)
            {
                idx = FindNext80IndexByArrival(C, S, next80Arrival, 0);
                finish = next80Arrival + 10;
            }
            else
            {
                // ciclo normal idoso->gravida->comum
                int found = FindByCat(C, S, curTime, nextCat, 0);
                if (found != -1)
                {
                    idx = found;
                    finish = Math.Max(curTime, C[idx].Arrival) + 10;
                    newNextCat = (nextCat + 1) % 3;
                }
                else
                {
                    // ninguém elegível: avança para próxima chegada
                    int nextArr = FindNextArr(C, S, curTime, 0, int.MaxValue);
                    return ServeAll(C, S, nextArr, nextCat, servedCount);
                }
            }
        }

        S[idx] = true;
        int[] tail = ServeAll(C, S, finish, newNextCat, servedCount + 1);
        int[] res = new int[tail.Length + 1];
        res[0] = C[idx].Age;
        Array.Copy(tail, 0, res, 1, tail.Length);
        return res;
    }

    static int Find80(Client[] C, bool[] S, int t, int i)
    {
        if (i >= C.Length) return -1;
        if (!S[i] && C[i].Age >= 80 && C[i].Arrival <= t) return i;
        return Find80(C, S, t, i + 1);
    }

    static int FindNext80Arrival(Client[] C, bool[] S, int t, int i, int curMin)
    {
        if (i >= C.Length) return curMin;
        if (!S[i] && C[i].Age >= 80 && C[i].Arrival > t && C[i].Arrival < curMin)
            curMin = C[i].Arrival;
        return FindNext80Arrival(C, S, t, i + 1, curMin);
    }

    static int FindNext80IndexByArrival(Client[] C, bool[] S, int arrival, int i)
    {
        if (i >= C.Length) return -1;
        if (!S[i] && C[i].Age >= 80 && C[i].Arrival == arrival) return i;
        return FindNext80IndexByArrival(C, S, arrival, i + 1);
    }

    static int FindByCat(Client[] C, bool[] S, int t, int cat, int k)
    {
        if (k >= 3) return -1;
        int want = (cat + k) % 3;
        int idx = FindInOneCat(C, S, t, want, 0);
        if (idx != -1) return idx;
        return FindByCat(C, S, t, cat, k + 1);
    }

    static int FindInOneCat(Client[] C, bool[] S, int t, int want, int i)
    {
        if (i >= C.Length) return -1;
        if (!S[i] && C[i].Arrival <= t &&
            ((want == 0 && C[i].Age >= 60 && C[i].Age < 80) ||
             (want == 1 && C[i].Gravida) ||
             (want == 2 && C[i].Age < 60 && !C[i].Gravida)))
            return i;
        return FindInOneCat(C, S, t, want, i + 1);
    }

    static int FindNextArr(Client[] C, bool[] S, int t, int i, int curMin)
    {
        if (i >= C.Length) return curMin;
        if (!S[i] && C[i].Arrival > t && C[i].Arrival < curMin)
            curMin = C[i].Arrival;
        return FindNextArr(C, S, t, i + 1, curMin);
    }

    
    static void PrintAges(int[] A, int i)
    {
        if (i >= A.Length) return;
        Console.WriteLine(A[i]);
        PrintAges(A, i + 1);
    }
}
