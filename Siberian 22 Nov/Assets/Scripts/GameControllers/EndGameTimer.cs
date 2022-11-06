using UnityEngine;
using TMPro;

namespace GameControllers
{
    public class EndGameTimer : MonoBehaviour
    {
        public event SendEvent TimesOut;

        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private float _timeLimitInSeconds;

        private float _time;
        private bool _timerStoped;

        private void Start()
        {
            SetupTimer(_timeLimitInSeconds);
        }

        public void SetupTimer(float timeLimit)
        {
            _timeLimitInSeconds = timeLimit;
            _timerText.text = _timeLimitInSeconds.ToString();
            _time = _timeLimitInSeconds;
            _timerStoped = false;
        }

        private void Update()
        {
            if (_timerStoped) return;

            _time -= Time.deltaTime;
            _timerText.text = Mathf.RoundToInt(_time).ToString();
            if (_time <= 0)
                TimesOut?.Invoke();
        }
    }
}