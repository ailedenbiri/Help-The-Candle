using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] FloatingJoystick joystick;
    [SerializeField] private float speed;
    [SerializeField] private float leftRightSpeed;
    [SerializeField] private Vector2 minMaxX;

    private float initialSpeed = 800f;
    private float initialLeftRightSpeed = 800f;
    private float leftRightSpeedIncreaseAmount = 5f;
    private float speedIncreaseInterval = 1f;
    private float speedIncreaseAmount = 5f;


    [Header("Particle")]
    public ParticleSystem particleSystem;


    Rigidbody rb;
    private GameManager gameManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        initialLeftRightSpeed = 800f; // Baþlangýç sola-saða hýzý
        initialSpeed = 800f; // Baþlangýç hýzý
        speed = gameManager.PlayerInitialSpeed; // GameManager'dan alýnan baþlangýç hýzý
        leftRightSpeed = initialLeftRightSpeed;
    }

    void Start()
    {
        transform.DOScaleY(0, 8);

        // Hýz ayarlarýný yazdýr
        Debug.Log("Speed: " + speed + ", LeftRightSpeed: " + leftRightSpeed);

        InvokeRepeating(nameof(IncreaseSpeed), speedIncreaseInterval, speedIncreaseInterval);

        // Önceki seviyeden oyuncu tercihlerini yükle
        float previousSpeed, previousLeftRightSpeed;
        gameManager.LoadPreviousPlayerPrefs(out previousSpeed, out previousLeftRightSpeed);

        // Eðer önceki seviyede oyuncu tercihleri varsa, onlarý kullan
        if (previousSpeed != 0f && previousLeftRightSpeed != 0f)
        {
            speed = previousSpeed;
            leftRightSpeed = previousLeftRightSpeed;
        }
        else
        {
            // 0. sahne için oyuncu tercihleri yoksa, varsayýlan deðerleri kullan
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                // Diðer sahneler
                // Önceki seviyeden kaydedilen hýzý kullan, eðer kayýt yoksa varsayýlan hýzý kullan
                speed = PlayerPrefs.GetFloat("LastLevelSpeed", initialSpeed);
                leftRightSpeed = PlayerPrefs.GetFloat("LastLevelLeftRightSpeed", initialLeftRightSpeed);
            }
        }

        if (particleSystem != null)
        {
            particleSystem.Stop();
        }
    }

    void IncreaseSpeed()
    {
        speed += speedIncreaseAmount;
        leftRightSpeed += leftRightSpeedIncreaseAmount;
    }

    void OnDisable()
    {
        // Oyuncunun ulaþtýðý son hýzý kaydet
        PlayerPrefs.SetFloat("LastLevelSpeed", speed);
        PlayerPrefs.SetFloat("LastLevelLeftRightSpeed", leftRightSpeed);
        PlayerPrefs.Save();
    }

    void PlayerDied()
    {
        // Oyuncu öldüðünde çaðrýlýr, hýzý 800 olarak ayarlar
        speed = initialSpeed;
        leftRightSpeed = initialLeftRightSpeed;
    }


    private void FixedUpdate()
    {
        rb.velocity = new Vector3(joystick.Horizontal * leftRightSpeed * Time.fixedDeltaTime, rb.velocity.y, speed * Time.fixedDeltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minMaxX.x, minMaxX.y), 0, transform.position.z);
        if (transform.localScale.y <= 0.25f)
        {

            Time.timeScale = 0;
            DOTween.Kill(transform);
            StartCoroutine(gameManager.Restart());

        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            StartCoroutine(gameManager.Restart());

        }
        if (other.CompareTag("Finish"))
        {
            gameManager.SavePreviousPlayerPrefs(speed, leftRightSpeed);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            // Gelecek seviyeye geçmeden önce oyuncu tercihlerini kaydet
        }


        if (other.CompareTag("Respawn"))
        {
            if (particleSystem != null && !particleSystem.isPlaying)
            {
                particleSystem.Play();
            }
        }
    }
}
