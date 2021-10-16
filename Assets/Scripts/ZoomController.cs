using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomController : MonoBehaviour
{
    float zoomFactor = 1;                           // factor of how much zoom, 
    static float pixelPerSecond = 100;              // how many pixels equal one second
    float prevZoomFactor = 1;                       // the previously used factor, needed for zooming in and out
    public GameObject SongProgressMarker;           // parent of the time markers
    public GameObject laneA, laneS, laneD, laneF;   // parents of all the notes

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // handle scroll wheel input and update stuff accordingly
        if (Input.mouseScrollDelta.y > 0)
        {
            IncreaseZoom();
            pixelPerSecond = 100 * zoomFactor;
            UpdateGameObjectPositions();
        }
        else if(Input.mouseScrollDelta.y < 0)
        {
            DecreaseZoom();
            pixelPerSecond = 100 * zoomFactor;
            UpdateGameObjectPositions();
        }
    }

    /**
     * Returns how many pixels are in a second
     */
    public static float GetPixelPerSecond()
    {
        return pixelPerSecond;
    }

    // statemachine/fixed values for how zooming in behaves
    void IncreaseZoom()
    {
        prevZoomFactor = zoomFactor;
        switch(zoomFactor)
        {
            case 4:
                zoomFactor = 4f;
                break;
            case 3:
                zoomFactor = 4f;
                break;
            case 2:
                zoomFactor = 3f;
                break;
            case 1.5f:
                zoomFactor = 2f;
                break;
            case 0.5f:
                zoomFactor = 1f;
                break;
            default:
                zoomFactor = 1.5f;
                break;
        }
    }

    // statemachine/fixed values for how zooming out behaves
    void DecreaseZoom()
    {
        prevZoomFactor = zoomFactor;
        switch(zoomFactor)
        {
            case 4:
                zoomFactor = 3f;
                break;
            case 3:
                zoomFactor = 2f;
                break;
            case 2:
                zoomFactor = 1.5f;
                break;
            case 1.5f:
                zoomFactor = 1f;
                break;
            case 0.5f:
                zoomFactor = 0.5f;
                break;
            default:
                zoomFactor = 0.5f;
                break;
        }
    }

    // update the coordinates of each note and time marker by removing the old zoom and apply the new zoom
    void UpdateGameObjectPositions()
    {
        for(int i = 0; i < SongProgressMarker.transform.childCount; i++)
        {
            Vector3 pos = SongProgressMarker.transform.GetChild(i).transform.localPosition;
            SongProgressMarker.transform.GetChild(i).transform.localPosition = new Vector3(pos.x, SongProgressMarker.transform.GetChild(i).transform.localPosition.y / prevZoomFactor, pos.z);
            SongProgressMarker.transform.GetChild(i).transform.localPosition = new Vector3(pos.x, SongProgressMarker.transform.GetChild(i).transform.localPosition.y * zoomFactor, pos.z);
        }
        for(int i = 0; i < laneA.transform.childCount; i++)
        {
            Vector3 pos = laneA.transform.GetChild(i).transform.localPosition;
            laneA.transform.GetChild(i).transform.localPosition = new Vector3(pos.x, laneA.transform.GetChild(i).transform.localPosition.y / prevZoomFactor, pos.z);
            laneA.transform.GetChild(i).transform.localPosition = new Vector3(pos.x, laneA.transform.GetChild(i).transform.localPosition.y * zoomFactor, pos.z);
        }
        for (int i = 0; i < laneS.transform.childCount; i++)
        {
            Vector3 pos = laneS.transform.GetChild(i).transform.localPosition;
            laneS.transform.GetChild(i).transform.localPosition = new Vector3(pos.x, laneS.transform.GetChild(i).transform.localPosition.y / prevZoomFactor, pos.z);
            laneS.transform.GetChild(i).transform.localPosition = new Vector3(pos.x, laneS.transform.GetChild(i).transform.localPosition.y * zoomFactor, pos.z);
        }
        for (int i = 0; i < laneD.transform.childCount; i++)
        {
            Vector3 pos = laneD.transform.GetChild(i).transform.localPosition;
            laneD.transform.GetChild(i).transform.localPosition = new Vector3(pos.x, laneD.transform.GetChild(i).transform.localPosition.y / prevZoomFactor, pos.z);
            laneD.transform.GetChild(i).transform.localPosition = new Vector3(pos.x, laneD.transform.GetChild(i).transform.localPosition.y * zoomFactor, pos.z);
        }
        for (int i = 0; i < laneF.transform.childCount; i++)
        {
            Vector3 pos = laneF.transform.GetChild(i).transform.localPosition;
            laneF.transform.GetChild(i).transform.localPosition = new Vector3(pos.x, laneF.transform.GetChild(i).transform.localPosition.y / prevZoomFactor, pos.z);
            laneF.transform.GetChild(i).transform.localPosition = new Vector3(pos.x, laneF.transform.GetChild(i).transform.localPosition.y * zoomFactor, pos.z);
        }
    }
}
