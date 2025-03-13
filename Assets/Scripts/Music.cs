using UnityEngine;

public class Music : MonoBehaviour{
    public static Music instance;
    private AudioSource audioSource;

    private void Awake(){
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        } else{
            Destroy(gameObject);
        }
    }

        public void ToggleMusic(bool isOn){
        if (audioSource != null){
            audioSource.mute = !isOn; 
        }
    }
}
