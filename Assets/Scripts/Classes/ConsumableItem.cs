using System.Collections.Generic; 
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
        itemCost = 75; 
    }

    public override void Effect() 
    {
        // Access to the list of enemies and make them changing their state to Freeze
        List<Enemy> enemies = new List<Enemy>(); 
        enemies = EnemyManager.enemyList;

        Debug.Log("Enemy list Count " +EnemyManager.enemyList.Count);


        for (int i = 0; i < enemies.Count; i++) 
        {
            Debug.Log("Enemy List Object: " + i);

            enemies[i].BeFrozen();
            enemies[i].healthSystem.TakeDamage(4);            
        }
    }
}

public class WindSlash : ConsumableItem 
{
    public WindSlash() 
    {
        itemName = "Wind Slash";
        itemCost = 30; 
    }

    public override void Effect()
    {
        // Player do the wind slash
        PlayerController.windSlash = true;
         
    }
}








