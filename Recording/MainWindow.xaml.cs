using System;
using System.Windows;

namespace Recording
{
    public partial class MainWindow : Window
    {
        private readonly RecordingManager recordingManager;
        private bool isRecording = false;

        public MainWindow()
        {
            InitializeComponent();
            DatabaseHelper databaseHelper = new DatabaseHelper("Data Source=your_database_file.db");
            recordingManager = new RecordingManager(databaseHelper);
        }

        private void RecordButton_Click(object sender, RoutedEventArgs e)
        {
            if (isRecording)
            {
                recordingManager.StopRecording();
                RecordButton.Content = "Record";
            }
            else
            {
                recordingManager.StartRecording();
                RecordButton.Content = "Stop";
            }

            isRecording = !isRecording;
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            // Example: Play audio with ID 1
            try
            {
                recordingManager.PlayAudio(1);
                MessageBox.Show("Playing audio...");
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

