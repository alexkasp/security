using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using SandBox.Msg;

namespace SandBox.Db
{
    public class MessageAnalyzer
    {
        public static void AnalyzeMessage(Message message)
        {
            switch (message.Type)
            {
                case MessageType.INITIAL: OnInitialMessage(null); break;
                case MessageType.VM_START: OnVmStatusMessage(message.GetParameters()); break;
                case MessageType.VM_STOP: OnVmStatusMessage(message.GetParameters()); break;
                case MessageType.VM_STATUS: OnVmStatusMessage(message.GetParameters()); break;
                case MessageType.VM_CREATE: OnVmCreateMessage(message.GetParameters()); break;
                case MessageType.VM_DELETE: OnVmDeleteMessage(message.GetParameters()); break;
                case MessageType.MALWARE_LOAD: OnMalwareLoad(message.GetParameters()); break;
                case MessageType.OBJECT_FOLLOW: OnObjectFolow(message.GetParameters()); break;
            }
        }

        private static void OnInitialMessage(List<byte[]> parameters)
        {
        }

        /*private static void OnVmStartMessage(List<byte[]> parameters)
        {
            if (parameters.Count != 2) return;

            String name = Encoding.UTF8.GetString(parameters[0], 0, parameters[0].Length);
            Byte stateByte = parameters[1][0];
            Int32 state = -1;

            switch (stateByte)
            {
                case 0xF0:
                    state = 1;
                    break;
                case 0xF1:
                    state = 0;
                    break;
            }

            MachineManager.UpdateState(name, state);
        }

        private static void OnVmStopMessage(List<byte[]> parameters)
        {
            if (parameters.Count != 2) return;

            String name = Encoding.UTF8.GetString(parameters[0], 0, parameters[0].Length);
            Byte stateByte = parameters[1][0];
            Int32 state = -1;

            switch (stateByte)
            {
                case 0xF0:
                    state = 1;
                    break;
                case 0xF1:
                    state = 0;
                    break;
            }

            MachineManager.UpdateState(name, state);
        }*/

        private static void OnVmStatusMessage(List<byte[]> parameters)
        {
            if (parameters.Count != 2) return;

            String name = Encoding.UTF8.GetString(parameters[0], 0, parameters[0].Length);
            Byte stateByte = parameters[1][0];
            Int32 state = -1;

            switch (stateByte)
            {
                case 0xF0:
                    state = 1;
                    break;
                case 0xF1:
                    state = 0;
                    break;
            }

            //MachineManager.UpdateState(name, state);
            Debug.Print("Status: " + name + " | " + state);
        }

        private static void OnVmCreateMessage(List<byte[]> parameters)
        {
        }

        private static void OnVmDeleteMessage(List<byte[]> parameters)
        {
        }

        private static void OnMalwareLoad(List<byte[]> parameters)
        {
        }

        private static void OnObjectFolow(List<byte[]> parameters)
        {
        }
    }//end class
}//end namespace
