using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public float speed = 800f;
    public float leftRightSpeed = 800f;

    private void Awake()
    {
        if (Instance == null)
        {
            // Bu nesne daha �nce olu�turulmad�ysa, bu nesneyi ayarla ve yok etme
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Bu nesne daha �nce olu�turulduysa, yeni olu�turulan nesneyi yok et
            Destroy(gameObject);
        }
    }
}
