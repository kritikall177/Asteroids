using System;
using UnityEngine;

namespace _Project._Code
{
    public class AsyncProcessor : MonoBehaviour
    {
        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}