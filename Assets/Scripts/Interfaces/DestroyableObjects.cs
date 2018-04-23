using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DestroyableObjects {

    void takeDamage(int damage);
    void destroySelf();
}
