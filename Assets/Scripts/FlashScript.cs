using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class FlashScript : MonoBehaviour
{

    private Rigidbody playerRb;
    private float chargeTimeout = 5.0f;
    private GameObject character;
    private Light spotLight;
    private float flashCharge;

    void Start()
    {
        playerRb = GameObject.Find("CharacterPlayer").GetComponent<Rigidbody>();
        character = GameObject.Find("Character");
        spotLight = GetComponent<Light>();
        flashCharge = 1.0f;
        GameState.SubscribeTrigger(BatteryTriggerListener, "Battery");

    }

    void Update()
    {


        if (flashCharge > 0)
        {
            float difficultyMultiplier = GameState.difficulty switch
            {
                GameState.GameDifficulty.Easy => 3.0f,
                GameState.GameDifficulty.Normal => 2.0f,
                GameState.GameDifficulty.Hard => 1.0f,
                _ => 1.0f
            };

            flashCharge -= Time.deltaTime / (chargeTimeout * difficultyMultiplier);
            if (flashCharge < 0)
            {
                flashCharge = 0;
            }
            spotLight.intensity = Mathf.Clamp01(flashCharge);
        }
        if (GameState.isFpv)
        {
            this.transform.rotation = Camera.main.transform.rotation;
        }
        else {
            if(playerRb.linearVelocity.magnitude > 0.01f)
            {
                this.transform.forward = playerRb.linearVelocity.normalized;
            }
            
        }

        
    }

    private void BatteryTriggerListener(string type, object payload)
    {
        if(type == "Battery")
        {
            flashCharge += (float)payload;
        }
    }

    private void OnDestroy()
    {
        GameState.UnSubscribeTrigger(BatteryTriggerListener, "Battery");
    }
}