using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ItemPickUp : MonoBehaviour {

	[Header("Tipo de Item no pickup")]
	public InventoryController.itemType type;

	void OnTriggerStay (Collider col){
		//Player pegou
		if (col.tag == "Player") {

			InventoryController ic = col.GetComponentInParent<InventoryController> ();

			if (ic.addItem (type)) {
				Destroy (this.gameObject);
				SoundManager.SM.PlayItemPickup ();
			}
		}
	}
}
