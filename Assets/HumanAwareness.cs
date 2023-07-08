using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAwareness : MonoBehaviour
{
    HumanController humanController;
    void Awake()
    {
        humanController = GetComponentInParent<HumanController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Food"))
            humanController.WalkToTarget(collision.transform.position);
    }
}
