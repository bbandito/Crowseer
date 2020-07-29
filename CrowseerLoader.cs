using Crowseer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CrowseerLoader
{
    public class Loader
    {
        private static GameObject load_object;

        public static void Init()
        {
            load_object = new GameObject();
            load_object.AddComponent<Seecrow>();
            UnityEngine.Object.DontDestroyOnLoad(load_object);
        }
        public static void Unload()
        {
            GameObject.Destroy(load_object);
        }
    }
}
