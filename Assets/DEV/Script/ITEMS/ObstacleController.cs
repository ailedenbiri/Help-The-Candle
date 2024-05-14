using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class ObstacleController : MonoBehaviour
{
    [Header("Particle")]
    public ParticleSystem explosionParticle;

    public Transform player;

    private GameManager gameManager;
    private bool explosionStarted = false; // Patlama ba�lad� m�?

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        if (explosionParticle != null)
        {
            explosionParticle.gameObject.SetActive(false);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (explosionParticle != null && !explosionStarted)
            {
                explosionStarted = true; // Patlama ba�lad� olarak i�aretle

                // Patlama par�ac���n� etkinle�tir ve oynat
                explosionParticle.gameObject.SetActive(true);
                explosionParticle.Play();

                // Patlama par�ac��� tamamlanana kadar bekle
                StartCoroutine(WaitForExplosionToEnd());
            }
        }
    }

    private IEnumerator WaitForExplosionToEnd()
    {
        // Patlama par�ac���n�n s�resi kadar bekle
        yield return new WaitForSecondsRealtime(explosionParticle.main.duration);

        // Patlama par�ac��� tamamland���nda oyunu yeniden ba�lat
        StartCoroutine(gameManager.Restart());
    }

}
