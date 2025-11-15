using System.Collections;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    private GameObject playerObj;
    private float speedMove;
    private int value;
    public Vector3 moveVec; 

    [SerializeField] private SoundsManager soundManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        soundManager = FindFirstObjectByType<SoundsManager>();
        playerObj = GameObject.Find("Player");
        speedMove = 10f;
        moveVec = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(FollowPlayer());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(value);
            soundManager.PlaySoundFXClip("GetMoney", transform);
            playerObj.GetComponent<PlayerController>().upgradeManager.ObtainingMoneyReward(value);
            this.gameObject.SetActive(false);
        }
    }

    public void SetValue(int _value) 
    {
        value = _value;
    }


    private IEnumerator FollowPlayer() 
    {
        moveVec = (playerObj.transform.position - transform.position).normalized;

        transform.position += moveVec * Time.deltaTime * speedMove;

        yield return new WaitForSeconds(0.5f);
    }
    
}
