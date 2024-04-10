using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitable
{
    public int HP { get; set; }
    public abstract void Hit(int _damage);
}
