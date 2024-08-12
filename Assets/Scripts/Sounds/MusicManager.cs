using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioClip BuildingTheme;
    [SerializeField] AudioClip DestroyingTheme;
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
    public void StartDestroyingPhaseMusic()
    {
        MusicSource.Stop();
        MusicSource.clip = DestroyingTheme;
        MusicSource.Play();
        MusicSource.volume = 0.2f;
        MusicSource.loop = true;
    }
    public void StopMusic()
    {
        MusicSource.Stop();
    }
}
