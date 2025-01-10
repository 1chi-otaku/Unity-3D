using UnityEngine;
using UnityEngine.UI;

public class KeyPointScript : MonoBehaviour
{
    [SerializeField] private string keyName = "1";
    [SerializeField] private Image key1Image;
    [SerializeField] private Image key2Image;
    public bool isInTime { get; set; }
    private bool iskeygot;
    public bool isKeyGot
    {
        get => iskeygot;
        set
        {
            iskeygot = value;
            if (value)
            {

                GameState.collectedKeys.Add(keyName, isInTime);
                switch (keyName)
                {
                    case "1":
                        key1Image.enabled = true;
                        break;
                    case "2":
                        key2Image.enabled = true;
                        break;

                }
                GameState.TriggerEvent("Key", new TriggerPayload()
                {
                    notification = "You have obtained Key " + keyName + (isInTime ? " in time!" : " late"),
                    payload = isInTime
                });

                GameState.score += (isInTime ? 2: 1) *
                    (GameState.difficulty switch
                    {
                        GameState.GameDifficulty.Easy => 1,
                        GameState.GameDifficulty.Normal => 2,
                        GameState.GameDifficulty.Hard => 3,
                        _ => 1.0f
                    });

            }
        }
    }
}