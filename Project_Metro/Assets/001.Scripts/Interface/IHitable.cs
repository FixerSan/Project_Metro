using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitable
{
    public int HP { get; set; }
    public string Tag { get; }
    public int KnockbackLevel { get; }
    public abstract void Hit(int _damage, Actor _attacker);
}
