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
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.localPosition;
    }
    public void BlockBounce()
    {
        if(canBounce)
        {
            canBounce = false;
            StartCoroutine(Bounce());
        }
    }
    public void ChangeSprite()
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = Blank;
    }
    void PresentCoin()
    {
        GameObject coin = (GameObject)Instantiate (Resources.Load("Prefabs/Coin", typeof(GameObject)));
        coin.transform.SetParent(this.transform.parent);
        coin.transform.localPosition = new Vector2(originalPosition.x, originalPosition.y + 1);
        StartCoroutine(MoveCoin(coin));
    }
    IEnumerator Bounce()
    {
        ChangeSprite();
        PresentCoin();
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

    IEnumerator MoveCoin(GameObject coin)
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
