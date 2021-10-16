using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NoteScript : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    /**
     * type 0 - A
     * type 1 - S
     * type 2 - D
     * type 3 - F
     */
    int type = 0;
    bool dragging;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    /**
     * Set which type of note it is
     */
    public void SetType(int type)
    {
        this.type = type;
        UpdateSprite();
    }

    /**
     * Get the notes type
     */
    public int GetNoteType()
    {
        return type;
    }

    // Determines color given it's type
    void UpdateSprite()
    {
        switch(type)
        {
            case 3:
                GetComponentInChildren<Image>().color = Color.yellow;
                break;
            case 2:
                GetComponentInChildren<Image>().color = Color.blue;
                break;
            case 1:
                GetComponentInChildren<Image>().color = Color.green;
                break;
            default: // 0 = A
                GetComponentInChildren<Image>().color = Color.red;
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // only delete if not dragging because OnPointerClick comes before OnEndDrag
        if(!dragging)
        {
            // tell songscript to remove it from it's list and destroy it
            SongScript.Instance.RemoveNote(gameObject);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // snap to seconds
        float pixelPerSecond = ZoomController.GetPixelPerSecond();
        // consider offset from scrolling
        float offset = transform.parent.parent.parent.GetComponent<SongScroller>().GetScrollOffset();
        // calculate it's position in seconds
        float pos = Mathf.Round((eventData.position.y + offset) / pixelPerSecond) - 3;
        // move it
        transform.localPosition = new Vector3(transform.localPosition.x, pos * pixelPerSecond, transform.localPosition.z);
        // no longer dragging
        dragging = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // this is now a dragged object
        dragging = true;
        // snap to seconds
        float pixelPerSecond = ZoomController.GetPixelPerSecond();
        // consider offset from scrolling
        float offset = transform.parent.parent.parent.GetComponent<SongScroller>().GetScrollOffset();
        // calculate it's position in seconds
        float pos = Mathf.Round((eventData.position.y + offset) / pixelPerSecond) - 3;
        // move it
        transform.localPosition = new Vector3(transform.localPosition.x, pos * pixelPerSecond, transform.localPosition.z);
    }
}
