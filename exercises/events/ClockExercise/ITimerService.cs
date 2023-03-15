using System;

namespace ClockExercise
{
    public interface ITimerService
    {
        event Action Tick;
    }

    public class Clock
    {
        public int seconds;
        public int minutes = 60;
        public int hours = 60*60;
        public int days = 24*60*60;

        public Clock(ITimerService timer)
        {
            timer.Tick += this.OnTick;
        }

        public void OnTick() {
            seconds++;
        }

        public void secondsNotify(int seconds)
        {
            
        }

        public void minutes(int minutes)
        {

        }





    }
}