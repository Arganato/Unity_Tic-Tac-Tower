using UnityEngine;
using System.Collections;

public static class TutorialAssets {

	public static string GetTutorialMessage(){
		switch(Tutorial.towerTut){
		case TowerType.shoot:
			return GetShootTutorial(Tutorial.chapter);
		case TowerType.build:
			return GetBuildTutorial(Tutorial.chapter);
		case TowerType.silence:
			return GetSilenceTutorial(Tutorial.chapter);
		case TowerType.skillCap:
			return GetSkillCapTutorial(Tutorial.chapter);
		default:
			Debug.LogError("No tutorial for that tower");
			return "invalid query";
		}
//		switch(Tutorial.chapter){
//		case Tutorial.Chapter.intro:
//			switch(Tutorial.towerTut){
//			case TowerType.shoot:
//			case TowerType.build:
//				return "Welcome to the tutorial for the build-skill. This tutorial will cover how to make build-towers,"+
//					"and how they can help you to win the game";
//			}
//		case Tutorial.Chapter.textStr:
//			switch(Tutorial.towerTut){
//			case TowerType.build:
//				return "The image above shows how to construct a straight Build Tower.";
//			case TowerType.shoot:
//			case TowerType.silence:
//				return "The image above shows how to construct a straight Silence Tower.";
//			case TowerType.skillCap:
//				return "The image above shows how to construct a straight Power Tower.";
//			default:
//				return "";
//			}
//		case Tutorial.Chapter.tutStr:
//			switch(Tutorial.towerTut){
//			case TowerType.build:
//				return "You'll now encounter a board mid-game (using only straight towers).\n During this turn, win the game!";
//			case TowerType.shoot:
//				return "You'll now encounter a board mid-game (using only straight towers).\n Your opponent is about to win the game next round. It's up to you to prevent it!";
//			case TowerType.silence:
//				return "You'll now encounter a board mid-game (using only straight towers).\n Your opponent is about to build a strong building. It's up to you to prevent it!";
//			case TowerType.skillCap:
//				return "You'll now encounter a board mid-game (using only straight towers).\n During this turn, win the game!";
//			default:
//				return "";
//			}
//		case Tutorial.Chapter.textDiag:
//			switch(Tutorial.towerTut){
//			case TowerType.build:
//				return "The image above shows how to construct a diagonal Build Tower.";
//			case TowerType.shoot:
//				return "The image above shows how to construct a diagonal Shoot Tower.";
//			case TowerType.silence:
//				return "The image above shows how to construct a diagonal Silence Tower.";
//			case TowerType.skillCap:
//				return "The image above shows how to construct a diagonal Power Tower.";
//			default:
//				return "";
//			}
//		case Tutorial.Chapter.tutDiag:
//			switch(Tutorial.towerTut){
//			case TowerType.build:
//				return "This game includes diagonal towers.\n During this turn, win the game!";
//			case TowerType.shoot:
//				return "This game includes diagonal towers.\n Your opponent is about to win the game next round. It's up to you to prevent it!";
//			case TowerType.silence:
//				return "This game includes diagonal towers.\n Your opponent is about to build a strong building. It's up to you to prevent it!";
//			case TowerType.skillCap:
//				return "This game includes diagonal towers.\n During this turn, win the game!";
//			default:
//				return "";
//			}
//		case Tutorial.Chapter.end:
//			switch(Tutorial.towerTut){
//			case TowerType.build:
//				return "Well done!\n You've just finished the tutorial for the Build Tower. Good luck in your games.";
//			case TowerType.shoot:
//				return "Well done!\n You've just finished the tutorial for the Shoot Tower. Good luck in your games.";
//			case TowerType.silence:
//				return "Well done!\n You've just finished the tutorial for the Silence Tower. Good luck in your games.";
//			case TowerType.skillCap:
//				return "Well done!\n You've just finished the tutorial for the Power Tower. Good luck in your games.";
//			default:
//				return "";
//			}
//		default:
//			return "";
//		}
	}
	
	private static string GetShootTutorial(Tutorial.Chapter chapter){
		switch(chapter){
		case Tutorial.Chapter.intro:
				return "Welcome to Tic-Tac Tower!\nTic-Tac Tower is a board game, inspired by the classic Tic-Tac-Toe. In Tic-Tac Tower the goal is " +
					"to get 5 in a row, either straight or diagonally, but in addition to this you can make towers. " +
					"A tower is a specific pattern of pieces. When you make this pattern they automatically turn into a tower. " +
					"When you make a tower, you gain a skill, which can be used to your advantage in the game.\n\n" +
					"This is the first tutorial, and covers the shoot-tower, and corresponding shoot-skill. \nPress Continue to get started.";
		case Tutorial.Chapter.textStr:
			return "Below you can find the pattern for the shoot-tower. It has two versions, one straight and one diagonal. " +
				"Both of these can be built in any orientation, and they all give you one ammo of the same skill. The shoot-skill, that is.\n\n" +
				"The explanation under the patterns says what the skill can do. When in game you use the skill by clicking the skill-icon " +
				"(see the top left of this tutorial) and then on the piece you want to destroy.\n\n" +
				"The explanation below can be accessed in game at any time by pressing the \"?\" button";
		case Tutorial.Chapter.tutStr:
			return "Now, lets get to action! In this scenario you are red, and blue is about to win. Build a shoot tower and destroy one of his pieces to stop him!"
				+"\n \nIf you dont remember how to build a shoot tower, click the ?-button and then the shoot-icon to open the help-screen. When you are done, press the check solution button."+
					"\n \nIf you have found the correct solution you can move to the next level. If not you must undo and try again. Use as many undo as you like. \n\n" +
					"In this scenario only the straight shoot tower is used\n\n To get started press the x in the top right corner to close this window. The window can be opened again at any time " +
					"with the \"Open Window\"-button which will appear in the top left corner of the screen.";
		case Tutorial.Chapter.textDiag:
			return "Well done! You stopped him from winning the game. But while doing so, you consumed all your four pieces for only one skill. "+
				"It is possible to get several skills from one building if you plan carefully. In the next example you will need to build both a straight "+
				"tower and a diagonal tower with the same piece";
		case Tutorial.Chapter.tutDiag:
			return "This scenario is much more complex than the last one. The blue player has many pieces on the board. Be careful when you build towers,"+
				"as you may open up more possibilities for him. Find a tower that gives enough shoot-skills, and destroy all his possibilities for 5 in a row!\n\n" +
				"In this scenario both the straight and diagonal shoot-tower is used. If you dont remember how it look, open the help-menu and click the shoot-icon. \n\n" +
				"To start the scenario press the x in the top right corner of this window";
		case Tutorial.Chapter.end:
			return "Well done!\n You've just finished the tutorial for the Shoot Tower. The next tutorial is about the build skill. This can be accessed from the main menu under Tutorial.";
		default:
			Debug.LogError("invalid chapter");
			return "invalid query";
		}
		
	}
	
	private static string GetBuildTutorial(Tutorial.Chapter chapter){
		switch(chapter){
		case Tutorial.Chapter.intro:
			return "Welcome to the tutorial for the build-skill. This tutorial will cover how to make build-towers,"+
					"and how they can help you to win the game";
		case Tutorial.Chapter.textStr:
			return "All towers in Tic-Tac Tower has two versions. One straight and one diagonal, "+
				"and they can all be built in any orientation. \nA picture of how to build the build-tower can be found below."+
				"This description can of course also be found in game, just as the shoot-description (see the shoot-tutorial)";
		case Tutorial.Chapter.tutStr:
			return "Now lets start a scenario! On this board you should make build-towers and use them to get 5 in a row, and win the game. "+
				"Remember that you can build several towers if the last piece you place is a part of more than one tower. "+
				"\n\nIf you dont remember how to make build-towers, press the \"?\"-button, and then the build-icon.\n\n" +
				"This scenario uses only straight towers";
		case Tutorial.Chapter.textDiag:
			return "Well done! By taking full advantage of the build-towers you managed to win the game. The possibility to place several pieces on one turn makes build the most dangerous, and probably most important skill in the game. "+
				"\n\nNext, lets look at some diagonal towers as well...";
		case Tutorial.Chapter.tutDiag:
			return "This scenario is much similar to the last one, just a bit more complex. The goal is the same: Build 5 in a row. " +
				"Try to find a building that gives enough build-skills to complete the entire row of 5.\n\n" +
				"This scenario uses both straight and diagonal towers";
		case Tutorial.Chapter.end:
			return "Well done!\n You've just finished the tutorial for the Build Tower. Next up is the silence-tutorial. Good luck in your games.";
		default:
			Debug.LogError("invalid chapter");
			return "invalid query";
		}		
	}
	
	private static string GetSilenceTutorial(Tutorial.Chapter chapter){		
		switch(chapter){
		case Tutorial.Chapter.intro:
			return "Welcome to the silence-tutorial. Silence is a bit more complex than the two earlier skills. It can be very powerful if used right, but on the other hand, "+
				"if used wrong your opponent might not even notice that you used it, or it will only delay the problem. So, what does it do? Lets have a look...";
		case Tutorial.Chapter.textStr:
			return "Silence stops the opponent from building any towers, and even 5 in a row, for the next turn. " +
				"How does it do that? It makes him unable to place pieces where they would make a tower (or 5 in a row)."+
				" But he can place them anywhere else. \nBelow you can see the in game description of the skill.";
		case Tutorial.Chapter.tutStr:
			return "Here is a scenario where blue is about to win, and we dont have any shoot to stop him. " +
				"We can, however, use a combination of silence and build.\nBuild a tower that gives you both silence and build, and use the build to put a " +
				"piece next to his row, and fire off silence. That way he wont be able to finish 5 in a row next turn, and you will have time to stop him.\n\n" +
				"Use silence just like the other abilities: Select the skill and press the board to use it. as the silence works for the entire board, " +
				"it doesnt matter where on the board you press, as long as you press somewhere on the board.";
		case Tutorial.Chapter.textDiag:
			return "You made it! \nBlue will not be able to finish his 5 in a row next turn, because he is silenced, and you have time to stop him. Next, lets look at a scenario with diagonal towers";
		case Tutorial.Chapter.tutDiag:
			return "In this scenario you are able to build advanced buildings. But you are only able to use build and silence to stop blue from getting 5 in a row." +
				"In addition, you only have enough power to use one of your builds this turn (more on power in the last tutorial)." +
				"Use both straight and diagonal towers to stop blue from beeing able to win in this scenario.";
		case Tutorial.Chapter.end:
			return "Well done!\n You've just finished the tutorial for the Silence Tower.\nThe last thing to learn about is the power, this mysterious force that kept us from stopping blue with builds alone, " +
				"or even going straight for a win, in the last scenario. After that tutorial you have learned all you need to get started on a full scale game of Tic-Tac Tower!";
		default:
			Debug.LogError("invalid chapter");
			return "invalid query";
		}
	}
	
	private static string GetSkillCapTutorial(Tutorial.Chapter chapter){
		switch(chapter){
		case Tutorial.Chapter.intro:
			return "";
		case Tutorial.Chapter.textStr:
			return "";
		case Tutorial.Chapter.tutStr:
			return "";
		case Tutorial.Chapter.textDiag:
			return "";
		case Tutorial.Chapter.tutDiag:
			return "";
		case Tutorial.Chapter.end:
			return "Well done!\n You've just finished the tutorial for the Power Tower. Good luck in your games.";
		default:
			Debug.LogError("invalid chapter");
			return "invalid query";
		}
		
	}
		
}
