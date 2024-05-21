using System.Collections;
using UnityEngine;

public class OnClickEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    private Vector2 mousePos;
    private Coroutine deactivateCoroutine;

    void Start()
    {
        particles.Stop();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            particles.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
            particles.Play();

            // Stop any existing coroutine that may deactivate the particles prematurely
            if (deactivateCoroutine != null)
            {
                StopCoroutine(deactivateCoroutine);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            deactivateCoroutine = StartCoroutine(DeactivateParticlesWithDelay(2.0f));
        }
    }

    IEnumerator DeactivateParticlesWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        particles.Stop();
    }
}