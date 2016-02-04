using UnityEditor;
using UnityEngine;

namespace SpriteAnimationPreview
{
    public class TimeControl
    {
        public bool isPlaying { get; private set; }

        protected float currentTime { get; set; }

        private double lastFrameEditorTime { get; set; }

        public float speed { get; set; }

        public TimeControl()
        {
            speed = 1;
            EditorApplication.update += Update;
        }

        public void Update()
        {
            if (isPlaying)
            {
                var timeSinceStartup = EditorApplication.timeSinceStartup;
                var deltaTime = timeSinceStartup - lastFrameEditorTime;
                lastFrameEditorTime = timeSinceStartup;
                currentTime += (float)deltaTime * speed;
            }
        }

        public float GetCurrentTime(float startTime, float stopTime)
        {
            var _currentTime = Mathf.Repeat(currentTime, stopTime);
            _currentTime = Mathf.Clamp(_currentTime, startTime, stopTime);
            return _currentTime;
        }

        public void Play()
        {
            isPlaying = true;
            lastFrameEditorTime = EditorApplication.timeSinceStartup;
        }

        public void Pause()
        {
            isPlaying = false;
        }
    }
}