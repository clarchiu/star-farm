using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HealthBar_))]
public class EnemyHealth : MonoBehaviour, ITargetable
{
    public EnemyAI enemy;
    public HealthBar_ healthBar;
    private int currentHealth;
    private int maxHealth;

    
    // Use this for initialization
    void Start()
    {
        if (!enemy || !healthBar)
        {
            this.enabled = false;
            throw new System.Exception("set enemy parent in inspector");
        }

        maxHealth = enemy.MyAttributes.maxHealth;
        currentHealth = enemy.MyAttributes.currentHealth;
    }

    #region ITargetable Implementation
    public virtual void SetHealth(int amount)
    {
        currentHealth = amount;
    }

    public virtual void RemoveHealth(GameObject source, int amount)
    {
        if (currentHealth - amount > 0)
        {
            currentHealth -= amount;
            healthBar.UpdateHealthBar((float)currentHealth / maxHealth);

            if (!GameObject.ReferenceEquals(enemy.Target, source) && source.CompareTag("Player"))
            {
                if (source.GetComponent<ITargetable>() != null)
                {
                    //Debug.Log("changing state");
                    enemy.Target = source;
                    enemy.ChangeState(new PathState());
                }
            }
            SoundEffects_.Instance.PlaySoundEffect(SoundEffect.gruntInPain);
        }
        else
        {
            currentHealth = 0;
            enemy.gameObject.SetActive(false);
            EnemyAI.enemies.Remove(enemy);
            PlayEffect.Instance.PlayBreakEffect(gameObject.transform.position);
            SoundEffects_.Instance.PlaySoundEffect(SoundEffect.gruntInPain2);
            Destroy(enemy.gameObject);
        }
    }

    //private IEnumerator EnemyDie()
    //{
    //    enemy.gameObject.SetActive(false);
    //    EnemyAI.enemies.Remove(enemy);
    //    PlayEffect.Instance.PlayBreakEffect(gameObject.transform.position);
    //    SoundEffects_.Instance.PlaySoundEffect(SoundEffect.gruntInPain2);
    //    yield return null;
    //    Destroy(enemy.gameObject);

    //}

    public virtual void GainHealth(int amount)
    {
        currentHealth += amount;
    }

    public virtual void GetKnockedBack(Vector2 origin, float amount)
    {
        StartCoroutine(enemy.GetKnockedBack(origin, amount));
    }
    #endregion
}
