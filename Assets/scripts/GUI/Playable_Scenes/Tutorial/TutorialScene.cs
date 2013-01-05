using UnityEngine;
using System.Collections;

public class TutorialScene : SceneTemplate{
	
	public GUISkin customSkin;
	public bool enable;
	public Transform gameCamera;
	
//	private TutorialWindow tutorialWindow;
	private GameGUIFactory gui;
	private ScenarioDescriptionGUI scenarioWindow;
	private IScenarioDescription tutorialPropagator;
	private TutorialCondition conditionChecker;
	private GraphicalEffectFactory graphicalEffects;
	
	public int tutorialStep = 0;
	public bool enableControl = false;
	
	protected override void Start () {
		base.Start();
		graphicalEffects = (GraphicalEffectFactory)FindObjectOfType(typeof(GraphicalEffectFactory));
		tutorialPropagator = Tutorial.GetTutorialDescription(this, control);
		scenarioWindow = new ScenarioDescriptionGUI(tutorialPropagator);
		conditionChecker = tutorialPropagator.GetCondition();
		gui = GameGUIFactory.Create(tutorialPropagator.GetGUIOptions(),(IGUIMessages)this);
		tutorialPropagator.Start();
	}
	
	public void EnableGameGUI(bool b){
		gui.gameGUIEnabled = b;
	}
	
	public ScenarioDescriptionGUI GetTutorialGUI(){ //needed by tutorialPropagator
		return scenarioWindow;
	}
	
	public void ChangeGUIOptions(GameGUIOptions opt){
		gui = GameGUIFactory.Create(opt,(IGUIMessages)this);
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
			conditionChecker.Calculate(TutorialCondition.ConditionEvent.endTurn);
			Control.SetUndoPoint(Control.cState);
		}
	}
	
	public override void UserFieldSelect (FieldIndex position)
	{
		if(enableControl){
			if(control.UserFieldSelect(position)){
				conditionChecker.Calculate(TutorialCondition.ConditionEvent.placedPiece);
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
