using System;
using System.Text.RegularExpressions;

namespace WordSearch
{
	public class WordSearchPuzzle
	{
		private string mSanitizedPuzzle = string.Empty;

		private string[] mSearchWords;
		private string[][] mSearchField;

		public string sanitizedPuzzle { get { return mSanitizedPuzzle; } }
		public string[] searchWords { get { return mSearchWords; } }
		public string[][] searchField { get { return mSearchField; } }

		public WordSearchPuzzle(string puzzleInput)
		{
			//Sanitize Input
			mSanitizedPuzzle = SanitizePuzzleInput(puzzleInput);

			ParsePuzzle(mSanitizedPuzzle);
		}

		private void ParsePuzzle(string puzzleInput)
		{
			//Split input into rows by \n
			string[] rows = puzzleInput.Split('\n');

			//Split the first row by commas to get an array of search words
			mSearchWords = rows[0].Split(',');

			//Initialize result array by row lenght - 1 to account for search words.
			mSearchField = new string[rows.Length - 1][];

			//Loop over rows to put into result. Start loop at index 1 to skip the search words.
			for (int i = 1; i < rows.Length; i++)
				searchField[i - 1] = rows[i].Split(',');
		}

		//Separated as function in case it needs to become more complex
		private string SanitizePuzzleInput(string puzzleInput)
		{
			return Regex.Replace(puzzleInput.ToUpper(), @"[^A-Z\n,]", String.Empty);
		}
	}
}
