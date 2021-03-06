//using UnityEngine;
//using System.Collections;
//
//public static class TutorialAssets { //Outdated!!
//
//	public static string GetTutorialMessage(){
//		switch(Tutorial.tutorialType){
//		case TowerType.shoot:
//			return GetShootTutorial(Tutorial.chapter);
//		case TowerType.build:
//			return GetBuildTutorial(Tutorial.chapter);
//		case TowerType.silence:
//			return GetSilenceTutorial(Tutorial.chapter);
//		case TowerType.skillCap:
//			return GetSkillCapTutorial(Tutorial.chapter);
//		default:
//			Debug.LogError("No tutorial for that tower");
//			return "invalid query";
//		}
//	}
//
//	private static string GetShootTutorial(Tutorial.Chapter chapter){
//		switch(chapter){
//		case Tutorial.Chapter.intro:
//				return "Welcome to Tic-Tac Tower!\nTic-Tac Tower is a board game, inspired by the classic Tic-Tac-Toe. In Tic-Tac Tower the goal is " +
//					"to get 5 in a row, either straight or diagonally, but in addition to this you can make towers. " +
//					"A tower is a specific pattern of pieces. When you make this pattern they automatically turn into a tower. " +
//					"When you make a tower, you gain a skill, which can be used to your advantage in the game.\n\n" +
//					"This is the first tutorial, and covers the shoot-tower, and corresponding shoot-skill. \nPress Continue to get started.";
//		case Tutorial.Chapter.textStr:
//			return "Below you can find the pattern for the shoot-tower. It has two versions, one straight and one diagonal. " +
//				"Both of these can be built in any orientation, and they all give you one ammo of the same skill. The shoot-skill, that is.\n\n" +
//				"The explanation under the patterns says what the skill can do. When in game you use the skill by clicking the skill-icon " +
//				"(see the top left of this tutorial) and then on the piece you want to destroy.\n\n" +
//				"The explanation below can be accessed in game at any time by pressing the \"?\" button";
//		case Tutorial.Chapter.tutStr:
//			return "Now, lets get to action! In this scenario you are red, and blue is about to win. Build a shoot tower and destroy one of his pieces to stop him!"
//				+"\n \nIf you dont remember how to build a shoot tower, click the ?-button and then the shoot-icon to open the help-screen. When you are done, press the check solution button."+
//					"\n \nIf you have found the correct solution you can move to the next level. If not you must undo and try again. Use as many undo as you like. \n\n" +
//					"In this scenario only the straight shoot tower is used\n\n To get started press the x in the top right corner to close this window. The window can be opened again at any time " +
//					"with the \"Open Window\"-button which will appear in the top left corner of the screen.";
//		case Tutorial.Chapter.textDiag:
//			return "Well done! You stopped him from winning the game. But while doing so, you consumed all your four pieces for only one skill. "+
//				"It is possible to get several skills from one building if you plan carefully. In the next example you will need to build both a straight "+
//				"tower and a diagonal tower with the same piece";
//		case Tutorial.Chapter.tutDiag:
//			return "This scenario is much more complex than the last one. The blue player has many pieces on the board. Be careful when you build towers,"+
//				"as you may open up more possibilities for him. Find a tower that gives enough shoot-skills, and destroy all his possibilities for 5 in a row!\n\n" +
//				"In this scenario both the straight and diagonal shoot-tower is used. If you dont remember how it look, open the help-menu and click the shoot-icon. \n\n" +
//				"To start the scenario press the x in the top right corner of this window";
//		case Tutorial.Chapter.end:
//			return "Well done!\n You've just finished the tutorial for the Shoot Tower. The next tutorial is about the build skill. This can be accessed from the main menu under Tutorial.";
//		default:
//			Debug.LogError("invalid chapter");
//			return "invalid query";
//		}
//
//	}
//
//	private static string GetBuildTutorial(Tutorial.Chapter chapter){
//		switch(chapter){
//		case Tutorial.Chapter.intro:
//			return "Welcome to the tutorial for the build-skill. This tutorial will cover how to make build-towers, "+
//					"and how they can help you to win the game";
//		case Tutorial.Chapter.textStr:
//			return "All towers in Tic-Tac Tower has two versions. One straight and one diagonal, "+
//				"and they can all be built in any orientation. \nA picture of how to build the build-tower can be found below. "+
//				"This description can of course also be found in game, just as the shoot-description (see the shoot-tutorial)";
//		case Tutorial.Chapter.tutStr:
//			return "Now lets start a scenario! On this board you should make build-towers and use them to get 5 in a row, and win the game. "+
//				"Remember that you can build several towers if the last piece you place is a part of more than one tower. "+
//				"\n\nIf you dont remember how to make build-towers, press the \"?\"-button, and then the build-icon.\n\n" +
//				"This scenario uses only straight towers.";
//		case Tutorial.Chapter.textDiag:
//			return "Well done! By taking full advantage of the build-towers you managed to win the game. The possibility to place several pieces in a single turn can create many unforseen events. "+
//				"\n\nNext, lets look at some diagonal towers as well...";
//		case Tutorial.Chapter.tutDiag:
//			return "This scenario is much similar to the last one, just a bit more complex. The goal is the same: Build 5 in a row. " +
//				"Try to find a building that consists of enough build towers to be able to win the game.\n\n" +
//				"This scenario uses both straight and diagonal towers";
//		case Tutorial.Chapter.end:
//			return "Well done!\n You've just finished the tutorial for the Build Tower. Next up is the silence-tutorial. Good luck in your games.";
//		default:
//			Debug.LogError("invalid chapter");
//			return "invalid query";
//		}		
//	}
//
//	private static string GetSilenceTutorial(Tutorial.Chapter chapter){		
//		switch(chapter){
//		case Tutorial.Chapter.intro:
//			return "Welcome to the silence-tutorial. Silence is a bit more complex than build and shoot. It can be very powerful if used right, but on the other hand, "+
//				"if used wrong your opponent might not even notice that you used it, or it will only delay the problem. So, what does it do? Lets have a look...";
//		case Tutorial.Chapter.textStr:
//			return "Silence stops the opponent from building any towers (even 5 in a row) for the next turn. " +
//				"How does it do that? It makes the opponent unable to place pieces where they would make a tower (or 5 in a row), "+
//				"although he/she can place them anywhere else. \nBelow you can see the in game description of the skill.";
//		case Tutorial.Chapter.tutStr:
//			return "Here is a scenario where blue is about to win, and we don't have any shoot to stop him. " +
//				"We can, however, use a combination of silence and build.\nBuild a tower that gives you both silence and build, and use the build to put a " +
//				"piece next to his row, and fire off silence. That way he won't be able to finish 5 in a row next turn, and you will have time to stop him.\n\n" +
//				"Use silence just like the other abilities: Select the skill and press the board to use it. As the silence works for the entire board, " +
//				"it doesn't matter where on the board you press, as long as you press somewhere on the board.";
//		case Tutorial.Chapter.textDiag:
//			return "You made it! \nBlue will not be able to finish his 5 in a row next turn, because he is silenced, and you have time to stop him/her. Next, let's look at a scenario with diagonal towers";
//		case Tutorial.Chapter.tutDiag:
//			return "In this scenario you are able to build diagonal buildings. But you are only able to use build and silence to stop blue from getting 5 in a row. " +
//				"In addition, you only have enough power to use one of your builds this turn (more on power in the last tutorial). " +
//				"Use both straight and diagonal towers to stop blue from being able to win next round.";
//		case Tutorial.Chapter.end:
//			return "Well done!\nYou've just finished the tutorial for the Silence Tower.\n\nThe last thing to learn about is the power, this mysterious force that kept us from stopping blue with builds alone, " +
//				"or even going straight for a win, in the last scenario. After that tutorial you have learned all you need to get started on a full scale game of Tic-Tac Tower!";
//		default:
//			Debug.LogError("invalid chapter");
//			return "invalid query";
//		}
//	}
//
//	private static string GetSkillCapTutorial(Tutorial.Chapter chapter){
//		switch(chapter){
//		case Tutorial.Chapter.intro:
//			return "Welcome to the last tutorial for Tic-Tac Tower!\nThis tutorial is for the power-skill. This skill is a bit " +
//				"different than the other skills in the game. \n\nIts a passive skill, which means that it cannot be used like the other " +
//				"skills. This has the benefit that any power you acquire during a game, stays with you for the entire game. " +
//				"The power skill probably the most important skill of the game, and likely to " +
//				"be the most important tower you make during a game.\n\n"+
//				"There is a limit on how many times the same skill can be used during a single turn, " +
//				"meaning that if you have 4 build you won't be able to put out 5 pieces in a single turn, thus winning the game right away." +
//				"This limit equals the amount of power you have (starting with a power of 1)";
//		case Tutorial.Chapter.textStr:
//			return "As you can see from the picture below, the pattern for the Power-tower is a square. This makes it harder to build " +
//				"(or rather, easier for your opponent to block), than the other skills. Now lets look at a scenario.";
//		case Tutorial.Chapter.tutStr:
//			return "This is a typical late game scenario. You have 2 builds, and have two in a row, meaning that you should be able to build a five-in-a-row " +
//				"this turn, but you can't. You only have 1 power and therefore you can only use one of your builds. " +
//				"And as if that wasnt bad enough, blue has already 3 in a row, and only need to use one of his two builds next round to win.\n\n" +
//				"So, we need to stop blue from winning, and at the same time plan to win next round. To do that we can't allow him to have even 2 in a row at the end of our turn. " +
//				"You'll need to gain more power as well as a shoot to destroy a blue piece. Remember to have enough builds for next round too! Don't be too eager to spend them all up at once.";
//		case Tutorial.Chapter.textDiag:
//			return "You did it! A possible defeat is turned to a chance to win, and now blue is forced to use his skills to stop you, instead of winning the game himself.\n\n" +
//				"Even though you used a build to make the P-shape, you get it back, as the shape gives you another build. " +
//				"This is a good way to use build as a catalyst to getting more complex buildings, and thus more skills.\n\n" +
//				"The P-shape, by giving a total of 4 skills, is the shape that gives the most skills with only 5 pieces. This, in addition to giving one power, makes it " +
//				"very important, and something that should be looked out for in every game.";
//		case Tutorial.Chapter.tutDiag:
//			return "Now you should have learned all you need to play Tic-Tac Tower. " +
//				"In this scenario, you are more on your own, and you need to think of a smart way to solve a complex problem. The goal is to get 5 in a row, and all towers are used, both straight and diagonal. " +
//				"So, if you're ready, just close this window and get started. If you need some more hints you can read on." +
//				"\n\nTo do that, you need several skills, including build, power, and a shoot. " +
//				"\"A shoot? what on earth could that be good for when I'm going to win this round anyways?\" Well, I'm not going to reveal too much, " +
//				 "but remember that you can also shoot your own pieces. Good luck.";
//		case Tutorial.Chapter.end:
//			return "Well done!\nYou've just finished the tutorial for the Power Tower. This is the last tutorial, and you should now be ready to try a real game. " +
//				"If you should forget what a skill does, or how its tower looks, fear not. The descriptions are available by clicking the \"?\"-button in game, just like in the tutorial.\n\n" +
//				"Good luck in your future games!";
//		default:
//			Debug.LogError("invalid chapter");
//			return "invalid query";
//		}
//
//	}
//	
//	//****************Setup-Functions from gamestate***************//
//	
//	//kan disse gjøres private?
//	public static void SetTutorialBuild1(GameState state){	//Win during this round (red)
//		Stats.skillEnabled.SetAll(false);
//		Stats.skillEnabled.build = true;
//		Stats.skillEnabled.five = true;
//		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
//		
//		state.field[5,4] = Route.red;
//		state.field[3,4] = Route.blue;
//		state.field[5,5] = Route.red;
//		state.field[5,3] = Route.blue;
//		state.field[4,6] = Route.red;
//		state.field[6,3] = Route.blue;
//		state.field[7,3] = Route.red;
//		state.field[4,5] = Route.blue;
//		state.field[7,4] = Route.red;
//		state.field[7,5] = Route.blue;
//		state.field[3,6] = Route.red;
//		state.field[4,3] = Route.blue;
//		state.field[3,3] = Route.red;
//		state.field[3,5] = Route.blue;
//		state.field[2,6] = Route.red;
//		state.field[1,6] = Route.blue;
//		state.player[0].playerSkill.skillCap = 1;
//		state.player[1].playerSkill.skillCap = 1;
//		state.placedPieces = 16;
//	}
//	
//	public static void SetTutorialBuild2(GameState state){	//Win during this round (red)
//		Stats.skillEnabled.SetAll(false);
//		Stats.skillEnabled.build = true;
//		Stats.skillEnabled.diagBuild = true;
//		Stats.skillEnabled.five = true;
//		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
//		
//		state.field[5,4] = Route.red;
//		state.field[3,4] = Route.blue;
//		state.field[4,5] = Route.red;
//		state.field[3,6] = Route.blue;
//		state.field[3,5] = Route.red;
//		state.field[2,5] = Route.blue;
//		state.field[2,6] = Route.red;
//		state.field[2,4] = Route.blue;
//		state.field[4,6] = Route.red;
//		state.field[5,2] = Route.blue;
//		state.field[3,3] = Route.red;
//		state.field[6,1] = Route.blue;
//		state.field[1,3] = Route.red;
//		state.field[2,2] = Route.blue;
//		state.field[2,1] = Route.red;
//		state.field[3,2] = Route.blue;
//		state.field[2,1] = Route.red;
//		state.field[6,1] = Route.blue;
//		state.field[1,0] = Route.red;
//		state.field[5,5] = Route.blue;
//		state.field[1,1] = Route.red;
//		state.field[3,1] = Route.blue;
//		state.player[0].playerSkill.skillCap = 1;
//		state.player[1].playerSkill.skillCap = 1;
//		state.placedPieces = 22;
//	}
//	
//	public static void SetTutorialShoot1(GameState state){	//Stop red from winning next round (blue)
//		Stats.skillEnabled.SetAll(false);
//		Stats.skillEnabled.shoot = true;
//		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
//		
//		state.field[5,4] = Route.blue;
//		state.field[5,5] = Route.blue;
//		state.field[5,6] = Route.blue;
//		state.field[5,3] = Route.blue;
//		state.field[3,4] = Route.red;
//		state.field[3,5] = Route.red;
//		state.field[3,6] = Route.red;
//	}
//	
//	public static void SetTutorialShoot2(GameState state){	//Stop red from winning next round (blue)
//		Stats.skillEnabled.SetAll(false);
//		Stats.skillEnabled.shoot = true;
//		Stats.skillEnabled.diagShoot = true;
//		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
//		
//		state.field[5,4] = Route.blue;
//		state.field[3,4] = Route.red;
//		state.field[5,5] = Route.blue;
//		state.field[5,6] = Route.red;
//		state.field[4,5] = Route.blue;
//		state.field[3,5] = Route.red;
//		state.field[3,6] = Route.blue;
//		state.field[6,3] = Route.red;
//		state.field[2,7] = Route.blue;
//		state.field[1,8] = Route.red;
//		state.field[7,2] = Route.blue;
//		state.field[5,3] = Route.red;
//		state.field[5,2] = Route.blue;
//		state.field[6,5] = Route.red;
//		state.field[6,2] = Route.blue;
//		state.field[4,7] = Route.red;
//		state.field[3,7] = Route.blue;
//		state.field[7,3] = Route.red;
//		state.field[4,2] = Route.blue;
//		state.field[6,6] = Route.red;
//		state.field[5,7] = Route.blue;
//		state.field[2,8] = Route.red;
//		state.field[5,8] = Route.blue;
//		state.player[0].playerSkill.skillCap = 1;
//		state.player[1].playerSkill.skillCap = 1;
//		state.activePlayer = 0;
//		state.placedPieces = 21;
//	}
//	
//	public static void SetTutorialSilence1(GameState state){
//		Stats.skillEnabled.SetAll(false);
//		Stats.skillEnabled.SetStraight(true);
//		Stats.skillEnabled.skillCap = false;
//		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
//		
//		state.field[5,4] = Route.blue;
//		state.field[5,5] = Route.blue;
//		state.field[5,6] = Route.blue;
//		state.field[5,3] = Route.blue;
//		state.field[3,4] = Route.red;
//		state.field[3,5] = Route.red;
//		state.field[3,6] = Route.red;
//		state.field[2,7] = Route.red;
//	}
//	
//	public static void SetTutorialSilence2(GameState state){
//		Stats.skillEnabled.SetAll(true);
//		Stats.skillEnabled.skillCap = false;
//		Stats.skillEnabled.diagSkillCap = false;
//		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
//		
//		state.field[5,4] = Route.blue;
//		state.field[3,4] = Route.red;
//		state.field[5,5] = Route.blue;
//		state.field[5,6] = Route.red;
//		state.field[4,6] = Route.blue;
//		state.field[4,5] = Route.red;
//		state.field[2,3] = Route.blue;
//		state.field[6,6] = Route.red;
//		state.field[6,7] = Route.blue;
//		state.field[7,6] = Route.red;
//		state.field[6,3] = Route.blue;
//		state.field[7,3] = Route.red;
//		state.field[4,3] = Route.blue;
//		state.field[6,4] = Route.red;
//		state.field[2,4] = Route.blue;
//		state.field[3,7] = Route.red;
//		state.field[1,3] = Route.blue;
//		state.field[7,2] = Route.red;
//		state.field[3,5] = Route.blue;
//	}
//	
//	public static void SetTutorialPower1(GameState state){
//		Stats.skillEnabled.SetAll(false);
//		Stats.skillEnabled.SetStraight(true);
//		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
//		
//		state.field[5,2] = Route.red;
//		state.field[4,2] = Route.red;
//		state.field[5,5] = Route.red;
//		state.field[4,5] = Route.red;
//		state.field[4,6] = Route.red;
//		state.field[3,3] = Route.blue;
//		state.field[5,3] = Route.blue;
//		state.field[4,3] = Route.blue;		
//		state.player[0].playerSkill.build = 2;
//		state.player[0].playerSkill.skillCap = 0;
//		state.player[1].playerSkill.build = 2;
//		state.player[1].playerSkill.skillCap = 1;
//		state.activePlayer = 0;
//	}	
//	
//	public static void SetTutorialPower2(GameState state){
//		Stats.skillEnabled.SetAll(true);
//		Stats.rules = Stats.Rules.INVISIBLE_TOWERS;
//		
//		state.field[5,4] = Route.blue;
//		state.field[3,4] = Route.red;
//		state.field[5,5] = Route.blue;
//		state.field[4,5] = Route.red;
//		state.field[6,4] = Route.blue;
//		state.field[3,6] = Route.red;
//		state.field[4,6] = Route.blue;
//		state.field[7,3] = Route.red;
//		state.field[3,7] = Route.blue;
//		state.field[8,2] = Route.red;
//		state.field[4,3] = Route.blue;
//		state.field[6,5] = Route.red;
//		state.field[2,4] = Route.blue;
//		state.field[1,4] = Route.red;
//		state.field[6,7] = Route.blue;
//		state.field[1,6] = Route.red;
//		state.field[1,5] = Route.blue;
//		state.field[5,7] = Route.red;
//		state.field[2,7] = Route.blue;
//		state.field[6,6] = Route.red;
//		state.field[2,6] = Route.blue;
//		state.field[7,7] = Route.red;
//		state.field[8,3] = Route.blue;
//		state.player[0].playerSkill.build = 1;
//		state.player[0].playerSkill.skillCap = 1;
//	}
//	
//
//
//}
