using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform toFollow;

    private void LateUpdate()
    {
        transform.position = new Vector3(toFollow.position.x, toFollow.position.y, transform.position.z);
    }
}
