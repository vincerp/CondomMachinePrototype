using UnityEngine;
using UnityEditor;
using System.Collections;

public class AudioAssetsUtils {

	[MenuItem("Assets/Audio/Set Audio Files as 2D", true)]
	[MenuItem("Assets/Audio/Set Audio Files as 3D", true)]
	static bool VerifyAudioMenu(){
		return Selection.GetFiltered(typeof(AudioClip), SelectionMode.Assets).Length > 0; 
	}

	[MenuItem("Assets/Audio/Set Audio Files as 2D", false, 0)]
	static void SetAudioFilesAs2D(){
		//Debug.Log(Selection.activeObject.GetType());
		Object[] audios = Selection.GetFiltered(typeof(AudioClip), SelectionMode.Assets);

		int amount = 0;
		foreach(AudioClip a in audios){
			string path = AssetDatabase.GetAssetPath(a);
			AudioImporter ai = AudioImporter.GetAtPath(path) as AudioImporter;
			
			if(ai.threeD){
				ai.threeD = false;
				amount++;
			}
			AssetDatabase.ImportAsset(path);
		}
		
		Debug.Log(""+amount+" AudioClips converted to 2D.");
	}
	
	[MenuItem("Assets/Audio/Set Audio Files as 3D", false, 0)]
	static void SetAudioFilesAs3D(){
		//Debug.Log(Selection.activeObject.GetType());
		Object[] audios = Selection.GetFiltered(typeof(AudioClip), SelectionMode.Assets);
		
		int amount = 0;
		foreach(AudioClip a in audios){
			string path = AssetDatabase.GetAssetPath(a);
			AudioImporter ai = AudioImporter.GetAtPath(path) as AudioImporter;
			
			if(!ai.threeD){
				ai.threeD = true;
				amount++;
			}
			AssetDatabase.ImportAsset(path);
		}
		
		Debug.Log(""+amount+" AudioClips converted to 3D.");
	}
}