using System;

namespace Lab3_Sorting_Menu
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isRunning = true;
            Random rnd = new Random();

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("Лабораторная работа №3: Сортировки");
                Console.WriteLine("Выберите алгоритм сортировки:");
                Console.WriteLine(" 1 - Пузырьковая");
                Console.WriteLine(" 2 - Шейкерная");
                Console.WriteLine(" 3 - Вставками");
                Console.WriteLine(" 4 - Выбором");
                Console.WriteLine(" 5 - Быстрая");
                Console.WriteLine(" 6 - Слиянием");
                Console.WriteLine(" 0 - Выход");
                Console.Write("\nВаш выбор: ");

                string input = Console.ReadLine();
                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("\nОшибка: введите число от 0 до 6.");
                    WaitForKey();
                    continue;
                }

                if (choice == 0) { isRunning = false; continue; }
                if (choice < 1 || choice > 6)
                {
                    Console.WriteLine("\nОшибка: вариант должен быть от 1 до 6.");
                    WaitForKey();
                    continue;
                }

                Console.Write("Введите размер массива: ");
                if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
                {
                    Console.WriteLine("\nОшибка: введите положительное целое число.");
                    WaitForKey();
                    continue;
                }

                int[] arr = new int[n];
                Console.WriteLine("\nИсходный массив:");
                for (int i = 0; i < n; i++)
                {
                    arr[i] = rnd.Next(1, 101);
                    Console.Write(arr[i] + " ");
                }

                int[] sorted = (int[])arr.Clone();

                Console.WriteLine("\n\nВыполняется сортировка");

                switch (choice)
                {
                    case 1: BubbleSort(sorted); break;
                    case 2: ShakerSort(sorted); break;
                    case 3: InsertionSort(sorted); break;
                    case 4: SelectionSort(sorted); break;
                    case 5: QuickSort(sorted, 0, n - 1); break;
                    case 6: MergeSort(sorted, 0, n - 1); break;
                }

                Console.WriteLine("\nОтсортированный массив:");
                PrintArray(sorted);

                WaitForKey();
            }

            Console.WriteLine("\nПрограмма завершена");
        }

        static void BubbleSort(int[] a)
        {
            for (int i = 0; i < a.Length - 1; i++)
                for (int j = 0; j < a.Length - 1 - i; j++)
                    if (a[j] > a[j + 1]) Swap(ref a[j], ref a[j + 1]);
        }

        static void ShakerSort(int[] a)
        {
            int left = 0, right = a.Length - 1;
            while (left < right)
            {
                for (int i = left; i < right; i++)
                    if (a[i] > a[i + 1]) Swap(ref a[i], ref a[i + 1]);
                right--;

                for (int i = right; i > left; i--)
                    if (a[i - 1] > a[i]) Swap(ref a[i - 1], ref a[i]);
                left++;
            }
        }

        static void InsertionSort(int[] a)
        {
            for (int i = 1; i < a.Length; i++)
            {
                int key = a[i], j = i - 1;
                while (j >= 0 && a[j] > key)
                {
                    a[j + 1] = a[j];
                    j--;
                }
                a[j + 1] = key;
            }
        }

        static void SelectionSort(int[] a)
        {
            for (int i = 0; i < a.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < a.Length; j++)
                    if (a[j] < a[min]) min = j;

                if (min != i) Swap(ref a[i], ref a[min]);
            }
        }

        static void QuickSort(int[] a, int l, int r)
        {
            if (l >= r) return;
            int p = Partition(a, l, r);
            QuickSort(a, l, p - 1);
            QuickSort(a, p + 1, r);
        }

        static int Partition(int[] a, int l, int r)
        {
            int p = a[r], i = l - 1;
            for (int j = l; j < r; j++)
                if (a[j] < p) Swap(ref a[++i], ref a[j]);
            Swap(ref a[i + 1], ref a[r]);
            return i + 1;
        }

        static void MergeSort(int[] a, int l, int r)
        {
            if (l >= r) return;
            int m = (l + r) / 2;
            MergeSort(a, l, m);
            MergeSort(a, m + 1, r);
            Merge(a, l, m, r);
        }

        static void Merge(int[] a, int l, int m, int r)
        {
            int[] temp = new int[r - l + 1];
            int i = l, j = m + 1, k = 0;

            while (i <= m && j <= r) temp[k++] = a[i] < a[j] ? a[i++] : a[j++];
            while (i <= m) temp[k++] = a[i++];
            while (j <= r) temp[k++] = a[j++];

            for (i = 0; i < temp.Length; i++) a[l + i] = temp[i];
        }

        static void Swap(ref int x, ref int y)
        {
            int t = x; x = y; y = t;
        }

        static void PrintArray(int[] arr)
        {
            foreach (int x in arr) Console.Write(x + " ");
            Console.WriteLine();
        }

        static void WaitForKey()
        {
            Console.WriteLine("\nНажмите Enter для возврата в главное меню...");
            Console.ReadLine();
        }
    }
}