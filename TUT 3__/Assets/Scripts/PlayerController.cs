using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float tilt;
    public Boundary boundary;
    public GameObject shot;
    public Transform shotspawn;
    public float fireRate;

    private float nextfire;
    public AudioSource musicSource;

    private Rigidbody rb;
    public Text lives;
    public int livesValue = 3;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lives.text = livesValue.ToString();
    }
    void Update()
    {
        
        
        if (Input.GetButton("Fire1") && Time.time > nextfire)
        {
          
            nextfire = Time.time + fireRate;
            // GameObject clone =
            Instantiate(shot, shotspawn.position, shotspawn.rotation); // as GameObject;
            musicSource.Play();
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3 
            (
              Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
               0.0f,
               Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
            );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }
    }
}
