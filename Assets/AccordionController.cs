using System.Collections.Generic;
using UnityEngine;

public class AccordionController : MonoBehaviour
{

    public void ToggleTab(GameObject clicked, bool shown) // getting the gameobject's scripts and boolean
    {
        AccordionTab tab = clicked.GetComponent<AccordionTab>(); //getting the script from the specific tab

            if (shown == false)
            {
                Debug.Log(clicked.name); 
                tab.shown = true; // if the tab is not shown, let it dropdown
                tab.Show();
            }
               
            else
            {
            Debug.Log(clicked.name);
            tab.shown = false; // if the tab is already dropped down, hide it
                tab.Hide();
            }
               

    }
}
