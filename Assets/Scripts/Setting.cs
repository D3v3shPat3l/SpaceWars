using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle soundEffectsToggle;

    void Start(){
        if (musicToggle != null){
            musicToggle.isOn = !Music.instance.GetComponent<AudioSource>().mute;
            musicToggle.onValueChanged.AddListener(ToggleMusic);
        }
    }

    public void ToggleMusic(bool isOn){
        Music.instance.ToggleMusic(isOn);
    }
}
