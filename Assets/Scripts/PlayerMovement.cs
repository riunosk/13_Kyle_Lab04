using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    Rigidbody playerRigidbody;
    int coinsCollected;
    int coinsToCollect;
    public Text coinCollectText;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        coinsToCollect = GameObject.FindGameObjectsWithTag("Coins").Length;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        playerRigidbody.AddForce(movement * speed * Time.deltaTime);
        if (coinsToCollect <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Coins")
        {
            coinsCollected++;
            coinsToCollect--;
            coinCollectText.text = "Coins: " + coinsCollected;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Hazard")
        {
            SceneManager.LoadScene("LoseScene");
        }
    }
}
