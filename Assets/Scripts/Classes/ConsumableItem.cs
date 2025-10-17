using UnityEngine;

public class ConsumableItem
{
    public string itemName { get; protected set; }

    public int itemCost { get; protected set; }    

    public virtual void Effect() 
    {

    }
    
}

public class InvisibleCloak : ConsumableItem
{
    public InvisibleCloak() 
    {
        itemName = "Invisible Cloak";
        itemCost = 10; 
    }

    public override void Effect() 
    {
        // Set a bool to false, to make the enemies not see the player
    }


}

public class FreezeMagic : ConsumableItem 
{
    
    public FreezeMagic() 
    {
        itemName = "Freeze Magic";
        itemCost = 25; 
    }

    public override void Effect() 
    {
        // Access to the list of enemies and make them changing their state to Freeze
    }
}





