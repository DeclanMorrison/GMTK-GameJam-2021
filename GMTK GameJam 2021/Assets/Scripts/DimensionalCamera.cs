using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionalCamera : MonoBehaviour
{
    public DimensionTag dimension = DimensionTag.PRIME;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        // Cull the other two dimensions
        cam = GetComponent<Camera>();
        switch (dimension)
        {
            case DimensionTag.FROZEN:
                cam.cullingMask = ~((1 << LayerMask.NameToLayer("Prime")) ^ (1 << LayerMask.NameToLayer("Overgrowth")));
                break;
            case DimensionTag.OVERGROWTH:
                cam.cullingMask = ~((1 << LayerMask.NameToLayer("Prime")) ^ (1 << LayerMask.NameToLayer("Frozen")));
                break;
            case DimensionTag.PRIME:
                cam.cullingMask = ~((1 << LayerMask.NameToLayer("Frozen")) ^ (1 << LayerMask.NameToLayer("Overgrowth")));
                break;
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
