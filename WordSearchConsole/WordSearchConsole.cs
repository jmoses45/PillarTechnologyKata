using System;
using System.Collections.Generic;
using System.Drawing;

namespace WordSearch
{
	class WordSearchConsole
	{
		private enum WordSearchType
		{
			CUSTOM,
			PREDEFINED
		}

		#region Predefined Puzzles
		private string smlPuzzle = @"CUP,DROP,FOUR
C,O,P,O,R,D,X
O,Q,T,E,S,V,U
R,P,E,V,N,F,Y
Z,C,X,F,D,Z,O
P,H,O,W,J,A,M
X,U,O,O,K,N,P
R,W,C,M,P,S,T";
		private string medPuzzle = @"CONSIST,DAFFY,INVENT,SISTER,VALUE
F,X,E,D,T,Q,F,O,I,Q
D,A,I,Z,S,Z,O,B,H,I
X,B,Y,S,S,W,X,K,G,N
N,K,U,X,I,J,J,O,A,V
E,W,C,O,N,S,I,S,T,E
O,V,Q,C,V,H,T,Q,K,N
M,M,C,A,D,D,Y,E,P,T
G,L,L,Q,D,G,Y,X,R,G
Z,U,L,H,L,S,Q,B,N,N
E,Y,F,F,A,D,J,O,K,E";
		private string lrgPuzzle = @"APPARATUS,DEADPAN,GRADE,ICKY,LUDICROUS,MOUNTAIN,PAUSE,PLASTIC,TEMPORARY,THOUGHTFUL
V,L,I,B,T,B,O,H,J,R,N,K,D,D,B,M,D,M
Y,K,E,J,N,H,Z,U,T,B,I,V,Q,E,P,G,Q,U
X,I,V,G,Q,A,G,P,Z,M,W,E,M,A,L,Q,V,N
M,I,Q,R,K,U,B,N,Y,Z,B,S,V,D,F,A,E,G
V,J,G,P,J,Q,H,I,A,J,B,F,Y,P,T,G,R,Q
B,U,R,X,Y,J,K,S,E,G,I,S,R,A,Y,A,D,C
L,U,F,T,H,G,U,O,H,T,U,U,A,N,D,K,X,G
Q,A,F,R,S,M,P,E,U,O,X,D,R,E,M,M,C,M
U,W,P,O,O,U,L,L,R,F,Y,E,O,U,X,W,N,I
R,R,N,V,B,Y,T,C,A,Q,V,G,P,C,F,H,K,A
Q,E,S,U,A,P,I,A,Y,S,E,Z,M,A,D,T,X,T
O,W,Q,K,J,D,O,Q,R,R,T,D,E,O,T,W,W,N
Q,K,G,Z,U,V,N,C,Y,A,O,I,T,P,G,Q,Q,I
K,G,N,L,I,P,M,V,Y,O,P,O,C,V,I,N,J,K
M,O,U,N,T,A,I,N,W,I,C,P,G,P,M,O,D,F
Y,R,S,Z,P,D,F,D,N,J,S,M,A,E,R,N,F,T
T,T,Y,G,M,J,C,E,R,B,M,B,L,W,Z,U,O,T
X,N,E,M,A,P,X,S,J,V,V,M,X,A,A,T,S,U";
		#endregion

		public WordSearchConsole()
		{
			Console.WriteLine("Welcome to Word Search!\nProgrammed by Joe Moses.\n");

			SetupWordSearch();
		}

		private void SetupWordSearch()
		{
			string puzzleInput = string.Empty;
			WordSearchType wordSearchType = GetWordSearchType();

			if (wordSearchType == WordSearchType.CUSTOM)
			{
				puzzleInput = GetCustomWordSearch();
			}
			else
			{
				puzzleInput = GetPredefinedSize();
			}

			if (puzzleInput != string.Empty)
			{
				Console.WriteLine();
				Console.WriteLine("Press any key to solve.");
				Console.ReadKey();

				SolveWordSearch(puzzleInput);

				if(GetPlayAgain())
				{
					Console.Clear();
					SetupWordSearch();
				}
			}
		}
		
		private string GetCustomWordSearch()
		{
			string line;
			string result = string.Empty;

			Console.Clear();
			Console.WriteLine("Enter a custom word search. First line consisting of search words, that are comma separated.");
			Console.WriteLine("Following lines consisting of a square of search letters, that are comma separated.");
			Console.WriteLine();
			Console.WriteLine("Type \'DONE\' when finished.");
			Console.WriteLine();

			line = Console.ReadLine();

			while (line.ToUpper() != "DONE")
			{
				//Add the input line to the response and add a new line character to separated lines.
				result += line + "\n";

				line = Console.ReadLine();
			}

			if (result != string.Empty)
			{
				//Once "DONE" is found remove the last new line character.
				result = result.Remove(result.Length - 1);
			}
			else
			{
				Console.WriteLine();
				Console.WriteLine("Puzzle formatted incorrectly.");
				Console.WriteLine("Press any key to exit.");
				Console.ReadKey();
			}

			return result;
		}

		private bool GetPlayAgain()
		{
			ConsoleKey response;

			do
			{
				Console.WriteLine();
				Console.Write("Would you like to run word search again? [y/n]");
				response = Console.ReadKey(false).Key;

				Console.WriteLine();
			}
			while (response != ConsoleKey.Y && response != ConsoleKey.N);

			return response == ConsoleKey.Y;
		}

		private string GetPredefinedSize()
		{
			string result = string.Empty;

			Console.Clear();

			ConsoleKey response;

			do
			{
				Console.Write("Would you like to run a small, medium, or large puzzle? [s/m/l]");
				response = Console.ReadKey(false).Key;

				Console.WriteLine();
			}
			while (response != ConsoleKey.S && response != ConsoleKey.M && response != ConsoleKey.L);

			switch (response)
			{
				case ConsoleKey.L:
					result = lrgPuzzle;
					break;

				case ConsoleKey.S:
					result = smlPuzzle;
					break;

				case ConsoleKey.M:
					result = medPuzzle;
					break;
			}

			return result;
		}

		private WordSearchType GetWordSearchType()
		{
			WordSearchType result = WordSearchType.PREDEFINED;

			ConsoleKey response;

			do
			{
				Console.Write("Would you like to use a predefined word search or enter a custom one? [p/c]");
				response = Console.ReadKey(false).Key;

				Console.WriteLine();
			}
			while (response != ConsoleKey.P && response != ConsoleKey.C);

			if (response == ConsoleKey.C)
				result = WordSearchType.CUSTOM;

			return result;
		}

		private void SolveWordSearch(string puzzleInput)
		{
			Console.Clear();
			Console.WriteLine(puzzleInput);
			Console.WriteLine();

			WordSearchSolver wordSearch = new WordSearchSolver();
			wordSearch.SetWordSearchPuzzle(puzzleInput);

			string positionsOutput = string.Empty;
			string[] searchWords = wordSearch.wordSearchPuzzle.searchWords;
			List<Point> wordPositions;

			for (int i = 0; i < searchWords.Length; i++)
			{
				positionsOutput = string.Empty;
				wordPositions = wordSearch.FindWordPositions(searchWords[i]);

				foreach(Point point in wordPositions)
				{
					positionsOutput += "(" + point.X + "," + point.Y + "),";
				}

				//Once loop is complete remove the last comma.
				positionsOutput = positionsOutput.Remove(positionsOutput.Length - 1);

				Console.WriteLine(searchWords[i] + ": " + positionsOutput);
			}
		}
	}
}
