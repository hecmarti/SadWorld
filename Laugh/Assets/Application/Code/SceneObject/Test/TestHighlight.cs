using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laugh.SceneObject.Test
{
    public class TestHighlight : BaseHighlight
    {
        public override void SwitchHighlight(bool switchOn)
        {
            Debug.Log($"{gameObject.name} highlight: {switchOn}");
            //MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();

            //Material material = meshRenderer.material;
            //material.color = switchOn ? Color.red : Color.white;

            //meshRenderer.material = material;

            SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.color = switchOn ? Color.red : Color.white;
        }
    }
}