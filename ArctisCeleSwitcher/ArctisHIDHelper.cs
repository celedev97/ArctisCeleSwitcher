using AudioSwitcher.AudioApi;
using HidLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ArctisCeleSwitcher {
    internal static class ArctisHIDHelper {


        const int MSG_SIZE = 64;

        const int VENDOR_STEELSERIES = 0x1038;

        const int ID_ARCTIS_NOVA_7 = 0x2202;
        const int ID_ARCTIS_NOVA_7x = 0x2206;
        const int ID_ARCTIS_NOVA_7p = 0x220a;

        const int BATTERY_MAX = 0x04;
        const int BATTERY_MIN = 0x00;

        const int HEADSET_OFFLINE = 0x00;

        const int EQUALIZER_BANDS_SIZE = 10;

        //define an array with all the product ids
        private static int[] productIds = { ID_ARCTIS_NOVA_7, ID_ARCTIS_NOVA_7x, ID_ARCTIS_NOVA_7p };

        private static byte[] ASK_STATUS_MESSAGE = { 0x00, 0xb0 };

        //function for finding the device
        public static HidDevice? FindArctis() {
            var device = HidDevices.Enumerate(VENDOR_STEELSERIES, productIds)
                .Where(device => ((UInt16)device.Capabilities.UsagePage == (UInt16)65472))
                .First();

            device?.OpenDevice();

            return device;
        }

        
        // create a function to read the status of the headset or an error
        
        public static ArctisStatus ReadStatus(HidDevice arctis, ArctisStatus? status = null) {
            arctis.Write(ASK_STATUS_MESSAGE);

            var read = arctis.Read();
            var statusBytes = read.Data;

            if (status == null) {
                status = new ArctisStatus();
            }

            status.online = statusBytes[4] != HEADSET_OFFLINE;
            status.battery = (byte) ((statusBytes[3] - BATTERY_MIN) * 100 / BATTERY_MAX);
            status.game = statusBytes[5];
            status.chat = statusBytes[6];

            if (!status.online) {
                status.game = 0;
                status.chat = 0;
            }

            return status;

        }
    }

    internal class ArctisStatus {
        public bool online;
        public byte battery;
        public byte game;
        public byte chat;

        //add tostring method
        public override string ToString() {
            return "[on: "+online+" battery: "+battery+"% game/chat:"+game+"/"+chat+"]";
        }
    }

}
    