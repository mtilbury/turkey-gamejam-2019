using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovementController : MonoBehaviour
{
    public float movementSpeed;

    private int direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Go up for two seconds, then down for two seconds
        if(direction % 100 < 60)
        {
            transform.position += Vector3.up * movementSpeed;
        }
        if(direction % 100 > 60)
        {
            transform.position += Vector3.down * movementSpeed;
        }
        direction++;
    }

    //public void givePriceTarget
}
