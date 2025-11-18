using UnityEngine;
using UnityEngine.UI; 

public class ItemDisplayHandler : MonoBehaviour
{
    public Sprite shieldSp;
    public Sprite frozeSpellSp;
    public Sprite windSlashSp;
    public Sprite defaultSp;   
    public Image itemSlot1;
    public Image itemSlot2;
    public Image itemSlot3; 

    
    public void UpdateItemImages(Inventory _inventory) 
    {
        if (_inventory.inventory.Count == 3)
        {
            itemSlot1.sprite = SetSprite(_inventory.inventory[0]);
            itemSlot2.sprite = SetSprite(_inventory.inventory[1]);
            itemSlot3.sprite = SetSprite(_inventory.inventory[2]);
        }
        else if(_inventory.inventory.Count == 2) 
        {
            itemSlot1.sprite = SetSprite(_inventory.inventory[0]);
            itemSlot2.sprite = SetSprite(_inventory.inventory[1]);
            itemSlot3.sprite = SetBlankSprite(); 
        }
        else if(_inventory.inventory.Count == 1) 
        {
            itemSlot1.sprite = SetSprite(_inventory.inventory[0]);
            itemSlot2.sprite = SetBlankSprite();
            itemSlot3.sprite = SetBlankSprite();             
        }
        else if(_inventory.inventory.Count == 0) 
        {
            itemSlot1.sprite = SetBlankSprite();
            itemSlot2.sprite = SetBlankSprite();
            itemSlot3.sprite = SetBlankSprite();
        }

    } 

    public Sprite SetSprite(ConsumableItem _item) 
    {
        
        switch (_item) 
        {
            case FreezeMagic:
                return frozeSpellSp;                
            case WindSlash:
                return windSlashSp;
            default:
                return defaultSp; 
                
        }
    }

    public Sprite SetBlankSprite() 
    {
        return defaultSp; 
    }
}
