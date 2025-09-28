using UnityEngine;
using UnityEngine.SceneManagement;

public class ToUpgradeShop : MonoBehaviour
{
    public LevelManager levelManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelManager = FindFirstObjectByType<LevelManager>();
    }

    

    public void goToUpgradeShop() 
    {
        levelManager.ChangeToUpgrade(); 
    }
}
