using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileSystem : MonoBehaviour
{
    public string levelFolder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * Creates a .gpop file at the default location with the name given by the text field
     */
    public void SaveLevel(TMPro.TMP_Text textField)
    {

        using (StreamWriter streamWriter = File.CreateText(levelFolder + textField.text + ".gpop"))
        {
            // get the string representation and store it in the file
            streamWriter.Write(SongScript.Instance.GetLevelAsString());
        }
    }

    /**
     * Loads a .gpop file at the default location given the name in the text field
     */
    public void LoadLevel(TMPro.TMP_Text textField)
    {
        // TO-DO: load from file
    }
}
