using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Note.Solution.Admin
{
    public abstract class View : MonoBehaviour {
        public abstract void Init();
        public abstract void Enter(Transform, object);
        public abstract void Leave();
        public abstract void CallUp();
        public abstract void CallDown();
        public abstract void CallLeft();
        public abstract void CallRight();
        public abstract void CallOk();
        public abstract void CallQuit();
    }
}