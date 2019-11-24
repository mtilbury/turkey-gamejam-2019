using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCameraAsCanvas : MonoBehaviour
{
    public Transform follow;

    private void LateUpdate()
    {
        transform.position = new Vector3(follow.position.x, transform.position.y, transform.position.z);
    }
}
