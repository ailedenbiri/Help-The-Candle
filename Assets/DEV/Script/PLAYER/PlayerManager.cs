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
            // Bu nesne daha önce oluşturulmadıysa, bu nesneyi ayarla ve yok etme
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Bu nesne daha önce oluşturulduysa, yeni oluşturulan nesneyi yok et
            Destroy(gameObject);
        }
    }
}
