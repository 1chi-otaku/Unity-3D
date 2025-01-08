using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class Door1Script : MonoBehaviour
    {

        private float inTimeTimeout = 2.0f;
        private float outTimeTimeout = 5.0f;
        private float timeout;

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

                if (GameState.collectedKeys.Keys.Contains("1"))
                {

                    bool isInTime = GameState.collectedKeys["1"];
                    timeout = isInTime ? inTimeTimeout : outTimeTimeout;
                    

                    ToastScript.ShowToast("Ключ \"1\" застосовано" +
                    (isInTime ? " вчасно" : " нe вчасно"));
                    opensound.Play();


                    Destroy(gameObject);
                    
                }

                else
                {
                    ToastScript.ShowToast("Для відкриття двері потрібен ключ \"1\"");
                    hitsound.Play();
                }

                   
            }

        }

    }
}