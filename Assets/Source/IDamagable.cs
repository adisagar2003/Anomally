using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    // Start is called before the first frame update
    void TakeDamage(float amount);
    void Destruct();
}
