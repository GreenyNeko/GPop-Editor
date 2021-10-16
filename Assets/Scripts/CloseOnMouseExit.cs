using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseOnMouseExit : MonoBehaviour, IPointerExitHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // hide the menu after the mouse leaves it
        gameObject.SetActive(false);
    }
}
