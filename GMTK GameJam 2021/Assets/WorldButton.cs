using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldButton : MonoBehaviour
{
    UnityEvent<bool> OnStateChange = new UnityEvent<bool>();

    public LayerMask interactableLayers;
    private ContactFilter2D interactableFilter;
    public Collider2D detectionField;
    public bool pressed = false;
    private bool lastFramePressed = false;

    private void Start()
    {
        interactableFilter.SetLayerMask(interactableLayers);
    }
    private void Update()
    {
        pressed = Physics2D.OverlapCollider(detectionField, interactableFilter, new Collider2D[1]) > 0;
    }


}