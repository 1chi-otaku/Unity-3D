using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class Door1Script : MonoBehaviour
    {

        private float inTimeTimeout = 2.0f;
        private float outTimeTimeout = 5.0f;
        private float timeout;
        [SerializeField] string keyNeeded = null;

        private AudioSource hitsound;
        private AudioSource opensound;


        void Start()
        {
            AudioSource[] audioSources = GetComponents<AudioSource>();
            hitsound = audioSources[0];
            opensound = audioSources[1];



        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name == "Player")
            {

                if (GameState.collectedKeys.Keys.Contains(keyNeeded))
                {

                    bool isInTime = GameState.collectedKeys[keyNeeded];
                    timeout = isInTime ? inTimeTimeout : outTimeTimeout;

                    GameState.TriggerEvent("Door", new TriggerPayload()
                    {
                        notification = "Door " + keyNeeded +  " has been unlocked" + (isInTime ? " in time" : " late"),
                        payload = "Opened"
                    });


   
                    opensound.Play();


                    Destroy(gameObject);
                    
                }

                else
                {
                    GameState.TriggerEvent("Door", new TriggerPayload()
                    {
                        notification = "In order to open the door, you need key " + keyNeeded,
                        payload = "Closed"
                    });
                    hitsound.Play();
                }

                   
            }

        }

    }
}