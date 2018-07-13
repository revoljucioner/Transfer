using UnityEngine;

public class ScoreScript : MonoBehaviour
{

    public int Score { get; private set; } = 0;

    public void AddScorePoint()
    {
        Score += 1;
        //Debug.Log("Score: " + Score);
    }

}
