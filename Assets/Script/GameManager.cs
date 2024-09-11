using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;
    public GameObject Clear;
    public GameObject Fail;
    public GameObject MatchingImage;

    AudioSource audioSource;
    public AudioClip clip;

    public int cardCount = 0;
    float time = 0.0f;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        if (time >= 30.0f)
        {
            timeTxt.text = "30.0";
            Time.timeScale = 0.0f;
            Fail.SetActive(true); 
        }
    }
    
    public void Matched()
    {
        if(firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(clip);
            MatchingImage.SetActive(true);
            Invoke("CloseMatchingImage", 1f);
            firstCard.DestoryCard();
            secondCard.DestoryCard();
            cardCount -= 2;
            if(cardCount == 0 )
            {
                Time.timeScale = 0.0f;
                Clear.SetActive(true);
            }
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        firstCard = null;
        secondCard = null;
    }

    public void CloseMatchingImage()
    {
        MatchingImage.SetActive(false);
    }
}
