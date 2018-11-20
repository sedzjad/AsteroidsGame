using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour {

    //movement
    public float maxThrust;
    public float maxTorque;
    //physics
    public Rigidbody2D rb;
    //screen
    public float screenTop;
    public float screenBottom;
    public float screenLeft;
    public float screenRight;
    //size of the asteroids, 3 = big etc >
    public int asteroidSize;
    public GameObject asteroidMedium;
    public GameObject asteroidSmall;
    public int points;
    public GameObject player;
    public GameObject explosie;

    public ManagerGame mG;
    

    // Use this for initialization
    void Start () {
        //random amount torque & thrust
        Vector2 thrust = new Vector2(Random.Range(-maxThrust, maxThrust),Random.Range(-maxThrust,maxThrust));
        float torque = Random.Range(-maxTorque, maxTorque);

        rb.AddForce(thrust);
        rb.AddTorque(torque);

        //finding playr
        player = GameObject.FindWithTag("Player");

        //find gamemanager
        mG = GameObject.FindObjectOfType<ManagerGame>();

	}
	
	// Update is called once per frame
	void Update () {
        Vector2 newPos = transform.position;
        if (transform.position.y > screenTop)
        {
            newPos.y = screenBottom;
        }
        if (transform.position.y < screenBottom)
        {
            newPos.y = screenTop;
        }
        if (transform.position.x > screenRight)
        {
            newPos.x = screenLeft;
        }
        if (transform.position.x < screenLeft)
        {
            newPos.x = screenRight;
        }
        transform.position = newPos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit by " + other.name);
        if (other.CompareTag("bullet"))
        {
            Destroy(other.gameObject);

            if(asteroidSize == 3)
            {//spawn 2 medium 
                 Instantiate(asteroidMedium, transform.position, transform.rotation);
                 Instantiate(asteroidMedium, transform.position, transform.rotation);

                mG.UpdateNumberOfAsteroids(1);
            }
            else if (asteroidSize == 2)
            {//spawn 2 small
                
                Instantiate(asteroidSmall, transform.position, transform.rotation);
                Instantiate(asteroidSmall, transform.position, transform.rotation);

                mG.UpdateNumberOfAsteroids(1);
            }
            else if (asteroidSize == 1)
            {//destroy asteroid
                mG.UpdateNumberOfAsteroids(-1);
            }
            player.SendMessage("ScorePoints",points);

            GameObject newExplosion = Instantiate(explosie, transform.position, transform.rotation);
            Destroy(newExplosion, 3f);
            Destroy(gameObject);

        }
    }
}
