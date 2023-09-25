using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Animal_Sounds
{
    abstract class CAnimal
    {
        public abstract void MakeSound();
    }


    class Cat : CAnimal
    {
        public override void MakeSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(Animal_Sounds.Form1.projectPath);
            simpleSound.Play();
        }
    }
    class Chimpanzee : CAnimal
    {
        public override void MakeSound()
        {
            SoundPlayer simpleSound = new SoundPlayer();
            simpleSound.Play();
        }
    }
    class Cow : CAnimal
    {
        public override void MakeSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\chimes.wav");
            simpleSound.Play();
        }
    }
    class Dog : CAnimal
    {
        public override void MakeSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\chimes.wav");
            simpleSound.Play();
        }
    }

    class Horse : CAnimal
    {
        public override void MakeSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\chimes.wav");
            simpleSound.Play();
        }
    }
    class Rattlesnake : CAnimal
    {
        public override void MakeSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\chimes.wav");
            simpleSound.Play();
        }
    }
    class Rooster : CAnimal
    {
        public override void MakeSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\chimes.wav");
            simpleSound.Play();
        }
    }
    class Sheep : CAnimal
    {
        public override void MakeSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\chimes.wav");
            simpleSound.Play();
        }
    }
    class Wolf : CAnimal
    {
        public override void MakeSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\chimes.wav");
            simpleSound.Play();
        }
    }
}
