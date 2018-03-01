using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

    static ScoreKeeper instance = null;
	public static int score;
	private Text myText;
    [SerializeField]
    private GameObject myGoalTextObject;
    private static GameObject myGoalText;
    public static int destroyed = 0;
    public static int myGoal;
    public static GameObject[] scoreObjects = new GameObject[3];

    private void Awake()
    {
        if(instance != null && this != instance)
        {
            if(myGoalTextObject != null)
                Destroy(myGoalTextObject);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            myGoalText = myGoalTextObject;
            score = 0;
            myGoal = 15;
            myText = GetComponent<Text>();
            Reset();
            scoreObjects[0] = transform.root.gameObject;
            scoreObjects[1] = gameObject;
            scoreObjects[2] = myGoalText;
            for (int i = 0; i < 3; i++)
            {
                DontDestroyOnLoad(scoreObjects[i]);
            }
        }
        myGoalText.GetComponent<Text>().text = destroyed.ToString() + "/" + myGoal;
    }

    public void Score(int points){
		Debug.Log ("Scored points");
		score += points;
		myText.text = score.ToString();
        destroyed += 1;
        if(destroyed >= myGoal)
        {
            destroyed = 0;
            myGoal += 5;
            LevelManager.LoadNextLevel();
        }
        myGoalText.GetComponent<Text>().text = destroyed.ToString() + "/" + myGoal;
    }
	
	public static void Reset(){
		score = 0;
        destroyed = 0;
	}
}
