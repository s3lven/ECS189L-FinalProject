using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Boba;



public class SyrupPowderMinigame : MonoBehaviour
{
    [SerializeField] GameObject GamePanel;
    private PlayerController playerController;
    private DrinkController drinkController;
    SyrupPowderType syrupPowder;
    [SerializeField] private AudioSource source;

    public void PressButtonPanelClose()
    {
        GamePanel.SetActive(false);
    }

    void OnEnable()
    {
        playerController.StopPlayer();
        if (drinkController._isSyrupPowderAdded && drinkController._isMilkAdded)
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
        this.drinkController = GameObject.FindGameObjectWithTag("Script Home").GetComponent<DrinkController>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void PourSyrupTea()
    {
        // Debug.Log("Minigame Start!");
        // Check which button is pressed and assign the ingredient
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        switch (buttonName)
        {
            case "Sugar_Button":
                Debug.Log("I poured sugar");
                // Play sound here to signify completion
                syrupPowder = SyrupPowderType.Sugar;
                break;
            case "Milk_Button":
                Debug.Log("I poured milk");
                // Play sound here to signify completion
                syrupPowder = SyrupPowderType.Milk;
                break;
            default:
                Debug.Log("Syrup or Powder not found");
                break;
        }
        // Debug.Log("Wait Start");
        // Add the ingredient in the controller
        if(!drinkController._isSyrupPowderAdded)
        {
            source.Play();
            drinkController.AddIngredient(syrupPowder);
        }
        StartCoroutine(WaitButtonPressed());
        
    }

    IEnumerator WaitButtonPressed()
    {
        yield return new WaitForSeconds(5);
        // Debug.Log("Syrup/Powder poured");
        
        // drinkController.CheckDrink();
        PressButtonPanelClose();
    }
}
