using UnityEngine;

namespace WorkShop.Utils.Timer
{
    public class Timer:ITimer
    {
        private float delay;
        private float lastTime;

        public bool IsFinished => lastTime < Time.time;

        public void SetTimer(float delay)
        {
            this.delay = delay;
        }

        public void Start()
        {
            lastTime = Time.time + delay;
        }
    }
}