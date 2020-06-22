using DDS;
using DDS.OpenSplice;

namespace Veh_HandShakeData
{
    #region IMotionInfo_ClientDataReader
    public interface IMotionInfo_ClientDataReader : DDS.IDataReader
    {
        ReturnCode Read(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Read(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Read(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Read(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Take(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Take(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadWithCondition(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode ReadWithCondition(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode ReadNextSample(
            ref MotionInfo_Client dataValue,
            ref SampleInfo sampleInfo);

        ReturnCode TakeNextSample(
            ref MotionInfo_Client dataValue,
            ref SampleInfo sampleInfo);

        ReturnCode ReadInstance(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeInstance(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstance(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeNextInstance(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstanceWithCondition(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReadNextInstanceWithCondition(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReturnLoan(
            ref MotionInfo_Client[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode GetKeyValue(
            ref MotionInfo_Client key,
            InstanceHandle handle);

        InstanceHandle LookupInstance(
            MotionInfo_Client instance);
    }
    #endregion

    #region IMotionInfo_ClientDataWriter
    public interface IMotionInfo_ClientDataWriter : DDS.IDataWriter
    {
        InstanceHandle RegisterInstance(
            MotionInfo_Client instanceData);

        InstanceHandle RegisterInstanceWithTimestamp(
            MotionInfo_Client instanceData,
            Time sourceTimestamp);

        ReturnCode UnregisterInstance(
            MotionInfo_Client instanceData,
            InstanceHandle instanceHandle);

        ReturnCode UnregisterInstanceWithTimestamp(
            MotionInfo_Client instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Write(
            MotionInfo_Client instanceData);

        ReturnCode Write(
            MotionInfo_Client instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteWithTimestamp(
            MotionInfo_Client instanceData,
            Time sourceTimestamp);

        ReturnCode WriteWithTimestamp(
            MotionInfo_Client instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Dispose(
            MotionInfo_Client instanceData,
            InstanceHandle instanceHandle);

        ReturnCode DisposeWithTimestamp(
            MotionInfo_Client instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode WriteDispose(
            MotionInfo_Client instanceData);

        ReturnCode WriteDispose(
            MotionInfo_Client instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteDisposeWithTimestamp(
            MotionInfo_Client instanceData,
            Time sourceTimestamp);

        ReturnCode WriteDisposeWithTimestamp(
            MotionInfo_Client instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode GetKeyValue(
            ref MotionInfo_Client key,
            InstanceHandle instanceHandle);

        InstanceHandle LookupInstance(
            MotionInfo_Client instanceData);
    }
    #endregion

    #region IMotionInfo_ServerDataReader
    public interface IMotionInfo_ServerDataReader : DDS.IDataReader
    {
        ReturnCode Read(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Read(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Read(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Read(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Take(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Take(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadWithCondition(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode ReadWithCondition(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode ReadNextSample(
            ref MotionInfo_Server dataValue,
            ref SampleInfo sampleInfo);

        ReturnCode TakeNextSample(
            ref MotionInfo_Server dataValue,
            ref SampleInfo sampleInfo);

        ReturnCode ReadInstance(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeInstance(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstance(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeNextInstance(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstanceWithCondition(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReadNextInstanceWithCondition(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReturnLoan(
            ref MotionInfo_Server[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode GetKeyValue(
            ref MotionInfo_Server key,
            InstanceHandle handle);

        InstanceHandle LookupInstance(
            MotionInfo_Server instance);
    }
    #endregion

    #region IMotionInfo_ServerDataWriter
    public interface IMotionInfo_ServerDataWriter : DDS.IDataWriter
    {
        InstanceHandle RegisterInstance(
            MotionInfo_Server instanceData);

        InstanceHandle RegisterInstanceWithTimestamp(
            MotionInfo_Server instanceData,
            Time sourceTimestamp);

        ReturnCode UnregisterInstance(
            MotionInfo_Server instanceData,
            InstanceHandle instanceHandle);

        ReturnCode UnregisterInstanceWithTimestamp(
            MotionInfo_Server instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Write(
            MotionInfo_Server instanceData);

        ReturnCode Write(
            MotionInfo_Server instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteWithTimestamp(
            MotionInfo_Server instanceData,
            Time sourceTimestamp);

        ReturnCode WriteWithTimestamp(
            MotionInfo_Server instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Dispose(
            MotionInfo_Server instanceData,
            InstanceHandle instanceHandle);

        ReturnCode DisposeWithTimestamp(
            MotionInfo_Server instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode WriteDispose(
            MotionInfo_Server instanceData);

        ReturnCode WriteDispose(
            MotionInfo_Server instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteDisposeWithTimestamp(
            MotionInfo_Server instanceData,
            Time sourceTimestamp);

        ReturnCode WriteDisposeWithTimestamp(
            MotionInfo_Server instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode GetKeyValue(
            ref MotionInfo_Server key,
            InstanceHandle instanceHandle);

        InstanceHandle LookupInstance(
            MotionInfo_Server instanceData);
    }
    #endregion

    #region IMotionInfo_Vehicle_CommDataReader
    public interface IMotionInfo_Vehicle_CommDataReader : DDS.IDataReader
    {
        ReturnCode Read(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Read(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Read(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Read(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Take(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Take(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadWithCondition(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode ReadWithCondition(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode ReadNextSample(
            ref MotionInfo_Vehicle_Comm dataValue,
            ref SampleInfo sampleInfo);

        ReturnCode TakeNextSample(
            ref MotionInfo_Vehicle_Comm dataValue,
            ref SampleInfo sampleInfo);

        ReturnCode ReadInstance(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeInstance(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstance(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeNextInstance(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstanceWithCondition(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReadNextInstanceWithCondition(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReturnLoan(
            ref MotionInfo_Vehicle_Comm[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode GetKeyValue(
            ref MotionInfo_Vehicle_Comm key,
            InstanceHandle handle);

        InstanceHandle LookupInstance(
            MotionInfo_Vehicle_Comm instance);
    }
    #endregion

    #region IMotionInfo_Vehicle_CommDataWriter
    public interface IMotionInfo_Vehicle_CommDataWriter : DDS.IDataWriter
    {
        InstanceHandle RegisterInstance(
            MotionInfo_Vehicle_Comm instanceData);

        InstanceHandle RegisterInstanceWithTimestamp(
            MotionInfo_Vehicle_Comm instanceData,
            Time sourceTimestamp);

        ReturnCode UnregisterInstance(
            MotionInfo_Vehicle_Comm instanceData,
            InstanceHandle instanceHandle);

        ReturnCode UnregisterInstanceWithTimestamp(
            MotionInfo_Vehicle_Comm instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Write(
            MotionInfo_Vehicle_Comm instanceData);

        ReturnCode Write(
            MotionInfo_Vehicle_Comm instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteWithTimestamp(
            MotionInfo_Vehicle_Comm instanceData,
            Time sourceTimestamp);

        ReturnCode WriteWithTimestamp(
            MotionInfo_Vehicle_Comm instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Dispose(
            MotionInfo_Vehicle_Comm instanceData,
            InstanceHandle instanceHandle);

        ReturnCode DisposeWithTimestamp(
            MotionInfo_Vehicle_Comm instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode WriteDispose(
            MotionInfo_Vehicle_Comm instanceData);

        ReturnCode WriteDispose(
            MotionInfo_Vehicle_Comm instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteDisposeWithTimestamp(
            MotionInfo_Vehicle_Comm instanceData,
            Time sourceTimestamp);

        ReturnCode WriteDisposeWithTimestamp(
            MotionInfo_Vehicle_Comm instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode GetKeyValue(
            ref MotionInfo_Vehicle_Comm key,
            InstanceHandle instanceHandle);

        InstanceHandle LookupInstance(
            MotionInfo_Vehicle_Comm instanceData);
    }
    #endregion

    #region IMotionInfo_Inter_Comm_SendDataDataReader
    public interface IMotionInfo_Inter_Comm_SendDataDataReader : DDS.IDataReader
    {
        ReturnCode Read(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Read(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Read(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Read(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Take(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Take(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadWithCondition(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode ReadWithCondition(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode ReadNextSample(
            ref MotionInfo_Inter_Comm_SendData dataValue,
            ref SampleInfo sampleInfo);

        ReturnCode TakeNextSample(
            ref MotionInfo_Inter_Comm_SendData dataValue,
            ref SampleInfo sampleInfo);

        ReturnCode ReadInstance(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeInstance(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstance(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeNextInstance(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstanceWithCondition(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReadNextInstanceWithCondition(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReturnLoan(
            ref MotionInfo_Inter_Comm_SendData[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode GetKeyValue(
            ref MotionInfo_Inter_Comm_SendData key,
            InstanceHandle handle);

        InstanceHandle LookupInstance(
            MotionInfo_Inter_Comm_SendData instance);
    }
    #endregion

    #region IMotionInfo_Inter_Comm_SendDataDataWriter
    public interface IMotionInfo_Inter_Comm_SendDataDataWriter : DDS.IDataWriter
    {
        InstanceHandle RegisterInstance(
            MotionInfo_Inter_Comm_SendData instanceData);

        InstanceHandle RegisterInstanceWithTimestamp(
            MotionInfo_Inter_Comm_SendData instanceData,
            Time sourceTimestamp);

        ReturnCode UnregisterInstance(
            MotionInfo_Inter_Comm_SendData instanceData,
            InstanceHandle instanceHandle);

        ReturnCode UnregisterInstanceWithTimestamp(
            MotionInfo_Inter_Comm_SendData instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Write(
            MotionInfo_Inter_Comm_SendData instanceData);

        ReturnCode Write(
            MotionInfo_Inter_Comm_SendData instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteWithTimestamp(
            MotionInfo_Inter_Comm_SendData instanceData,
            Time sourceTimestamp);

        ReturnCode WriteWithTimestamp(
            MotionInfo_Inter_Comm_SendData instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Dispose(
            MotionInfo_Inter_Comm_SendData instanceData,
            InstanceHandle instanceHandle);

        ReturnCode DisposeWithTimestamp(
            MotionInfo_Inter_Comm_SendData instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode WriteDispose(
            MotionInfo_Inter_Comm_SendData instanceData);

        ReturnCode WriteDispose(
            MotionInfo_Inter_Comm_SendData instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteDisposeWithTimestamp(
            MotionInfo_Inter_Comm_SendData instanceData,
            Time sourceTimestamp);

        ReturnCode WriteDisposeWithTimestamp(
            MotionInfo_Inter_Comm_SendData instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode GetKeyValue(
            ref MotionInfo_Inter_Comm_SendData key,
            InstanceHandle instanceHandle);

        InstanceHandle LookupInstance(
            MotionInfo_Inter_Comm_SendData instanceData);
    }
    #endregion

    #region IMotionInfo_Vehicle_Inter_Comm_ReportData_134DataReader
    public interface IMotionInfo_Vehicle_Inter_Comm_ReportData_134DataReader : DDS.IDataReader
    {
        ReturnCode Read(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Read(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Read(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Read(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Take(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Take(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadWithCondition(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode ReadWithCondition(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode ReadNextSample(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134 dataValue,
            ref SampleInfo sampleInfo);

        ReturnCode TakeNextSample(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134 dataValue,
            ref SampleInfo sampleInfo);

        ReturnCode ReadInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeNextInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstanceWithCondition(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReadNextInstanceWithCondition(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReturnLoan(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode GetKeyValue(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134 key,
            InstanceHandle handle);

        InstanceHandle LookupInstance(
            MotionInfo_Vehicle_Inter_Comm_ReportData_134 instance);
    }
    #endregion

    #region IMotionInfo_Vehicle_Inter_Comm_ReportData_134DataWriter
    public interface IMotionInfo_Vehicle_Inter_Comm_ReportData_134DataWriter : DDS.IDataWriter
    {
        InstanceHandle RegisterInstance(
            MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData);

        InstanceHandle RegisterInstanceWithTimestamp(
            MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData,
            Time sourceTimestamp);

        ReturnCode UnregisterInstance(
            MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData,
            InstanceHandle instanceHandle);

        ReturnCode UnregisterInstanceWithTimestamp(
            MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Write(
            MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData);

        ReturnCode Write(
            MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteWithTimestamp(
            MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData,
            Time sourceTimestamp);

        ReturnCode WriteWithTimestamp(
            MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Dispose(
            MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData,
            InstanceHandle instanceHandle);

        ReturnCode DisposeWithTimestamp(
            MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode WriteDispose(
            MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData);

        ReturnCode WriteDispose(
            MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteDisposeWithTimestamp(
            MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData,
            Time sourceTimestamp);

        ReturnCode WriteDisposeWithTimestamp(
            MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode GetKeyValue(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData_134 key,
            InstanceHandle instanceHandle);

        InstanceHandle LookupInstance(
            MotionInfo_Vehicle_Inter_Comm_ReportData_134 instanceData);
    }
    #endregion

    #region IMotionInfo_Vehicle_Inter_Comm_ReportDataDataReader
    public interface IMotionInfo_Vehicle_Inter_Comm_ReportDataDataReader : DDS.IDataReader
    {
        ReturnCode Read(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Read(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Read(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Read(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Take(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Take(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadWithCondition(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode ReadWithCondition(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode ReadNextSample(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData dataValue,
            ref SampleInfo sampleInfo);

        ReturnCode TakeNextSample(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData dataValue,
            ref SampleInfo sampleInfo);

        ReturnCode ReadInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeNextInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstanceWithCondition(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReadNextInstanceWithCondition(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReturnLoan(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode GetKeyValue(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData key,
            InstanceHandle handle);

        InstanceHandle LookupInstance(
            MotionInfo_Vehicle_Inter_Comm_ReportData instance);
    }
    #endregion

    #region IMotionInfo_Vehicle_Inter_Comm_ReportDataDataWriter
    public interface IMotionInfo_Vehicle_Inter_Comm_ReportDataDataWriter : DDS.IDataWriter
    {
        InstanceHandle RegisterInstance(
            MotionInfo_Vehicle_Inter_Comm_ReportData instanceData);

        InstanceHandle RegisterInstanceWithTimestamp(
            MotionInfo_Vehicle_Inter_Comm_ReportData instanceData,
            Time sourceTimestamp);

        ReturnCode UnregisterInstance(
            MotionInfo_Vehicle_Inter_Comm_ReportData instanceData,
            InstanceHandle instanceHandle);

        ReturnCode UnregisterInstanceWithTimestamp(
            MotionInfo_Vehicle_Inter_Comm_ReportData instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Write(
            MotionInfo_Vehicle_Inter_Comm_ReportData instanceData);

        ReturnCode Write(
            MotionInfo_Vehicle_Inter_Comm_ReportData instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteWithTimestamp(
            MotionInfo_Vehicle_Inter_Comm_ReportData instanceData,
            Time sourceTimestamp);

        ReturnCode WriteWithTimestamp(
            MotionInfo_Vehicle_Inter_Comm_ReportData instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Dispose(
            MotionInfo_Vehicle_Inter_Comm_ReportData instanceData,
            InstanceHandle instanceHandle);

        ReturnCode DisposeWithTimestamp(
            MotionInfo_Vehicle_Inter_Comm_ReportData instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode WriteDispose(
            MotionInfo_Vehicle_Inter_Comm_ReportData instanceData);

        ReturnCode WriteDispose(
            MotionInfo_Vehicle_Inter_Comm_ReportData instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteDisposeWithTimestamp(
            MotionInfo_Vehicle_Inter_Comm_ReportData instanceData,
            Time sourceTimestamp);

        ReturnCode WriteDisposeWithTimestamp(
            MotionInfo_Vehicle_Inter_Comm_ReportData instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode GetKeyValue(
            ref MotionInfo_Vehicle_Inter_Comm_ReportData key,
            InstanceHandle instanceHandle);

        InstanceHandle LookupInstance(
            MotionInfo_Vehicle_Inter_Comm_ReportData instanceData);
    }
    #endregion

    #region IMotionInfo_HandShake_SendDataDataReader
    public interface IMotionInfo_HandShake_SendDataDataReader : DDS.IDataReader
    {
        ReturnCode Read(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Read(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Read(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Read(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Take(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Take(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadWithCondition(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode ReadWithCondition(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode ReadNextSample(
            ref MotionInfo_HandShake_SendData dataValue,
            ref SampleInfo sampleInfo);

        ReturnCode TakeNextSample(
            ref MotionInfo_HandShake_SendData dataValue,
            ref SampleInfo sampleInfo);

        ReturnCode ReadInstance(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeInstance(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstance(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeNextInstance(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstanceWithCondition(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReadNextInstanceWithCondition(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReturnLoan(
            ref MotionInfo_HandShake_SendData[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode GetKeyValue(
            ref MotionInfo_HandShake_SendData key,
            InstanceHandle handle);

        InstanceHandle LookupInstance(
            MotionInfo_HandShake_SendData instance);
    }
    #endregion

    #region IMotionInfo_HandShake_SendDataDataWriter
    public interface IMotionInfo_HandShake_SendDataDataWriter : DDS.IDataWriter
    {
        InstanceHandle RegisterInstance(
            MotionInfo_HandShake_SendData instanceData);

        InstanceHandle RegisterInstanceWithTimestamp(
            MotionInfo_HandShake_SendData instanceData,
            Time sourceTimestamp);

        ReturnCode UnregisterInstance(
            MotionInfo_HandShake_SendData instanceData,
            InstanceHandle instanceHandle);

        ReturnCode UnregisterInstanceWithTimestamp(
            MotionInfo_HandShake_SendData instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Write(
            MotionInfo_HandShake_SendData instanceData);

        ReturnCode Write(
            MotionInfo_HandShake_SendData instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteWithTimestamp(
            MotionInfo_HandShake_SendData instanceData,
            Time sourceTimestamp);

        ReturnCode WriteWithTimestamp(
            MotionInfo_HandShake_SendData instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Dispose(
            MotionInfo_HandShake_SendData instanceData,
            InstanceHandle instanceHandle);

        ReturnCode DisposeWithTimestamp(
            MotionInfo_HandShake_SendData instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode WriteDispose(
            MotionInfo_HandShake_SendData instanceData);

        ReturnCode WriteDispose(
            MotionInfo_HandShake_SendData instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteDisposeWithTimestamp(
            MotionInfo_HandShake_SendData instanceData,
            Time sourceTimestamp);

        ReturnCode WriteDisposeWithTimestamp(
            MotionInfo_HandShake_SendData instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode GetKeyValue(
            ref MotionInfo_HandShake_SendData key,
            InstanceHandle instanceHandle);

        InstanceHandle LookupInstance(
            MotionInfo_HandShake_SendData instanceData);
    }
    #endregion

    #region IMotionInfo_HandShake_RecieveDataDataReader
    public interface IMotionInfo_HandShake_RecieveDataDataReader : DDS.IDataReader
    {
        ReturnCode Read(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Read(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Read(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Read(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Take(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Take(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadWithCondition(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode ReadWithCondition(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode ReadNextSample(
            ref MotionInfo_HandShake_RecieveData dataValue,
            ref SampleInfo sampleInfo);

        ReturnCode TakeNextSample(
            ref MotionInfo_HandShake_RecieveData dataValue,
            ref SampleInfo sampleInfo);

        ReturnCode ReadInstance(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeInstance(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstance(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeNextInstance(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstanceWithCondition(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReadNextInstanceWithCondition(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReturnLoan(
            ref MotionInfo_HandShake_RecieveData[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode GetKeyValue(
            ref MotionInfo_HandShake_RecieveData key,
            InstanceHandle handle);

        InstanceHandle LookupInstance(
            MotionInfo_HandShake_RecieveData instance);
    }
    #endregion

    #region IMotionInfo_HandShake_RecieveDataDataWriter
    public interface IMotionInfo_HandShake_RecieveDataDataWriter : DDS.IDataWriter
    {
        InstanceHandle RegisterInstance(
            MotionInfo_HandShake_RecieveData instanceData);

        InstanceHandle RegisterInstanceWithTimestamp(
            MotionInfo_HandShake_RecieveData instanceData,
            Time sourceTimestamp);

        ReturnCode UnregisterInstance(
            MotionInfo_HandShake_RecieveData instanceData,
            InstanceHandle instanceHandle);

        ReturnCode UnregisterInstanceWithTimestamp(
            MotionInfo_HandShake_RecieveData instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Write(
            MotionInfo_HandShake_RecieveData instanceData);

        ReturnCode Write(
            MotionInfo_HandShake_RecieveData instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteWithTimestamp(
            MotionInfo_HandShake_RecieveData instanceData,
            Time sourceTimestamp);

        ReturnCode WriteWithTimestamp(
            MotionInfo_HandShake_RecieveData instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Dispose(
            MotionInfo_HandShake_RecieveData instanceData,
            InstanceHandle instanceHandle);

        ReturnCode DisposeWithTimestamp(
            MotionInfo_HandShake_RecieveData instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode WriteDispose(
            MotionInfo_HandShake_RecieveData instanceData);

        ReturnCode WriteDispose(
            MotionInfo_HandShake_RecieveData instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteDisposeWithTimestamp(
            MotionInfo_HandShake_RecieveData instanceData,
            Time sourceTimestamp);

        ReturnCode WriteDisposeWithTimestamp(
            MotionInfo_HandShake_RecieveData instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode GetKeyValue(
            ref MotionInfo_HandShake_RecieveData key,
            InstanceHandle instanceHandle);

        InstanceHandle LookupInstance(
            MotionInfo_HandShake_RecieveData instanceData);
    }
    #endregion

    #region IBetween_Vehicle_DataDataReader
    public interface IBetween_Vehicle_DataDataReader : DDS.IDataReader
    {
        ReturnCode Read(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Read(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Read(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Read(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Take(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Take(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadWithCondition(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode ReadWithCondition(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode ReadNextSample(
            ref Between_Vehicle_Data dataValue,
            ref SampleInfo sampleInfo);

        ReturnCode TakeNextSample(
            ref Between_Vehicle_Data dataValue,
            ref SampleInfo sampleInfo);

        ReturnCode ReadInstance(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeInstance(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstance(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeNextInstance(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstanceWithCondition(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReadNextInstanceWithCondition(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReturnLoan(
            ref Between_Vehicle_Data[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode GetKeyValue(
            ref Between_Vehicle_Data key,
            InstanceHandle handle);

        InstanceHandle LookupInstance(
            Between_Vehicle_Data instance);
    }
    #endregion

    #region IBetween_Vehicle_DataDataWriter
    public interface IBetween_Vehicle_DataDataWriter : DDS.IDataWriter
    {
        InstanceHandle RegisterInstance(
            Between_Vehicle_Data instanceData);

        InstanceHandle RegisterInstanceWithTimestamp(
            Between_Vehicle_Data instanceData,
            Time sourceTimestamp);

        ReturnCode UnregisterInstance(
            Between_Vehicle_Data instanceData,
            InstanceHandle instanceHandle);

        ReturnCode UnregisterInstanceWithTimestamp(
            Between_Vehicle_Data instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Write(
            Between_Vehicle_Data instanceData);

        ReturnCode Write(
            Between_Vehicle_Data instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteWithTimestamp(
            Between_Vehicle_Data instanceData,
            Time sourceTimestamp);

        ReturnCode WriteWithTimestamp(
            Between_Vehicle_Data instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Dispose(
            Between_Vehicle_Data instanceData,
            InstanceHandle instanceHandle);

        ReturnCode DisposeWithTimestamp(
            Between_Vehicle_Data instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode WriteDispose(
            Between_Vehicle_Data instanceData);

        ReturnCode WriteDispose(
            Between_Vehicle_Data instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteDisposeWithTimestamp(
            Between_Vehicle_Data instanceData,
            Time sourceTimestamp);

        ReturnCode WriteDisposeWithTimestamp(
            Between_Vehicle_Data instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode GetKeyValue(
            ref Between_Vehicle_Data key,
            InstanceHandle instanceHandle);

        InstanceHandle LookupInstance(
            Between_Vehicle_Data instanceData);
    }
    #endregion

    #region ILoadPort_PIO_FromVehicleDataReader
    public interface ILoadPort_PIO_FromVehicleDataReader : DDS.IDataReader
    {
        ReturnCode Read(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Read(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Read(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Read(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Take(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Take(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadWithCondition(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode ReadWithCondition(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode ReadNextSample(
            ref LoadPort_PIO_FromVehicle dataValue,
            ref SampleInfo sampleInfo);

        ReturnCode TakeNextSample(
            ref LoadPort_PIO_FromVehicle dataValue,
            ref SampleInfo sampleInfo);

        ReturnCode ReadInstance(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeInstance(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstance(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeNextInstance(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstanceWithCondition(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReadNextInstanceWithCondition(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReturnLoan(
            ref LoadPort_PIO_FromVehicle[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode GetKeyValue(
            ref LoadPort_PIO_FromVehicle key,
            InstanceHandle handle);

        InstanceHandle LookupInstance(
            LoadPort_PIO_FromVehicle instance);
    }
    #endregion

    #region ILoadPort_PIO_FromVehicleDataWriter
    public interface ILoadPort_PIO_FromVehicleDataWriter : DDS.IDataWriter
    {
        InstanceHandle RegisterInstance(
            LoadPort_PIO_FromVehicle instanceData);

        InstanceHandle RegisterInstanceWithTimestamp(
            LoadPort_PIO_FromVehicle instanceData,
            Time sourceTimestamp);

        ReturnCode UnregisterInstance(
            LoadPort_PIO_FromVehicle instanceData,
            InstanceHandle instanceHandle);

        ReturnCode UnregisterInstanceWithTimestamp(
            LoadPort_PIO_FromVehicle instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Write(
            LoadPort_PIO_FromVehicle instanceData);

        ReturnCode Write(
            LoadPort_PIO_FromVehicle instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteWithTimestamp(
            LoadPort_PIO_FromVehicle instanceData,
            Time sourceTimestamp);

        ReturnCode WriteWithTimestamp(
            LoadPort_PIO_FromVehicle instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Dispose(
            LoadPort_PIO_FromVehicle instanceData,
            InstanceHandle instanceHandle);

        ReturnCode DisposeWithTimestamp(
            LoadPort_PIO_FromVehicle instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode WriteDispose(
            LoadPort_PIO_FromVehicle instanceData);

        ReturnCode WriteDispose(
            LoadPort_PIO_FromVehicle instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteDisposeWithTimestamp(
            LoadPort_PIO_FromVehicle instanceData,
            Time sourceTimestamp);

        ReturnCode WriteDisposeWithTimestamp(
            LoadPort_PIO_FromVehicle instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode GetKeyValue(
            ref LoadPort_PIO_FromVehicle key,
            InstanceHandle instanceHandle);

        InstanceHandle LookupInstance(
            LoadPort_PIO_FromVehicle instanceData);
    }
    #endregion

    #region ILoadPort_PIO_ToVehicleDataReader
    public interface ILoadPort_PIO_ToVehicleDataReader : DDS.IDataReader
    {
        ReturnCode Read(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Read(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Read(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Read(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Take(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Take(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadWithCondition(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode ReadWithCondition(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode ReadNextSample(
            ref LoadPort_PIO_ToVehicle dataValue,
            ref SampleInfo sampleInfo);

        ReturnCode TakeNextSample(
            ref LoadPort_PIO_ToVehicle dataValue,
            ref SampleInfo sampleInfo);

        ReturnCode ReadInstance(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeInstance(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstance(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeNextInstance(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstanceWithCondition(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReadNextInstanceWithCondition(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReturnLoan(
            ref LoadPort_PIO_ToVehicle[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode GetKeyValue(
            ref LoadPort_PIO_ToVehicle key,
            InstanceHandle handle);

        InstanceHandle LookupInstance(
            LoadPort_PIO_ToVehicle instance);
    }
    #endregion

    #region ILoadPort_PIO_ToVehicleDataWriter
    public interface ILoadPort_PIO_ToVehicleDataWriter : DDS.IDataWriter
    {
        InstanceHandle RegisterInstance(
            LoadPort_PIO_ToVehicle instanceData);

        InstanceHandle RegisterInstanceWithTimestamp(
            LoadPort_PIO_ToVehicle instanceData,
            Time sourceTimestamp);

        ReturnCode UnregisterInstance(
            LoadPort_PIO_ToVehicle instanceData,
            InstanceHandle instanceHandle);

        ReturnCode UnregisterInstanceWithTimestamp(
            LoadPort_PIO_ToVehicle instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Write(
            LoadPort_PIO_ToVehicle instanceData);

        ReturnCode Write(
            LoadPort_PIO_ToVehicle instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteWithTimestamp(
            LoadPort_PIO_ToVehicle instanceData,
            Time sourceTimestamp);

        ReturnCode WriteWithTimestamp(
            LoadPort_PIO_ToVehicle instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Dispose(
            LoadPort_PIO_ToVehicle instanceData,
            InstanceHandle instanceHandle);

        ReturnCode DisposeWithTimestamp(
            LoadPort_PIO_ToVehicle instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode WriteDispose(
            LoadPort_PIO_ToVehicle instanceData);

        ReturnCode WriteDispose(
            LoadPort_PIO_ToVehicle instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteDisposeWithTimestamp(
            LoadPort_PIO_ToVehicle instanceData,
            Time sourceTimestamp);

        ReturnCode WriteDisposeWithTimestamp(
            LoadPort_PIO_ToVehicle instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode GetKeyValue(
            ref LoadPort_PIO_ToVehicle key,
            InstanceHandle instanceHandle);

        InstanceHandle LookupInstance(
            LoadPort_PIO_ToVehicle instanceData);
    }
    #endregion

    #region IEQ_Stages_Interface_IODataReader
    public interface IEQ_Stages_Interface_IODataReader : DDS.IDataReader
    {
        ReturnCode Read(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Read(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Read(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Read(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Take(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Take(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadWithCondition(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode ReadWithCondition(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode ReadNextSample(
            ref EQ_Stages_Interface_IO dataValue,
            ref SampleInfo sampleInfo);

        ReturnCode TakeNextSample(
            ref EQ_Stages_Interface_IO dataValue,
            ref SampleInfo sampleInfo);

        ReturnCode ReadInstance(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeInstance(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstance(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeNextInstance(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstanceWithCondition(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReadNextInstanceWithCondition(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReturnLoan(
            ref EQ_Stages_Interface_IO[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode GetKeyValue(
            ref EQ_Stages_Interface_IO key,
            InstanceHandle handle);

        InstanceHandle LookupInstance(
            EQ_Stages_Interface_IO instance);
    }
    #endregion

    #region IEQ_Stages_Interface_IODataWriter
    public interface IEQ_Stages_Interface_IODataWriter : DDS.IDataWriter
    {
        InstanceHandle RegisterInstance(
            EQ_Stages_Interface_IO instanceData);

        InstanceHandle RegisterInstanceWithTimestamp(
            EQ_Stages_Interface_IO instanceData,
            Time sourceTimestamp);

        ReturnCode UnregisterInstance(
            EQ_Stages_Interface_IO instanceData,
            InstanceHandle instanceHandle);

        ReturnCode UnregisterInstanceWithTimestamp(
            EQ_Stages_Interface_IO instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Write(
            EQ_Stages_Interface_IO instanceData);

        ReturnCode Write(
            EQ_Stages_Interface_IO instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteWithTimestamp(
            EQ_Stages_Interface_IO instanceData,
            Time sourceTimestamp);

        ReturnCode WriteWithTimestamp(
            EQ_Stages_Interface_IO instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Dispose(
            EQ_Stages_Interface_IO instanceData,
            InstanceHandle instanceHandle);

        ReturnCode DisposeWithTimestamp(
            EQ_Stages_Interface_IO instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode WriteDispose(
            EQ_Stages_Interface_IO instanceData);

        ReturnCode WriteDispose(
            EQ_Stages_Interface_IO instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteDisposeWithTimestamp(
            EQ_Stages_Interface_IO instanceData,
            Time sourceTimestamp);

        ReturnCode WriteDisposeWithTimestamp(
            EQ_Stages_Interface_IO instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode GetKeyValue(
            ref EQ_Stages_Interface_IO key,
            InstanceHandle instanceHandle);

        InstanceHandle LookupInstance(
            EQ_Stages_Interface_IO instanceData);
    }
    #endregion

    #region IInterVehicles_BlockZones_ControlDataReader
    public interface IInterVehicles_BlockZones_ControlDataReader : DDS.IDataReader
    {
        ReturnCode Read(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Read(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Read(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Read(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode Take(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples);

        ReturnCode Take(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode Take(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadWithCondition(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode ReadWithCondition(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            IReadCondition readCondition);

        ReturnCode TakeWithCondition(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            IReadCondition readCondition);

        ReturnCode ReadNextSample(
            ref InterVehicles_BlockZones_Control dataValue,
            ref SampleInfo sampleInfo);

        ReturnCode TakeNextSample(
            ref InterVehicles_BlockZones_Control dataValue,
            ref SampleInfo sampleInfo);

        ReturnCode ReadInstance(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadInstance(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeInstance(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeInstance(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstance(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode ReadNextInstance(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode TakeNextInstance(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle);

        ReturnCode TakeNextInstance(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            SampleStateKind sampleStates,
            ViewStateKind viewStates,
            InstanceStateKind instanceStates);

        ReturnCode ReadNextInstanceWithCondition(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReadNextInstanceWithCondition(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode TakeNextInstanceWithCondition(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos,
            int maxSamples,
            InstanceHandle instanceHandle,
            IReadCondition readCondition);

        ReturnCode ReturnLoan(
            ref InterVehicles_BlockZones_Control[] dataValues,
            ref SampleInfo[] sampleInfos);

        ReturnCode GetKeyValue(
            ref InterVehicles_BlockZones_Control key,
            InstanceHandle handle);

        InstanceHandle LookupInstance(
            InterVehicles_BlockZones_Control instance);
    }
    #endregion

    #region IInterVehicles_BlockZones_ControlDataWriter
    public interface IInterVehicles_BlockZones_ControlDataWriter : DDS.IDataWriter
    {
        InstanceHandle RegisterInstance(
            InterVehicles_BlockZones_Control instanceData);

        InstanceHandle RegisterInstanceWithTimestamp(
            InterVehicles_BlockZones_Control instanceData,
            Time sourceTimestamp);

        ReturnCode UnregisterInstance(
            InterVehicles_BlockZones_Control instanceData,
            InstanceHandle instanceHandle);

        ReturnCode UnregisterInstanceWithTimestamp(
            InterVehicles_BlockZones_Control instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Write(
            InterVehicles_BlockZones_Control instanceData);

        ReturnCode Write(
            InterVehicles_BlockZones_Control instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteWithTimestamp(
            InterVehicles_BlockZones_Control instanceData,
            Time sourceTimestamp);

        ReturnCode WriteWithTimestamp(
            InterVehicles_BlockZones_Control instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode Dispose(
            InterVehicles_BlockZones_Control instanceData,
            InstanceHandle instanceHandle);

        ReturnCode DisposeWithTimestamp(
            InterVehicles_BlockZones_Control instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode WriteDispose(
            InterVehicles_BlockZones_Control instanceData);

        ReturnCode WriteDispose(
            InterVehicles_BlockZones_Control instanceData,
            InstanceHandle instanceHandle);

        ReturnCode WriteDisposeWithTimestamp(
            InterVehicles_BlockZones_Control instanceData,
            Time sourceTimestamp);

        ReturnCode WriteDisposeWithTimestamp(
            InterVehicles_BlockZones_Control instanceData,
            InstanceHandle instanceHandle,
            Time sourceTimestamp);

        ReturnCode GetKeyValue(
            ref InterVehicles_BlockZones_Control key,
            InstanceHandle instanceHandle);

        InstanceHandle LookupInstance(
            InterVehicles_BlockZones_Control instanceData);
    }
    #endregion

}

