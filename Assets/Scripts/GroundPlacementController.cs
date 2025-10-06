using System;
using UnityEngine;

public class GroundPlacementController : MonoBehaviour
{
    [SerializeField] private GameObject placeableObjectPrefab;

    [SerializeField] KeyCode newObjectHotKey = KeyCode.V;

    private GameObject currentPlaceableObject;

    private float mouseWheelRotation;

    private void Update()
    {
        HandleNewObjectHotkey();

        if (currentPlaceableObject != null)
        {
            MoveCurrentObjectToMouse();
            RotateFromMouseWheel();
            ReleaseIfClicked();
        }
    }

    private void HandleNewObjectHotkey()
    {
        if (Input.GetKeyDown(newObjectHotKey))
        {
            if (currentPlaceableObject != null)
            {
                Destroy(currentPlaceableObject);
            }
        }
        else
        {
            currentPlaceableObject = Instantiate(placeableObjectPrefab);
        }
    }

    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            currentPlaceableObject.transform.position = hitInfo.point;
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }

    void RotateFromMouseWheel()
    {
        Debug.Log(Input.mouseScrollDelta);
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    }

    void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentPlaceableObject = null;
        }
    }
}
