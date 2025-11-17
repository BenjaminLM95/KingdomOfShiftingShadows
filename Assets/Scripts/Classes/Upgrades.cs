using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public enum typeUpgrade 
{
    Sword,
    Health,
    Speed,
    Push
}

public class Upgrades
{
    public List<Upgrade> swordUpgrades = new List<Upgrade>();
    public List<Upgrade> healthUpgrades = new List<Upgrade>();
    public List<Upgrade> speedUpgrades = new List<Upgrade>();
    public List<Upgrade> pushUpgrades = new List<Upgrade>(); 


    // Upgrades for swords

    public void AddSwordUpgrades() 
    {
        swordUpgrades.Add(new Upgrade(0, "Great Sword", "Increases ATK by 3", 1, 50, 3, typeUpgrade.Sword));
        swordUpgrades.Add(new Upgrade(1, "Great Sword+", "Increases ATK by 5", 2, 125, 5, typeUpgrade.Sword));
        swordUpgrades.Add(new Upgrade(2, "Hero Sword", "Increases ATK by 8", 3, 300, 8, typeUpgrade.Sword));
        swordUpgrades.Add(new Upgrade(3, "Hero Sword+", "Increases ATK by 12", 4, 700, 12, typeUpgrade.Sword));
        swordUpgrades.Add(new Upgrade(99, "MAX OUT", "", 5, 0, 0, typeUpgrade.Sword)); 

    }

    public void AddHealthUpgrades() 
    {
        healthUpgrades.Add(new Upgrade(4, "Health+", "Increases MaxHP by 10", 1, 50, 10, typeUpgrade.Health));
        healthUpgrades.Add(new Upgrade(5, "Health++", "Increases MaxHP by 20", 2, 125, 20, typeUpgrade.Health));
        healthUpgrades.Add(new Upgrade(6, "Super Health", "Increases MaxHP by 30", 3, 300, 30, typeUpgrade.Health));
        healthUpgrades.Add(new Upgrade(7, "Mega Health", "Increases MaxHP by 40", 4, 700, 40, typeUpgrade.Health));
        healthUpgrades.Add(new Upgrade(98, "MAX OUT", "", 5, 0, 0, typeUpgrade.Health));
    }

    public void AddSpeedUpgrades() 
    {
        speedUpgrades.Add(new Upgrade(8, "Speedy Boots", "Increases Speed by 25% the original speed", 1, 50, 1, typeUpgrade.Speed));
        speedUpgrades.Add(new Upgrade(9, "Speedy Boots+", "Increases Speed by 50% the original speed", 2, 125, 2, typeUpgrade.Speed));
        speedUpgrades.Add(new Upgrade(10, "Light Boots", "Increases Speed 75% times the original speed", 3, 300, 3, typeUpgrade.Speed));
        speedUpgrades.Add(new Upgrade(11, "Light Boots+", "Increases Speed 2 times the original speed", 4, 700, 4, typeUpgrade.Speed));
        speedUpgrades.Add(new Upgrade(97, "MAX OUT", "", 5, 0, 0, typeUpgrade.Speed)); 
        
    }

    public void AddPushUpgrades() 
    {
        pushUpgrades.Add(new Upgrade(12, "Push1", "Push the enemies further", 1, 50, 2, typeUpgrade.Push));
        pushUpgrades.Add(new Upgrade(13, "Push2", "Push the enemies even further", 2, 300, 3, typeUpgrade.Push)); 
    }
}

public class Upgrade 
{
    public int id;
    public string name;
    public string description;
    public int tier;
    public int cost;
    public int value;
    public typeUpgrade type; 

    public Upgrade(int _id, string _name, string _description, int _tier, int _cost, int _value, typeUpgrade _type) 
    {
        id = _id;
        name = _name;
        description = _description;
        tier = _tier;
        cost = _cost;
        value = _value;
        type = _type;   
    }

    public virtual int UpgradeEffect() 
    {
        return value; 
    }

}


