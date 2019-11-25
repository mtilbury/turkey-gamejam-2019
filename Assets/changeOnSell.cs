using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeOnSell : MonoBehaviour
{

    public Sprite buy;
    public Sprite sell;

    public string currentImage = "buy";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleSprite()
    {
        if(currentImage == "buy")
        {
            GetComponent<Image>().sprite = sell;
            currentImage = "sell";
        }

        else if(currentImage == "sell")
        {
            GetComponent<Image>().sprite = buy;
            currentImage = "buy";
        }
    }
}
