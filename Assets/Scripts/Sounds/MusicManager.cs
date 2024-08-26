using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioClip BuildingTheme;
    [SerializeField] AudioClip DestroyingTheme;
    [SerializeField] AudioClip ParkourTheme;
    void Awake()
    {
        instance = this;
    }

    public void StartBuildingPhaseMusic()
    {
        if(SerializeBlockManager.instance.OnlyParkourMap == false)
        {
            MusicSource.Stop();
            MusicSource.clip = BuildingTheme;
            MusicSource.Play();
            MusicSource.volume = 0.8f;
            MusicSource.loop = true;
        }
    }
    public void StartParkourMusic()
    {
        MusicSource.Stop();
        MusicSource.clip = ParkourTheme;
        MusicSource.Play();
        MusicSource.volume = 0.8f;
        MusicSource.loop = true;
        MusicSource.pitch = 3;
    }
    public void StartDestroyingPhaseMusic()
    {
        MusicSource.Stop();
        MusicSource.clip = DestroyingTheme;
        MusicSource.Play();
        MusicSource.volume = 0.4f;
        MusicSource.loop = true;
    }
    public void StopMusic()
    {
        MusicSource.Stop();
    }
}
