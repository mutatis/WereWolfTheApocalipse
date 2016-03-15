using UnityEngine;
using System.Collections;

public class MusicaManager : MonoBehaviour
{
    public AudioSource musica;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        musica.volume = PlayerPrefs.GetFloat("VolumeMusica");
    }
}
