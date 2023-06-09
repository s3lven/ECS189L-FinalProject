using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] GameObject newToppingPrefab;
    private ToppingsMinigame toppingsMinigame;

    void Start()
    {
        toppingsMinigame = GameObject.Find("Toppings_MG").GetComponent<ToppingsMinigame>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0) 
        {
            // Grab the object and its script (to access public member)
            GameObject dropped = eventData.pointerDrag;
            Topping topping = dropped.GetComponent<Topping>();

            // Place the item into the empty slot instead of resetting to original slot
            topping.parentAfterDrag = transform;
            // Spawn another item for replayability
            GameObject childObject = Instantiate(newToppingPrefab);
            // Move that spawned item into the slot from before
            childObject.transform.parent = topping.parentBeforeDrag.transform;
            toppingsMinigame.isToppingLoaded = true;
        }
    }
}
