using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleController : MonoBehaviour
{
    // Variáveis de movimentação
    [SerializeField] private float speed;
    private float timeMovement = 0f;
    private float range = 7f;
    private float attackRange = 2f;
    private float attackTime = 0f;
    private float gemSpawnTime = 0f;
    
    // Componentes
    private Vector2 position;
    private Vector2 posA;
    private Vector2 posB;
    private Vector2 nextPos;
    private RaycastHit2D hit;
    private LayerMask playerLayerMask;
    private Animator animator;
    private Vida life;
    private Vida playerLife;
    [SerializeField] GameObject dropLife;
    [SerializeField] GameObject dropGem;
    
    // Chaves
    [SerializeField] private bool isMoving;
    [SerializeField] private bool isChasing;
    [SerializeField] private bool isAttacking;
    [SerializeField] private bool isDead;
    
    // Start is called before the first frame update
    void Start()
    {
        position = gameObject.transform.position;
        
        posA = position;
        posA.x -= 2;
        
        posB = position;
        posB.x += 2;
        
        nextPos = posB;
        
        isMoving = true;
        
        playerLayerMask = LayerMask.GetMask("Player");
        
        animator = GetComponent<Animator>();
        
        life = GetComponentInChildren<Vida>();
        playerLife = GameObject.FindWithTag("VidaPlayer").GetComponent<Vida>();
        
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving && !isDead){
            Movimentar();
        }
        
        if(!isDead){
            PerseguirEAtacar();   
        }
        
        if(life.life <= 0){
            Morrer();
        }
        
    }
    
    private void Movimentar(){
        
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, nextPos, speed * Time.deltaTime);
        
        if(Time.time > timeMovement){
            timeMovement = Time.time + 15f;
            TrocarDirecaoMovimento();
        }
        
    }
    
    private void TrocarDirecaoMovimento(){
        
        if (nextPos == posA){
            nextPos = posB;
        } else if(nextPos == posB){
            nextPos = posA;
        }
        
    }
    
    private void PerseguirEAtacar(){
        
        if(nextPos == posA){
            hit = Physics2D.Raycast(transform.position + (transform.up * 0.5f), Vector2.left, range, playerLayerMask);
            Debug.DrawRay(transform.position + (transform.up * 0.5f), Vector2.left * range, Color.blue);
            
            
        } else if(nextPos == posB){
            hit = Physics2D.Raycast(transform.position + (transform.up * 0.5f), Vector2.right, range, playerLayerMask);
            Debug.DrawRay(transform.position + (transform.up * 0.5f), Vector2.right * range, Color.blue);
            
        }
        
        if(hit){
            if(hit.collider.CompareTag("Player")){
                isMoving = false;
                animator.SetBool("isChasing", true);
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, hit.transform.position, 2 * speed * Time.deltaTime);
                
                if((gameObject.transform.position.x - hit.transform.position.x) < 2.2f){
                    Debug.Log("Atacando");
                    if(Time.time > attackTime){
                        attackTime = Time.time + 1.3f;
                        animator.SetTrigger("isAttacking");
                        Debug.Log("Ataque realizado!");
                        playerLife.SetLife(20f);
                    }
                }
                
                //Debug.Log("Está seguindo");
                isChasing = true;
            } 
        } else {
            isMoving = true;
            isChasing = false;
            animator.SetBool("isChasing", false);
        }
    }
    
    private void Morrer(){
        
        isDead = true;
        animator.SetTrigger("isDead");
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        
        if(Time.time > gemSpawnTime){
            gemSpawnTime = Time.time + 6f;
            
            for (int i = 0; i < Random.Range(1,2); i++) {
                Instantiate(dropLife, this.transform.position, this.transform.rotation);
            }
            
            for (int i = 0; i < Random.Range(1,4); i++) {
                Instantiate(dropGem, this.transform.position, this.transform.rotation);
            }
        
        }
        
        Destroy(gameObject, 4f);
        
    }
    
}
