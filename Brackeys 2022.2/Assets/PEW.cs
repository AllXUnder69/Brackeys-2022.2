using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PEW : MonoBehaviour
{
    [SerializeField] Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        transform.LookAt(player);
    }
}
