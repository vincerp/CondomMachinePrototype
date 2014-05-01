using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	int _currentPleasure = 0;
	int currentPleasure {
		get{ return _currentPleasure;}
		set{
			_currentPleasure = value;
			if(value == 0) {
				FaceController.instance.ChangeFaceTo(FaceType.NORMAL);
			} else {
				FaceController.instance.ChangeFaceTo(FaceType.SMILING);
			}
		}
	}

	[SerializeField]int maxPleasure = 5;
	
	[SerializeField]int addAmount = 1;
	[SerializeField]int reduceAmount = 0;

	[SerializeField]bool useMouseClicks = true;

	void IncreasePleasure(){
		currentPleasure += addAmount;
		if(currentPleasure >= maxPleasure){
			FaceController.instance.ChangeFaceTo(FaceType.BLINKING);
			AudioLibrary.instance.PlayAudio(FeedbackType.END, true);
			animation.Stop();
			animation.Play("CondomDrop");
		} else {
			AudioLibrary.instance.PlayAudio(FeedbackType.YEAH, true);
		}
	}

	void DecreasePleasure(){
		currentPleasure -= reduceAmount;
		if(currentPleasure < 0) currentPleasure = 0;
		
		AudioLibrary.instance.PlayAudio(FeedbackType.OUCH, true);
	}

	IEnumerator Start(){
		yield return null;
		FaceController.instance.ChangeFaceTo(FaceType.NORMAL);
		AudioLibrary.instance.PlayAudio(FeedbackType.INTRO, true);
		currentPleasure = 0;

	}

	void Reset(){
		Application.LoadLevel(Application.loadedLevel);
	}

	void ToggleSmile(){
		switch(FaceController.instance.currentFace){
		case FaceType.NORMAL:
			FaceController.instance.ChangeFaceTo(FaceType.SMILING);
			break;
		case FaceType.SMILING:
			FaceController.instance.ChangeFaceTo(FaceType.NORMAL);
			break;
		case FaceType.BLINKING:
			currentPleasure = 0;
			FaceController.instance.ChangeFaceTo(FaceType.NORMAL);
			break;
		}
	}

	void Update(){
		Screen.showCursor = false;

		if(!useMouseClicks) return;

		if(Input.GetMouseButtonDown(0)) IncreasePleasure();
		if(Input.GetMouseButtonDown(1)) DecreasePleasure();
		if(Input.GetMouseButtonDown(2)) Reset();
	}

	void OnGUI(){
		if(useMouseClicks) return;

		Rect quarterScreen = new Rect(0f,0f,Screen.width*0.5f, Screen.height*0.5f);
		
		if(GUI.Button(quarterScreen, "", "Label")) Reset();
		quarterScreen.x += quarterScreen.width;
		if(GUI.Button(quarterScreen, "", "Label")) ToggleSmile();
		quarterScreen.y += quarterScreen.height;
		quarterScreen.x = 0f;
		if(GUI.Button(quarterScreen, "", "Label")) DecreasePleasure();
		quarterScreen.x += quarterScreen.width;
		if(GUI.Button(quarterScreen, "", "Label")) IncreasePleasure();
	}
}
