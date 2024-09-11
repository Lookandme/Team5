using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public string idx = null;

    public GameObject front;
    public GameObject back;

    public Animator anim;
    public SpriteRenderer frontImage;

    AudioSource audioSource;
    public AudioClip clip;

    public ParticleSystem Pt;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setting(string name) //!!!int number
    {
        idx = name;
        frontImage.sprite = Resources.Load<Sprite>(idx); //!!!$"Kim{idx}"
    }

    public void OpenCard()
    {
        audioSource.PlayOneShot(clip);
        anim.SetBool("isOpen", true);
        Invoke("CardState", 0.4f);

        // firstCard가 비었다면,
        if (GameManager.Instance.firstCard == null)
        {
            // firstCard에 내 정보를 넘겨준다.
            GameManager.Instance.firstCard = this;
        }
        // firstCard가 비어있지 않다면,
        else
        {
            // secondCard에 내 정보를 넘겨준다.
            GameManager.Instance.secondCard = this;
            // Mached 함수를 호출해 준다.
            GameManager.Instance.Matched();
        }
    }

    public void DestoryCard()
    {
        Invoke("PlayPt", 0.2f);
        Invoke("DestroyCardInvoke", 1.0f);
    }

    public void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 1.0f);
    }

    public void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
    }
    public void CardState()
    {
        front.SetActive(true);
        back.SetActive(false);
    }

    public void PlayPt()
    {
        Pt.Play();
    }
}
