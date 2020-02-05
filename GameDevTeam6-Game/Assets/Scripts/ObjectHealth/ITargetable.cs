using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Use this interface if you want your class to be targetable -Clarence
 * Make sure your class specifies its maxhealth and current health
 */
public interface ITargetable
{
    void SetHealth(int amount);

    void RemoveHealth(GameObject source, int amount); //use source if you need to aggro the source of damage

    void GainHealth(int amount);

    //you can leave the implementation blank if your object doesn't need to 
    void KnockBack(Vector2 origin, float amount);
}
