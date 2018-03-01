using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settings : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GameObject musicObject = GameObject.FindWithTag("music");
        Slider musicSlider = musicObject.GetComponent<Slider>();
        GameObject soundObject = GameObject.FindWithTag("sound");
        Slider soundSlider = soundObject.GetComponent<Slider>();
        this.GetComponent<Button>().onClick.AddListener(delegate { MusicPlayer.adjustMusicSlider(musicSlider); });
        this.GetComponent<Button>().onClick.AddListener(delegate { MusicPlayer.adjustSoundSlider(soundSlider); });

        //hide panel
        GameObject panel = GameObject.FindWithTag("panel");
        panel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
