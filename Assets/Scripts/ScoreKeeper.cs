using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int score = 0;
    static ScoreKeeper instance;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void ModifyScore(int value)
    {
        score += value;
        Mathf.Clamp(score, 0, int.MaxValue);
        Debug.Log(score);
    }
    
    public void ResetScore()
    {
        score = 0;
    }
}
