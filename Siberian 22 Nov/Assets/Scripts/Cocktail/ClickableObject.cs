using UnityEngine;

namespace Cocktails
{
    public delegate void SendString(int i);

    public class ClickableObject : MonoBehaviour
    {
        private SendString _callback;
        private event SendString _clicked;
        private int _result;

        public void InitializeClick(int result, SendString callback)
        {
             _result = result;
            _callback = callback;
            _clicked += callback;
        }

        private void OnMouseDown()
        {
            _clicked?.Invoke(_result);
        }

        private void OnDisable()
        {
            _clicked -= _callback;
        }
    }
}