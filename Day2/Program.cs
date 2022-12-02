// See https://aka.ms/new-console-template for more information


using System.IO;
using System;
using System.Collections.Generic;

internal class Program
{
    private static void Main(string[] args)
    {
        string filename = "input.txt";
        Dictionary<string, int> pointList = new Dictionary<string, int>();
        int points = 0;
        bool part2 = args.Length>0 ? true : false;
        pointList.Add("Rock", 1);
        pointList.Add("Paper", 2);
        pointList.Add("Scissors", 3);

        Console.WriteLine("Running Part {0}",part2 ? 2 :1 );
        string? textLine = string.Empty;
        StreamReader streamReader = new StreamReader(filename);
        while (!streamReader.EndOfStream)
        {
            textLine = streamReader.ReadLine();
            if (textLine != string.Empty)
            {
                string[] textLinePieces = textLine.Split(" ");
                if (textLinePieces.Length == 2)
                {
                    textLinePieces[0] = mapToPlay(textLinePieces[0]);
                    if (part2) { 
                        textLinePieces[1] = GetDesiredOutcome(textLinePieces[0], textLinePieces[1]); 
                    }
                    else
                    {
                        textLinePieces[1] = mapToPlay(textLinePieces[1]);
                    }
                    int matchPoints = 0;
                    matchPoints = OutcomePoints(textLinePieces[1], textLinePieces[0]);
                    matchPoints += pointList[textLinePieces[1]];
                    points += matchPoints;
                    Console.WriteLine("{0} vs {1} = {2}", textLinePieces[0], textLinePieces[1], matchPoints);
                }
                else
                {
                    throw new Exception(string.Format("Error, not two string pieces - {0}", textLinePieces.Length));
                }
            }
        }
        Console.WriteLine("Total Points {0}", points);
        streamReader.Close();

    }

    private static string mapToPlay(string code)
    {
        switch (code)
        {
            case "A" or "X":
                return "Rock";
            case "B" or "Y":
                return "Paper";
            case "C" or "Z":
                return "Scissors";
            default:
                throw new Exception(string.Format("invalid play {0}", code));

        }
    }

    private static string GetDesiredOutcome(string theirPlay, string outcome)
    {
        switch (outcome)
        {
            case "X": //lose
                switch (theirPlay)
                {
                    case "Rock":
                        return "Scissors";
                    case "Scissors":
                        return "Paper";
                    case "Paper":
                        return "Rock";
                    default:
                        throw new Exception(string.Format("invalid play {0}", theirPlay));
                }
                break;
            case "Y": //draw
                return theirPlay;
                break;
            case "Z": //win
                switch (theirPlay)
                {
                    case "Rock":
                        return "Paper";
                    case "Scissors":
                        return "Rock";
                    case "Paper":
                        return "Scissors";
                    default:
                        throw new Exception(string.Format("invalid play {0}", theirPlay));
                }
            default:
                throw new Exception(string.Format("invalid outcome {0}", outcome));
        }
    }
    private static int OutcomePoints(string myPlay, string theirPlay)
    {
        if (myPlay == theirPlay)
        {
            return 3;
        }
        switch (myPlay)
        {
            case "Rock":
                if (theirPlay == "Scissors") return 6;
                break;
            case "Paper":
                if (theirPlay == "Rock") return 6;
                break;
            case "Scissors":
                if (theirPlay == "Paper") return 6;
                break;
        }
        return 0;
    }

}


/*
A,X = Rock
B,Y = Paper
C,Z = Scissors
*/