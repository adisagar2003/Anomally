using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    class Vectors
    {
        public static Vector2 FindDirectionTowardsPlayer(Player playerRef, Vector3 target)
        {
            if (playerRef == null) throw new MissingReferenceException();
            Vector2 directionTowardsPlayer = (playerRef.transform.position - target).normalized;
            directionTowardsPlayer.y = 0;
            return directionTowardsPlayer;
        }

        public static float DistanceBetweenTwoObjects(MonoBehaviour ref1, MonoBehaviour ref2)
        {
            if (ref1 == null || ref2 == null) throw new MissingReferenceException();

            return Vector2.Distance(ref1.transform.position, ref2.transform.position);
        }
    }
}
