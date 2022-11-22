using System;
using System.Collections.Generic;

namespace Maximal_Rectangle
{

  // https://www.youtube.com/watch?v=oaN9ibZKMpA - Explanation found here
  class Program
  {
    static void Main(string[] args)
    {
      /*
       * [["1","0","1","0","0"],["1","0","1","1","1"],["1","1","1","1","1"],["1","0","0","1","0"]]
       */
    }
  }


  public class Solution
  {
    public int MaximalRectangle(char[][] matrix)
    {
      int m = matrix.Length;
      int n = matrix[0].Length;
      var height = new int[m][];
      for (int i = 0; i < m; i++)
      {
        height[i] = new int[n];
        for (int j = 0; j < n; j++)
        {
          height[i][j] = matrix[i][j] - '0';
          if (i > 0)
          {
            if (height[i][j] > 0) height[i][j] += height[i - 1][j];
            else height[i][j] = 0;
          }
        }
      }

      int max = 0;
      foreach (var item in height) max = Math.Max(max, LargestRectangleArea(item));

      return max;
    }

    int LargestRectangleArea(int[] heights)
    {
      int maxArea = 0;
      Stack<(int, int)> stack = new Stack<(int, int)>();
      for (int i = 0; i < heights.Length; i++)
      {
        int currentIndex = i;
        int currentHeight = heights[i];
        while (stack.Count > 0 && stack.Peek().Item2 > currentHeight)
        {
          var (prevIndex, prevHeight) = stack.Pop();
          int area = prevHeight * (i - prevIndex);
          maxArea = Math.Max(maxArea, area);
          currentIndex = prevIndex;
        }
        stack.Push((currentIndex, currentHeight));
      }

      int size = heights.Length;
      while (stack.Count > 0)
      {
        var (prevIndex, prevHeight) = stack.Pop();
        int area = prevHeight * (size - prevIndex);
        maxArea = Math.Max(maxArea, area);
      }
      return maxArea;
    }
  }
}
