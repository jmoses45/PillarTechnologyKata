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

		public List<Point> GetAllPositionsOfLetter(string letter)
		{
			List<Point> result = new List<Point>();

			if (mWordSearchPuzzle != null)
			{
				for (int i = 0; i < mWordSearchPuzzle.searchField.Length; i++)
				{
					for (int j = 0; j < mWordSearchPuzzle.searchField[i].Length; j++)
					{
						if (mWordSearchPuzzle.searchField[i][j] == letter)
							result.Add(new Point(j, i));
					}
				}
			}

			return result;
		}

		public List<string> GetNeighboringLetters(Point position)
		{
			//Initialize an empty array
			List<string> result = new List<string>();

			Point neighborPosition;

			//Loop over neighboring positions
			for (int i = -1; i < 2; i++)
			{
				for (int j = -1; j < 2; j++)
				{
					//Find global position
					neighborPosition = new Point(position.X + j, position.Y + i);

					//Get letter of Global position
					result.Add(GetLetterAtPosiiont(neighborPosition));
				}
			}

			return result;
		}

		public string[][] GetSearchField(string puzzleInput)
		{
			//Sanitize Input
			puzzleInput = SanitizePuzzleInput(puzzleInput);

			//Split input into rows by \n
			string[] rows = puzzleInput.Split('\n');

			//Initialize result array by row lenght - 1 to account for search words.
			string[][] result = new string[rows.Length - 1][];

			//Loop over rows to put into result. Start loop at index 1 to skip the search words.
			for (int i = 1; i < rows.Length; i++)
				result[i - 1] = rows[i].Split(',');

			return result;
		}

		public string[] GetSearchWords(string puzzleInput)
		{
			//Sanitize Input
			puzzleInput = SanitizePuzzleInput(puzzleInput);

			//Get a substring of the puzzle input string consisting of the first row of characters
			string firstRowSubstring = puzzleInput.Substring(0, puzzleInput.IndexOf('\n'));

			//Split the substring by commas to get an array of search words
			string[] result = firstRowSubstring.Split(',');
			
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

		private string GetLetterAtPosiiont(Point position)
		{
			//Default result to empty string.
			string result = string.Empty;

			//Check to make sure we have a puzzle and that the position is within the puzzle.
			if ((mWordSearchPuzzle != null) && (mWordSearchPuzzle.searchField.Length > 0) &&
				(position.X >= 0) && (position.X < mWordSearchPuzzle.searchField[0].Length) &&
				(position.Y >= 0) && (position.Y < mWordSearchPuzzle.searchField.Length))
			{
				//Set result to letter at position in searchField.
				result = mWordSearchPuzzle.searchField[position.Y][position.X];
			}

			return result;
		}
	}

	public class WordSearchPuzzle
	{
		public string[] searchWords;
		public string[][] searchField;
	}
}
