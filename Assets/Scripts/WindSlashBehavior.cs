using UnityEngine;

public class WindSlashBehavior : MonoBehaviour
{
    private float slashMovX;

    private int direction;    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slashMovX = 8;
        direction = 1; 

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + slashMovX * Time.deltaTime * direction, transform.position.y, transform.position.z); 
    }
}
