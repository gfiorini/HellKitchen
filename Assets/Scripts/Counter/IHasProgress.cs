
using System;


public interface IHasProgress 
{
    public event EventHandler<OnProgressChangeArgs> OnProgressChange;
    public class OnProgressChangeArgs : EventArgs
    {
        public float progressNormalized;
    }
}