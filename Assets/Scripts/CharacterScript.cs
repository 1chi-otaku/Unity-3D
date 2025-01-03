using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody playerRb;
    void Start()
    {
        player = GameObject.Find("CharacterPlayer");
        playerRb = player.GetComponent<Rigidbody>();
        
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



}