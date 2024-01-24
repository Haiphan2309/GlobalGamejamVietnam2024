using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenEnd : MonoBehaviour
{
    [SerializeField] float timeLife;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeLife);
    }
}
