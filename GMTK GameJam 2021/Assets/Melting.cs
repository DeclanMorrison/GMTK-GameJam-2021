using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melting : MonoBehaviour
{
    public Sprite meltedSprite;
    public bool isMelted = false;
    private Collider2D frozenCollider;
    // Start is called before the first frame update
    void Start()
    {
        frozenCollider = GetComponent<Collider2D>();
    }

    public void OnHitByLaser()
    {
        if (isMelted == false)
        {
            isMelted = false;
            GetComponent<SpriteRenderer>().sprite = meltedSprite;
            frozenCollider.enabled = false;
        }
    }
}
