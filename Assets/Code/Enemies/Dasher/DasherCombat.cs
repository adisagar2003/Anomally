using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DasherCombat : MonoBehaviour
{
    [SerializeField] private GameObject hurtBoxGameObject;
    [SerializeField] private GameObject hitBoxGameObject;
    [SerializeField] private Player player;

    private Collider2D hurtBox;
    private Collider2D hitBox;

    private void Awake()
    {
        hurtBox = hurtBoxGameObject.GetComponent<BoxCollider2D>();
        hitBox = hitBoxGameObject.GetComponent<BoxCollider2D>();
    }
    

}
