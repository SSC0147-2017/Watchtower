using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour {

    public GameObject player;

    float Cooldown;
    InventoryController.itemType CurrentItem;

    public Sprite BombSprite;
    public Sprite BreadSprite;
    public Sprite ArquebusSprite;

	// Use this for initialization
	void Start () {
		if(player != null)
        {
            Cooldown = player.GetComponent<Movement>().SpecialTime;
        }
	}
	
	// Update is called once per frame
	void Update () {

        /*if (player.GetComponent<Movement>().CurrentHP < 0)
            transform.Find("HealthBar").GetComponent<Image>().fillAmount = 0;
        else*/
        transform.Find("HealthBar").GetComponent<Image>().fillAmount = player.GetComponent<Movement>().CurrentHP;
        print(transform.Find("HealthBar").GetComponent<Image>().fillAmount);

        CurrentItem = player.GetComponent<InventoryController>().selectedItem;
        if (CurrentItem == InventoryController.itemType.bomb)
        {
            transform.Find("Item").GetComponent<Image>().sprite = BombSprite;
            print("bomb");
        }
        else if (CurrentItem == InventoryController.itemType.bread)
        {
            transform.Find("Item").GetComponent<Image>().sprite = BreadSprite;
            print("bread");
        }
        else if (CurrentItem == InventoryController.itemType.arquebus)
        {
            transform.Find("Item").GetComponent<Image>().sprite = ArquebusSprite;
            print("arq");
        }
    }

    void UseSkill()
    {
        //transform.Find("SkillBar").GetComponent<Image>().fillAmount = player.GetComponent<Movement>().SpecialTime;
    }
}
