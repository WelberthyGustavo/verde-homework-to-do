using System;
using System.Collections.Generic;

class Program
{
    static List<(int Time, int Age, bool IsPregnant)> clients = new List<(int, int, bool)>();
    static List<int> over80Queue = new List<int>();
    static List<int> elderlyQueue = new List<int>();
    static List<int> pregnantQueue = new List<int>();
    static List<int> regularQueue = new List<int>();
    static List<int> servedOrder = new List<int>();
    static int currentTime = 0;
    static int state = 0; // 0: elderly, 1: pregnant, 2: regular

    static void Main()
    {
        ReadInput(0);
        ProcessClients(0, 0);
        PrintOutput(0);
    }

    static void ReadInput(int index)
    {
        string[] input = Console.ReadLine().Split();
        int time = int.Parse(input[0]);
        
        if (time == -1) return;

        int age = int.Parse(input[1]);
        bool isPregnant = input.Length > 2 && input[2] == "G";
        clients.Add((time, age, isPregnant));

        ReadInput(index + 1);
    }

    static void ProcessClients(int clientIndex, int nextServiceTime)
    {
        if (clientIndex < clients.Count)
        {
            var (arrival, age, isPregnant) = clients[clientIndex];
            int adjustedArrival = clientIndex == 0 ? 0 : clients[clientIndex - 1].Time + arrival;

            // Add client to the appropriate queue
            if (age >= 80)
                over80Queue.Add(age);
            else if (age >= 60)
                elderlyQueue.Add(age);
            else if (isPregnant)
                pregnantQueue.Add(age);
            else
                regularQueue.Add(age);

            ProcessClients(clientIndex + 1, nextServiceTime);
        }

        ServeClients(nextServiceTime);
    }

    static void ServeClients(int nextServiceTime)
    {
        if (nextServiceTime > currentTime)
        {
            currentTime = nextServiceTime;
        }

        // Find the next client to arrive after the current time
        int nextArrival = int.MaxValue;
        FindNextArrival(0, ref nextArrival);

        if (nextArrival == int.MaxValue && over80Queue.Count == 0 && 
            elderlyQueue.Count == 0 && pregnantQueue.Count == 0 && regularQueue.Count == 0)
        {
            return; // No more clients to process
        }

        // If there's someone in the 80+ queue, serve them first
        if (over80Queue.Count > 0)
        {
            ServeFromQueue(over80Queue, nextServiceTime);
            ServeClients(nextServiceTime + 10);
            return;
        }

        // Normal rotation: elderly -> pregnant -> regular
        if (state == 0 && elderlyQueue.Count > 0)
        {
            ServeFromQueue(elderlyQueue, nextServiceTime);
            state = 1;
            ServeClients(nextServiceTime + 10);
        }
        else if (state == 1 && pregnantQueue.Count > 0)
        {
            ServeFromQueue(pregnantQueue, nextServiceTime);
            state = 2;
            ServeClients(nextServiceTime + 10);
        }
        else if (state == 2 && regularQueue.Count > 0)
        {
            ServeFromQueue(regularQueue, nextServiceTime);
            state = 0;
            ServeClients(nextServiceTime + 10);
        }
        else
        {
            // Skip to the next non-empty queue
            state = (state + 1) % 3;
            if (state == 0 && elderlyQueue.Count == 0 ||
                state == 1 && pregnantQueue.Count == 0 ||
                state == 2 && regularQueue.Count == 0)
            {
                state = (state + 1) % 3;
            }
            ServeClients(nextServiceTime);
        }
    }

    static void ServeFromQueue(List<int> queue, int serviceTime)
    {
        if (queue.Count > 0)
        {
            servedOrder.Add(queue[0]);
            queue.RemoveAt(0);
        }
    }

    static void FindNextArrival(int index, ref int nextArrival)
    {
        if (index >= clients.Count) return;

        int adjustedArrival = index == 0 ? 0 : clients[index - 1].Time + clients[index].Time;
        if (adjustedArrival > currentTime && adjustedArrival < nextArrival)
        {
            nextArrival = adjustedArrival;
        }

        FindNextArrival(index + 1, ref nextArrival);
    }

    static void PrintOutput(int index)
    {
        if (index >= servedOrder.Count) return;

        Console.WriteLine($"{index + 1}\t{servedOrder[index]}");
        PrintOutput(index + 1);
    }
}