using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager uimanager;
    
    public void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && gameObject.CompareTag("FinishLine"))
        {
            
            CoinCalculator(100);
            uimanager.CoinTextUpdate();
            uimanager.FinishScreen();

        }
    }

    public void CoinCalculator(int coin)
    {
        if (PlayerPrefs.HasKey("coinn"))
        {
            int oldScore = PlayerPrefs.GetInt("coinn");
            PlayerPrefs.SetInt("coinn", oldScore + coin);
        }
        else
        {
            PlayerPrefs.SetInt("coinn", 0);
        }
    }
}
