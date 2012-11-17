using UnityEngine;
using System.Collections;

public class TutorialScene : SceneTemplate{
	
	public GUISkin customSkin;
	public bool enable;
	public Transform gameCamera;
	
//	private TutorialWindow tutorialWindow;
	private GameGUIFactory gui;
	private ScenarioDescriptionGUI scenarioWindow;
	private BasicTutorial tutorialPropagator;
	private TutorialConditionChecker conditionChecker;
	private GraphicalEffectFactory graphicalEffects;
	
	public int tutorialStep = 0;
	public bool enableControl = false;
	
	protected override void Start () {
		base.Start();
		graphicalEffects = (GraphicalEffectFactory)FindObjectOfType(typeof(GraphicalEffectFactory));
		tutorialPropagator = new BasicTutorial(this, control);
		scenarioWindow = new ScenarioDescriptionGUI((IScenarioDescription)tutorialPropagator);
		conditionChecker = new TutorialConditionChecker((IScenarioDescription)tutorialPropagator,this);
		tutorialPropagator.SetGUI(scenarioWindow);
		gui = GameGUIFactory.Create(Tutorial.guiOptions,(IGUIMessages)this);
		tutorialPropagator.Start();
	}
	
	public void EnableGameGUI(bool b){
		gui.gameGUIEnabled = b;
	}
	
	public void OnVictory(){ //event sent from control
		Stats.gameRunning = true;
	}
	
	public void FlashEndTurnButton(){
		gui.FlashEndTurnButton();
	}
	public void FlashUndoButton(){
		gui.FlashUndoButton();
	}
	public void FlashSkillButton(int skill){
		gui.FlashSkillButton(skill);
	}
	public void FlashHelpButton(){
		gui.FlashHelpButton();
	}
	public void FlashBoard(FieldIndex ind){
		if(graphicalEffects == null){
			Debug.LogError("no graphical effects factory found");
			return;
		}
		graphicalEffects.FlashBoard(ind);
	}
	
	
	//IGUIMessages
	
	public override void UserEndTurn (){
		if(enableControl){
			//end turn and reset to player 1's turn
			control.UserEndTurn();
			Control.cState.activePlayer = 0;
			conditionChecker.Calculate();
		}
	}
	
	public override void UserFieldSelect (FieldIndex position)
	{
		if(enableControl){
			if(control.UserFieldSelect(position)){
				conditionChecker.Calculate();
			}
		}
	}

	void OnGUI() {
		if(!enable){
			return;
		}
		GUI.skin = customSkin;
		
		gui.PrintGUI();
		scenarioWindow.PrintGUI();
		
		base.HandleMouseInput();
	}
}
