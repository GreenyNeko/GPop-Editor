using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject MenuStrip;    // the menu strip that shows up
    // Start is called before the first frame update
    void Start()
    {
        // hide the menu strip  by default
        MenuStrip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * Called through the on click button event, opens the respective menustrip
     */
    public void OnMenuClick()
    {
        // show menu strip when menu is clicked
        MenuStrip.SetActive(true);
    }
}
