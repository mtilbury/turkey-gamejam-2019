﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform toFollow;
    public float lerpFraction = 0.5f;
    public Vector3 offset;

    private void LateUpdate()
    {
        float oldZVal = transform.position.z;
        transform.position = transform.position + (toFollow.position + offset - transform.position) * lerpFraction;
        transform.position = new Vector3(transform.position.x, transform.position.y, oldZVal);
    }
}