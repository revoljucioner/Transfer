using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public int Score { get; private set; } = 0;
    public Text ScoreLable;

    public void Start()
    {
        SetScoreLableText();
    }

    public void AddScorePoint()
    {
        Score += 1;
        SetScoreLableText();
    }

    private void SetScoreLableText()
    {
        ScoreLable.text = $"Score: {Score}";
    }

}
