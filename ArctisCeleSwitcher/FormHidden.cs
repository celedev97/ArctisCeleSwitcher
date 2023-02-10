using ArctisCeleSwitcher.Properties;
using AudioSwitcher.AudioApi.CoreAudio;
using HidLibrary;
using System.Data;
using System.Linq;

namespace ArctisCeleSwitcher {
    public partial class FormHidden : Form {

        public FormHidden() {
            InitializeComponent();
            setVisibility(false);

            // getting the audio devices
            GetDevices();

            //loading settings
            loadSettings();

            timerRefreshStatus.Enabled = true;
        }


        CoreAudioController audioController = new CoreAudioController();
        HidDevice? arctisHID;

        CoreAudioDevice? arctisPlayBackDevice;
        CoreAudioDevice? speakerPlaybackDevice;
        CoreAudioDevice? arctisRecordDevice;
        CoreAudioDevice? speakerRecordDevice;

        private List<KeyValuePair<string, CoreAudioDevice>> comboArctisPlaybackDataSource = new List<KeyValuePair<string, CoreAudioDevice>>();
        private List<KeyValuePair<string, CoreAudioDevice>> comboSpeakerPlaybackDataSource = new List<KeyValuePair<string, CoreAudioDevice>>();
        
        private List<KeyValuePair<string, CoreAudioDevice>> comboArctisCaptureDataSource = new List<KeyValuePair<string, CoreAudioDevice>>();
        private List<KeyValuePair<string, CoreAudioDevice>> comboSpeakerCaptureDataSource = new List<KeyValuePair<string, CoreAudioDevice>>();

        private void GetDevices() {
            #region //getting audio devices from windows
            var audioPlaybackDevices = audioController.GetPlaybackDevices()
                    .Where(device => device.State == AudioSwitcher.AudioApi.DeviceState.Active)
                    .ToList();

            var audioCaptureDevices = audioController.GetCaptureDevices()
                    .Where(device => device.State == AudioSwitcher.AudioApi.DeviceState.Active)
                    .ToList();

            comboArctisPlaybackDataSource = audioPlaybackDevices.Select(device => new KeyValuePair<string, CoreAudioDevice>(device.FullName, device)).ToList();
            comboSpeakerPlaybackDataSource = audioPlaybackDevices.Select(device => new KeyValuePair<string, CoreAudioDevice>(device.FullName, device)).ToList();

            comboArctisCaptureDataSource = audioCaptureDevices.Select(device => new KeyValuePair<string, CoreAudioDevice>(device.FullName, device)).ToList();
            comboSpeakerCaptureDataSource = audioCaptureDevices.Select(device => new KeyValuePair<string, CoreAudioDevice>(device.FullName, device)).ToList();

            comboPlaybackArctis.Items.Clear();
            comboPlaybackSpeaker.Items.Clear();
            
            
            comboPlaybackArctis.ValueMember = "Value";
            comboPlaybackArctis.DisplayMember = "Key";
            comboPlaybackSpeaker.ValueMember = "Value";
            comboPlaybackSpeaker.DisplayMember = "Key";
            comboRecordArctis.ValueMember = "Value";
            comboRecordArctis.DisplayMember = "Key";
            comboRecordSpeaker.ValueMember = "Value";
            comboRecordSpeaker.DisplayMember = "Key";

            comboPlaybackArctis.DataSource = comboArctisPlaybackDataSource;
            comboPlaybackSpeaker.DataSource = comboSpeakerPlaybackDataSource;
            comboRecordArctis.DataSource = comboArctisCaptureDataSource;
            comboRecordSpeaker.DataSource = comboSpeakerCaptureDataSource;
            #endregion

            //getting arctis hid device
            arctisHID = arctisHID != null ? arctisHID : ArctisHIDHelper.FindArctis();

            if (audioPlaybackDevices.Count < 2 || arctisHID == null) {
                MessageBox.Show("Error obtaining devices", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private ArctisStatus? lastStatus = null;
        private ArctisStatus? currentStatus = null;

        private void timerRefreshStatus_Tick(object sender, EventArgs e) {
            currentStatus = ArctisHIDHelper.ReadStatus(arctisHID!);

            var changed = lastStatus == null || lastStatus.online != currentStatus.online || lastStatus.battery != currentStatus.battery;

            if (changed) {
                if (currentStatus.online) {
                    if (currentStatus.battery >= 75) {
                        trayIcon.Icon = Resources.headphones_green;
                    } else if (currentStatus.battery >= 50) {
                        trayIcon.Icon = Resources.headphones_yellow;
                    } else {
                        trayIcon.Icon = Resources.headphones_red;
                    }
                } else {
                    trayIcon.Icon = Resources.speaker;
                }
                if (Settings.Default.AutoSwap) {
                    var playbackToSet = currentStatus.online ? arctisPlayBackDevice! : speakerPlaybackDevice!;
                    var microphoneToSet = currentStatus.online ? arctisRecordDevice! : speakerRecordDevice!;
                    if (audioController.DefaultPlaybackDevice.Id != playbackToSet?.Id) {
                        audioController.DefaultPlaybackDevice = playbackToSet;
                        audioController.DefaultPlaybackCommunicationsDevice = playbackToSet;
                    }
                    if (audioController.DefaultCaptureDevice.Id != microphoneToSet?.Id) {
                        audioController.DefaultCaptureDevice = microphoneToSet;
                        audioController.DefaultCaptureCommunicationsDevice = microphoneToSet;
                    }
                }
            }


            lastStatus = currentStatus;
        }

        private void save_Click(object sender, EventArgs e) {
            if (comboPlaybackArctis.SelectedValue == null ||
                comboPlaybackSpeaker.SelectedValue == null ||
                comboRecordArctis.SelectedValue == null ||
                comboRecordSpeaker.SelectedValue == null
                ) {
                MessageBox.Show("Select all devices", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboPlaybackArctis.SelectedValue == comboPlaybackSpeaker.SelectedValue) {
                MessageBox.Show("Set different devices for speakers and headphones", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Settings.Default.Arctis = ((CoreAudioDevice)comboPlaybackArctis.SelectedValue).Id;
            Settings.Default.Speakers = ((CoreAudioDevice)comboPlaybackSpeaker.SelectedValue).Id;
            Settings.Default.ArctisRecord = ((CoreAudioDevice)comboRecordArctis.SelectedValue).Id;
            Settings.Default.SpeakersRecord = ((CoreAudioDevice)comboRecordSpeaker.SelectedValue).Id;
            Settings.Default.AutoSwap = autoSwapCheckBox.Checked;

            Settings.Default.Save();
            MessageBox.Show("Settings saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            setVisibility(false);
        }
        
        //create a method for setting form visibility
        private void setVisibility(bool visibility, bool reload = true) {
            this.FormBorderStyle = visibility ? FormBorderStyle.FixedDialog : FormBorderStyle.None;
            this.Visible = visibility;
            this.Opacity = visibility ? 100 : 0;
            this.TopMost = visibility;
            this.ShowInTaskbar = visibility;

            //loading settings when the settings form is shown
            if (visibility && reload) {
                this.loadSettings();
            }
        }

        private void loadSettings() {
            var arctisGUID = Settings.Default.Arctis;
            var speakersGUID = Settings.Default.Speakers;
            var arctisRecordGUID = Settings.Default.ArctisRecord;
            var speakersRecordGUID = Settings.Default.SpeakersRecord;
            autoSwapCheckBox.Checked = Settings.Default.AutoSwap;

            //setting the selected devices
            var arctisToSet = comboArctisPlaybackDataSource.Where(kvp => kvp.Value.Id == arctisGUID).FirstOrDefault();
            var speakerToSet = comboSpeakerPlaybackDataSource.Where(kvp => kvp.Value.Id == speakersGUID).FirstOrDefault();
            var arctisRecordToSet = comboArctisCaptureDataSource.Where(kvp => kvp.Value.Id == arctisRecordGUID).FirstOrDefault();
            var speakerRecordToSet = comboSpeakerCaptureDataSource.Where(kvp => kvp.Value.Id == speakersRecordGUID).FirstOrDefault();

            comboPlaybackArctis.SelectedItem = arctisToSet;
            comboPlaybackSpeaker.SelectedItem = speakerToSet;
            comboRecordArctis.SelectedItem = arctisRecordToSet;
            comboRecordSpeaker.SelectedItem = speakerRecordToSet;

            if (arctisToSet.Value == null || speakerToSet.Value == null || arctisRecordToSet.Value == null || speakerRecordToSet.Value == null) {
                setVisibility(true, false);
                MessageBox.Show(this, "Please select your devices", "Arctis Cele Switcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else {
                arctisPlayBackDevice = arctisToSet.Value;
                speakerPlaybackDevice = speakerToSet.Value;
                arctisRecordDevice = arctisRecordToSet.Value;
                speakerRecordDevice = speakerRecordToSet.Value;
            }

        }

        private void impostazioniToolStripMenuItem_Click(object sender, EventArgs e) {
            setVisibility(true);
        }

        private void esciToolStripMenuItem_Click(object sender, EventArgs e) {
            arctisHID?.CloseDevice();
            Application.Exit();
        }

        private void FormHidden_FormClosing(object sender, FormClosingEventArgs e) {
            if (Visible) {
                e.Cancel = true;
                setVisibility(false);
            }
        }

        private void trayMenu_Opened(object sender, EventArgs e) {
            statusLabel.Text = "Headset: "+(currentStatus?.online != null ? (currentStatus!.online ? "ON ":"OFF") : " ? ") + " Battery: " + (currentStatus?.battery.ToString() ?? " ? ") + "%";
        }
    }
}