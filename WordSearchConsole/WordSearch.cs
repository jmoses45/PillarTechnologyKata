using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;

namespace WordSearchConsole
{
	public class WordSearch
	{
		private string mSanitizedPuzzleInput = String.Empty;
		private WordSearchPuzzle mWordSearchPuzzle = null;

		public WordSearchPuzzle wordSearchPuzzle { get { return mWordSearchPuzzle; } set{ mWordSearchPuzzle = value; } }
	
		public List<Point> GetAllLocationsOfLetter(string letter)
		{
			List<Point> result = new List<Point>();

			if (mWordSearchPuzzle != null)
			{
				for (int i = 0; i < mWordSearchPuzzle.searchField.Length; i++)
				{
					for (int j = 0; j < mWordSearchPuzzle.searchField[i].Length; j++)
					{
						if (mWordSearchPuzzle.searchField[i][j] == letter)
							result.Add(new Point(i, j));
					}
				}
			}

			return result;
		}

		public string[][] GetSearchField(string puzzleInput)
		{
			//Sanitize Input
			puzzleInput = SanitizePuzzleInput(puzzleInput);

			//Split input into lines by \n
			string[] lines = puzzleInput.Split('\n');

			//Initialize results array by lines lenght - 1 to account for search words.
			string[][] result = new string[lines.Length - 1][];

			//Loop over lines to put into results. Start loop at index 1 to skip the search words.
			for (int i = 1; i < lines.Length; i++)
				result[i - 1] = lines[i].Split(',');

			for (int i = 0; i < result.Length; i++)
			{
				Console.Write(i.ToString() + ": ");

				for (int j = 0; j < result.Length; j++)
				{
					Console.Write(result[i][j]);
				}
				Console.WriteLine();
			}

			return result;
		}

		public string[] GetSearchWords(string puzzleInput)
		{
			//Sanitize Input
			puzzleInput = SanitizePuzzleInput(puzzleInput);

			//Get a substring of the puzzle input string consisting of the first line of characters
			string firstLineSubstring = puzzleInput.Substring(0, puzzleInput.IndexOf('\n'));

			//Split the substring by commas to get an array of search words
			string[] result = firstLineSubstring.Split(',');
			
			//return the array of search words.
			return result;
		}

		public string SanitizePuzzleInput(string puzzleInput)
		{
			if (mSanitizedPuzzleInput == String.Empty)
			{
				puzzleInput = puzzleInput.ToUpper();
				mSanitizedPuzzleInput = Regex.Replace(puzzleInput, @"[^A-Z\n,]", String.Empty);
			}

			return mSanitizedPuzzleInput;
		}

		public void SetWordSearchPuzzle(string puzzleInput)
		{
			//Sanitize Input
			puzzleInput = SanitizePuzzleInput(puzzleInput);

			mWordSearchPuzzle = new WordSearchPuzzle();
			mWordSearchPuzzle.searchField = GetSearchField(puzzleInput);
			mWordSearchPuzzle.searchWords = GetSearchWords(puzzleInput);
		}
	}

	public class WordSearchPuzzle
	{
		public string[] searchWords;
		public string[][] searchField;
	}
}
