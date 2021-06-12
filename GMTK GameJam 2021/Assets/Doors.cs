using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Doors : MonoBehaviour
{
    public bool open;
    private Animator anim;
    public Collider2D doorColider;
    public List<WorldButton> requiredButtons;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if(requiredButtons.Count != 0) { SetState(checkRequiredButtons()); }
    }

    void SetState(bool open)
    {
        if (open == this.open) { return; }

        this.open = open;
        anim.SetBool("Open", open);
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
