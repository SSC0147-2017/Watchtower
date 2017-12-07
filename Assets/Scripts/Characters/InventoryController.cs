using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    public Movement playerBehav;

    public enum itemType { bread, bomb, arquebus };
    public itemType selectedItem;

    [Header("HP increment per Bread")]
    public int hpInc;

    public int maxBread;
    public int maxBombs;

    public int currBread;
    public int currBombs;
    public bool hasArquebus;


    void Start()
    {

        if (playerBehav == null)
            playerBehav = gameObject.GetComponent<Movement>();
        selectedItem = itemType.bread;
        currBread = 1;
        currBread = 0;
        hasArquebus = false;

    }

    void FixedUpdate()
    {
        //if (Input.GetButtonDown (playerBehav.Controller + "RightBumper")) {
        if (Input.GetButtonDown(playerBehav.Controller + "RightBumper"))
        {
            nextItem();
        }
        if (Input.GetButtonDown(playerBehav.Controller + "LeftBumper"))
        {
            prevItem();
        }
        if (Input.GetAxis(playerBehav.Controller + "Triggers") < 0)
        {
            useCurrentItem();
        }
        if (Input.GetAxis(playerBehav.Controller + "Triggers") > 0)
        {
            dropCurrentItem();
        }
    }

    /**
	 * Cycles to the next valid item or stays with the current if there is not a next valid.
	 */
    public void nextItem()
    {
        for (int i = 1; i <= 2; i++)
        {
            int nextItem = ((int)selectedItem + i) % 3;

            if (nextItem == (int)itemType.arquebus && hasArquebus)
            {
                selectedItem = itemType.arquebus;
                break;
            }
            else if (nextItem == (int)itemType.bomb && currBombs > 0)
            {
                selectedItem = itemType.bomb;
                break;
            }
            else if (nextItem == (int)itemType.bread && currBread > 0)
            {
                selectedItem = itemType.bread;
                break;
            }
        }
    }

    /**
	 * Cycles to the next valid item or stays with the current if there is not a next valid.
	 */
    public void prevItem()
    {
        for (int i = 1; i <= 2; i++)
        {
            int nextItem = Mathf.Abs(((int)selectedItem - i) + 3) % 3;

            if (nextItem == (int)itemType.arquebus && hasArquebus)
            {
                selectedItem = itemType.arquebus;
                break;
            }
            else if (nextItem == (int)itemType.bomb && currBombs > 0)
            {
                selectedItem = itemType.bomb;
                break;
            }
            else if (nextItem == (int)itemType.bread && currBread > 0)
            {
                selectedItem = itemType.bread;
                break;
            }
        }
    }



    /**
	 * Drops current selected item
	 */
    public bool dropCurrentItem()
    {
        switch (selectedItem)
        {
            case itemType.bread:
                if (currBread > 0)
                {
                    currBread--;
                    //INSTANTIATE BREAD PICKUP PREFAB
                }
                break;
            case itemType.bomb:
                if (currBombs > 0)
                {
                    currBombs--;
                    //INSTANTIATE BOMB PICKUP PREFAB
                }
                break;
            case itemType.arquebus:
                if (hasArquebus)
                {
                    hasArquebus = false;
                    //INSTANTIATE ARQUEBUS PICKUP PREFAB
                }
                break;
        }
        return true;
    }

    public bool useCurrentItem()
    {
        switch (selectedItem)
        {
            case itemType.bread:
                useBread();
                break;
            case itemType.bomb:
                useBomb();
                break;
            case itemType.arquebus:
                useArquebus();
                break;
        }
        return true;
    }


    /**
	 *Decrements bread, increases HP
	 */
    public void useBread()
    {
        if (currBread > 0)
        {
            playerBehav.GainHP(hpInc);
            currBread--;
        }
    }

    /**
	 * Decrements bombs, instantiates bomb
	 */
    public void useBomb()
    {
        if (currBombs > 0)
        {
            //INSTANCIA A BOMBA
            currBombs--;
        }
    }

    //TODO ?
    public void useArquebus()
    {
        /*
		Apertou o Trigger = Desenha o raio mostrando onde o tiro vai.
		Soltou o Trigger = Dispara
		Apertou o outro tigger = Cancela
		*/
    }



    /**
	 * Função para incremento dos itens
	 * 
	 * @param item = enum do tipo de item que foi coletado
	 * 
	 * @return true = item foi pego e incrementado, o prefab que chamar a função deve se destruir
	 * @return false = o item não foi pego, o item se mantem
	 */
    public bool addItem(itemType item)
    {
        switch (item)
        {
            case itemType.bomb:
                {
                    if (currBombs < maxBombs)
                    {
                        this.currBombs++;
                        return true;
                    }
                    else
                        return false;
                }

            case itemType.bread:
                {
                    if (currBread < maxBombs)
                    {
                        this.currBread++;
                        return true;
                    }
                    else
                        return false;
                }

            case itemType.arquebus:
                {
                    if (!hasArquebus)
                    {
                        this.hasArquebus = true;
                        return true;
                    }
                    else
                        return false;
                }
            default:
                return false;
        }
    }

}
