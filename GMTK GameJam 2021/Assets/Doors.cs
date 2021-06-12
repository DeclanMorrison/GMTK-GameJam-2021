using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Doors : MonoBehaviour
{
    public bool open;
    public List<WorldButton> requiredButtons;
    public Collider2D doorColider;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SetState(!open);
        }

        if(requiredButtons.Count != 0) { open = checkRequiredButtons(); }
        
    }

    void SetState(bool open)
    {
        if (open == this.open) { return; }

        this.open = open;
        if (open)
        {
            doorColider.enabled = false;
        }
        else
        {
            doorColider.enabled = true;
        }
    }
    bool checkRequiredButtons()
    {
        foreach (WorldButton button in requiredButtons)
        {
            if (!button.pressed)
            {
                return false;
            }
        }
        return true;
    }

}
