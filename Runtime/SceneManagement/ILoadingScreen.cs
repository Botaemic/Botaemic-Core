using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Botaemic.SceneManagement
{
    public abstract class ILoadingScreen : MonoBehaviour
    {
        public abstract bool ShowLoadingScreen { set; }
        public abstract float Progress { get; set; }
    }
}