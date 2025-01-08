using UnityEngine;

public class MusicScript : MonoBehaviour
{

    private AudioSource musicSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicSound = GetComponent<AudioSource>();

        GameState.Subscribe(nameof(GameState.musicVolume), OnMusicVolumeChanged);
        GameState.Subscribe(nameof(GameState.isMuted), OnMuteAllChanged);
        OnMusicVolumeChanged();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMusicVolumeChanged()
    {
        musicSound.volume = GameState.musicVolume;
    }
    private void OnMuteAllChanged()
    {
        musicSound.volume = GameState.isMuted ? 0.0f : GameState.musicVolume;
    }
    private void OnDestroy()
    {
        GameState.UnSubscribe(nameof(GameState.musicVolume), OnMusicVolumeChanged);
        GameState.Subscribe(nameof(GameState.isMuted), OnMuteAllChanged);
    }
}
