using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CryptoCounter : MonoBehaviour
{
    public float amountUSD = 10.0f;
    public float amountSC = 0.0f;
    public Slider slider;
    public Text sliderText;
    public Image sliderFill;

    private Color defaultSliderColor;
    public Color soldSliderColor;
    private Color defaultTextColor;
    public Color soldTextColor;

    public float startingPriceSC = 100.0f;
    public GameObject player;

    struct Abilities
    {
        public int buy;
        public int sell;
        public int numOptions;

        public Abilities(int b, int s)
        {
            buy = b;
            sell = s;
            numOptions = 2;
        }
    }

    private Abilities buyOrSell;
    private int nextAbility;


    private void Awake()
    {
        buyOrSell = new Abilities(0, 1);
        nextAbility = buyOrSell.buy;
    }

    // Start is called before the first frame update
    void Start()
    {
        defaultTextColor = sliderText.color;
        defaultSliderColor = sliderFill.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buyOrSellCrypto(float price)
    {
        price += startingPriceSC;

        if(nextAbility == buyOrSell.buy)
        {
            // Buy
            amountSC = amountUSD / price;
            amountUSD = 0.0f;
            Debug.Log("Buy! at " + price);

            // Modify slider and text
            sliderFill.color = soldSliderColor;
            sliderText.color = soldTextColor;

        }
        else if(nextAbility == buyOrSell.sell)
        {
            // Sell
            amountUSD = amountSC * price;
            amountSC = 0.0f;
            Debug.Log("Sell! at " + price);

            // Modify slider and text
            sliderFill.color = defaultSliderColor;
            sliderText.color = defaultTextColor;

            slider.value = amountUSD;
            sliderText.text = amountUSD.ToString("F2");
        }
        nextAbility = (nextAbility + 1) % buyOrSell.numOptions;
    }
}
