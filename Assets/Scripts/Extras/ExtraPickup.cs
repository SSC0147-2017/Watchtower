using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ExtraPickup : MonoBehaviour {

	[Header("Tipo de Extra Armazenado no pickup")]
	public ExtrasManager.extrasType type;
	[Header("Index do vetor para desbloquear")]
	public int index;

    public GameObject AchievementPanel;

	void OnTriggerStay (Collider col){
		//Player pegou
		if (col.tag == "Player" && col.GetType() != typeof(SphereCollider)) {

			bool saveResult = ExtrasManager.extrasManager.unlockExtra (type, index);

			if (saveResult) {
				print ("You unlocked a new " + type + " Entry!");
				AchievementPanel.SetActive(true);
				SoundManager.SM.PlayAchievement ();
			}
			else
				print ("Save failed");


			Destroy (this.gameObject);
		}
	}
}
