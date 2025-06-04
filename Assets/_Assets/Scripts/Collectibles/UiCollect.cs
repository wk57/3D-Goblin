using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiCollect : MonoBehaviour
{

    private TextMeshProUGUI bottleText;


    // Start is called before the first frame update
    void Start()
    {
        bottleText = GetComponent<TextMeshProUGUI>();   
    }


    public void UpdateBottleText(PlayerCollect playerCollect) 
    {
        bottleText.text = playerCollect.numberOfBottles.ToString();
    }

}
