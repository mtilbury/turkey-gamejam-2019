using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ChangeControlScheme : MonoBehaviour
{
    public string gamepadInstruction;
    public string keyboardInstruction;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = keyboardInstruction;

        if (Gamepad.current != null)
        {
            GetComponent<Text>().text = gamepadInstruction;
        }
    }

}
