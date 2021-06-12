using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera primeCamera;
    public Camera frozenCamera;
    public Camera overgrowthCamera;

    public SuperPositionState superPositionState = SuperPositionState.TOGETHER;
    public static GameManager instance;
    GlitchEffects[] glitches;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        glitches = GetComponentsInChildren<GlitchEffects>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (superPositionState == SuperPositionState.TOGETHER) 
            {
                StartCoroutine("SplitDimensions");
            }
            else
            {
                StartCoroutine("JoinDimensions");
            }
        } 
    }

    IEnumerator SplitDimensions()
    {
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
        primeCamera.enabled = false;
        frozenCamera.enabled = true;
        overgrowthCamera.enabled = true;
        // Set new superposition state
        superPositionState = SuperPositionState.SPLIT;
    }

    IEnumerator JoinDimensions()
    {
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
        // Set new superposition state
        superPositionState = SuperPositionState.TOGETHER;
    }
}
