using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class CharacterScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody playerRb;
    private AudioSource ambientSound;
    void Start()
    {
        player = GameObject.Find("CharacterPlayer");
        playerRb = player.GetComponent<Rigidbody>();
        ambientSound = GetComponent<AudioSource>();

        GameState.Subscribe(nameof(GameState.ambientVolume), OnAmbientVolumeChanged);
        GameState.Subscribe(nameof(GameState.isMuted), OnMuteAllChanged);
        OnAmbientVolumeChanged();


    }

    void Update()
    {
        this.transform.position = player.transform.position;
        player.transform.localPosition = Vector3.zero;

        //float v = Vector3.SignedAngle(playerRb.linearVelocity, Vector3.forward, Vector3.up);

        //this.transform.eulerAngles = new Vector3(0, -v, 0);

        //player.transform.localEulerAngles = new Vector3(
        //    player.transform.eulerAngles.x,
        //    player.transform.eulerAngles.y - v,
        //    player.transform.eulerAngles.z
        //    );

        this.transform.eulerAngles = new Vector3(0, player.transform.eulerAngles.y, 0);
        player.transform.localEulerAngles = new Vector3(player.transform.eulerAngles.x, 0, player.transform.eulerAngles.z);
    }

    private void OnAmbientVolumeChanged()
    {
        ambientSound.volume = GameState.ambientVolume;
    }
    private void OnMuteAllChanged()
    {
        ambientSound.volume = GameState.isMuted ? 0.0f : GameState.ambientVolume;
    }
    private void OnDestroy()
    {
        GameState.UnSubscribe(nameof(GameState.ambientVolume), OnAmbientVolumeChanged);
        GameState.Subscribe(nameof(GameState.isMuted), OnMuteAllChanged);
    }


}