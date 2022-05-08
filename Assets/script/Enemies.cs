using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Enemies:MonoBehaviour
{
    int hp;
    int damage;

    public System.Int32 Hp
    {
        get { return hp; }
        set { hp = value; }
    }
    public System.Int32 Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    
    public void TakeDamage(System.Int32 damage)
    {
        hp -= damage;
    }
    public bool isDead()
    {
        return hp < 0;
    }

    public void Dead()
    {
        Destroy(gameObject);
    }

}
