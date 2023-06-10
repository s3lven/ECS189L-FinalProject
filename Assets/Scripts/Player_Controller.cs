using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    // Components
    Rigidbody rigidbody;
    Transform avatar;

    // Player Movement
    [SerializeField] public InputAction inputAction;
    [SerializeField] float movementSpeed;
    Vector2 movementInput;
    public bool canMove;

    // Interaction
    [SerializeField] public InputAction mouseBinding;
    [SerializeField] public InputAction interactionBinding;
    [SerializeField] LayerMask interactLayer;
    Vector2 mousePositionInput;
    Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        // Assign the rigidbody component to get velocity
        rigidbody = GetComponent<Rigidbody>();
        // Assign the sprite to the player so we can move it
        avatar = transform.GetChild(0);
        camera = Camera.main;
        canMove = true;

    }

    private void Awake()
    {
        // Subscribe the function to the player object
        // On left click, use the Interact function
        interactionBinding.performed += Interact;
    }

    // Update is called once per frame
    void Update()
    {
        // Assign the scale to move the player
        movementInput = inputAction.ReadValue<Vector2>();
        if (movementInput.x != 0 && this.canMove)
        {
            avatar.localScale = new Vector2(Mathf.Sign(movementInput.x), 1);
        }

        // Move the mouse
        mousePositionInput = mouseBinding.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        if(this.canMove)
        {
            // Move the player
            rigidbody.velocity = movementInput * movementSpeed;
        }
        
    }

    private void OnEnable()
    {
        inputAction.Enable();
        mouseBinding.Enable();
        interactionBinding.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
        mouseBinding.Disable();
        interactionBinding.Disable();
    }

    // Allow the player to interact with objects with the mouse
    void Interact(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(mousePositionInput);
            if(Physics.Raycast(ray, out hit, interactLayer))
            {
                if(hit.transform.tag == "Interactable")
                {
                    // Debug.Log("Interacted");
                    if (!hit.transform.GetChild(0).gameObject.activeInHierarchy)
                    {
                        return;
                    }
                    Interactable temp = hit.transform.GetComponent<Interactable>();
                    temp.PlayMiniGame();
                }
            }
        }
    }

    public void StopPlayer()
    {
        this.rigidbody.isKinematic = true;  
    }

    public void RestartPlayer()
    {
        this.rigidbody.isKinematic = false;
    }
}


