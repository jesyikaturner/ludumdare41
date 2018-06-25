using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DestroyableObjects {

    void TakeDamage(float damage);
    void DestroySelf();
}
