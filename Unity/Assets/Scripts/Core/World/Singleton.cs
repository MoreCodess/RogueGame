using MemoryPack;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    public interface ISingletonReverseDispose
    {
        
    }
    
    public abstract class ASingleton: DisposeObject
    {
        internal abstract void Register();
    }
    
    public abstract class Singleton<T>: ASingleton where T: Singleton<T>
    {
#if ENABLE_VIEW && UNITY_EDITOR
        [BsonIgnore]
        [UnityEngine.HideInInspector]
        [MemoryPackIgnore]
        public UnityEngine.GameObject ViewGO;
#endif
        private bool isDisposed;
        
        [StaticField]
        private static T instance;
        
        [StaticField]
        public static T Instance
        {
            get
            {
                return instance;
            }
            private set
            {
                instance = value;
            }
        }

        internal override void Register()
        {
            Instance = (T)this;
#if ENABLE_VIEW && UNITY_EDITOR
            if (Instance.GetType().Name == "Logger" || instance.GetType().Name == "Options")
            {
                return;
            }
            ViewGO = new UnityEngine.GameObject(Instance.GetType().Name);
            ViewGO.AddComponent<ASingletonView>().Singleton = this;
            ViewGO.transform.SetParent(UnityEngine.GameObject.Find("Global/World").transform);
#endif
        }

        public bool IsDisposed()
        {
            return this.isDisposed;
        }

        protected virtual void Destroy()
        {
            
        }

        public override void Dispose()
        {
            if (this.isDisposed)
            {
                return;
            }
            
            this.isDisposed = true;

            this.Destroy();
            
            Instance = null;

#if ENABLE_VIEW && UNITY_EDITOR
            if (ViewGO != null)
            {
                UnityEngine.Object.Destroy(ViewGO);
            }
#endif
        }
    }
}