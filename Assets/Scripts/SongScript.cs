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
}
