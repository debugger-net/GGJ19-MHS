﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace MHS.Player.Audio
{
    internal class AudioPlaybackEngine : IDisposable
    {
        private readonly IWavePlayer outputDevice;
        private readonly MixingSampleProvider mixer;

        public AudioPlaybackEngine(int sampleRate = 44100, int channelCount = 2)
        {
            outputDevice = new WaveOutEvent();
            mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount));
            mixer.ReadFully = true;
            outputDevice.Init(mixer);
            outputDevice.Play();
        }

        public void PlaySound(string fileName)
        {
            var input = new AudioFileReader(fileName);
            AddMixerInput(new AutoDisposeFileReader(input));
        }

        private ISampleProvider ConvertToRightChannelCount(ISampleProvider input)
        {
            if (input.WaveFormat.Channels == mixer.WaveFormat.Channels)
            {
                return input;
            }
            if (input.WaveFormat.Channels == 1 && mixer.WaveFormat.Channels == 2)
            {
                return new MonoToStereoSampleProvider(input);
            }
            throw new NotImplementedException("Not yet implemented this channel count conversion");
        }

        public void PlaySound(CachedSound sound)
        {
            AddMixerInput(new CachedSoundSampleProvider(sound));
        }

        public SoundSourceHandle PlayHandledSound(CachedSound sound)
        {
            return AddMixerInputWithHandle(new CachedSoundSampleProvider(sound));
        }

        public struct SoundSourceHandle
        {
            public CachedSoundSampleProvider    sourceProvider;
            public ISampleProvider              insertedProvider;
        }

        private void AddMixerInput(ISampleProvider input)
        {
            mixer.AddMixerInput(ConvertToRightChannelCount(input));
        }

        private SoundSourceHandle AddMixerInputWithHandle(CachedSoundSampleProvider input)
        {
            SoundSourceHandle resultData = new SoundSourceHandle();
            resultData.sourceProvider = input;
            resultData.insertedProvider = ConvertToRightChannelCount(input);

            mixer.AddMixerInput(ConvertToRightChannelCount(input));

            return resultData;
        }

        public void RemoveSoundFromMixer(SoundSourceHandle handle)
        {
            mixer.RemoveMixerInput(handle.insertedProvider);
        }

        public void Dispose()
        {
            outputDevice.Dispose();
        }

        public static readonly AudioPlaybackEngine Instance = new AudioPlaybackEngine(44100, 2);
    }


    internal class CachedSound
    {
        public float[] AudioData { get; private set; }
        public WaveFormat WaveFormat { get; private set; }
        public CachedSound(string audioFileName, bool isLoop = false)
        {
            IsLoop = isLoop;

            using (var audioFileReader = new AudioFileReader(audioFileName))
            {
                WaveFormat = audioFileReader.WaveFormat;
                var wholeFile = new List<float>((int)(audioFileReader.Length / 4));
                var readBuffer = new float[audioFileReader.WaveFormat.SampleRate * audioFileReader.WaveFormat.Channels];
                int samplesRead;
                while ((samplesRead = audioFileReader.Read(readBuffer, 0, readBuffer.Length)) > 0)
                {
                    wholeFile.AddRange(readBuffer.Take(samplesRead));
                }
                AudioData = wholeFile.ToArray();
            }
        }

        public bool IsLoop { get; private set; }
    }


    internal class CachedSoundSampleProvider : ISampleProvider
    {
        private readonly CachedSound cachedSound;
        private long position;

        public CachedSoundSampleProvider(CachedSound cachedSound)
        {
            this.cachedSound = cachedSound;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            var availableSamples = cachedSound.AudioData.Length - position;
            var samplesToCopy = Math.Min(availableSamples, count);
            Array.Copy(cachedSound.AudioData, position, buffer, offset, samplesToCopy);
            position += samplesToCopy;
            if (cachedSound.IsLoop && position >= cachedSound.AudioData.Length)
            {
                position = 0;
                count -= (int)samplesToCopy;
                offset += (int)samplesToCopy;
                availableSamples = cachedSound.AudioData.Length - position;
                var newSamplesToCopy = Math.Min(availableSamples, count);
                Array.Copy(cachedSound.AudioData, position, buffer, offset, newSamplesToCopy);
                position += newSamplesToCopy;
                samplesToCopy += newSamplesToCopy;
            }
            return (int)samplesToCopy;
        }

        public bool IsFinished { get { return position >= cachedSound.AudioData.Length ; } }

        public WaveFormat WaveFormat { get { return cachedSound.WaveFormat; } }
    }


    class AutoDisposeFileReader : ISampleProvider
    {
        private readonly AudioFileReader reader;
        private bool isDisposed;
        public AutoDisposeFileReader(AudioFileReader reader)
        {
            this.reader = reader;
            this.WaveFormat = reader.WaveFormat;
        }

        public int Read(float[] buffer, int offset, int count)
        {
            if (isDisposed)
                return 0;
            int read = reader.Read(buffer, offset, count);
            if (read == 0)
            {
                reader.Dispose();
                isDisposed = true;
            }
            return read;
        }

        public WaveFormat WaveFormat { get; private set; }
    }
}
