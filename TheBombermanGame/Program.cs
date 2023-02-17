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

        //List<string> PlantedBombsPrev;
        //List<string> PlantedBombsCurr = grid;
        //List<string> Explosionfield = new List<string>(grid);
        //List<string> ExplosionRemains = grid;
        List<string> FullBombs = new List<string>();
        

        for (int i = 0; i < lin;i++) //just fills the Explosionfield
        {
            FullBombs.Add(new string('O', col));
        }

        if (n % 2 == 0) return FullBombs;
        int sec = 2;
        //seconds here
        while (n > sec)
        {
            sec++;
            if (sec % 2 != 0)
            {
                //PlantedBombsPrev = new List<string>(Explosionfield);
                //PlantedBombsCurr = PlantsBombs(Explosionfield);
                //Explosionfield = ExplodeBombs(Explosionfield, FullBombs);
                grid = ExplodeBombs(grid,FullBombs);
            }

            Console.WriteLine(sec);//debug
            Console.WriteLine(String.Join("\n", grid));
        } 

        return grid;
    }

    private static List<string> PlantsBombs(List<string> ExplodedGrid)
    {
        List<string> PlantedBombs = ExplodedGrid;

        for (int i = 0; i < PlantedBombs.Count; i++) //plants new bombs
        {
            PlantedBombs[i] = PlantedBombs[i].Replace("O", "o");
            PlantedBombs[i] = PlantedBombs[i].Replace(".", "O");
            PlantedBombs[i] = PlantedBombs[i].Replace("o", ".");
        }
        return PlantedBombs;
    }

    private static List<string> ExplodeBombs(List<string> BombsToExplode, List<string> Grid)
    {
        int lin = BombsToExplode.Count, col = BombsToExplode[0].Length;

        for (int l = 0; l < lin; l++) //cycle through lines
        {
            for (int c = 0; c < col; c++) //cycle through columns/chars
            {
                if (BombsToExplode[l].Substring(c, 1) == "O") //finds bomb
                {
                    //PREVIOUS LINE (l-1)

                    if (l > 0) //if not the first line
                    {
                        string pLeft = "", pRight = "";
                        //1 away from border
                        if (c > 0) { pLeft = Grid[l - 1].Substring(0, c); }
                        if (c < col - 1) { pRight = Grid[l - 1].Substring(c + 1, col - c - 1); }

                        Grid[l - 1] = pLeft + "." + pRight;
                    }
                    //CURRENT LINE (l)

                    string left = "", right = "", center;
                    //2 away from border
                    if (c > 1) { left = Grid[l].Substring(0, c - 1); }
                    if (c < col - 2) { right = Grid[l].Substring(c + 2, col - c - 2); }

                    if (c == 0 || c == col - 1)
                    {
                        center = ".."; //bomb adjacent to border
                    }
                    else
                    {
                        center = "..."; //bomb at least 1 away from border
                    }

                    Grid[l] = left + center + right;

                    //NEXT LINE (l+1)
                    if (l < lin - 1) //if not the last line
                    {
                        string nLeft = "", nRight = "";
                        //1 away from border
                        if (c > 0) { nLeft = Grid[l + 1].Substring(0, c); }
                        if (c < col - 1) { nRight = Grid[l + 1].Substring(c + 1, col - c - 1); }

                        Grid[l + 1] = nLeft + "." + nRight;
                    }
                }
            }
        }

        return Grid;
    }
}

class Solution
{
    public static void Main(string[] args)
    {
        /*
         * string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int r = Convert.ToInt32(firstMultipleInput[0]);

        int c = Convert.ToInt32(firstMultipleInput[1]);

        int n = Convert.ToInt32(firstMultipleInput[2]);

        List<string> grid = new List<string>();

        for (int i = 0; i < r; i++)
        {
            string gridItem = Console.ReadLine();
            grid.Add(gridItem);
        }
            */

        int n;
        List<string> grid = new List<string>();

        grid.Clear();
        n =5;        
        grid.Add(".......");
        grid.Add("...O.O.");
        grid.Add("....O..");
        grid.Add("..O....");
        grid.Add("OO...OO");
        grid.Add("OO.O...");


        //grid.Clear();
        //n = 3;
        //grid.Add(".......");
        //grid.Add("...O...");
        //grid.Add("....O..");
        //grid.Add(".......");
        //grid.Add("OO.....");
        //grid.Add("OO.....");



        List<string> result = Result.bomberMan(n, grid);

        Console.WriteLine("\n\n\nResult\n");

       Console.WriteLine(String.Join("\n", result));
    }
}
