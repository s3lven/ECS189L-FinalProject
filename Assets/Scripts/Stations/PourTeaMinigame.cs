using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Boba;

public class PourTeaMinigame : MonoBehaviour
{
    [SerializeField] GameObject GamePanel;
    private PlayerController playerController;
    private DrinkController drinkController;
    TeaTypes tea;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioSource done;

    public void PressButtonPanelClose()
    {
        GamePanel.SetActive(false);
    }

    void OnEnable()
    {
        playerController.StopPlayer();
        if (drinkController._isTeaAdded)
        {
            PressButtonPanelClose();
        }
    }

    void OnDisable()
    {
        playerController.RestartPlayer();
    }

    void Awake()
    {
        drinkController = GameObject.FindGameObjectWithTag("Script Home").GetComponent<DrinkController>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void PourTea()
    {
        // Debug.Log("Minigame Start!");
        // Checks which button is pressed and assigns the tea
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        switch (buttonName)
        {
            case "BlackTea_Button":
                // Debug.Log("I poured black tea");
                tea = TeaTypes.BlackTea;
                break;
            case "GreenTea_Button":
                // Debug.Log("I poured green tea");
                tea = TeaTypes.GreenTea;
                break;
            case "OolongTea_Button":
                // Debug.Log("I poured oolong tea");
                tea = TeaTypes.OolongTea;
                break;
            default:
                Debug.Log("Tea not found");
                break;
        }
        // Debug.Log("Wait Start");
        // Start animation here
        // Assign the drink
        if (!drinkController._isTeaAdded)
        {
            source.Play();

            drinkController.AddIngredient(tea);
        }
        StartCoroutine(WaitButtonPressed());
        
    }

    IEnumerator WaitButtonPressed()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Tea has been poured");
        done.Play();
        // Play sound here to signify completion
        // Debug Function
        // drinkController.CheckDrink();
        PressButtonPanelClose();
    }
}
