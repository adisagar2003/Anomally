using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DasherMovement : MonoBehaviour
{
    private Player player;
    private Rigidbody2D rb2D;
    private float direction = -1;
    [SerializeField] private float speed = 10;
    [SerializeField] private float recoilSpeed= 10;

}
