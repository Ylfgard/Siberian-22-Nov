using UnityEngine;
using TMPro;

namespace GameControllers
{
    public class EndGameTimer : MonoBehaviour
    {
        public event SendEvent TimesOut;

        [SerializeField] private bool _endlessLevel;
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private float _timeLimitInSeconds;

        private float _time;
        private bool _timerStoped;

        private void Awake()
        {
            if (_endlessLevel)
                FindObjectOfType<ScoreCounter>().ScoreChanged += ChangeRestTime;
        }

        private void Start()
        {
            SetupTimer(_timeLimitInSeconds);
        }

        private void ChangeRestTime(int time)
        {
            _time += time;
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
            {
                _timerStoped = true;
                TimesOut?.Invoke();
            }
        }
    }
}