using UnityEngine;
using System.Collections;

public class MusicaManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string musica;

    FMOD.Studio.EventInstance heal;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        heal = FMODUnity.RuntimeManager.CreateInstance(musica);
        heal.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        heal.start();
    }

    void Update()
    {
        heal.setVolume(PlayerPrefs.GetFloat("VolumeMusica"));
    }
}
