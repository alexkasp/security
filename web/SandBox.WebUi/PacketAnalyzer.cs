using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SandBox.Connection;
using SandBox.Data;
using SandBox.Db;
using SandBox.Log;
using SandBox.WebUi.Pages.Information;
using SandBox.WebUi.Pages.Research;


namespace SandBox.WebUi

{
    public class PacketAnalyzer
    {
        private static ConnectionClientEx _client;

        private static void GetVmStatus(String machineName)
        {
            MLogger.LogTo(Level.TRACE, false, "Get status for " + machineName);
            Packet packet = new Packet { Type = PacketType.CMD_VM_STATUS, Direction = PacketDirection.REQUEST };
            packet.AddParameter(Encoding.UTF8.GetBytes(machineName));
            _client.Send(packet.ToByteArray());
        }

        public static void AnalyzeReceived(Packet packet, ConnectionClientEx client)
        {
            _client = client;
            switch (packet.Type)
            {
                case PacketType.ANS_VM_START:       OnReceiveVmStart(packet.GetParameters()); break;
                case PacketType.ANS_VM_STOP:        OnReceiveVmStop(packet.GetParameters()); break;
                case PacketType.ANS_VM_STATUS:      OnReceiveVmStatus(packet.GetParameters()); break;
                case PacketType.ANS_VM_CREATE:      OnReceiveVmCreate(packet.GetParameters()); break;
                case PacketType.ANS_LOAD_MALWARE:   OnReceiveMalwareLoad(packet.GetParameters()); break;
                case PacketType.ANS_SET_TARGET:     OnReceiveSetTarget(packet.GetParameters()); break;
                case PacketType.ANS_REPORT:         OnReceiveReport(packet.GetParameters()); break;
                case PacketType.ANS_SET_OBJECT:     OnReceiveSetObject(packet.GetParameters()); break;
                case PacketType.ANS_VM_READY:       OnReceiveVmReady(packet.GetParameters()); break;
                case PacketType.ANS_VM_COMPLETE:    OnReceiveVmComplete(packet.GetParameters()); break;
                case PacketType.ANS_LOAD_TRAFFIC:   OnReceiveLoadTraffic(packet.GetParameters()); break;
                case PacketType.ANS_VM_NEWCREATE: OnReceiveVmCreateEvent(packet.GetParameters()); break;
            }
        }

        private static void OnReceiveVmStart(IList<byte[]> parameters)
        {
            try
            {
                MLogger.LogTo(Level.TRACE, false, "[ANS_VM_START] received");
                
                String machineName  = Encoding.UTF8.GetString(parameters[0], 0, parameters[0].Length);
                Byte   status       = parameters[1][0];

                Vm vm = VmManager.GetVm(machineName);
                if (vm == null) return;

                switch (status)
                {
                    case 0xFE: VmManager.UpdateVmState(machineName, (Int32)VmManager.State.ERROR); break;
                    case 0xFA: VmManager.UpdateVmState(machineName, (Int32)VmManager.State.STARTED); break;
                }

                GetVmStatus(machineName);
            }
            catch (Exception)
            {
                MLogger.LogTo(Level.WARNING, false, "OnReceiveVmStart, catch");
            }
        }

        private static void OnReceiveVmStop(IList<byte[]> parameters)
        {
            try
            {
                MLogger.LogTo(Level.TRACE, false, "[ANS_VM_STOP] received");
                
                String machineName = Encoding.UTF8.GetString(parameters[0], 0, parameters[0].Length);
                Byte status = parameters[1][0];

                Vm vm = VmManager.GetVm(machineName);
                if (vm == null) return;

                switch (status)
                {
                    case 0xFE: VmManager.UpdateVmState(machineName, (Int32)VmManager.State.ERROR); break;
                    case 0xFA: VmManager.UpdateVmState(machineName, (Int32)VmManager.State.STOPPED); break;
                }

                GetVmStatus(machineName);
            }
            catch (Exception)
            {
                MLogger.LogTo(Level.WARNING, false, "OnReceiveVmStop, catch");
            }  
        }

        private static void OnReceiveVmStatus(IList<byte[]> parameters)
        {
            try
            {
                MLogger.LogTo(Level.TRACE, false, "[ANS_VM_STATUS] received");
                
                String machineName = Encoding.UTF8.GetString(parameters[0], 0, parameters[0].Length);
                Byte status = parameters[1][0];
                Byte[] mac  = parameters[2];

                Vm vm = VmManager.GetVm(machineName);
                if (vm == null) return;

                switch (status)
                {
                    case 0xF0: 
                        VmManager.UpdateVmState(machineName, (Int32)VmManager.State.STARTED); break;
                    case 0xF1: 
                        VmManager.UpdateVmState(machineName, (Int32)VmManager.State.STOPPED); 
                        VmManager.ResetEnvData(vm.EnvId);
                        break;
                    case 0xF9: 
                        VmManager.UpdateVmState(machineName, (Int32)VmManager.State.UNAVAILABLE);
                        VmManager.ResetEnvData(vm.EnvId);
                        break;
                    case 0xFE: 
                        VmManager.UpdateVmState(machineName, (Int32)VmManager.State.ERROR);
                        VmManager.ResetEnvData(vm.EnvId);
                        break;
                }

                VmManager.UpdateVmEnvMac(vm.Id, DataUtils.ByteArrayToHexString(mac));
            }
            catch (Exception)
            {
                MLogger.LogTo(Level.WARNING, false, "OnReceiveVmStatus, catch");
            }  
        }

        private static void OnReceiveVmCreate(List<byte[]> parameters)
        {
        }

        private static void OnReceiveMalwareLoad(List<byte[]> parameters)
        {
        }

        private static void OnReceiveSetTarget(List<byte[]> parameters)
        {
        }

        private static void OnReceiveSetObject(List<byte[]> parameters)
        {
        }

        
        //Отчет о готовности ИС к работе
        private static void OnReceiveVmReady(IList<byte[]> parameters)
        {
            try
            {
                MLogger.LogTo(Level.TRACE, false, "[ANS_VM_READY] received");

                Byte[] vmReadyStr = parameters[0];
                
                Int32  envId = Convert.ToInt32(vmReadyStr[0]);
                String envIp = Convert.ToInt32(vmReadyStr[2]) + "." + Convert.ToInt32(vmReadyStr[3]) + "." + Convert.ToInt32(vmReadyStr[4]) + "." + Convert.ToInt32(vmReadyStr[5]);
                String envMac = DataUtils.ByteArrayToHexString(new[] { vmReadyStr[6], vmReadyStr[7], vmReadyStr[8], vmReadyStr[9], vmReadyStr[10], vmReadyStr[11] });

                Vm vm = VmManager.GetVmByMac(envMac);
                if (vm == null) return;
                VmManager.UpdateEnvData(vm.Id, envId, envIp);

                Research r = ResearchManager.GetResearchByVmName(vm.Name);
                if (r != null)
                {
                    Current.StartResearch(String.Format("{0}", r.Id));
                }
               
            }
            catch (Exception)
            {
                MLogger.LogTo(Level.WARNING, false, "OnReceiveVmReady, catch");
            }
            
            
        }


        private static void OnReceiveVmComplete(IList<byte[]> parameters)
        {
            try
            {
                MLogger.LogTo(Level.TRACE, false, "[ANS_VM_COMPLETE] Vm_complete message received");
                
                Byte[] vmCompleteStr = parameters[0];
                Byte envId = vmCompleteStr[0];

                Vm vm = VmManager.GetVmByEnvId(Convert.ToInt32(envId));        // Получили Vm, завершившую работу
                Research research = ResearchManager.GetResearchByVmId(vm.Id);  // Получили текущее исследование
                if (research == null) return;

                ResearchManager.UpdateResearchStopTime(research.Id);
                ResearchManager.UpdateResearchState(research.Id, ResearchState.COMPLETED);
            }
            catch (Exception)
            {
                MLogger.LogTo(Level.WARNING, false, "OnReceiveVmComplete, catch");
            }  
        }



        private static void OnReceiveReport(IList<byte[]> parameters)
        {
            try
            {
                byte[] report = parameters[0];

                Int32 envId = Convert.ToInt32(report[0]);
                Int32 modId = Convert.ToInt32(report[1]);

                byte[] data = new byte[report.Length - 2];
                Buffer.BlockCopy(report, 2, data, 0, report.Length - 2);
                MemoryStream ms = new MemoryStream(data);
                BinaryReader br = new BinaryReader(ms);

                String obj = Encoding.UTF8.GetString(br.ReadBytes(100)).Trim(new[] { '\0' });
                Int32 actionId = br.ReadInt32();
                String target = Encoding.UTF8.GetString(br.ReadBytes(200)).Trim(new[] { '\0' });

                br.Close();
                ms.Close();
                MLogger.LogTo(Level.TRACE, false, "Report message received: EnvId=" + envId + ", ModId=" + modId + ", Data.Length=" + data.Length);

                Vm vm = VmManager.GetVmByEnvId(envId);                         //Получаем текущую Vm этого исследования
                Research research = ResearchManager.GetResearchByVmId(vm.Id);  //Получаем текущее исследование

                ReportManager.AddReport(research.Id, modId, actionId, obj, target);
            }
            catch (Exception)
            {
                MLogger.LogTo(Level.WARNING, false, "OnReceiveReport, catch");
            } 
        }

        private static void OnReceiveVmCreateEvent(List<byte[]> parameters)
        {

            Int32 Length = Convert.ToInt32(parameters[0][0]);
            String machineName = Encoding.UTF8.GetString(parameters[1], 0, Length);
            Byte[] mac = parameters[2];
            Vm vm = VmManager.GetVm(machineName);
            VmManager.UpdateVmState(machineName, (Int32)VmManager.State.STOPPED);
            if (ResearchManager.GetResearchByVmName(machineName) != null)
            {
                Resources.StartVm(vm.Id);
                VmManager.UpdateVmState(machineName, (Int32)VmManager.State.STARTED);
                VmManager.UpdateVmEnvMac(vm.Id, DataUtils.ByteArrayToHexString(mac));
               
            }

        }

        private static void OnReceiveLoadTraffic(List<byte[]> parameters)
        {
            try
            {
                MLogger.LogTo(Level.TRACE, false, "[ANS_LOAD_TRAFFIC] received");
                
                String ip        = Encoding.UTF8.GetString(parameters[0]);
                String startTime = Encoding.UTF8.GetString(parameters[1]);
                String filename = ip + startTime + ".pcap";

                MLogger.LogTo(Level.TRACE, false, "--> filename = " + filename);

                Research research = ResearchManager.GetResearchByTrafficFileName(filename);
                if (research == null) return;

                String link = "ftp://10.32.200.143/" + filename;
                ResearchManager.UpdateTrafficInfo(research.Id, TrafficFileReady.COMPLETE, link);
            }
            catch (Exception)
            {
                MLogger.LogTo(Level.WARNING, false, "OnReceiveLoadTraffic, catch");
            }
        }
    }//end namespace
}//end class