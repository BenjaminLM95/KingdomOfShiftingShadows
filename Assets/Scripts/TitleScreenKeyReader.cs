using UnityEngine;

public class TitleScreenKeyReader : MonoBehaviour
{
    public LevelManager levelManager = null;

    private void Start()
    {
        if(levelManager == null) 
        {
            levelManager = GameObject.FindObjectOfType<LevelManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey) 
        {
            levelManager.ChangeToMainMenu(); 
        }
    }
}
