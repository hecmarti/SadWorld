using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laugh
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }

}