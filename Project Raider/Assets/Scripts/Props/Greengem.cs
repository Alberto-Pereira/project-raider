using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Greengem : MonoBehaviour
{
    private AudioSource gemAudio;
    public AudioClip dropSound;
    // Start is called before the first frame update
    void Start()
    {
        gemAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        
        if(other.transform.CompareTag("Player")){
            
            other.transform.GetComponent<PlayerController>().AumentarVida(10f);
            
            gemAudio.PlayOneShot(dropSound);
            
            Destroy(gameObject, 0.3f);
            
        }
        
    }
}
