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
    private BoxCollider playerCollider;

    public float jumpMagnitude = 1.0f;
    public float initialVerticalMagnitude = 10.0f;
    public float gravityMagnitude = 2.0f;
    private bool jumping = false;

    void Start()
    {
        gamepad = Gamepad.current;
        follow = GetComponent<PathFollower>();
        playerCollider = GetComponent<BoxCollider>();
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
        playerCollider.enabled = true;
        float verticalMagnitude = initialVerticalMagnitude;

        while (jumping)
        {
            transform.position += new Vector3(follow.speed, verticalMagnitude, 0) / 60;
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
