using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] GameObject cam;
    [SerializeField] float parallaxEff;
    float length, startPos;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float tmp = cam.transform.position.x * (1 - parallaxEff);
        float dist = cam.transform.position.x * parallaxEff;

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (tmp > startPos + length)
            startPos += length;
        else if (tmp < startPos - length)
            startPos -= length;
    }
}
