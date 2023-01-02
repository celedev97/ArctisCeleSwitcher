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
        
        CoreAudioDevice? arctisDevice;
        CoreAudioDevice? speakerDevice;

        private List<KeyValuePair<string, CoreAudioDevice>> comboArctisDataSource = new List<KeyValuePair<string, CoreAudioDevice>>();
        private List<KeyValuePair<string, CoreAudioDevice>> comboSpeakerDataSource = new List<KeyValuePair<string, CoreAudioDevice>>();

        private void GetDevices() {
            #region //getting audio devices from windows
            var audioDevices = audioController.GetPlaybackDevices()
                    .Where(device => device.State == AudioSwitcher.AudioApi.DeviceState.Active)
                    .ToList();

            comboArctisDataSource = audioDevices.Select(device => new KeyValuePair<string, CoreAudioDevice>(device.FullName, device)).ToList();
            comboSpeakerDataSource = audioDevices.Select(device => new KeyValuePair<string, CoreAudioDevice>(device.FullName, device)).ToList();

            comboArctis.Items.Clear();
            comboSpeaker.Items.Clear();
            
            comboArctis.ValueMember = "Value";
            comboArctis.DisplayMember = "Key";
            comboSpeaker.ValueMember = "Value";
            comboSpeaker.DisplayMember = "Key";

            comboArctis.DataSource = comboArctisDataSource;
            comboSpeaker.DataSource = comboSpeakerDataSource;
            #endregion

            //getting arctis hid device
            arctisHID = arctisHID != null ? arctisHID : ArctisHIDHelper.FindArctis();

            if (audioDevices.Count < 2 || arctisHID == null) {
                MessageBox.Show("Error obtaining devices", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private ArctisStatus? lastStatus = null;
        private ArctisStatus? currentStatus = null;

        private void timerRefreshStatus_Tick(object sender, EventArgs e) {
            currentStatus = ArctisHIDHelper.ReadStatus(arctisHID!);

            var changed = lastStatus == null || lastStatus.online != currentStatus.online;

            if (changed) {
                trayIcon.Icon = currentStatus.online ? Resources.headphones : Resources.speaker;
                if (Settings.Default.AutoSwap) {
                    var deviceToSet = currentStatus.online ? arctisDevice! : speakerDevice!;
                    if (audioController.DefaultPlaybackDevice.Id != deviceToSet?.Id) {
                        audioController.DefaultPlaybackDevice = deviceToSet;
                    }
                }
            }


            lastStatus = currentStatus;
        }

        private void save_Click(object sender, EventArgs e) {
            if (comboArctis.SelectedValue == null || comboSpeaker.SelectedValue == null) {
                MessageBox.Show("Select both devices", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboArctis.SelectedValue == comboSpeaker.SelectedValue) {
                MessageBox.Show("Set different devices for speakers and headphones", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Settings.Default.Arctis = ((CoreAudioDevice)comboArctis.SelectedValue).Id;
            Settings.Default.Speakers = ((CoreAudioDevice)comboSpeaker.SelectedValue).Id;
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
            autoSwapCheckBox.Checked = Settings.Default.AutoSwap;

            //setting the selected devices
            var arctisToSet = comboArctisDataSource.Where(kvp => kvp.Value.Id == arctisGUID).FirstOrDefault();
            var speakerToSet = comboSpeakerDataSource.Where(kvp => kvp.Value.Id == speakersGUID).FirstOrDefault();

            comboArctis.SelectedItem = arctisToSet;
            comboSpeaker.SelectedItem = speakerToSet;

            if (arctisToSet.Value == null || speakerToSet.Value == null) {
                setVisibility(true, false);
                MessageBox.Show(this, "Please select your devices", "Arctis Cele Switcher", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else {
                arctisDevice = arctisToSet.Value;
                speakerDevice = speakerToSet.Value;
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
            e.Cancel = true;
            setVisibility(false);
        }

        private void trayMenu_Opened(object sender, EventArgs e) {
            statusLabel.Text = "Headset: "+(currentStatus?.online != null ? (currentStatus!.online ? "ON ":"OFF") : " ? ") + " Battery: " + (currentStatus?.battery.ToString() ?? " ? ") + "%";
        }
    }
}