using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerexplosion;
    public int scoreValue;
    private GameController gameController;
    public Text lives;
    public int livesValue = 3;
    private void OnTriggerEnter(Collider other)
    {

            if (other.CompareTag ("Boundary") || other.CompareTag ("Enemy")) 
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }


        if (other.tag == "Player")
        {
            Instantiate(playerexplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }
        gameController.AddScore (scoreValue);    
        Destroy(other.gameObject);
        Destroy(gameObject);

        if (other.CompareTag ("Enemy"))
        {
            livesValue -= 1;
            lives.text = livesValue.ToString();
            Destroy(gameObject);
        }


    }
    void Start()
    {
        lives.text = livesValue.ToString();
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
