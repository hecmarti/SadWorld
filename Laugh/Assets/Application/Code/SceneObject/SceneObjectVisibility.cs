using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Laugh.SceneObject
{
    public class SceneObjectVisibility : MonoBehaviour
    {
        private List<VisibilityPoint> visibilityPoints;

        private void Awake()
        {
            visibilityPoints = GetComponentsInChildren<VisibilityPoint>().ToList();
        }

        public bool IsVisible()
        {
            int layerMask = LayerMask.GetMask("SceneObject");
            return CompleteOverlape(layerMask);
        }

        private bool CompleteOverlape(int layerMask)
        {
            if (!visibilityPoints.Any())
            {
                return true;
            }

            foreach (VisibilityPoint visibilityPoint in visibilityPoints)
            {
                Collider2D collider = GetComponent<Collider2D>();
                Collider2D[] colliders = Physics2D.OverlapPointAll(visibilityPoint.transform.position, layerMask);
                if (colliders.Length == 0)
                {
                    return true;
                }
                bool isOverlaped = colliders.Any(otherCollider => otherCollider != collider && otherCollider.transform.position.z < collider.transform.position.z);
                if (!isOverlaped)
                {
                    return true;
                }
            }
            return false;
        }

        private bool PartialyOverlaps(int layerMask)
        {
            Collider2D collider = GetComponent<Collider2D>();
            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.layerMask = layerMask;
            Collider2D[] results = new Collider2D[10];
            int overlaps = Physics2D.OverlapCollider(collider, contactFilter, results);
            if (overlaps == 0)
            {
                return true;
            }
            return results.Any(overlap => overlap != null && overlap.transform.position.z > transform.position.z);
        }
    }
}