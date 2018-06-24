using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DestroyableObjects {

    void takeDamage(float damage);
    void destroySelf();
}
