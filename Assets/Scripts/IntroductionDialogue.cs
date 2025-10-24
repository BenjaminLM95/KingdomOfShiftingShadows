using UnityEngine;
using TMPro;
public class IntroductionDialogue : MonoBehaviour
{
    public GameObject buttonNext;
    public GameObject buttonPrevious;
    public GameObject buttonBegin;
    public TextMeshProUGUI introText;

    private string[] introDialogues = new string[3];
    
    [SerializeField] private LevelManager levelManager; 
    private int dialIndex = 0; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SettingTheDialogue(); 
        levelManager = FindFirstObjectByType<LevelManager>();        
        dialIndex = 0; 
        introText.text = introDialogues[dialIndex];
        SettingButtons(dialIndex); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SettingTheDialogue() 
    {
        introDialogues[0] = "Sky, the last warrior of the Daydream Kingdom, is sent to defend it against evil witches and prevent them from entering through the gates.\r\nThe warrior has to be alert day and night!!";
        introDialogues[1] = "Survive for five days! Reach the dawn of the sixth day. \r\n All our hopes are resting on Sky";
        introDialogues[2] = "Move with Key Arrow or WASD. Press 'I' to attack. Prevent enemies from reaching the Kingdom Gate. (Pink Rectangle)";
    }

    public void NextDialogue() 
    {
        if (dialIndex < introDialogues.Length)
        {
            dialIndex++;
            introText.text = introDialogues[dialIndex];

        }
        else if (dialIndex == introDialogues.Length) 
        {
            return; 
        }

        SettingButtons(dialIndex);
        
    }

    public void PreviousDialogue() 
    {
        if(dialIndex > 0) 
        {
            dialIndex--;
            introText.text = introDialogues[dialIndex];
        }
        else 
        {
            return;
        }

        SettingButtons(dialIndex); 
    }

    public void DisableButtons() 
    {
        buttonNext.SetActive(false);
        buttonPrevious.SetActive(false);
        buttonBegin.SetActive(false);
    }

    public void SettingButtons(int index) 
    {
        DisableButtons();
        
        if(index <= 0) 
        {
            buttonNext.SetActive(true);
        }
        else if(index >= introDialogues.Length - 1) 
        {
            buttonPrevious.SetActive(true);
            buttonBegin.SetActive(true);
        }
        else 
        {
            buttonNext.SetActive(true);
            buttonPrevious.SetActive(true);
        }

    }

    public void BeginGamePlay() 
    {
        ResetDialogue();       
        levelManager.StartNewGame(); 
    }

    public void ResetDialogue() 
    {
        dialIndex = 0;
        introText.text = introDialogues[dialIndex];
        SettingButtons(dialIndex);
    }
}
