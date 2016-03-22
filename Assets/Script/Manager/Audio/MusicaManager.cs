using UnityEngine;
using System.Collections;

public class MusicaManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string musica;

    FMOD.Studio.EventInstance vol;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        vol = FMODUnity.RuntimeManager.CreateInstance(musica);
        vol.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        vol.start();
    }

    void Update()
    {
        vol.setVolume(PlayerPrefs.GetFloat("VolumeMusica"));
    }
}
