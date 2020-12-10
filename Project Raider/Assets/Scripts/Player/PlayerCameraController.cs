using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    [SerializeField] private float speed;
    
    // Update is called once per frame
    void Update () {
        
        Vector3 position = this.transform.position;
        
        position.y = Mathf.Lerp(this.transform.position.y, player.transform.position.y, speed * Time.deltaTime);
        position.x = Mathf.Lerp(this.transform.position.x, player.transform.position.x, speed * Time.deltaTime);
        
        this.transform.position = position;
    }
}
