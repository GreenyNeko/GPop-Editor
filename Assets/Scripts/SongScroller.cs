using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SongScroller : MonoBehaviour
{ 
    public GameObject content;              // this is what we move
    public GameObject prefabTimeMarker;     // required to create new time markers
    public GameObject SongProgressMarkers;  // the parent for the time markers
    public GameObject currPositionMarker;   // the marker of the current position, we need this to prevent it from scrolling if we don't want
    List<GameObject> timeMarkers;           // list of time marker objects
    float maxScrollOffset;                  // the furthest we scrolled (used for generating new time markers
    float scrollOffset;                     // how far we've scrolled currently (used for calculations)
    float cooldown;                         // prevent scrolling too far when using snapping

    // Start is called before the first frame update
    void Start()
    {
        // init
        timeMarkers = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;

        // create flags
        bool unsnapped = false;
        bool staticCurrPositionMarker = true;

        // set flags given system keys
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            unsnapped = true;
        }
        if(Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            staticCurrPositionMarker = false;
        }

        // handle input for scrolling
        if(Input.GetKey(KeyCode.DownArrow))
        {
            if(unsnapped)
            {
                content.transform.position += new Vector3(0, Time.deltaTime * 40, 0);
                scrollOffset -= Time.deltaTime * 40;
                if(staticCurrPositionMarker)
                {
                    currPositionMarker.transform.position -= new Vector3(0, Time.deltaTime * 40, 0);
                }
            }
            else
            {
                if(cooldown <= 0)
                {
                    content.transform.position += new Vector3(0, ZoomController.GetPixelPerSecond(), 0);
                    scrollOffset -= ZoomController.GetPixelPerSecond();
                    cooldown = 0.25f;
                    if (staticCurrPositionMarker)
                    {
                        currPositionMarker.transform.position += new Vector3(0, -ZoomController.GetPixelPerSecond(), 0);
                    }
                }
            }
        }
        else if(Input.GetKey(KeyCode.UpArrow))
        {
            if (unsnapped)
            {
                content.transform.position -= new Vector3(0, Time.deltaTime * 40, 0);
                scrollOffset += Time.deltaTime * 40;
                if (staticCurrPositionMarker)
                {
                    currPositionMarker.transform.position += new Vector3(0, Time.deltaTime * 40, 0);
                }
            }
            else
            {
                if (cooldown <= 0)
                {
                    content.transform.position -= new Vector3(0, ZoomController.GetPixelPerSecond(), 0);
                    scrollOffset += ZoomController.GetPixelPerSecond();
                    cooldown = 0.25f;
                    if (staticCurrPositionMarker)
                    {
                        currPositionMarker.transform.position += new Vector3(0, ZoomController.GetPixelPerSecond(), 0);
                    }
                }
            }
        }
        // set the new max amount we scrolled
        if (scrollOffset > maxScrollOffset)
        {
            maxScrollOffset = scrollOffset;
        }
        // create new marker when we've scrolled far enough
        if (maxScrollOffset > (timeMarkers.Count + 1) * ZoomController.GetPixelPerSecond())
        {
            GenerateTimeMarker();
        }
    }

    /**
     * Get the amount we've scrolled currently for calculations
     */
    public float GetScrollOffset()
    {
        return scrollOffset;
    }

    // Refactored, creates a new marker 
    void GenerateTimeMarker()
    {
        GameObject newMarker = Instantiate(prefabTimeMarker, SongProgressMarkers.transform);
        newMarker.transform.position += new Vector3(0, timeMarkers.Count * ZoomController.GetPixelPerSecond() + 4 * ZoomController.GetPixelPerSecond(), 0);
        newMarker.GetComponentInChildren<TMP_Text>().SetText(SecondsToTimeString(timeMarkers.Count + 4));
        timeMarkers.Add(newMarker);
    }

    // dandy function to quickly convert seconds to a format of [hh:m]m:ss
    string SecondsToTimeString(int seconds)
    {
        string result = "";
        if(seconds >= 3600)
        {
            result += ((int)(seconds / 3600)).ToString() + ":";
        }
        int minutes = seconds % 3600 / 60;
        if(seconds >= 3600 && minutes < 10)
        {
            result += "0";
        }
        result += minutes.ToString() + ":";
        int sec = seconds % 60;
        if(sec < 10)
        {
            result += "0";
        }
        result += sec.ToString();
        return result;
    }
}
