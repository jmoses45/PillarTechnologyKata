using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WordSearchConsole
{
	public class WordSearch
	{
		public string[][] GetSearchField(string puzzleInput)
		{
			//Sanitize Input - Will be moved to a sanitize input puzzle function later
			puzzleInput = Regex.Replace(puzzleInput, @"[^A-Z\n,]", String.Empty);
			
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
			//Get a substring of the puzzle input string consisting of the first line of 
			//character minus the \n character
			string firstLineSubstring = puzzleInput.Substring(0, puzzleInput.IndexOf('\n') - 1);

			//Split the substring by commas to get an array of search words
			string[] result = firstLineSubstring.Split(',');
			
			//return the array of search words.
			return result;
		}
	}
}
