using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TMP_RedGem : MonoBehaviour
{
    private TMP_Text texto;
    private int gema = 0;
    // Start is called before the first frame update
    void Start()
    {
        texto = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        texto.text = "" + gema;
    }
    
    public void AumentarGemas(int gema){
        
        this.gema += gema;
        texto.text = "" + gema;
        
    }
}
