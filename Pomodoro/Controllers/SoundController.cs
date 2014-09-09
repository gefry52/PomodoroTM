using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;

namespace Pomodoro.Controllers
{
    class SoundController
    {
        private static SoundPlayer _player = new SoundPlayer();
       

        public static void Play (bool loop)
        {
            SystemSounds.Asterisk.Play();
        }

    }
}
