using UnityEngine;
using UnityEngine.InputSystem;

public class RocketLauncher : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;

    private InputAction interactAction;

    [SerializeField] private GameObject rocketLauncherLvlOne;
    [SerializeField] private GameObject rocketLauncherLvlTwo;
    [SerializeField] private GameObject rocketLauncherLvlThree;

    [SerializeField] private Transform spawnPosition; // use Transform instead of GameObject

    private bool playerInRange = false;
    private GameObject currentCar;

    private void OnEnable()
    {
        var playerMap = inputActions.FindActionMap("Player");
        playerMap.Enable();

        interactAction = playerMap.FindAction("Intract");
        interactAction.performed += OnInteract; // register callback
    }

    private void OnDisable()
    {
        var playerMap = inputActions.FindActionMap("Player");
        playerMap.Disable();

        interactAction.performed -= OnInteract; // remove callback
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            playerInRange = true;
            currentCar = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            playerInRange = false;
            currentCar = null;
        }
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (playerInRange)
        {
            Instantiate(rocketLauncherLvlOne, spawnPosition.position, spawnPosition.rotation);
            Debug.Log("Rocket launcher spawned!");
        }
    }
}
