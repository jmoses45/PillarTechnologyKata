using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WordSearch
{
	class WordSearchConsole
	{
		//Enum for possible future expansion of types.
		private enum WordSearchType
		{
			CUSTOM,
			FILE,
			PREDEFINED
		}

		public WordSearchConsole()
		{
			Console.WriteLine("Welcome to Word Search!\nProgrammed by Joe Moses.\n");

			SetupWordSearch();
		}

		private void SetupWordSearch()
		{
			string puzzleInput = string.Empty;

			WordSearchType wordSearchType = GetWordSearchType();

			switch (wordSearchType)
			{
				case WordSearchType.CUSTOM:
					puzzleInput = GetCustomWordSearch();
					break;

				case WordSearchType.FILE:
					puzzleInput = GetWordSearchFromFile();
					break;

				case WordSearchType.PREDEFINED:
					puzzleInput = GetPredefinedSize();
					break;
			}

			if (puzzleInput != string.Empty)
			{
				Console.WriteLine();
				Console.WriteLine("Press any key to solve.");
				Console.ReadKey();

				SolveWordSearch(puzzleInput);
			}

			if (GetRunAgain())
			{
				Console.Clear();
				SetupWordSearch();
			}
		}

		//This funciton does not do any error checking of input.
		//It was designed as a way to copy correctly formated puzzles into the system.
		private string GetCustomWordSearch()
		{
			string line;
			string result = string.Empty;

			Console.Clear();
			Console.WriteLine("Enter a custom word search. First line consisting of search words that are comma separated.");
			Console.WriteLine("Following lines consisting of a square of search letters that are comma separated.");
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
				Console.WriteLine("Puzzle is empty.");
				Console.WriteLine();
			}

			return result;
		}

		private string GetPredefinedSize()
		{
			Console.Clear();

			ConsoleKey response;

			do
			{
				Console.Write("Would you like to run a small, medium, or large puzzle? [s/m/l]");
				response = Console.ReadKey(false).Key;

				Console.WriteLine();
			}
			while (response != ConsoleKey.S && response != ConsoleKey.M && response != ConsoleKey.L);

			string fileName = string.Empty;

			switch (response)
			{
				case ConsoleKey.L:
					fileName = "lrg.txt";
					break;

				case ConsoleKey.M:
					fileName = "med.txt";
					break;

				case ConsoleKey.S:
					fileName = "sml.txt";
					break;
			}

			return GetFileFromStream(Path.Combine(Application.StartupPath, fileName));
		}

		private bool GetRunAgain()
		{
			ConsoleKey response;

			do
			{
				Console.WriteLine();
				Console.Write("Would you like to run word search again? [y/n]");
				response = Console.ReadKey(false).Key;
			}
			while (response != ConsoleKey.Y && response != ConsoleKey.N);

			return response == ConsoleKey.Y;
		}

		private string GetWordSearchFromFile()
		{
			string result = string.Empty;

			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "Text files (*.txt)|*.txt";
			dialog.Title = "Select a Puzzle File";

			if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				result = GetFileFromStream(dialog.FileName);

			if (result == string.Empty)
			{
				Console.WriteLine();
				Console.WriteLine("Puzzle File was empty.");
				Console.WriteLine();
			}

			return result;
		}

		private WordSearchType GetWordSearchType()
		{
			WordSearchType result = WordSearchType.PREDEFINED;

			ConsoleKey response;

			Console.WriteLine("Would you like to:");
			Console.WriteLine("Open a word search file");
			Console.WriteLine("Enter a custom word search");
			Console.WriteLine("Use a predefined word search");

			do
			{
				Console.Write("Enter [c/f/p]");
				response = Console.ReadKey(false).Key;

				Console.WriteLine();
			}
			while (response != ConsoleKey.C && response != ConsoleKey.F && response != ConsoleKey.P);

			switch (response)
			{
				case ConsoleKey.C:
					result = WordSearchType.CUSTOM;
					break;

				case ConsoleKey.F:
					result = WordSearchType.FILE;
					break;

				case ConsoleKey.P:
					result = WordSearchType.PREDEFINED;
					break;
			}

			return result;
		}

		private string GetFileFromStream(string fileName)
		{
			string result = string.Empty;

			System.IO.StreamReader sr = new System.IO.StreamReader(fileName);
			result = sr.ReadToEnd();
			sr.Close();

			return result;
		}

		private void SolveWordSearch(string puzzleInput)
		{
			Console.Clear();
			Console.WriteLine(puzzleInput);
			Console.WriteLine();

			WordSearchSolver wordSearch = new WordSearchSolver(puzzleInput);

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
