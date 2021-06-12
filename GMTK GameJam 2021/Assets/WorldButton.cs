using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldButton : MonoBehaviour
{
    public UnityEvent<bool> OnStateChange = new UnityEvent<bool>();
 
    public LayerMask interactableLayers;
    private ContactFilter2D interactableFilter;
    public Collider2D detectionField;
    public bool pressed = false;
    private bool lastFramePressed = false;

    public Sprite unPressedSprite;
    public Sprite pressedSprite;
    private SpriteRenderer sr;

    private void Start()
    {
        interactableFilter.SetLayerMask(interactableLayers);
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = unPressedSprite;
    }
    private void Update()
    {
        pressed = (Physics2D.OverlapCollider(detectionField, interactableFilter, new Collider2D[1]) > 0);

        if(pressed != lastFramePressed)
        {
            OnStateChange.Invoke(pressed);
            Debug.Log(pressed);
            if (pressed)
            {
                sr.sprite = pressedSprite;
            }
            else
            {
                sr.sprite = unPressedSprite;
            }
        }

        lastFramePressed = pressed;
    }


}
