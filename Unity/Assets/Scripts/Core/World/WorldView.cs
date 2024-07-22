using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using System;

namespace ET
{
#if UNITY
    public class WorldView: SerializedMonoBehaviour
    {
        public List<string> TypeName = new List<string>();
        public Dictionary<Type, ASingleton> Singletons;


        private void Start()
        {
            TypeName.Clear();
            Singletons = World.Instance.singletons;
        }

        private void Update()
        {
            foreach (var type in World.Instance.stack)
            {
                if (TypeName.Contains(type.Name))
                {
                    continue;
                }
                TypeName.Add(type.Name);
            }

            foreach (var type in World.Instance.singletons)
            {
                if (TypeName.Contains(type.Key.Name))
                {
                    continue;
                }
                TypeName.Add(type.Key.Name);
            }
        }

    }
#endif
}