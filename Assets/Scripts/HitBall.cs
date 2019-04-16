using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class HitBall : MonoBehaviour
{
    public GameObject ball;
    public float force;
    public Camera cam;
    public bool isHit;
    public GameObject arrow;
    public Slider slider;
    public float step;
    public bool firstSpace;
    public int numOfShots;

/*    public int firstHoleScore;
    public int secondHoleScore;
    public int thirdHoleScore;*/

    public Vector3 hole1;
    public Vector3 hole2;
    public Vector3 hole3;

    public Text shots;
    public Text numHole;

    public Goals goals;

    public Text Congr;

    public bool tabOn;

    private void Start()
    {
        arrow.transform.position = new Vector3(ball.transform.position.x, ball.transform.position.y + 6f, ball.transform.position.z);
        slider.gameObject.SetActive(false);
        goals = ball.GetComponent<Goals>();
    }

    void Update()
    {
        arrow.transform.forward = cam.transform.forward * -1;
        arrow.transform.Rotate(40,0,0);
        if (firstSpace)
        {
            force += step;
            if (force >= 100f)
            {
                force = 100f;
                step = step * -1;
            }
                
            if (force <= 0)
            {
                force = 0f;
                step = step * -1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !firstSpace)
        {
            firstSpace = true;
            slider.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && firstSpace)
        {
            ball.GetComponent<Rigidbody>().AddForce((cam.transform.forward * force), ForceMode.Impulse);
            force = 0;
            isHit = true;
            firstSpace = false;
            slider.gameObject.SetActive(false);
            arrow.gameObject.SetActive(false);
            numOfShots++;
        }
        slider.value = force;

        

        if (Input.GetKey(KeyCode.Return) && goals.goal1)
        {
            goals.winScreen.gameObject.SetActive(false);
            ball.SetActive(true);
            numHole.text = "Hole 2";
            ball.transform.position = hole2;
            numOfShots = 0;
        }

        if (Input.GetKey(KeyCode.Return) && goals.goal2)
        {
            goals.winScreen.gameObject.SetActive(false);
            ball.SetActive(true);
            numHole.text = "Hole 3";
            ball.transform.position = hole3;
            numOfShots = 0;
        }
        
        if (goals.goal3)
        {
            shots.gameObject.SetActive(false);
            numHole.gameObject.SetActive(false);
            numOfShots = 0;
        }
        shots.text = "Shots: " + numOfShots.ToString();

        if (Input.GetKey(KeyCode.Z) && !tabOn)
        {
            Debug.Log("kek");
            goals.scoreBoard.gameObject.SetActive(true);
            Congr.gameObject.SetActive(false);
            tabOn = true;
        }
        else if (Input.GetKey(KeyCode.Z) && tabOn)
        {
            Congr.gameObject.SetActive(true);
            goals.scoreBoard.gameObject.SetActive(false);
            tabOn = false;
        }
    }
}
    