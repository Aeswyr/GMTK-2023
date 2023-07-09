using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public void OnKnock()
    {
        Destroy(this.gameObject);
    }
}
