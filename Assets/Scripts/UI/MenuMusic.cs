using UnityEngine;

public class MenuMusic : MonoBehaviour {

    AudioSource music;

    static MenuMusic instance;

    void Awake() {
        if (instance == null) {
            instance = this;
            GameObject.DontDestroyOnLoad(instance);
            music = GetComponent<AudioSource>();
        } else {
            GameObject.DestroyImmediate(gameObject);
        }
    }

    public static void StartMusic() {
        if (instance.music.isPlaying) { return; }
        instance.music.Play();
    }

    public static void StopMusic() {
        instance.music.Stop();
    }
}
