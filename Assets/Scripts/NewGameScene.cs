using System.Collections;
using UnityEngine;

public class NewGameScene : MonoBehaviour
{
    public bool isStarted { get; private set; }
    public GameObject kingdomsLine;
    private Animator lineAnimator;
    public GameObject textIndicator; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isStarted = false; 
        lineAnimator = kingdomsLine.GetComponent<Animator>();
        lineAnimator.SetBool("itBegin", true);
        StartCoroutine(EndCutScene()); 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator EndCutScene() 
    {
        Debug.Log("EndCutScene");
        yield return new WaitForSeconds(2.5f);
        lineAnimator.SetBool("itBegin", false);
        yield return new WaitForSeconds(1f);
        isStarted = true;
        textIndicator.gameObject.SetActive(false); 
        
    }

    
}
