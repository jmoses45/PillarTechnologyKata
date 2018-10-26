using System;
using System.Collections.Generic;
using System.Drawing;

namespace WordSearch
{
	public class WordSearchSolver
	{
		private string mSanitizedPuzzleInput = String.Empty;
		private WordSearchPuzzle mWordSearchPuzzle = null;

		public WordSearchPuzzle wordSearchPuzzle { get { return mWordSearchPuzzle; } }
		public string[][] searchField { get { return wordSearchPuzzle.searchField; } }
		public string[] searchWords { get { return wordSearchPuzzle.searchWords;  } }

		public WordSearchSolver(string puzzleInput)
		{
			mWordSearchPuzzle = new WordSearchPuzzle(puzzleInput);
		}

		public List<Point> FindWordPositions(string word)
		{
			List<Point> result = new List<Point>();

			//Confirm word is not empty
			if (word != string.Empty)
			{
				List<Point> startPositions = GetAllPositionsOfLetter(word[0].ToString());

				//Loop over start positions
				for (int i = 0; i < startPositions.Count; i++)
				{
					//Start recursive function looking for whole word.
					result = FindConnectingLetter(word, 0, startPositions[i], 4);

					if (result.Count > 0)
						break;
				}
			}

			return result;
		}

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

		public List<Letter> GetNeighboringLetters(Point position)
		{
			List<Letter> result = new List<Letter>();

			Letter neighborLetter;

			//Loop over neighboring positions
			for (int i = -1; i < 2; i++)
			{
				for (int j = -1; j < 2; j++)
				{
					//Create a new NeighborLetter object and set global position and letter.
					neighborLetter = new Letter();

					//Find global position and add to neighborLetter
					neighborLetter.position = new Point(position.X + j, position.Y + i);

					//Get letter of Global position and add to neighborLetter
					neighborLetter.letter = GetLetterAtPosition(neighborLetter.position);

					//Add to result
					result.Add(neighborLetter);
				}
			}

			return result;
		}

		//Recursive function to check letter connections
		//previousNeighborIndex - An int representing the neighbor that was the previous letter in the word.
		private List<Point> FindConnectingLetter(string word, int wordIndex, Point position, int previousNeighborIndex)
		{
			List<Point> result = new List<Point>();

			//Increment word index
			wordIndex++;

			//Check for end of word
			if (wordIndex == word.Length)
			{
				result.Add(position);
			}
			else
			{
				List<Letter> neighbors = GetNeighboringLetters(position);
				List<Point> neighborResult;

				for (int i = 0; i < neighbors.Count; i++)
				{
					//Skip over itself and the previous neighbor in the list.
					if ((i == 4) || i == previousNeighborIndex)
						continue;

					//If next letter in word is found
					if (neighbors[i].letter == word[wordIndex].ToString())
					{
						//Call function again with that letter
						neighborResult = FindConnectingLetter(word, wordIndex, neighbors[i].position, 4 + (4 - i));

						//If neighbor returns a count, it means the end was found. Add this letters position and return result.
						if (neighborResult.Count > 0)
						{
							result.Add(position);
							result.AddRange(neighborResult);
							break;
						}
					}
				}
			}

			return result;
		}

		private string GetLetterAtPosition(Point position)
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
}
