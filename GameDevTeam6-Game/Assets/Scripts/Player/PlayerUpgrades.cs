using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    private int obstacleAttackLevel = 0;
    private int meleeAttackLevel = 0;
    private int rangedAttackLevel = 0;

    int[] obstacleAttackUpgrade = new int[] { 40, 100, 200, 400, 800, 1500 };
    int[] meleeAttackUpgrade = new int[] { 20, 25, 30, 35, 40, 100 };
    int[] rangedAttackUpgrade = new int[] { 10, 15, 20, 25, 30, 60 };

    public int obstacleAttackDamage { get { return obstacleAttackUpgrade[obstacleAttackLevel]; } }
    public int meleeAttackDamage { get { return meleeAttackUpgrade[meleeAttackLevel]; } }
    public int rangedAttackDamage { get { return rangedAttackUpgrade[rangedAttackLevel]; } }

    private static PlayerUpgrades _instance;
    public static PlayerUpgrades Instance
    {
        get { {
                if (_instance == null) {
                    _instance = FindObjectOfType<PlayerUpgrades>();
                }
                if (_instance == null) {
                    Debug.Log("PlayerUpgrades script not found!");
                }
                return _instance;
            }
        }
    }

    public void UpgradeObstacleDamage()
    {
        if (obstacleAttackLevel < obstacleAttackUpgrade.Length - 1)
        {
            obstacleAttackLevel += 1;
        }
    }
    public void UpgradeMeleeDamage()
    {
        if (meleeAttackLevel < meleeAttackUpgrade.Length - 1)
        {
            meleeAttackLevel += 1;
        }
    }
    public void UpgradeRangedDamage()
    {
        if (rangedAttackLevel < rangedAttackUpgrade.Length - 1)
        {
            rangedAttackLevel += 1;
        }
    }
}