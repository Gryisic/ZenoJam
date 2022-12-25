using UnityEngine;
using static ZenoJam.Utils.Enums;

namespace ZenoJam.Common
{
    public class MovingStrategy 
    {
        public Vector2 GetDestination(MoveType type, Vector2 currentPosition) 
        {
            switch (type)
            {
                case MoveType.None:
                    return Vector2.zero;

                case MoveType.Linear:
                    return LinearDestination(currentPosition);
            }

            return Vector2.zero;
        }

        private Vector2 LinearDestination(Vector2 currentPosition) 
        {
            var distance = Random.Range(currentPosition.x - 10f, currentPosition.x + 10f);

            Vector2 direction = new Vector2(distance, currentPosition.y);

            var raycastHit = Physics2D.Linecast(currentPosition, direction, 1 << LayerMask.NameToLayer("Ground"));

            if (raycastHit)
                return raycastHit.point;

            return direction;
        }
    }
}
