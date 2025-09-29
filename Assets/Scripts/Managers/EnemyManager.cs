using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemy List")]
    public GameObject enemyTypeOne;
    [SerializeField] private List<Enemy> enemyList = new List<Enemy>();
    [SerializeField] private int enemyDeployed = 0; 


    [Header("Setting")]
    [SerializeField] private float timeSpawn = 4.0f;

    private float timer = 0f; 


    [Header("Reference")]
    public KingdomGate _kingdomGate;
    [SerializeField] private GameObject spawnPoint1; 
    [SerializeField] private GameObject spawnPoint2;
    [SerializeField] private GameObject spawnPoint3;
    [SerializeField] private GameObject spawnPoint4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        spawnPoint1 = GameObject.Find("SpawnPointA");
        spawnPoint2 = GameObject.Find("SpawnPointB");
        spawnPoint3 = GameObject.Find("SpawnPointC");
        spawnPoint4 = GameObject.Find("SpawnPointD"); 
    }

    // Update is called once per frame
    void Update()
    {         

        if(_kingdomGate.gameOver != true && Time.time > timer) 
        {             
            SpawnEnemy(); 
        }

        if(timeSpawn > 0.5f)
        timeSpawn -= (Time.deltaTime * Time.deltaTime); 
    }

    private void SpawnEnemy() 
    {
        GameObject newEnemy = Instantiate(enemyTypeOne, GetRandomSpawnPosition(), Quaternion.identity);
        enemyList.Add(newEnemy.GetComponent<Enemy>());
        enemyDeployed++; 
        timer = Time.time + timeSpawn;
        //Debug.Log(Time.time + "  " + timer);
    }

    private Vector3 GetRandomSpawnPosition() 
    {
        int randomNum = Random.Range(1, 5);
        Vector3 spawnPosition; 

        switch (randomNum) 
        {
            case 1: 
                spawnPosition = spawnPoint1.transform.position;
                break;
            case 2: 
                spawnPosition = spawnPoint2.transform.position;
                break;
            case 3:     
                spawnPosition = spawnPoint3.transform.position;
                break;
            case 4: 
                spawnPosition = spawnPoint4.transform.position;
                break;
            default: 
                spawnPosition = spawnPoint1.transform.position;
                break;
        }

        return spawnPosition;
    }


    public void RemoveEnemyOnList(Enemy enemyDefeated) 
    {
        enemyList.RemoveAt(enemyList.IndexOf(enemyDefeated)); 
    }

}
