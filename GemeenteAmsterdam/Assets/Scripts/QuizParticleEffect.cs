using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class QuizParticleEffect : MonoBehaviour
{
    public ParticleSystem particleEffect; // Reference to the Particle System
    public List<GameObject> uiElements; // List of UI elements to check

    void Update()
    {
        foreach (GameObject uiElement in uiElements)
        {
            if (uiElement.activeInHierarchy)
            {
                if (!particleEffect.isPlaying)
                {
                    particleEffect.Play();
                }
                return;
            }
        }

        if (particleEffect.isPlaying)
        {
            particleEffect.Stop();
        }
    }
}