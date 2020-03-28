using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, ITargetable
{
    //Configuration Parameters
    [SerializeField] float moveSpeed = 3f;
    private float maxX = 58, minX = 0, maxY = 58, minY = 0;

    private HealthBar_ healthBar;

    //Reference Variables
    private Rigidbody2D playerRB;
    private TimeSystem time;


    private void Awake() {
        FindPlayerRB();
        healthBar = gameObject.AddComponent<HealthBar_>();
        time = FindObjectOfType<TimeSystem>();
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

        if (time.isDay())
        {
            health = maxHealth;
            healthBar.UpdateHealthBar(health / maxHealth);
            //reset health to max during the day
        }
    }

    private void PlayerMove() {

        var deltaX = Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime;
        var deltaY = Input.GetAxisRaw("Vertical") * Time.fixedDeltaTime;

        if (PlayerStates.Instance.GetState() == playerStates.IDLE && (deltaX != 0 || deltaY != 0))
        {
            PlayerStates.Instance.ChangeState(playerStates.WALKING);
            //GetComponent<AudioSource>().Play();
        }
        if (PlayerStates.Instance.GetState() == playerStates.WALKING && (deltaX == 0 && deltaY == 0))
        {
            PlayerStates.Instance.ChangeState(playerStates.IDLE);
            //GetComponent<AudioSource>().Stop();
        }

        if (PlayerStates.Instance.GetState() != playerStates.WALKING) {
            playerRB.velocity = new Vector2(0,0);
            
            if (SoundEffects_.Instance.walkLoud.isPlaying)
            {
                SoundEffects_.Instance.walkLoud.Stop();
            }
            return;

        } else
        {        
            if (!SoundEffects_.Instance.walkLoud.isPlaying)
            {
                SoundEffects_.Instance.PlaySoundEffect(SoundEffect.walkLoud);
            }
        }

       

        Vector2 newVelocity = new Vector2(deltaX, deltaY);
        newVelocity.Normalize();
        playerRB.velocity = newVelocity * moveSpeed;
       


        //Stop going off screen
        if (transform.position.x < minX) { transform.position = new Vector2(minX, transform.position.y); }
        if (transform.position.x > maxX) { transform.position = new Vector2(maxX, transform.position.y); }
        if (transform.position.y < minY) { transform.position = new Vector2(transform.position.x, minY); }
        if (transform.position.y > maxY) { transform.position = new Vector2(transform.position.x, maxY); }

        //Set sorting order so that player sprite is below trees, this requires trees and player to be on same sorting layer
        transform.GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(-transform.position.y);
    }

    //TODO: set this somewhere else, possibly have a different script - Clarence
    public int maxHealth;
    public int health;

    void ITargetable.GainHealth(int amount)
    {
        health += amount;
        healthBar.UpdateHealthBar((float)health / maxHealth);
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
            ActivateLayer("Death Layer");
            health = 0;
            GetComponent<Animator>().Play("PlayerDeath1");
            PlayerStates.Instance.ChangeState(playerStates.DEATH);
            StartCoroutine(WaitForDeath());
        }
        healthBar.UpdateHealthBar((float)health / maxHealth);
    }

    void ITargetable.SetHealth(int amount)
    {
        health = amount;
        healthBar.UpdateHealthBar((float)health / maxHealth);
    }

    private void ActivateLayer(string layerName)
    {
        for (int i = 0; i < GetComponent<Animator>().layerCount; i++)
        {
            GetComponent<Animator>().SetLayerWeight(i, 0);
        }
        GetComponent<Animator>().SetLayerWeight(GetComponent<Animator>().GetLayerIndex(layerName), 1);
    }

    IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Death");
    }
}
