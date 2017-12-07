using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {


	public enum itemType{bread,bomb,arquebus};

	public int maxBread;
	public int maxBombs;

	public int currBread;
	public int currBombs;
	public bool hasArquebus;


	void Start(){
	
		currBread = 1;
		currBread = 0;
		hasArquebus = false;

	}
		
	/**
	 * Função para incremento dos itens
	 * 
	 * @param item = enum do tipo de item que foi coletado
	 * 
	 * @return true = item foi pego e incrementado, o prefab que chamar a função deve se destruir
	 * @return false = o item não foi pego, o item se mantem
	 */
	public bool addItem(itemType item){
		switch(item){
		case itemType.bomb: {
			if (currBombs < maxBombs) {
				this.currBombs++;
				return true;
			} else
				return false;
		}

		case itemType.bread: {
			if (currBread < maxBombs) {
				this.currBread++;
				return true;
			} else
				return false;	
		}

		case itemType.arquebus: {
			if (!hasArquebus) {
				this.hasArquebus = true;
				return true;
			} else
				return false;
		}
		default:
			return false;
		}
	}
}
