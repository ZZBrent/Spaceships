using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	
	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip endClip;
    [SerializeField]
    Slider musicSlider;
    public static float musicSliderValue;
    [SerializeField]
    Slider soundSlider;
    public static float soundSliderValue;
	
	private AudioSource music;
	
	void Awake ()
    {
        if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
        } else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>();
            
            musicSliderValue = musicSlider.value;
            soundSliderValue = soundSlider.value;

            music.volume = musicSlider.value;
            music.clip = startClip;
			music.loop = true;
			music.Play();
        }
	}

    public static void adjustMusicSlider(Slider music)
    {
        music.value = musicSliderValue;
    }

    public static void adjustSoundSlider(Slider sound)
    {
        sound.value = soundSliderValue;
    }

    public void changeVolume()
    {
        music.volume = musicSlider.value;
        musicSliderValue = musicSlider.value;
        soundSliderValue = soundSlider.value;
    }
	
	void OnLevelWasLoaded(int level){
		Debug.Log("MusicPlayer: loaded level "+level);
        if(music != null)
        {
            music.volume = musicSliderValue;

            music.Stop();

            if (level == 0)
            {
                music.clip = startClip;
            }
            if (level == 1)
            {
                music.clip = gameClip;
            }
            if (level == 2)
            {
                music.clip = endClip;
            }
            music.loop = true;
            music.Play();
        }		    
	}
}
