using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using System;

namespace ET
{
#if UNITY
    public class ASingletonView: SerializedMonoBehaviour
    {        
        public ASingleton Singleton;

    }
#endif
}