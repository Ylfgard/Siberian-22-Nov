using UnityEngine;

namespace Cocktails
{
    public delegate void SendString(int i);

    public class ClickableObject : MonoBehaviour
    {
        private event SendString _clicked;
        private int _result;

        public void InitializeClick(int result, SendString callback)
        {
             _result = result;
            _clicked += callback;
        }

        private void OnMouseDown()
        {
            _clicked.Invoke(_result);
        }
    }
}