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
    private bool explosionStarted = false; // Patlama baþladý mý?

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
                explosionStarted = true; // Patlama baþladý olarak iþaretle

                // Patlama parçacýðýný etkinleþtir ve oynat
                explosionParticle.gameObject.SetActive(true);
                explosionParticle.Play();

                // Patlama parçacýðý tamamlanana kadar bekle
                StartCoroutine(WaitForExplosionToEnd());
            }
        }
    }

    private IEnumerator WaitForExplosionToEnd()
    {
        // Patlama parçacýðýnýn süresi kadar bekle
        yield return new WaitForSecondsRealtime(explosionParticle.main.duration);

        // Patlama parçacýðý tamamlandýðýnda oyunu yeniden baþlat
        StartCoroutine(gameManager.Restart());
    }

}
