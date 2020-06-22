using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DDS;
using DDS.OpenSplice;
using DDS.OpenSplice.CustomMarshalers;

namespace Veh_HandShakeData
{
    #region MotionInfo_ClientDataReader
    public class MotionInfo_ClientDataReader : DDS.OpenSplice.FooDataReader<MotionInfo_Client, MotionInfo_ClientMarshaler>, 
                                         IMotionInfo_ClientDataReader
    {
        public MotionInfo_ClientDataReader(DatabaseMarshaler marshaler)
            : base(marshaler) { }

        public ReturnCode Read(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Read(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Read(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Read(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public override ReturnCode Read(
                ref MotionInfo_Client[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.Read(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode Take(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Take(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Take(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Take(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public override ReturnCode Take(
                ref MotionInfo_Client[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.Take(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadWithCondition(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return ReadWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public override ReturnCode ReadWithCondition(
                ref MotionInfo_Client[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.ReadWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            return result;
        }

        public ReturnCode TakeWithCondition(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return TakeWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public override ReturnCode TakeWithCondition(
                ref MotionInfo_Client[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.TakeWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            return result;
        }

        public override ReturnCode ReadNextSample(
                ref MotionInfo_Client dataValue,
                ref SampleInfo sampleInfo)
        {
            ReturnCode result =
                base.ReadNextSample(
                        ref dataValue,
                        ref sampleInfo);
            return result;
        }

        public override ReturnCode TakeNextSample(
                ref MotionInfo_Client dataValue,
                ref SampleInfo sampleInfo)
        {
            ReturnCode result =
                base.TakeNextSample(
                        ref dataValue,
                        ref sampleInfo);
            return result;
        }

        public ReturnCode ReadInstance(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadInstance(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode ReadInstance(
                ref MotionInfo_Client[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.ReadInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode TakeInstance(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeInstance(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode TakeInstance(
                ref MotionInfo_Client[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.TakeInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadNextInstance(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadNextInstance(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode ReadNextInstance(
                ref MotionInfo_Client[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.ReadNextInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode TakeNextInstance(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeNextInstance(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode TakeNextInstance(
                ref MotionInfo_Client[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.TakeNextInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadNextInstanceWithCondition(
                ref MotionInfo_Client[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return ReadNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public override ReturnCode ReadNextInstanceWithCondition(
                ref MotionInfo_Client[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.ReadNextInstanceWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);
            return result;
        }

        public ReturnCode TakeNextInstanceWithCondition(
                ref MotionInfo_Client[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return TakeNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public override ReturnCode TakeNextInstanceWithCondition(
                ref MotionInfo_Client[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.TakeNextInstanceWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);

            return result;
        }

        public override ReturnCode ReturnLoan(
                ref MotionInfo_Client[] dataValues,
                ref SampleInfo[] sampleInfos)
        {
            ReturnCode result =
                base.ReturnLoan(
                        ref dataValues,
                        ref sampleInfos);

            return result;
        }

        public override ReturnCode GetKeyValue(
                ref MotionInfo_Client key,
                InstanceHandle handle)
        {
            ReturnCode result = base.GetKeyValue(
                        ref key,
                        handle);
            return result;
        }

        public override InstanceHandle LookupInstance(
                MotionInfo_Client instance)
        {
            return
                base.LookupInstance(
                        instance);
        }

    }
    #endregion
    
    #region MotionInfo_ClientDataWriter
    public class MotionInfo_ClientDataWriter : DDS.OpenSplice.FooDataWriter<MotionInfo_Client, MotionInfo_ClientMarshaler>, 
                                         IMotionInfo_ClientDataWriter
    {
        public MotionInfo_ClientDataWriter(DatabaseMarshaler marshaler)
            : base(marshaler) { }

        public InstanceHandle RegisterInstance(
                MotionInfo_Client instanceData)
        {
            return base.RegisterInstance(
                    instanceData,
                    Time.Current);
        }

        public InstanceHandle RegisterInstanceWithTimestamp(
                MotionInfo_Client instanceData,
                Time sourceTimestamp)
        {
            return base.RegisterInstance(
                    instanceData,
                    sourceTimestamp);
        }

        public ReturnCode UnregisterInstance(
                MotionInfo_Client instanceData,
                InstanceHandle instanceHandle)
        {
            return base.UnregisterInstance(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode UnregisterInstanceWithTimestamp(
                MotionInfo_Client instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.UnregisterInstance(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode Write(MotionInfo_Client instanceData)
        {
            return base.Write(
                    instanceData,
                    InstanceHandle.Nil,
                    Time.Current);
        }

        public ReturnCode Write(
                MotionInfo_Client instanceData,
                InstanceHandle instanceHandle)
        {
            return base.Write(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode WriteWithTimestamp(
                MotionInfo_Client instanceData,
                Time sourceTimestamp)
        {
            return base.Write(
                    instanceData,
                    InstanceHandle.Nil,
                    sourceTimestamp);
        }

        public ReturnCode WriteWithTimestamp(
                MotionInfo_Client instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.Write(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode Dispose(
                MotionInfo_Client instanceData,
                InstanceHandle instanceHandle)
        {
            return base.Dispose(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode DisposeWithTimestamp(
                MotionInfo_Client instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.Dispose(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode WriteDispose(
                MotionInfo_Client instanceData)
        {
            return base.WriteDispose(
                    instanceData,
                    InstanceHandle.Nil,
                    Time.Current);
        }

        public ReturnCode WriteDispose(
                MotionInfo_Client instanceData,
                InstanceHandle instanceHandle)
        {
            return base.WriteDispose(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                MotionInfo_Client instanceData,
                Time sourceTimestamp)
        {
            return base.WriteDispose(
                    instanceData,
                    InstanceHandle.Nil,
                    sourceTimestamp);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                MotionInfo_Client instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.WriteDispose(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public override ReturnCode GetKeyValue(
                ref MotionInfo_Client key,
                InstanceHandle instanceHandle)
        {
            return base.GetKeyValue(ref key, instanceHandle);
        }

        public override InstanceHandle LookupInstance(
            MotionInfo_Client instanceData)
        {
            return base.LookupInstance(instanceData);
        }
    }
    #endregion

    #region MotionInfo_ClientTypeSupport
    public class MotionInfo_ClientTypeSupport : DDS.OpenSplice.TypeSupport
    {
        private static readonly string[] metaDescriptor = {"<MetaData version=\"1.0.0\"><Module name=\"Veh_HandShakeData\"><Struct name=\"MotionInfo_Client\"><Member name=\"vehID\">",
"<Long/></Member><Member name=\"vehName\"><String/></Member><Member name=\"NO\"><Long/></Member><Member name=\"En_TYPE\">",
"<Long/></Member><Member name=\"CUR_ADR\"><Long/></Member><Member name=\"CUR_SEC\"><Long/></Member><Member name=\"IS_CARRY\">",
"<Long/></Member><Member name=\"IS_OBST\"><Long/></Member><Member name=\"IS_BLOCK\"><Long/></Member><Member name=\"IS_HID\">",
"<Long/></Member><Member name=\"IS_PAUSE\"><Long/></Member><Member name=\"IS_LGUIDE\"><Long/></Member>",
"<Member name=\"IS_RGUIDE\"><Long/></Member><Member name=\"SEC_DISTANCE\"><Long/></Member><Member name=\"BLOCK_SEC\">",
"<Long/></Member><Member name=\"HID_SEC\"><Long/></Member><Member name=\"ACT_TYPE\"><Long/></Member><Member name=\"RPLY_CODE\">",
"<Long/></Member></Struct></Module></MetaData>"};

        public MotionInfo_ClientTypeSupport()
            : base(typeof(MotionInfo_Client), metaDescriptor, "Veh_HandShakeData::MotionInfo_Client", "", "vehID")
        { }


        public override ReturnCode RegisterType(IDomainParticipant participant, string typeName)
        {
            return RegisterType(participant, typeName, new MotionInfo_ClientMarshaler());
        }

        public override DDS.OpenSplice.DataWriter CreateDataWriter(DatabaseMarshaler marshaler)
        {
            return new MotionInfo_ClientDataWriter(marshaler);
        }

        public override DDS.OpenSplice.DataReader CreateDataReader(DatabaseMarshaler marshaler)
        {
            return new MotionInfo_ClientDataReader(marshaler);
        }
    }
    #endregion

    #region MotionInfo_ServerDataReader
    public class MotionInfo_ServerDataReader : DDS.OpenSplice.FooDataReader<MotionInfo_Server, MotionInfo_ServerMarshaler>, 
                                         IMotionInfo_ServerDataReader
    {
        public MotionInfo_ServerDataReader(DatabaseMarshaler marshaler)
            : base(marshaler) { }

        public ReturnCode Read(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Read(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Read(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Read(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public override ReturnCode Read(
                ref MotionInfo_Server[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.Read(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode Take(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Take(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Take(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Take(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public override ReturnCode Take(
                ref MotionInfo_Server[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.Take(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadWithCondition(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return ReadWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public override ReturnCode ReadWithCondition(
                ref MotionInfo_Server[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.ReadWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            return result;
        }

        public ReturnCode TakeWithCondition(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return TakeWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public override ReturnCode TakeWithCondition(
                ref MotionInfo_Server[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.TakeWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            return result;
        }

        public override ReturnCode ReadNextSample(
                ref MotionInfo_Server dataValue,
                ref SampleInfo sampleInfo)
        {
            ReturnCode result =
                base.ReadNextSample(
                        ref dataValue,
                        ref sampleInfo);
            return result;
        }

        public override ReturnCode TakeNextSample(
                ref MotionInfo_Server dataValue,
                ref SampleInfo sampleInfo)
        {
            ReturnCode result =
                base.TakeNextSample(
                        ref dataValue,
                        ref sampleInfo);
            return result;
        }

        public ReturnCode ReadInstance(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadInstance(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode ReadInstance(
                ref MotionInfo_Server[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.ReadInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode TakeInstance(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeInstance(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode TakeInstance(
                ref MotionInfo_Server[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.TakeInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadNextInstance(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadNextInstance(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode ReadNextInstance(
                ref MotionInfo_Server[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.ReadNextInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode TakeNextInstance(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeNextInstance(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode TakeNextInstance(
                ref MotionInfo_Server[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.TakeNextInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadNextInstanceWithCondition(
                ref MotionInfo_Server[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return ReadNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public override ReturnCode ReadNextInstanceWithCondition(
                ref MotionInfo_Server[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.ReadNextInstanceWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);
            return result;
        }

        public ReturnCode TakeNextInstanceWithCondition(
                ref MotionInfo_Server[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return TakeNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public override ReturnCode TakeNextInstanceWithCondition(
                ref MotionInfo_Server[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.TakeNextInstanceWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);

            return result;
        }

        public override ReturnCode ReturnLoan(
                ref MotionInfo_Server[] dataValues,
                ref SampleInfo[] sampleInfos)
        {
            ReturnCode result =
                base.ReturnLoan(
                        ref dataValues,
                        ref sampleInfos);

            return result;
        }

        public override ReturnCode GetKeyValue(
                ref MotionInfo_Server key,
                InstanceHandle handle)
        {
            ReturnCode result = base.GetKeyValue(
                        ref key,
                        handle);
            return result;
        }

        public override InstanceHandle LookupInstance(
                MotionInfo_Server instance)
        {
            return
                base.LookupInstance(
                        instance);
        }

    }
    #endregion
    
    #region MotionInfo_ServerDataWriter
    public class MotionInfo_ServerDataWriter : DDS.OpenSplice.FooDataWriter<MotionInfo_Server, MotionInfo_ServerMarshaler>, 
                                         IMotionInfo_ServerDataWriter
    {
        public MotionInfo_ServerDataWriter(DatabaseMarshaler marshaler)
            : base(marshaler) { }

        public InstanceHandle RegisterInstance(
                MotionInfo_Server instanceData)
        {
            return base.RegisterInstance(
                    instanceData,
                    Time.Current);
        }

        public InstanceHandle RegisterInstanceWithTimestamp(
                MotionInfo_Server instanceData,
                Time sourceTimestamp)
        {
            return base.RegisterInstance(
                    instanceData,
                    sourceTimestamp);
        }

        public ReturnCode UnregisterInstance(
                MotionInfo_Server instanceData,
                InstanceHandle instanceHandle)
        {
            return base.UnregisterInstance(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode UnregisterInstanceWithTimestamp(
                MotionInfo_Server instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.UnregisterInstance(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode Write(MotionInfo_Server instanceData)
        {
            return base.Write(
                    instanceData,
                    InstanceHandle.Nil,
                    Time.Current);
        }

        public ReturnCode Write(
                MotionInfo_Server instanceData,
                InstanceHandle instanceHandle)
        {
            return base.Write(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode WriteWithTimestamp(
                MotionInfo_Server instanceData,
                Time sourceTimestamp)
        {
            return base.Write(
                    instanceData,
                    InstanceHandle.Nil,
                    sourceTimestamp);
        }

        public ReturnCode WriteWithTimestamp(
                MotionInfo_Server instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.Write(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode Dispose(
                MotionInfo_Server instanceData,
                InstanceHandle instanceHandle)
        {
            return base.Dispose(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode DisposeWithTimestamp(
                MotionInfo_Server instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.Dispose(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode WriteDispose(
                MotionInfo_Server instanceData)
        {
            return base.WriteDispose(
                    instanceData,
                    InstanceHandle.Nil,
                    Time.Current);
        }

        public ReturnCode WriteDispose(
                MotionInfo_Server instanceData,
                InstanceHandle instanceHandle)
        {
            return base.WriteDispose(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                MotionInfo_Server instanceData,
                Time sourceTimestamp)
        {
            return base.WriteDispose(
                    instanceData,
                    InstanceHandle.Nil,
                    sourceTimestamp);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                MotionInfo_Server instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.WriteDispose(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public override ReturnCode GetKeyValue(
                ref MotionInfo_Server key,
                InstanceHandle instanceHandle)
        {
            return base.GetKeyValue(ref key, instanceHandle);
        }

        public override InstanceHandle LookupInstance(
            MotionInfo_Server instanceData)
        {
            return base.LookupInstance(instanceData);
        }
    }
    #endregion

    #region MotionInfo_ServerTypeSupport
    public class MotionInfo_ServerTypeSupport : DDS.OpenSplice.TypeSupport
    {
        private static readonly string[] metaDescriptor = {"<MetaData version=\"1.0.0\"><Module name=\"Veh_HandShakeData\"><Struct name=\"MotionInfo_Server\"><Member name=\"vehID\">",
"<Long/></Member><Member name=\"vehName\"><String/></Member><Member name=\"NO\"><Long/></Member><Member name=\"ACT_TYPE\">",
"<Long/></Member><Member name=\"FROM_ADR_ID\"><String/></Member><Member name=\"TO_ADR_ID\"><String/></Member>",
"<Member name=\"CAST_ID\"><String/></Member><Member name=\"RPLY_CODE\"><Long/></Member></Struct></Module>",
"</MetaData>"};

        public MotionInfo_ServerTypeSupport()
            : base(typeof(MotionInfo_Server), metaDescriptor, "Veh_HandShakeData::MotionInfo_Server", "", "vehID")
        { }


        public override ReturnCode RegisterType(IDomainParticipant participant, string typeName)
        {
            return RegisterType(participant, typeName, new MotionInfo_ServerMarshaler());
        }

        public override DDS.OpenSplice.DataWriter CreateDataWriter(DatabaseMarshaler marshaler)
        {
            return new MotionInfo_ServerDataWriter(marshaler);
        }

        public override DDS.OpenSplice.DataReader CreateDataReader(DatabaseMarshaler marshaler)
        {
            return new MotionInfo_ServerDataReader(marshaler);
        }
    }
    #endregion

    #region MotionInfo_Vehicle_CommDataReader
    public class MotionInfo_Vehicle_CommDataReader : DDS.OpenSplice.FooDataReader<MotionInfo_Vehicle_Comm, MotionInfo_Vehicle_CommMarshaler>, 
                                         IMotionInfo_Vehicle_CommDataReader
    {
        public MotionInfo_Vehicle_CommDataReader(DatabaseMarshaler marshaler)
            : base(marshaler) { }

        public ReturnCode Read(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Read(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Read(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Read(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public override ReturnCode Read(
                ref MotionInfo_Vehicle_Comm[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.Read(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode Take(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Take(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Take(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Take(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public override ReturnCode Take(
                ref MotionInfo_Vehicle_Comm[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.Take(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadWithCondition(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return ReadWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public override ReturnCode ReadWithCondition(
                ref MotionInfo_Vehicle_Comm[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.ReadWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            return result;
        }

        public ReturnCode TakeWithCondition(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return TakeWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public override ReturnCode TakeWithCondition(
                ref MotionInfo_Vehicle_Comm[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.TakeWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            return result;
        }

        public override ReturnCode ReadNextSample(
                ref MotionInfo_Vehicle_Comm dataValue,
                ref SampleInfo sampleInfo)
        {
            ReturnCode result =
                base.ReadNextSample(
                        ref dataValue,
                        ref sampleInfo);
            return result;
        }

        public override ReturnCode TakeNextSample(
                ref MotionInfo_Vehicle_Comm dataValue,
                ref SampleInfo sampleInfo)
        {
            ReturnCode result =
                base.TakeNextSample(
                        ref dataValue,
                        ref sampleInfo);
            return result;
        }

        public ReturnCode ReadInstance(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadInstance(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode ReadInstance(
                ref MotionInfo_Vehicle_Comm[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.ReadInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode TakeInstance(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeInstance(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode TakeInstance(
                ref MotionInfo_Vehicle_Comm[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.TakeInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadNextInstance(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadNextInstance(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode ReadNextInstance(
                ref MotionInfo_Vehicle_Comm[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.ReadNextInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode TakeNextInstance(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeNextInstance(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode TakeNextInstance(
                ref MotionInfo_Vehicle_Comm[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.TakeNextInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadNextInstanceWithCondition(
                ref MotionInfo_Vehicle_Comm[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return ReadNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public override ReturnCode ReadNextInstanceWithCondition(
                ref MotionInfo_Vehicle_Comm[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.ReadNextInstanceWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);
            return result;
        }

        public ReturnCode TakeNextInstanceWithCondition(
                ref MotionInfo_Vehicle_Comm[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return TakeNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public override ReturnCode TakeNextInstanceWithCondition(
                ref MotionInfo_Vehicle_Comm[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.TakeNextInstanceWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);

            return result;
        }

        public override ReturnCode ReturnLoan(
                ref MotionInfo_Vehicle_Comm[] dataValues,
                ref SampleInfo[] sampleInfos)
        {
            ReturnCode result =
                base.ReturnLoan(
                        ref dataValues,
                        ref sampleInfos);

            return result;
        }

        public override ReturnCode GetKeyValue(
                ref MotionInfo_Vehicle_Comm key,
                InstanceHandle handle)
        {
            ReturnCode result = base.GetKeyValue(
                        ref key,
                        handle);
            return result;
        }

        public override InstanceHandle LookupInstance(
                MotionInfo_Vehicle_Comm instance)
        {
            return
                base.LookupInstance(
                        instance);
        }

    }
    #endregion
    
    #region MotionInfo_Vehicle_CommDataWriter
    public class MotionInfo_Vehicle_CommDataWriter : DDS.OpenSplice.FooDataWriter<MotionInfo_Vehicle_Comm, MotionInfo_Vehicle_CommMarshaler>, 
                                         IMotionInfo_Vehicle_CommDataWriter
    {
        public MotionInfo_Vehicle_CommDataWriter(DatabaseMarshaler marshaler)
            : base(marshaler) { }

        public InstanceHandle RegisterInstance(
                MotionInfo_Vehicle_Comm instanceData)
        {
            return base.RegisterInstance(
                    instanceData,
                    Time.Current);
        }

        public InstanceHandle RegisterInstanceWithTimestamp(
                MotionInfo_Vehicle_Comm instanceData,
                Time sourceTimestamp)
        {
            return base.RegisterInstance(
                    instanceData,
                    sourceTimestamp);
        }

        public ReturnCode UnregisterInstance(
                MotionInfo_Vehicle_Comm instanceData,
                InstanceHandle instanceHandle)
        {
            return base.UnregisterInstance(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode UnregisterInstanceWithTimestamp(
                MotionInfo_Vehicle_Comm instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.UnregisterInstance(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode Write(MotionInfo_Vehicle_Comm instanceData)
        {
            return base.Write(
                    instanceData,
                    InstanceHandle.Nil,
                    Time.Current);
        }

        public ReturnCode Write(
                MotionInfo_Vehicle_Comm instanceData,
                InstanceHandle instanceHandle)
        {
            return base.Write(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode WriteWithTimestamp(
                MotionInfo_Vehicle_Comm instanceData,
                Time sourceTimestamp)
        {
            return base.Write(
                    instanceData,
                    InstanceHandle.Nil,
                    sourceTimestamp);
        }

        public ReturnCode WriteWithTimestamp(
                MotionInfo_Vehicle_Comm instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.Write(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode Dispose(
                MotionInfo_Vehicle_Comm instanceData,
                InstanceHandle instanceHandle)
        {
            return base.Dispose(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode DisposeWithTimestamp(
                MotionInfo_Vehicle_Comm instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.Dispose(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode WriteDispose(
                MotionInfo_Vehicle_Comm instanceData)
        {
            return base.WriteDispose(
                    instanceData,
                    InstanceHandle.Nil,
                    Time.Current);
        }

        public ReturnCode WriteDispose(
                MotionInfo_Vehicle_Comm instanceData,
                InstanceHandle instanceHandle)
        {
            return base.WriteDispose(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                MotionInfo_Vehicle_Comm instanceData,
                Time sourceTimestamp)
        {
            return base.WriteDispose(
                    instanceData,
                    InstanceHandle.Nil,
                    sourceTimestamp);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                MotionInfo_Vehicle_Comm instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.WriteDispose(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public override ReturnCode GetKeyValue(
                ref MotionInfo_Vehicle_Comm key,
                InstanceHandle instanceHandle)
        {
            return base.GetKeyValue(ref key, instanceHandle);
        }

        public override InstanceHandle LookupInstance(
            MotionInfo_Vehicle_Comm instanceData)
        {
            return base.LookupInstance(instanceData);
        }
    }
    #endregion

    #region MotionInfo_Vehicle_CommTypeSupport
    public class MotionInfo_Vehicle_CommTypeSupport : DDS.OpenSplice.TypeSupport
    {
        private static readonly string[] metaDescriptor = {"<MetaData version=\"1.0.0\"><Module name=\"Veh_HandShakeData\"><Struct name=\"MotionInfo_Vehicle_Comm\">",
"<Member name=\"vehID\"><Long/></Member><Member name=\"vehName\"><String/></Member><Member name=\"front_wheel_torque\">",
"<Double/></Member><Member name=\"rear_wheel_torque\"><Double/></Member><Member name=\"front_wheel_speed\">",
"<Double/></Member><Member name=\"rear_wheel_speed\"><Double/></Member><Member name=\"front_wheel_acc\">",
"<Double/></Member><Member name=\"front_wheel_dec\"><Double/></Member><Member name=\"rear_wheel_acc\">",
"<Double/></Member><Member name=\"rear_wheel_dec\"><Double/></Member><Member name=\"front_wheel_dist\">",
"<Double/></Member><Member name=\"rear_wheel_dist\"><Double/></Member><Member name=\"ave_dist\"><Double/>",
"</Member><Member name=\"ave_speed\"><Double/></Member><Member name=\"ave_torque\"><Double/></Member><Member name=\"ave_acc\">",
"<Double/></Member><Member name=\"ave_dec\"><Double/></Member><Member name=\"current_pos\"><Double/></Member>",
"<Member name=\"target_speed\"><Double/></Member><Member name=\"max_speed\"><Double/></Member><Member name=\"min_spedd\">",
"<Double/></Member><Member name=\"right_guide_up\"><Boolean/></Member><Member name=\"left_guide_up\"><Boolean/>",
"</Member><Member name=\"right_guide_down\"><Boolean/></Member><Member name=\"left_guide_down\"><Boolean/>",
"</Member><Member name=\"right_guide_detection\"><Boolean/></Member><Member name=\"left_guide_detection\">",
"<Boolean/></Member><Member name=\"long_range_obst\"><Boolean/></Member><Member name=\"short_range_obst\">",
"<Boolean/></Member><Member name=\"mid_range_obst\"><Boolean/></Member><Member name=\"current_address\">",
"<String/></Member><Member name=\"from_address\"><String/></Member><Member name=\"to_address\"><String/>",
"</Member><Member name=\"current_stage\"><String/></Member><Member name=\"from_stage\"><String/></Member>",
"<Member name=\"to_stage\"><String/></Member><Member name=\"port_address\"><String/></Member><Member name=\"barcode_read_address\">",
"<String/></Member></Struct></Module></MetaData>"};

        public MotionInfo_Vehicle_CommTypeSupport()
            : base(typeof(MotionInfo_Vehicle_Comm), metaDescriptor, "Veh_HandShakeData::MotionInfo_Vehicle_Comm", "", "vehID")
        { }


        public override ReturnCode RegisterType(IDomainParticipant participant, string typeName)
        {
            return RegisterType(participant, typeName, new MotionInfo_Vehicle_CommMarshaler());
        }

        public override DDS.OpenSplice.DataWriter CreateDataWriter(DatabaseMarshaler marshaler)
        {
            return new MotionInfo_Vehicle_CommDataWriter(marshaler);
        }

        public override DDS.OpenSplice.DataReader CreateDataReader(DatabaseMarshaler marshaler)
        {
            return new MotionInfo_Vehicle_CommDataReader(marshaler);
        }
    }
    #endregion

    #region MotionInfo_Inter_Comm_SendDataDataReader
    public class MotionInfo_Inter_Comm_SendDataDataReader : DDS.OpenSplice.FooDataReader<MotionInfo_Inter_Comm_SendData, MotionInfo_Inter_Comm_SendDataMarshaler>, 
                                         IMotionInfo_Inter_Comm_SendDataDataReader
    {
        public MotionInfo_Inter_Comm_SendDataDataReader(DatabaseMarshaler marshaler)
            : base(marshaler) { }

        public ReturnCode Read(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Read(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Read(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Read(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public override ReturnCode Read(
                ref MotionInfo_Inter_Comm_SendData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.Read(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode Take(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Take(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Take(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Take(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public override ReturnCode Take(
                ref MotionInfo_Inter_Comm_SendData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.Take(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadWithCondition(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return ReadWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public override ReturnCode ReadWithCondition(
                ref MotionInfo_Inter_Comm_SendData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.ReadWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            return result;
        }

        public ReturnCode TakeWithCondition(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return TakeWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public override ReturnCode TakeWithCondition(
                ref MotionInfo_Inter_Comm_SendData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.TakeWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            return result;
        }

        public override ReturnCode ReadNextSample(
                ref MotionInfo_Inter_Comm_SendData dataValue,
                ref SampleInfo sampleInfo)
        {
            ReturnCode result =
                base.ReadNextSample(
                        ref dataValue,
                        ref sampleInfo);
            return result;
        }

        public override ReturnCode TakeNextSample(
                ref MotionInfo_Inter_Comm_SendData dataValue,
                ref SampleInfo sampleInfo)
        {
            ReturnCode result =
                base.TakeNextSample(
                        ref dataValue,
                        ref sampleInfo);
            return result;
        }

        public ReturnCode ReadInstance(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadInstance(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode ReadInstance(
                ref MotionInfo_Inter_Comm_SendData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.ReadInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode TakeInstance(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeInstance(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode TakeInstance(
                ref MotionInfo_Inter_Comm_SendData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.TakeInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadNextInstance(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadNextInstance(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode ReadNextInstance(
                ref MotionInfo_Inter_Comm_SendData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.ReadNextInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode TakeNextInstance(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeNextInstance(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode TakeNextInstance(
                ref MotionInfo_Inter_Comm_SendData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.TakeNextInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadNextInstanceWithCondition(
                ref MotionInfo_Inter_Comm_SendData[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return ReadNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public override ReturnCode ReadNextInstanceWithCondition(
                ref MotionInfo_Inter_Comm_SendData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.ReadNextInstanceWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);
            return result;
        }

        public ReturnCode TakeNextInstanceWithCondition(
                ref MotionInfo_Inter_Comm_SendData[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return TakeNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public override ReturnCode TakeNextInstanceWithCondition(
                ref MotionInfo_Inter_Comm_SendData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.TakeNextInstanceWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);

            return result;
        }

        public override ReturnCode ReturnLoan(
                ref MotionInfo_Inter_Comm_SendData[] dataValues,
                ref SampleInfo[] sampleInfos)
        {
            ReturnCode result =
                base.ReturnLoan(
                        ref dataValues,
                        ref sampleInfos);

            return result;
        }

        public override ReturnCode GetKeyValue(
                ref MotionInfo_Inter_Comm_SendData key,
                InstanceHandle handle)
        {
            ReturnCode result = base.GetKeyValue(
                        ref key,
                        handle);
            return result;
        }

        public override InstanceHandle LookupInstance(
                MotionInfo_Inter_Comm_SendData instance)
        {
            return
                base.LookupInstance(
                        instance);
        }

    }
    #endregion
    
    #region MotionInfo_Inter_Comm_SendDataDataWriter
    public class MotionInfo_Inter_Comm_SendDataDataWriter : DDS.OpenSplice.FooDataWriter<MotionInfo_Inter_Comm_SendData, MotionInfo_Inter_Comm_SendDataMarshaler>, 
                                         IMotionInfo_Inter_Comm_SendDataDataWriter
    {
        public MotionInfo_Inter_Comm_SendDataDataWriter(DatabaseMarshaler marshaler)
            : base(marshaler) { }

        public InstanceHandle RegisterInstance(
                MotionInfo_Inter_Comm_SendData instanceData)
        {
            return base.RegisterInstance(
                    instanceData,
                    Time.Current);
        }

        public InstanceHandle RegisterInstanceWithTimestamp(
                MotionInfo_Inter_Comm_SendData instanceData,
                Time sourceTimestamp)
        {
            return base.RegisterInstance(
                    instanceData,
                    sourceTimestamp);
        }

        public ReturnCode UnregisterInstance(
                MotionInfo_Inter_Comm_SendData instanceData,
                InstanceHandle instanceHandle)
        {
            return base.UnregisterInstance(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode UnregisterInstanceWithTimestamp(
                MotionInfo_Inter_Comm_SendData instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.UnregisterInstance(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode Write(MotionInfo_Inter_Comm_SendData instanceData)
        {
            return base.Write(
                    instanceData,
                    InstanceHandle.Nil,
                    Time.Current);
        }

        public ReturnCode Write(
                MotionInfo_Inter_Comm_SendData instanceData,
                InstanceHandle instanceHandle)
        {
            return base.Write(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode WriteWithTimestamp(
                MotionInfo_Inter_Comm_SendData instanceData,
                Time sourceTimestamp)
        {
            return base.Write(
                    instanceData,
                    InstanceHandle.Nil,
                    sourceTimestamp);
        }

        public ReturnCode WriteWithTimestamp(
                MotionInfo_Inter_Comm_SendData instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.Write(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode Dispose(
                MotionInfo_Inter_Comm_SendData instanceData,
                InstanceHandle instanceHandle)
        {
            return base.Dispose(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode DisposeWithTimestamp(
                MotionInfo_Inter_Comm_SendData instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.Dispose(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode WriteDispose(
                MotionInfo_Inter_Comm_SendData instanceData)
        {
            return base.WriteDispose(
                    instanceData,
                    InstanceHandle.Nil,
                    Time.Current);
        }

        public ReturnCode WriteDispose(
                MotionInfo_Inter_Comm_SendData instanceData,
                InstanceHandle instanceHandle)
        {
            return base.WriteDispose(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                MotionInfo_Inter_Comm_SendData instanceData,
                Time sourceTimestamp)
        {
            return base.WriteDispose(
                    instanceData,
                    InstanceHandle.Nil,
                    sourceTimestamp);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                MotionInfo_Inter_Comm_SendData instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.WriteDispose(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public override ReturnCode GetKeyValue(
                ref MotionInfo_Inter_Comm_SendData key,
                InstanceHandle instanceHandle)
        {
            return base.GetKeyValue(ref key, instanceHandle);
        }

        public override InstanceHandle LookupInstance(
            MotionInfo_Inter_Comm_SendData instanceData)
        {
            return base.LookupInstance(instanceData);
        }
    }
    #endregion

    #region MotionInfo_Inter_Comm_SendDataTypeSupport
    public class MotionInfo_Inter_Comm_SendDataTypeSupport : DDS.OpenSplice.TypeSupport
    {
        private static readonly string[] metaDescriptor = {"<MetaData version=\"1.0.0\"><Module name=\"Veh_HandShakeData\"><Enum name=\"CmdType\"><Element name=\"NotDefinedYet\" value=\"0\"/>",
"<Element name=\"CmdToMove\" value=\"1\"/><Element name=\"CmdToLoad\" value=\"2\"/><Element name=\"CmdToUnload\" value=\"3\"/>",
"<Element name=\"CmdToBlockSectionQueryResult\" value=\"4\"/><Element name=\"CmdToHIDSectionQueryResult\" value=\"5\"/>",
"<Element name=\"CmdToReserveSectionQueryResult\" value=\"6\"/><Element name=\"CmdToStop\" value=\"7\"/>",
"<Element name=\"CmdToPause\" value=\"8\"/><Element name=\"CmdToContinue\" value=\"9\"/><Element name=\"CmdToRestart\" value=\"10\"/>",
"<Element name=\"CmdToCancel\" value=\"11\"/><Element name=\"CmdToAbort\" value=\"12\"/><Element name=\"CmdToOverride\" value=\"13\"/>",
"<Element name=\"CmdToAvoid\" value=\"14\"/><Element name=\"CmdToMaintenance\" value=\"15\"/><Element name=\"CmdForChangeStatus\" value=\"16\"/>",
"</Enum><Enum name=\"VehControlMode\"><Element name=\"OnlineRemote\" value=\"0\"/><Element name=\"OnlineLocal\" value=\"1\"/>",
"<Element name=\"Offline\" value=\"2\"/></Enum><Enum name=\"MoveType\"><Element name=\"single\" value=\"0\"/>",
"<Element name=\"cycle\" value=\"1\"/></Enum><Enum name=\"VehLoadedStatus\"><Element name=\"NotExisted\" value=\"0\"/>",
"<Element name=\"Existed\" value=\"1\"/></Enum><Enum name=\"Status\"><Element name=\"OK\" value=\"0\"/>",
"<Element name=\"NG\" value=\"1\"/><Element name=\"TimeOut\" value=\"2\"/></Enum><Struct name=\"VehCmdType\">",
"<Member name=\"eCmdType\"><Type name=\"CmdType\"/></Member></Struct><Struct name=\"MotionInfo_Move\">",
"<Member name=\"eMoveType\"><Type name=\"MoveType\"/></Member><Member name=\"Address\"><String/></Member>",
"<Member name=\"Stage\"><String/></Member><Member name=\"GuidingSections\"><Sequence><String/></Sequence>",
"</Member><Member name=\"GuidingAddresses\"><Sequence><String/></Sequence></Member><Member name=\"ForLoading\">",
"<Long/></Member><Member name=\"ForUnLoading\"><Long/></Member><Member name=\"ForMaintain\"><Long/></Member>",
"</Struct><Struct name=\"MotionInfo_UnLoad\"><Member name=\"MCS_CSTID\"><String/></Member><Member name=\"MCS_CST2ID\">",
"<String/></Member><Member name=\"Veh_CSTID\"><String/></Member><Member name=\"Veh_CST2ID\"><String/></Member>",
"<Member name=\"VerPort_OK\"><Long/></Member><Member name=\"VerPort_NG\"><Long/></Member><Member name=\"VerCST_OK\">",
"<Long/></Member><Member name=\"VerCST_NG\"><Long/></Member><Member name=\"VerCST2_OK\"><Long/></Member>",
"<Member name=\"VerCST2_NG\"><Long/></Member><Member name=\"With_CST\"><Long/></Member><Member name=\"Without_CST\">",
"<Long/></Member><Member name=\"With_CST2\"><Long/></Member><Member name=\"Without_CST2\"><Long/></Member>",
"<Member name=\"LoadStatus\"><Type name=\"VehLoadedStatus\"/></Member></Struct><Struct name=\"MotionInfo_Load\">",
"<Member name=\"MCS_CSTID\"><String/></Member><Member name=\"MCS_CST2ID\"><String/></Member><Member name=\"Veh_CSTID\">",
"<String/></Member><Member name=\"Veh_CST2ID\"><String/></Member><Member name=\"VerPort_OK\"><Long/></Member>",
"<Member name=\"VerPort_NG\"><Long/></Member><Member name=\"VerCST_OK\"><Long/></Member><Member name=\"VerCST_NG\">",
"<Long/></Member><Member name=\"VerCST2_OK\"><Long/></Member><Member name=\"VerCST2_NG\"><Long/></Member>",
"<Member name=\"With_CST\"><Long/></Member><Member name=\"Without_CST\"><Long/></Member><Member name=\"With_CST2\">",
"<Long/></Member><Member name=\"Without_CST2\"><Long/></Member><Member name=\"LoadStatus\"><Type name=\"VehLoadedStatus\"/>",
"</Member></Struct><Struct name=\"MotionInfo_HIDSectionPassReply\"><Member name=\"Section\"><String/></Member>",
"<Member name=\"HIDSectionPassReply\"><Type name=\"Status\"/></Member></Struct><Struct name=\"MotionInfo_BlockSectionPassReply\">",
"<Member name=\"Section\"><String/></Member><Member name=\"BlockSectionPassReply\"><Type name=\"Status\"/>",
"</Member></Struct><Struct name=\"MotionInfo_ReserveSectionPassReply\"><Member name=\"SectionList\"><Sequence>",
"<String/></Sequence></Member><Member name=\"ReserveSectionPassReply\"><Type name=\"Status\"/></Member>",
"</Struct><Struct name=\"MotionInfo_Inter_Comm_SendData\"><Member name=\"vehID\"><Long/></Member><Member name=\"vehName\">",
"<String/></Member><Member name=\"cmd_Send\"><Long/></Member><Member name=\"cmd_Receive\"><Long/></Member>",
"<Member name=\"Proc_ON\"><Long/></Member><Member name=\"udtCmdType\"><Type name=\"VehCmdType\"/></Member>",
"<Member name=\"udtControlMode\"><Type name=\"VehControlMode\"/></Member><Member name=\"udtMove\"><Type name=\"MotionInfo_Move\"/>",
"</Member><Member name=\"udtLoad\"><Type name=\"MotionInfo_Load\"/></Member><Member name=\"udtUnLoad\">",
"<Type name=\"MotionInfo_UnLoad\"/></Member><Member name=\"isContinue\"><Long/></Member><Member name=\"isStop\">",
"<Long/></Member><Member name=\"isPause\"><Long/></Member><Member name=\"BlockControlTimeOut\"><Boolean/>",
"</Member><Member name=\"HIDControlTimeOut\"><Boolean/></Member><Member name=\"ReserveSectionTimeOut\">",
"<Boolean/></Member><Member name=\"ReserveSectionPassReply\"><Type name=\"MotionInfo_ReserveSectionPassReply\"/>",
"</Member><Member name=\"BlockSectionPassReply\"><Type name=\"MotionInfo_BlockSectionPassReply\"/></Member>",
"<Member name=\"HIDSectionPassReply\"><Type name=\"MotionInfo_HIDSectionPassReply\"/></Member></Struct>",
"</Module></MetaData>"};

        public MotionInfo_Inter_Comm_SendDataTypeSupport()
            : base(typeof(MotionInfo_Inter_Comm_SendData), metaDescriptor, "Veh_HandShakeData::MotionInfo_Inter_Comm_SendData", "", "vehID")
        { }


        public override ReturnCode RegisterType(IDomainParticipant participant, string typeName)
        {
            return RegisterType(participant, typeName, new MotionInfo_Inter_Comm_SendDataMarshaler());
        }

        public override DDS.OpenSplice.DataWriter CreateDataWriter(DatabaseMarshaler marshaler)
        {
            return new MotionInfo_Inter_Comm_SendDataDataWriter(marshaler);
        }

        public override DDS.OpenSplice.DataReader CreateDataReader(DatabaseMarshaler marshaler)
        {
            return new MotionInfo_Inter_Comm_SendDataDataReader(marshaler);
        }
    }
    #endregion

    #region MotionInfo_Vehicle_Inter_Comm_ReportData_134DataReader
    public class MotionInfo_Vehicle_Inter_Comm_ReportData_134DataReader : DDS.OpenSplice.FooDataReader<MotionInfo_Vehicle_Inter_Comm_ReportData_134, MotionInfo_Vehicle_Inter_Comm_ReportData_134Marshaler>, 
                                         IMotionInfo_Vehicle_Inter_Comm_ReportData_134DataReader
    {
        public MotionInfo_Vehicle_Inter_Comm_ReportData_134DataReader(DatabaseMarshaler marshaler)
            : base(marshaler) { }

        public ReturnCode Read(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Read(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Read(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Read(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public override ReturnCode Read(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.Read(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode Take(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Take(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Take(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Take(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public override ReturnCode Take(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.Take(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadWithCondition(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return ReadWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public override ReturnCode ReadWithCondition(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.ReadWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            return result;
        }

        public ReturnCode TakeWithCondition(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return TakeWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public override ReturnCode TakeWithCondition(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.TakeWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            return result;
        }

        public override ReturnCode ReadNextSample(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData_134 dataValue,
                ref SampleInfo sampleInfo)
        {
            ReturnCode result =
                base.ReadNextSample(
                        ref dataValue,
                        ref sampleInfo);
            return result;
        }

        public override ReturnCode TakeNextSample(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData_134 dataValue,
                ref SampleInfo sampleInfo)
        {
            ReturnCode result =
                base.TakeNextSample(
                        ref dataValue,
                        ref sampleInfo);
            return result;
        }

        public ReturnCode ReadInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode ReadInstance(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.ReadInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode TakeInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode TakeInstance(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.TakeInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadNextInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadNextInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode ReadNextInstance(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.ReadNextInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode TakeNextInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeNextInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode TakeNextInstance(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.TakeNextInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadNextInstanceWithCondition(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return ReadNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public override ReturnCode ReadNextInstanceWithCondition(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.ReadNextInstanceWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);
            return result;
        }

        public ReturnCode TakeNextInstanceWithCondition(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return TakeNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public override ReturnCode TakeNextInstanceWithCondition(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.TakeNextInstanceWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);

            return result;
        }

        public override ReturnCode ReturnLoan(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
                ref SampleInfo[] sampleInfos)
        {
            ReturnCode result =
                base.ReturnLoan(
                        ref dataValues,
                        ref sampleInfos);

            return result;
        }

        public override ReturnCode GetKeyValue(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData_134 key,
                InstanceHandle handle)
        {
            ReturnCode result = base.GetKeyValue(
                        ref key,
                        handle);
            return result;
        }

        public override InstanceHandle LookupInstance(
                MotionInfo_Vehicle_Inter_Comm_ReportData_134 instance)
        {
            return
                base.LookupInstance(
                        instance);
        }

    }
    #endregion
    
    #region MotionInfo_Vehicle_Inter_Comm_ReportData_134DataWriter
    public class MotionInfo_Vehicle_Inter_Comm_ReportData_134DataWriter : DDS.OpenSplice.FooDataWriter<MotionInfo_Vehicle_Inter_Comm_ReportData_134, MotionInfo_Vehicle_Inter_Comm_ReportData_134Marshaler>, 
                                         IMotionInfo_Vehicle_Inter_Comm_ReportData_134DataWriter
    {
        public MotionInfo_Vehicle_Inter_Comm_ReportData_134DataWriter(DatabaseMarshaler marshaler)
            : base(marshaler) { }

        public InstanceHandle RegisterInstance(
                MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData)
        {
            return base.RegisterInstance(
                    instanceData,
                    Time.Current);
        }

        public InstanceHandle RegisterInstanceWithTimestamp(
                MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData,
                Time sourceTimestamp)
        {
            return base.RegisterInstance(
                    instanceData,
                    sourceTimestamp);
        }

        public ReturnCode UnregisterInstance(
                MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData,
                InstanceHandle instanceHandle)
        {
            return base.UnregisterInstance(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode UnregisterInstanceWithTimestamp(
                MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.UnregisterInstance(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode Write(MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData)
        {
            return base.Write(
                    instanceData,
                    InstanceHandle.Nil,
                    Time.Current);
        }

        public ReturnCode Write(
                MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData,
                InstanceHandle instanceHandle)
        {
            return base.Write(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode WriteWithTimestamp(
                MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData,
                Time sourceTimestamp)
        {
            return base.Write(
                    instanceData,
                    InstanceHandle.Nil,
                    sourceTimestamp);
        }

        public ReturnCode WriteWithTimestamp(
                MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.Write(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode Dispose(
                MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData,
                InstanceHandle instanceHandle)
        {
            return base.Dispose(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode DisposeWithTimestamp(
                MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.Dispose(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode WriteDispose(
                MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData)
        {
            return base.WriteDispose(
                    instanceData,
                    InstanceHandle.Nil,
                    Time.Current);
        }

        public ReturnCode WriteDispose(
                MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData,
                InstanceHandle instanceHandle)
        {
            return base.WriteDispose(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData,
                Time sourceTimestamp)
        {
            return base.WriteDispose(
                    instanceData,
                    InstanceHandle.Nil,
                    sourceTimestamp);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.WriteDispose(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public override ReturnCode GetKeyValue(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData_134 key,
                InstanceHandle instanceHandle)
        {
            return base.GetKeyValue(ref key, instanceHandle);
        }

        public override InstanceHandle LookupInstance(
            MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData)
        {
            return base.LookupInstance(instanceData);
        }
    }
    #endregion

    #region MotionInfo_Vehicle_Inter_Comm_ReportData_134TypeSupport
    public class MotionInfo_Vehicle_Inter_Comm_ReportData_134TypeSupport : DDS.OpenSplice.TypeSupport
    {
        private static readonly string[] metaDescriptor = {"<MetaData version=\"1.0.0\"><Module name=\"Veh_HandShakeData\"><Enum name=\"VehLoadedStatus\"><Element name=\"NotExisted\" value=\"0\"/>",
"<Element name=\"Existed\" value=\"1\"/></Enum><Enum name=\"Status\"><Element name=\"OK\" value=\"0\"/>",
"<Element name=\"NG\" value=\"1\"/><Element name=\"TimeOut\" value=\"2\"/></Enum><Enum name=\"VehWheelSteeringAngle\">",
"<Element name=\"Zero\" value=\"0\"/><Element name=\"RightNinety\" value=\"1\"/><Element name=\"LeftNinety\" value=\"2\"/>",
"</Enum><Enum name=\"VehControlMode\"><Element name=\"OnlineRemote\" value=\"0\"/><Element name=\"OnlineLocal\" value=\"1\"/>",
"<Element name=\"Offline\" value=\"2\"/></Enum><Struct name=\"MotionInfo_UnLoad\"><Member name=\"MCS_CSTID\">",
"<String/></Member><Member name=\"MCS_CST2ID\"><String/></Member><Member name=\"Veh_CSTID\"><String/></Member>",
"<Member name=\"Veh_CST2ID\"><String/></Member><Member name=\"VerPort_OK\"><Long/></Member><Member name=\"VerPort_NG\">",
"<Long/></Member><Member name=\"VerCST_OK\"><Long/></Member><Member name=\"VerCST_NG\"><Long/></Member>",
"<Member name=\"VerCST2_OK\"><Long/></Member><Member name=\"VerCST2_NG\"><Long/></Member><Member name=\"With_CST\">",
"<Long/></Member><Member name=\"Without_CST\"><Long/></Member><Member name=\"With_CST2\"><Long/></Member>",
"<Member name=\"Without_CST2\"><Long/></Member><Member name=\"LoadStatus\"><Type name=\"VehLoadedStatus\"/>",
"</Member></Struct><Struct name=\"MotionInfo_Load\"><Member name=\"MCS_CSTID\"><String/></Member><Member name=\"MCS_CST2ID\">",
"<String/></Member><Member name=\"Veh_CSTID\"><String/></Member><Member name=\"Veh_CST2ID\"><String/></Member>",
"<Member name=\"VerPort_OK\"><Long/></Member><Member name=\"VerPort_NG\"><Long/></Member><Member name=\"VerCST_OK\">",
"<Long/></Member><Member name=\"VerCST_NG\"><Long/></Member><Member name=\"VerCST2_OK\"><Long/></Member>",
"<Member name=\"VerCST2_NG\"><Long/></Member><Member name=\"With_CST\"><Long/></Member><Member name=\"Without_CST\">",
"<Long/></Member><Member name=\"With_CST2\"><Long/></Member><Member name=\"Without_CST2\"><Long/></Member>",
"<Member name=\"LoadStatus\"><Type name=\"VehLoadedStatus\"/></Member></Struct><Struct name=\"MotionInfo_ReserveSectionPassReqst\">",
"<Member name=\"SectionList\"><Sequence><String/></Sequence></Member><Member name=\"ReserveSectionPassReqst\">",
"<Type name=\"Status\"/></Member></Struct><Struct name=\"MotionInfo_HIDSectionPassReqst\"><Member name=\"Section\">",
"<String/></Member><Member name=\"HIDSectionPassReqst\"><Type name=\"Status\"/></Member></Struct><Struct name=\"MotionInfo_BlockSectionPassReqst\">",
"<Member name=\"Section\"><String/></Member><Member name=\"BlockSectionPassReqst\"><Type name=\"Status\"/>",
"</Member></Struct><Struct name=\"MotionInfo_Vehicle_Inter_Comm_ReportData_134\"><Member name=\"vehID\">",
"<Long/></Member><Member name=\"vehName\"><String/></Member><Member name=\"loadStatus\"><Type name=\"MotionInfo_Load\"/>",
"</Member><Member name=\"unLoadStatus\"><Type name=\"MotionInfo_UnLoad\"/></Member><Member name=\"BlockSectionPassReqst\">",
"<Type name=\"MotionInfo_BlockSectionPassReqst\"/></Member><Member name=\"HIDSectionPassReqst\"><Type name=\"MotionInfo_HIDSectionPassReqst\"/>",
"</Member><Member name=\"ReserveSectionPassReqst\"><Type name=\"MotionInfo_ReserveSectionPassReqst\"/>",
"</Member><Member name=\"WheelAngle\"><Type name=\"VehWheelSteeringAngle\"/></Member><Member name=\"ConrtolMode\">",
"<Type name=\"VehControlMode\"/></Member><Member name=\"WhichType\"><Long/></Member><Member name=\"LocationType\">",
"<Long/></Member><Member name=\"Section\"><String/></Member><Member name=\"Address\"><String/></Member>",
"<Member name=\"Stage\"><String/></Member><Member name=\"DistanceFromSectionStart\"><Double/></Member>",
"<Member name=\"WalkLength\"><Double/></Member><Member name=\"PowerConsume\"><Double/></Member><Member name=\"Guiding\">",
"<Long/></Member><Member name=\"ReserveSection\"><String/></Member><Member name=\"BlockControlSection\">",
"<String/></Member><Member name=\"HIDControlSection\"><String/></Member><Member name=\"Proc_ON\"><Long/>",
"</Member><Member name=\"cmd_Send\"><Long/></Member><Member name=\"cmd_Receive\"><Long/></Member><Member name=\"cmpCode\">",
"<Long/></Member><Member name=\"cmpStatus\"><Long/></Member><Member name=\"stopStatusForEvent\"><Long/>",
"</Member><Member name=\"vehModeStatus\"><Long/></Member><Member name=\"vehActionStatus\"><Long/></Member>",
"<Member name=\"eventTypes\"><Long/></Member><Member name=\"actionType\"><Long/></Member><Member name=\"vehLeftGuideLockStatus\">",
"<Long/></Member><Member name=\"vehRightGuideLockStatus\"><Long/></Member><Member name=\"vehPauseStatus\">",
"<Long/></Member><Member name=\"vehBlockStopStatus\"><Long/></Member><Member name=\"vehReserveStopStatus\">",
"<Long/></Member><Member name=\"vehHIDStopStatus\"><Long/></Member><Member name=\"vehObstacleStopStatus\">",
"<Long/></Member><Member name=\"vehBlockSectionPassReqst\"><Long/></Member><Member name=\"vehHIDSectionPassReqst\">",
"<Long/></Member><Member name=\"vehReserveSectionPassReqst\"><Long/></Member><Member name=\"locationTypes\">",
"<Long/></Member><Member name=\"vehLoadStatus\"><Long/></Member><Member name=\"vehObstDist\"><Long/></Member>",
"<Member name=\"vehPowerStatus\"><Long/></Member><Member name=\"ChargeStatus\"><Long/></Member><Member name=\"BatteryCapacity\">",
"<Long/></Member><Member name=\"BatteryTemperature\"><Long/></Member><Member name=\"ErrorCode\"><Long/>",
"</Member><Member name=\"ErrorStatus\"><Long/></Member></Struct></Module></MetaData>"};

        public MotionInfo_Vehicle_Inter_Comm_ReportData_134TypeSupport()
            : base(typeof(MotionInfo_Vehicle_Inter_Comm_ReportData_134), metaDescriptor, "Veh_HandShakeData::MotionInfo_Vehicle_Inter_Comm_ReportData_134", "", "vehID")
        { }


        public override ReturnCode RegisterType(IDomainParticipant participant, string typeName)
        {
            return RegisterType(participant, typeName, new MotionInfo_Vehicle_Inter_Comm_ReportData_134Marshaler());
        }

        public override DDS.OpenSplice.DataWriter CreateDataWriter(DatabaseMarshaler marshaler)
        {
            return new MotionInfo_Vehicle_Inter_Comm_ReportData_134DataWriter(marshaler);
        }

        public override DDS.OpenSplice.DataReader CreateDataReader(DatabaseMarshaler marshaler)
        {
            return new MotionInfo_Vehicle_Inter_Comm_ReportData_134DataReader(marshaler);
        }
    }
    #endregion

    #region MotionInfo_Vehicle_Inter_Comm_ReportDataDataReader
    public class MotionInfo_Vehicle_Inter_Comm_ReportDataDataReader : DDS.OpenSplice.FooDataReader<MotionInfo_Vehicle_Inter_Comm_ReportData, MotionInfo_Vehicle_Inter_Comm_ReportDataMarshaler>, 
                                         IMotionInfo_Vehicle_Inter_Comm_ReportDataDataReader
    {
        public MotionInfo_Vehicle_Inter_Comm_ReportDataDataReader(DatabaseMarshaler marshaler)
            : base(marshaler) { }

        public ReturnCode Read(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Read(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Read(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Read(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public override ReturnCode Read(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.Read(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode Take(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Take(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Take(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Take(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public override ReturnCode Take(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.Take(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadWithCondition(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return ReadWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public override ReturnCode ReadWithCondition(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.ReadWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            return result;
        }

        public ReturnCode TakeWithCondition(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return TakeWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public override ReturnCode TakeWithCondition(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.TakeWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            return result;
        }

        public override ReturnCode ReadNextSample(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData dataValue,
                ref SampleInfo sampleInfo)
        {
            ReturnCode result =
                base.ReadNextSample(
                        ref dataValue,
                        ref sampleInfo);
            return result;
        }

        public override ReturnCode TakeNextSample(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData dataValue,
                ref SampleInfo sampleInfo)
        {
            ReturnCode result =
                base.TakeNextSample(
                        ref dataValue,
                        ref sampleInfo);
            return result;
        }

        public ReturnCode ReadInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode ReadInstance(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.ReadInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode TakeInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode TakeInstance(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.TakeInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadNextInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadNextInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode ReadNextInstance(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.ReadNextInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode TakeNextInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeNextInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode TakeNextInstance(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.TakeNextInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadNextInstanceWithCondition(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return ReadNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public override ReturnCode ReadNextInstanceWithCondition(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.ReadNextInstanceWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);
            return result;
        }

        public ReturnCode TakeNextInstanceWithCondition(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return TakeNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public override ReturnCode TakeNextInstanceWithCondition(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.TakeNextInstanceWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);

            return result;
        }

        public override ReturnCode ReturnLoan(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
                ref SampleInfo[] sampleInfos)
        {
            ReturnCode result =
                base.ReturnLoan(
                        ref dataValues,
                        ref sampleInfos);

            return result;
        }

        public override ReturnCode GetKeyValue(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData key,
                InstanceHandle handle)
        {
            ReturnCode result = base.GetKeyValue(
                        ref key,
                        handle);
            return result;
        }

        public override InstanceHandle LookupInstance(
                MotionInfo_Vehicle_Inter_Comm_ReportData instance)
        {
            return
                base.LookupInstance(
                        instance);
        }

    }
    #endregion
    
    #region MotionInfo_Vehicle_Inter_Comm_ReportDataDataWriter
    public class MotionInfo_Vehicle_Inter_Comm_ReportDataDataWriter : DDS.OpenSplice.FooDataWriter<MotionInfo_Vehicle_Inter_Comm_ReportData, MotionInfo_Vehicle_Inter_Comm_ReportDataMarshaler>, 
                                         IMotionInfo_Vehicle_Inter_Comm_ReportDataDataWriter
    {
        public MotionInfo_Vehicle_Inter_Comm_ReportDataDataWriter(DatabaseMarshaler marshaler)
            : base(marshaler) { }

        public InstanceHandle RegisterInstance(
                MotionInfo_Vehicle_Inter_Comm_ReportData instanceData)
        {
            return base.RegisterInstance(
                    instanceData,
                    Time.Current);
        }

        public InstanceHandle RegisterInstanceWithTimestamp(
                MotionInfo_Vehicle_Inter_Comm_ReportData instanceData,
                Time sourceTimestamp)
        {
            return base.RegisterInstance(
                    instanceData,
                    sourceTimestamp);
        }

        public ReturnCode UnregisterInstance(
                MotionInfo_Vehicle_Inter_Comm_ReportData instanceData,
                InstanceHandle instanceHandle)
        {
            return base.UnregisterInstance(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode UnregisterInstanceWithTimestamp(
                MotionInfo_Vehicle_Inter_Comm_ReportData instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.UnregisterInstance(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode Write(MotionInfo_Vehicle_Inter_Comm_ReportData instanceData)
        {
            return base.Write(
                    instanceData,
                    InstanceHandle.Nil,
                    Time.Current);
        }

        public ReturnCode Write(
                MotionInfo_Vehicle_Inter_Comm_ReportData instanceData,
                InstanceHandle instanceHandle)
        {
            return base.Write(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode WriteWithTimestamp(
                MotionInfo_Vehicle_Inter_Comm_ReportData instanceData,
                Time sourceTimestamp)
        {
            return base.Write(
                    instanceData,
                    InstanceHandle.Nil,
                    sourceTimestamp);
        }

        public ReturnCode WriteWithTimestamp(
                MotionInfo_Vehicle_Inter_Comm_ReportData instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.Write(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode Dispose(
                MotionInfo_Vehicle_Inter_Comm_ReportData instanceData,
                InstanceHandle instanceHandle)
        {
            return base.Dispose(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode DisposeWithTimestamp(
                MotionInfo_Vehicle_Inter_Comm_ReportData instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.Dispose(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode WriteDispose(
                MotionInfo_Vehicle_Inter_Comm_ReportData instanceData)
        {
            return base.WriteDispose(
                    instanceData,
                    InstanceHandle.Nil,
                    Time.Current);
        }

        public ReturnCode WriteDispose(
                MotionInfo_Vehicle_Inter_Comm_ReportData instanceData,
                InstanceHandle instanceHandle)
        {
            return base.WriteDispose(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                MotionInfo_Vehicle_Inter_Comm_ReportData instanceData,
                Time sourceTimestamp)
        {
            return base.WriteDispose(
                    instanceData,
                    InstanceHandle.Nil,
                    sourceTimestamp);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                MotionInfo_Vehicle_Inter_Comm_ReportData instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.WriteDispose(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public override ReturnCode GetKeyValue(
                ref MotionInfo_Vehicle_Inter_Comm_ReportData key,
                InstanceHandle instanceHandle)
        {
            return base.GetKeyValue(ref key, instanceHandle);
        }

        public override InstanceHandle LookupInstance(
            MotionInfo_Vehicle_Inter_Comm_ReportData instanceData)
        {
            return base.LookupInstance(instanceData);
        }
    }
    #endregion

    #region MotionInfo_Vehicle_Inter_Comm_ReportDataTypeSupport
    public class MotionInfo_Vehicle_Inter_Comm_ReportDataTypeSupport : DDS.OpenSplice.TypeSupport
    {
        private static readonly string[] metaDescriptor = {"<MetaData version=\"1.0.0\"><Module name=\"Veh_HandShakeData\"><Enum name=\"VehLoadedStatus\"><Element name=\"NotExisted\" value=\"0\"/>",
"<Element name=\"Existed\" value=\"1\"/></Enum><Enum name=\"Status\"><Element name=\"OK\" value=\"0\"/>",
"<Element name=\"NG\" value=\"1\"/><Element name=\"TimeOut\" value=\"2\"/></Enum><Enum name=\"VehWheelSteeringAngle\">",
"<Element name=\"Zero\" value=\"0\"/><Element name=\"RightNinety\" value=\"1\"/><Element name=\"LeftNinety\" value=\"2\"/>",
"</Enum><Enum name=\"VehControlMode\"><Element name=\"OnlineRemote\" value=\"0\"/><Element name=\"OnlineLocal\" value=\"1\"/>",
"<Element name=\"Offline\" value=\"2\"/></Enum><Struct name=\"MotionInfo_UnLoad\"><Member name=\"MCS_CSTID\">",
"<String/></Member><Member name=\"MCS_CST2ID\"><String/></Member><Member name=\"Veh_CSTID\"><String/></Member>",
"<Member name=\"Veh_CST2ID\"><String/></Member><Member name=\"VerPort_OK\"><Long/></Member><Member name=\"VerPort_NG\">",
"<Long/></Member><Member name=\"VerCST_OK\"><Long/></Member><Member name=\"VerCST_NG\"><Long/></Member>",
"<Member name=\"VerCST2_OK\"><Long/></Member><Member name=\"VerCST2_NG\"><Long/></Member><Member name=\"With_CST\">",
"<Long/></Member><Member name=\"Without_CST\"><Long/></Member><Member name=\"With_CST2\"><Long/></Member>",
"<Member name=\"Without_CST2\"><Long/></Member><Member name=\"LoadStatus\"><Type name=\"VehLoadedStatus\"/>",
"</Member></Struct><Struct name=\"MotionInfo_Load\"><Member name=\"MCS_CSTID\"><String/></Member><Member name=\"MCS_CST2ID\">",
"<String/></Member><Member name=\"Veh_CSTID\"><String/></Member><Member name=\"Veh_CST2ID\"><String/></Member>",
"<Member name=\"VerPort_OK\"><Long/></Member><Member name=\"VerPort_NG\"><Long/></Member><Member name=\"VerCST_OK\">",
"<Long/></Member><Member name=\"VerCST_NG\"><Long/></Member><Member name=\"VerCST2_OK\"><Long/></Member>",
"<Member name=\"VerCST2_NG\"><Long/></Member><Member name=\"With_CST\"><Long/></Member><Member name=\"Without_CST\">",
"<Long/></Member><Member name=\"With_CST2\"><Long/></Member><Member name=\"Without_CST2\"><Long/></Member>",
"<Member name=\"LoadStatus\"><Type name=\"VehLoadedStatus\"/></Member></Struct><Struct name=\"MotionInfo_ReserveSectionPassReqst\">",
"<Member name=\"SectionList\"><Sequence><String/></Sequence></Member><Member name=\"ReserveSectionPassReqst\">",
"<Type name=\"Status\"/></Member></Struct><Struct name=\"MotionInfo_HIDSectionPassReqst\"><Member name=\"Section\">",
"<String/></Member><Member name=\"HIDSectionPassReqst\"><Type name=\"Status\"/></Member></Struct><Struct name=\"MotionInfo_BlockSectionPassReqst\">",
"<Member name=\"Section\"><String/></Member><Member name=\"BlockSectionPassReqst\"><Type name=\"Status\"/>",
"</Member></Struct><Struct name=\"MotionInfo_Vehicle_Inter_Comm_ReportData\"><Member name=\"vehID\"><Long/>",
"</Member><Member name=\"vehName\"><String/></Member><Member name=\"loadStatus\"><Type name=\"MotionInfo_Load\"/>",
"</Member><Member name=\"unLoadStatus\"><Type name=\"MotionInfo_UnLoad\"/></Member><Member name=\"BlockSectionPassReqst\">",
"<Type name=\"MotionInfo_BlockSectionPassReqst\"/></Member><Member name=\"HIDSectionPassReqst\"><Type name=\"MotionInfo_HIDSectionPassReqst\"/>",
"</Member><Member name=\"ReserveSectionPassReqst\"><Type name=\"MotionInfo_ReserveSectionPassReqst\"/>",
"</Member><Member name=\"WheelAngle\"><Type name=\"VehWheelSteeringAngle\"/></Member><Member name=\"ConrtolMode\">",
"<Type name=\"VehControlMode\"/></Member><Member name=\"WhichType\"><Long/></Member><Member name=\"LocationType\">",
"<Long/></Member><Member name=\"Section\"><String/></Member><Member name=\"Address\"><String/></Member>",
"<Member name=\"Stage\"><String/></Member><Member name=\"DistanceFromSectionStart\"><Double/></Member>",
"<Member name=\"WalkLength\"><Double/></Member><Member name=\"PowerConsume\"><Double/></Member><Member name=\"Guiding\">",
"<Long/></Member><Member name=\"ReserveSection\"><String/></Member><Member name=\"BlockControlSection\">",
"<String/></Member><Member name=\"HIDControlSection\"><String/></Member><Member name=\"Proc_ON\"><Long/>",
"</Member><Member name=\"cmd_Send\"><Long/></Member><Member name=\"cmd_Receive\"><Long/></Member><Member name=\"cmpCode\">",
"<Long/></Member><Member name=\"cmpStatus\"><Long/></Member><Member name=\"stopStatusForEvent\"><Long/>",
"</Member><Member name=\"vehModeStatus\"><Long/></Member><Member name=\"vehActionStatus\"><Long/></Member>",
"<Member name=\"eventTypes\"><Long/></Member><Member name=\"actionType\"><Long/></Member><Member name=\"vehLeftGuideLockStatus\">",
"<Long/></Member><Member name=\"vehRightGuideLockStatus\"><Long/></Member><Member name=\"vehPauseStatus\">",
"<Long/></Member><Member name=\"vehBlockStopStatus\"><Long/></Member><Member name=\"vehReserveStopStatus\">",
"<Long/></Member><Member name=\"vehHIDStopStatus\"><Long/></Member><Member name=\"vehObstacleStopStatus\">",
"<Long/></Member><Member name=\"vehBlockSectionPassReqst\"><Long/></Member><Member name=\"vehHIDSectionPassReqst\">",
"<Long/></Member><Member name=\"vehReserveSectionPassReqst\"><Long/></Member><Member name=\"locationTypes\">",
"<Long/></Member><Member name=\"vehLoadStatus\"><Long/></Member><Member name=\"vehObstDist\"><Long/></Member>",
"<Member name=\"vehPowerStatus\"><Long/></Member><Member name=\"ChargeStatus\"><Long/></Member><Member name=\"BatteryCapacity\">",
"<Long/></Member><Member name=\"BatteryTemperature\"><Long/></Member><Member name=\"ErrorCode\"><Long/>",
"</Member><Member name=\"ErrorStatus\"><Long/></Member></Struct></Module></MetaData>"};

        public MotionInfo_Vehicle_Inter_Comm_ReportDataTypeSupport()
            : base(typeof(MotionInfo_Vehicle_Inter_Comm_ReportData), metaDescriptor, "Veh_HandShakeData::MotionInfo_Vehicle_Inter_Comm_ReportData", "", "vehID")
        { }


        public override ReturnCode RegisterType(IDomainParticipant participant, string typeName)
        {
            return RegisterType(participant, typeName, new MotionInfo_Vehicle_Inter_Comm_ReportDataMarshaler());
        }

        public override DDS.OpenSplice.DataWriter CreateDataWriter(DatabaseMarshaler marshaler)
        {
            return new MotionInfo_Vehicle_Inter_Comm_ReportDataDataWriter(marshaler);
        }

        public override DDS.OpenSplice.DataReader CreateDataReader(DatabaseMarshaler marshaler)
        {
            return new MotionInfo_Vehicle_Inter_Comm_ReportDataDataReader(marshaler);
        }
    }
    #endregion

    #region MotionInfo_HandShake_SendDataDataReader
    public class MotionInfo_HandShake_SendDataDataReader : DDS.OpenSplice.FooDataReader<MotionInfo_HandShake_SendData, MotionInfo_HandShake_SendDataMarshaler>, 
                                         IMotionInfo_HandShake_SendDataDataReader
    {
        public MotionInfo_HandShake_SendDataDataReader(DatabaseMarshaler marshaler)
            : base(marshaler) { }

        public ReturnCode Read(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Read(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Read(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Read(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public override ReturnCode Read(
                ref MotionInfo_HandShake_SendData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.Read(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode Take(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Take(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Take(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Take(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public override ReturnCode Take(
                ref MotionInfo_HandShake_SendData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.Take(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadWithCondition(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return ReadWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public override ReturnCode ReadWithCondition(
                ref MotionInfo_HandShake_SendData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.ReadWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            return result;
        }

        public ReturnCode TakeWithCondition(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return TakeWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public override ReturnCode TakeWithCondition(
                ref MotionInfo_HandShake_SendData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.TakeWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            return result;
        }

        public override ReturnCode ReadNextSample(
                ref MotionInfo_HandShake_SendData dataValue,
                ref SampleInfo sampleInfo)
        {
            ReturnCode result =
                base.ReadNextSample(
                        ref dataValue,
                        ref sampleInfo);
            return result;
        }

        public override ReturnCode TakeNextSample(
                ref MotionInfo_HandShake_SendData dataValue,
                ref SampleInfo sampleInfo)
        {
            ReturnCode result =
                base.TakeNextSample(
                        ref dataValue,
                        ref sampleInfo);
            return result;
        }

        public ReturnCode ReadInstance(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadInstance(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode ReadInstance(
                ref MotionInfo_HandShake_SendData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.ReadInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode TakeInstance(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeInstance(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode TakeInstance(
                ref MotionInfo_HandShake_SendData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.TakeInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadNextInstance(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadNextInstance(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode ReadNextInstance(
                ref MotionInfo_HandShake_SendData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.ReadNextInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode TakeNextInstance(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeNextInstance(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode TakeNextInstance(
                ref MotionInfo_HandShake_SendData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.TakeNextInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadNextInstanceWithCondition(
                ref MotionInfo_HandShake_SendData[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return ReadNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public override ReturnCode ReadNextInstanceWithCondition(
                ref MotionInfo_HandShake_SendData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.ReadNextInstanceWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);
            return result;
        }

        public ReturnCode TakeNextInstanceWithCondition(
                ref MotionInfo_HandShake_SendData[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return TakeNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public override ReturnCode TakeNextInstanceWithCondition(
                ref MotionInfo_HandShake_SendData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.TakeNextInstanceWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);

            return result;
        }

        public override ReturnCode ReturnLoan(
                ref MotionInfo_HandShake_SendData[] dataValues,
                ref SampleInfo[] sampleInfos)
        {
            ReturnCode result =
                base.ReturnLoan(
                        ref dataValues,
                        ref sampleInfos);

            return result;
        }

        public override ReturnCode GetKeyValue(
                ref MotionInfo_HandShake_SendData key,
                InstanceHandle handle)
        {
            ReturnCode result = base.GetKeyValue(
                        ref key,
                        handle);
            return result;
        }

        public override InstanceHandle LookupInstance(
                MotionInfo_HandShake_SendData instance)
        {
            return
                base.LookupInstance(
                        instance);
        }

    }
    #endregion
    
    #region MotionInfo_HandShake_SendDataDataWriter
    public class MotionInfo_HandShake_SendDataDataWriter : DDS.OpenSplice.FooDataWriter<MotionInfo_HandShake_SendData, MotionInfo_HandShake_SendDataMarshaler>, 
                                         IMotionInfo_HandShake_SendDataDataWriter
    {
        public MotionInfo_HandShake_SendDataDataWriter(DatabaseMarshaler marshaler)
            : base(marshaler) { }

        public InstanceHandle RegisterInstance(
                MotionInfo_HandShake_SendData instanceData)
        {
            return base.RegisterInstance(
                    instanceData,
                    Time.Current);
        }

        public InstanceHandle RegisterInstanceWithTimestamp(
                MotionInfo_HandShake_SendData instanceData,
                Time sourceTimestamp)
        {
            return base.RegisterInstance(
                    instanceData,
                    sourceTimestamp);
        }

        public ReturnCode UnregisterInstance(
                MotionInfo_HandShake_SendData instanceData,
                InstanceHandle instanceHandle)
        {
            return base.UnregisterInstance(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode UnregisterInstanceWithTimestamp(
                MotionInfo_HandShake_SendData instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.UnregisterInstance(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode Write(MotionInfo_HandShake_SendData instanceData)
        {
            return base.Write(
                    instanceData,
                    InstanceHandle.Nil,
                    Time.Current);
        }

        public ReturnCode Write(
                MotionInfo_HandShake_SendData instanceData,
                InstanceHandle instanceHandle)
        {
            return base.Write(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode WriteWithTimestamp(
                MotionInfo_HandShake_SendData instanceData,
                Time sourceTimestamp)
        {
            return base.Write(
                    instanceData,
                    InstanceHandle.Nil,
                    sourceTimestamp);
        }

        public ReturnCode WriteWithTimestamp(
                MotionInfo_HandShake_SendData instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.Write(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode Dispose(
                MotionInfo_HandShake_SendData instanceData,
                InstanceHandle instanceHandle)
        {
            return base.Dispose(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode DisposeWithTimestamp(
                MotionInfo_HandShake_SendData instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.Dispose(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode WriteDispose(
                MotionInfo_HandShake_SendData instanceData)
        {
            return base.WriteDispose(
                    instanceData,
                    InstanceHandle.Nil,
                    Time.Current);
        }

        public ReturnCode WriteDispose(
                MotionInfo_HandShake_SendData instanceData,
                InstanceHandle instanceHandle)
        {
            return base.WriteDispose(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                MotionInfo_HandShake_SendData instanceData,
                Time sourceTimestamp)
        {
            return base.WriteDispose(
                    instanceData,
                    InstanceHandle.Nil,
                    sourceTimestamp);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                MotionInfo_HandShake_SendData instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.WriteDispose(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public override ReturnCode GetKeyValue(
                ref MotionInfo_HandShake_SendData key,
                InstanceHandle instanceHandle)
        {
            return base.GetKeyValue(ref key, instanceHandle);
        }

        public override InstanceHandle LookupInstance(
            MotionInfo_HandShake_SendData instanceData)
        {
            return base.LookupInstance(instanceData);
        }
    }
    #endregion

    #region MotionInfo_HandShake_SendDataTypeSupport
    public class MotionInfo_HandShake_SendDataTypeSupport : DDS.OpenSplice.TypeSupport
    {
        private static readonly string[] metaDescriptor = {"<MetaData version=\"1.0.0\"><Module name=\"Veh_HandShakeData\"><Struct name=\"MotionInfo_HandShake_SendData\">",
"<Member name=\"vehID\"><Long/></Member><Member name=\"vehName\"><String/></Member><Member name=\"cmdSend\">",
"<Long/></Member><Member name=\"cmdReceive\"><Long/></Member></Struct></Module></MetaData>"};

        public MotionInfo_HandShake_SendDataTypeSupport()
            : base(typeof(MotionInfo_HandShake_SendData), metaDescriptor, "Veh_HandShakeData::MotionInfo_HandShake_SendData", "", "vehID")
        { }


        public override ReturnCode RegisterType(IDomainParticipant participant, string typeName)
        {
            return RegisterType(participant, typeName, new MotionInfo_HandShake_SendDataMarshaler());
        }

        public override DDS.OpenSplice.DataWriter CreateDataWriter(DatabaseMarshaler marshaler)
        {
            return new MotionInfo_HandShake_SendDataDataWriter(marshaler);
        }

        public override DDS.OpenSplice.DataReader CreateDataReader(DatabaseMarshaler marshaler)
        {
            return new MotionInfo_HandShake_SendDataDataReader(marshaler);
        }
    }
    #endregion

    #region MotionInfo_HandShake_RecieveDataDataReader
    public class MotionInfo_HandShake_RecieveDataDataReader : DDS.OpenSplice.FooDataReader<MotionInfo_HandShake_RecieveData, MotionInfo_HandShake_RecieveDataMarshaler>, 
                                         IMotionInfo_HandShake_RecieveDataDataReader
    {
        public MotionInfo_HandShake_RecieveDataDataReader(DatabaseMarshaler marshaler)
            : base(marshaler) { }

        public ReturnCode Read(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Read(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Read(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Read(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public override ReturnCode Read(
                ref MotionInfo_HandShake_RecieveData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.Read(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode Take(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Take(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Take(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Take(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public override ReturnCode Take(
                ref MotionInfo_HandShake_RecieveData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.Take(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadWithCondition(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return ReadWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public override ReturnCode ReadWithCondition(
                ref MotionInfo_HandShake_RecieveData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.ReadWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            return result;
        }

        public ReturnCode TakeWithCondition(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return TakeWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public override ReturnCode TakeWithCondition(
                ref MotionInfo_HandShake_RecieveData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.TakeWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            return result;
        }

        public override ReturnCode ReadNextSample(
                ref MotionInfo_HandShake_RecieveData dataValue,
                ref SampleInfo sampleInfo)
        {
            ReturnCode result =
                base.ReadNextSample(
                        ref dataValue,
                        ref sampleInfo);
            return result;
        }

        public override ReturnCode TakeNextSample(
                ref MotionInfo_HandShake_RecieveData dataValue,
                ref SampleInfo sampleInfo)
        {
            ReturnCode result =
                base.TakeNextSample(
                        ref dataValue,
                        ref sampleInfo);
            return result;
        }

        public ReturnCode ReadInstance(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadInstance(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode ReadInstance(
                ref MotionInfo_HandShake_RecieveData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.ReadInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode TakeInstance(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeInstance(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode TakeInstance(
                ref MotionInfo_HandShake_RecieveData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.TakeInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadNextInstance(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadNextInstance(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode ReadNextInstance(
                ref MotionInfo_HandShake_RecieveData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.ReadNextInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode TakeNextInstance(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeNextInstance(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode TakeNextInstance(
                ref MotionInfo_HandShake_RecieveData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.TakeNextInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadNextInstanceWithCondition(
                ref MotionInfo_HandShake_RecieveData[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return ReadNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public override ReturnCode ReadNextInstanceWithCondition(
                ref MotionInfo_HandShake_RecieveData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.ReadNextInstanceWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);
            return result;
        }

        public ReturnCode TakeNextInstanceWithCondition(
                ref MotionInfo_HandShake_RecieveData[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return TakeNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public override ReturnCode TakeNextInstanceWithCondition(
                ref MotionInfo_HandShake_RecieveData[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.TakeNextInstanceWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);

            return result;
        }

        public override ReturnCode ReturnLoan(
                ref MotionInfo_HandShake_RecieveData[] dataValues,
                ref SampleInfo[] sampleInfos)
        {
            ReturnCode result =
                base.ReturnLoan(
                        ref dataValues,
                        ref sampleInfos);

            return result;
        }

        public override ReturnCode GetKeyValue(
                ref MotionInfo_HandShake_RecieveData key,
                InstanceHandle handle)
        {
            ReturnCode result = base.GetKeyValue(
                        ref key,
                        handle);
            return result;
        }

        public override InstanceHandle LookupInstance(
                MotionInfo_HandShake_RecieveData instance)
        {
            return
                base.LookupInstance(
                        instance);
        }

    }
    #endregion
    
    #region MotionInfo_HandShake_RecieveDataDataWriter
    public class MotionInfo_HandShake_RecieveDataDataWriter : DDS.OpenSplice.FooDataWriter<MotionInfo_HandShake_RecieveData, MotionInfo_HandShake_RecieveDataMarshaler>, 
                                         IMotionInfo_HandShake_RecieveDataDataWriter
    {
        public MotionInfo_HandShake_RecieveDataDataWriter(DatabaseMarshaler marshaler)
            : base(marshaler) { }

        public InstanceHandle RegisterInstance(
                MotionInfo_HandShake_RecieveData instanceData)
        {
            return base.RegisterInstance(
                    instanceData,
                    Time.Current);
        }

        public InstanceHandle RegisterInstanceWithTimestamp(
                MotionInfo_HandShake_RecieveData instanceData,
                Time sourceTimestamp)
        {
            return base.RegisterInstance(
                    instanceData,
                    sourceTimestamp);
        }

        public ReturnCode UnregisterInstance(
                MotionInfo_HandShake_RecieveData instanceData,
                InstanceHandle instanceHandle)
        {
            return base.UnregisterInstance(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode UnregisterInstanceWithTimestamp(
                MotionInfo_HandShake_RecieveData instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.UnregisterInstance(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode Write(MotionInfo_HandShake_RecieveData instanceData)
        {
            return base.Write(
                    instanceData,
                    InstanceHandle.Nil,
                    Time.Current);
        }

        public ReturnCode Write(
                MotionInfo_HandShake_RecieveData instanceData,
                InstanceHandle instanceHandle)
        {
            return base.Write(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode WriteWithTimestamp(
                MotionInfo_HandShake_RecieveData instanceData,
                Time sourceTimestamp)
        {
            return base.Write(
                    instanceData,
                    InstanceHandle.Nil,
                    sourceTimestamp);
        }

        public ReturnCode WriteWithTimestamp(
                MotionInfo_HandShake_RecieveData instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.Write(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode Dispose(
                MotionInfo_HandShake_RecieveData instanceData,
                InstanceHandle instanceHandle)
        {
            return base.Dispose(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode DisposeWithTimestamp(
                MotionInfo_HandShake_RecieveData instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.Dispose(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode WriteDispose(
                MotionInfo_HandShake_RecieveData instanceData)
        {
            return base.WriteDispose(
                    instanceData,
                    InstanceHandle.Nil,
                    Time.Current);
        }

        public ReturnCode WriteDispose(
                MotionInfo_HandShake_RecieveData instanceData,
                InstanceHandle instanceHandle)
        {
            return base.WriteDispose(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                MotionInfo_HandShake_RecieveData instanceData,
                Time sourceTimestamp)
        {
            return base.WriteDispose(
                    instanceData,
                    InstanceHandle.Nil,
                    sourceTimestamp);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                MotionInfo_HandShake_RecieveData instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.WriteDispose(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public override ReturnCode GetKeyValue(
                ref MotionInfo_HandShake_RecieveData key,
                InstanceHandle instanceHandle)
        {
            return base.GetKeyValue(ref key, instanceHandle);
        }

        public override InstanceHandle LookupInstance(
            MotionInfo_HandShake_RecieveData instanceData)
        {
            return base.LookupInstance(instanceData);
        }
    }
    #endregion

    #region MotionInfo_HandShake_RecieveDataTypeSupport
    public class MotionInfo_HandShake_RecieveDataTypeSupport : DDS.OpenSplice.TypeSupport
    {
        private static readonly string[] metaDescriptor = {"<MetaData version=\"1.0.0\"><Module name=\"Veh_HandShakeData\"><Struct name=\"MotionInfo_HandShake_RecieveData\">",
"<Member name=\"vehID\"><Long/></Member><Member name=\"vehName\"><String/></Member><Member name=\"cmdSend\">",
"<Long/></Member><Member name=\"cmdReceive\"><Long/></Member></Struct></Module></MetaData>"};

        public MotionInfo_HandShake_RecieveDataTypeSupport()
            : base(typeof(MotionInfo_HandShake_RecieveData), metaDescriptor, "Veh_HandShakeData::MotionInfo_HandShake_RecieveData", "", "vehID")
        { }


        public override ReturnCode RegisterType(IDomainParticipant participant, string typeName)
        {
            return RegisterType(participant, typeName, new MotionInfo_HandShake_RecieveDataMarshaler());
        }

        public override DDS.OpenSplice.DataWriter CreateDataWriter(DatabaseMarshaler marshaler)
        {
            return new MotionInfo_HandShake_RecieveDataDataWriter(marshaler);
        }

        public override DDS.OpenSplice.DataReader CreateDataReader(DatabaseMarshaler marshaler)
        {
            return new MotionInfo_HandShake_RecieveDataDataReader(marshaler);
        }
    }
    #endregion

    #region Between_Vehicle_DataDataReader
    public class Between_Vehicle_DataDataReader : DDS.OpenSplice.FooDataReader<Between_Vehicle_Data, Between_Vehicle_DataMarshaler>, 
                                         IBetween_Vehicle_DataDataReader
    {
        public Between_Vehicle_DataDataReader(DatabaseMarshaler marshaler)
            : base(marshaler) { }

        public ReturnCode Read(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Read(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Read(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Read(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public override ReturnCode Read(
                ref Between_Vehicle_Data[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.Read(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode Take(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Take(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Take(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Take(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public override ReturnCode Take(
                ref Between_Vehicle_Data[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.Take(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadWithCondition(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return ReadWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public override ReturnCode ReadWithCondition(
                ref Between_Vehicle_Data[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.ReadWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            return result;
        }

        public ReturnCode TakeWithCondition(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return TakeWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public override ReturnCode TakeWithCondition(
                ref Between_Vehicle_Data[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.TakeWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            return result;
        }

        public override ReturnCode ReadNextSample(
                ref Between_Vehicle_Data dataValue,
                ref SampleInfo sampleInfo)
        {
            ReturnCode result =
                base.ReadNextSample(
                        ref dataValue,
                        ref sampleInfo);
            return result;
        }

        public override ReturnCode TakeNextSample(
                ref Between_Vehicle_Data dataValue,
                ref SampleInfo sampleInfo)
        {
            ReturnCode result =
                base.TakeNextSample(
                        ref dataValue,
                        ref sampleInfo);
            return result;
        }

        public ReturnCode ReadInstance(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadInstance(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode ReadInstance(
                ref Between_Vehicle_Data[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.ReadInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode TakeInstance(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeInstance(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode TakeInstance(
                ref Between_Vehicle_Data[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.TakeInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadNextInstance(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadNextInstance(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode ReadNextInstance(
                ref Between_Vehicle_Data[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.ReadNextInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode TakeNextInstance(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeNextInstance(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode TakeNextInstance(
                ref Between_Vehicle_Data[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.TakeNextInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadNextInstanceWithCondition(
                ref Between_Vehicle_Data[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return ReadNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public override ReturnCode ReadNextInstanceWithCondition(
                ref Between_Vehicle_Data[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.ReadNextInstanceWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);
            return result;
        }

        public ReturnCode TakeNextInstanceWithCondition(
                ref Between_Vehicle_Data[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return TakeNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public override ReturnCode TakeNextInstanceWithCondition(
                ref Between_Vehicle_Data[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.TakeNextInstanceWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);

            return result;
        }

        public override ReturnCode ReturnLoan(
                ref Between_Vehicle_Data[] dataValues,
                ref SampleInfo[] sampleInfos)
        {
            ReturnCode result =
                base.ReturnLoan(
                        ref dataValues,
                        ref sampleInfos);

            return result;
        }

        public override ReturnCode GetKeyValue(
                ref Between_Vehicle_Data key,
                InstanceHandle handle)
        {
            ReturnCode result = base.GetKeyValue(
                        ref key,
                        handle);
            return result;
        }

        public override InstanceHandle LookupInstance(
                Between_Vehicle_Data instance)
        {
            return
                base.LookupInstance(
                        instance);
        }

    }
    #endregion
    
    #region Between_Vehicle_DataDataWriter
    public class Between_Vehicle_DataDataWriter : DDS.OpenSplice.FooDataWriter<Between_Vehicle_Data, Between_Vehicle_DataMarshaler>, 
                                         IBetween_Vehicle_DataDataWriter
    {
        public Between_Vehicle_DataDataWriter(DatabaseMarshaler marshaler)
            : base(marshaler) { }

        public InstanceHandle RegisterInstance(
                Between_Vehicle_Data instanceData)
        {
            return base.RegisterInstance(
                    instanceData,
                    Time.Current);
        }

        public InstanceHandle RegisterInstanceWithTimestamp(
                Between_Vehicle_Data instanceData,
                Time sourceTimestamp)
        {
            return base.RegisterInstance(
                    instanceData,
                    sourceTimestamp);
        }

        public ReturnCode UnregisterInstance(
                Between_Vehicle_Data instanceData,
                InstanceHandle instanceHandle)
        {
            return base.UnregisterInstance(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode UnregisterInstanceWithTimestamp(
                Between_Vehicle_Data instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.UnregisterInstance(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode Write(Between_Vehicle_Data instanceData)
        {
            return base.Write(
                    instanceData,
                    InstanceHandle.Nil,
                    Time.Current);
        }

        public ReturnCode Write(
                Between_Vehicle_Data instanceData,
                InstanceHandle instanceHandle)
        {
            return base.Write(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode WriteWithTimestamp(
                Between_Vehicle_Data instanceData,
                Time sourceTimestamp)
        {
            return base.Write(
                    instanceData,
                    InstanceHandle.Nil,
                    sourceTimestamp);
        }

        public ReturnCode WriteWithTimestamp(
                Between_Vehicle_Data instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.Write(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode Dispose(
                Between_Vehicle_Data instanceData,
                InstanceHandle instanceHandle)
        {
            return base.Dispose(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode DisposeWithTimestamp(
                Between_Vehicle_Data instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.Dispose(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode WriteDispose(
                Between_Vehicle_Data instanceData)
        {
            return base.WriteDispose(
                    instanceData,
                    InstanceHandle.Nil,
                    Time.Current);
        }

        public ReturnCode WriteDispose(
                Between_Vehicle_Data instanceData,
                InstanceHandle instanceHandle)
        {
            return base.WriteDispose(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                Between_Vehicle_Data instanceData,
                Time sourceTimestamp)
        {
            return base.WriteDispose(
                    instanceData,
                    InstanceHandle.Nil,
                    sourceTimestamp);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                Between_Vehicle_Data instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.WriteDispose(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public override ReturnCode GetKeyValue(
                ref Between_Vehicle_Data key,
                InstanceHandle instanceHandle)
        {
            return base.GetKeyValue(ref key, instanceHandle);
        }

        public override InstanceHandle LookupInstance(
            Between_Vehicle_Data instanceData)
        {
            return base.LookupInstance(instanceData);
        }
    }
    #endregion

    #region Between_Vehicle_DataTypeSupport
    public class Between_Vehicle_DataTypeSupport : DDS.OpenSplice.TypeSupport
    {
        private static readonly string[] metaDescriptor = {"<MetaData version=\"1.0.0\"><Module name=\"Veh_HandShakeData\"><Struct name=\"Between_Vehicle_Data\">",
"<Member name=\"Vehicle_ID\"><String/></Member><Member name=\"InService\"><Boolean/></Member><Member name=\"EQ_Online\">",
"<Boolean/></Member><Member name=\"EQ_Error\"><Boolean/></Member><Member name=\"IsMoving\"><Boolean/></Member>",
"<Member name=\"IsHoisting\"><Boolean/></Member><Member name=\"Current_Zone_ID\"><String/></Member><Member name=\"Current_Zone_ID2\">",
"<String/></Member><Member name=\"Current_Zone_ID3\"><String/></Member><Member name=\"Current_Section_ID\">",
"<String/></Member><Member name=\"Current_Section_Offset\"><Long/></Member><Member name=\"Current_Map_HeadingPose_Angle\">",
"<Double/></Member><Member name=\"Current_Map_AbsPos_X\"><Long/></Member><Member name=\"Current_Map_AbsPos_Y\">",
"<Long/></Member><Member name=\"Current_Address_ID\"><String/></Member><Member name=\"Current_Stage_ID\">",
"<String/></Member><Member name=\"BlockQry_Zone_ID_1\"><String/></Member><Member name=\"BlockQry_Zone_ID_2\">",
"<String/></Member><Member name=\"BlockQry_Zone_ID_3\"><String/></Member><Member name=\"BlockQry_Zone_ID_4\">",
"<String/></Member><Member name=\"BlockQry_Zone_ID_5\"><String/></Member><Member name=\"BlockQry_Zone_ID_6\">",
"<String/></Member><Member name=\"BlockQry_Zone_ID_7\"><String/></Member><Member name=\"BlockQry_Zone_ID_8\">",
"<String/></Member><Member name=\"BlockQry_Zone_ID_9\"><String/></Member><Member name=\"Blocking_Zone_Owner\">",
"<String/></Member><Member name=\"Blocking_Zone_ID\"><String/></Member><Member name=\"Blocking_ZoneEntry_Distance\">",
"<Long/></Member><Member name=\"UniqueSections_FromNow2JustB4QryBlockZoneExit\"><Sequence><String/></Sequence>",
"</Member><Member name=\"UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraSplit\"><Sequence><String/>",
"</Sequence></Member><Member name=\"UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraMerge\"><Sequence>",
"<String/></Sequence></Member><Member name=\"UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraNearby\">",
"<Sequence><String/></Sequence></Member><Member name=\"UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraTwoWay\">",
"<Sequence><String/></Sequence></Member><Member name=\"Status1\"><Long/></Member><Member name=\"Status2\">",
"<Long/></Member><Member name=\"Start_Time\"><String/></Member><Member name=\"Software_Version\"><String/>",
"</Member><Member name=\"Updating_Timestamp\"><String/></Member></Struct></Module></MetaData>"};

        public Between_Vehicle_DataTypeSupport()
            : base(typeof(Between_Vehicle_Data), metaDescriptor, "Veh_HandShakeData::Between_Vehicle_Data", "", "Vehicle_ID")
        { }


        public override ReturnCode RegisterType(IDomainParticipant participant, string typeName)
        {
            return RegisterType(participant, typeName, new Between_Vehicle_DataMarshaler());
        }

        public override DDS.OpenSplice.DataWriter CreateDataWriter(DatabaseMarshaler marshaler)
        {
            return new Between_Vehicle_DataDataWriter(marshaler);
        }

        public override DDS.OpenSplice.DataReader CreateDataReader(DatabaseMarshaler marshaler)
        {
            return new Between_Vehicle_DataDataReader(marshaler);
        }
    }
    #endregion

    #region LoadPort_PIO_FromVehicleDataReader
    public class LoadPort_PIO_FromVehicleDataReader : DDS.OpenSplice.FooDataReader<LoadPort_PIO_FromVehicle, LoadPort_PIO_FromVehicleMarshaler>, 
                                         ILoadPort_PIO_FromVehicleDataReader
    {
        public LoadPort_PIO_FromVehicleDataReader(DatabaseMarshaler marshaler)
            : base(marshaler) { }

        public ReturnCode Read(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Read(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Read(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Read(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public override ReturnCode Read(
                ref LoadPort_PIO_FromVehicle[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.Read(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode Take(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Take(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Take(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Take(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public override ReturnCode Take(
                ref LoadPort_PIO_FromVehicle[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.Take(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadWithCondition(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return ReadWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public override ReturnCode ReadWithCondition(
                ref LoadPort_PIO_FromVehicle[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.ReadWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            return result;
        }

        public ReturnCode TakeWithCondition(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return TakeWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public override ReturnCode TakeWithCondition(
                ref LoadPort_PIO_FromVehicle[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.TakeWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            return result;
        }

        public override ReturnCode ReadNextSample(
                ref LoadPort_PIO_FromVehicle dataValue,
                ref SampleInfo sampleInfo)
        {
            ReturnCode result =
                base.ReadNextSample(
                        ref dataValue,
                        ref sampleInfo);
            return result;
        }

        public override ReturnCode TakeNextSample(
                ref LoadPort_PIO_FromVehicle dataValue,
                ref SampleInfo sampleInfo)
        {
            ReturnCode result =
                base.TakeNextSample(
                        ref dataValue,
                        ref sampleInfo);
            return result;
        }

        public ReturnCode ReadInstance(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadInstance(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode ReadInstance(
                ref LoadPort_PIO_FromVehicle[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.ReadInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode TakeInstance(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeInstance(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode TakeInstance(
                ref LoadPort_PIO_FromVehicle[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.TakeInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadNextInstance(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadNextInstance(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode ReadNextInstance(
                ref LoadPort_PIO_FromVehicle[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.ReadNextInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode TakeNextInstance(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeNextInstance(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode TakeNextInstance(
                ref LoadPort_PIO_FromVehicle[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.TakeNextInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadNextInstanceWithCondition(
                ref LoadPort_PIO_FromVehicle[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return ReadNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public override ReturnCode ReadNextInstanceWithCondition(
                ref LoadPort_PIO_FromVehicle[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.ReadNextInstanceWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);
            return result;
        }

        public ReturnCode TakeNextInstanceWithCondition(
                ref LoadPort_PIO_FromVehicle[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return TakeNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public override ReturnCode TakeNextInstanceWithCondition(
                ref LoadPort_PIO_FromVehicle[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.TakeNextInstanceWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);

            return result;
        }

        public override ReturnCode ReturnLoan(
                ref LoadPort_PIO_FromVehicle[] dataValues,
                ref SampleInfo[] sampleInfos)
        {
            ReturnCode result =
                base.ReturnLoan(
                        ref dataValues,
                        ref sampleInfos);

            return result;
        }

        public override ReturnCode GetKeyValue(
                ref LoadPort_PIO_FromVehicle key,
                InstanceHandle handle)
        {
            ReturnCode result = base.GetKeyValue(
                        ref key,
                        handle);
            return result;
        }

        public override InstanceHandle LookupInstance(
                LoadPort_PIO_FromVehicle instance)
        {
            return
                base.LookupInstance(
                        instance);
        }

    }
    #endregion
    
    #region LoadPort_PIO_FromVehicleDataWriter
    public class LoadPort_PIO_FromVehicleDataWriter : DDS.OpenSplice.FooDataWriter<LoadPort_PIO_FromVehicle, LoadPort_PIO_FromVehicleMarshaler>, 
                                         ILoadPort_PIO_FromVehicleDataWriter
    {
        public LoadPort_PIO_FromVehicleDataWriter(DatabaseMarshaler marshaler)
            : base(marshaler) { }

        public InstanceHandle RegisterInstance(
                LoadPort_PIO_FromVehicle instanceData)
        {
            return base.RegisterInstance(
                    instanceData,
                    Time.Current);
        }

        public InstanceHandle RegisterInstanceWithTimestamp(
                LoadPort_PIO_FromVehicle instanceData,
                Time sourceTimestamp)
        {
            return base.RegisterInstance(
                    instanceData,
                    sourceTimestamp);
        }

        public ReturnCode UnregisterInstance(
                LoadPort_PIO_FromVehicle instanceData,
                InstanceHandle instanceHandle)
        {
            return base.UnregisterInstance(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode UnregisterInstanceWithTimestamp(
                LoadPort_PIO_FromVehicle instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.UnregisterInstance(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode Write(LoadPort_PIO_FromVehicle instanceData)
        {
            return base.Write(
                    instanceData,
                    InstanceHandle.Nil,
                    Time.Current);
        }

        public ReturnCode Write(
                LoadPort_PIO_FromVehicle instanceData,
                InstanceHandle instanceHandle)
        {
            return base.Write(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode WriteWithTimestamp(
                LoadPort_PIO_FromVehicle instanceData,
                Time sourceTimestamp)
        {
            return base.Write(
                    instanceData,
                    InstanceHandle.Nil,
                    sourceTimestamp);
        }

        public ReturnCode WriteWithTimestamp(
                LoadPort_PIO_FromVehicle instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.Write(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode Dispose(
                LoadPort_PIO_FromVehicle instanceData,
                InstanceHandle instanceHandle)
        {
            return base.Dispose(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode DisposeWithTimestamp(
                LoadPort_PIO_FromVehicle instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.Dispose(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode WriteDispose(
                LoadPort_PIO_FromVehicle instanceData)
        {
            return base.WriteDispose(
                    instanceData,
                    InstanceHandle.Nil,
                    Time.Current);
        }

        public ReturnCode WriteDispose(
                LoadPort_PIO_FromVehicle instanceData,
                InstanceHandle instanceHandle)
        {
            return base.WriteDispose(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                LoadPort_PIO_FromVehicle instanceData,
                Time sourceTimestamp)
        {
            return base.WriteDispose(
                    instanceData,
                    InstanceHandle.Nil,
                    sourceTimestamp);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                LoadPort_PIO_FromVehicle instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.WriteDispose(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public override ReturnCode GetKeyValue(
                ref LoadPort_PIO_FromVehicle key,
                InstanceHandle instanceHandle)
        {
            return base.GetKeyValue(ref key, instanceHandle);
        }

        public override InstanceHandle LookupInstance(
            LoadPort_PIO_FromVehicle instanceData)
        {
            return base.LookupInstance(instanceData);
        }
    }
    #endregion

    #region LoadPort_PIO_FromVehicleTypeSupport
    public class LoadPort_PIO_FromVehicleTypeSupport : DDS.OpenSplice.TypeSupport
    {
        private static readonly string[] metaDescriptor = {"<MetaData version=\"1.0.0\"><Module name=\"Veh_HandShakeData\"><Enum name=\"PortOwnerType\"><Element name=\"NotDefinedYet\" value=\"0\"/>",
"<Element name=\"Equipment\" value=\"1\"/><Element name=\"Stocker\" value=\"2\"/><Element name=\"MaintainLifter\" value=\"3\"/>",
"<Element name=\"OverHeadBuffer\" value=\"4\"/><Element name=\"HidControlBox\" value=\"5\"/><Element name=\"ChargeDock\" value=\"6\"/>",
"<Element name=\"Others\" value=\"7\"/></Enum><Struct name=\"LoadPort_PIO_FromVehicle\"><Member name=\"EQ_Name\">",
"<String/></Member><Member name=\"Port_Name\"><String/></Member><Member name=\"PIO_ID\"><String/></Member>",
"<Member name=\"PIO_Channel\"><Long/></Member><Member name=\"PortType\"><Type name=\"PortOwnerType\"/>",
"</Member><Member name=\"InService\"><Boolean/></Member><Member name=\"DO01_VALID\"><Boolean/></Member>",
"<Member name=\"DO02_CS_0\"><Boolean/></Member><Member name=\"DO03_CS_1\"><Boolean/></Member><Member name=\"DO04_nil\">",
"<Boolean/></Member><Member name=\"DO05_TR_REQ\"><Boolean/></Member><Member name=\"DO06_BUSY\"><Boolean/>",
"</Member><Member name=\"DO07_COMPT\"><Boolean/></Member><Member name=\"DO08_CONT\"><Boolean/></Member>",
"<Member name=\"SELECT\"><Boolean/></Member></Struct></Module></MetaData>"};

        public LoadPort_PIO_FromVehicleTypeSupport()
            : base(typeof(LoadPort_PIO_FromVehicle), metaDescriptor, "Veh_HandShakeData::LoadPort_PIO_FromVehicle", "", "Port_Name")
        { }


        public override ReturnCode RegisterType(IDomainParticipant participant, string typeName)
        {
            return RegisterType(participant, typeName, new LoadPort_PIO_FromVehicleMarshaler());
        }

        public override DDS.OpenSplice.DataWriter CreateDataWriter(DatabaseMarshaler marshaler)
        {
            return new LoadPort_PIO_FromVehicleDataWriter(marshaler);
        }

        public override DDS.OpenSplice.DataReader CreateDataReader(DatabaseMarshaler marshaler)
        {
            return new LoadPort_PIO_FromVehicleDataReader(marshaler);
        }
    }
    #endregion

    #region LoadPort_PIO_ToVehicleDataReader
    public class LoadPort_PIO_ToVehicleDataReader : DDS.OpenSplice.FooDataReader<LoadPort_PIO_ToVehicle, LoadPort_PIO_ToVehicleMarshaler>, 
                                         ILoadPort_PIO_ToVehicleDataReader
    {
        public LoadPort_PIO_ToVehicleDataReader(DatabaseMarshaler marshaler)
            : base(marshaler) { }

        public ReturnCode Read(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Read(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Read(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Read(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public override ReturnCode Read(
                ref LoadPort_PIO_ToVehicle[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.Read(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode Take(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Take(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Take(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Take(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public override ReturnCode Take(
                ref LoadPort_PIO_ToVehicle[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.Take(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadWithCondition(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return ReadWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public override ReturnCode ReadWithCondition(
                ref LoadPort_PIO_ToVehicle[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.ReadWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            return result;
        }

        public ReturnCode TakeWithCondition(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return TakeWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public override ReturnCode TakeWithCondition(
                ref LoadPort_PIO_ToVehicle[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.TakeWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            return result;
        }

        public override ReturnCode ReadNextSample(
                ref LoadPort_PIO_ToVehicle dataValue,
                ref SampleInfo sampleInfo)
        {
            ReturnCode result =
                base.ReadNextSample(
                        ref dataValue,
                        ref sampleInfo);
            return result;
        }

        public override ReturnCode TakeNextSample(
                ref LoadPort_PIO_ToVehicle dataValue,
                ref SampleInfo sampleInfo)
        {
            ReturnCode result =
                base.TakeNextSample(
                        ref dataValue,
                        ref sampleInfo);
            return result;
        }

        public ReturnCode ReadInstance(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadInstance(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode ReadInstance(
                ref LoadPort_PIO_ToVehicle[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.ReadInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode TakeInstance(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeInstance(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode TakeInstance(
                ref LoadPort_PIO_ToVehicle[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.TakeInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadNextInstance(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadNextInstance(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode ReadNextInstance(
                ref LoadPort_PIO_ToVehicle[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.ReadNextInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode TakeNextInstance(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeNextInstance(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode TakeNextInstance(
                ref LoadPort_PIO_ToVehicle[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.TakeNextInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadNextInstanceWithCondition(
                ref LoadPort_PIO_ToVehicle[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return ReadNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public override ReturnCode ReadNextInstanceWithCondition(
                ref LoadPort_PIO_ToVehicle[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.ReadNextInstanceWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);
            return result;
        }

        public ReturnCode TakeNextInstanceWithCondition(
                ref LoadPort_PIO_ToVehicle[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return TakeNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public override ReturnCode TakeNextInstanceWithCondition(
                ref LoadPort_PIO_ToVehicle[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.TakeNextInstanceWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);

            return result;
        }

        public override ReturnCode ReturnLoan(
                ref LoadPort_PIO_ToVehicle[] dataValues,
                ref SampleInfo[] sampleInfos)
        {
            ReturnCode result =
                base.ReturnLoan(
                        ref dataValues,
                        ref sampleInfos);

            return result;
        }

        public override ReturnCode GetKeyValue(
                ref LoadPort_PIO_ToVehicle key,
                InstanceHandle handle)
        {
            ReturnCode result = base.GetKeyValue(
                        ref key,
                        handle);
            return result;
        }

        public override InstanceHandle LookupInstance(
                LoadPort_PIO_ToVehicle instance)
        {
            return
                base.LookupInstance(
                        instance);
        }

    }
    #endregion
    
    #region LoadPort_PIO_ToVehicleDataWriter
    public class LoadPort_PIO_ToVehicleDataWriter : DDS.OpenSplice.FooDataWriter<LoadPort_PIO_ToVehicle, LoadPort_PIO_ToVehicleMarshaler>, 
                                         ILoadPort_PIO_ToVehicleDataWriter
    {
        public LoadPort_PIO_ToVehicleDataWriter(DatabaseMarshaler marshaler)
            : base(marshaler) { }

        public InstanceHandle RegisterInstance(
                LoadPort_PIO_ToVehicle instanceData)
        {
            return base.RegisterInstance(
                    instanceData,
                    Time.Current);
        }

        public InstanceHandle RegisterInstanceWithTimestamp(
                LoadPort_PIO_ToVehicle instanceData,
                Time sourceTimestamp)
        {
            return base.RegisterInstance(
                    instanceData,
                    sourceTimestamp);
        }

        public ReturnCode UnregisterInstance(
                LoadPort_PIO_ToVehicle instanceData,
                InstanceHandle instanceHandle)
        {
            return base.UnregisterInstance(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode UnregisterInstanceWithTimestamp(
                LoadPort_PIO_ToVehicle instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.UnregisterInstance(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode Write(LoadPort_PIO_ToVehicle instanceData)
        {
            return base.Write(
                    instanceData,
                    InstanceHandle.Nil,
                    Time.Current);
        }

        public ReturnCode Write(
                LoadPort_PIO_ToVehicle instanceData,
                InstanceHandle instanceHandle)
        {
            return base.Write(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode WriteWithTimestamp(
                LoadPort_PIO_ToVehicle instanceData,
                Time sourceTimestamp)
        {
            return base.Write(
                    instanceData,
                    InstanceHandle.Nil,
                    sourceTimestamp);
        }

        public ReturnCode WriteWithTimestamp(
                LoadPort_PIO_ToVehicle instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.Write(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode Dispose(
                LoadPort_PIO_ToVehicle instanceData,
                InstanceHandle instanceHandle)
        {
            return base.Dispose(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode DisposeWithTimestamp(
                LoadPort_PIO_ToVehicle instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.Dispose(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode WriteDispose(
                LoadPort_PIO_ToVehicle instanceData)
        {
            return base.WriteDispose(
                    instanceData,
                    InstanceHandle.Nil,
                    Time.Current);
        }

        public ReturnCode WriteDispose(
                LoadPort_PIO_ToVehicle instanceData,
                InstanceHandle instanceHandle)
        {
            return base.WriteDispose(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                LoadPort_PIO_ToVehicle instanceData,
                Time sourceTimestamp)
        {
            return base.WriteDispose(
                    instanceData,
                    InstanceHandle.Nil,
                    sourceTimestamp);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                LoadPort_PIO_ToVehicle instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.WriteDispose(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public override ReturnCode GetKeyValue(
                ref LoadPort_PIO_ToVehicle key,
                InstanceHandle instanceHandle)
        {
            return base.GetKeyValue(ref key, instanceHandle);
        }

        public override InstanceHandle LookupInstance(
            LoadPort_PIO_ToVehicle instanceData)
        {
            return base.LookupInstance(instanceData);
        }
    }
    #endregion

    #region LoadPort_PIO_ToVehicleTypeSupport
    public class LoadPort_PIO_ToVehicleTypeSupport : DDS.OpenSplice.TypeSupport
    {
        private static readonly string[] metaDescriptor = {"<MetaData version=\"1.0.0\"><Module name=\"Veh_HandShakeData\"><Enum name=\"PortOwnerType\"><Element name=\"NotDefinedYet\" value=\"0\"/>",
"<Element name=\"Equipment\" value=\"1\"/><Element name=\"Stocker\" value=\"2\"/><Element name=\"MaintainLifter\" value=\"3\"/>",
"<Element name=\"OverHeadBuffer\" value=\"4\"/><Element name=\"HidControlBox\" value=\"5\"/><Element name=\"ChargeDock\" value=\"6\"/>",
"<Element name=\"Others\" value=\"7\"/></Enum><Struct name=\"LoadPort_PIO_ToVehicle\"><Member name=\"EQ_Name\">",
"<String/></Member><Member name=\"Port_Name\"><String/></Member><Member name=\"PIO_ID\"><String/></Member>",
"<Member name=\"PIO_Channel\"><Long/></Member><Member name=\"PortType\"><Type name=\"PortOwnerType\"/>",
"</Member><Member name=\"InService\"><Boolean/></Member><Member name=\"DI01_L_REQ\"><Boolean/></Member>",
"<Member name=\"DI02_U_REQ\"><Boolean/></Member><Member name=\"DI03_nil\"><Boolean/></Member><Member name=\"DI04_READY\">",
"<Boolean/></Member><Member name=\"DI05_nil\"><Boolean/></Member><Member name=\"DI06_nil\"><Boolean/></Member>",
"<Member name=\"DI07_HO_AVBL\"><Boolean/></Member><Member name=\"DI08_ES\"><Boolean/></Member><Member name=\"READY_GO\">",
"<Boolean/></Member><Member name=\"Tray_Detection\"><Boolean/></Member></Struct></Module></MetaData>"};

        public LoadPort_PIO_ToVehicleTypeSupport()
            : base(typeof(LoadPort_PIO_ToVehicle), metaDescriptor, "Veh_HandShakeData::LoadPort_PIO_ToVehicle", "", "Port_Name")
        { }


        public override ReturnCode RegisterType(IDomainParticipant participant, string typeName)
        {
            return RegisterType(participant, typeName, new LoadPort_PIO_ToVehicleMarshaler());
        }

        public override DDS.OpenSplice.DataWriter CreateDataWriter(DatabaseMarshaler marshaler)
        {
            return new LoadPort_PIO_ToVehicleDataWriter(marshaler);
        }

        public override DDS.OpenSplice.DataReader CreateDataReader(DatabaseMarshaler marshaler)
        {
            return new LoadPort_PIO_ToVehicleDataReader(marshaler);
        }
    }
    #endregion

    #region EQ_Stages_Interface_IODataReader
    public class EQ_Stages_Interface_IODataReader : DDS.OpenSplice.FooDataReader<EQ_Stages_Interface_IO, EQ_Stages_Interface_IOMarshaler>, 
                                         IEQ_Stages_Interface_IODataReader
    {
        public EQ_Stages_Interface_IODataReader(DatabaseMarshaler marshaler)
            : base(marshaler) { }

        public ReturnCode Read(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Read(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Read(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Read(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public override ReturnCode Read(
                ref EQ_Stages_Interface_IO[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.Read(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode Take(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Take(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Take(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Take(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public override ReturnCode Take(
                ref EQ_Stages_Interface_IO[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.Take(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadWithCondition(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return ReadWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public override ReturnCode ReadWithCondition(
                ref EQ_Stages_Interface_IO[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.ReadWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            return result;
        }

        public ReturnCode TakeWithCondition(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return TakeWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public override ReturnCode TakeWithCondition(
                ref EQ_Stages_Interface_IO[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.TakeWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            return result;
        }

        public override ReturnCode ReadNextSample(
                ref EQ_Stages_Interface_IO dataValue,
                ref SampleInfo sampleInfo)
        {
            ReturnCode result =
                base.ReadNextSample(
                        ref dataValue,
                        ref sampleInfo);
            return result;
        }

        public override ReturnCode TakeNextSample(
                ref EQ_Stages_Interface_IO dataValue,
                ref SampleInfo sampleInfo)
        {
            ReturnCode result =
                base.TakeNextSample(
                        ref dataValue,
                        ref sampleInfo);
            return result;
        }

        public ReturnCode ReadInstance(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadInstance(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode ReadInstance(
                ref EQ_Stages_Interface_IO[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.ReadInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode TakeInstance(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeInstance(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode TakeInstance(
                ref EQ_Stages_Interface_IO[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.TakeInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadNextInstance(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadNextInstance(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode ReadNextInstance(
                ref EQ_Stages_Interface_IO[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.ReadNextInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode TakeNextInstance(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeNextInstance(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode TakeNextInstance(
                ref EQ_Stages_Interface_IO[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.TakeNextInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadNextInstanceWithCondition(
                ref EQ_Stages_Interface_IO[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return ReadNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public override ReturnCode ReadNextInstanceWithCondition(
                ref EQ_Stages_Interface_IO[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.ReadNextInstanceWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);
            return result;
        }

        public ReturnCode TakeNextInstanceWithCondition(
                ref EQ_Stages_Interface_IO[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return TakeNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public override ReturnCode TakeNextInstanceWithCondition(
                ref EQ_Stages_Interface_IO[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.TakeNextInstanceWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);

            return result;
        }

        public override ReturnCode ReturnLoan(
                ref EQ_Stages_Interface_IO[] dataValues,
                ref SampleInfo[] sampleInfos)
        {
            ReturnCode result =
                base.ReturnLoan(
                        ref dataValues,
                        ref sampleInfos);

            return result;
        }

        public override ReturnCode GetKeyValue(
                ref EQ_Stages_Interface_IO key,
                InstanceHandle handle)
        {
            ReturnCode result = base.GetKeyValue(
                        ref key,
                        handle);
            return result;
        }

        public override InstanceHandle LookupInstance(
                EQ_Stages_Interface_IO instance)
        {
            return
                base.LookupInstance(
                        instance);
        }

    }
    #endregion
    
    #region EQ_Stages_Interface_IODataWriter
    public class EQ_Stages_Interface_IODataWriter : DDS.OpenSplice.FooDataWriter<EQ_Stages_Interface_IO, EQ_Stages_Interface_IOMarshaler>, 
                                         IEQ_Stages_Interface_IODataWriter
    {
        public EQ_Stages_Interface_IODataWriter(DatabaseMarshaler marshaler)
            : base(marshaler) { }

        public InstanceHandle RegisterInstance(
                EQ_Stages_Interface_IO instanceData)
        {
            return base.RegisterInstance(
                    instanceData,
                    Time.Current);
        }

        public InstanceHandle RegisterInstanceWithTimestamp(
                EQ_Stages_Interface_IO instanceData,
                Time sourceTimestamp)
        {
            return base.RegisterInstance(
                    instanceData,
                    sourceTimestamp);
        }

        public ReturnCode UnregisterInstance(
                EQ_Stages_Interface_IO instanceData,
                InstanceHandle instanceHandle)
        {
            return base.UnregisterInstance(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode UnregisterInstanceWithTimestamp(
                EQ_Stages_Interface_IO instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.UnregisterInstance(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode Write(EQ_Stages_Interface_IO instanceData)
        {
            return base.Write(
                    instanceData,
                    InstanceHandle.Nil,
                    Time.Current);
        }

        public ReturnCode Write(
                EQ_Stages_Interface_IO instanceData,
                InstanceHandle instanceHandle)
        {
            return base.Write(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode WriteWithTimestamp(
                EQ_Stages_Interface_IO instanceData,
                Time sourceTimestamp)
        {
            return base.Write(
                    instanceData,
                    InstanceHandle.Nil,
                    sourceTimestamp);
        }

        public ReturnCode WriteWithTimestamp(
                EQ_Stages_Interface_IO instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.Write(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode Dispose(
                EQ_Stages_Interface_IO instanceData,
                InstanceHandle instanceHandle)
        {
            return base.Dispose(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode DisposeWithTimestamp(
                EQ_Stages_Interface_IO instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.Dispose(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode WriteDispose(
                EQ_Stages_Interface_IO instanceData)
        {
            return base.WriteDispose(
                    instanceData,
                    InstanceHandle.Nil,
                    Time.Current);
        }

        public ReturnCode WriteDispose(
                EQ_Stages_Interface_IO instanceData,
                InstanceHandle instanceHandle)
        {
            return base.WriteDispose(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                EQ_Stages_Interface_IO instanceData,
                Time sourceTimestamp)
        {
            return base.WriteDispose(
                    instanceData,
                    InstanceHandle.Nil,
                    sourceTimestamp);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                EQ_Stages_Interface_IO instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.WriteDispose(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public override ReturnCode GetKeyValue(
                ref EQ_Stages_Interface_IO key,
                InstanceHandle instanceHandle)
        {
            return base.GetKeyValue(ref key, instanceHandle);
        }

        public override InstanceHandle LookupInstance(
            EQ_Stages_Interface_IO instanceData)
        {
            return base.LookupInstance(instanceData);
        }
    }
    #endregion

    #region EQ_Stages_Interface_IOTypeSupport
    public class EQ_Stages_Interface_IOTypeSupport : DDS.OpenSplice.TypeSupport
    {
        private static readonly string[] metaDescriptor = {"<MetaData version=\"1.0.0\"><Module name=\"Veh_HandShakeData\"><Enum name=\"PortOwnerType\"><Element name=\"NotDefinedYet\" value=\"0\"/>",
"<Element name=\"Equipment\" value=\"1\"/><Element name=\"Stocker\" value=\"2\"/><Element name=\"MaintainLifter\" value=\"3\"/>",
"<Element name=\"OverHeadBuffer\" value=\"4\"/><Element name=\"HidControlBox\" value=\"5\"/><Element name=\"ChargeDock\" value=\"6\"/>",
"<Element name=\"Others\" value=\"7\"/></Enum><Struct name=\"EQ_Stages_Interface_IO\"><Member name=\"EQ_Name\">",
"<String/></Member><Member name=\"PortType\"><Type name=\"PortOwnerType\"/></Member><Member name=\"InService\">",
"<Boolean/></Member><Member name=\"EQ_Online\"><Boolean/></Member><Member name=\"EQ_Error\"><Boolean/>",
"</Member><Member name=\"ST01_Port_Name\"><String/></Member><Member name=\"ST01_Stage_ID\"><Long/></Member>",
"<Member name=\"ST01_Ready\"><Boolean/></Member><Member name=\"ST01_Loaded\"><Boolean/></Member><Member name=\"ST02_Port_Name\">",
"<String/></Member><Member name=\"ST02_Stage_ID\"><Long/></Member><Member name=\"ST02_Ready\"><Boolean/>",
"</Member><Member name=\"ST02_Loaded\"><Boolean/></Member><Member name=\"ST03_Port_Name\"><String/></Member>",
"<Member name=\"ST03_Stage_ID\"><Long/></Member><Member name=\"ST03_Ready\"><Boolean/></Member><Member name=\"ST03_Loaded\">",
"<Boolean/></Member><Member name=\"ST04_Port_Name\"><String/></Member><Member name=\"ST04_Stage_ID\"><Long/>",
"</Member><Member name=\"ST04_Ready\"><Boolean/></Member><Member name=\"ST04_Loaded\"><Boolean/></Member>",
"<Member name=\"ST05_Port_Name\"><String/></Member><Member name=\"ST05_Stage_ID\"><Long/></Member><Member name=\"ST05_Ready\">",
"<Boolean/></Member><Member name=\"ST05_Loaded\"><Boolean/></Member><Member name=\"ST06_Port_Name\"><String/>",
"</Member><Member name=\"ST06_Stage_ID\"><Long/></Member><Member name=\"ST06_Ready\"><Boolean/></Member>",
"<Member name=\"ST06_Loaded\"><Boolean/></Member><Member name=\"ST07_Port_Name\"><String/></Member><Member name=\"ST07_Stage_ID\">",
"<Long/></Member><Member name=\"ST07_Ready\"><Boolean/></Member><Member name=\"ST07_Loaded\"><Boolean/>",
"</Member><Member name=\"ST08_Port_Name\"><String/></Member><Member name=\"ST08_Stage_ID\"><Long/></Member>",
"<Member name=\"ST08_Ready\"><Boolean/></Member><Member name=\"ST08_Loaded\"><Boolean/></Member><Member name=\"ST09_Port_Name\">",
"<String/></Member><Member name=\"ST09_Stage_ID\"><Long/></Member><Member name=\"ST09_Ready\"><Boolean/>",
"</Member><Member name=\"ST09_Loaded\"><Boolean/></Member><Member name=\"ST10_Port_Name\"><String/></Member>",
"<Member name=\"ST10_Stage_ID\"><Long/></Member><Member name=\"ST10_Ready\"><Boolean/></Member><Member name=\"ST10_Loaded\">",
"<Boolean/></Member><Member name=\"ST11_Port_Name\"><String/></Member><Member name=\"ST11_Stage_ID\"><Long/>",
"</Member><Member name=\"ST11_Ready\"><Boolean/></Member><Member name=\"ST11_Loaded\"><Boolean/></Member>",
"<Member name=\"ST12_Port_Name\"><String/></Member><Member name=\"ST12_Stage_ID\"><Long/></Member><Member name=\"ST12_Ready\">",
"<Boolean/></Member><Member name=\"ST12_Loaded\"><Boolean/></Member><Member name=\"ST13_Port_Name\"><String/>",
"</Member><Member name=\"ST13_Stage_ID\"><Long/></Member><Member name=\"ST13_Ready\"><Boolean/></Member>",
"<Member name=\"ST13_Loaded\"><Boolean/></Member><Member name=\"ST14_Port_Name\"><String/></Member><Member name=\"ST14_Stage_ID\">",
"<Long/></Member><Member name=\"ST14_Ready\"><Boolean/></Member><Member name=\"ST14_Loaded\"><Boolean/>",
"</Member><Member name=\"ST15_Port_Name\"><String/></Member><Member name=\"ST15_Stage_ID\"><Long/></Member>",
"<Member name=\"ST15_Ready\"><Boolean/></Member><Member name=\"ST15_Loaded\"><Boolean/></Member><Member name=\"ST16_Port_Name\">",
"<String/></Member><Member name=\"ST16_Stage_ID\"><Long/></Member><Member name=\"ST16_Ready\"><Boolean/>",
"</Member><Member name=\"ST16_Loaded\"><Boolean/></Member></Struct></Module></MetaData>"};

        public EQ_Stages_Interface_IOTypeSupport()
            : base(typeof(EQ_Stages_Interface_IO), metaDescriptor, "Veh_HandShakeData::EQ_Stages_Interface_IO", "", "EQ_Name")
        { }


        public override ReturnCode RegisterType(IDomainParticipant participant, string typeName)
        {
            return RegisterType(participant, typeName, new EQ_Stages_Interface_IOMarshaler());
        }

        public override DDS.OpenSplice.DataWriter CreateDataWriter(DatabaseMarshaler marshaler)
        {
            return new EQ_Stages_Interface_IODataWriter(marshaler);
        }

        public override DDS.OpenSplice.DataReader CreateDataReader(DatabaseMarshaler marshaler)
        {
            return new EQ_Stages_Interface_IODataReader(marshaler);
        }
    }
    #endregion

    #region InterVehicles_BlockZones_ControlDataReader
    public class InterVehicles_BlockZones_ControlDataReader : DDS.OpenSplice.FooDataReader<InterVehicles_BlockZones_Control, InterVehicles_BlockZones_ControlMarshaler>, 
                                         IInterVehicles_BlockZones_ControlDataReader
    {
        public InterVehicles_BlockZones_ControlDataReader(DatabaseMarshaler marshaler)
            : base(marshaler) { }

        public ReturnCode Read(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Read(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Read(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Read(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Read(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public override ReturnCode Read(
                ref InterVehicles_BlockZones_Control[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.Read(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode Take(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited);
        }

        public ReturnCode Take(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples)
        {
            return Take(ref dataValues, ref sampleInfos, maxSamples, SampleStateKind.Any,
                ViewStateKind.Any, InstanceStateKind.Any);
        }

        public ReturnCode Take(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates)
        {
            return Take(ref dataValues, ref sampleInfos, Length.Unlimited, sampleStates,
                viewStates, instanceStates);
        }

        public override ReturnCode Take(
                ref InterVehicles_BlockZones_Control[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.Take(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadWithCondition(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return ReadWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public override ReturnCode ReadWithCondition(
                ref InterVehicles_BlockZones_Control[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.ReadWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            return result;
        }

        public ReturnCode TakeWithCondition(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition)
        {
            return TakeWithCondition(ref dataValues, ref sampleInfos,
                Length.Unlimited, readCondition);
        }

        public override ReturnCode TakeWithCondition(
                ref InterVehicles_BlockZones_Control[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.TakeWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        readCondition);
            return result;
        }

        public override ReturnCode ReadNextSample(
                ref InterVehicles_BlockZones_Control dataValue,
                ref SampleInfo sampleInfo)
        {
            ReturnCode result =
                base.ReadNextSample(
                        ref dataValue,
                        ref sampleInfo);
            return result;
        }

        public override ReturnCode TakeNextSample(
                ref InterVehicles_BlockZones_Control dataValue,
                ref SampleInfo sampleInfo)
        {
            ReturnCode result =
                base.TakeNextSample(
                        ref dataValue,
                        ref sampleInfo);
            return result;
        }

        public ReturnCode ReadInstance(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadInstance(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode ReadInstance(
                ref InterVehicles_BlockZones_Control[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.ReadInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode TakeInstance(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeInstance(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode TakeInstance(
                ref InterVehicles_BlockZones_Control[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.TakeInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadNextInstance(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode ReadNextInstance(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return ReadNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode ReadNextInstance(
                ref InterVehicles_BlockZones_Control[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.ReadNextInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode TakeNextInstance(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, Length.Unlimited, instanceHandle);
        }

        public ReturnCode TakeNextInstance(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle)
        {
            return TakeNextInstance(ref dataValues, ref sampleInfos, maxSamples, instanceHandle,
                SampleStateKind.Any, ViewStateKind.Any, InstanceStateKind.Any);
        }

        public override ReturnCode TakeNextInstance(
                ref InterVehicles_BlockZones_Control[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                SampleStateKind sampleStates,
                ViewStateKind viewStates,
                InstanceStateKind instanceStates)
        {
            ReturnCode result =
                base.TakeNextInstance(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        sampleStates,
                        viewStates,
                        instanceStates);
            return result;
        }

        public ReturnCode ReadNextInstanceWithCondition(
                ref InterVehicles_BlockZones_Control[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return ReadNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public override ReturnCode ReadNextInstanceWithCondition(
                ref InterVehicles_BlockZones_Control[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.ReadNextInstanceWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);
            return result;
        }

        public ReturnCode TakeNextInstanceWithCondition(
                ref InterVehicles_BlockZones_Control[] dataValues,
                ref SampleInfo[] sampleInfos,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            return TakeNextInstanceWithCondition(
                ref dataValues,
                ref sampleInfos,
                Length.Unlimited,
                instanceHandle,
                readCondition);
        }

        public override ReturnCode TakeNextInstanceWithCondition(
                ref InterVehicles_BlockZones_Control[] dataValues,
                ref SampleInfo[] sampleInfos,
                int maxSamples,
                InstanceHandle instanceHandle,
                IReadCondition readCondition)
        {
            ReturnCode result =
                base.TakeNextInstanceWithCondition(
                        ref dataValues,
                        ref sampleInfos,
                        maxSamples,
                        instanceHandle,
                        readCondition);

            return result;
        }

        public override ReturnCode ReturnLoan(
                ref InterVehicles_BlockZones_Control[] dataValues,
                ref SampleInfo[] sampleInfos)
        {
            ReturnCode result =
                base.ReturnLoan(
                        ref dataValues,
                        ref sampleInfos);

            return result;
        }

        public override ReturnCode GetKeyValue(
                ref InterVehicles_BlockZones_Control key,
                InstanceHandle handle)
        {
            ReturnCode result = base.GetKeyValue(
                        ref key,
                        handle);
            return result;
        }

        public override InstanceHandle LookupInstance(
                InterVehicles_BlockZones_Control instance)
        {
            return
                base.LookupInstance(
                        instance);
        }

    }
    #endregion
    
    #region InterVehicles_BlockZones_ControlDataWriter
    public class InterVehicles_BlockZones_ControlDataWriter : DDS.OpenSplice.FooDataWriter<InterVehicles_BlockZones_Control, InterVehicles_BlockZones_ControlMarshaler>, 
                                         IInterVehicles_BlockZones_ControlDataWriter
    {
        public InterVehicles_BlockZones_ControlDataWriter(DatabaseMarshaler marshaler)
            : base(marshaler) { }

        public InstanceHandle RegisterInstance(
                InterVehicles_BlockZones_Control instanceData)
        {
            return base.RegisterInstance(
                    instanceData,
                    Time.Current);
        }

        public InstanceHandle RegisterInstanceWithTimestamp(
                InterVehicles_BlockZones_Control instanceData,
                Time sourceTimestamp)
        {
            return base.RegisterInstance(
                    instanceData,
                    sourceTimestamp);
        }

        public ReturnCode UnregisterInstance(
                InterVehicles_BlockZones_Control instanceData,
                InstanceHandle instanceHandle)
        {
            return base.UnregisterInstance(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode UnregisterInstanceWithTimestamp(
                InterVehicles_BlockZones_Control instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.UnregisterInstance(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode Write(InterVehicles_BlockZones_Control instanceData)
        {
            return base.Write(
                    instanceData,
                    InstanceHandle.Nil,
                    Time.Current);
        }

        public ReturnCode Write(
                InterVehicles_BlockZones_Control instanceData,
                InstanceHandle instanceHandle)
        {
            return base.Write(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode WriteWithTimestamp(
                InterVehicles_BlockZones_Control instanceData,
                Time sourceTimestamp)
        {
            return base.Write(
                    instanceData,
                    InstanceHandle.Nil,
                    sourceTimestamp);
        }

        public ReturnCode WriteWithTimestamp(
                InterVehicles_BlockZones_Control instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.Write(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode Dispose(
                InterVehicles_BlockZones_Control instanceData,
                InstanceHandle instanceHandle)
        {
            return base.Dispose(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode DisposeWithTimestamp(
                InterVehicles_BlockZones_Control instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.Dispose(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public ReturnCode WriteDispose(
                InterVehicles_BlockZones_Control instanceData)
        {
            return base.WriteDispose(
                    instanceData,
                    InstanceHandle.Nil,
                    Time.Current);
        }

        public ReturnCode WriteDispose(
                InterVehicles_BlockZones_Control instanceData,
                InstanceHandle instanceHandle)
        {
            return base.WriteDispose(
                    instanceData,
                    instanceHandle,
                    Time.Current);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                InterVehicles_BlockZones_Control instanceData,
                Time sourceTimestamp)
        {
            return base.WriteDispose(
                    instanceData,
                    InstanceHandle.Nil,
                    sourceTimestamp);
        }

        public ReturnCode WriteDisposeWithTimestamp(
                InterVehicles_BlockZones_Control instanceData,
                InstanceHandle instanceHandle,
                Time sourceTimestamp)
        {
            return base.WriteDispose(
                    instanceData,
                    instanceHandle,
                    sourceTimestamp);
        }

        public override ReturnCode GetKeyValue(
                ref InterVehicles_BlockZones_Control key,
                InstanceHandle instanceHandle)
        {
            return base.GetKeyValue(ref key, instanceHandle);
        }

        public override InstanceHandle LookupInstance(
            InterVehicles_BlockZones_Control instanceData)
        {
            return base.LookupInstance(instanceData);
        }
    }
    #endregion

    #region InterVehicles_BlockZones_ControlTypeSupport
    public class InterVehicles_BlockZones_ControlTypeSupport : DDS.OpenSplice.TypeSupport
    {
        private static readonly string[] metaDescriptor = {"<MetaData version=\"1.0.0\"><Module name=\"Veh_HandShakeData\"><Struct name=\"InterVehicles_BlockZones_Control\">",
"<Member name=\"BlockZoneID\"><String/></Member><Member name=\"HasLockedBy_VehID\"><String/></Member><Member name=\"RequestUnlockingBy_VehID\">",
"<String/></Member><Member name=\"HasUnlockedBy_VehID\"><String/></Member><Member name=\"HasAcquiredBy_VehID\">",
"<String/></Member><Member name=\"Locking_Timestamp\"><String/></Member><Member name=\"RequestUnlocking_Timestamp\">",
"<String/></Member><Member name=\"Unlocking_Timestamp\"><String/></Member><Member name=\"Acquiring_Timestamp\">",
"<String/></Member><Member name=\"ServerHeartbeating_Timestamp\"><String/></Member><Member name=\"SeverInstanceID\">",
"<String/></Member><Member name=\"InService\"><Boolean/></Member></Struct></Module></MetaData>"};

        public InterVehicles_BlockZones_ControlTypeSupport()
            : base(typeof(InterVehicles_BlockZones_Control), metaDescriptor, "Veh_HandShakeData::InterVehicles_BlockZones_Control", "", "BlockZoneID")
        { }


        public override ReturnCode RegisterType(IDomainParticipant participant, string typeName)
        {
            return RegisterType(participant, typeName, new InterVehicles_BlockZones_ControlMarshaler());
        }

        public override DDS.OpenSplice.DataWriter CreateDataWriter(DatabaseMarshaler marshaler)
        {
            return new InterVehicles_BlockZones_ControlDataWriter(marshaler);
        }

        public override DDS.OpenSplice.DataReader CreateDataReader(DatabaseMarshaler marshaler)
        {
            return new InterVehicles_BlockZones_ControlDataReader(marshaler);
        }
    }
    #endregion

}

