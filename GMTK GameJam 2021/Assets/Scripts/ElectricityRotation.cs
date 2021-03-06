using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityRotation : MonoBehaviour
{
    public float particleDensity = 15;
    public ParticleSystem electricityParticles;
    public ParticleSystem otherElectricityParticles;
    public GameObject otherPlayer;
    public SuperPositionState superPositionState = SuperPositionState.TOGETHER;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        electricityParticles.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, GetAngleToOtherPlayer() - 180));
        otherElectricityParticles.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, GetAngleToOtherPlayer() - 180));
        
        Vector2 distance = electricityParticles.transform.position - otherPlayer.transform.position;

        var particleSystem = electricityParticles.main;
        var otherParticleSystem = otherElectricityParticles.main;
        particleSystem.maxParticles = (int)MapDistanceToParticleCount(distance.magnitude, 15, 10, 0, particleDensity);
        otherParticleSystem.maxParticles = (int)MapDistanceToParticleCount(distance.magnitude, 15, 10, 0, particleDensity);
    }

    float MapDistanceToParticleCount(float value, float from1, float to1, float from2, float to2)
    {
        float particleCount = (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        if (particleCount <= 0) {
            return 1;
        } else if (particleCount >= 10) {
            return 10;
        } else {
            return particleCount;
        }
    }

    float GetAngleToOtherPlayer()
    {
        Vector3 dir = electricityParticles.transform.position - otherPlayer.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return angle;
    }

}
