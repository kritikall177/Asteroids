using UnityEngine;

namespace _Project._Code.Core
{
    public class AsyncProcessor : MonoBehaviour
    {
        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}