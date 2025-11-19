using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Inventory _inventory = new Inventory();
    [SerializeField] ItemDisplayHandler itemDisplay;
    [SerializeField] private UpgradeManager _upgradeManager; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake ()
    {
        itemDisplay = FindFirstObjectByType<ItemDisplayHandler>();
        _upgradeManager = FindFirstObjectByType<UpgradeManager>();       
        
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

    private void UpgradeInventory() 
    {        

        _inventory.inventory.Clear();      


        for(int i = 0; i < UpgradeManager.playerInventory.inventory.Count; i++) 
        {
            _inventory.inventory.Add(UpgradeManager.playerInventory.inventory[i]);
            Debug.Log(_inventory.inventory.Count);
            Debug.Log(UpgradeManager.playerInventory.inventory.Count); 
        }

        Debug.Log("Inventory Upgraded"); 
    }

    private void OnEnable()
    {
        if (_upgradeManager == null)
        {
            _upgradeManager = FindFirstObjectByType<UpgradeManager>();
        }
            
        UpgradeInventory();
        ShowItemNames();
        itemDisplay.UpdateItemImages(_inventory);
        
    }
}
