using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Camera Follow Reference
    [SerializeField] public GameObject cameraFollowPoint;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShakeCamera(float facingDirection)
    {
        if (cameraFollowPoint != null)
        {
            CharacterFollowPoint characterFollowPointAttributes = cameraFollowPoint.GetComponent<CharacterFollowPoint>();
            characterFollowPointAttributes.ShakeCamera(facingDirection);
        }
    }
}
