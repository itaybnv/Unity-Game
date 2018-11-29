using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour 
{
	public GameObject projectile;
    public Vector3 projectileSpeed;
    public float attackRange;
    public int meleeDamage;
    [Header("Cooldown settings: ")]
    public float ability1CD;
    public float ability2CD;
    public float ability3CD;
    public float ability4CD;

    [Header("cooldown Images: ")]
    public Image cooldown1Image;
    public Image cooldown2Image;
    public Image cooldown3Image;
    public Image cooldown4Image;

    [Header("Ignore: ")]
    public float ability1CDMax;
    public float ability2CDMax;
    public float ability3CDMax;
    public float ability4CDMax;

    void Start()
    {
        // could put a built in cooldown on gameobject projectile and use it later
        ability1CDMax = ability1CD;
        ability2CDMax = ability2CD;
        ability3CDMax = ability3CD;
        ability4CDMax = ability4CD;
    }

}
