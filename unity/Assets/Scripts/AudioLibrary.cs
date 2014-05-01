using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AudioLibrary : MonoBehaviour {

	[SerializeField]List<AudioClip> introAudio;
	[SerializeField]List<AudioClip> yeahAudio;
	[SerializeField]List<AudioClip> ouchAudio;
	[SerializeField]List<AudioClip> endAudio;
	int[] lastPlayed = new int[4];

	[SerializeField]float timeBetweenAudios = 1f;

	AudioSource voiceSource;

	bool isPlayingAudio;

	public static AudioLibrary instance {
		get; private set;
	}

	void Start(){
		instance = this;
		voiceSource = gameObject.AddComponent<AudioSource>();
		for(int i = 0; i<lastPlayed.Length; i++) lastPlayed[i] = -1;
	}

	void OnDestroy(){
		if(this == instance) instance = null;
	}

	public void PlayAudio(FeedbackType type, bool overrideAudio = false){
		if(type == FeedbackType.NONE){
			print ("Trying to play a FeedbackType.NONE audio. Why?");
			return;
		}

		int r, max, i = (int)type;
		List<AudioClip> useThis;
		switch(type){
		default:
			useThis = introAudio;
			break;
		case FeedbackType.YEAH:
			useThis = yeahAudio;
			break;
		case FeedbackType.OUCH:
			useThis = ouchAudio;
			break;
		case FeedbackType.END:
			useThis = endAudio;
			break;
		}
		max = useThis.Count;

		do{
			r = Random.Range(0, max);
		} while (r == lastPlayed[i]);
		lastPlayed[i] = r;

		StartCoroutine(PlayAudioClip(useThis[r], overrideAudio));
	}

	IEnumerator PlayAudioClip(AudioClip clip, bool overrideAudio = false, bool waitToPlay = false){
		if(!isPlayingAudio) StartCoroutine(Coroutine_PlayAudio(clip));
		else if(overrideAudio){
			StopCoroutine("Coroutine_PlayAudio");
			StartCoroutine(Coroutine_PlayAudio(clip));
		} else if(waitToPlay){
			if(isPlayingAudio) while(isPlayingAudio) yield return null;
			StartCoroutine(Coroutine_PlayAudio(clip));
		}

		yield return true;
	}

	IEnumerator Coroutine_PlayAudio(AudioClip clip){
		isPlayingAudio = true;
		voiceSource.clip = clip;
		voiceSource.loop = false;
		voiceSource.Play();
		yield return new WaitForSeconds(voiceSource.clip.length + timeBetweenAudios);
		isPlayingAudio = false;
	}
}

public enum FeedbackType{
	NONE = -1,
	INTRO = 0,
	YEAH = 1,
	OUCH = 2,
	END = 3
}
