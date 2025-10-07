using UnityEngine;
using TMPro; 

public class ResultInformation : MonoBehaviour
{
    public TextMeshProUGUI resultText;

    [Header("All Reference")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private DayNightManager daynightManager; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        daynightManager = FindFirstObjectByType<DayNightManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        CheckForNullReference();
    }

    public void DisplayResult() 
    {
        CheckForNullReference();

        resultText.text = "Enemies killed: " + playerController.numKill + "\n" +
            "Days: " + daynightManager.dayCount + "\n" +
            "Time: " + daynightManager.GetTimeString() + "\n" +
            "Currency: " + playerController.currency; 
    }

    public void CheckForNullReference() 
    {
        if (playerController == null)
            playerController = FindFirstObjectByType<PlayerController>();

        if (daynightManager == null)
            daynightManager = FindFirstObjectByType<DayNightManager>();
    }
}
