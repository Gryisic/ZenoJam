using UnityEngine;
using static ZenoJam.Utils.Enums;

namespace ZenoJam.Common
{
    public class SpawnMarker : MonoBehaviour 
    {
        [SerializeField] private UnitType _type;

        public UnitType Type => _type;
    }
}
