using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public interface IDestructable
    {
        float Health { get; set; }
        void RecieveHit(float damage);
        void Die();
    }

