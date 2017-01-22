using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MusicType
{
	Full,
	Loop,
	GameOver,
	FailSound,
	CollectedSound
}

public class AudioController : Singleton<AudioController>
{
	protected AudioController ()
	{
		
	}

	[SerializeField] public AudioSource MusicAudioSource;
	[SerializeField] public AudioSource SoundAudioSource;
	[SerializeField] public AudioClip ThemeMusicMain;
	[SerializeField] public AudioClip ThemeMusicLoop;
	[SerializeField] public AudioClip ThemeMusicGameOver;
	[SerializeField] public AudioClip FailSound;
	[SerializeField] public AudioClip CollectedSound;

	//    public void PlaySound(AudioClip clip, bool loop=false){
	//        if( loop){
	//            AudioSource.PlayOneShot(clip);
	//        }else{
	//            AudioSource.pla
	//        }
	//    }

	public void PlayMusic (MusicType type, bool stop = false)
	{
		MusicAudioSource.volume = 1;
		switch (type) {
		case MusicType.Full:
			MusicAudioSource.clip = ThemeMusicMain;
			MusicAudioSource.loop = true;
			break;
		case MusicType.Loop:
			MusicAudioSource.clip = ThemeMusicLoop;
			MusicAudioSource.loop = true;
			break;
		case MusicType.GameOver:
			MusicAudioSource.clip = ThemeMusicGameOver;
			MusicAudioSource.loop = false;
			MusicAudioSource.volume = 0.5f;
			break;
		case MusicType.FailSound:
//                SoundAudioSource.clip = FailSound;
//                SoundAudioSource.loop = false;
			SoundAudioSource.PlayOneShot (FailSound);
			return;
		case MusicType.CollectedSound:
//                SoundAudioSource.clip = CollectedSound;
//                SoundAudioSource.loop = false;
			SoundAudioSource.PlayOneShot (CollectedSound);
			return;
		}
		if (stop) {
			MusicAudioSource.Stop ();
		} else {
			MusicAudioSource.Play ();
		}
	}

	public void PlayGameOver ()
	{
		MusicAudioSource.PlayOneShot (ThemeMusicGameOver);
	}
}
