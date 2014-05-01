using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraBgColorSetter : MonoBehaviour {

	[Range(0f, 1f)] float minGrayscaleValue = 0.4f;
	[Range(0f, 1f)] float maxGrayscaleValue = 0.85f;
	[SerializeField]Vector3 colorMult = Vector3.one;

	void Start(){
		SortColor();
	}

	void SortColor () {
		Color c;
		do{
			c = new Color(Random.value*colorMult.x, Random.value*colorMult.y, Random.value*colorMult.z);
		} while(c.grayscale < minGrayscaleValue ||
		        c.grayscale > maxGrayscaleValue);

		camera.backgroundColor = c;
	}
}
