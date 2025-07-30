using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if player entered the exit trigger
        if (other.CompareTag("Player"))
        {
            if (coinScript.hasCollectedCoin == true)
            {
                Debug.Log("Level complete!");
                // Add level completion logic here
                // Example: SceneManager.LoadScene("NextLevel");
            }
            else
            {
                Debug.Log("Collect the coin first!");
            }
        }
    }
}
