using SecurityConsultantCore.EngineInterfaces;

namespace SecurityConsultantCore.Test.EngineMocks
{
    public class SoundMock : ISound
    {
        public bool Played { get; private set; }
        public bool Stopped { get; private set; }

        public void Play()
        {
            Played = true;
        }

        public void Stop()
        {
            Stopped = true;
        }
    }
}
