using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace sampleDelegate
{
    class Program
    {
        delegate void delegateSorter(int[] unSorted);
        //All this method does is copy an array from the source to destination so we can hae an unsorted array every time.
        static void reset(int []source, int []destination)
        {
            for (int x = 0; x < source.Length; x++)
                destination[x] = source[x];
        }


        //These 3 functions are all the same function but are timing different sor
        //ts. In this case if we could tell this function
        //which sorting function to use, we could just utilize one. Your job is to repace this function with one function that will
        //Call the appropriate sorting function
        static void timeSortingRoutine(int []unsortedData, delegateSorter dTime)
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();
            dTime(unsortedData);
            sw.Stop();

            TimeSpan ts = sw.Elapsed;

            Console.WriteLine("Time elapsed {0} milliseconds.", ts.TotalMilliseconds);
        }

        ///////////////////////////////////////////////////////////////////////////////////

        static void printArray(int []sortedData)
        {
            foreach (int x in sortedData)
                Console.Write(x + ", ");
        }

        //Bubble sort
        static void bubblesort(int[] unsortedData)
        {
            for(int i = 0; i < unsortedData.Length; i++)
            {
                for(int j = 0; j < unsortedData.Length - 1; j++)
                {
                    if(unsortedData[j] > unsortedData[j+1])
                    {
                        int temp = unsortedData[j+1];
                        unsortedData[j+1] = unsortedData[j];
                        unsortedData[j] = temp;
                    }
                }
            }
        }

        //Selection Sort
        static void selectionSort(int[] unsortedData)
        {
            int largestIndex = 0;

            for(int i = unsortedData.Length - 1; i > 0; i--)
            {
                for(int j = 0; j < i; j++)
                {
                    if (unsortedData[largestIndex] < unsortedData[j])
                        largestIndex = j;
                }
                int temp = unsortedData[i];
                unsortedData[i] = unsortedData[largestIndex];
                unsortedData[largestIndex] = temp;
            }
        }

        //3 functions used for quick sort
        static void Quicksort_Starter(int[] unsortedData)
        {
            QuickSort(unsortedData, 0, unsortedData.Length - 1);
        }

        static void QuickSort(int[] unsortedData, int start, int end)
        {
            int i;
            if (start < end)
            {
                i = Partition(unsortedData, start, end);

                QuickSort(unsortedData, start, i - 1);
                QuickSort(unsortedData, i + 1, end);
            }
        }

        static int Partition(int[] unsortedData, int start, int end)
        {
            int temp;
            int p = unsortedData[end];
            int i = start - 1;

            for (int j = start; j <= end - 1; j++)
            {
                if (unsortedData[j] <= p)
                {
                    i++;
                    temp = unsortedData[i];
                    unsortedData[i] = unsortedData[j];
                    unsortedData[j] = temp;
                }
            }

            temp = unsortedData[i + 1];
            unsortedData[i + 1] = unsortedData[end];
            unsortedData[end] = temp;
            return i + 1;
        }
        ////////////////////////////////////////////////////////////////////////////////

        
        static void Main(string[] args)
        {
            string[] data = File.ReadAllLines("..\\..\\input.txt");
            int[] intdata = new int[data.Length];
            int count = 0;
            foreach (string s in data)
            {
                intdata[count] = Convert.ToInt32(s);
                count++;
                
            }

            int[] copyData = new int[intdata.Length];

            reset(intdata, copyData);


            //In this section we are calling the three different methods. Instead we need to use delegates to create 3
            //pointers to the different sorting routine and passing them to one method to run the sorting mechanism.

            delegateSorter dBubble = new delegateSorter(bubblesort);
            delegateSorter dSelection = new delegateSorter(selectionSort);
            delegateSorter dQuick = new delegateSorter(Quicksort_Starter);

            Console.WriteLine("Bubble Sort");
           timeSortingRoutine(copyData, dBubble);

            reset(intdata, copyData);

            Console.WriteLine("Selection Sort");
            timeSortingRoutine(copyData, dSelection);

            
            reset(intdata, copyData);

            Console.WriteLine("Quick Sort");
            timeSortingRoutine(copyData, dQuick);
;

            Console.WriteLine("Press Enter to exit");
            Console.ReadLine();
        }
    }
}
