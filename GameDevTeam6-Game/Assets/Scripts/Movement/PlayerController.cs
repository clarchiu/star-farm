using UnityEngine;

public class PlayerController : MonoBehaviour, ITargetable
{
   
    //Configuration Parameters
    [SerializeField] float moveSpeed = 3f;

    
    //Reference Variables
    private Rigidbody2D playerRB;

    
    private void Awake() {
        FindPlayerRB();

    }

    private void FindPlayerRB() {
        playerRB = GetComponent<Rigidbody2D>();
        if (!playerRB) {
            Debug.LogError("No Rigidbody2D Component Found on Player!");
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }
    private void PlayerMove() {
     
        var deltaX = Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime;
        var deltaY = Input.GetAxisRaw("Vertical") * Time.fixedDeltaTime;
        Vector2 newVelocity = new Vector2(deltaX, deltaY);
        newVelocity.Normalize();
        playerRB.velocity = newVelocity * moveSpeed;
        //plays the sound when you move -B
        SoundManager.PlaySound();
    }

    //TODO: set this somewhere else, possibly have a different script - Clarence
    public int MaxHealth;
    public int health;

    void ITargetable.GainHealth(int amount)
    {
        health += amount;
    }

    void ITargetable.KnockBack(Vector2 origin, float amount)
    {
        //throw new System.NotImplementedException();
    }

    void ITargetable.RemoveHealth(GameObject source, int amount) //no need to use source 
    {
        if (health - amount > 0)
        {
            health -= amount;
        } else
        {
            health = 0;
            //Destroy(gameObject);
            //TODO: what is the logic for when a player dies? - Clarence 
            Debug.Log("player died");
        }
    }

    void ITargetable.SetHealth(int amount)
    {
        health = amount;
    }
}
