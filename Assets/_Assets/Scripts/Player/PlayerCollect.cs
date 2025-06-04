using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerCollect : MonoBehaviour
{
     public int numberOfBottles {  get; private set; }

    public UnityEvent<PlayerCollect> OnBottleCollected;

   

    public void BottleCollected()
    {
        numberOfBottles++;
        OnBottleCollected.Invoke(this);

    }

   
}
