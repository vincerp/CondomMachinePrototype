using UnityEngine;
using System.Collections;

public class FaceController : MonoBehaviour {

	public SpriteRenderer[] faces;

	public static FaceController instance{
		private set; get;
	}

	public FaceType currentFace {private set; get;}

	void Start(){
		instance = this;

		ChangeFaceTo(FaceType.NORMAL);
	}

	void OnDestroy(){
		if(this == instance) instance = null;
	}

	public void ChangeFaceTo(FaceType to){
		currentFace = to;
		for(int i = 0; i < faces.Length; i++){
			faces[i].enabled = (i == (int)to);
		}
	}
}

public enum FaceType{
	NORMAL = 0,
	SMILING = 1,
	BLINKING = 2
}