using System;
using UnityEngine;

namespace DefaultNamespace
{
    public static class PrintManager
    {

        public static void Print(string content)
        {
            Debug.Log(content);
        }
    }
}