using UnityEngine;
using System.Collections;

public static class GameBoardFactory {


	public static Field<Route> MakeBoard(BoardType type){
		switch(type){
		case BoardType.square:
			return SquareBoard();
		case BoardType.circular:
			return Circular();
		case BoardType.largeSquare:
			return LargeBoard();
		case BoardType.donut:
			return Donut();
		default:
			return SquareBoard();
		}
	}
	
	public static Field<Route> SquareBoard(){
		Stats.fieldSize = 9;
		Field<Route> gameBoard = new Field<Route>(Route.empty);
		gameBoard[5,4] = Route.red;
		gameBoard[3,4] = Route.blue;
		return gameBoard;
	}
	
	public static Field<Route> LargeBoard(){
		Stats.fieldSize = 11;
		Field<Route> gameBoard = new Field<Route>(Route.empty);
		gameBoard[6,5] = Route.red;
		gameBoard[4,5] = Route.blue;
		return gameBoard;
	}
	
	public static Field<Route> Circular(){
		Stats.fieldSize = 10;
		Field<Route> gameBoard = new Field<Route>(Route.empty);
		
		// The four corners are cut off:
		gameBoard[0,0] = Route.outOfBounds;
		gameBoard[0,1] = Route.outOfBounds;
		gameBoard[0,2] = Route.outOfBounds;
		gameBoard[1,1] = Route.outOfBounds;
		gameBoard[2,0] = Route.outOfBounds;
		gameBoard[1,0] = Route.outOfBounds;	
		
		gameBoard[9,0] = Route.outOfBounds;
		gameBoard[9,1] = Route.outOfBounds;
		gameBoard[9,2] = Route.outOfBounds;
		gameBoard[8,1] = Route.outOfBounds;
		gameBoard[7,0] = Route.outOfBounds;
		gameBoard[8,0] = Route.outOfBounds;	
		
		gameBoard[0,9] = Route.outOfBounds;
		gameBoard[0,8] = Route.outOfBounds;
		gameBoard[0,7] = Route.outOfBounds;
		gameBoard[1,8] = Route.outOfBounds;
		gameBoard[2,9] = Route.outOfBounds;
		gameBoard[1,9] = Route.outOfBounds;	
		
		gameBoard[9,9] = Route.outOfBounds;
		gameBoard[9,8] = Route.outOfBounds;
		gameBoard[9,7] = Route.outOfBounds;
		gameBoard[8,8] = Route.outOfBounds;
		gameBoard[7,9] = Route.outOfBounds;
		gameBoard[8,9] = Route.outOfBounds;	
		
		return gameBoard;
	}
	
	public static Field<Route> Donut(){
		Stats.fieldSize = 10;
		Field<Route> gameBoard = new Field<Route>(Route.empty);
		
		// The four corners are cut off:
		gameBoard[0,0] = Route.outOfBounds;
		gameBoard[0,1] = Route.outOfBounds;
		gameBoard[0,2] = Route.outOfBounds;
		gameBoard[1,1] = Route.outOfBounds;
		gameBoard[2,0] = Route.outOfBounds;
		gameBoard[1,0] = Route.outOfBounds;	
		
		gameBoard[9,0] = Route.outOfBounds;
		gameBoard[9,1] = Route.outOfBounds;
		gameBoard[9,2] = Route.outOfBounds;
		gameBoard[8,1] = Route.outOfBounds;
		gameBoard[7,0] = Route.outOfBounds;
		gameBoard[8,0] = Route.outOfBounds;	
		
		gameBoard[0,9] = Route.outOfBounds;
		gameBoard[0,8] = Route.outOfBounds;
		gameBoard[0,7] = Route.outOfBounds;
		gameBoard[1,8] = Route.outOfBounds;
		gameBoard[2,9] = Route.outOfBounds;
		gameBoard[1,9] = Route.outOfBounds;	
		
		gameBoard[9,9] = Route.outOfBounds;
		gameBoard[9,8] = Route.outOfBounds;
		gameBoard[9,7] = Route.outOfBounds;
		gameBoard[8,8] = Route.outOfBounds;
		gameBoard[7,9] = Route.outOfBounds;
		gameBoard[8,9] = Route.outOfBounds;
		
		//and there's a hole in the middle:
		gameBoard[4,4] = Route.outOfBounds;
		gameBoard[4,5] = Route.outOfBounds;
		gameBoard[5,4] = Route.outOfBounds;
		gameBoard[5,5] = Route.outOfBounds;		
		
		return gameBoard;
	}
	
}
