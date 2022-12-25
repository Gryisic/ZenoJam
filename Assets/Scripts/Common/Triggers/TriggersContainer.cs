using System.Collections.Generic;
using UnityEngine;

namespace ZenoJam.Common
{
    [System.Serializable]
    public class TriggersContainer
    {
        [SerializeField] private List<Trigger> _triggers;

        public IEnumerable<Trigger> Triggers => _triggers;
    }
}