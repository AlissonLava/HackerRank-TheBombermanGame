using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    //https://www.hackerrank.com/challenges/one-month-preparation-kit-bomber-man

    public static List<string> bomberMan(int n, List<string> grid)
    {
        if (n < 2) return grid;

        int lin = grid.Count, col = grid[0].Length; //total number of lines & columns

        List<string> fullgrid = new List<string>();
        for(int i = 0; i < lin;i++)
        {
            fullgrid.Add(new string('O', col));
        }               
        
        if (n <= 3)
        {   
            for (int l = 0; l < lin; l++) //cycle through lines
            {
                for(int c = 0; c < col; c++) //cycle through columns/chars
                {
                    if (grid[l].Substring(c,1) == "O") //finds bomb
                    {
                        //PREVIOUS LINE (l-1)
                        
                        if (l > 0) //if not the first line
                        {
                            string pLeft = "", pRight = "";
                                                //1 away from border
                            if (c > 0)          { pLeft = fullgrid[l-1].Substring(0, c); }
                            if (c < col - 1)    { pRight = fullgrid[l-1].Substring(c + 1, col - c - 1); }

                            fullgrid[l - 1] = pLeft + "." + pRight;
                        }
                        //CURRENT LINE (l)

                        string left="", right="", center;
                                            //2 away from border
                        if (c > 1)          { left = fullgrid[l].Substring(0, c - 1); }
                        if (c < col - 2)    { right = fullgrid[l].Substring(c + 2, col - c - 2); }

                        if (c == 0 || c == col - 1){ 
                            center = ".."; //bomb adjacent to border
                        }else{
                            center = "..."; //bomb at least 1 away from border
                        }                        

                        fullgrid[l] = left + center + right;

                        //NEXT LINE (l+1)
                        if (l < lin-1) //if not the last line
                        {
                            string nLeft = "", nRight = "";
                                                //1 away from border
                            if (c > 0)          { nLeft = fullgrid[l+1].Substring(0, c); }
                            if (c < col - 1)    { nRight = fullgrid[l+1].Substring(c + 1, col - c - 1); }

                            fullgrid[l + 1] = nLeft + "." + nRight;
                        }
                    }
                }
            }
        }

        return fullgrid;
    }
}

class Solution
{
    public static void Main(string[] args)
    {
        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int r = Convert.ToInt32(firstMultipleInput[0]);

        int c = Convert.ToInt32(firstMultipleInput[1]);

        int n = Convert.ToInt32(firstMultipleInput[2]);

        List<string> grid = new List<string>();

        for (int i = 0; i < r; i++)
        {
            string gridItem = Console.ReadLine();
            grid.Add(gridItem);
        }

        List<string> result = Result.bomberMan(n, grid);

        Console.WriteLine(String.Join("\n", result));
    }
}
