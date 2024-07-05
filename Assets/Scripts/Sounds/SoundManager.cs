using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource SoundSoure;
    [SerializeField] AudioSource WeaponSoundSoure;
    [SerializeField] AudioSource UnitHittedSoundSoure;
    [SerializeField] AudioSource UISoundSource;
    [SerializeField] AudioClip BlockCrushing;
    [SerializeField] AudioClip BlockBurning;
    [SerializeField] AudioClip Lightning;
    [SerializeField] AudioClip DynamiteExplosion;
    [SerializeField] AudioClip MeteorHittedGround;
    [SerializeField] AudioClip UnitHitted;
    [SerializeField] AudioClip WinMap;
    public static SoundManager instance;
    void Start()
    {
        instance = this;
    }

    public void PlayBlockCrushedSound()
    {
        SoundSoure.clip = BlockCrushing;
        SoundSoure.Play();
    }
    public void PlayBlockBurningSound()
    {
        SoundSoure.clip = BlockBurning;
        SoundSoure.Play();
    }
    public void PlayLightningSound()
    {
        WeaponSoundSoure.clip = Lightning;
        WeaponSoundSoure.Play();
    }
    public void PlayDynamiteSound()
    {
        WeaponSoundSoure.clip = DynamiteExplosion;
        WeaponSoundSoure.Play();
    }
    public void PlayMeteorCrushedSound()
    {
        WeaponSoundSoure.clip = MeteorHittedGround;
        WeaponSoundSoure.Play();
    }
    public void PlayUnitedHittedSound()
    {
        UnitHittedSoundSoure.pitch = Random.Range(0.8f, 1.3f);
        UnitHittedSoundSoure.PlayOneShot(UnitHitted);
    }
    public void OnWinMapSound()
    {
        UISoundSource.clip = WinMap;
        UISoundSource.Play();
    }
}
