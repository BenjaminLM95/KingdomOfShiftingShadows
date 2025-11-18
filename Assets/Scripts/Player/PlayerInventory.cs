using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Inventory _inventory = new Inventory();
    [SerializeField] ItemDisplayHandler itemDisplay;  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemDisplay = FindFirstObjectByType<ItemDisplayHandler>();       
        _inventory.SetInventorySlots(3);        
        _inventory.GetAnItem(new FreezeMagic());
        _inventory.GetAnItem(new FreezeMagic());                
        ShowItemNames();
        itemDisplay.UpdateItemImages(_inventory); 
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            if (_inventory.inventory.Count > 0)
            {
                _inventory.UseAnItem(0);
                ShowItemNames();
                itemDisplay.UpdateItemImages(_inventory);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (_inventory.inventory.Count > 1)
            {
                _inventory.UseAnItem(1);
                ShowItemNames();
                itemDisplay.UpdateItemImages(_inventory);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (_inventory.inventory.Count > 2)
            {
                _inventory.UseAnItem(2);
                ShowItemNames();
                itemDisplay.UpdateItemImages(_inventory);
            }
        }
       
    }

    public void ShowItemNames() 
    {
        for (int i = 0; i < _inventory.inventory.Count; i++) 
        {
            Debug.Log(_inventory.inventory[i].itemName + " , " + _inventory.inventory[i].itemCost); 
        }
    }
}
