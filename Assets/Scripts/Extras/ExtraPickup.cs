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
		if (col.tag == "Player") {

			bool saveResult = ExtrasManager.extrasManager.unlockExtra (type, index);

			if (saveResult)
				print ("You unlocked a new " + type + " Entry!");
			else
				print ("Save failed");

            AchievementPanel.SetActive(true);

			Destroy (this.gameObject);
		}
	}
}
