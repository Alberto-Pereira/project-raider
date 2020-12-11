using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Redgem : MonoBehaviour
{
    private TMP_RedGem redGemPlayer;
    // Start is called before the first frame update
    void Start()
    {
        redGemPlayer = GameObject.FindGameObjectWithTag("RedGemPlayer").GetComponent<TMP_RedGem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        
        if(other.transform.CompareTag("Player")){
            redGemPlayer.AumentarGemas(1);
            Destroy(gameObject);    
        }
        
        
    }
}
