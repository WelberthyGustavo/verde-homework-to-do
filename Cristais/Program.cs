using System;

class Program
{
    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());
        string[] colectiOne = new string[n];

        string numbersOne = Console.ReadLine();
        string[] numbersOneArray = numbersOne.Split(' ');

        FillArrayRecursively(colectiOne, numbersOneArray, 0);

        int m = int.Parse(Console.ReadLine());
        string[] colectiTwo = new string[m];

        string numbersTwo = Console.ReadLine();
        string[] numbersTwoArray = numbersTwo.Split(' ');

        FillArrayRecursively(colectiTwo, numbersTwoArray, 0);

        Magic(colectiOne, colectiTwo);
    }

    static void FillArrayRecursively(string[] destination, string[] source, int index)
    {
        if (index >= destination.Length) return;

        destination[index] = source[index];
        FillArrayRecursively(destination, source, index + 1);
    }

    static void Magic(string[] colectiOne, string[] colectiTwo)
    {
        int totalLength = colectiOne.Length + colectiTwo.Length;
        string[] combined = new string[totalLength];

        MergeArraysRecursively(colectiOne, colectiTwo, combined, 0, 0);

        BubbleSortRecursively(combined, combined.Length);

        PrintArrayRecursively(combined, 0);
    }

    static void MergeArraysRecursively(string[] arr1, string[] arr2, string[] result, int index1, int index2)
    {
        if (index1 < arr1.Length)
        {
            result[index1] = arr1[index1];
            MergeArraysRecursively(arr1, arr2, result, index1 + 1, index2);
        }
        else if (index2 < arr2.Length)
        {
            result[index1 + index2] = arr2[index2];
            MergeArraysRecursively(arr1, arr2, result, index1, index2 + 1);
        }
    }

    static void BubbleSortRecursively(string[] arr, int n)
    {
        if (n == 1) return;

        BubblePass(arr, 0, n);

        BubbleSortRecursively(arr, n - 1);
    }

    static void BubblePass(string[] arr, int i, int n)
    {
        if (i >= n - 1) return;

        if (int.Parse(arr[i]) > int.Parse(arr[i + 1]))
        {
            string temp = arr[i];
            arr[i] = arr[i + 1];
            arr[i + 1] = temp;
        }

        BubblePass(arr, i + 1, n);
    }

    static void PrintArrayRecursively(string[] arr, int index)
    {
        if (index >= arr.Length) return;

        Console.WriteLine(arr[index]);
        PrintArrayRecursively(arr, index + 1);
    }
}