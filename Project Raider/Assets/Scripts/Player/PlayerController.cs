using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    // Variáveis de movimento
    private int moveSpeed = 4;
    private float jumpVelocity = 40f;
    private bool isAttacking;
    private float attackRate;
    
    // Variáveis de animação
    private bool isRunning = false;
    
    // Componentes
    private Animator animator;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask plataformLayerMask;
    private Vida enemyHealth;
    private Vida playerLife;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        playerLife = GameObject.FindGameObjectWithTag("VidaPlayer").GetComponent<Vida>();
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
        
        if(playerLife.life <= 0){
            Application.LoadLevel(Application.loadedLevel);
        }
        
        if(gameObject.transform.position.y < -30){
            Application.LoadLevel(Application.loadedLevel);
        }
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
        //Debug.Log(raycastHit2D.collider);
        return raycastHit2D.collider != null;
    }
    
    // Atacar
    private bool Atacar(){
        
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown("z") || Input.GetKeyDown("k")){
            animator.SetTrigger("isAttacking");
            isAttacking = true;
            return true;
        } else {
            if(Time.time > attackRate){
                attackRate = Time.time + 0.5f;
                isAttacking = false;
            }
        }
        
        return false;
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        
        // Quando atacar vai tirar 25 de dano da abelha
        if(other.gameObject.CompareTag("Bee")){
            enemyHealth = other.gameObject.GetComponentInChildren<Vida>();
            if(isAttacking){
                enemyHealth.SetLife(25f);
            }
        }
        
        // Quando atacar vai tirar 20 de dano da águia
        if(other.gameObject.CompareTag("Eagle")){
            enemyHealth = other.gameObject.GetComponentInChildren<Vida>();
            if(isAttacking){
                enemyHealth.SetLife(20f);
            }
        }
        
        // Quando atacar vai tirar 15 de dano da formiga
        if(other.gameObject.CompareTag("Ant")){
            enemyHealth = other.gameObject.GetComponentInChildren<Vida>();
            if(isAttacking){
                enemyHealth.SetLife(15f);
            }
        }
        
    }
    
    public void AumentarVida(float vida){
        
        playerLife.MoreLife(vida);
        
    }
    
    public void GanharChave(){
        
        animator.SetBool("Key", true);
        
    }
}
