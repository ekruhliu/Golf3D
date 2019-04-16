using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goals : MonoBehaviour
{
    public bool goal1;
    public bool goal2;
    public bool goal3;
    
    public int firstHoleScore;
    public int secondHoleScore;
    public int thirdHoleScore;

    public HitBall ball;
    public Canvas winScreen;
    public Text score;
    
    public Canvas scoreBoard;
    public Text score1;
    public Text score2;
    public Text score3;

    public GameObject vika;
    
    private void Awake()
    {
        vika.SetActive(false);
    }

    private void Start()
    {
        winScreen.gameObject.SetActive(false);
        scoreBoard.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.name.Equals("hole1"))
        {
            goal1 = true;
            firstHoleScore = ball.numOfShots;
            winScreen.gameObject.SetActive(true);
            score.text = "Shots: " + ball.numOfShots;
        }

        if (other.transform.name.Equals("hole2"))
        {
            goal2 = true;
            secondHoleScore = ball.numOfShots;
            winScreen.gameObject.SetActive(true);
            score.text = "Shots: " + ball.numOfShots;
        }

        if (other.transform.name.Equals("hole3"))
        {
            vika.SetActive(true);
            goal3 = true;
            thirdHoleScore = ball.numOfShots;
            scoreBoard.gameObject.SetActive(true);
            score1.text = "Hole 1: " + firstHoleScore;
            score2.text = "Hole 2: " + secondHoleScore;
            score3.text = "Hole 3: " + thirdHoleScore;
        }
    }
}
