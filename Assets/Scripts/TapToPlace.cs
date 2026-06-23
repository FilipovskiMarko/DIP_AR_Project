using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

public class TapToPlace : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;
    // [SerializeField] Canvas canvas;
    [SerializeField] ARRaycastManager raycastManager;
    [SerializeField] ARPlaneManager planeManager;
    [SerializeField] GameObject objectToPlace;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    // Text placementText;
    bool isObjectPlaced = false;

    void Start()
    {
        // placementText = canvas.GetComponentInChildren<Text>();
        // placementText.text = "Tap to test placement"; 
        inputActions.Enable();
    }


    void Update()
    {
        if (inputActions.FindAction("Touch").triggered)
        {
            Vector2 touchPosition = inputActions.FindAction("Touch").ReadValue<Vector2>();

            if (raycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                if (isObjectPlaced){
                    // placementText.text = "Object placed";
                }
                else 
                {
                    GameObject plane = GameObject.FindObjectOfType<ARPlane>().gameObject;

                    Instantiate(objectToPlace, plane.transform.position, plane.transform.rotation);

                    isObjectPlaced = true;

                    planeManager.SetTrackablesActive(false);
                    planeManager.enabled = false;
                }
            }
            else
            {
                // placementText.text = "No hit detected";
            }
        }
    }
}
