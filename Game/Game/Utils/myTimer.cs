using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;
using System.Globalization;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game
{
  /// <summary>
  /// asynchroní timer - v milisekundách, timer v xamarinu má nedostatečnou funkčnost, ostatní timery nefungují nebo nemají dostatečnou multiplatformní podporu
  /// </summary>
  public class myTimer
  {

    private int _waitTime;
    int ms = 0;
    public int WaitTime
    {
      get { return _waitTime; }
      set { _waitTime = value; }
    }

    private bool _isRunning;
    public bool IsRunning
    {
      get { return _isRunning; }
      set { _isRunning = value; }
    }

    public event EventHandler Elapsed;
    protected virtual void OnTimerElapsed()
    {
      if (Elapsed != null)
      {
        Elapsed(this, new EventArgs());
      }
    }

    public myTimer(int waitTime)
    {
      WaitTime = waitTime;
    }

    public async Task Start()
    {
      ms = 0;
      IsRunning = true;
      while (IsRunning)
      {
        if (ms != 0 && ms % WaitTime == 0) //pokaždá když probhne vyžadovaný počet
        {
          OnTimerElapsed();
        }
        await Task.Delay(5);  //1ms příliš krátká
        ms++;
      }
    }

    public void Stop()
    {
      IsRunning = false;
      ms = 0;
    }
  }
}
