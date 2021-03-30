using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : MonoBehaviour
{
    public float bounceHeight = 0.5f;
    public float bounceSpeed = 4f;
    public float coinSpeed = 5f;
    public float coinHeight = 1f;
    public float coinFall = 2f;
    private Vector2 originalPosition;
    private bool canBounce = true;
    public Sprite Blank;
    public bool hasFlower = false;    //Checks if Box has flower or coin
    public int coinCount = 1;        //Number of coins in abox
    public AudioClip[] clips = new AudioClip[2];
    private AudioSource source;
    void Start()
    {
        originalPosition = transform.localPosition;
        source = GetComponent<AudioSource>();
    }
    public void BlockBounce()            
    {
        if(canBounce)
        {
            if(coinCount == 1)
                canBounce = false;
            StartCoroutine(Bounce());
            coinCount--;
        }
    }
    public void ChangeSprite()               //Changes Block to empty after bouncing
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = Blank;
    }
    void PresentCoin()                      //Instantiate coin if box has coin
    {
        source.clip = clips[1];
        source.Play();
        GameObject coin = (GameObject)Instantiate (Resources.Load("Prefabs/Coin", typeof(GameObject)));
        coin.transform.SetParent(this.transform.parent);
        coin.transform.localPosition = new Vector2(originalPosition.x, originalPosition.y + 1);
        StartCoroutine(MoveCoin(coin));
    }

    void PresentFlower()                  //Instantiate flower if hasFlower is true
    {
        source.clip = clips[0];
        source.Play();
        GameObject flower = (GameObject)Instantiate(Resources.Load("Prefabs/Flower", typeof(GameObject)));
        flower.transform.SetParent(this.transform.parent);
        flower.transform.localPosition = new Vector2(originalPosition.x, originalPosition.y + 0.45f);
    }

    IEnumerator Bounce()                  //Coroutine to bounce the block
    {
        if (hasFlower == false)
        {
            PresentCoin();
            if (coinCount == 1)
                ChangeSprite();
        }
        else
        {
            ChangeSprite();
            Invoke("PresentFlower", 0.3f);
        }
        while(true)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + bounceSpeed * Time.deltaTime);
            if (transform.localPosition.y >= originalPosition.y + bounceHeight)
                break;
            yield return null;
        }

        while(true)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - bounceSpeed * Time.deltaTime);
            if (transform.localPosition.y <= originalPosition.y)
            {
                transform.localPosition = originalPosition;
                break;
            }
            yield return null;
        }
    }

    IEnumerator MoveCoin(GameObject coin)         //Coroutine to move the coin after spawning
    {
        while(true)
        {
            coin.transform.localPosition = new Vector2(coin.transform.localPosition.x, coin.transform.localPosition.y + coinSpeed * Time.deltaTime);
            if (coin.transform.localPosition.y >= originalPosition.y + coinHeight + 1)
                break;
            yield return null;
        }
        while(true)
        {
            coin.transform.localPosition = new Vector2(coin.transform.localPosition.x, coin.transform.localPosition.y - coinSpeed * Time.deltaTime);
            if (coin.transform.localPosition.y <= originalPosition.y + coinFall + 1)
            {
                Destroy(coin.gameObject);
                Player.count++;
                break;
            }
            yield return null;
        }
    }
}
