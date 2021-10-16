using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LaneScript : MonoBehaviour, IPointerClickHandler
{
    public GameObject positionMarker;   // marks current position and gets moved when a lane is clicked
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        // moves the marker to the position that was clicked
        positionMarker.transform.position = new Vector3(positionMarker.transform.position.x, pointerEventData.position.y, positionMarker.transform.position.z);
    }
}

