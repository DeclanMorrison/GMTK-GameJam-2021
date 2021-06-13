using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Doors : MonoBehaviour
{
    [HideInInspector] public bool open;
    private Animator anim;
    public Collider2D doorColider;
    public float requiredInputs = 1;
    private List<GameObject> activeInputs = new List<GameObject>();

    private void Start()
    {
        anim = GetComponent<Animator>();
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

    public void InputActivity(bool state, GameObject gameObject)
    {
        if(state == true) // the input just turned on
        {
            if (activeInputs.Contains(gameObject)) { return; }
            activeInputs.Add(gameObject);
        }
        else //the input just shut off
        {
            if (!activeInputs.Contains(gameObject)){return;}
            activeInputs.Remove(gameObject);
        }
        SetState(activeInputs.Count >= requiredInputs);
    }

}
