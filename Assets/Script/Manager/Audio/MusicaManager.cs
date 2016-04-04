using UnityEngine;
using System.Collections;

public class MusicaManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string musica1;

    [FMODUnity.EventRef]
    public string musica2;

    FMOD.Studio.EventInstance vol;
    FMOD.Studio.EventInstance vol2;

    public string nomeCena;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {        
        vol = FMODUnity.RuntimeManager.CreateInstance(musica1);
        vol.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        vol.start();

        vol2 = FMODUnity.RuntimeManager.CreateInstance(musica2);
        vol2.setVolume(PlayerPrefs.GetFloat("VolumeFX"));
        vol2.start();
    }

    void Update()
    {
        if (Application.loadedLevelName != nomeCena)
        {
            vol.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            vol2.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            Destroy(gameObject);
            
        }
        vol.setVolume(PlayerPrefs.GetFloat("VolumeMusica"));
        vol2.setVolume(PlayerPrefs.GetFloat("VolumeMusica"));
    }
}
