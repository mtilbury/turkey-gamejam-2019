using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using PathCreation.Examples;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Gamepad gamepad;

    public CryptoCounter cryptoWallet;
    private PathFollower follow;
    private Collider playerCollider;

    public float jumpMagnitude = 1.0f;
    public float initialVerticalMagnitude = 10.0f;
    public float gravityMagnitude = 2.0f;
    public float initialVerticalBoost = 5.0f;
    private bool jumping = false;

    void Start()
    {
        gamepad = Gamepad.current;
        follow = GetComponent<PathFollower>();
        playerCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        // If presses X, buy/sell
        if (gamepad.xButton.wasPressedThisFrame || Keyboard.current.enterKey.wasPressedThisFrame)
        {
            cryptoWallet.buyOrSellCrypto(transform.position.y);
        }

        // If presses A, jump
        if (gamepad.aButton.wasPressedThisFrame || Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (!jumping)
            {
                StartCoroutine(jump());
            }
        }
    }

    private IEnumerator jump()
    {
        jumping = true;
        follow.toggleFollow(false); // Stop following path
        float verticalMagnitude = initialVerticalMagnitude;
        playerCollider.enabled = true;

        int numFrames = 0;

        while (jumping)
        { 
            numFrames++;
            transform.position += new Vector3(follow.speed, (verticalMagnitude * Mathf.Max((initialVerticalBoost / numFrames), 1)), 0) / 60;
            verticalMagnitude -= (9.8f / 60) * gravityMagnitude;
            yield return null;
        } 
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Not jumping anymore
        jumping = false;
        Debug.Log("collide");

        follow.toggleFollow(true);
        playerCollider.enabled = false;
    }
}
