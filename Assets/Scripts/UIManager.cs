using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GraphicRaycaster _raycaster;
    private PointerEventData _pointerData;
    private EventSystem _eventSystem;

    public Transform selectionPoint;

    public static UIManager sample;

    public static UIManager Sample
    {
        get
        {
            if (sample == null)
            {
                sample = FindObjectOfType<UIManager>();
            }
            return sample;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _raycaster = GetComponent<GraphicRaycaster>();
        _eventSystem = GetComponent<EventSystem>();
        _pointerData = new PointerEventData(_eventSystem);

        _pointerData.position = selectionPoint.position;
    }

    
    // check if there is an object in the center of the panel
    public bool OnEntered(GameObject button) // button from buttonManager Class
    {
        List<RaycastResult> results = new List<RaycastResult>();
        _raycaster.Raycast(_pointerData, results);

        foreach(var result in results)
        {
            if(result.gameObject == button)
            {
                return true;
            }
        }
        return false;
    }
}
