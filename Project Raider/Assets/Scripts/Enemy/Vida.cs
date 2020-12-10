using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    // Variáveis
    public float life;
    
    // Componentes
    private Transform barra;
    // Start is called before the first frame update
    void Start()
    {
        barra = transform.Find("Barra");
        life = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetLife(float damage){
        life -= damage;
        
        if(life <= 0){
            gameObject.SetActive(false);
        }
        
        barra.localScale = new Vector2((life/100f), 1f);
    }
    
    public void MoreLife(float moreLife){
        life += moreLife;
        
        if(life > 100){
            life = 100;
        }
        
        barra.localScale = new Vector2((life/100f), 1f);
    }
}
