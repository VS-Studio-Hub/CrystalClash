using UnityEngine;
using UnityEngine.InputSystem;

public class CarShooterController : MonoBehaviour
{
    [SerializeField] private Camera aimCamera;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;

    public InputActionAsset inputActions;

    private InputAction aimAction;
    private InputAction shootAction;

    bool aimInput;
    bool shootInput;

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        var playerMap = inputActions.FindActionMap("Player");
        aimAction = playerMap.FindAction("Aim");
        shootAction = playerMap.FindAction("Shoot");
    }

    void GetInput()
    {
        aimInput = aimAction.ReadValue<float>() > 0.5f;
        shootInput = shootAction.ReadValue<float>() > 0.5f;
    }

    private void Update()
    {
        GetInput();

        // Toggle cameras based on aim input
        if (aimInput)
        {
            aimCamera.gameObject.SetActive(true);
            mainCamera.gameObject.SetActive(false);
        }
        else
        {
            aimCamera.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);
        }

        Vector3 mouseWorldPosition = Vector3.zero;

        // Always use the current active camera for raycasting
        Camera currentCam = aimInput ? aimCamera : mainCamera;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = currentCam.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }


        if (shootInput)
        {
            Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
            shootInput = false;
        }
    }
}
