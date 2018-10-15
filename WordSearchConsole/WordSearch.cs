using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearchConsole
{
	public class WordSearch
	{
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
