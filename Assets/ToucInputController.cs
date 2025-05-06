using UnityEngine;

namespace DiceGame.Mechanics.TouchInput
{
    public class TouchInputController : MonoBehaviour
    {
        public event System.Action<Vector2> OnTouchBegin;
        public event System.Action<Vector2> OnTouchMove;
        public event System.Action OnTouchEnd;

        private void Update()
        {
            if (Input.touchCount == 0) return;

            foreach (Touch touch in Input.touches)
            {
                if (touch.position.y >= Screen.height / 2) continue;

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        OnTouchBegin?.Invoke(touch.position);
                        break;
                    case TouchPhase.Moved:
                        OnTouchMove?.Invoke(touch.position);
                        break;
                    case TouchPhase.Ended:
                        OnTouchEnd?.Invoke();
                        break;
                }
            }
        }
    }
}
