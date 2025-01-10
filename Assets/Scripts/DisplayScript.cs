using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScript : MonoBehaviour
{
    private TextMeshProUGUI clock;
    private TextMeshProUGUI score;
    private Image keyImage;
    private float gameTime;

    private void Start()
    {
        gameTime = 0.0f;
        clock = transform.Find("Content/Background/ClockTMP").GetComponent<TextMeshProUGUI>();
        score = transform.Find("Content/Background/ScoreTMP").GetComponent<TextMeshProUGUI>();
        //keyImage = transform.Find("Content/Background/KeyImage").GetComponent<Image>();
    }
    private void Update()
    {
        gameTime += Time.deltaTime;
        score.text = GameState.score.ToString();
    } 
    private void LateUpdate()
    {
        int hour = (int)gameTime / 3600, min = ((int)gameTime % 3600) / 60, sec = (int)gameTime % 60;
        clock.text = $"{hour:D2}:{min:D2}:{sec:D2}";
    }
  
 
}