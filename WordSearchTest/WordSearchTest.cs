using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Drawing;
using WordSearchConsole;

namespace WordSearchTest
{
	[TestClass]
	public class WordSearchTest
	{
		private string[][] expectedSearchField =
		{
			new string[] { "E","R","F","Q","K","I","Q","K","Y","Z","W","K","O","L","S" },
			new string[] { "F","K","I","C","T","E","I","F","Q","J","Y","I","J","E","S" },
			new string[] { "L","T","I","C","F","T","N","E","D","R","A","G","Q","I","Y" },
			new string[] { "D","L","V","R","T","H","T","Q","S","M","Y","G","B","U","M" },
			new string[] { "C","K","Q","E","T","B","Y","O","K","U","C","Q","M","L","E" },
			new string[] { "L","X","N","R","D","S","V","G","C","Q","Q","O","M","P","T" },
			new string[] { "H","S","L","O","U","W","D","P","U","K","B","Q","Z","X","V" },
			new string[] { "W","F","V","H","V","L","V","K","R","F","D","Q","U","X","V" },
			new string[] { "M","O","A","O","Q","S","C","W","T","F","Q","P","U","I","V" },
			new string[] { "O","N","J","Z","O","C","W","T","Y","H","A","J","B","G","L" },
			new string[] { "R","U","J","J","F","P","D","C","I","K","J","M","W","U","I" },
			new string[] { "O","B","E","L","N","P","O","R","Y","B","D","D","F","Q","P" },
			new string[] { "R","G","V","G","A","L","V","U","A","W","S","A","B","S","E" },
			new string[] { "X","C","B","J","Y","G","S","C","V","N","N","F","C","Y","T" },
			new string[] { "G","I","Q","U","E","M","N","F","T","K","X","V","V","S","T" }
		};
		private string[] expectedSearchWords = { "GARDEN", "STRIKE", "KITTENS", "TRUCK", "CLICK" };
		private string testPuzzle = @"GARDEN,STRIKE,KITTENS,TRUCK,CLICK
									  E,R,F,Q,K,I,Q,K,Y,Z,W,K,O,L,S
									  F,K,I,C,T,E,I,F,Q,J,Y,I,J,E,S
									  L,T,I,C,F,T,N,E,D,R,A,G,Q,I,Y
									  D,L,V,R,T,H,T,Q,S,M,Y,G,B,U,M
									  C,K,Q,E,T,B,Y,O,K,U,C,Q,M,L,E
									  L,X,N,R,D,S,V,G,C,Q,Q,O,M,P,T
									  H,S,L,O,U,W,D,P,U,K,B,Q,Z,X,V
									  W,F,V,H,V,L,V,K,R,F,D,Q,U,X,V
									  M,O,A,O,Q,S,C,W,T,F,Q,P,U,I,V
									  O,N,J,Z,O,C,W,T,Y,H,A,J,B,G,L
									  R,U,J,J,F,P,D,C,I,K,J,M,W,U,I
									  O,B,E,L,N,P,O,R,Y,B,D,D,F,Q,P
									  R,G,V,G,A,L,V,U,A,W,S,A,B,S,E
									  X,C,B,J,Y,G,S,C,V,N,N,F,C,Y,T
									  G,I,Q,U,E,M,N,F,T,K,X,V,V,S,T";

		WordSearch wordSearch = new WordSearch();

		[TestMethod]
		public void WhenWordSearchIsPassedAStringPuzzleInputItReturnsAListOfStringSearchWords()
		{
			CollectionAssert.AreEqual(expectedSearchWords, wordSearch.GetSearchWords(testPuzzle));
		}

		[TestMethod]
		public void WhenWordSearchIsPassedAStringPuzzleInputItReturnsATwoDimensionalArrayOfASearchField()
		{
			string[][] actualSearchField = wordSearch.GetSearchField(testPuzzle);

			//AreEqual will not work on multi-dimensional arrays must loop over arrays and compare them individually.
			//First simple check on length to avoid array overruns.
			if (expectedSearchField.Length != actualSearchField.Length)
				Assert.Fail();

			for (int i = 0; i < expectedSearchField.Length; i++)
				CollectionAssert.AreEqual(expectedSearchField[i],actualSearchField[i]);
		}

		[TestMethod]
		public void WhenWordSearchIsPassedAStringPuzzleInputItReturnsASanitizedVersionOfThePuzzleInput()
		{
			string expectedSanitizedInput = "THIS,IS,A,PUZZLE\nINPUT,TEST,STRING";

			Assert.AreEqual(expectedSanitizedInput, wordSearch.SanitizePuzzleInput("This, IS, A, PUZZLE\n INPUT, 7 TEST 3, STRING."));
		}

		[TestMethod]
		public void WhenWordSearchIsPassedAStringPuzzleInputItWillSetItsWordSearchPuzzleMemberVariable()
		{
			WordSearchPuzzle expectedWordSearchPuzzle = new WordSearchPuzzle();
			expectedWordSearchPuzzle.searchField = expectedSearchField;
			expectedWordSearchPuzzle.searchWords = expectedSearchWords;

			wordSearch.SetWordSearchPuzzle(testPuzzle);

			if (expectedWordSearchPuzzle.searchField.Length != wordSearch.wordSearchPuzzle.searchField.Length)
				Assert.Fail();

			for (int i = 0; i < expectedWordSearchPuzzle.searchField.Length; i++)
				CollectionAssert.AreEqual(expectedWordSearchPuzzle.searchField[i], wordSearch.wordSearchPuzzle.searchField[i]);

			CollectionAssert.AreEqual(expectedWordSearchPuzzle.searchWords, wordSearch.wordSearchPuzzle.searchWords);
		}

		[TestMethod]
		public void WhenWordSearchIsPassedAStringLetterItWillReturnAListOfPointsOfAllOccurrancesOfThatLetterInTheSearchField()
		{
			wordSearch.SetWordSearchPuzzle(testPuzzle);

			List<Point> actualPoints = wordSearch.GetAllLocationsOfLetter("E");
			List<Point> expectedPoints = new List<Point>()
			{
				new Point(0, 0),
				new Point(1, 5),
				new Point(1, 13),
				new Point(2, 7),
				new Point(4, 3),
				new Point(4, 14),
				new Point(11, 2),
				new Point(12, 14),
				new Point(14, 4)
			};
			
			if (expectedPoints.Count != actualPoints.Count)
				Assert.Fail();

			for (int i = 0; i < expectedPoints.Count; i++)
			{
				Assert.AreEqual(expectedPoints[i].X, actualPoints[i].X);
				Assert.AreEqual(expectedPoints[i].Y, actualPoints[i].Y);
			}
		}
	}
}
