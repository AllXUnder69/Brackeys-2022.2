using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    [Header("Main Properties")]
    public float triggerRange;

    [Header("Gun Properties")]
    public float fireRate = 2.25f;
    public int damage;
}