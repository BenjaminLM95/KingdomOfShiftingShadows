using UnityEngine;

public class WindSlashBehavior : MonoBehaviour
{
    public float slashMovX;

    private int direction;  
    
    private float velValue = 8f; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + slashMovX * Time.deltaTime, transform.position.y, transform.position.z); 
    }

    public void GetDirection(bool isRight) 
    {
        Debug.Log("Moving Wind Slash"); 

        if (!isRight) 
        {
            slashMovX = -8f;
            transform.localScale = new Vector3(-1, 1, 1);
            Debug.Log("Moving Left"); 
        }
        else 
        {
            slashMovX = 8f;
            transform.localScale = new Vector3(1, 1, 1);
            Debug.Log("Moving Right"); 
        }
    }
}
