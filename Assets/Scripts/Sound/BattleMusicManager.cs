using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMusicManager : MonoBehaviour {

	public AudioClip[] BattleMusic;
	
	public AudioClip GameOverMusic;

	public void PlayBattleMusic (){
		int index = Random.Range(0, BattleMusic.Length-1);
		GetComponent<AudioSource>().clip = BattleMusic[index];
		GetComponent<AudioSource>().volume = 0.4f;
		GetComponent<AudioSource>().Play();
	}
	
	public void StopBattleMusic (){
		StartCoroutine(MusicStopDelay(1f));
	}
	
	IEnumerator MusicStopDelay(float delay){
		
		
		while(GetComponent<AudioSource>().volume > 0f){
			yield return new WaitForSeconds(delay/5);
			GetComponent<AudioSource>().volume -= 0.1f;
		}
		
		
		GetComponent<AudioSource>().Stop();
	}
	
	public void StartGameOverMusic(){
		print("entrou");
		GetComponent<AudioSource>().clip = GameOverMusic;
		GetComponent<AudioSource>().volume = 0f;
		GetComponent<AudioSource>().Play();
		StartCoroutine(MusicStartDelay(1f));
	}
	
	IEnumerator MusicStartDelay(float delay){
		
		
		while(GetComponent<AudioSource>().volume < 1f){
			yield return new WaitForSeconds(delay/10);
			GetComponent<AudioSource>().volume += 0.1f;
		}
	}
}
