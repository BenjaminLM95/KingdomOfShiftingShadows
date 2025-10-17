using System.Collections.Generic;
using System;
using System.Linq;

public class Inventory
{
    public List<ConsumableItem> inventory = new List<ConsumableItem>();

    private int InventorySlots;  // how many item the inventory can hold

    public int inventorySlots
    {
        get { return InventorySlots; }

        set { InventorySlots = Math.Max(1, value); }   // the inventory cannot have negative numbers or 0 as slot numbers
    }

    public void SetInventorySlots(int num)
    {
        inventorySlots = num;
    }


    public void GetAnItem(ConsumableItem newItem) 
    {
        if(inventory.Count < InventorySlots)
        {
            inventory.Add(newItem);
        }
    }

    public void UseAnItem(int index) 
    {
        if (index <= inventory.Count)
        {
            if (inventory[index] != null)
            {
                inventory[index].Effect();
                inventory.Remove(inventory.ElementAt(index));
            }
        }
    }
}
