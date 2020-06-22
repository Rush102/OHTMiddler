using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using DDS;
using DDS.OpenSplice;
using Veh_HandShakeData;
using OHTM.NLog_USE;

namespace MirleOHT.類別.DCPS
{
    public class DDS_SetUp
    {
        static IPublisher motionInfoPublisher;
        static ISubscriber motionInfoSubscriber;
        //
        static IPublisher motionInfoVehCommPublisher;
        static ISubscriber motionInfoVehCommSubscriber;
        //
        static ISubscriber motionInfoVehInterCommSendDataSubscriber;
        static IPublisher motionInfoVehInterCommSendDataPublisher;

        static ISubscriber motionInfoVehInterCommReptDataSubscriber;
        static ISubscriber motionInfoVehInterCommReptData_134Subscriber;                // Roy+19113
        static IPublisher motionInfoVehInterCommReptDataPublisher;
        static IPublisher motionInfoVehInterCommReptData_134Publisher;                  // Roy+191113
        //
        static ISubscriber motionInfoHandShakeSendDataSubscriber;
        static IPublisher motionInfoHandShakeSendDataPublisher;
        static ISubscriber motionInfoHandShakeRecieveDataSubscriber;
        static IPublisher motionInfoHandShakeRecieveDataPublisher;

        //+++++++++++++++               // Roy+180205
        static IPublisher betweenVehicleDataPublisher;
        static ISubscriber betweenVehicleDataSubscriber;

        static ITopic between_Vehicle_DataTopic;
        //+++++++++++++++           

        //++++++++++++++++++++++++++++++++++               // Roy+180222
        static IPublisher loadPortPioFromVehiclePublisher;
        static ISubscriber loadPortPioFromVehicleSubscriber;

        static ITopic loadPort_PIO_FromVehicleTopic;

        //~~~~~~~~~~~~

        static IPublisher loadPortPioToVehiclePublisher;
        static ISubscriber loadPortPioToVehicleSubscriber;

        static ITopic loadPort_PIO_ToVehicleTopic;

        //~~~~~~~~~~~~

        static IPublisher eQStagesInterfaceIOPublisher;
        static ISubscriber eQStagesInterfaceIOSubscriber;

        static ITopic eQ_Stages_Interface_IOTopic;

        //~~~~~~~~~~~~

        static IPublisher interVehiclesBlockZonesControlPublisher;
        static ISubscriber interVehiclesBlockZonesControlSubscriber;

        static ITopic interVehicles_BlockZones_ControlTopic;
        //++++++++++++++++++++++++++++++++++            

        //
        static ITopic motionInfo_ClientTopic;
        static ITopic motionInfo_ServerTopic;
        //
        static ITopic motionInfo_VehCommTopic;
        static ITopic motionInfo_VehInterCommSendDataTopic;

        static ITopic motionInfo_VehInterCommReptDataTopic;
        static ITopic motionInfo_VehInterCommReptData_134Topic;                 // Roy+191113

        static ITopic motionInfo_HandShakeSendDataTopic;
        static ITopic motionInfo_HandShakeRecieveDataTopic;

        static GuardCondition escape;
        static ReturnCode status;


        //+++++++++++++++++++++++++++++++++++++++++++               // Roy+180319
        #region DDS CopyFromTopicQos_Ex
        public static void CopyFromTopicQos_Ex(ref DataReaderQos ForchangeQos, TopicQos reliableQos)
        {
            ForchangeQos.Durability = reliableQos.Durability;
            ForchangeQos.DestinationOrder = reliableQos.DestinationOrder;
            ForchangeQos.History = reliableQos.History;
            ForchangeQos.Reliability = reliableQos.Reliability;
        }
        #endregion
        //+++++++++++++++++++++++++++++++++++++++++++


        public static bool Initial()
        {

            #region ospl config xml
            string configPath = System.Environment.GetEnvironmentVariable("OSPL_HOME") + "\\etc\\config\\";
            XDocument ospl_external = XDocument.Load(configPath + "ospl_external.xml");
            XDocument ospl_internal = XDocument.Load(configPath + "ospl_internal.xml");
            int domain_NetWork = int.Parse(ospl_external.Root.Element("Domain").Element("Id").Value);
            int domain_Local = int.Parse(ospl_internal.Root.Element("Domain").Element("Id").Value);
            
            #endregion


            string partitionName = "Veh_C_MotionInfo";
            string partitionName_local = "Veh_MotionInfo";

            //++++++++++++++++++++++++++++                 // Roy+181019		// Roy-190422
            //if (MirleOHT.類別.定義.eq.bIsLine_OHT)
            //{
            //    domain_NetWork = 11;
            //}
            //++++++++++++++++++++++++++++

            //++++++++++++++++++++++++++++                 // Roy+181213		// Roy-190422
            //else if (MirleOHT.類別.定義.eq.bIsSLAM_AGV)
            //{
            //    domain_NetWork = 51;
            //}
            //++++++++++++++++++++++++++++

            try
            {
                #region  Create a DomainParticipantFactory and a DomainParticipant(using Default QoS settings).
                DDS_Global.dpf = DomainParticipantFactory.Instance;
                ErrorHandler.checkHandle(DDS_Global.dpf, "DDS.DomainParticipantFactory.Instance");

                //Create participant for Network connected 
                IDomainParticipant participant = DDS_Global.dpf.CreateParticipant(domain_NetWork, null, StatusKind.Any);
                ErrorHandler.checkHandle(participant, "DDS.DomainParticipantFactory.CreateParticipant");

                //Create participant for Local Share Memory
                IDomainParticipant participant_local = DDS_Global.dpf.CreateParticipant(domain_Local, null, StatusKind.Any);
                ErrorHandler.checkHandle(participant_local, "DDS.DomainParticipantFactory.CreateParticipant");
                #endregion

                #region Register Data Types for each Topics
                /* Register the required datatype for MotionInfo_Client. */
                MotionInfo_ClientTypeSupport motionInfo_ClientTS = new MotionInfo_ClientTypeSupport();
                string motionInfo_ClientTypeName = motionInfo_ClientTS.TypeName;
                status = motionInfo_ClientTS.RegisterType(participant, motionInfo_ClientTypeName);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  MotionInfo_ClientTypeSupport.RegisterType - Error");                    // Roy*180612

                /* Register the required datatype for MotionInfo_Server. */
                MotionInfo_ServerTypeSupport motionInfo_ServerTS = new MotionInfo_ServerTypeSupport();
                string motionInfo_ServerTypeName = motionInfo_ServerTS.TypeName;
                status = motionInfo_ServerTS.RegisterType(participant, motionInfo_ServerTypeName);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfo_ServerTS.RegisterType - Error");                    // Roy*180612

                //+++++++++++++++               // Roy+180205
                /* Register the required datatype for Between_Vehicle_Data. */
                Between_Vehicle_DataTypeSupport between_Vehicle_DataTS = new Between_Vehicle_DataTypeSupport();
                string between_Vehicle_DataTypeName = between_Vehicle_DataTS.TypeName;
                status = between_Vehicle_DataTS.RegisterType(participant, between_Vehicle_DataTypeName);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  between_Vehicle_DataTS.RegisterType - Error");                    // Roy*180612
                //+++++++++++++++             


                //++++++++++++++++++++++++++++++++++               // Roy+180222
                /* Register the required datatype for LoadPort_PIO_FromVehicle. */
                LoadPort_PIO_FromVehicleTypeSupport loadPort_PIO_FromVehicleTS = new LoadPort_PIO_FromVehicleTypeSupport();
                string loadPort_PIO_FromVehicleTypeName = loadPort_PIO_FromVehicleTS.TypeName;
                status = loadPort_PIO_FromVehicleTS.RegisterType(participant, loadPort_PIO_FromVehicleTypeName);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  loadPort_PIO_FromVehicleTS.RegisterType - Error");                    // Roy*180612

                //~~~~~~~~~~~~

                /* Register the required datatype for LoadPort_PIO_ToVehicle. */
                LoadPort_PIO_ToVehicleTypeSupport loadPort_PIO_ToVehicleTS = new LoadPort_PIO_ToVehicleTypeSupport();
                string loadPort_PIO_ToVehicleTypeName = loadPort_PIO_ToVehicleTS.TypeName;
                status = loadPort_PIO_ToVehicleTS.RegisterType(participant, loadPort_PIO_ToVehicleTypeName);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  loadPort_PIO_ToVehicleTS.RegisterType - Error");                    // Roy*180612

                //~~~~~~~~~~~~

                /* Register the required datatype for EQ_Stages_Interface_IO. */
                EQ_Stages_Interface_IOTypeSupport eQ_Stages_Interface_IOTS = new EQ_Stages_Interface_IOTypeSupport();
                string eQ_Stages_Interface_IOTypeName = eQ_Stages_Interface_IOTS.TypeName;
                status = eQ_Stages_Interface_IOTS.RegisterType(participant, eQ_Stages_Interface_IOTypeName);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  eQ_Stages_Interface_IOTS.RegisterType - Error");                    // Roy*180612

                //~~~~~~~~~~~~

                /* Register the required datatype for InterVehicles_BlockZones_Control. */
                InterVehicles_BlockZones_ControlTypeSupport interVehicles_BlockZones_ControlTS = new InterVehicles_BlockZones_ControlTypeSupport();
                string interVehicles_BlockZones_ControlTypeName = interVehicles_BlockZones_ControlTS.TypeName;
                status = interVehicles_BlockZones_ControlTS.RegisterType(participant, interVehicles_BlockZones_ControlTypeName);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  interVehicles_BlockZones_ControlTS.RegisterType - Error");                    // Roy*180612
                //++++++++++++++++++++++++++++++++++           


                /* Register the required datatype for MotionInfo_VehComm.  
                   Register Local Topics for local participant */
                MotionInfo_Vehicle_CommTypeSupport motionInfo_VehCommTS = new MotionInfo_Vehicle_CommTypeSupport();
                string motionInfo_VehCommTypeName = motionInfo_VehCommTS.TypeName;
                status = motionInfo_VehCommTS.RegisterType(participant_local, motionInfo_VehCommTypeName);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfo_VehCommTS.RegisterType - Error");                    // Roy*180612

                //
                MotionInfo_Inter_Comm_SendDataTypeSupport motionInfo_VehInterCommSendDataTS = new MotionInfo_Inter_Comm_SendDataTypeSupport();
                string motionInfo_InterCommSendDataTypeName = motionInfo_VehInterCommSendDataTS.TypeName;
                status = motionInfo_VehInterCommSendDataTS.RegisterType(participant_local, motionInfo_InterCommSendDataTypeName);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfo_VehInterCommSendDataTS.RegisterType - Error");                    // Roy*180612

                //
                MotionInfo_Vehicle_Inter_Comm_ReportDataTypeSupport motionInfo_VehInterCommReptDataTS = new MotionInfo_Vehicle_Inter_Comm_ReportDataTypeSupport();
                string motionInfo_InterCommReptDataTypeName = motionInfo_VehInterCommReptDataTS.TypeName;
                status = motionInfo_VehInterCommReptDataTS.RegisterType(participant_local, motionInfo_InterCommReptDataTypeName);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfo_VehInterCommReptDataTS.RegisterType - Error");                    // Roy*180612

                //
                MotionInfo_Vehicle_Inter_Comm_ReportData_134TypeSupport motionInfo_VehInterCommReptData_134TS = new MotionInfo_Vehicle_Inter_Comm_ReportData_134TypeSupport();                    // Roy+191113
                string motionInfo_InterCommReptData_134TypeName = motionInfo_VehInterCommReptData_134TS.TypeName;                    // Roy+191113
                status = motionInfo_VehInterCommReptData_134TS.RegisterType(participant_local, motionInfo_InterCommReptData_134TypeName);                    // Roy+191113
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfo_VehInterCommReptData_134TS.RegisterType - Error");                    // Roy+191113

                //
                MotionInfo_HandShake_SendDataTypeSupport motionInfo_HandShakeSendDatTS = new MotionInfo_HandShake_SendDataTypeSupport();
                string motionInfo_HandShakeSendDataTypeName = motionInfo_HandShakeSendDatTS.TypeName;
                status = motionInfo_HandShakeSendDatTS.RegisterType(participant_local, motionInfo_HandShakeSendDataTypeName);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfo_HandShakeSendDatTS.RegisterType - Error");                    // Roy*180612

                //
                MotionInfo_HandShake_RecieveDataTypeSupport motionInfo_HandShakeRecieveDataTS = new MotionInfo_HandShake_RecieveDataTypeSupport();
                string motionInfo_HandShake_RecieveDataTypeName = motionInfo_HandShakeRecieveDataTS.TypeName;
                status = motionInfo_HandShakeRecieveDataTS.RegisterType(participant_local, motionInfo_HandShake_RecieveDataTypeName);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfo_HandShakeRecieveDataTS.RegisterType - Error");                    // Roy*180612
                #endregion

                #region Set Up Qos Policies for the Topics
                /* Initialise Qos variables for Network connected */
                TopicQos reliableTopicQos = new TopicQos();
                TopicQos settingTopicQos = new TopicQos();
                PublisherQos pubQos = new PublisherQos();
                SubscriberQos subQos = new SubscriberQos();
                DataWriterQos motionInfoDwQos = new DataWriterQos();
                DataReaderQos motionInfoDrQos = new DataReaderQos();

                DataWriterQos betweenVehicleDataDwQos = new DataWriterQos();                     // Roy+180205
                DataReaderQos betweenVehicleDataDrQos = new DataReaderQos();                     // Roy+180205

                //++++++++++++++++++++++++++++++++++               // Roy+180222
                DataWriterQos loadPortPioFromVehicleDwQos = new DataWriterQos();
                DataReaderQos loadPortPioFromVehicleDrQos = new DataReaderQos();

                DataWriterQos loadPortPioToVehicleDwQos = new DataWriterQos();
                DataReaderQos loadPortPioToVehicleDrQos = new DataReaderQos();

                DataWriterQos eQStagesInterfaceIODwQos = new DataWriterQos();
                DataReaderQos eQStagesInterfaceIODrQos = new DataReaderQos();

                DataWriterQos interVehiclesBlockZonesControlDwQos = new DataWriterQos();
                DataReaderQos interVehiclesBlockZonesControlDrQos = new DataReaderQos();
                //++++++++++++++++++++++++++++++++++          

                /* Initialise Qos variables for Local Network */
                TopicQos reliableTopicQos_local = new TopicQos();
                TopicQos settingTopicQos_local = new TopicQos();
                PublisherQos pubQos_local = new PublisherQos();
                SubscriberQos subQos_local = new SubscriberQos();
                DataWriterQos motionInfoDwQos_local = new DataWriterQos();
                DataReaderQos motionInfoDrQos_local = new DataReaderQos();

                /* Set the ReliabilityQosPolicy to RELIABLE. for network connected case */
                status = participant.GetDefaultTopicQos(ref reliableTopicQos);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  participant.GetDefaultTopicQos - Error");                    // Roy*180612
                reliableTopicQos.Reliability.Kind = ReliabilityQosPolicyKind.ReliableReliabilityQos;

                //reliableTopicQos.Durability.Kind = DurabilityQosPolicyKind.PersistentDurabilityQos;                     // Roy-+171204

                /* Make the tailored QoS the new default. */
                //status = participant.SetDefaultTopicQos(reliableTopicQos);                                                         // Roy-171204 ... if set 'PersistentDurabilityQos', CreateTopics will fail later ...
                //ErrorHandler.checkStatus(status, "DDS.DomainParticipant.SetDefaultTopicQos");                // Roy-171204 ... if set 'PersistentDurabilityQos', CreateTopics will fail later ...
                //

                /* Set the ReliabilityQosPolicy to RELIABLE. for no network connection */
                status = participant_local.GetDefaultTopicQos(ref reliableTopicQos_local);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  participant_local.GetDefaultTopicQos - Error");                    // Roy*180612
                reliableTopicQos_local.Reliability.Kind = ReliabilityQosPolicyKind.ReliableReliabilityQos;

                /* Make the tailored QoS the new default. */
                //status = participant_local.SetDefaultTopicQos(reliableTopicQos_local);                                    // Roy-171204 ... if set 'PersistentDurabilityQos', CreateTopics will fail later ...
                //ErrorHandler.checkStatus(status, "DDS.DomainParticipant.SetDefaultTopicQos");                // Roy-171204 ... if set 'PersistentDurabilityQos', CreateTopics will fail later ...
                #endregion


                #region Create Subscriber:MotionInfo Subscriber for the Partition:
                /* Adapt the default SubscriberQos to read from the MotonInfo_Server/Client. */
                status = participant.GetDefaultSubscriberQos(ref subQos);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  participant.GetDefaultSubscriberQos - Error");                    // Roy*180612
                subQos.Partition.Name = new string[1];
                subQos.Partition.Name[0] = partitionName;

                /* Create a Subscriber for the client application. */
                motionInfoSubscriber = participant.CreateSubscriber(subQos);
                ErrorHandler.checkHandle(motionInfoSubscriber, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoSubscriber= participant.CreateSubscriber - Error");                    // Roy*180612

                //+++++++++++++++               // Roy+180205
                /* Create a Subscriber for the Between_Vehicle_Data application. */
                betweenVehicleDataSubscriber = participant.CreateSubscriber(subQos);
                ErrorHandler.checkHandle(betweenVehicleDataSubscriber, "@DDS_SetUp:  Initial  \\   <DDS>  betweenVehicleDataSubscriber= participant.CreateSubscriber - Error");                    // Roy*180612
                //+++++++++++++++          

                //++++++++++++++++++++++++++++++++++               // Roy+180222
                /* Create a Subscriber for the LoadPort_PIO_FromVehicle application. */
                loadPortPioFromVehicleSubscriber = participant.CreateSubscriber(subQos);
                ErrorHandler.checkHandle(loadPortPioFromVehicleSubscriber, "@DDS_SetUp:  Initial  \\   <DDS>  loadPortPioFromVehicleSubscriber= participant.CreateSubscriber - Error");                    // Roy*180612

                /* Create a Subscriber for the LoadPort_PIO_ToVehicle application. */
                loadPortPioToVehicleSubscriber = participant.CreateSubscriber(subQos);
                ErrorHandler.checkHandle(loadPortPioToVehicleSubscriber, "@DDS_SetUp:  Initial  \\   <DDS>  loadPortPioToVehicleSubscriber= participant.CreateSubscriber - Error");                    // Roy*180612

                /* Create a Subscriber for the EQ_Stages_Interface_IO application. */
                eQStagesInterfaceIOSubscriber = participant.CreateSubscriber(subQos);
                ErrorHandler.checkHandle(eQStagesInterfaceIOSubscriber, "@DDS_SetUp:  Initial  \\   <DDS>  eQStagesInterfaceIOSubscriber= participant.CreateSubscriber - Error");                    // Roy*180612

                /* Create a Subscriber for the InterVehicles_BlockZones_Control application. */
                interVehiclesBlockZonesControlSubscriber = participant.CreateSubscriber(subQos);
                ErrorHandler.checkHandle(interVehiclesBlockZonesControlSubscriber, "@DDS_SetUp:  Initial  \\   <DDS>  interVehiclesBlockZonesControlSubscriber= participant.CreateSubscriber - Error");                    // Roy*180612
                //++++++++++++++++++++++++++++++++++           

                /* Adapt the default SubscriberQos to read from the MotonInfo_Vehicle_Comm. */
                status = participant_local.GetDefaultSubscriberQos(ref subQos_local);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  DomainParticipant_local.GetDefaultSubscriberQos - Error");                    // Roy*180612
                subQos_local.Partition.Name = new string[1];
                subQos_local.Partition.Name[0] = partitionName_local;

                /* Create a Subscriber for the MotionInfo_Vehicle_Comm application. */
                motionInfoVehCommSubscriber = participant_local.CreateSubscriber(subQos_local);
                ErrorHandler.checkHandle(motionInfoVehCommSubscriber, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehCommSubscriber= DomainParticipant_local.CreateSubscriber - Error");                    // Roy*180612

                /* Create a Subscriber for the MotionInfo_Vehicle_InterComm_SendData application. */
                motionInfoVehInterCommSendDataSubscriber = participant_local.CreateSubscriber(subQos_local);
                ErrorHandler.checkHandle(motionInfoVehInterCommSendDataSubscriber, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehInterCommSendDataSubscriber= DomainParticipant_local.CreateSubscriber - Error");                    // Roy*180612

                /* Create a Subscriber for the MotionInfo_Vehicle_InterComm_ReptData application. */
                motionInfoVehInterCommReptDataSubscriber = participant_local.CreateSubscriber(subQos_local);
                ErrorHandler.checkHandle(motionInfoVehInterCommReptDataSubscriber, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehInterCommReptDataSubscriber= DomainParticipant_local.CreateSubscriber - Error");                    // Roy*180612

                /* Create a Subscriber for the MotionInfo_Vehicle_InterComm_ReptData_134 application. */
                motionInfoVehInterCommReptData_134Subscriber = participant_local.CreateSubscriber(subQos_local);                    // Roy+191113
                ErrorHandler.checkHandle(motionInfoVehInterCommReptData_134Subscriber, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehInterCommReptData_134Subscriber= DomainParticipant_local.CreateSubscriber - Error");                    // Roy+191113

                /* Create a Subscriber for the MotionInfo_HandShake_SendData application. */
                motionInfoHandShakeSendDataSubscriber = participant_local.CreateSubscriber(subQos_local);
                ErrorHandler.checkHandle(motionInfoHandShakeSendDataSubscriber, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoHandShakeSendDataSubscriber= DomainParticipant_local.CreateSubscriber - Error");                    // Roy*180612

                /* Create a Subscriber for the MotionInfo_HandShake_RecieveData application. */
                motionInfoHandShakeRecieveDataSubscriber = participant_local.CreateSubscriber(subQos_local);
                ErrorHandler.checkHandle(motionInfoHandShakeRecieveDataSubscriber, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoHandShakeRecieveDataSubscriber= DomainParticipant_local.CreateSubscriber - Error");                    // Roy*180612
                #endregion

                #region Create Pubisher: the write_partition
                /* Adapt the default PublisherQos to write into the
                   "write" Partition. */
                status = participant.GetDefaultPublisherQos(ref pubQos);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  DomainParticipant.GetDefaultPublisherQos - Error");                    // Roy*180612
                pubQos.Partition.Name = new string[1];
                pubQos.Partition.Name[0] = partitionName;

                /* Create a Publisher for the motionInfo application. */
                motionInfoPublisher = participant.CreatePublisher(pubQos);
                ErrorHandler.checkHandle(motionInfoPublisher, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoPublisher= DomainParticipant.CreatePublisher - Error");                    // Roy*180612

                //+++++++++++++++               // Roy+180205
                /* Create a Publisher for the Between_Vehicle_Data application. */
                betweenVehicleDataPublisher = participant.CreatePublisher(pubQos);
                ErrorHandler.checkHandle(betweenVehicleDataPublisher, "@DDS_SetUp:  Initial  \\   <DDS>  betweenVehicleDataPublisher= DomainParticipant.CreatePublisher - Error");                    // Roy*180612
                //+++++++++++++++        

                //++++++++++++++++++++++++++++++++++               // Roy+180222
                /* Create a Publisher for the LoadPort_PIO_FromVehicle application. */
                loadPortPioFromVehiclePublisher = participant.CreatePublisher(pubQos);
                ErrorHandler.checkHandle(loadPortPioFromVehiclePublisher, "@DDS_SetUp:  Initial  \\   <DDS>  loadPortPioFromVehiclePublisher= DomainParticipant.CreatePublisher - Error");                    // Roy*180612

                /* Create a Publisher for the LoadPort_PIO_ToVehicle application. */
                loadPortPioToVehiclePublisher = participant.CreatePublisher(pubQos);
                ErrorHandler.checkHandle(loadPortPioToVehiclePublisher, "@DDS_SetUp:  Initial  \\   <DDS>  loadPortPioToVehiclePublisher= DomainParticipant.CreatePublisher - Error");                    // Roy*180612

                /* Create a Publisher for the EQ_Stages_Interface_IO application. */
                eQStagesInterfaceIOPublisher = participant.CreatePublisher(pubQos);
                ErrorHandler.checkHandle(eQStagesInterfaceIOPublisher, "@DDS_SetUp:  Initial  \\   <DDS>  eQStagesInterfaceIOPublisher= DomainParticipant.CreatePublisher - Error");                    // Roy*180612

                /* Create a Publisher for the InterVehicles_BlockZones_Control application. */
                interVehiclesBlockZonesControlPublisher = participant.CreatePublisher(pubQos);
                ErrorHandler.checkHandle(interVehiclesBlockZonesControlPublisher, "@DDS_SetUp:  Initial  \\   <DDS>  interVehiclesBlockZonesControlPublisher= DomainParticipant.CreatePublisher - Error");                    // Roy*180612
                //++++++++++++++++++++++++++++++++++            

                /* Adapt the default PublisherQos to write into the "write" Partition. */
                status = participant_local.GetDefaultPublisherQos(ref pubQos_local);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  DomainParticipant_local.GetDefaultPublisherQos - Error");                    // Roy*180612
                pubQos_local.Partition.Name = new string[1];
                pubQos_local.Partition.Name[0] = partitionName_local;

                /* Create a Publisher for the motionInfo_veh_comm application. */
                motionInfoVehCommPublisher = participant_local.CreatePublisher(pubQos_local);
                ErrorHandler.checkHandle(motionInfoVehCommPublisher, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehCommPublisher= DomainParticipant_local.CreatePublisher - Error");                    // Roy*180612

                /* Create a Publisher for the motionInfo_veh_Inter_comm_SendData application. */
                motionInfoVehInterCommSendDataPublisher = participant_local.CreatePublisher(pubQos_local);
                ErrorHandler.checkHandle(motionInfoVehInterCommSendDataPublisher, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehInterCommSendDataPublisher= DomainParticipant_local.CreatePublisher - Error");                    // Roy*180612

                /* Create a Publisher for the motionInfo_veh_Inter_comm_ReptData application. */
                motionInfoVehInterCommReptDataPublisher = participant_local.CreatePublisher(pubQos_local);
                ErrorHandler.checkHandle(motionInfoVehInterCommReptDataPublisher, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehInterCommReptDataPublisher= DomainParticipant_local.CreatePublisher - Error");                    // Roy*180612

                /* Create a Publisher for the motionInfo_veh_Inter_comm_ReptData_134 application. */
                motionInfoVehInterCommReptData_134Publisher = participant_local.CreatePublisher(pubQos_local);                  // Roy+191113
                ErrorHandler.checkHandle(motionInfoVehInterCommReptData_134Publisher, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehInterCommReptData_134Publisher= DomainParticipant_local.CreatePublisher - Error");                    // Roy+191113

                /* Create a Publisher for the motionInfo_HandShake_SendData application. */
                motionInfoHandShakeSendDataPublisher = participant_local.CreatePublisher(pubQos_local);
                ErrorHandler.checkHandle(motionInfoHandShakeSendDataPublisher, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoHandShakeSendDataPublisher= DomainParticipant_local.CreatePublisher - Error");                    // Roy*180612

                /* Create a Publisher for the motionInfo_HandShake_RecieveData application. */
                motionInfoHandShakeRecieveDataPublisher = participant_local.CreatePublisher(pubQos_local);
                ErrorHandler.checkHandle(motionInfoHandShakeRecieveDataPublisher, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoHandShakeRecieveDataPublisher= DomainParticipant_local.CreatePublisher - Error");                    // Roy*180612
                #endregion


                #region Create Topics: MotionInfo_***Topic
                //reliableTopicQos.Durability.Kind = DurabilityQosPolicyKind.PersistentDurabilityQos;                     // Roy+171204               // Roy-180918
                reliableTopicQos.Durability.Kind = DurabilityQosPolicyKind.TransientDurabilityQos;                     // Roy+180918

                /* Use the changed policy when defining the OHT_MotionInf_Client topic */

                //motionInfo_ClientTopic = participant.CreateTopic(
                //    "Veh_MotionInf_Client",
                //    motionInfo_ClientTypeName);

                // Roy-171204
                motionInfo_ClientTopic = participant.CreateTopic(
                    "Veh_MotionInf_Client",
                    motionInfo_ClientTypeName,
                    reliableTopicQos);



                ErrorHandler.checkHandle(motionInfo_ClientTopic, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfo_ClientTopic= DomainParticipant.CreateTopic - Error");                    // Roy*180612


                /* Use the changed policy when defining the OHT_MotionInf_Server topic */

                //motionInfo_ServerTopic = participant.CreateTopic(
                //    "Veh_MotionInf_Server",
                //    motionInfo_ServerTypeName);

                /* Use the changed policy when defining the OHT_MotionInf_Veh_Comm topic */

                // Roy-171204
                motionInfo_ServerTopic = participant.CreateTopic(
                    "Veh_MotionInf_Server",
                    motionInfo_ServerTypeName,
                    reliableTopicQos);



                ErrorHandler.checkHandle(motionInfo_ServerTopic, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfo_ServerTopic= DomainParticipant.CreateTopic - Error");                    // Roy*180612

                //+++++++++++++++               // Roy+180205
                between_Vehicle_DataTopic = participant.CreateTopic(
                   "Veh_Between_Vehicle_Data",
                   between_Vehicle_DataTypeName,
                   reliableTopicQos);

                ErrorHandler.checkHandle(between_Vehicle_DataTopic, "@DDS_SetUp:  Initial  \\   <DDS>  between_Vehicle_DataTopic= DomainParticipant.CreateTopic - Error");                    // Roy*180612
                //+++++++++++++++             


                //++++++++++++++++++++++++++++++++++               // Roy+180222
                loadPort_PIO_FromVehicleTopic = participant.CreateTopic(
                  "Veh_LoadPort_PIO_FromVehicle",
                  loadPort_PIO_FromVehicleTypeName,
                  reliableTopicQos);

                ErrorHandler.checkHandle(loadPort_PIO_FromVehicleTopic, "@DDS_SetUp:  Initial  \\   <DDS>  loadPort_PIO_FromVehicleTopic= DomainParticipant.CreateTopic - Error");                    // Roy*180612

                //~~~~~~~~~~~~

                loadPort_PIO_ToVehicleTopic = participant.CreateTopic(
                  "Veh_LoadPort_PIO_ToVehicle",
                  loadPort_PIO_ToVehicleTypeName,
                  reliableTopicQos);

                ErrorHandler.checkHandle(loadPort_PIO_ToVehicleTopic, "@DDS_SetUp:  Initial  \\   <DDS>  loadPort_PIO_ToVehicleTopic= DomainParticipant.CreateTopic - Error");                    // Roy*180612

                //~~~~~~~~~~~~

                eQ_Stages_Interface_IOTopic = participant.CreateTopic(
                  "Veh_EQ_Stages_Interface_IO",
                  eQ_Stages_Interface_IOTypeName,
                  reliableTopicQos);

                ErrorHandler.checkHandle(eQ_Stages_Interface_IOTopic, "@DDS_SetUp:  Initial  \\   <DDS>  eQ_Stages_Interface_IOTopic= DomainParticipant.CreateTopic - Error");                    // Roy*180612

                //~~~~~~~~~~~~
                interVehicles_BlockZones_ControlTopic = participant.CreateTopic(
                                  "Veh_InterVehicles_BlockZones_Control",
                                  interVehicles_BlockZones_ControlTypeName,
                                  reliableTopicQos);

                ErrorHandler.checkHandle(interVehicles_BlockZones_ControlTopic, "@DDS_SetUp:  Initial  \\   <DDS>  interVehicles_BlockZones_ControlTopic= DomainParticipant.CreateTopic - Error");                    // Roy*180612
                //++++++++++++++++++++++++++++++++++            


                /* Use the changed policy when defining the OHT_MotionInf_Veh_Comm topic */


                motionInfo_VehCommTopic = participant_local.CreateTopic(
                    "Veh_MotionInfo_Veh_Comm",
                    motionInfo_VehCommTypeName,
                    reliableTopicQos_local);

                ErrorHandler.checkHandle(motionInfo_VehCommTopic, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfo_VehCommTopic= DomainParticipant_local.CreateTopic - Error");                    // Roy*180612

                /* Use the changed policy when defining the OHT_MotionInfo_Veh_InterComm_SendData topic */
                motionInfo_VehInterCommSendDataTopic = participant_local.CreateTopic(
                    "Veh_MotionInfo_VehInterCommSendData",
                    motionInfo_InterCommSendDataTypeName,
                    reliableTopicQos_local);

                ErrorHandler.checkHandle(motionInfo_VehInterCommSendDataTopic, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfo_VehInterCommSendDataTopic= DomainParticipant_local.CreateTopic - Error");                    // Roy*180612

                /* Use the changed policy when defining the OHT_MotionInfo_Veh_InterComm_ReptData topic */
                motionInfo_VehInterCommReptDataTopic = participant_local.CreateTopic(
                    "Veh_MotionInfo_VehInterCommReptData",
                    motionInfo_InterCommReptDataTypeName,
                    reliableTopicQos_local);

                ErrorHandler.checkHandle(motionInfo_VehInterCommReptDataTopic, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfo_VehInterCommReptDataTopic= DomainParticipant_local.CreateTopic - Error");                    // Roy*180612


                /* Use the changed policy when defining the OHT_MotionInfo_Veh_InterComm_ReptData_134 topic */
                motionInfo_VehInterCommReptData_134Topic = participant_local.CreateTopic(
                    "Veh_MotionInfo_VehInterCommReptData_134",
                    motionInfo_InterCommReptData_134TypeName,
                    reliableTopicQos_local);                    // Roy+191113

                ErrorHandler.checkHandle(motionInfo_VehInterCommReptData_134Topic, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfo_VehInterCommReptData_134Topic= DomainParticipant_local.CreateTopic - Error");                    // Roy+191113


                /* Use the changed policy when defining the OHT_MotionInfo_HandShakeSendData topic */
                motionInfo_HandShakeSendDataTopic = participant_local.CreateTopic(
                   "Veh_MotionInfo_HandShakeSendData",
                   motionInfo_HandShakeSendDataTypeName,
                   reliableTopicQos_local);

                ErrorHandler.checkHandle(motionInfo_HandShakeSendDataTopic, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfo_HandShakeSendDataTopic= DomainParticipant_local.CreateTopic - Error");                    // Roy*180612

                /* Use the changed policy when defining the OHT_MotionInfo_HandShakeRecieveData topic */
                motionInfo_HandShakeRecieveDataTopic = participant_local.CreateTopic(
                    "Veh_MotionInfo_HandShakeRecieveData",
                    motionInfo_HandShake_RecieveDataTypeName,
                    reliableTopicQos_local);

                ErrorHandler.checkHandle(motionInfo_HandShakeRecieveDataTopic, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfo_HandShakeRecieveDataTopic= DomainParticipant_local.CreateTopic - Error");                    // Roy*180612
                #endregion


                #region MotionInfo Create Client/Server DataReader  for NetWork connected
                status = motionInfoSubscriber.GetDefaultDataReaderQos(ref motionInfoDrQos); ;
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoSubscriber.GetDefaultDataReaderQos - Error");                    // Roy*180612

                //
                status = motionInfoSubscriber.CopyFromTopicQos(ref motionInfoDrQos, reliableTopicQos);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoSubscriber.CopyFromTopicQos - Error");                    // Roy*180612

                //
                motionInfoDrQos.History.Kind = HistoryQosPolicyKind.KeepLastHistoryQos;
                //motionInfoDrQos.Durability.Kind = DurabilityQosPolicyKind.PersistentDurabilityQos;                     // Roy+171204                // Roy-180918
                motionInfoDrQos.Durability.Kind = DurabilityQosPolicyKind.TransientDurabilityQos;                     // Roy+180918

                //motionInfoDrQos.DestinationOrder.Kind = DestinationOrderQosPolicyKind.BySourceTimestampDestinationorderQos;                     // Roy+180131               //Roy-180205
                motionInfoDrQos.DestinationOrder.Kind = DestinationOrderQosPolicyKind.ByReceptionTimestampDestinationorderQos;                     // Roy+180205
                motionInfoDrQos.Reliability.Kind = ReliabilityQosPolicyKind.ReliableReliabilityQos;                     // Roy+180319 ... 補強DDS怪問題 ...  

                //+++++++++++++++               // Roy+180205
                betweenVehicleDataSubscriber.GetDefaultDataReaderQos(ref betweenVehicleDataDrQos);
                status = betweenVehicleDataSubscriber.CopyFromTopicQos(ref betweenVehicleDataDrQos, reliableTopicQos);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  betweenVehicleDataSubscriber.CopyFromTopicQos - Error");                    // Roy*180612

                betweenVehicleDataDrQos.History.Kind = HistoryQosPolicyKind.KeepLastHistoryQos;
                //betweenVehicleDataDrQos.Durability.Kind = DurabilityQosPolicyKind.PersistentDurabilityQos;                    // Roy+180222                // Roy-180918
                //betweenVehicleDataDrQos.Durability.Kind = DurabilityQosPolicyKind.PersistentDurabilityQos;                    // Roy+180918                // Roy-180918
                betweenVehicleDataDrQos.Durability.Kind = DurabilityQosPolicyKind.TransientDurabilityQos;                    // Roy+180918

                //betweenVehicleDataDrQos.Durability.Kind = DurabilityQosPolicyKind.TransientDurabilityQos;                    // Roy-180222
                betweenVehicleDataDrQos.DestinationOrder.Kind = DestinationOrderQosPolicyKind.ByReceptionTimestampDestinationorderQos;
                betweenVehicleDataDrQos.Reliability.Kind = ReliabilityQosPolicyKind.ReliableReliabilityQos;                     // Roy+180319 ... 補強DDS怪問題 ...  
                //+++++++++++++++             

                //++++++++++++++++++++++++++++++++++               // Roy+180222
                loadPortPioFromVehicleSubscriber.GetDefaultDataReaderQos(ref loadPortPioFromVehicleDrQos);
                status = loadPortPioFromVehicleSubscriber.CopyFromTopicQos(ref loadPortPioFromVehicleDrQos, reliableTopicQos);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  loadPortPioFromVehicleSubscriber.CopyFromTopicQos - Error");                    // Roy*180612

                loadPortPioFromVehicleDrQos.History.Kind = HistoryQosPolicyKind.KeepLastHistoryQos;
                //loadPortPioFromVehicleDrQos.Durability.Kind = DurabilityQosPolicyKind.PersistentDurabilityQos;
                loadPortPioFromVehicleDrQos.Durability.Kind = DurabilityQosPolicyKind.TransientDurabilityQos;
                loadPortPioFromVehicleDrQos.DestinationOrder.Kind = DestinationOrderQosPolicyKind.ByReceptionTimestampDestinationorderQos;
                loadPortPioFromVehicleDrQos.Reliability.Kind = ReliabilityQosPolicyKind.ReliableReliabilityQos;                     // Roy+180319 ... 補強DDS怪問題 ...  

                //~~~~~~~~~~~~

                loadPortPioToVehicleSubscriber.GetDefaultDataReaderQos(ref loadPortPioToVehicleDrQos);
                status = loadPortPioToVehicleSubscriber.CopyFromTopicQos(ref loadPortPioToVehicleDrQos, reliableTopicQos);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  loadPortPioToVehicleSubscriber.CopyFromTopicQos - Error");                    // Roy*180612

                loadPortPioToVehicleDrQos.History.Kind = HistoryQosPolicyKind.KeepLastHistoryQos;
                //loadPortPioToVehicleDrQos.Durability.Kind = DurabilityQosPolicyKind.PersistentDurabilityQos;
                loadPortPioToVehicleDrQos.Durability.Kind = DurabilityQosPolicyKind.TransientDurabilityQos;
                loadPortPioToVehicleDrQos.DestinationOrder.Kind = DestinationOrderQosPolicyKind.ByReceptionTimestampDestinationorderQos;
                loadPortPioToVehicleDrQos.Reliability.Kind = ReliabilityQosPolicyKind.ReliableReliabilityQos;                     // Roy+180319 ... 補強DDS怪問題 ...  

                //~~~~~~~~~~~~

                eQStagesInterfaceIOSubscriber.GetDefaultDataReaderQos(ref eQStagesInterfaceIODrQos);
                status = eQStagesInterfaceIOSubscriber.CopyFromTopicQos(ref eQStagesInterfaceIODrQos, reliableTopicQos);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  eQStagesInterfaceIOSubscriber.CopyFromTopicQos - Error");                    // Roy*180612

                eQStagesInterfaceIODrQos.History.Kind = HistoryQosPolicyKind.KeepLastHistoryQos;
                //eQStagesInterfaceIODrQos.Durability.Kind = DurabilityQosPolicyKind.PersistentDurabilityQos;
                eQStagesInterfaceIODrQos.Durability.Kind = DurabilityQosPolicyKind.TransientDurabilityQos;
                eQStagesInterfaceIODrQos.DestinationOrder.Kind = DestinationOrderQosPolicyKind.ByReceptionTimestampDestinationorderQos;
                eQStagesInterfaceIODrQos.Reliability.Kind = ReliabilityQosPolicyKind.ReliableReliabilityQos;                     // Roy+180319 ... 補強DDS怪問題 ...  

                //~~~~~~~~~~~~

                interVehiclesBlockZonesControlSubscriber.GetDefaultDataReaderQos(ref interVehiclesBlockZonesControlDrQos);
                status = interVehiclesBlockZonesControlSubscriber.CopyFromTopicQos(ref interVehiclesBlockZonesControlDrQos, reliableTopicQos);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  interVehiclesBlockZonesControlSubscriber.CopyFromTopicQos - Error");                    // Roy*180612

                interVehiclesBlockZonesControlDrQos.History.Kind = HistoryQosPolicyKind.KeepLastHistoryQos;
                //interVehiclesBlockZonesControlDrQos.Durability.Kind = DurabilityQosPolicyKind.PersistentDurabilityQos;                // Roy-180918
                interVehiclesBlockZonesControlDrQos.Durability.Kind = DurabilityQosPolicyKind.TransientDurabilityQos;                // Roy+180918
                
                interVehiclesBlockZonesControlDrQos.DestinationOrder.Kind = DestinationOrderQosPolicyKind.ByReceptionTimestampDestinationorderQos;
                interVehiclesBlockZonesControlDrQos.Reliability.Kind = ReliabilityQosPolicyKind.ReliableReliabilityQos;                     // Roy+180319 ... 補強DDS怪問題 ...  
                //++++++++++++++++++++++++++++++++++             

                //
                /* Create a DataReader for the Client Topic (using the appropriate QoS). */
                IDataReader parentReader = motionInfoSubscriber.CreateDataReader(motionInfo_ClientTopic, motionInfoDrQos);                   // Roy*171204
                ErrorHandler.checkHandle(parentReader, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoSubscriber.CreateDataReader /motionInfo_ClientTopic - Error");                    // Roy*180612
                DDS_Global.motionInfo_ClientReader = parentReader as MotionInfo_ClientDataReader;

                //
                /* Create a DataReader for the Server Topic (using the same QoS). */
                parentReader = motionInfoSubscriber.CreateDataReader(motionInfo_ServerTopic, motionInfoDrQos);                   // Roy*171204
                ErrorHandler.checkHandle(parentReader, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoSubscriber.CreateDataReader /motionInfo_ServerTopic - Error");                    // Roy*180612
                DDS_Global.motionInfo_ServerReader = parentReader as MotionInfo_ServerDataReader;


                //+++++++++++++++               // Roy+180205
                /* Create a DataReader for the Between_Vehicle_Data Topic (using the same QoS). */
                //parentReader = betweenVehicleDataSubscriber.CreateDataReader(between_Vehicle_DataTopic, motionInfoDrQos);                   // Roy-180222
                parentReader = betweenVehicleDataSubscriber.CreateDataReader(between_Vehicle_DataTopic, betweenVehicleDataDrQos);                   // Roy+180222
                ErrorHandler.checkHandle(parentReader, "@DDS_SetUp:  Initial  \\   <DDS>  betweenVehicleDataSubscriber.CreateDataReader /between_Vehicle_DataTopic - Error");                    // Roy*180612
                DDS_Global.between_Vehicle_DataReader = parentReader as Between_Vehicle_DataDataReader;
                //+++++++++++++++             

                //++++++++++++++++++++++++++++++++++               // Roy+180222
                /* Create a DataReader for the LoadPort_PIO_FromVehicle Topic (using the same QoS). */
                parentReader = loadPortPioFromVehicleSubscriber.CreateDataReader(loadPort_PIO_FromVehicleTopic, loadPortPioFromVehicleDrQos);
                ErrorHandler.checkHandle(parentReader, "@DDS_SetUp:  Initial  \\   <DDS>  loadPortPioFromVehicleSubscriber.CreateDataReader /loadPort_PIO_FromVehicleTopic - Error");                    // Roy*180612
                DDS_Global.loadPort_PIO_FromVehicleReader = parentReader as LoadPort_PIO_FromVehicleDataReader;

                //~~~~~~~~~~~~

                /* Create a DataReader for the LoadPort_PIO_ToVehicle Topic (using the same QoS). */
                parentReader = loadPortPioToVehicleSubscriber.CreateDataReader(loadPort_PIO_ToVehicleTopic, loadPortPioToVehicleDrQos);
                ErrorHandler.checkHandle(parentReader, "@DDS_SetUp:  Initial  \\   <DDS>  loadPortPioToVehicleSubscriber.CreateDataReader /loadPort_PIO_ToVehicleTopic - Error");                    // Roy*180612
                DDS_Global.loadPort_PIO_ToVehicleReader = parentReader as LoadPort_PIO_ToVehicleDataReader;

                //~~~~~~~~~~~~

                /* Create a DataReader for the EQ_Stages_Interface_IO Topic (using the same QoS). */
                parentReader = eQStagesInterfaceIOSubscriber.CreateDataReader(eQ_Stages_Interface_IOTopic, eQStagesInterfaceIODrQos);
                ErrorHandler.checkHandle(parentReader, "@DDS_SetUp:  Initial  \\   <DDS>  eQStagesInterfaceIOSubscriber.CreateDataReader /eQ_Stages_Interface_IOTopic - Error");                    // Roy*180612
                DDS_Global.eQ_Stages_Interface_IOReader = parentReader as EQ_Stages_Interface_IODataReader;

                //~~~~~~~~~~~~

                /* Create a DataReader for the InterVehicles_BlockZones_Control Topic (using the same QoS). */
                parentReader = interVehiclesBlockZonesControlSubscriber.CreateDataReader(interVehicles_BlockZones_ControlTopic, interVehiclesBlockZonesControlDrQos);
                ErrorHandler.checkHandle(parentReader, "@DDS_SetUp:  Initial  \\   <DDS>  interVehiclesBlockZonesControlSubscriber.CreateDataReader /interVehicles_BlockZones_ControlTopic - Error");                    // Roy*180612
                DDS_Global.interVehicles_BlockZones_ControlReader = parentReader as InterVehicles_BlockZones_ControlDataReader;
                //++++++++++++++++++++++++++++++++++           
                #endregion



                DDS.DataReaderQos xDrQos = new DDS.DataReaderQos();                                     // Roy+171204
                DDS_Global.motionInfo_ServerReader.GetQos(ref xDrQos);                   // Roy+171204




                #region MotionInfo_Vehicle_Comm Topic Data Reader for Local NetWork
                /* Create a DataReader for the Server Topic (using the same QoS). */
                status = motionInfoVehCommSubscriber.GetDefaultDataReaderQos(ref motionInfoDrQos_local);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehCommSubscriber.GetDefaultDataReaderQos - Error");                    // Roy*180612

                //
                status = motionInfoSubscriber.CopyFromTopicQos(ref motionInfoDrQos_local, reliableTopicQos_local);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoSubscriber.CopyFromTopicQos - Error");                    // Roy*180612
                //
                motionInfoDrQos_local.History.Kind = HistoryQosPolicyKind.KeepLastHistoryQos;
                motionInfoDrQos_local.Reliability.Kind = ReliabilityQosPolicyKind.ReliableReliabilityQos;                     // Roy+180319 ... 補強DDS怪問題 ...  

                //
                IDataReader parentDataReader_local = motionInfoVehCommSubscriber.CreateDataReader(motionInfo_VehCommTopic, motionInfoDrQos_local);                   // Roy*171204
                ErrorHandler.checkHandle(parentDataReader_local, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehCommSubscriber.CreateDataReader /motionInfo_VehCommTopic - Error");                    // Roy*180612
                DDS_Global.motionInfo_VehCommReader = parentDataReader_local as MotionInfo_Vehicle_CommDataReader;
                //ErrorHandler.checkHandle(DDS_Global.motionInfo_VehCommReader, "DDS_Global.motionInfo_VehCommReader");                 // Roy-180612
                #endregion

                #region MotionInfo_Vehicle_InterComm_SendData_Topic Data Reader for Local NetWork
                /* Create a DataReader for the Server Topic (using the same QoS). */
                status = motionInfoVehInterCommSendDataSubscriber.GetDefaultDataReaderQos(ref motionInfoDrQos_local);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehInterCommSendDataSubscriber.GetDefaultDataReaderQos - Error");                    // Roy*180612

                //
                status = motionInfoVehInterCommSendDataSubscriber.CopyFromTopicQos(ref motionInfoDrQos_local, reliableTopicQos_local);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehInterCommSendDataSubscriber.CopyFromTopicQos - Error");                    // Roy*180612

                //
                motionInfoDrQos_local.History.Kind = HistoryQosPolicyKind.KeepLastHistoryQos;
                motionInfoDrQos_local.Reliability.Kind = ReliabilityQosPolicyKind.ReliableReliabilityQos;                     // Roy+180319 ... 補強DDS怪問題 ...  

                //
                parentDataReader_local = motionInfoVehInterCommSendDataSubscriber.CreateDataReader(motionInfo_VehInterCommSendDataTopic, motionInfoDrQos_local);                   // Roy*171204
                ErrorHandler.checkHandle(parentDataReader_local, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehInterCommSendDataSubscriber.CreateDataReader /motionInfo_VehInterCommSendDataTopic - Error");                    // Roy*180612
                DDS_Global.motionInfo_VehInterCommSendDataReader = parentDataReader_local as MotionInfo_Inter_Comm_SendDataDataReader;
                //ErrorHandler.checkHandle(DDS_Global.motionInfo_VehInterCommSendDataReader, "DDS_Global.motionInfo_VehInterCommSendDataReader");               // Roy-180612
                #endregion

                #region MotionInfo_Vehicle_InterComm_ReptData_Topic Data Reader for Local NetWork
                /* Create a DataReader for the Server Topic (using the same QoS). */
                status = motionInfoVehInterCommReptDataSubscriber.GetDefaultDataReaderQos(ref motionInfoDrQos_local);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehInterCommReptDataSubscriber.GetDefaultDataReaderQos - Error");                    // Roy*180612

                //
                status = motionInfoVehInterCommReptDataSubscriber.CopyFromTopicQos(ref motionInfoDrQos_local, reliableTopicQos_local);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehInterCommReptDataSubscriber.CopyFromTopicQos - Error");                    // Roy*180612

                //
                motionInfoDrQos_local.History.Kind = HistoryQosPolicyKind.KeepLastHistoryQos;
                motionInfoDrQos_local.Reliability.Kind = ReliabilityQosPolicyKind.ReliableReliabilityQos;                     // Roy+180319 ... 補強DDS怪問題 ...  
                motionInfoDrQos_local.History.Depth = 10;
                //
                parentDataReader_local = motionInfoVehInterCommReptDataSubscriber.CreateDataReader(motionInfo_VehInterCommReptDataTopic, motionInfoDrQos_local);                   // Roy*171204
                ErrorHandler.checkHandle(parentDataReader_local, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehInterCommReptDataSubscriber.CreateDataReader /motionInfo_VehInterCommReptDataTopic - Error");                    // Roy*180612
                DDS_Global.motionInfo_VehInterCommReptDataReader = parentDataReader_local as MotionInfo_Vehicle_Inter_Comm_ReportDataDataReader;
                //ErrorHandler.checkHandle(DDS_Global.motionInfo_VehInterCommReptDataReader, "DDS_Global.motionInfo_VehInterCommReptDataReader");               // Roy-180612
                #endregion

                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++                 // Roy+191113
                #region MotionInfo_Vehicle_InterComm_ReptData_134Topic Data Reader for Local NetWork
                /* Create a DataReader for the Server Topic (using the same QoS). */
                status = motionInfoVehInterCommReptData_134Subscriber.GetDefaultDataReaderQos(ref motionInfoDrQos_local);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehInterCommReptData_134Subscriber.GetDefaultDataReaderQos - Error");              

                //
                status = motionInfoVehInterCommReptData_134Subscriber.CopyFromTopicQos(ref motionInfoDrQos_local, reliableTopicQos_local);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehInterCommReptData_134Subscriber.CopyFromTopicQos - Error");              

                //
                motionInfoDrQos_local.History.Kind = HistoryQosPolicyKind.KeepLastHistoryQos;
                motionInfoDrQos_local.Reliability.Kind = ReliabilityQosPolicyKind.ReliableReliabilityQos;                     // ... 補強DDS怪問題 ...  
                motionInfoDrQos_local.History.Depth = 10;
                //
                parentDataReader_local = motionInfoVehInterCommReptData_134Subscriber.CreateDataReader(motionInfo_VehInterCommReptData_134Topic, motionInfoDrQos_local);            
                ErrorHandler.checkHandle(parentDataReader_local, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehInterCommReptData_134Subscriber.CreateDataReader /motionInfo_VehInterCommReptData_134Topic - Error");      
                DDS_Global.motionInfo_VehInterCommReptData_134Reader = parentDataReader_local as MotionInfo_Vehicle_Inter_Comm_ReportData_134DataReader;
                //ErrorHandler.checkHandle(DDS_Global.motionInfo_VehInterCommReptData_134Reader, "DDS_Global.motionInfo_VehInterCommReptData_134Reader");           
                #endregion
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                #region MotionInfo_HandShake_SendData_Topic Data Reader for Local NetWork
                /* Create a DataReader for the Server Topic (using the same QoS). */
                status = motionInfoHandShakeSendDataSubscriber.GetDefaultDataReaderQos(ref motionInfoDrQos_local);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoHandShakeSendDataSubscriber.GetDefaultDataReaderQos - Error");                    // Roy*180612

                //
                status = motionInfoHandShakeSendDataSubscriber.CopyFromTopicQos(ref motionInfoDrQos_local, reliableTopicQos_local);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoHandShakeSendDataSubscriber.CopyFromTopicQos - Error");                    // Roy*180612

                //
                motionInfoDrQos_local.History.Kind = HistoryQosPolicyKind.KeepLastHistoryQos;
                motionInfoDrQos_local.Reliability.Kind = ReliabilityQosPolicyKind.ReliableReliabilityQos;                     // Roy+180319 ... 補強DDS怪問題 ...  

                //
                parentDataReader_local = motionInfoHandShakeSendDataSubscriber.CreateDataReader(motionInfo_HandShakeSendDataTopic, motionInfoDrQos_local);                   // Roy*171204
                ErrorHandler.checkHandle(parentDataReader_local, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoHandShakeSendDataSubscriber.CreateDataReader /motionInfo_HandShakeSendDataTopic - Error");                    // Roy*180612
                DDS_Global.motionInfo_HandShakeSendDataReader = parentDataReader_local as MotionInfo_HandShake_SendDataDataReader;
                //ErrorHandler.checkHandle(DDS_Global.motionInfo_HandShakeSendDataReader, "DDS_Global.motionInfo_HandShakeSendDataReader");             // Roy-180612
                #endregion

                #region MotionInfo_HandShake_RecieveData_Topic Data Reader for Local NetWork
                /* Create a DataReader for the Server Topic (using the same QoS). */
                status = motionInfoHandShakeRecieveDataSubscriber.GetDefaultDataReaderQos(ref motionInfoDrQos_local);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoHandShakeRecieveDataSubscriber.GetDefaultDataReaderQos - Error");                    // Roy*180612

                //
                status = motionInfoHandShakeRecieveDataSubscriber.CopyFromTopicQos(ref motionInfoDrQos_local, reliableTopicQos_local);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoHandShakeRecieveDataSubscriber.CopyFromTopicQos - Error");                    // Roy*180612
                //
                motionInfoDrQos_local.History.Kind = HistoryQosPolicyKind.KeepLastHistoryQos;
                motionInfoDrQos_local.Reliability.Kind = ReliabilityQosPolicyKind.ReliableReliabilityQos;                     // Roy+180319 ... 補強DDS怪問題 ...  

                //
                parentDataReader_local = motionInfoHandShakeRecieveDataSubscriber.CreateDataReader(motionInfo_HandShakeRecieveDataTopic, motionInfoDrQos_local);                   // Roy*171204
                ErrorHandler.checkHandle(parentDataReader_local, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoHandShakeRecieveDataSubscriber.CreateDataReader /motionInfo_HandShakeRecieveDataTopic - Error");                    // Roy*180612
                DDS_Global.motionInfo_HandShakeRecieveDataReader = parentDataReader_local as MotionInfo_HandShake_RecieveDataDataReader;
                //ErrorHandler.checkHandle(DDS_Global.motionInfo_HandShakeRecieveDataReader, "DDS_Global.motionInfo_HandShakeRecieveDataReader");               // Roy-180612
                #endregion


                #region  Create a DataWriter for the MotionInfo_Client/Server Topic (using the appropriate QoS).
                // Get Data Writer Qos for MotionInfo_Server and Client
                motionInfoPublisher.GetDefaultDataWriterQos(ref motionInfoDwQos);

                status = motionInfoPublisher.CopyFromTopicQos(ref motionInfoDwQos, reliableTopicQos);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoPublisher.CopyFromTopicQos - Error");                    // Roy*180612

                //
                WriterDataLifecycleQosPolicy writerDataLifecycle = motionInfoDwQos.WriterDataLifecycle;
                writerDataLifecycle.AutodisposeUnregisteredInstances = false;

                motionInfoDwQos.History.Kind = HistoryQosPolicyKind.KeepLastHistoryQos;                     // Roy+180131 
                //motionInfoDwQos.Durability.Kind = DurabilityQosPolicyKind.PersistentDurabilityQos;                     // Roy+180131                 // Roy-180918
                motionInfoDwQos.Durability.Kind = DurabilityQosPolicyKind.TransientDurabilityQos;                     // Roy+180918

                //motionInfoDwQos.DestinationOrder.Kind = DestinationOrderQosPolicyKind.BySourceTimestampDestinationorderQos;                     // Roy+180131               //Roy-180205
                motionInfoDwQos.DestinationOrder.Kind = DestinationOrderQosPolicyKind.ByReceptionTimestampDestinationorderQos;                     // Roy+180205
                motionInfoDwQos.Reliability.Kind = ReliabilityQosPolicyKind.ReliableReliabilityQos;                     // Roy+180319 ... 補強DDS怪問題 ...  

                //+++++++++++++++               // Roy+180205
                betweenVehicleDataPublisher.GetDefaultDataWriterQos(ref betweenVehicleDataDwQos);
                status = betweenVehicleDataPublisher.CopyFromTopicQos(ref betweenVehicleDataDwQos, reliableTopicQos);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  betweenVehicleDataPublisher.CopyFromTopicQos - Error");                    // Roy*180612

                WriterDataLifecycleQosPolicy writerDataLifecycle2 = betweenVehicleDataDwQos.WriterDataLifecycle;
                writerDataLifecycle2.AutodisposeUnregisteredInstances = false;

                betweenVehicleDataDwQos.History.Kind = HistoryQosPolicyKind.KeepLastHistoryQos;
                //betweenVehicleDataDwQos.Durability.Kind = DurabilityQosPolicyKind.PersistentDurabilityQos;                    // Roy+180222                 // Roy-180918
                betweenVehicleDataDwQos.Durability.Kind = DurabilityQosPolicyKind.TransientDurabilityQos;                    // Roy+180918

                betweenVehicleDataDwQos.DestinationOrder.Kind = DestinationOrderQosPolicyKind.ByReceptionTimestampDestinationorderQos;
                betweenVehicleDataDwQos.Reliability.Kind = ReliabilityQosPolicyKind.ReliableReliabilityQos;                     // Roy+180319 ... 補強DDS怪問題 ...  
                //+++++++++++++++             


                //++++++++++++++++++++++++++++++++++               // Roy+180222
                loadPortPioFromVehiclePublisher.GetDefaultDataWriterQos(ref loadPortPioFromVehicleDwQos);
                status = loadPortPioFromVehiclePublisher.CopyFromTopicQos(ref loadPortPioFromVehicleDwQos, reliableTopicQos);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  loadPortPioFromVehiclePublisher.CopyFromTopicQos - Error");                    // Roy*180612

                writerDataLifecycle2 = loadPortPioFromVehicleDwQos.WriterDataLifecycle;
                writerDataLifecycle2.AutodisposeUnregisteredInstances = false;

                loadPortPioFromVehicleDwQos.History.Kind = HistoryQosPolicyKind.KeepLastHistoryQos;
                //loadPortPioFromVehicleDwQos.Durability.Kind = DurabilityQosPolicyKind.PersistentDurabilityQos;
                loadPortPioFromVehicleDwQos.Durability.Kind = DurabilityQosPolicyKind.TransientDurabilityQos;
                loadPortPioFromVehicleDwQos.DestinationOrder.Kind = DestinationOrderQosPolicyKind.ByReceptionTimestampDestinationorderQos;
                loadPortPioFromVehicleDwQos.Reliability.Kind = ReliabilityQosPolicyKind.ReliableReliabilityQos;                     // Roy+180319 ... 補強DDS怪問題 ...  

                //~~~~~~~~~~~~

                loadPortPioToVehiclePublisher.GetDefaultDataWriterQos(ref loadPortPioToVehicleDwQos);
                status = loadPortPioToVehiclePublisher.CopyFromTopicQos(ref loadPortPioToVehicleDwQos, reliableTopicQos);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  loadPortPioToVehiclePublisher.CopyFromTopicQos - Error");                    // Roy*180612

                writerDataLifecycle2 = loadPortPioToVehicleDwQos.WriterDataLifecycle;
                writerDataLifecycle2.AutodisposeUnregisteredInstances = false;

                loadPortPioToVehicleDwQos.History.Kind = HistoryQosPolicyKind.KeepLastHistoryQos;
                //loadPortPioToVehicleDwQos.Durability.Kind = DurabilityQosPolicyKind.PersistentDurabilityQos;
                loadPortPioToVehicleDwQos.Durability.Kind = DurabilityQosPolicyKind.TransientDurabilityQos;
                loadPortPioToVehicleDwQos.DestinationOrder.Kind = DestinationOrderQosPolicyKind.ByReceptionTimestampDestinationorderQos;
                loadPortPioToVehicleDwQos.Reliability.Kind = ReliabilityQosPolicyKind.ReliableReliabilityQos;                     // Roy+180319 ... 補強DDS怪問題 ...  

                //~~~~~~~~~~~~

                eQStagesInterfaceIOPublisher.GetDefaultDataWriterQos(ref eQStagesInterfaceIODwQos);
                status = eQStagesInterfaceIOPublisher.CopyFromTopicQos(ref eQStagesInterfaceIODwQos, reliableTopicQos);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  eQStagesInterfaceIOPublisher.CopyFromTopicQos - Error");                    // Roy*180612

                writerDataLifecycle2 = eQStagesInterfaceIODwQos.WriterDataLifecycle;
                writerDataLifecycle2.AutodisposeUnregisteredInstances = false;

                eQStagesInterfaceIODwQos.History.Kind = HistoryQosPolicyKind.KeepLastHistoryQos;
                //eQStagesInterfaceIODwQos.Durability.Kind = DurabilityQosPolicyKind.PersistentDurabilityQos;
                eQStagesInterfaceIODwQos.Durability.Kind = DurabilityQosPolicyKind.TransientDurabilityQos;
                eQStagesInterfaceIODwQos.DestinationOrder.Kind = DestinationOrderQosPolicyKind.ByReceptionTimestampDestinationorderQos;
                eQStagesInterfaceIODwQos.Reliability.Kind = ReliabilityQosPolicyKind.ReliableReliabilityQos;                     // Roy+180319 ... 補強DDS怪問題 ...  

                //~~~~~~~~~~~~

                interVehiclesBlockZonesControlPublisher.GetDefaultDataWriterQos(ref interVehiclesBlockZonesControlDwQos);
                status = interVehiclesBlockZonesControlPublisher.CopyFromTopicQos(ref interVehiclesBlockZonesControlDwQos, reliableTopicQos);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  interVehiclesBlockZonesControlPublisher.CopyFromTopicQos - Error");                    // Roy*180612

                writerDataLifecycle2 = interVehiclesBlockZonesControlDwQos.WriterDataLifecycle;
                writerDataLifecycle2.AutodisposeUnregisteredInstances = false;

                interVehiclesBlockZonesControlDwQos.History.Kind = HistoryQosPolicyKind.KeepLastHistoryQos;
                //interVehiclesBlockZonesControlDwQos.Durability.Kind = DurabilityQosPolicyKind.PersistentDurabilityQos;                 // Roy-180918
                interVehiclesBlockZonesControlDwQos.Durability.Kind = DurabilityQosPolicyKind.TransientDurabilityQos;                 // Roy+180918

                interVehiclesBlockZonesControlDwQos.DestinationOrder.Kind = DestinationOrderQosPolicyKind.ByReceptionTimestampDestinationorderQos;
                interVehiclesBlockZonesControlDwQos.Reliability.Kind = ReliabilityQosPolicyKind.ReliableReliabilityQos;                     // Roy+180319 ... 補強DDS怪問題 ...  
                //++++++++++++++++++++++++++++++++++    


                //
                /* Create Datat Writer for the Client/Server Topics */
                IDataWriter parentWriter = motionInfoPublisher.CreateDataWriter(motionInfo_ClientTopic, motionInfoDwQos);
                ErrorHandler.checkHandle(parentWriter, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoPublisher.CreateDataWriter /motionInfo_ClientTopic - Error");                    // Roy*180612

                /* Narrow the abstract parent into its typed representative. */
                DDS_Global.motionInfo_ClientWriter = parentWriter as MotionInfo_ClientDataWriter;
                //ErrorHandler.checkHandle(DDS_Global.motionInfo_ClientWriter, "MotionInfo_ClientDataWriter");              // Roy-180612

                //
                parentWriter = motionInfoPublisher.CreateDataWriter(motionInfo_ServerTopic, motionInfoDwQos);
                ErrorHandler.checkHandle(parentWriter, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoPublisher.CreateDataWriter /motionInfo_ServerTopic - Error");                    // Roy*180612
                DDS_Global.motionInfo_ServerWriter = parentWriter as MotionInfo_ServerDataWriter;
                //ErrorHandler.checkHandle(DDS_Global.motionInfo_ServerWriter, "MotionInfo_ServerDataWriter");                    // Roy+171204             // Roy-180612

                //+++++++++++++++               // Roy+180205
                parentWriter = betweenVehicleDataPublisher.CreateDataWriter(between_Vehicle_DataTopic, betweenVehicleDataDwQos);
                ErrorHandler.checkHandle(parentWriter, "@DDS_SetUp:  Initial  \\   <DDS>  betweenVehicleDataPublisher.CreateDataWriter /between_Vehicle_DataTopic - Error");                    // Roy*180612
                DDS_Global.between_Vehicle_DataWriter = parentWriter as Between_Vehicle_DataDataWriter;
                //ErrorHandler.checkHandle(DDS_Global.between_Vehicle_DataWriter, "Between_Vehicle_DataDataWriter");                // Roy-180612
                //+++++++++++++++          

                //++++++++++++++++++++++++++++++++++               // Roy+180222
                parentWriter = loadPortPioFromVehiclePublisher.CreateDataWriter(loadPort_PIO_FromVehicleTopic, loadPortPioFromVehicleDwQos);
                ErrorHandler.checkHandle(parentWriter, "@DDS_SetUp:  Initial  \\   <DDS>  loadPortPioFromVehiclePublisher.CreateDataWriter /loadPort_PIO_FromVehicleTopic - Error");                    // Roy*180612
                DDS_Global.loadPort_PIO_FromVehicleWriter = parentWriter as LoadPort_PIO_FromVehicleDataWriter;
                //ErrorHandler.checkHandle(DDS_Global.loadPort_PIO_FromVehicleWriter, "LoadPort_PIO_FromVehicleDataWriter");                // Roy-180612

                //~~~~~~~~~~~~

                parentWriter = loadPortPioToVehiclePublisher.CreateDataWriter(loadPort_PIO_ToVehicleTopic, loadPortPioToVehicleDwQos);
                ErrorHandler.checkHandle(parentWriter, "@DDS_SetUp:  Initial  \\   <DDS>  loadPortPioToVehiclePublisher.CreateDataWriter /loadPort_PIO_ToVehicleTopic - Error");                    // Roy*180612
                DDS_Global.loadPort_PIO_ToVehicleWriter = parentWriter as LoadPort_PIO_ToVehicleDataWriter;
                //ErrorHandler.checkHandle(DDS_Global.loadPort_PIO_ToVehicleWriter, "LoadPort_PIO_ToVehicleDataWriter");                // Roy-180612

                //~~~~~~~~~~~~

                parentWriter = eQStagesInterfaceIOPublisher.CreateDataWriter(eQ_Stages_Interface_IOTopic, eQStagesInterfaceIODwQos);
                ErrorHandler.checkHandle(parentWriter, "@DDS_SetUp:  Initial  \\   <DDS>  eQStagesInterfaceIOPublisher.CreateDataWriter /eQ_Stages_Interface_IOTopic - Error");                    // Roy*180612
                DDS_Global.eQ_Stages_Interface_IOWriter = parentWriter as EQ_Stages_Interface_IODataWriter;
                //ErrorHandler.checkHandle(DDS_Global.eQ_Stages_Interface_IOWriter, "EQ_Stages_Interface_IODataWriter");                // Roy-180612

                //~~~~~~~~~~~~

                parentWriter = interVehiclesBlockZonesControlPublisher.CreateDataWriter(interVehicles_BlockZones_ControlTopic, interVehiclesBlockZonesControlDwQos);
                ErrorHandler.checkHandle(parentWriter, "@DDS_SetUp:  Initial  \\   <DDS>  interVehiclesBlockZonesControlPublisher.CreateDataWriter /interVehicles_BlockZones_ControlTopic - Error");                    // Roy*180612
                DDS_Global.interVehicles_BlockZones_ControlWriter = parentWriter as InterVehicles_BlockZones_ControlDataWriter;
                //ErrorHandler.checkHandle(DDS_Global.interVehicles_BlockZones_ControlWriter, "InterVehicles_BlockZones_ControlDataWriter");                // Roy-180612
                //++++++++++++++++++++++++++++++++++            
                #endregion



                DDS.DataWriterQos xDwQos = new DDS.DataWriterQos();                                     // Roy+171204
                DDS_Global.motionInfo_ServerWriter.GetQos(ref xDwQos);                   // Roy+171204



                #region Create a DataWriter for the MotionInfo_VehComm
                //Get Data Writer Qos for MotionInfo_VehComm
                motionInfoVehCommPublisher.GetDefaultDataWriterQos(ref motionInfoDwQos_local);
                ErrorHandler.checkHandle(motionInfoDwQos_local, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehCommPublisher.GetDefaultDataWriterQos - Error");                    // Roy*180612

                status = motionInfoVehCommPublisher.CopyFromTopicQos(ref motionInfoDwQos_local, reliableTopicQos_local);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehCommPublisher.CopyFromTopicQos - Error");                    // Roy*180612

                //
                WriterDataLifecycleQosPolicy writerDataLifecycle_local = motionInfoDwQos_local.WriterDataLifecycle;
                writerDataLifecycle_local.AutodisposeUnregisteredInstances = false;

                //
                /* Create Datat Reader for the MotionInf_Veh_Comm Topics */
                IDataWriter parentWriter_local = motionInfoVehCommPublisher.CreateDataWriter(motionInfo_VehCommTopic, motionInfoDwQos_local);
                ErrorHandler.checkHandle(parentWriter_local, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehCommPublisher.CreateDataWriter /motionInfo_VehCommTopic - Error");                    // Roy*180612
                DDS_Global.motionInfo_VehCommWriter = parentWriter_local as MotionInfo_Vehicle_CommDataWriter;
                //ErrorHandler.checkHandle(DDS_Global.motionInfo_VehCommWriter, "MotionInfo_Vehicle_CommDataWriter");                    // Roy+171204              // Roy-180612
                #endregion

                #region Create a DataWriter for the MotionInfo_VehInterComm_SendData
                //Get Data Writer Qos for MotionInfo_VehComm
                motionInfoVehInterCommSendDataPublisher.GetDefaultDataWriterQos(ref motionInfoDwQos_local);
                ErrorHandler.checkHandle(motionInfoDwQos_local, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehInterCommSendDataPublisher.GetDefaultDataWriterQos - Error");                    // Roy*180612

                status = motionInfoVehInterCommSendDataPublisher.CopyFromTopicQos(ref motionInfoDwQos_local, reliableTopicQos_local);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehInterCommSendDataPublisher.CopyFromTopicQos - Error");                    // Roy*180612

                //
                writerDataLifecycle_local = motionInfoDwQos_local.WriterDataLifecycle;
                writerDataLifecycle_local.AutodisposeUnregisteredInstances = false;

                //
                /* Create Datat Reader for the MotionInf_Veh_Comm Topics */
                parentWriter_local = motionInfoVehInterCommSendDataPublisher.CreateDataWriter(motionInfo_VehInterCommSendDataTopic, motionInfoDwQos_local);
                ErrorHandler.checkHandle(parentWriter_local, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehInterCommSendDataPublisher.CreateDataWriter /motionInfo_VehInterCommSendDataTopic - Error");                    // Roy*180612
                DDS_Global.motionInfo_VehInterCommSendDataWriter = parentWriter_local as MotionInfo_Inter_Comm_SendDataDataWriter;
                //ErrorHandler.checkHandle(DDS_Global.motionInfo_VehInterCommSendDataWriter, "MotionInfo_Inter_Comm_SendDataDataWriter");                    // Roy+171204              // Roy-180612
                #endregion

                #region Create a DataWriter for the MotionInfo_VehInterComm_ReptData
                //Get Data Writer Qos for MotionInfo_VehComm
                motionInfoVehInterCommReptDataPublisher.GetDefaultDataWriterQos(ref motionInfoDwQos_local);
                ErrorHandler.checkHandle(motionInfoDwQos_local, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehInterCommReptDataPublisher.GetDefaultDataWriterQos - Error");                    // Roy*180612

                status = motionInfoVehInterCommReptDataPublisher.CopyFromTopicQos(ref motionInfoDwQos_local, reliableTopicQos_local);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehInterCommReptDataPublisher.CopyFromTopicQos - Error");                    // Roy*180612

                //
                writerDataLifecycle_local = motionInfoDwQos_local.WriterDataLifecycle;
                writerDataLifecycle_local.AutodisposeUnregisteredInstances = false;

                //
                /* Create Datat Reader for the MotionInf_Veh_Comm Topics */
                parentWriter_local = motionInfoVehInterCommReptDataPublisher.CreateDataWriter(motionInfo_VehInterCommReptDataTopic, motionInfoDwQos_local);
                ErrorHandler.checkHandle(parentWriter_local, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehInterCommReptDataPublisher.CreateDataWriter /motionInfo_VehInterCommReptDataTopic - Error");                    // Roy*180612
                DDS_Global.motionInfo_VehInterCommReptDataWriter = parentWriter_local as MotionInfo_Vehicle_Inter_Comm_ReportDataDataWriter;
                //ErrorHandler.checkHandle(DDS_Global.motionInfo_VehInterCommReptDataWriter, "MotionInfo_Vehicle_Inter_Comm_ReportDataDataWriter");                    // Roy+171204                // Roy-180612
                #endregion

                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++                 // Roy+191113
                #region Create a DataWriter for the MotionInfo_VehInterComm_ReptData_134
                //Get Data Writer Qos for MotionInfo_VehComm
                motionInfoVehInterCommReptData_134Publisher.GetDefaultDataWriterQos(ref motionInfoDwQos_local);
                ErrorHandler.checkHandle(motionInfoDwQos_local, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehInterCommReptData_134Publisher.GetDefaultDataWriterQos - Error");          

                status = motionInfoVehInterCommReptData_134Publisher.CopyFromTopicQos(ref motionInfoDwQos_local, reliableTopicQos_local);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehInterCommReptData_134Publisher.CopyFromTopicQos - Error");              

                //
                writerDataLifecycle_local = motionInfoDwQos_local.WriterDataLifecycle;
                writerDataLifecycle_local.AutodisposeUnregisteredInstances = false;

                //
                /* Create Datat Reader for the MotionInf_Veh_Comm Topics */
                parentWriter_local = motionInfoVehInterCommReptData_134Publisher.CreateDataWriter(motionInfo_VehInterCommReptData_134Topic, motionInfoDwQos_local);
                ErrorHandler.checkHandle(parentWriter_local, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoVehInterCommReptData_134Publisher.CreateDataWriter /motionInfo_VehInterCommReptDataTopic_134 - Error");         
                DDS_Global.motionInfo_VehInterCommReptData_134Writer = parentWriter_local as MotionInfo_Vehicle_Inter_Comm_ReportData_134DataWriter;
                //ErrorHandler.checkHandle(DDS_Global.motionInfo_VehInterCommReptData_134Writer, "MotionInfo_Vehicle_Inter_Comm_ReportData_134DataWriter");          
                #endregion
                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++              

                #region Create a DataWriter for the MotionInfo_HandShake_SendData
                //Get Data Writer Qos for MotionInfo_VehComm
                motionInfoHandShakeSendDataPublisher.GetDefaultDataWriterQos(ref motionInfoDwQos_local);
                ErrorHandler.checkHandle(motionInfoDwQos_local, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoHandShakeSendDataPublisher.GetDefaultDataWriterQos - Error");                    // Roy*180612

                status = motionInfoHandShakeSendDataPublisher.CopyFromTopicQos(ref motionInfoDwQos_local, reliableTopicQos_local);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoHandShakeSendDataPublisher.CopyFromTopicQos - Error");                    // Roy*180612

                //
                writerDataLifecycle_local = motionInfoDwQos_local.WriterDataLifecycle;
                writerDataLifecycle_local.AutodisposeUnregisteredInstances = false;

                //
                /* Create Datat Reader for the MotionInf_Veh_Comm Topics */
                parentWriter_local = motionInfoHandShakeSendDataPublisher.CreateDataWriter(motionInfo_HandShakeSendDataTopic, motionInfoDwQos_local);
                ErrorHandler.checkHandle(parentWriter_local, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoHandShakeSendDataPublisher.CreateDataWriter /motionInfo_HandShakeSendDataTopic - Error");                    // Roy*180612
                DDS_Global.motionInfo_HandShakeSendDataWriter = parentWriter_local as MotionInfo_HandShake_SendDataDataWriter;
                //ErrorHandler.checkHandle(DDS_Global.motionInfo_HandShakeSendDataWriter, "MotionInfo_HandShake_SendDataDataWriter");                    // Roy+171204              // Roy-180612
                #endregion

                #region Create a DataWriter for the MotionInfo_HandShake_RecieveData
                //Get Data Writer Qos for MotionInfo_VehComm
                motionInfoHandShakeRecieveDataPublisher.GetDefaultDataWriterQos(ref motionInfoDwQos_local);
                ErrorHandler.checkHandle(motionInfoDwQos_local, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoHandShakeRecieveDataPublisher.GetDefaultDataWriterQos - Error");                    // Roy*180612

                status = motionInfoHandShakeRecieveDataPublisher.CopyFromTopicQos(ref motionInfoDwQos_local, reliableTopicQos_local);
                ErrorHandler.checkStatus(status, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoHandShakeRecieveDataPublisher.CopyFromTopicQos - Error");                    // Roy*180612

                //
                writerDataLifecycle_local = motionInfoDwQos_local.WriterDataLifecycle;
                writerDataLifecycle_local.AutodisposeUnregisteredInstances = false;

                //
                /* Create Datat Reader for the MotionInf_Veh_Comm Topics */
                parentWriter_local = motionInfoHandShakeRecieveDataPublisher.CreateDataWriter(motionInfo_HandShakeRecieveDataTopic, motionInfoDwQos_local);
                ErrorHandler.checkHandle(parentWriter_local, "@DDS_SetUp:  Initial  \\   <DDS>  motionInfoHandShakeRecieveDataPublisher.CreateDataWriter /motionInfo_HandShakeRecieveDataTopic - Error");                    // Roy*180612
                DDS_Global.motionInfo_HandShakeRecieveDataWriter = parentWriter_local as MotionInfo_HandShake_RecieveDataDataWriter;
                //ErrorHandler.checkHandle(DDS_Global.motionInfo_HandShakeRecieveDataWriter, "MotionInfo_HandShake_RecieveDataDataWriter");                    // Roy+171204                // Roy-180612
                #endregion

                return true;
            }
            catch (Exception ex)
            {
                //eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Info, null, "DDS_SetUp Initial Error : " + ex.Message);               // Roy-180612
                eqTool.Fun_Log(eqTool.MyLogKind.GeneralProcess, NLog.LogLevel.Fatal, null, "@DDS_SetUp:  Exception= {0},  StackTrace= {1}", ex.Message, ex.StackTrace);             //Roy+180612
                return false;
            }
        }

    }
}
