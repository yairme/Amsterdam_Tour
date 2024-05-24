using System.Collections;
using UnityEngine;

public class OnClickEffect : MonoBehaviour
{
    [SerializeField] private GameObject particlesPrefab;
    private Vector2 mousePos;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 spawnPosition = new Vector3(mousePos.x, mousePos.y, 0f);

            GameObject particlesInstance = Instantiate(particlesPrefab, spawnPosition, Quaternion.identity);

            StartCoroutine(DestroyParticlesAfterDelay(particlesInstance));
        }
    }

    IEnumerator DestroyParticlesAfterDelay(GameObject particlesInstance)
    {
        ParticleSystem particleSystem = particlesInstance.GetComponent<ParticleSystem>();
        float duration = particleSystem.main.duration + particleSystem.main.startLifetime.constantMax;
        yield return new WaitForSeconds(duration);
        Destroy(particlesInstance);
    }
}