using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongScript : MonoBehaviour
{
    public GameObject notePrefab;                   // needed to create new notes
    public GameObject CurrMarker;                   // allows us to determine where to place the notes
    public GameObject laneA, laneS, laneD, laneF;   // the parents of the notes

    /**
     * Allows us to access SongScript from anywhere
     * currently used to remove notes
     */
    public static SongScript Instance
    {
        get;
        private set;
    }

    /**
     * Currently unused, planned for saving the file
     */
    public struct Note
    {
        int type;
        float position;
        float length;
    };

    List<GameObject> noteObjects;

    // Start is called before the first frame update
    void Start()
    {
        // pass as instance
        Instance = this;
        // init
        noteObjects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // create notes on key press
        // discard if there is already one at the same position with the same type
        if(Input.GetKeyDown(KeyCode.A))
        {
            GameObject newNote = Instantiate(notePrefab, laneA.transform);
            newNote.transform.position = new Vector3(laneA.transform.position.x + 24, CurrMarker.transform.position.y, laneA.transform.position.z);
            newNote.GetComponent<NoteScript>().SetType(0);
            if(!noteObjects.Exists((note) => {
                return note.transform.position.y == newNote.transform.position.y
                 && note.GetComponent<NoteScript>().GetNoteType() == newNote.GetComponent<NoteScript>().GetNoteType(); 
            }))
            {
                noteObjects.Add(newNote);
            }
            else
            {
                Destroy(newNote);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            GameObject newNote = Instantiate(notePrefab, laneS.transform);
            newNote.transform.position = new Vector3(laneS.transform.position.x + 24, CurrMarker.transform.position.y, laneS.transform.position.z);
            newNote.GetComponent<NoteScript>().SetType(1);
            if (!noteObjects.Exists((note) => {
                return note.transform.position.y == newNote.transform.position.y
                 && note.GetComponent<NoteScript>().GetNoteType() == newNote.GetComponent<NoteScript>().GetNoteType();
            }))
            {
                noteObjects.Add(newNote);
            }
            else
            {
                Destroy(newNote);
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GameObject newNote = Instantiate(notePrefab, laneD.transform);
            newNote.transform.position = new Vector3(laneD.transform.position.x + 24, CurrMarker.transform.position.y, laneD.transform.position.z);
            newNote.GetComponent<NoteScript>().SetType(2);
            if (!noteObjects.Exists((note) => {
                return note.transform.position.y == newNote.transform.position.y
                 && note.GetComponent<NoteScript>().GetNoteType() == newNote.GetComponent<NoteScript>().GetNoteType();
            }))
            {
                noteObjects.Add(newNote);
            }
            else
            {
                Destroy(newNote);
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject newNote = Instantiate(notePrefab, laneF.transform);
            newNote.transform.position = new Vector3(laneF.transform.position.x + 24, CurrMarker.transform.position.y, laneF.transform.position.z);
            newNote.GetComponent<NoteScript>().SetType(3);
            if (!noteObjects.Exists((note) => {
                return Mathf.Approximately(note.transform.position.y, newNote.transform.position.y)
                 && note.GetComponent<NoteScript>().GetNoteType() == newNote.GetComponent<NoteScript>().GetNoteType();
            }))
            {
                noteObjects.Add(newNote);
            }
            else
            {
                Destroy(newNote);
            }
        }
    }

    /**
     * Remove the given note from the list and destroy it
     */
    public void RemoveNote(GameObject note)
    {
        noteObjects.Remove(note);
        Destroy(note);
    }

    /** 
     * Returns the string representation of this level
     * String format is:
     * [{"type",game mode,"dict":{aNote,id,longANote,id,sNote,id,longSNote,id,dNote,id,longDNote,id,fNote,id,longFNote,id}},[note...]]
     */
    public string GetLevelAsString()
    {
        // default part of any classic mode level
        string levelStr = "[{\"type\":\"s2\",\"dict\":{\"a\":0,\"a1\":1,\"s\":2,\"s1\":3,\"d\":4,\"d1\":5,\"f\":6,\"f1\":7}}";
        // convert each object
        foreach(GameObject note in noteObjects)
        {
            // preceding comma, so we don't have to think about what is the last note to convert
            levelStr += ",";
            float noteScale = note.transform.localScale.y;
            float timePos = note.transform.localPosition.y / ZoomController.GetPixelPerSecond();
            // normalNote
            if(noteScale == 1)
            {
                // convert our internal type 0,1,2,3 to the one for the file for normal notes: 0,2,4,6
                int noteType = note.GetComponent<NoteScript>().GetNoteType() * 2;

                // format for normal notes:
                // type,position
                levelStr += noteType.ToString() + "," + timePos.ToString();
            }
            else if(noteScale == 0.5) // half note
            {
                // convert our internal type 0,1,2,3 to the one for the file for long notes: 1,3,5,7
                int noteType = note.GetComponent<NoteScript>().GetNoteType() * 2 + 1;

                // format for half notes:
                // type,position,negativeLength
                levelStr += noteType.ToString() + "," + timePos.ToString() + ",-1";
            }
            else // long note
            {
                // need to correct timePos, since our pivot is in the middle
                float noteSizeInPixel = 48 * noteScale;
                timePos -= noteSizeInPixel / 2;
                // convert the length of our note to seconds
                float timeLength = noteSizeInPixel / ZoomController.GetPixelPerSecond();
                // convert our internal type 0,1,2,3 to the one for the file for long notes: 1,3,5,7
                int noteType = note.GetComponent<NoteScript>().GetNoteType() * 2 + 1;

                // format for long notes:
                // type,position,length
                levelStr += noteType.ToString() + "," + timePos.ToString() + "," + timeLength.ToString();
            }
        }
        // close off the list
        levelStr += "]";
        return levelStr;
    }
}
