using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class spaceship : MonoBehaviour {

    
    public Rigidbody2D rb;
    public float thrust;
    public float turnThrust;
    private float thrustInput;
    private float turnInput;
    public float screenTop;
    public float screenBottom;
    public float screenLeft;
    public float screenRight;
    public float bulletForce;
    public float collisionForce;
    public int score;
    public int lives;

    public Text ScoreText;
    public Text LivesText;
    public Text highScoreListText;
    public GameObject gameOverPanel;
    public GameObject newHighscorePanel;
    public InputField highScoreInput;
    public ManagerGame mG;

    public GameObject explosion;
    public GameObject bullet;

    public Color aliveColor;
    public Color normalColor;
    

    // Use this for initialization
    void Start () {
        score = 0;
        lives = 3;

        ScoreText.text = "Score " + score;
        LivesText.text = "Lives " + lives; 
	}
	// Update is called once per frame
	void Update () {
        //checking input from the keyboard
        thrustInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        //input check & bullets
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
            newBullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * bulletForce);
            Destroy(newBullet, 4.0f);
        }
        
        //screenwrap
        Vector2 newPos = transform.position;
        if(transform.position.y > screenTop)
        {
            newPos.y = screenBottom;
        }
        else if (transform.position.y < screenBottom)
        {
            newPos.y = screenTop;
        }
        else if (transform.position.x > screenRight)
        {
            newPos.x = screenLeft;
        }
        else if (transform.position.x < screenLeft)
        {
            newPos.x = screenRight;
        }
        transform.position = newPos;
    }
    void FixedUpdate(){
        rb.AddRelativeForce (Vector2.up * thrustInput);
        rb.AddTorque(-turnInput);
    }

    void ScorePoints(int pointsToAdd)
    {
        score += pointsToAdd;
        ScoreText.text = "Score " + score;
    }

    void Respawn()
    {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.enabled = true;
        sr.color = aliveColor;
        Invoke("alive", 2f);
    }

    void alive()
    {
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().color = normalColor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit");
        if(collision.relativeVelocity.magnitude > collisionForce)
        {
            Debug.Log(collision.relativeVelocity.magnitude);
            lives--;
            LivesText.text = "Lives " + lives;
            GameObject newGameObject = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(newGameObject, 3.0f);
            //respawn
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Invoke("Respawn", 3f);

            if (lives <= 0)
            {
                GameOver();

            }
        }
    }

    void GameOver()
    {
        CancelInvoke();
        
        //mG checks score
        if (mG.CheckForHighScore(score))
        {
            newHighscorePanel.SetActive(true);
        }
        else{
            gameOverPanel.SetActive(true);
            highScoreListText.text = "HIGH SCORE" + "\n" + "\n" + PlayerPrefs.GetString("highscoreName") + " " + PlayerPrefs.GetInt("highscore");
        }
    }

    public void HighScoreInput()
    {
        string newInput = highScoreInput.text;
        Debug.Log(newInput);
        newHighscorePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        PlayerPrefs.SetString("highscoreName", newInput);
        PlayerPrefs.SetInt("highscore", score);
        highScoreListText.text = "HIGH SCORE" + "\n" + "\n" + PlayerPrefs.GetString("highscoreName") + " " + PlayerPrefs.GetInt("highscore");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("AsteroidGame");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
