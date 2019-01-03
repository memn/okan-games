using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

public struct Position
{
    public int x;
    public int y;

    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

public struct Puzzle
{
    private const int MaxHeight = 5;
    public int width;
    public int height;
    public char[,] puzzleData;
    public string word;


    public Puzzle(string word, int maxHeight = MaxHeight)
    {
        this.word = word;
        var size = (int) (Math.Round(Math.Sqrt(word.Length)));
        height = size;
        width = size;
        if (size > maxHeight)
        {
            height = maxHeight;
            width = word.Length / height;
        }

        if (height * width < word.Length) width++;

        Assert.IsTrue(height * width >= word.Length);

        puzzleData = new char[width, height];
        Array.Clear(puzzleData, 0, height * width);
    }
}

public class PuzzleMaker : MonoBehaviour
{
    private static readonly Position Up = new Position(-1, 0);
    private static readonly Position Right = new Position(0, 1);
    private static readonly Position Down = new Position(1, 0);

    private static readonly Position Left = new Position(0, -1);
    private static readonly Position[] Sides = {Up, Right, Down, Left};

    //public static string word = "Siyer, İslam dini literatüründe peygamberlerin, din büyüklerinin ve halifelerin hayat hikâyesidir.";

    public static Puzzle MakePuzzle(string word)
    {
        var puzzle = new Puzzle(word);

        //Position position = new Position(randomNumber.Next(size-1), randomNumber.Next(size-1) );
        var position = new Position(0, 0);

        AddNewCharToPuzzle(0, position, puzzle);
        PrintPuzzle(puzzle);
        return puzzle;
    }

    private static bool AddNewCharToPuzzle(int charIndex, Position position, Puzzle puzzle)
    {
        var tryNumber = 0;
        var added = false;
        if (puzzle.word.Length <= charIndex)
        {
            added = true;
        }
        else if (position.x >= 0 && position.x < puzzle.width && position.y >= 0 && position.y < puzzle.height &&
                 puzzle.puzzleData[position.x, position.y] == 0)
        {
            puzzle.puzzleData[position.x, position.y] = puzzle.word.ElementAt(charIndex);

            var sideNumber = (short) Random.Range(1, Sides.Length);
            // sideNumber = 0;
            while (!added && tryNumber < Sides.Length)
            {
                tryNumber++;
                var nextPosition = new Position((Sides[sideNumber].x + position.x), (Sides[sideNumber].y + position.y));
                sideNumber = (short) ((sideNumber + 1) % Sides.Length);
                added = AddNewCharToPuzzle(charIndex + 1, nextPosition, puzzle);
            }

            if (!added)
            {
                puzzle.puzzleData[position.x, position.y] = (char) 0;
            }
        }


        return added;
    }

    private static void PrintPuzzle(Puzzle puzzle)
    {
        for (var i = 0; i < puzzle.width; i++)
        {
            for (var j = 0; j < puzzle.height; j++)
            {
                Console.Write(puzzle.puzzleData[i, j] + " ");
            }

            Console.WriteLine();
        }
    }
}