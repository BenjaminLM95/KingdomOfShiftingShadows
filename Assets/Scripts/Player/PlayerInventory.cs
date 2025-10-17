using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Inventory _inventory = new Inventory(); 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _inventory.SetInventorySlots(5);
        _inventory.GetAnItem(new InvisibleCloak());
        _inventory.GetAnItem(new FreezeMagic());
        _inventory.GetAnItem(new FreezeMagic());
        _inventory.GetAnItem(new FreezeMagic());
        _inventory.GetAnItem(new InvisibleCloak());
        _inventory.GetAnItem(new InvisibleCloak());
        ShowItemNames(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            _inventory.UseAnItem(0); 
            ShowItemNames();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _inventory.UseAnItem(1);
            ShowItemNames();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _inventory.UseAnItem(2);
            ShowItemNames();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _inventory.UseAnItem(3);
            ShowItemNames();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _inventory.UseAnItem(4);
            ShowItemNames();
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
