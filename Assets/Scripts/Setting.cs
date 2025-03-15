using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour{
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle soundEffectsToggle;
    [SerializeField] private AudioSource[] soundEffectAudioSources;

    void Start(){
        if (musicToggle != null){
            bool musicOn = PlayerPrefs.GetInt("Music", 1) == 1; 
            musicToggle.isOn = musicOn;
            Music.instance.GetComponent<AudioSource>().mute = !musicOn; 
            musicToggle.onValueChanged.AddListener(ToggleMusic);
        }
        if (soundEffectsToggle != null){
            bool soundEffectsOn = PlayerPrefs.GetInt("SoundEffects", 1) == 1;
            soundEffectsToggle.isOn = soundEffectsOn;
            soundEffectsToggle.onValueChanged.AddListener(ToggleSoundEffects);
            foreach (AudioSource audioSource in soundEffectAudioSources){
                if (audioSource != null){
                    audioSource.mute = !soundEffectsOn;
                }
            }
        }
    }

    public void ToggleMusic(bool isOn){
        PlayerPrefs.SetInt("Music", isOn ? 1 : 0); 
        PlayerPrefs.Save();
        Music.instance.GetComponent<AudioSource>().mute = !isOn; 
    }

    public void ToggleSoundEffects(bool isOn){
        PlayerPrefs.SetInt("SoundEffects", isOn ? 1 : 0);
        PlayerPrefs.Save();
        foreach (AudioSource audioSource in soundEffectAudioSources){
            if (audioSource != null){
                audioSource.mute = !isOn;
            }
        }
    }
}
