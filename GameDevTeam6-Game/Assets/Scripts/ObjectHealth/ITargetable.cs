using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Use this interface if you want your class to be targetable -Clarence
 * Make sure your class specifies its maxhealth and current health
 * TODO: possibly change this to an abstract class
 */
public interface ITargetable
{
    void SetHealth(int amount);

    void RemoveHealth(int amount);

    void GainHealth(int amount);

    //you can leave the implementation blank if your object doesn't need to 
    void KnockBack(Vector3 origin, float amount);
}
