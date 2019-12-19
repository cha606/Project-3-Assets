using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject cell;
    public int mazeSize;
    public GameObject player;
    void Start()
    {
        //Creates empty maze
        Maze maze = new Maze(mazeSize, mazeSize, cell, player);
        maze.generateMaze();
    }
}

class Cell
{
    //{left, right, top, bottom};
    //false is closed, true is open
    public bool[] sides = { false, false, false, false };
    public int rowIndex;
    public int colIndex;

    public Cell(int x, int y)
    {
        rowIndex = x;
        colIndex = y;
    }
    public void openSide(int sideIndex)
    {
        sides[sideIndex] = true;

    }
}

class Maze
{
    private System.Random randy = new System.Random();
    private Stack<Cell> cellPath = new Stack<Cell>();
    private Cell[,] mazeCells;
    private int numOfRows;
    private int numOfCols;
    private int currRow; //current row index of generator
    private int currCol; //current col index of generator
    private GameObject cell;
    private GameObject player;

    public Maze(int rowCount, int colCount, GameObject whatever, GameObject p)
    {
        //Initializes maze size and picks random location
        numOfRows = rowCount;
        numOfCols = colCount;
        mazeCells = new Cell[numOfRows, numOfCols];
        currRow = randy.Next(0, numOfRows);
        currCol = randy.Next(0, numOfCols);
        player = p;

        //Generates cell in maze and add onto stack
        mazeCells[currRow, currCol] = new Cell(currRow, currCol);
        cellPath.Push(mazeCells[currRow, currCol]);
        cell = whatever;
    }

    void generateNextCell()
    {
        //Below code adds a list of valid paths to branch off into, just adds randomness

        List<int> validDirections = new List<int>();
        //If there is a cell to the left
        if (currCol - 1 != -1 && mazeCells[currRow, currCol - 1] == null)
            validDirections.Add(0);
        //If there is a cell to the right
        if (currCol + 1 < numOfRows && mazeCells[currRow, currCol + 1] == null)
            validDirections.Add(1);
        //If there is a cell above
        if (currRow - 1 != -1 && mazeCells[currRow - 1, currCol] == null)
            validDirections.Add(2);
        //If there is a cell below
        if (currRow + 1 < numOfRows && mazeCells[currRow + 1, currCol] == null)
            validDirections.Add(3);

        //If a dead end is reached, start retracing steps
        if (validDirections.Count == 0)
        {
            Cell poppedCell = cellPath.Pop();
            currRow = poppedCell.rowIndex;
            currCol = poppedCell.colIndex;
        }
        else
        { //Based on direction, change currRow & currCol
            switch (validDirections[randy.Next(validDirections.Count)])
            {
                case 0:
                    mazeCells[currRow, currCol].openSide(0);
                    cellPath.Push(mazeCells[currRow, currCol]);
                    currCol--; //col - 1
                    mazeCells[currRow, currCol] = new Cell(currRow, currCol);
                    mazeCells[currRow, currCol].openSide(1);
                    break;
                case 1:
                    mazeCells[currRow, currCol].openSide(1);
                    cellPath.Push(mazeCells[currRow, currCol]);
                    currCol++;//col + 1
                    mazeCells[currRow, currCol] = new Cell(currRow, currCol);
                    mazeCells[currRow, currCol].openSide(0);
                    break;
                case 2:
                    mazeCells[currRow, currCol].openSide(2);
                    cellPath.Push(mazeCells[currRow, currCol]);
                    currRow--; //row - 1
                    mazeCells[currRow, currCol] = new Cell(currRow, currCol);
                    mazeCells[currRow, currCol].openSide(3);
                    break;
                case 3:
                    mazeCells[currRow, currCol].openSide(3);
                    cellPath.Push(mazeCells[currRow, currCol]);
                    currRow++; //row + 1
                    mazeCells[currRow, currCol] = new Cell(currRow, currCol);
                    mazeCells[currRow, currCol].openSide(2);
                    break;
                default:
                    break;
            }
        }
    }

    private void createExits()
    {
        int[] opening;
        int[] exit;

        //Adds cell the ghetto way
        List<Cell> outerCells = new List<Cell>();
        for (int x = 0; x < numOfRows; x++)
        {
            for (int y = 0; y < numOfCols; y++)
            {
                if (x == 0 || x == numOfRows - 1 || y == 0 || y == numOfCols - 1)
                {
                    outerCells.Add(mazeCells[x, y]);
                }
            }
        }

        //Picks the first exit and depending on its location, choose the right side to open
        int index = randy.Next(outerCells.Count);
        opening = new int[] { outerCells[index].rowIndex, outerCells[index].colIndex };
        if (opening[0] == 0)
            mazeCells[opening[0], opening[1]].openSide(2);
        else if (opening[0] == numOfRows - 1)
            mazeCells[opening[0], opening[1]].openSide(3);
        else if (opening[1] == 0)
            mazeCells[opening[0], opening[1]].openSide(0);
        else if (opening[1] == numOfCols - 1)
            mazeCells[opening[0], opening[1]].openSide(1);
        else
            Console.WriteLine("Exit machine broke");

        //Removes previous exit from valid exit list so you don't have duplicate exits
        //Resets index and opening location and create second exit.
        outerCells.RemoveAt(index);
        index = randy.Next(outerCells.Count);
        exit = new int[] { outerCells[index].rowIndex, outerCells[index].colIndex };
        if (exit[0] == 0)
            mazeCells[exit[0], exit[1]].openSide(2);
        else if (exit[0] == numOfRows - 1)
            mazeCells[exit[0], exit[1]].openSide(3);
        else if (exit[1] == 0)
            mazeCells[exit[0], exit[1]].openSide(0);
        else if (exit[1] == numOfCols - 1)
            mazeCells[exit[0], exit[1]].openSide(1);
        else
            Console.WriteLine("Exit machine broke");

        player.transform.position = new Vector3(opening[1] * 25f, 2f, opening[0] * 25f); //moves deh player do the entranx cell.
    }

    public void generateMaze()
    {
        while (cellPath.Count != 0)
            generateNextCell();
        createExits();

        for (int row = 0; row < numOfRows; row++)
        {
            for (int col = 0; col < numOfCols; col++)
            {
                UnityEngine.Object.Instantiate(cell, new Vector3(col * 25f, 0f, row * 25f), new Quaternion(0f, 0f, 0f, 0f)).GetComponent<CellSideFixer>().deleteSides(mazeCells[row, col].sides);
            }
        }
    }
}
