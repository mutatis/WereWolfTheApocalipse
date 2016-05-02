using UnityEngine;
using System.Collections;

public class AudioEntrada : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string som;

    FMOD.Studio.EventInstance vol;

    public string nomeCena;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        vol = FMODUnity.RuntimeManager.CreateInstance(som);
        vol.setVolume(PlayerPrefs.GetFloat("VolumeMusica"));
        vol.start();
    }

    void Update()
    {
        if (Application.loadedLevelName != nomeCena)
        {
            vol.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            vol.release();
            //Destroy(gameObject);
        }
        else
        {
            vol.setVolume(PlayerPrefs.GetFloat("VolumeMusica"));
        }
    }
}
