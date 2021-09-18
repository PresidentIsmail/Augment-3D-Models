using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit.AR;
using UnityEngine.XR.ARSubsystems;

public class InputManager : ARBaseGestureInteractable
{

    [SerializeField]
    private Camera arCamera;
    [SerializeField]
    ARRaycastManager _raycastManager;
    [SerializeField]
    private GameObject crosshair;

    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private Touch touch;
    private Pose pose;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // make sure user doesn't put objects ontop of each other
    protected override bool CanStartManipulationForGesture(TapGesture gesture)
    {
        if (gesture.targetObject == null)        
            return true;        
        return false;
    }


    protected override void OnEndManipulation(TapGesture gesture)
    {
        if (gesture.isCanceled)        
            return;
        
        if (gesture.targetObject != null || IsPointerOverUI(gesture))        
            return;        

        // object is put in the same place as the anchor
        if (GestureTransformationUtility.Raycast(gesture.startPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            GameObject placeObj = Instantiate(dataHandler.Instance.GetFurniture(), pose.position, pose.rotation);
            
            // make sure object has a placement anchor
            var anchorObject = new GameObject("PlacementAnchor");
            anchorObject.transform.position = pose.position;
            anchorObject.transform.rotation = pose.rotation;
            placeObj.transform.parent = anchorObject.transform;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        CrosshairCalculation();
    }

 
    bool IsPointerOverUI(TapGesture touch)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touch.startPosition.x, touch.startPosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        return results.Count > 0;
    }

    void CrosshairCalculation()
    {
        Vector3 origin = arCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));

        if (GestureTransformationUtility.Raycast(origin, hits, TrackableType.PlaneWithinPolygon))
        {
            pose = hits[0].pose;
            crosshair.transform.position = pose.position;
            crosshair.transform.eulerAngles = new Vector3(90, 0, 0);
        }
    }
}
