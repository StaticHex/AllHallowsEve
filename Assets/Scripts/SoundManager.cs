using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SoundManager : MonoBehaviour
{
    public List<AudioClip> Sounds;
    private List<AudioSource> _loops; 

	// Use this for initialization
	void Awake ()
	{
        _loops = new List<AudioSource>();
	}

    public AudioClip GetSound(string soundName)
    {
        foreach(var sound in Sounds)
        {
            if (sound.name == soundName) return sound;
        }

        return null;
    }

    public AudioSource GetLoopSource(string soundName)
    {
        return _loops.FirstOrDefault(source => source && source.clip.name == soundName);
    }

    public AudioSource LoopSound(string soundName)
    {
        return LoopSound(soundName, 1.0f);
    }

    public AudioSource LoopSound(string soundName, float volume)
    {
        var sound = GetSound(soundName);
        if (sound == null) return null;

        var loop = gameObject.AddComponent<AudioSource>();
        loop.volume = volume;
        loop.clip = sound;
        loop.loop = true;
        _loops.Add(loop);
        loop.Play();

        return loop;
    }

    public bool StopLoop(string soundName)
    {
        var loop = GetLoopSource(soundName);
        if (loop == null) return false;

        if (loop.isPlaying)
        {
            if(!_loops.Remove(loop)) return false;
            loop.Stop();
            Destroy(loop);
            return true;
        }

        return false;
    }

    public AudioClip PlaySound(string soundName, float volume = 1.0f)
    {
        var sound = GetSound(soundName);
        if (sound == null) return null;
        AudioSource.PlayClipAtPoint(sound, Vector3.zero, volume);
        return sound;
    }
}
