using System.Collections.Generic;
using UnityEngine;
using ZenoJam.Common;
using System.Linq;

namespace ZenoJam.Core
{
    public class UIContext : MonoBehaviour 
    {
        [SerializeField] private List<UIElement> _uiElements;

        public T Resolve<T>() where T : UIElement => (T) _uiElements.First(element => element is T);
    }
}