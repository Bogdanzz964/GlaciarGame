using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private const int COIN_SCORE_AMOUNT = 5;

    public static GameManager Instance { set; get; }

    private bool isGameStarted = false;
    public bool IsDead { set; get; }
    private PlayerMotor motor;

    //UI and UI fields

    public Animator gameCanvas;
    public Text scoreText, cointText, modifierText;
    public float score, coinScore, modifierScore;
    private int lastScore;

    //Death Menu

    public Animator deathMenuAnim;
    public Text deadScoreText, deadCoinText;

    private void Awake()
    {

        Instance = this;
        modifierScore = 1;
        motor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();

        modifierText.text = "x" + modifierScore.ToString("0.0");
        cointText.text = coinScore.ToString("0");
        scoreText.text = scoreText.text = score.ToString("0");
    }

    private void Update()
    {
        
        if(MobileInput.Instance.Tap && !isGameStarted)
        {

            isGameStarted = true;
            motor.StartRunning();
            FindObjectOfType<GlacierSpawner>().IsScrolling = true;
            FindObjectOfType<CameraMotor>().IsMoving = true;
            gameCanvas.SetTrigger("Show");

        }

        if (isGameStarted && !IsDead)
        {

            //Bump the score up

            score += (Time.deltaTime * modifierScore);
            if(lastScore != (int)score)
            {

                lastScore = (int)score;
                //Debug.Log(lastScore);
                scoreText.text = score.ToString("0");

            }
            
        }

    }

    public void GetCoin()
    {

        coinScore++;
        cointText.text = cointText.text = coinScore.ToString("0");
        score += COIN_SCORE_AMOUNT;
        scoreText.text = scoreText.text = score.ToString("0");

    }

    public void UpdateModifier(float modifierAmount)
    {

        modifierScore = 1.0f + modifierAmount;
        modifierText.text = "x" + modifierScore.ToString("0.0");

    }

    public void OnPlayButton()
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");

    }

    public void OnDeath()
    {

        IsDead = true;
        FindObjectOfType<GlacierSpawner>().IsScrolling = false;
        deadScoreText.text = score.ToString("0");
        deadCoinText.text = coinScore.ToString("0");
        deathMenuAnim.SetTrigger("Dead");

    }

}