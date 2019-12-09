using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    public float scrollSpeed;
    public float tileSizeZ;
    private Vector3 startPosition;
    public GameController GameF;
    void Start()
    {
        startPosition = transform.position;
        GameF = GameF.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
       float newposition = Mathf.Repeat (Time.time * scrollSpeed, tileSizeZ);
        transform.position = startPosition + Vector3.forward * newposition;
        if (GameF.score >= 100)
        {
            scrollSpeed = -20;
        }
    }
}
