
using CSCore;
using CSCore.Codecs.WAV;
using CSCore.SoundIn;
using CSCore.SoundOut;
using System;
using System.IO;
using System.Windows;

public class RecordingManager
{
    private WasapiCapture capture;
    private WaveWriter waveWriter;
    private string filePath;
    private readonly DatabaseHelper databaseHelper;

    public RecordingManager(DatabaseHelper databaseHelper)
    {
        this.databaseHelper = databaseHelper;
    }

    public void StartRecording()
    {
        string fileName = $"recorded_audio_{DateTime.Now:yyyyMMdd_HHmmss}.wav";
        filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

        if (capture != null)
        {
            capture.Dispose();
        }

        capture = new WasapiCapture();
        capture.Initialize();

        waveWriter = new WaveWriter(filePath, capture.WaveFormat);
        capture.DataAvailable += (s, e) =>
        {
            waveWriter.Write(e.Data, e.Offset, e.ByteCount);
        };

        capture.Start();
    }

    public void StopRecording()
    {
        
        if (capture != null)
        {
            capture.Stop();
            capture.Dispose();
            capture = null;
        }

      
        if (waveWriter != null)
        {
            waveWriter.Dispose();
            waveWriter = null;
        }

        
        if (File.Exists(filePath))
        {
            byte[] audioData = File.ReadAllBytes(filePath); 
            string fileName = Path.GetFileName(filePath); 

            
            databaseHelper.InsertRecording(fileName, audioData);

            MessageBox.Show("Recording saved to " + filePath);
        }
        else
        {
            MessageBox.Show("Error: File not found.");
        }
    }

    public void PlayAudio(int recordingId)
    {
        byte[] audioData = databaseHelper.GetAudioById(recordingId);
        if (audioData == null)
        {
            throw new InvalidOperationException("No audio data found.");
        }

        using (var memoryStream = new MemoryStream(audioData))
        using (var waveStream = new WaveFileReader(memoryStream))
        using (var waveOut = new WaveOut())
        {
            waveOut.Initialize(waveStream);
            waveOut.Play();
            while (waveOut.PlaybackState == PlaybackState.Playing)
            {
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}

