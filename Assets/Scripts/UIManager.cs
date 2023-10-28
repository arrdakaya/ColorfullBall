using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Image whiteImage;
    private int effectControl = 0;
    private bool radial_shine;

    public Animator LayoutAnimator;

    public TextMeshProUGUI coinText;

    public Image FillRateImage;
    public GameObject player;
    public GameObject finish_line;
    
    //BUTTONS
    public GameObject settings_open;
    public GameObject settings_close;
    public GameObject sound_on;
    public GameObject sound_off;
    public GameObject vibration_on;
    public GameObject vibration_off;
    public GameObject iap;
    public GameObject information;
    public GameObject background;
    

    public GameObject intro_hand;
    public GameObject touchtoMove;
    public GameObject noAds;
    public GameObject shop_button;

    public GameObject restartScreen;

    public GameObject finishScreen;
    public GameObject blackBackground;
    public GameObject completed;
    public GameObject coin;
    public GameObject radialshine;
    public GameObject rewarded;
    public GameObject nothanks;


    public void Start()
    {
        if (PlayerPrefs.HasKey("Sound") == false)
        {
            PlayerPrefs.SetInt("Sound", 1);
            
        }
        if(PlayerPrefs.HasKey("Vibration") == false)
        {
            PlayerPrefs.SetInt("Vibration", 1);
        }

        CoinTextUpdate();
    }

    public void Update()
    {
        if (radial_shine == true)
        {
            radialshine.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 15f * Time.deltaTime));
        }
        FillRateImage.fillAmount = ((player.transform.position.z *100) / (finish_line.transform.position.z))/100;
    }

    public void FirstTouch()
    {
        intro_hand.SetActive(false);
        touchtoMove.SetActive(false);
        noAds.SetActive(false);
        shop_button.SetActive(false);
        settings_open.SetActive(false);
        settings_close.SetActive(false);
        sound_on.SetActive(false);
        sound_off.SetActive(false);
        vibration_on.SetActive(false);
        vibration_off.SetActive(false);
        iap.SetActive(false);
        information.SetActive(false);
        background.SetActive(false);
    }

    public void CoinTextUpdate()
    {
        coinText.text = PlayerPrefs.GetInt("coinn").ToString();
    }

    public void RestartButtonActive()
    {
        restartScreen.SetActive(true);
    }

    public void RestartScene()
    {
        Variables.firstTouch = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void NextScene()
    {
        Variables.firstTouch = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void FinishScreen()
    {
        StartCoroutine("FinishLaunch");
    }
    
    public IEnumerator FinishLaunch()
    {
        finishScreen.SetActive(true);
        radial_shine = true;
        Time.timeScale = 0.5f;
        finishScreen.SetActive(true);
        blackBackground.SetActive(true);
        yield return new WaitForSecondsRealtime(0.8f);
        completed.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        radialshine.SetActive(true);
        coin.SetActive(true);
        yield return new WaitForSecondsRealtime(0.4f);
        rewarded.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        nothanks.SetActive(true);
    }


    public void Settings_open()
    {
        settings_open.SetActive(false);
        settings_close.SetActive(true);
        LayoutAnimator.SetTrigger("Slide_in");

        if(PlayerPrefs.GetInt("Sound") == 1)
        {
            sound_off.SetActive(false);
            sound_on.SetActive(true);
            AudioListener.volume = 1;

        }
        else if (PlayerPrefs.GetInt("Sound") == 2)
        {
            sound_on.SetActive(false);
            sound_off.SetActive(true);
            AudioListener.volume = 0;
            
        }


        if (PlayerPrefs.GetInt("Vibration") == 1)
        {
            vibration_off.SetActive(false);
            vibration_on.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Vibration") == 2)
        {
            vibration_on.SetActive(false);
            vibration_off.SetActive(true);
        }
    }

    public void Settings_close()
    {
        settings_close.SetActive(false);
        settings_open.SetActive(true);
        LayoutAnimator.SetTrigger("Slide_out");
    }

    public void Sound_on()
    {
        sound_on.SetActive(false);
        sound_off.SetActive(true);
        AudioListener.volume = 0;
        PlayerPrefs.SetInt("Sound",2);
    }
    public void Sound_off()
    {
        sound_off.SetActive(false);
        sound_on.SetActive(true);
        AudioListener.volume = 1;
        PlayerPrefs.SetInt("Sound", 1);
    }
    public void Vibration_On()
    {
        vibration_on.SetActive(false);
        vibration_off.SetActive(true);
        PlayerPrefs.SetInt("Vibration", 2);
        
    }
    public void Vibration_Off()
    {
        vibration_off.SetActive(false);
        vibration_on.SetActive(true);
        PlayerPrefs.SetInt("Vibration", 1);
    }




    public IEnumerator WhiteEffect()
    {
        whiteImage.gameObject.SetActive(true);
        while (effectControl == 0)
        {
            yield return new WaitForSeconds(0.001f);
            whiteImage.color += new Color(0,0,0,0.1f);
            if (whiteImage.color == new Color(whiteImage.color.r, whiteImage.color.g, whiteImage.color.b, 1))
            {
                effectControl = 1;
            }
        }
        while(effectControl == 1)
        {
            yield return new WaitForSeconds(0.001f);
            whiteImage.color -= new Color(0, 0, 0, 0.1f);
            if (whiteImage.color == new Color(whiteImage.color.r, whiteImage.color.g, whiteImage.color.b, 0))
            {
                effectControl = 2;
            }
                 
        }
        if(effectControl == 2)
        {
            Debug.Log("White Effect");
        }
        
    }

    
}
