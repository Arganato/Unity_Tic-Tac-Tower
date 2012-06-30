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
			return "Well done!\nYou've just finished the tutorial for the Silence Tower.\n\nThe last thing to learn about is the power, this mysterious force that kept us from stopping blue with builds alone, " +
				"or even going straight for a win, in the last scenario. After that tutorial you have learned all you need to get started on a full scale game of Tic-Tac Tower!";
		default:
			Debug.LogError("invalid chapter");
			return "invalid query";
		}
	}
	
	private static string GetSkillCapTutorial(Tutorial.Chapter chapter){
		switch(chapter){
		case Tutorial.Chapter.intro:
			return "Welcome to the last tutorial for Tic-Tac Tower!\nThis tutorial is for the power-skill. This skill is a bit " +
				"different than the other skills in the game. \n\nIts a passive skill, which means that it cannon be used, like the other " +
				"skills. This has the benefit that any power you acquire during a game, stays with you for the entire game. " +
				"The skill does nothing on its own, but it is nevertheless probably the most important skill of the game, and likely to " +
				"be the most important tower you make during a game.\n\n"+
				"The power is the skill that fuels the other skills, and it is particularly important for the build-skill. " +
				"Build will therefore be used to explain it: There is a limit of how many skills you can use in a game on the same turn. " +
				"If you have 4 build you would be able to put out 5 pieces in a single turn, and win the game automatically, if not for this limit." +
				"This limit is the amount of power you have. You can only use as many builds as you have power.";
		case Tutorial.Chapter.textStr:
			return "As you can see from the picture below, the pattern for the Power-tower is a square. This makes it harder to build " +
				"(or rather, easier for your opponent to block), than the other skills. Why? Lets take an example: if you have made an angle of " +
				"three pieces, you can build every skill with one more piece. Shoot, build and silence can each be built on two different places, but power " +
				"can only be built on one place. Now lets look at a scenario.";
		case Tutorial.Chapter.tutStr:
			return "This is a typical late game scenario. You have 2 builds, and have two in a row. With two build, and one piece to place. Hey thats 3 pieces " +
				"and theres already a two in a row there? Cant I just make 5 in a row right at once? No, sorry. You only have 1 power, and therefore you can only use one of your builds. " +
				"And as if that wasnt bad enough, blue has allready 3 in a row, and only need to use one of his builds next round to win. And he's even got two of them with enough power.\n\n" +
				"So, we need to stop blue from winning. To do that we cant even allow him to have 2 in a row at the end of our turn. In addition, we need to take the " +
				"offensive, and get that extra power to be able to use two builds. We have an angle of three pieces; thats a good start. And we have a build, which we can use. " +
				"Use it to build a building formed like a P, by placing a piece outside of the open place in the angle, and then in the open place. " +
				"This building gives you one of each of all the skills! Then use the shoot to stop blue.";
		case Tutorial.Chapter.textDiag:
			return "You did it! A possible defeat is turned to a chance to win, and now blue is forced to use his skills to stop you, instead of winning the game himself.\n\n" +
				"Even though you used a build to make the P-shape, you get it back, as the shape gives you another build. " +
				"This is a good way to use build as a catalyst to getting more complex buildings, and thus more skills\n\n" +
				"The P-shape, by giving a total of 5 skills, is the shape that gives the most skills with only 5 pieces. This, in addition to giving one power, makes it" +
				"very important, and something that should be looked out for in every game.";
		case Tutorial.Chapter.tutDiag:
			return "Now you should have learned all you need to play Tic-Tac Tower. " +
				"In this scenario, you are more on your own, and you need to think of a smart way to solve a complex problem. The goal is to get 5 in a row, and all towers are used, both straight and diagonal. " +
				"So, if you'r ready, just close this window and get started. If you need some more hints you can read on." +
				"\n\nTo do that, you need several skills, including build, power, and a shoot. " +
				"\"A shoot? what on earth could that be good for when I'm going to win this round anyways?\" Well, I'm not going to reveal too much," +
				 "but remember that you can also shoot your own pieces. Good luck";
		case Tutorial.Chapter.end:
			return "Well done!\nYou've just finished the tutorial for the Power Tower. This is the last tutorial, and you should now be ready to try a real game. " +
				"If you should forget what a skill does, or how its tower looks, fear not. The description is availible by clicking the \"?\"-button in game, just like in the tutorial.\n\n" +
				"Good luck in your future games!";
		default:
			Debug.LogError("invalid chapter");
			return "invalid query";
		}
		
	}
		
}
