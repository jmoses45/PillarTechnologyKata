using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WordSearchConsole;

namespace WordSearchTest
{
	[TestClass]
	public class WordSearchTest
	{
		string testPuzzle = @"GARDEN,STRIKE,KITTENS,TRUCK,CLICK
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
		[TestMethod]
		public void WhenWordSearchIsPassedAStringPuzzleInputItReturnsAListOfStringSearchWords()
		{
			WordSearch wordSearch = new WordSearch();
			string[] searchWords = { "GARDEN", "STRIKE", "KITTENS", "TRUCK", "CLICK" };

			CollectionAssert.AreEqual(searchWords, wordSearch.GetSearchWords(testPuzzle));
		}
	}
}
