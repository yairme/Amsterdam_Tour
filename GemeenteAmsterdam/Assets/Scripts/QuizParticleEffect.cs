using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class QuizParticleEffect : MonoBehaviour
{
    public ParticleSystem particleEffect;
    public List<GameObject> uiElements;

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