using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    // Variáveis de movimento
    private int moveSpeed = 4;
    private float jumpVelocity = 30f;
    
    // Variáveis de animação
    private bool isRunning = false;
    
    // Componentes
    private Animator animator;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask plataformLayerMask;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(MovimentarJogador()){
            animator.SetBool("isRunning", true);
        } else {
            animator.SetBool("isRunning", false);
        }
        
        Pular();
        
        Atacar();
    }
    
    // Mecânicas básicas de movimentação - andar
    private bool MovimentarJogador(){
        
        // Movimento horizontal - esquerda
        if (Input.GetKey("a") || Input.GetKey("left")){
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            return true;
        }
        
        // Movimento horizontal - direita
        if (Input.GetKey("d") || Input.GetKey("right")){
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            return true;
        }
        
        return false;
    }
    
    // Mecânicas básicas de movimentação - pulo
    private bool Pular(){
        
        // Pulo
        if (isGrounded() && (Input.GetKeyDown("space") || Input.GetKeyDown("x") || Input.GetKeyDown("l"))){
            rb.velocity = Vector2.up * jumpVelocity;
            animator.SetTrigger("isJumping");
            return true;
        }
        
        return false;
        
    }
    
    // Verifica se o player está em cima de uma plataforma
    private bool isGrounded(){
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, plataformLayerMask);
        Debug.Log(raycastHit2D.collider);
        return raycastHit2D.collider != null;
    }
    
    // Atacar
    private bool Atacar(){
        
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown("z") || Input.GetKeyDown("k")){
            animator.SetTrigger("isAttacking");
            return true;
        }
        
        return false;
    }
}
