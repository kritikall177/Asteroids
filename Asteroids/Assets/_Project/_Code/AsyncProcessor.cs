using System;
using UnityEngine;

namespace _Project._Code
{
    //Короче в документации unirx сказали либо делать так карутины либо заменять на unirx
    public class AsyncProcessor : MonoBehaviour
    {
        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}