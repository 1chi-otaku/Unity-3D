using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class DoorScript2 : MonoBehaviour
    {

        private float inTimeTimeout = 2.0f;
        private float outTimeTimeout = 5.0f;
        private float timeout;



        void Start()
        {

         
        }

        void Update()
        {
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name == "Player")
            {

                if (GameState.collectedKeys.Keys.Contains("2"))
                {

                    bool isInTime = GameState.collectedKeys["2"];
                    timeout = isInTime ? inTimeTimeout : outTimeTimeout;
                    ToastScript.ShowToast("Ключ \"2\" застосовано" +
                    (isInTime ? " вчасно" : " нe вчасно"));

                    Destroy(gameObject);
                }

                else

                    ToastScript.ShowToast("Для відкриття двері потрібен ключ \"2\"");
            }

        }

    }
}