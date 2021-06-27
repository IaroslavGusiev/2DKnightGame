using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Audio 
{
    #region Private_Variables
    private AudioSource _sourceSFX; 
    private AudioSource _sourceMusic; 
    private AudioSource _sourceRandomPitchSFX; 
    private float _musicVolume = 1.0f; //
    private float _sFXVolume = 1.0f; //
    [SerializeField] private AudioClip[] _sounds;
    [SerializeField] private AudioClip _defaultClip;
    [SerializeField] private AudioClip _menuMusic;
    [SerializeField] private AudioClip _gameMusic;
    #endregion
    public AudioSource SourceSFX 
    {
        get 
        {
            return _sourceSFX;
        }
        set 
        {
            _sourceSFX = value;
        }
    }
    public AudioSource SourceMusic 
    {
        get 
        {
            return _sourceMusic;
        }
        set 
        {
            _sourceMusic = value;
        }
    }
    public AudioSource SourceRandomPitchSFX 
    {
        get 
        {
            return _sourceRandomPitchSFX;
        }
        set 
        {
            _sourceRandomPitchSFX = value;
        }
    }
    public float MusicVolume 
    {
        get 
        {
            return _musicVolume;
        }
        set 
        {
            _musicVolume = value;
            SourceMusic.volume = MusicVolume;
        }
    }
    public float SFXVolume 
    {
        get 
        {
            return _sFXVolume;
        }
        set 
        {
            _sFXVolume = value;
            SourceSFX.volume = SFXVolume;
            SourceRandomPitchSFX.volume = SFXVolume;
        }
    }

    private AudioClip GetSound(string clipName) 
    {
        for (int i = 0; i < _sounds.Length; i++)
        {
            if (_sounds[i].name == clipName)
            {
                return _sounds[i];
            }
        }
        Debug.LogError("Can't find audioclip");
        return _defaultClip;
    }

    public void PlaySound(string clipName) 
    {
        SourceSFX.PlayOneShot(GetSound(clipName), SFXVolume);
    }

    public void PlaySoundRandomPitch(string clipName) 
    {
        SourceRandomPitchSFX.pitch = Random.Range(0.7f, 1.3f);
        SourceRandomPitchSFX.PlayOneShot(GetSound(clipName), SFXVolume);
    }

    public void PlayMusic(bool menu) 
    {
        if (menu)
        {
            SourceMusic.clip = _menuMusic;
        }
        else 
        {
            SourceMusic.clip = _gameMusic;
        }
        SourceMusic.volume = MusicVolume;
        SourceMusic.loop = true;
        SourceMusic.Play();
    }

}
