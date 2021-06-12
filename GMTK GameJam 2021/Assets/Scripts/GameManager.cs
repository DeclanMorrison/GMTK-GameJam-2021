using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject frozenPlayer;
    public GameObject overgrowthPlayer;

    public float splitDistance = 1;

    public Camera primeCamera;
    public Camera frozenCamera;
    public Camera overgrowthCamera;

    public SuperPositionState superPositionState = SuperPositionState.TOGETHER;
    public static GameManager instance;
    GlitchEffects[] glitches;
    AudioSource glitchingSound;

    Rect CAMERA_POSITION_LEFT = new Rect(0, 0, 0.5f, 1);
    Rect CAMERA_POSITION_RIGHT = new Rect(0.5f, 0, 0.5f, 1); 
    void Awake() 
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        glitches = GetComponentsInChildren<GlitchEffects>();
        glitchingSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckSplit();
    }

    void CheckSplit() {
        Vector2 distance = frozenPlayer.transform.position - overgrowthPlayer.transform.position;
        if (superPositionState == SuperPositionState.TOGETHER && distance.magnitude > splitDistance) 
        {
            StartCoroutine("SplitDimensions");
        }
        else if (superPositionState == SuperPositionState.SPLIT && distance.magnitude < splitDistance)
        {
            StartCoroutine("JoinDimensions");
        }
    }

    IEnumerator SplitDimensions()
    {
        glitchingSound.Play();
        // Glitching Effect
        foreach (GlitchEffects glitch in glitches) 
        {
            glitch.enabled = true;
        }
    
        for (float ft = 0f; ft <= 5; ft += 0.1f)
        {
            foreach (GlitchEffects glitch in glitches) 
            {
                glitch.colorIntensity = ft;
                glitch.flipIntensity = ft;
            }
            yield return new WaitForSeconds(.00001f);
        }
        foreach (GlitchEffects glitch in glitches) 
        {
            glitch.colorIntensity = 0;
            glitch.flipIntensity = 0;
            glitch.enabled = false;
        }
        // Check camera side
        if (frozenPlayer.transform.position.x < overgrowthCamera.transform.position.x)
        {
            frozenCamera.rect = CAMERA_POSITION_LEFT;
            overgrowthCamera.rect = CAMERA_POSITION_RIGHT;
        }
        else {
            frozenCamera.rect = CAMERA_POSITION_RIGHT;
            overgrowthCamera.rect = CAMERA_POSITION_LEFT;
        }
        // Switch Cameras
        primeCamera.enabled = false;
        frozenCamera.enabled = true;
        overgrowthCamera.enabled = true;
        // Set player dimension layers
        frozenPlayer.layer = LayerMask.NameToLayer("Frozen");
        overgrowthPlayer.layer = LayerMask.NameToLayer("Overgrowth");
        // Set new superposition state
        superPositionState = SuperPositionState.SPLIT;
    }

    IEnumerator JoinDimensions()
    {
        glitchingSound.Play();
        // Glitching Effect
        foreach (GlitchEffects glitch in glitches) 
        {
            glitch.enabled = true;
        }
    
        for (float ft = 0f; ft <= 5; ft += 0.1f)
        {
            foreach (GlitchEffects glitch in glitches) 
            {
                glitch.colorIntensity = ft;
                glitch.flipIntensity = ft;
            }
            yield return new WaitForSeconds(.00001f);
        }
        foreach (GlitchEffects glitch in glitches) 
        {
            glitch.colorIntensity = 0;
            glitch.flipIntensity = 0;
            glitch.enabled = false;
        }

        // Switch Cameras
        primeCamera.enabled = true;
        frozenCamera.enabled = false;
        overgrowthCamera.enabled = false;
        // Set player dimension layers
        frozenPlayer.layer = LayerMask.NameToLayer("Prime");
        overgrowthPlayer.layer = LayerMask.NameToLayer("Prime");
        // Set new superposition state
        superPositionState = SuperPositionState.TOGETHER;
    }
}
