using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound 
    {
        public string clipName;
        public AudioClip clip;
        [HideInInspector]
        public AudioSource source;
    }
    public Sound[] sounds;
    private Dictionary<string,AudioSource> soundMap = new Dictionary<string,AudioSource>(); 

    void Start()
    {
        foreach(Sound noise in sounds)
        {
            
            Debug.Log("hey");
            
            noise.source = gameObject.AddComponent<AudioSource>();
            noise.source.clip = noise.clip;
            soundMap[noise.clipName] = noise.source;
        }
    }

    public void play(string clipName)
    {
        soundMap[clipName].Play();
    }


}
