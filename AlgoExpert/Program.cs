using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AlgoExpert
{
    public class Program
    {
        public class BinaryTree
        {
            public int value;
            public BinaryTree left;
            public BinaryTree right;

            public BinaryTree(int value)
            {
                this.value = value;
                this.left = null;
                this.right = null;
            }
        }

        public static bool IsMonotonic(int[] array)
        {
            if (array.Length == 0 || array.Length == 1) return true;
            int index = 0;
           
            bool ascending = array[0] - array[1] < 0;
            bool founddifferent = false;
            while (index < array.Length - 1 && !founddifferent)
            {
                if (array[index] != array[index + 1])
                {
                    founddifferent = true;
                }
                else
                {
                    index++;
                }
            }
            if (!founddifferent) return true;
            for(int i=index+2; i<array.Length-1; i++)
            {
                if (ascending && array[i] > array[i + 1]) return false;
                if (!ascending && array[i] < array[i + 1]) return false;
                return true;
            }
            return false;
        }


        public static int LongestPeak(int[] array)
        {
            int longestPeakLength = 0;
            int i = 1;
            if (array.Length < 3) return -1;
            while (i < array.Length - 1)
            {
                bool isPeak = array[i - 1] < array[i] && array[i] > array[i + 1];
                if(!isPeak)
                {
                    i++;
                    continue;
                }
                int leftIndex = i - 2;
                int rightIndex = i + 2;

                while(leftIndex>=0 && array[leftIndex] < array[leftIndex + 1])
                {
                    leftIndex -= 1;
                }

                while (rightIndex < array.Length && array[rightIndex] > array[rightIndex - 1])
                {
                    rightIndex += 1;
                }

                int currentLength = rightIndex - leftIndex - 1;
                if (currentLength > longestPeakLength) longestPeakLength = currentLength;

                i = rightIndex;
            }

            return longestPeakLength;
        }


        public int[] ArrayOfProducts(int[] array)
        {
            for(int i=0; i< array.Length; i++)
            {
                if (i == 0) array[i] = Prod(array, 1, array.Length);
                if (i > 0)
                {
                    int prodBefore = Prod(array, 0, i - 1);
                    int prodAfter = Prod(array, i + 1, array.Length - 1);
                    array[i] = prodBefore * prodAfter;
                }
            }
            return array;
        }

        public static int Prod(int[] array, int indexStart, int indexFinish)
        {
            int prod = 1;
            for(int i= indexStart; i<=indexFinish; i++)
            {
                prod *= array[i];
            }
            return prod;
        }


        public int FirstDuplicateValue(int[] array)
        {
            // Array.Sort(array);
            
            
            for(int i = 0; i< array.Length; i++)
            {
                bool found = false;
                int index = i+1;
                while(!found && index < array.Length)
                {
                    if(array[i] == array[index])
                    {
                        found = true;
                        return array[i];
                    }
                    index++;
                }
                if (found) break;

            }
            return -1;
        }

        public int[][] MergeOverlappingIntervals(int[][] intervals)
        {
            int[][] sortedIntervals = intervals.Clone() as int[][];
            Array.Sort(sortedIntervals, (a, b) => a[0].CompareTo(b[0]));

            List<int[]> mergedIntervals = new List<int[]>();
            int[] currentInterval = sortedIntervals[0];

            mergedIntervals.Add(currentInterval);

            foreach(var nextInterval in sortedIntervals)
            {
                int currentIntervalEnd = currentInterval[1];
                int nextIntervalStart = nextInterval[0];
                int nextIntervalEnd = nextInterval[1];

                if (currentIntervalEnd >= nextIntervalStart)
                {
                    currentInterval[1] = Math.Max(currentIntervalEnd, nextIntervalEnd);
                }
                else
                {
                    currentInterval = nextInterval;
                    mergedIntervals.Add(currentInterval);
                }
            }
            // Write your code here.
            return mergedIntervals.ToArray();
        }


        public static int BinarySearch(int[] array, int target)
        {
            int left = 0;
            int right = array.Length - 1;
            int med = (left + right) / 2;
            bool found = false;
            while (!found && left<=right)
            {
                if(array[med] > target)
                {
                    left = med + 1;
                    med = (left + right) / 2;
                }
                else if (array[med] > target)
                {
                    right = med - 1;
                    med = (left + right) / 2;
                }
                else
                {
                    found = true;
                    return med;
                }
            }

            return -1;
        }

        public static int[] FindThreeLargestNumbers(int[] array)
        {
            // Write your code here.
            if (array.Length < 3) return new int[] { };

            int first = Int32.MinValue;
            int second = first;
            int third = second;
            for(int i=0; i<array.Length; i++)
            {
                if (array[i] > first)
                {
                    third = second;
                    second = first;
                    first = array[i];
                }
                else if (array[i] > second)
                {
                    third = second;
                    second = array[i];
                }
                else if (array[i] > third)
                {
                    third = array[i];
                }
            }
            int[] arrayToreturn = new int[3] { first, second, third };
            Array.Sort(arrayToreturn);
            return arrayToreturn;
        }

        public static int[] SearchInSortedMatrix(int[,] matrix, int target)
        {
            int row = 0;


            bool found = false;
            while (!found && row < matrix.GetLength(0))
            {
                int col = 0;
                while (!found && col < matrix.GetLength(1) && row < matrix.GetLength(0))
                {
                    if (matrix[row, col] == target)
                    {
                        found = true;
                        return new int[] { row, col };
                    }
                    else if (matrix[row, col] < target)
                    {
                        col++;
                    }
                    else
                    {
                        row++;
                        col = 0;
                    }

                }
                row++;
            }
            return new int[] { -1, -1 };
        }

        public static int GetNthFib(int n)
        {
            if (n == 2) return 1;
            if (n == 1) return 0;
            return GetNthFib(n - 1) + GetNthFib(n - 2);
        }

        public static List<int> SpiralTraverse(int[,] array)
        {
            int[] onedimensional = new int[array.GetLength(0) * array.GetLength(1)];
            int pointer = 0;
           for(int i=0; i<array.GetLength(0); i++)
            {
                for(int j=0; j<array.GetLength(1); j++)
                {
                    onedimensional[pointer] = array[i, j];
                    pointer++;
                }
            }
            Array.Sort(onedimensional);
            return onedimensional.ToList();
        }

        public static List<int> BranchSums(BinaryTree root)
        {
            List<int> sums = new List<int>();
            calculateBranchSum(root, 0, sums);

            return sums;
        }

        public static void calculateBranchSum(BinaryTree node, int sum, List<int> sums)
        {
            if (node == null) return;
            sum += node.value;
            if (node.left == null && node.right == null)
            {
                sums.Add(sum);
                return;
            }


            calculateBranchSum(node.left, sum, sums);
            calculateBranchSum(node.right, sum, sums);
        }


        public static List<int[]> ThreeNumberSum(int[] array, int targetSum)
        {
            Array.Sort(array);
            List<int[]> list = new List<int[]>();
            for (int i = 0; i < array.Length - 2; i++)
            {
                for (int j = i + 1; j < array.Length - 1; j++)
                {
                    for (int k = j + 1; k < array.Length; k++)
                    {
                        if (array[i] + array[j] + array[k] == targetSum)
                        {
                            list.Add(new int[] { array[i], array[j], array[k] });
                        }
                    }
                }
            }
            return list;
        }


        public static int[] SmallestDifference(int[] arrayOne, int[] arrayTwo)
        {
            Array.Sort(arrayOne);
            Array.Sort(arrayTwo);

            int min = Int32.MaxValue;
            int[] smallestPair = new int[2];
            bool zerofound = false;
            int currentOne = 0;
            int currentTwo = 0;
            int difference = 0;
            while (!zerofound && currentOne < arrayOne.Length && currentTwo < arrayTwo.Length)
            {

                if (Math.Abs(arrayOne[currentOne] - arrayTwo[currentTwo]) == 0)
                {
                    zerofound = true;
                    smallestPair[0] = arrayOne[currentOne];
                    smallestPair[1] = arrayTwo[currentTwo];

                }
                else if (arrayOne[currentOne] < arrayTwo[currentTwo])
                {
                    difference = arrayTwo[currentTwo] - arrayOne[currentOne];
                    currentOne++;
                }
                else if (arrayOne[currentOne] > arrayTwo[currentTwo])
                {
                    difference = arrayOne[currentOne] - arrayTwo[currentTwo];
                    currentTwo++;
                }
                if (difference < min)
                {
                    min = difference;
                    smallestPair[0] = arrayOne[currentOne];
                    smallestPair[1] = arrayTwo[currentTwo];
                }

            }
            return smallestPair;
            // [1, 3 ,7, 9]
            // [0, 2, 4, 6]
        }

        public static List<int> MoveElementToEnd(List<int> array, int toMove)
        {
            array.Sort();
            List<int> helper = new List<int>();
            int firstIndex = array.IndexOf(toMove);
            int count = array.Where(item => item == toMove).Count();

            foreach (var item in array)
            {
                if (item != toMove) helper.Add(item);
            }
            for (int i = 1; i <= count; i++)
            {
                helper.Add(toMove);
            }

            array = helper;
            return array;
        }

        public static int[] TwoNumberSum(int[] array, int targetSum)
        {
            bool found = false;

            int i = 0;
            while (i < array.Length - 1 && found == false)
            {
                int j = i + 1;
                while (j < array.Length && found == false)
                {
                    if (array[i] + array[j] == targetSum)
                    {
                        found = true;
                        return new int[2] { array[i], array[j] };
                    }
                    j++;
                }
                i++;
            }
            return new int[0];
        }

        public int NonConstructibleChange(int[] coins)
        {
            Array.Sort(coins);
            if (coins.Length == 0) return 1;

            int sum = 0;
            int minChange = 0;
            for (int i = 0; i < coins.Length; i++)
            {
                if (coins[i] > minChange + 1) return minChange + 1;
                minChange += coins[i];
            }
            return minChange + 1;
        }



        public int[] SortedSquaredArray(int[] array)
        {

            int[] arrayB = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                arrayB[i] = array[i] * array[i];
            }
            Array.Sort(arrayB);
            return arrayB;
        }

        public string TournamentWinner(List<List<string>> competitions, List<int> results)
        {
            Hashtable scores = new Hashtable();


            var teamsAndResult = competitions.Zip(results, (c, r) => new { Teams = c, Result = r });
            int MAXSCORE = -1;
            string bestTeam = string.Empty;
            foreach (var item in teamsAndResult)
            {
                if (item.Result == 1)
                {
                    if (!scores.ContainsKey(item.Teams.First()))
                    {
                        scores.Add(item.Teams.First(), 3);
                    }
                    else
                    {
                        int old = (int)scores[item.Teams.First()];
                        scores[item.Teams.First()] = old + 3;
                    }
                    if ((int)scores[item.Teams.First()] > MAXSCORE)
                    {
                        MAXSCORE = (int)scores[item.Teams.First()];
                        bestTeam = item.Teams.First();
                    }

                }
                else
                {
                    if (!scores.ContainsKey(item.Teams.Last()))
                    {
                        scores.Add(item.Teams.Last(), 3);
                    }
                    else
                    {
                        int old = (int)scores[item.Teams.Last()];
                        scores[item.Teams.Last()] = old + 3;
                    }
                    if ((int)scores[item.Teams.Last()] > MAXSCORE)
                    {
                        MAXSCORE = (int)scores[item.Teams.Last()];
                        bestTeam = item.Teams.Last();
                    }

                }
            }
            // Write your code here.
            return bestTeam;
        }
        public static void Main(string[] args)
        {
            var matrix = new int[,]{{1, 4, 7, 12, 15, 1000 },
                                    { 2, 5, 19, 31, 32, 1001},
                                    {3, 8, 24, 33, 35, 1002},
                                    {40, 41, 42, 44, 45, 1003},
                                    {99, 100, 103, 106, 128, 1004}};

            var x = SearchInSortedMatrix(matrix, 43);
        }
    }
}
