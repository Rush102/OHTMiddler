using DDS;
using DDS.OpenSplice.CustomMarshalers;
using DDS.OpenSplice.Database;
using DDS.OpenSplice.Kernel;
using System;
using System.Runtime.InteropServices;

namespace Veh_HandShakeData
{
    #region __MotionInfo_Client
    [StructLayout(LayoutKind.Sequential)]
    public struct __MotionInfo_Client
    {
        public int vehID;
        public IntPtr vehName;
        public int NO;
        public int En_TYPE;
        public int CUR_ADR;
        public int CUR_SEC;
        public int IS_CARRY;
        public int IS_OBST;
        public int IS_BLOCK;
        public int IS_HID;
        public int IS_PAUSE;
        public int IS_LGUIDE;
        public int IS_RGUIDE;
        public int SEC_DISTANCE;
        public int BLOCK_SEC;
        public int HID_SEC;
        public int ACT_TYPE;
        public int RPLY_CODE;
    }
    #endregion

    #region MotionInfo_ClientMarshaler
    public sealed class MotionInfo_ClientMarshaler : DDS.OpenSplice.CustomMarshalers.FooDatabaseMarshaler<MotionInfo_Client>
    {
        public static readonly string fullyScopedName = "Veh_HandShakeData::MotionInfo_Client";

        public override void InitEmbeddedMarshalers(IDomainParticipant participant)
        {
        }

        public override V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, System.IntPtr from, System.IntPtr to)
        {
            GCHandle tmpGCHandle = GCHandle.FromIntPtr(from);
            MotionInfo_Client fromData = tmpGCHandle.Target as MotionInfo_Client;
            return CopyIn(typePtr, fromData, to);
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_Client from, System.IntPtr to)
        {
            __MotionInfo_Client nativeImg = new __MotionInfo_Client();
            V_COPYIN_RESULT result = CopyIn(typePtr, from, ref nativeImg);
            if (result == V_COPYIN_RESULT.OK)
            {
                Marshal.StructureToPtr(nativeImg, to, false);
            }
            return result;
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_Client from, ref __MotionInfo_Client to)
        {
            if (from == null) return V_COPYIN_RESULT.INVALID;
            to.vehID = from.vehID;
            if (from.vehName == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.vehName, from.vehName)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.NO = from.NO;
            to.En_TYPE = from.En_TYPE;
            to.CUR_ADR = from.CUR_ADR;
            to.CUR_SEC = from.CUR_SEC;
            to.IS_CARRY = from.IS_CARRY;
            to.IS_OBST = from.IS_OBST;
            to.IS_BLOCK = from.IS_BLOCK;
            to.IS_HID = from.IS_HID;
            to.IS_PAUSE = from.IS_PAUSE;
            to.IS_LGUIDE = from.IS_LGUIDE;
            to.IS_RGUIDE = from.IS_RGUIDE;
            to.SEC_DISTANCE = from.SEC_DISTANCE;
            to.BLOCK_SEC = from.BLOCK_SEC;
            to.HID_SEC = from.HID_SEC;
            to.ACT_TYPE = from.ACT_TYPE;
            to.RPLY_CODE = from.RPLY_CODE;
            return V_COPYIN_RESULT.OK;
        }

        public override void CopyOut(System.IntPtr from, System.IntPtr to)
        {
            __MotionInfo_Client nativeImg = (__MotionInfo_Client) Marshal.PtrToStructure(from, typeof(__MotionInfo_Client));
            GCHandle tmpGCHandleTo = GCHandle.FromIntPtr(to);
            MotionInfo_Client toObj = tmpGCHandleTo.Target as MotionInfo_Client;
            CopyOut(ref nativeImg, ref toObj);
            tmpGCHandleTo.Target = toObj;
        }

        public override void CopyOut(System.IntPtr from, ref MotionInfo_Client to)
        {
            __MotionInfo_Client nativeImg = (__MotionInfo_Client) Marshal.PtrToStructure(from, typeof(__MotionInfo_Client));
            CopyOut(ref nativeImg, ref to);
        }

        public static void StaticCopyOut(System.IntPtr from, ref MotionInfo_Client to)
        {
            __MotionInfo_Client nativeImg = (__MotionInfo_Client) Marshal.PtrToStructure(from, typeof(__MotionInfo_Client));
            CopyOut(ref nativeImg, ref to);
        }

        public static void CopyOut(ref __MotionInfo_Client from, ref MotionInfo_Client to)
        {
            if (to == null) {
                to = new MotionInfo_Client();
            }
            to.vehID = from.vehID;
            to.vehName = ReadString(from.vehName);
            to.NO = from.NO;
            to.En_TYPE = from.En_TYPE;
            to.CUR_ADR = from.CUR_ADR;
            to.CUR_SEC = from.CUR_SEC;
            to.IS_CARRY = from.IS_CARRY;
            to.IS_OBST = from.IS_OBST;
            to.IS_BLOCK = from.IS_BLOCK;
            to.IS_HID = from.IS_HID;
            to.IS_PAUSE = from.IS_PAUSE;
            to.IS_LGUIDE = from.IS_LGUIDE;
            to.IS_RGUIDE = from.IS_RGUIDE;
            to.SEC_DISTANCE = from.SEC_DISTANCE;
            to.BLOCK_SEC = from.BLOCK_SEC;
            to.HID_SEC = from.HID_SEC;
            to.ACT_TYPE = from.ACT_TYPE;
            to.RPLY_CODE = from.RPLY_CODE;
        }

    }
    #endregion

    #region __MotionInfo_Server
    [StructLayout(LayoutKind.Sequential)]
    public struct __MotionInfo_Server
    {
        public int vehID;
        public IntPtr vehName;
        public int NO;
        public int ACT_TYPE;
        public IntPtr FROM_ADR_ID;
        public IntPtr TO_ADR_ID;
        public IntPtr CAST_ID;
        public int RPLY_CODE;
    }
    #endregion

    #region MotionInfo_ServerMarshaler
    public sealed class MotionInfo_ServerMarshaler : DDS.OpenSplice.CustomMarshalers.FooDatabaseMarshaler<MotionInfo_Server>
    {
        public static readonly string fullyScopedName = "Veh_HandShakeData::MotionInfo_Server";

        public override void InitEmbeddedMarshalers(IDomainParticipant participant)
        {
        }

        public override V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, System.IntPtr from, System.IntPtr to)
        {
            GCHandle tmpGCHandle = GCHandle.FromIntPtr(from);
            MotionInfo_Server fromData = tmpGCHandle.Target as MotionInfo_Server;
            return CopyIn(typePtr, fromData, to);
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_Server from, System.IntPtr to)
        {
            __MotionInfo_Server nativeImg = new __MotionInfo_Server();
            V_COPYIN_RESULT result = CopyIn(typePtr, from, ref nativeImg);
            if (result == V_COPYIN_RESULT.OK)
            {
                Marshal.StructureToPtr(nativeImg, to, false);
            }
            return result;
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_Server from, ref __MotionInfo_Server to)
        {
            if (from == null) return V_COPYIN_RESULT.INVALID;
            to.vehID = from.vehID;
            if (from.vehName == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.vehName, from.vehName)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.NO = from.NO;
            to.ACT_TYPE = from.ACT_TYPE;
            if (from.FROM_ADR_ID == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.FROM_ADR_ID, from.FROM_ADR_ID)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.TO_ADR_ID == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.TO_ADR_ID, from.TO_ADR_ID)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.CAST_ID == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.CAST_ID, from.CAST_ID)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.RPLY_CODE = from.RPLY_CODE;
            return V_COPYIN_RESULT.OK;
        }

        public override void CopyOut(System.IntPtr from, System.IntPtr to)
        {
            __MotionInfo_Server nativeImg = (__MotionInfo_Server) Marshal.PtrToStructure(from, typeof(__MotionInfo_Server));
            GCHandle tmpGCHandleTo = GCHandle.FromIntPtr(to);
            MotionInfo_Server toObj = tmpGCHandleTo.Target as MotionInfo_Server;
            CopyOut(ref nativeImg, ref toObj);
            tmpGCHandleTo.Target = toObj;
        }

        public override void CopyOut(System.IntPtr from, ref MotionInfo_Server to)
        {
            __MotionInfo_Server nativeImg = (__MotionInfo_Server) Marshal.PtrToStructure(from, typeof(__MotionInfo_Server));
            CopyOut(ref nativeImg, ref to);
        }

        public static void StaticCopyOut(System.IntPtr from, ref MotionInfo_Server to)
        {
            __MotionInfo_Server nativeImg = (__MotionInfo_Server) Marshal.PtrToStructure(from, typeof(__MotionInfo_Server));
            CopyOut(ref nativeImg, ref to);
        }

        public static void CopyOut(ref __MotionInfo_Server from, ref MotionInfo_Server to)
        {
            if (to == null) {
                to = new MotionInfo_Server();
            }
            to.vehID = from.vehID;
            to.vehName = ReadString(from.vehName);
            to.NO = from.NO;
            to.ACT_TYPE = from.ACT_TYPE;
            to.FROM_ADR_ID = ReadString(from.FROM_ADR_ID);
            to.TO_ADR_ID = ReadString(from.TO_ADR_ID);
            to.CAST_ID = ReadString(from.CAST_ID);
            to.RPLY_CODE = from.RPLY_CODE;
        }

    }
    #endregion

    #region __MotionInfo_Vehicle_Comm
    [StructLayout(LayoutKind.Sequential)]
    public struct __MotionInfo_Vehicle_Comm
    {
        public int vehID;
        public IntPtr vehName;
        public double front_wheel_torque;
        public double rear_wheel_torque;
        public double front_wheel_speed;
        public double rear_wheel_speed;
        public double front_wheel_acc;
        public double front_wheel_dec;
        public double rear_wheel_acc;
        public double rear_wheel_dec;
        public double front_wheel_dist;
        public double rear_wheel_dist;
        public double ave_dist;
        public double ave_speed;
        public double ave_torque;
        public double ave_acc;
        public double ave_dec;
        public double current_pos;
        public double target_speed;
        public double max_speed;
        public double min_spedd;
        public byte right_guide_up;
        public byte left_guide_up;
        public byte right_guide_down;
        public byte left_guide_down;
        public byte right_guide_detection;
        public byte left_guide_detection;
        public byte long_range_obst;
        public byte short_range_obst;
        public byte mid_range_obst;
        public IntPtr current_address;
        public IntPtr from_address;
        public IntPtr to_address;
        public IntPtr current_stage;
        public IntPtr from_stage;
        public IntPtr to_stage;
        public IntPtr port_address;
        public IntPtr barcode_read_address;
    }
    #endregion

    #region MotionInfo_Vehicle_CommMarshaler
    public sealed class MotionInfo_Vehicle_CommMarshaler : DDS.OpenSplice.CustomMarshalers.FooDatabaseMarshaler<MotionInfo_Vehicle_Comm>
    {
        public static readonly string fullyScopedName = "Veh_HandShakeData::MotionInfo_Vehicle_Comm";

        public override void InitEmbeddedMarshalers(IDomainParticipant participant)
        {
        }

        public override V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, System.IntPtr from, System.IntPtr to)
        {
            GCHandle tmpGCHandle = GCHandle.FromIntPtr(from);
            MotionInfo_Vehicle_Comm fromData = tmpGCHandle.Target as MotionInfo_Vehicle_Comm;
            return CopyIn(typePtr, fromData, to);
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_Vehicle_Comm from, System.IntPtr to)
        {
            __MotionInfo_Vehicle_Comm nativeImg = new __MotionInfo_Vehicle_Comm();
            V_COPYIN_RESULT result = CopyIn(typePtr, from, ref nativeImg);
            if (result == V_COPYIN_RESULT.OK)
            {
                Marshal.StructureToPtr(nativeImg, to, false);
            }
            return result;
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_Vehicle_Comm from, ref __MotionInfo_Vehicle_Comm to)
        {
            if (from == null) return V_COPYIN_RESULT.INVALID;
            to.vehID = from.vehID;
            if (from.vehName == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.vehName, from.vehName)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.front_wheel_torque = from.front_wheel_torque;
            to.rear_wheel_torque = from.rear_wheel_torque;
            to.front_wheel_speed = from.front_wheel_speed;
            to.rear_wheel_speed = from.rear_wheel_speed;
            to.front_wheel_acc = from.front_wheel_acc;
            to.front_wheel_dec = from.front_wheel_dec;
            to.rear_wheel_acc = from.rear_wheel_acc;
            to.rear_wheel_dec = from.rear_wheel_dec;
            to.front_wheel_dist = from.front_wheel_dist;
            to.rear_wheel_dist = from.rear_wheel_dist;
            to.ave_dist = from.ave_dist;
            to.ave_speed = from.ave_speed;
            to.ave_torque = from.ave_torque;
            to.ave_acc = from.ave_acc;
            to.ave_dec = from.ave_dec;
            to.current_pos = from.current_pos;
            to.target_speed = from.target_speed;
            to.max_speed = from.max_speed;
            to.min_spedd = from.min_spedd;
            to.right_guide_up = from.right_guide_up ? (byte) 1 : (byte) 0;
            to.left_guide_up = from.left_guide_up ? (byte) 1 : (byte) 0;
            to.right_guide_down = from.right_guide_down ? (byte) 1 : (byte) 0;
            to.left_guide_down = from.left_guide_down ? (byte) 1 : (byte) 0;
            to.right_guide_detection = from.right_guide_detection ? (byte) 1 : (byte) 0;
            to.left_guide_detection = from.left_guide_detection ? (byte) 1 : (byte) 0;
            to.long_range_obst = from.long_range_obst ? (byte) 1 : (byte) 0;
            to.short_range_obst = from.short_range_obst ? (byte) 1 : (byte) 0;
            to.mid_range_obst = from.mid_range_obst ? (byte) 1 : (byte) 0;
            if (from.current_address == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.current_address, from.current_address)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.from_address == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.from_address, from.from_address)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.to_address == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.to_address, from.to_address)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.current_stage == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.current_stage, from.current_stage)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.from_stage == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.from_stage, from.from_stage)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.to_stage == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.to_stage, from.to_stage)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.port_address == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.port_address, from.port_address)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.barcode_read_address == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.barcode_read_address, from.barcode_read_address)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            return V_COPYIN_RESULT.OK;
        }

        public override void CopyOut(System.IntPtr from, System.IntPtr to)
        {
            __MotionInfo_Vehicle_Comm nativeImg = (__MotionInfo_Vehicle_Comm) Marshal.PtrToStructure(from, typeof(__MotionInfo_Vehicle_Comm));
            GCHandle tmpGCHandleTo = GCHandle.FromIntPtr(to);
            MotionInfo_Vehicle_Comm toObj = tmpGCHandleTo.Target as MotionInfo_Vehicle_Comm;
            CopyOut(ref nativeImg, ref toObj);
            tmpGCHandleTo.Target = toObj;
        }

        public override void CopyOut(System.IntPtr from, ref MotionInfo_Vehicle_Comm to)
        {
            __MotionInfo_Vehicle_Comm nativeImg = (__MotionInfo_Vehicle_Comm) Marshal.PtrToStructure(from, typeof(__MotionInfo_Vehicle_Comm));
            CopyOut(ref nativeImg, ref to);
        }

        public static void StaticCopyOut(System.IntPtr from, ref MotionInfo_Vehicle_Comm to)
        {
            __MotionInfo_Vehicle_Comm nativeImg = (__MotionInfo_Vehicle_Comm) Marshal.PtrToStructure(from, typeof(__MotionInfo_Vehicle_Comm));
            CopyOut(ref nativeImg, ref to);
        }

        public static void CopyOut(ref __MotionInfo_Vehicle_Comm from, ref MotionInfo_Vehicle_Comm to)
        {
            if (to == null) {
                to = new MotionInfo_Vehicle_Comm();
            }
            to.vehID = from.vehID;
            to.vehName = ReadString(from.vehName);
            to.front_wheel_torque = from.front_wheel_torque;
            to.rear_wheel_torque = from.rear_wheel_torque;
            to.front_wheel_speed = from.front_wheel_speed;
            to.rear_wheel_speed = from.rear_wheel_speed;
            to.front_wheel_acc = from.front_wheel_acc;
            to.front_wheel_dec = from.front_wheel_dec;
            to.rear_wheel_acc = from.rear_wheel_acc;
            to.rear_wheel_dec = from.rear_wheel_dec;
            to.front_wheel_dist = from.front_wheel_dist;
            to.rear_wheel_dist = from.rear_wheel_dist;
            to.ave_dist = from.ave_dist;
            to.ave_speed = from.ave_speed;
            to.ave_torque = from.ave_torque;
            to.ave_acc = from.ave_acc;
            to.ave_dec = from.ave_dec;
            to.current_pos = from.current_pos;
            to.target_speed = from.target_speed;
            to.max_speed = from.max_speed;
            to.min_spedd = from.min_spedd;
            to.right_guide_up = from.right_guide_up != 0 ? true : false;
            to.left_guide_up = from.left_guide_up != 0 ? true : false;
            to.right_guide_down = from.right_guide_down != 0 ? true : false;
            to.left_guide_down = from.left_guide_down != 0 ? true : false;
            to.right_guide_detection = from.right_guide_detection != 0 ? true : false;
            to.left_guide_detection = from.left_guide_detection != 0 ? true : false;
            to.long_range_obst = from.long_range_obst != 0 ? true : false;
            to.short_range_obst = from.short_range_obst != 0 ? true : false;
            to.mid_range_obst = from.mid_range_obst != 0 ? true : false;
            to.current_address = ReadString(from.current_address);
            to.from_address = ReadString(from.from_address);
            to.to_address = ReadString(from.to_address);
            to.current_stage = ReadString(from.current_stage);
            to.from_stage = ReadString(from.from_stage);
            to.to_stage = ReadString(from.to_stage);
            to.port_address = ReadString(from.port_address);
            to.barcode_read_address = ReadString(from.barcode_read_address);
        }

    }
    #endregion

    #region __MotionInfo_Move
    [StructLayout(LayoutKind.Sequential)]
    public struct __MotionInfo_Move
    {
        public uint eMoveType;
        public IntPtr Address;
        public IntPtr Stage;
        public IntPtr GuidingSections;
        public IntPtr GuidingAddresses;
        public int ForLoading;
        public int ForUnLoading;
        public int ForMaintain;
    }
    #endregion

    #region MotionInfo_MoveMarshaler
    public sealed class MotionInfo_MoveMarshaler : DDS.OpenSplice.CustomMarshalers.FooDatabaseMarshaler<MotionInfo_Move>
    {
        public static readonly string fullyScopedName = "Veh_HandShakeData::MotionInfo_Move";
        private IntPtr attr3Seq0Type = IntPtr.Zero;
        private static readonly int attr3Seq0Size = 1 * Marshal.SizeOf(typeof(IntPtr));
        private IntPtr attr4Seq0Type = IntPtr.Zero;
        private static readonly int attr4Seq0Size = 1 * Marshal.SizeOf(typeof(IntPtr));

        public override void InitEmbeddedMarshalers(IDomainParticipant participant)
        {
        }

        public override V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, System.IntPtr from, System.IntPtr to)
        {
            GCHandle tmpGCHandle = GCHandle.FromIntPtr(from);
            MotionInfo_Move fromData = tmpGCHandle.Target as MotionInfo_Move;
            return CopyIn(typePtr, fromData, to);
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_Move from, System.IntPtr to)
        {
            __MotionInfo_Move nativeImg = new __MotionInfo_Move();
            V_COPYIN_RESULT result = CopyIn(typePtr, from, ref nativeImg);
            if (result == V_COPYIN_RESULT.OK)
            {
                Marshal.StructureToPtr(nativeImg, to, false);
            }
            return result;
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_Move from, ref __MotionInfo_Move to)
        {
            if (from == null) return V_COPYIN_RESULT.INVALID;
            to.eMoveType = (uint) from.eMoveType;
            if (from.Address == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Address, from.Address)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.Stage == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Stage, from.Stage)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.GuidingSections == null) return V_COPYIN_RESULT.INVALID;
            int attr3Seq0Length = from.GuidingSections.Length;
            // Unbounded sequence: bounds check not required...
            if (attr3Seq0Type == IntPtr.Zero) {
                IntPtr memberOwnerType = DDS.OpenSplice.Database.c.resolve(c.getBase(typePtr), fullyScopedName);
                IntPtr specifier = DDS.OpenSplice.Database.c.metaResolveSpecifier(memberOwnerType, "GuidingSections");
                IntPtr specifierType = DDS.OpenSplice.Database.c.specifierType(specifier);
                attr3Seq0Type = DDS.OpenSplice.Database.c.typeActualType(specifierType);
            }
            IntPtr attr3Seq0Buf = DDS.OpenSplice.Database.c.newSequence(attr3Seq0Type, attr3Seq0Length);
            if (attr3Seq0Buf == IntPtr.Zero) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            for (int i0 = 0; i0 < attr3Seq0Length; i0++) {
                if (from.GuidingSections[i0] == null) return V_COPYIN_RESULT.INVALID;
                // Unbounded string: bounds check not required...
                IntPtr stringElementPtr = IntPtr.Zero;
                if (!Write(c.getBase(typePtr), ref stringElementPtr, from.GuidingSections[i0])) return V_COPYIN_RESULT.OUT_OF_MEMORY;
                Marshal.WriteIntPtr(attr3Seq0Buf, stringElementPtr);
                attr3Seq0Buf = new IntPtr(attr3Seq0Buf.ToInt64() + attr3Seq0Size);
            }
            to.GuidingSections = new IntPtr(attr3Seq0Buf.ToInt64() - ((long) attr3Seq0Size * (long) attr3Seq0Length));
            if (from.GuidingAddresses == null) return V_COPYIN_RESULT.INVALID;
            int attr4Seq0Length = from.GuidingAddresses.Length;
            // Unbounded sequence: bounds check not required...
            if (attr4Seq0Type == IntPtr.Zero) {
                IntPtr memberOwnerType = DDS.OpenSplice.Database.c.resolve(c.getBase(typePtr), fullyScopedName);
                IntPtr specifier = DDS.OpenSplice.Database.c.metaResolveSpecifier(memberOwnerType, "GuidingAddresses");
                IntPtr specifierType = DDS.OpenSplice.Database.c.specifierType(specifier);
                attr4Seq0Type = DDS.OpenSplice.Database.c.typeActualType(specifierType);
            }
            IntPtr attr4Seq0Buf = DDS.OpenSplice.Database.c.newSequence(attr4Seq0Type, attr4Seq0Length);
            if (attr4Seq0Buf == IntPtr.Zero) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            for (int i0 = 0; i0 < attr4Seq0Length; i0++) {
                if (from.GuidingAddresses[i0] == null) return V_COPYIN_RESULT.INVALID;
                // Unbounded string: bounds check not required...
                IntPtr stringElementPtr = IntPtr.Zero;
                if (!Write(c.getBase(typePtr), ref stringElementPtr, from.GuidingAddresses[i0])) return V_COPYIN_RESULT.OUT_OF_MEMORY;
                Marshal.WriteIntPtr(attr4Seq0Buf, stringElementPtr);
                attr4Seq0Buf = new IntPtr(attr4Seq0Buf.ToInt64() + attr4Seq0Size);
            }
            to.GuidingAddresses = new IntPtr(attr4Seq0Buf.ToInt64() - ((long) attr4Seq0Size * (long) attr4Seq0Length));
            to.ForLoading = from.ForLoading;
            to.ForUnLoading = from.ForUnLoading;
            to.ForMaintain = from.ForMaintain;
            return V_COPYIN_RESULT.OK;
        }

        public override void CopyOut(System.IntPtr from, System.IntPtr to)
        {
            __MotionInfo_Move nativeImg = (__MotionInfo_Move) Marshal.PtrToStructure(from, typeof(__MotionInfo_Move));
            GCHandle tmpGCHandleTo = GCHandle.FromIntPtr(to);
            MotionInfo_Move toObj = tmpGCHandleTo.Target as MotionInfo_Move;
            CopyOut(ref nativeImg, ref toObj);
            tmpGCHandleTo.Target = toObj;
        }

        public override void CopyOut(System.IntPtr from, ref MotionInfo_Move to)
        {
            __MotionInfo_Move nativeImg = (__MotionInfo_Move) Marshal.PtrToStructure(from, typeof(__MotionInfo_Move));
            CopyOut(ref nativeImg, ref to);
        }

        public static void StaticCopyOut(System.IntPtr from, ref MotionInfo_Move to)
        {
            __MotionInfo_Move nativeImg = (__MotionInfo_Move) Marshal.PtrToStructure(from, typeof(__MotionInfo_Move));
            CopyOut(ref nativeImg, ref to);
        }

        public static void CopyOut(ref __MotionInfo_Move from, ref MotionInfo_Move to)
        {
            if (to == null) {
                to = new MotionInfo_Move();
            }
            to.eMoveType = (Veh_HandShakeData.MoveType) from.eMoveType;
            to.Address = ReadString(from.Address);
            to.Stage = ReadString(from.Stage);
            IntPtr attr3Seq0Buf = from.GuidingSections;
            int attr3Seq0Length = DDS.OpenSplice.Database.c.arraySize(attr3Seq0Buf);
            if (to.GuidingSections == null || to.GuidingSections.Length != attr3Seq0Length) {
                string[] target = new string[attr3Seq0Length];
                initObjectSeq(to.GuidingSections, target);
                to.GuidingSections = target;
            }
            for (int i0 = 0; i0 < attr3Seq0Length; i0++) {
                IntPtr stringElementPtr = Marshal.ReadIntPtr(attr3Seq0Buf);
                to.GuidingSections[i0] = ReadString(stringElementPtr);
                attr3Seq0Buf = new IntPtr(attr3Seq0Buf.ToInt64() + attr3Seq0Size);
            }
            IntPtr attr4Seq0Buf = from.GuidingAddresses;
            int attr4Seq0Length = DDS.OpenSplice.Database.c.arraySize(attr4Seq0Buf);
            if (to.GuidingAddresses == null || to.GuidingAddresses.Length != attr4Seq0Length) {
                string[] target = new string[attr4Seq0Length];
                initObjectSeq(to.GuidingAddresses, target);
                to.GuidingAddresses = target;
            }
            for (int i0 = 0; i0 < attr4Seq0Length; i0++) {
                IntPtr stringElementPtr = Marshal.ReadIntPtr(attr4Seq0Buf);
                to.GuidingAddresses[i0] = ReadString(stringElementPtr);
                attr4Seq0Buf = new IntPtr(attr4Seq0Buf.ToInt64() + attr4Seq0Size);
            }
            to.ForLoading = from.ForLoading;
            to.ForUnLoading = from.ForUnLoading;
            to.ForMaintain = from.ForMaintain;
        }

    }
    #endregion

    #region __MotionInfo_Load
    [StructLayout(LayoutKind.Sequential)]
    public struct __MotionInfo_Load
    {
        public IntPtr MCS_CSTID;
        public IntPtr MCS_CST2ID;
        public IntPtr Veh_CSTID;
        public IntPtr Veh_CST2ID;
        public int VerPort_OK;
        public int VerPort_NG;
        public int VerCST_OK;
        public int VerCST_NG;
        public int VerCST2_OK;
        public int VerCST2_NG;
        public int With_CST;
        public int Without_CST;
        public int With_CST2;
        public int Without_CST2;
        public uint LoadStatus;
    }
    #endregion

    #region MotionInfo_LoadMarshaler
    public sealed class MotionInfo_LoadMarshaler : DDS.OpenSplice.CustomMarshalers.FooDatabaseMarshaler<MotionInfo_Load>
    {
        public static readonly string fullyScopedName = "Veh_HandShakeData::MotionInfo_Load";

        public override void InitEmbeddedMarshalers(IDomainParticipant participant)
        {
        }

        public override V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, System.IntPtr from, System.IntPtr to)
        {
            GCHandle tmpGCHandle = GCHandle.FromIntPtr(from);
            MotionInfo_Load fromData = tmpGCHandle.Target as MotionInfo_Load;
            return CopyIn(typePtr, fromData, to);
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_Load from, System.IntPtr to)
        {
            __MotionInfo_Load nativeImg = new __MotionInfo_Load();
            V_COPYIN_RESULT result = CopyIn(typePtr, from, ref nativeImg);
            if (result == V_COPYIN_RESULT.OK)
            {
                Marshal.StructureToPtr(nativeImg, to, false);
            }
            return result;
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_Load from, ref __MotionInfo_Load to)
        {
            if (from == null) return V_COPYIN_RESULT.INVALID;
            if (from.MCS_CSTID == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.MCS_CSTID, from.MCS_CSTID)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.MCS_CST2ID == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.MCS_CST2ID, from.MCS_CST2ID)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.Veh_CSTID == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Veh_CSTID, from.Veh_CSTID)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.Veh_CST2ID == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Veh_CST2ID, from.Veh_CST2ID)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.VerPort_OK = from.VerPort_OK;
            to.VerPort_NG = from.VerPort_NG;
            to.VerCST_OK = from.VerCST_OK;
            to.VerCST_NG = from.VerCST_NG;
            to.VerCST2_OK = from.VerCST2_OK;
            to.VerCST2_NG = from.VerCST2_NG;
            to.With_CST = from.With_CST;
            to.Without_CST = from.Without_CST;
            to.With_CST2 = from.With_CST2;
            to.Without_CST2 = from.Without_CST2;
            to.LoadStatus = (uint) from.LoadStatus;
            return V_COPYIN_RESULT.OK;
        }

        public override void CopyOut(System.IntPtr from, System.IntPtr to)
        {
            __MotionInfo_Load nativeImg = (__MotionInfo_Load) Marshal.PtrToStructure(from, typeof(__MotionInfo_Load));
            GCHandle tmpGCHandleTo = GCHandle.FromIntPtr(to);
            MotionInfo_Load toObj = tmpGCHandleTo.Target as MotionInfo_Load;
            CopyOut(ref nativeImg, ref toObj);
            tmpGCHandleTo.Target = toObj;
        }

        public override void CopyOut(System.IntPtr from, ref MotionInfo_Load to)
        {
            __MotionInfo_Load nativeImg = (__MotionInfo_Load) Marshal.PtrToStructure(from, typeof(__MotionInfo_Load));
            CopyOut(ref nativeImg, ref to);
        }

        public static void StaticCopyOut(System.IntPtr from, ref MotionInfo_Load to)
        {
            __MotionInfo_Load nativeImg = (__MotionInfo_Load) Marshal.PtrToStructure(from, typeof(__MotionInfo_Load));
            CopyOut(ref nativeImg, ref to);
        }

        public static void CopyOut(ref __MotionInfo_Load from, ref MotionInfo_Load to)
        {
            if (to == null) {
                to = new MotionInfo_Load();
            }
            to.MCS_CSTID = ReadString(from.MCS_CSTID);
            to.MCS_CST2ID = ReadString(from.MCS_CST2ID);
            to.Veh_CSTID = ReadString(from.Veh_CSTID);
            to.Veh_CST2ID = ReadString(from.Veh_CST2ID);
            to.VerPort_OK = from.VerPort_OK;
            to.VerPort_NG = from.VerPort_NG;
            to.VerCST_OK = from.VerCST_OK;
            to.VerCST_NG = from.VerCST_NG;
            to.VerCST2_OK = from.VerCST2_OK;
            to.VerCST2_NG = from.VerCST2_NG;
            to.With_CST = from.With_CST;
            to.Without_CST = from.Without_CST;
            to.With_CST2 = from.With_CST2;
            to.Without_CST2 = from.Without_CST2;
            to.LoadStatus = (Veh_HandShakeData.VehLoadedStatus) from.LoadStatus;
        }

    }
    #endregion

    #region __MotionInfo_UnLoad
    [StructLayout(LayoutKind.Sequential)]
    public struct __MotionInfo_UnLoad
    {
        public IntPtr MCS_CSTID;
        public IntPtr MCS_CST2ID;
        public IntPtr Veh_CSTID;
        public IntPtr Veh_CST2ID;
        public int VerPort_OK;
        public int VerPort_NG;
        public int VerCST_OK;
        public int VerCST_NG;
        public int VerCST2_OK;
        public int VerCST2_NG;
        public int With_CST;
        public int Without_CST;
        public int With_CST2;
        public int Without_CST2;
        public uint LoadStatus;
    }
    #endregion

    #region MotionInfo_UnLoadMarshaler
    public sealed class MotionInfo_UnLoadMarshaler : DDS.OpenSplice.CustomMarshalers.FooDatabaseMarshaler<MotionInfo_UnLoad>
    {
        public static readonly string fullyScopedName = "Veh_HandShakeData::MotionInfo_UnLoad";

        public override void InitEmbeddedMarshalers(IDomainParticipant participant)
        {
        }

        public override V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, System.IntPtr from, System.IntPtr to)
        {
            GCHandle tmpGCHandle = GCHandle.FromIntPtr(from);
            MotionInfo_UnLoad fromData = tmpGCHandle.Target as MotionInfo_UnLoad;
            return CopyIn(typePtr, fromData, to);
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_UnLoad from, System.IntPtr to)
        {
            __MotionInfo_UnLoad nativeImg = new __MotionInfo_UnLoad();
            V_COPYIN_RESULT result = CopyIn(typePtr, from, ref nativeImg);
            if (result == V_COPYIN_RESULT.OK)
            {
                Marshal.StructureToPtr(nativeImg, to, false);
            }
            return result;
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_UnLoad from, ref __MotionInfo_UnLoad to)
        {
            if (from == null) return V_COPYIN_RESULT.INVALID;
            if (from.MCS_CSTID == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.MCS_CSTID, from.MCS_CSTID)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.MCS_CST2ID == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.MCS_CST2ID, from.MCS_CST2ID)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.Veh_CSTID == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Veh_CSTID, from.Veh_CSTID)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.Veh_CST2ID == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Veh_CST2ID, from.Veh_CST2ID)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.VerPort_OK = from.VerPort_OK;
            to.VerPort_NG = from.VerPort_NG;
            to.VerCST_OK = from.VerCST_OK;
            to.VerCST_NG = from.VerCST_NG;
            to.VerCST2_OK = from.VerCST2_OK;
            to.VerCST2_NG = from.VerCST2_NG;
            to.With_CST = from.With_CST;
            to.Without_CST = from.Without_CST;
            to.With_CST2 = from.With_CST2;
            to.Without_CST2 = from.Without_CST2;
            to.LoadStatus = (uint) from.LoadStatus;
            return V_COPYIN_RESULT.OK;
        }

        public override void CopyOut(System.IntPtr from, System.IntPtr to)
        {
            __MotionInfo_UnLoad nativeImg = (__MotionInfo_UnLoad) Marshal.PtrToStructure(from, typeof(__MotionInfo_UnLoad));
            GCHandle tmpGCHandleTo = GCHandle.FromIntPtr(to);
            MotionInfo_UnLoad toObj = tmpGCHandleTo.Target as MotionInfo_UnLoad;
            CopyOut(ref nativeImg, ref toObj);
            tmpGCHandleTo.Target = toObj;
        }

        public override void CopyOut(System.IntPtr from, ref MotionInfo_UnLoad to)
        {
            __MotionInfo_UnLoad nativeImg = (__MotionInfo_UnLoad) Marshal.PtrToStructure(from, typeof(__MotionInfo_UnLoad));
            CopyOut(ref nativeImg, ref to);
        }

        public static void StaticCopyOut(System.IntPtr from, ref MotionInfo_UnLoad to)
        {
            __MotionInfo_UnLoad nativeImg = (__MotionInfo_UnLoad) Marshal.PtrToStructure(from, typeof(__MotionInfo_UnLoad));
            CopyOut(ref nativeImg, ref to);
        }

        public static void CopyOut(ref __MotionInfo_UnLoad from, ref MotionInfo_UnLoad to)
        {
            if (to == null) {
                to = new MotionInfo_UnLoad();
            }
            to.MCS_CSTID = ReadString(from.MCS_CSTID);
            to.MCS_CST2ID = ReadString(from.MCS_CST2ID);
            to.Veh_CSTID = ReadString(from.Veh_CSTID);
            to.Veh_CST2ID = ReadString(from.Veh_CST2ID);
            to.VerPort_OK = from.VerPort_OK;
            to.VerPort_NG = from.VerPort_NG;
            to.VerCST_OK = from.VerCST_OK;
            to.VerCST_NG = from.VerCST_NG;
            to.VerCST2_OK = from.VerCST2_OK;
            to.VerCST2_NG = from.VerCST2_NG;
            to.With_CST = from.With_CST;
            to.Without_CST = from.Without_CST;
            to.With_CST2 = from.With_CST2;
            to.Without_CST2 = from.Without_CST2;
            to.LoadStatus = (Veh_HandShakeData.VehLoadedStatus) from.LoadStatus;
        }

    }
    #endregion

    #region __MotionInfo_BlockSectionPassReply
    [StructLayout(LayoutKind.Sequential)]
    public struct __MotionInfo_BlockSectionPassReply
    {
        public IntPtr Section;
        public uint BlockSectionPassReply;
    }
    #endregion

    #region MotionInfo_BlockSectionPassReplyMarshaler
    public sealed class MotionInfo_BlockSectionPassReplyMarshaler : DDS.OpenSplice.CustomMarshalers.FooDatabaseMarshaler<MotionInfo_BlockSectionPassReply>
    {
        public static readonly string fullyScopedName = "Veh_HandShakeData::MotionInfo_BlockSectionPassReply";

        public override void InitEmbeddedMarshalers(IDomainParticipant participant)
        {
        }

        public override V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, System.IntPtr from, System.IntPtr to)
        {
            GCHandle tmpGCHandle = GCHandle.FromIntPtr(from);
            MotionInfo_BlockSectionPassReply fromData = tmpGCHandle.Target as MotionInfo_BlockSectionPassReply;
            return CopyIn(typePtr, fromData, to);
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_BlockSectionPassReply from, System.IntPtr to)
        {
            __MotionInfo_BlockSectionPassReply nativeImg = new __MotionInfo_BlockSectionPassReply();
            V_COPYIN_RESULT result = CopyIn(typePtr, from, ref nativeImg);
            if (result == V_COPYIN_RESULT.OK)
            {
                Marshal.StructureToPtr(nativeImg, to, false);
            }
            return result;
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_BlockSectionPassReply from, ref __MotionInfo_BlockSectionPassReply to)
        {
            if (from == null) return V_COPYIN_RESULT.INVALID;
            if (from.Section == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Section, from.Section)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.BlockSectionPassReply = (uint) from.BlockSectionPassReply;
            return V_COPYIN_RESULT.OK;
        }

        public override void CopyOut(System.IntPtr from, System.IntPtr to)
        {
            __MotionInfo_BlockSectionPassReply nativeImg = (__MotionInfo_BlockSectionPassReply) Marshal.PtrToStructure(from, typeof(__MotionInfo_BlockSectionPassReply));
            GCHandle tmpGCHandleTo = GCHandle.FromIntPtr(to);
            MotionInfo_BlockSectionPassReply toObj = tmpGCHandleTo.Target as MotionInfo_BlockSectionPassReply;
            CopyOut(ref nativeImg, ref toObj);
            tmpGCHandleTo.Target = toObj;
        }

        public override void CopyOut(System.IntPtr from, ref MotionInfo_BlockSectionPassReply to)
        {
            __MotionInfo_BlockSectionPassReply nativeImg = (__MotionInfo_BlockSectionPassReply) Marshal.PtrToStructure(from, typeof(__MotionInfo_BlockSectionPassReply));
            CopyOut(ref nativeImg, ref to);
        }

        public static void StaticCopyOut(System.IntPtr from, ref MotionInfo_BlockSectionPassReply to)
        {
            __MotionInfo_BlockSectionPassReply nativeImg = (__MotionInfo_BlockSectionPassReply) Marshal.PtrToStructure(from, typeof(__MotionInfo_BlockSectionPassReply));
            CopyOut(ref nativeImg, ref to);
        }

        public static void CopyOut(ref __MotionInfo_BlockSectionPassReply from, ref MotionInfo_BlockSectionPassReply to)
        {
            if (to == null) {
                to = new MotionInfo_BlockSectionPassReply();
            }
            to.Section = ReadString(from.Section);
            to.BlockSectionPassReply = (Veh_HandShakeData.Status) from.BlockSectionPassReply;
        }

    }
    #endregion

    #region __MotionInfo_HIDSectionPassReply
    [StructLayout(LayoutKind.Sequential)]
    public struct __MotionInfo_HIDSectionPassReply
    {
        public IntPtr Section;
        public uint HIDSectionPassReply;
    }
    #endregion

    #region MotionInfo_HIDSectionPassReplyMarshaler
    public sealed class MotionInfo_HIDSectionPassReplyMarshaler : DDS.OpenSplice.CustomMarshalers.FooDatabaseMarshaler<MotionInfo_HIDSectionPassReply>
    {
        public static readonly string fullyScopedName = "Veh_HandShakeData::MotionInfo_HIDSectionPassReply";

        public override void InitEmbeddedMarshalers(IDomainParticipant participant)
        {
        }

        public override V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, System.IntPtr from, System.IntPtr to)
        {
            GCHandle tmpGCHandle = GCHandle.FromIntPtr(from);
            MotionInfo_HIDSectionPassReply fromData = tmpGCHandle.Target as MotionInfo_HIDSectionPassReply;
            return CopyIn(typePtr, fromData, to);
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_HIDSectionPassReply from, System.IntPtr to)
        {
            __MotionInfo_HIDSectionPassReply nativeImg = new __MotionInfo_HIDSectionPassReply();
            V_COPYIN_RESULT result = CopyIn(typePtr, from, ref nativeImg);
            if (result == V_COPYIN_RESULT.OK)
            {
                Marshal.StructureToPtr(nativeImg, to, false);
            }
            return result;
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_HIDSectionPassReply from, ref __MotionInfo_HIDSectionPassReply to)
        {
            if (from == null) return V_COPYIN_RESULT.INVALID;
            if (from.Section == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Section, from.Section)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.HIDSectionPassReply = (uint) from.HIDSectionPassReply;
            return V_COPYIN_RESULT.OK;
        }

        public override void CopyOut(System.IntPtr from, System.IntPtr to)
        {
            __MotionInfo_HIDSectionPassReply nativeImg = (__MotionInfo_HIDSectionPassReply) Marshal.PtrToStructure(from, typeof(__MotionInfo_HIDSectionPassReply));
            GCHandle tmpGCHandleTo = GCHandle.FromIntPtr(to);
            MotionInfo_HIDSectionPassReply toObj = tmpGCHandleTo.Target as MotionInfo_HIDSectionPassReply;
            CopyOut(ref nativeImg, ref toObj);
            tmpGCHandleTo.Target = toObj;
        }

        public override void CopyOut(System.IntPtr from, ref MotionInfo_HIDSectionPassReply to)
        {
            __MotionInfo_HIDSectionPassReply nativeImg = (__MotionInfo_HIDSectionPassReply) Marshal.PtrToStructure(from, typeof(__MotionInfo_HIDSectionPassReply));
            CopyOut(ref nativeImg, ref to);
        }

        public static void StaticCopyOut(System.IntPtr from, ref MotionInfo_HIDSectionPassReply to)
        {
            __MotionInfo_HIDSectionPassReply nativeImg = (__MotionInfo_HIDSectionPassReply) Marshal.PtrToStructure(from, typeof(__MotionInfo_HIDSectionPassReply));
            CopyOut(ref nativeImg, ref to);
        }

        public static void CopyOut(ref __MotionInfo_HIDSectionPassReply from, ref MotionInfo_HIDSectionPassReply to)
        {
            if (to == null) {
                to = new MotionInfo_HIDSectionPassReply();
            }
            to.Section = ReadString(from.Section);
            to.HIDSectionPassReply = (Veh_HandShakeData.Status) from.HIDSectionPassReply;
        }

    }
    #endregion

    #region __MotionInfo_ReserveSectionPassReply
    [StructLayout(LayoutKind.Sequential)]
    public struct __MotionInfo_ReserveSectionPassReply
    {
        public IntPtr SectionList;
        public uint ReserveSectionPassReply;
    }
    #endregion

    #region MotionInfo_ReserveSectionPassReplyMarshaler
    public sealed class MotionInfo_ReserveSectionPassReplyMarshaler : DDS.OpenSplice.CustomMarshalers.FooDatabaseMarshaler<MotionInfo_ReserveSectionPassReply>
    {
        public static readonly string fullyScopedName = "Veh_HandShakeData::MotionInfo_ReserveSectionPassReply";
        private IntPtr attr0Seq0Type = IntPtr.Zero;
        private static readonly int attr0Seq0Size = 1 * Marshal.SizeOf(typeof(IntPtr));

        public override void InitEmbeddedMarshalers(IDomainParticipant participant)
        {
        }

        public override V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, System.IntPtr from, System.IntPtr to)
        {
            GCHandle tmpGCHandle = GCHandle.FromIntPtr(from);
            MotionInfo_ReserveSectionPassReply fromData = tmpGCHandle.Target as MotionInfo_ReserveSectionPassReply;
            return CopyIn(typePtr, fromData, to);
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_ReserveSectionPassReply from, System.IntPtr to)
        {
            __MotionInfo_ReserveSectionPassReply nativeImg = new __MotionInfo_ReserveSectionPassReply();
            V_COPYIN_RESULT result = CopyIn(typePtr, from, ref nativeImg);
            if (result == V_COPYIN_RESULT.OK)
            {
                Marshal.StructureToPtr(nativeImg, to, false);
            }
            return result;
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_ReserveSectionPassReply from, ref __MotionInfo_ReserveSectionPassReply to)
        {
            if (from == null) return V_COPYIN_RESULT.INVALID;
            if (from.SectionList == null) return V_COPYIN_RESULT.INVALID;
            int attr0Seq0Length = from.SectionList.Length;
            // Unbounded sequence: bounds check not required...
            if (attr0Seq0Type == IntPtr.Zero) {
                IntPtr memberOwnerType = DDS.OpenSplice.Database.c.resolve(c.getBase(typePtr), fullyScopedName);
                IntPtr specifier = DDS.OpenSplice.Database.c.metaResolveSpecifier(memberOwnerType, "SectionList");
                IntPtr specifierType = DDS.OpenSplice.Database.c.specifierType(specifier);
                attr0Seq0Type = DDS.OpenSplice.Database.c.typeActualType(specifierType);
            }
            IntPtr attr0Seq0Buf = DDS.OpenSplice.Database.c.newSequence(attr0Seq0Type, attr0Seq0Length);
            if (attr0Seq0Buf == IntPtr.Zero) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            for (int i0 = 0; i0 < attr0Seq0Length; i0++) {
                if (from.SectionList[i0] == null) return V_COPYIN_RESULT.INVALID;
                // Unbounded string: bounds check not required...
                IntPtr stringElementPtr = IntPtr.Zero;
                if (!Write(c.getBase(typePtr), ref stringElementPtr, from.SectionList[i0])) return V_COPYIN_RESULT.OUT_OF_MEMORY;
                Marshal.WriteIntPtr(attr0Seq0Buf, stringElementPtr);
                attr0Seq0Buf = new IntPtr(attr0Seq0Buf.ToInt64() + attr0Seq0Size);
            }
            to.SectionList = new IntPtr(attr0Seq0Buf.ToInt64() - ((long) attr0Seq0Size * (long) attr0Seq0Length));
            to.ReserveSectionPassReply = (uint) from.ReserveSectionPassReply;
            return V_COPYIN_RESULT.OK;
        }

        public override void CopyOut(System.IntPtr from, System.IntPtr to)
        {
            __MotionInfo_ReserveSectionPassReply nativeImg = (__MotionInfo_ReserveSectionPassReply) Marshal.PtrToStructure(from, typeof(__MotionInfo_ReserveSectionPassReply));
            GCHandle tmpGCHandleTo = GCHandle.FromIntPtr(to);
            MotionInfo_ReserveSectionPassReply toObj = tmpGCHandleTo.Target as MotionInfo_ReserveSectionPassReply;
            CopyOut(ref nativeImg, ref toObj);
            tmpGCHandleTo.Target = toObj;
        }

        public override void CopyOut(System.IntPtr from, ref MotionInfo_ReserveSectionPassReply to)
        {
            __MotionInfo_ReserveSectionPassReply nativeImg = (__MotionInfo_ReserveSectionPassReply) Marshal.PtrToStructure(from, typeof(__MotionInfo_ReserveSectionPassReply));
            CopyOut(ref nativeImg, ref to);
        }

        public static void StaticCopyOut(System.IntPtr from, ref MotionInfo_ReserveSectionPassReply to)
        {
            __MotionInfo_ReserveSectionPassReply nativeImg = (__MotionInfo_ReserveSectionPassReply) Marshal.PtrToStructure(from, typeof(__MotionInfo_ReserveSectionPassReply));
            CopyOut(ref nativeImg, ref to);
        }

        public static void CopyOut(ref __MotionInfo_ReserveSectionPassReply from, ref MotionInfo_ReserveSectionPassReply to)
        {
            if (to == null) {
                to = new MotionInfo_ReserveSectionPassReply();
            }
            IntPtr attr0Seq0Buf = from.SectionList;
            int attr0Seq0Length = DDS.OpenSplice.Database.c.arraySize(attr0Seq0Buf);
            if (to.SectionList == null || to.SectionList.Length != attr0Seq0Length) {
                string[] target = new string[attr0Seq0Length];
                initObjectSeq(to.SectionList, target);
                to.SectionList = target;
            }
            for (int i0 = 0; i0 < attr0Seq0Length; i0++) {
                IntPtr stringElementPtr = Marshal.ReadIntPtr(attr0Seq0Buf);
                to.SectionList[i0] = ReadString(stringElementPtr);
                attr0Seq0Buf = new IntPtr(attr0Seq0Buf.ToInt64() + attr0Seq0Size);
            }
            to.ReserveSectionPassReply = (Veh_HandShakeData.Status) from.ReserveSectionPassReply;
        }

    }
    #endregion

    #region __MotionInfo_BlockSectionPassReqst
    [StructLayout(LayoutKind.Sequential)]
    public struct __MotionInfo_BlockSectionPassReqst
    {
        public IntPtr Section;
        public uint BlockSectionPassReqst;
    }
    #endregion

    #region MotionInfo_BlockSectionPassReqstMarshaler
    public sealed class MotionInfo_BlockSectionPassReqstMarshaler : DDS.OpenSplice.CustomMarshalers.FooDatabaseMarshaler<MotionInfo_BlockSectionPassReqst>
    {
        public static readonly string fullyScopedName = "Veh_HandShakeData::MotionInfo_BlockSectionPassReqst";

        public override void InitEmbeddedMarshalers(IDomainParticipant participant)
        {
        }

        public override V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, System.IntPtr from, System.IntPtr to)
        {
            GCHandle tmpGCHandle = GCHandle.FromIntPtr(from);
            MotionInfo_BlockSectionPassReqst fromData = tmpGCHandle.Target as MotionInfo_BlockSectionPassReqst;
            return CopyIn(typePtr, fromData, to);
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_BlockSectionPassReqst from, System.IntPtr to)
        {
            __MotionInfo_BlockSectionPassReqst nativeImg = new __MotionInfo_BlockSectionPassReqst();
            V_COPYIN_RESULT result = CopyIn(typePtr, from, ref nativeImg);
            if (result == V_COPYIN_RESULT.OK)
            {
                Marshal.StructureToPtr(nativeImg, to, false);
            }
            return result;
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_BlockSectionPassReqst from, ref __MotionInfo_BlockSectionPassReqst to)
        {
            if (from == null) return V_COPYIN_RESULT.INVALID;
            if (from.Section == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Section, from.Section)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.BlockSectionPassReqst = (uint) from.BlockSectionPassReqst;
            return V_COPYIN_RESULT.OK;
        }

        public override void CopyOut(System.IntPtr from, System.IntPtr to)
        {
            __MotionInfo_BlockSectionPassReqst nativeImg = (__MotionInfo_BlockSectionPassReqst) Marshal.PtrToStructure(from, typeof(__MotionInfo_BlockSectionPassReqst));
            GCHandle tmpGCHandleTo = GCHandle.FromIntPtr(to);
            MotionInfo_BlockSectionPassReqst toObj = tmpGCHandleTo.Target as MotionInfo_BlockSectionPassReqst;
            CopyOut(ref nativeImg, ref toObj);
            tmpGCHandleTo.Target = toObj;
        }

        public override void CopyOut(System.IntPtr from, ref MotionInfo_BlockSectionPassReqst to)
        {
            __MotionInfo_BlockSectionPassReqst nativeImg = (__MotionInfo_BlockSectionPassReqst) Marshal.PtrToStructure(from, typeof(__MotionInfo_BlockSectionPassReqst));
            CopyOut(ref nativeImg, ref to);
        }

        public static void StaticCopyOut(System.IntPtr from, ref MotionInfo_BlockSectionPassReqst to)
        {
            __MotionInfo_BlockSectionPassReqst nativeImg = (__MotionInfo_BlockSectionPassReqst) Marshal.PtrToStructure(from, typeof(__MotionInfo_BlockSectionPassReqst));
            CopyOut(ref nativeImg, ref to);
        }

        public static void CopyOut(ref __MotionInfo_BlockSectionPassReqst from, ref MotionInfo_BlockSectionPassReqst to)
        {
            if (to == null) {
                to = new MotionInfo_BlockSectionPassReqst();
            }
            to.Section = ReadString(from.Section);
            to.BlockSectionPassReqst = (Veh_HandShakeData.Status) from.BlockSectionPassReqst;
        }

    }
    #endregion

    #region __MotionInfo_HIDSectionPassReqst
    [StructLayout(LayoutKind.Sequential)]
    public struct __MotionInfo_HIDSectionPassReqst
    {
        public IntPtr Section;
        public uint HIDSectionPassReqst;
    }
    #endregion

    #region MotionInfo_HIDSectionPassReqstMarshaler
    public sealed class MotionInfo_HIDSectionPassReqstMarshaler : DDS.OpenSplice.CustomMarshalers.FooDatabaseMarshaler<MotionInfo_HIDSectionPassReqst>
    {
        public static readonly string fullyScopedName = "Veh_HandShakeData::MotionInfo_HIDSectionPassReqst";

        public override void InitEmbeddedMarshalers(IDomainParticipant participant)
        {
        }

        public override V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, System.IntPtr from, System.IntPtr to)
        {
            GCHandle tmpGCHandle = GCHandle.FromIntPtr(from);
            MotionInfo_HIDSectionPassReqst fromData = tmpGCHandle.Target as MotionInfo_HIDSectionPassReqst;
            return CopyIn(typePtr, fromData, to);
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_HIDSectionPassReqst from, System.IntPtr to)
        {
            __MotionInfo_HIDSectionPassReqst nativeImg = new __MotionInfo_HIDSectionPassReqst();
            V_COPYIN_RESULT result = CopyIn(typePtr, from, ref nativeImg);
            if (result == V_COPYIN_RESULT.OK)
            {
                Marshal.StructureToPtr(nativeImg, to, false);
            }
            return result;
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_HIDSectionPassReqst from, ref __MotionInfo_HIDSectionPassReqst to)
        {
            if (from == null) return V_COPYIN_RESULT.INVALID;
            if (from.Section == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Section, from.Section)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.HIDSectionPassReqst = (uint) from.HIDSectionPassReqst;
            return V_COPYIN_RESULT.OK;
        }

        public override void CopyOut(System.IntPtr from, System.IntPtr to)
        {
            __MotionInfo_HIDSectionPassReqst nativeImg = (__MotionInfo_HIDSectionPassReqst) Marshal.PtrToStructure(from, typeof(__MotionInfo_HIDSectionPassReqst));
            GCHandle tmpGCHandleTo = GCHandle.FromIntPtr(to);
            MotionInfo_HIDSectionPassReqst toObj = tmpGCHandleTo.Target as MotionInfo_HIDSectionPassReqst;
            CopyOut(ref nativeImg, ref toObj);
            tmpGCHandleTo.Target = toObj;
        }

        public override void CopyOut(System.IntPtr from, ref MotionInfo_HIDSectionPassReqst to)
        {
            __MotionInfo_HIDSectionPassReqst nativeImg = (__MotionInfo_HIDSectionPassReqst) Marshal.PtrToStructure(from, typeof(__MotionInfo_HIDSectionPassReqst));
            CopyOut(ref nativeImg, ref to);
        }

        public static void StaticCopyOut(System.IntPtr from, ref MotionInfo_HIDSectionPassReqst to)
        {
            __MotionInfo_HIDSectionPassReqst nativeImg = (__MotionInfo_HIDSectionPassReqst) Marshal.PtrToStructure(from, typeof(__MotionInfo_HIDSectionPassReqst));
            CopyOut(ref nativeImg, ref to);
        }

        public static void CopyOut(ref __MotionInfo_HIDSectionPassReqst from, ref MotionInfo_HIDSectionPassReqst to)
        {
            if (to == null) {
                to = new MotionInfo_HIDSectionPassReqst();
            }
            to.Section = ReadString(from.Section);
            to.HIDSectionPassReqst = (Veh_HandShakeData.Status) from.HIDSectionPassReqst;
        }

    }
    #endregion

    #region __MotionInfo_ReserveSectionPassReqst
    [StructLayout(LayoutKind.Sequential)]
    public struct __MotionInfo_ReserveSectionPassReqst
    {
        public IntPtr SectionList;
        public uint ReserveSectionPassReqst;
    }
    #endregion

    #region MotionInfo_ReserveSectionPassReqstMarshaler
    public sealed class MotionInfo_ReserveSectionPassReqstMarshaler : DDS.OpenSplice.CustomMarshalers.FooDatabaseMarshaler<MotionInfo_ReserveSectionPassReqst>
    {
        public static readonly string fullyScopedName = "Veh_HandShakeData::MotionInfo_ReserveSectionPassReqst";
        private IntPtr attr0Seq0Type = IntPtr.Zero;
        private static readonly int attr0Seq0Size = 1 * Marshal.SizeOf(typeof(IntPtr));

        public override void InitEmbeddedMarshalers(IDomainParticipant participant)
        {
        }

        public override V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, System.IntPtr from, System.IntPtr to)
        {
            GCHandle tmpGCHandle = GCHandle.FromIntPtr(from);
            MotionInfo_ReserveSectionPassReqst fromData = tmpGCHandle.Target as MotionInfo_ReserveSectionPassReqst;
            return CopyIn(typePtr, fromData, to);
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_ReserveSectionPassReqst from, System.IntPtr to)
        {
            __MotionInfo_ReserveSectionPassReqst nativeImg = new __MotionInfo_ReserveSectionPassReqst();
            V_COPYIN_RESULT result = CopyIn(typePtr, from, ref nativeImg);
            if (result == V_COPYIN_RESULT.OK)
            {
                Marshal.StructureToPtr(nativeImg, to, false);
            }
            return result;
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_ReserveSectionPassReqst from, ref __MotionInfo_ReserveSectionPassReqst to)
        {
            if (from == null) return V_COPYIN_RESULT.INVALID;
            if (from.SectionList == null) return V_COPYIN_RESULT.INVALID;
            int attr0Seq0Length = from.SectionList.Length;
            // Unbounded sequence: bounds check not required...
            if (attr0Seq0Type == IntPtr.Zero) {
                IntPtr memberOwnerType = DDS.OpenSplice.Database.c.resolve(c.getBase(typePtr), fullyScopedName);
                IntPtr specifier = DDS.OpenSplice.Database.c.metaResolveSpecifier(memberOwnerType, "SectionList");
                IntPtr specifierType = DDS.OpenSplice.Database.c.specifierType(specifier);
                attr0Seq0Type = DDS.OpenSplice.Database.c.typeActualType(specifierType);
            }
            IntPtr attr0Seq0Buf = DDS.OpenSplice.Database.c.newSequence(attr0Seq0Type, attr0Seq0Length);
            if (attr0Seq0Buf == IntPtr.Zero) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            for (int i0 = 0; i0 < attr0Seq0Length; i0++) {
                if (from.SectionList[i0] == null) return V_COPYIN_RESULT.INVALID;
                // Unbounded string: bounds check not required...
                IntPtr stringElementPtr = IntPtr.Zero;
                if (!Write(c.getBase(typePtr), ref stringElementPtr, from.SectionList[i0])) return V_COPYIN_RESULT.OUT_OF_MEMORY;
                Marshal.WriteIntPtr(attr0Seq0Buf, stringElementPtr);
                attr0Seq0Buf = new IntPtr(attr0Seq0Buf.ToInt64() + attr0Seq0Size);
            }
            to.SectionList = new IntPtr(attr0Seq0Buf.ToInt64() - ((long) attr0Seq0Size * (long) attr0Seq0Length));
            to.ReserveSectionPassReqst = (uint) from.ReserveSectionPassReqst;
            return V_COPYIN_RESULT.OK;
        }

        public override void CopyOut(System.IntPtr from, System.IntPtr to)
        {
            __MotionInfo_ReserveSectionPassReqst nativeImg = (__MotionInfo_ReserveSectionPassReqst) Marshal.PtrToStructure(from, typeof(__MotionInfo_ReserveSectionPassReqst));
            GCHandle tmpGCHandleTo = GCHandle.FromIntPtr(to);
            MotionInfo_ReserveSectionPassReqst toObj = tmpGCHandleTo.Target as MotionInfo_ReserveSectionPassReqst;
            CopyOut(ref nativeImg, ref toObj);
            tmpGCHandleTo.Target = toObj;
        }

        public override void CopyOut(System.IntPtr from, ref MotionInfo_ReserveSectionPassReqst to)
        {
            __MotionInfo_ReserveSectionPassReqst nativeImg = (__MotionInfo_ReserveSectionPassReqst) Marshal.PtrToStructure(from, typeof(__MotionInfo_ReserveSectionPassReqst));
            CopyOut(ref nativeImg, ref to);
        }

        public static void StaticCopyOut(System.IntPtr from, ref MotionInfo_ReserveSectionPassReqst to)
        {
            __MotionInfo_ReserveSectionPassReqst nativeImg = (__MotionInfo_ReserveSectionPassReqst) Marshal.PtrToStructure(from, typeof(__MotionInfo_ReserveSectionPassReqst));
            CopyOut(ref nativeImg, ref to);
        }

        public static void CopyOut(ref __MotionInfo_ReserveSectionPassReqst from, ref MotionInfo_ReserveSectionPassReqst to)
        {
            if (to == null) {
                to = new MotionInfo_ReserveSectionPassReqst();
            }
            IntPtr attr0Seq0Buf = from.SectionList;
            int attr0Seq0Length = DDS.OpenSplice.Database.c.arraySize(attr0Seq0Buf);
            if (to.SectionList == null || to.SectionList.Length != attr0Seq0Length) {
                string[] target = new string[attr0Seq0Length];
                initObjectSeq(to.SectionList, target);
                to.SectionList = target;
            }
            for (int i0 = 0; i0 < attr0Seq0Length; i0++) {
                IntPtr stringElementPtr = Marshal.ReadIntPtr(attr0Seq0Buf);
                to.SectionList[i0] = ReadString(stringElementPtr);
                attr0Seq0Buf = new IntPtr(attr0Seq0Buf.ToInt64() + attr0Seq0Size);
            }
            to.ReserveSectionPassReqst = (Veh_HandShakeData.Status) from.ReserveSectionPassReqst;
        }

    }
    #endregion

    #region __VehCmdType
    [StructLayout(LayoutKind.Sequential)]
    public struct __VehCmdType
    {
        public uint eCmdType;
    }
    #endregion

    #region VehCmdTypeMarshaler
    public sealed class VehCmdTypeMarshaler : DDS.OpenSplice.CustomMarshalers.FooDatabaseMarshaler<VehCmdType>
    {
        public static readonly string fullyScopedName = "Veh_HandShakeData::VehCmdType";

        public override void InitEmbeddedMarshalers(IDomainParticipant participant)
        {
        }

        public override V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, System.IntPtr from, System.IntPtr to)
        {
            GCHandle tmpGCHandle = GCHandle.FromIntPtr(from);
            VehCmdType fromData = tmpGCHandle.Target as VehCmdType;
            return CopyIn(typePtr, fromData, to);
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, VehCmdType from, System.IntPtr to)
        {
            __VehCmdType nativeImg = new __VehCmdType();
            V_COPYIN_RESULT result = CopyIn(typePtr, from, ref nativeImg);
            if (result == V_COPYIN_RESULT.OK)
            {
                Marshal.StructureToPtr(nativeImg, to, false);
            }
            return result;
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, VehCmdType from, ref __VehCmdType to)
        {
            if (from == null) return V_COPYIN_RESULT.INVALID;
            to.eCmdType = (uint) from.eCmdType;
            return V_COPYIN_RESULT.OK;
        }

        public override void CopyOut(System.IntPtr from, System.IntPtr to)
        {
            __VehCmdType nativeImg = (__VehCmdType) Marshal.PtrToStructure(from, typeof(__VehCmdType));
            GCHandle tmpGCHandleTo = GCHandle.FromIntPtr(to);
            VehCmdType toObj = tmpGCHandleTo.Target as VehCmdType;
            CopyOut(ref nativeImg, ref toObj);
            tmpGCHandleTo.Target = toObj;
        }

        public override void CopyOut(System.IntPtr from, ref VehCmdType to)
        {
            __VehCmdType nativeImg = (__VehCmdType) Marshal.PtrToStructure(from, typeof(__VehCmdType));
            CopyOut(ref nativeImg, ref to);
        }

        public static void StaticCopyOut(System.IntPtr from, ref VehCmdType to)
        {
            __VehCmdType nativeImg = (__VehCmdType) Marshal.PtrToStructure(from, typeof(__VehCmdType));
            CopyOut(ref nativeImg, ref to);
        }

        public static void CopyOut(ref __VehCmdType from, ref VehCmdType to)
        {
            if (to == null) {
                to = new VehCmdType();
            }
            to.eCmdType = (Veh_HandShakeData.CmdType) from.eCmdType;
        }

    }
    #endregion

    #region __MotionInfo_Inter_Comm_SendData
    [StructLayout(LayoutKind.Sequential)]
    public struct __MotionInfo_Inter_Comm_SendData
    {
        public int vehID;
        public IntPtr vehName;
        public int cmd_Send;
        public int cmd_Receive;
        public int Proc_ON;
        public Veh_HandShakeData.__VehCmdType udtCmdType;
        public uint udtControlMode;
        public Veh_HandShakeData.__MotionInfo_Move udtMove;
        public Veh_HandShakeData.__MotionInfo_Load udtLoad;
        public Veh_HandShakeData.__MotionInfo_UnLoad udtUnLoad;
        public int isContinue;
        public int isStop;
        public int isPause;
        public byte BlockControlTimeOut;
        public byte HIDControlTimeOut;
        public byte ReserveSectionTimeOut;
        public Veh_HandShakeData.__MotionInfo_ReserveSectionPassReply ReserveSectionPassReply;
        public Veh_HandShakeData.__MotionInfo_BlockSectionPassReply BlockSectionPassReply;
        public Veh_HandShakeData.__MotionInfo_HIDSectionPassReply HIDSectionPassReply;
    }
    #endregion

    #region MotionInfo_Inter_Comm_SendDataMarshaler
    public sealed class MotionInfo_Inter_Comm_SendDataMarshaler : DDS.OpenSplice.CustomMarshalers.FooDatabaseMarshaler<MotionInfo_Inter_Comm_SendData>
    {
        public static readonly string fullyScopedName = "Veh_HandShakeData::MotionInfo_Inter_Comm_SendData";
        private Veh_HandShakeData.VehCmdTypeMarshaler attr5Marshaler;
        private Veh_HandShakeData.MotionInfo_MoveMarshaler attr7Marshaler;
        private Veh_HandShakeData.MotionInfo_LoadMarshaler attr8Marshaler;
        private Veh_HandShakeData.MotionInfo_UnLoadMarshaler attr9Marshaler;
        private Veh_HandShakeData.MotionInfo_ReserveSectionPassReplyMarshaler attr16Marshaler;
        private Veh_HandShakeData.MotionInfo_BlockSectionPassReplyMarshaler attr17Marshaler;
        private Veh_HandShakeData.MotionInfo_HIDSectionPassReplyMarshaler attr18Marshaler;

        public override void InitEmbeddedMarshalers(IDomainParticipant participant)
        {
            if (attr5Marshaler == null) {
                attr5Marshaler = DatabaseMarshaler.GetMarshaler(participant, typeof(Veh_HandShakeData.VehCmdType)) as Veh_HandShakeData.VehCmdTypeMarshaler;
                if (attr5Marshaler == null) {
                    attr5Marshaler = new Veh_HandShakeData.VehCmdTypeMarshaler();
                    DatabaseMarshaler.Add(participant, typeof(Veh_HandShakeData.VehCmdType), attr5Marshaler);
                    attr5Marshaler.InitEmbeddedMarshalers(participant);
                }
            }
            if (attr7Marshaler == null) {
                attr7Marshaler = DatabaseMarshaler.GetMarshaler(participant, typeof(Veh_HandShakeData.MotionInfo_Move)) as Veh_HandShakeData.MotionInfo_MoveMarshaler;
                if (attr7Marshaler == null) {
                    attr7Marshaler = new Veh_HandShakeData.MotionInfo_MoveMarshaler();
                    DatabaseMarshaler.Add(participant, typeof(Veh_HandShakeData.MotionInfo_Move), attr7Marshaler);
                    attr7Marshaler.InitEmbeddedMarshalers(participant);
                }
            }
            if (attr8Marshaler == null) {
                attr8Marshaler = DatabaseMarshaler.GetMarshaler(participant, typeof(Veh_HandShakeData.MotionInfo_Load)) as Veh_HandShakeData.MotionInfo_LoadMarshaler;
                if (attr8Marshaler == null) {
                    attr8Marshaler = new Veh_HandShakeData.MotionInfo_LoadMarshaler();
                    DatabaseMarshaler.Add(participant, typeof(Veh_HandShakeData.MotionInfo_Load), attr8Marshaler);
                    attr8Marshaler.InitEmbeddedMarshalers(participant);
                }
            }
            if (attr9Marshaler == null) {
                attr9Marshaler = DatabaseMarshaler.GetMarshaler(participant, typeof(Veh_HandShakeData.MotionInfo_UnLoad)) as Veh_HandShakeData.MotionInfo_UnLoadMarshaler;
                if (attr9Marshaler == null) {
                    attr9Marshaler = new Veh_HandShakeData.MotionInfo_UnLoadMarshaler();
                    DatabaseMarshaler.Add(participant, typeof(Veh_HandShakeData.MotionInfo_UnLoad), attr9Marshaler);
                    attr9Marshaler.InitEmbeddedMarshalers(participant);
                }
            }
            if (attr16Marshaler == null) {
                attr16Marshaler = DatabaseMarshaler.GetMarshaler(participant, typeof(Veh_HandShakeData.MotionInfo_ReserveSectionPassReply)) as Veh_HandShakeData.MotionInfo_ReserveSectionPassReplyMarshaler;
                if (attr16Marshaler == null) {
                    attr16Marshaler = new Veh_HandShakeData.MotionInfo_ReserveSectionPassReplyMarshaler();
                    DatabaseMarshaler.Add(participant, typeof(Veh_HandShakeData.MotionInfo_ReserveSectionPassReply), attr16Marshaler);
                    attr16Marshaler.InitEmbeddedMarshalers(participant);
                }
            }
            if (attr17Marshaler == null) {
                attr17Marshaler = DatabaseMarshaler.GetMarshaler(participant, typeof(Veh_HandShakeData.MotionInfo_BlockSectionPassReply)) as Veh_HandShakeData.MotionInfo_BlockSectionPassReplyMarshaler;
                if (attr17Marshaler == null) {
                    attr17Marshaler = new Veh_HandShakeData.MotionInfo_BlockSectionPassReplyMarshaler();
                    DatabaseMarshaler.Add(participant, typeof(Veh_HandShakeData.MotionInfo_BlockSectionPassReply), attr17Marshaler);
                    attr17Marshaler.InitEmbeddedMarshalers(participant);
                }
            }
            if (attr18Marshaler == null) {
                attr18Marshaler = DatabaseMarshaler.GetMarshaler(participant, typeof(Veh_HandShakeData.MotionInfo_HIDSectionPassReply)) as Veh_HandShakeData.MotionInfo_HIDSectionPassReplyMarshaler;
                if (attr18Marshaler == null) {
                    attr18Marshaler = new Veh_HandShakeData.MotionInfo_HIDSectionPassReplyMarshaler();
                    DatabaseMarshaler.Add(participant, typeof(Veh_HandShakeData.MotionInfo_HIDSectionPassReply), attr18Marshaler);
                    attr18Marshaler.InitEmbeddedMarshalers(participant);
                }
            }
        }

        public override V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, System.IntPtr from, System.IntPtr to)
        {
            GCHandle tmpGCHandle = GCHandle.FromIntPtr(from);
            MotionInfo_Inter_Comm_SendData fromData = tmpGCHandle.Target as MotionInfo_Inter_Comm_SendData;
            return CopyIn(typePtr, fromData, to);
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_Inter_Comm_SendData from, System.IntPtr to)
        {
            __MotionInfo_Inter_Comm_SendData nativeImg = new __MotionInfo_Inter_Comm_SendData();
            V_COPYIN_RESULT result = CopyIn(typePtr, from, ref nativeImg);
            if (result == V_COPYIN_RESULT.OK)
            {
                Marshal.StructureToPtr(nativeImg, to, false);
            }
            return result;
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_Inter_Comm_SendData from, ref __MotionInfo_Inter_Comm_SendData to)
        {
            if (from == null) return V_COPYIN_RESULT.INVALID;
            to.vehID = from.vehID;
            if (from.vehName == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.vehName, from.vehName)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.cmd_Send = from.cmd_Send;
            to.cmd_Receive = from.cmd_Receive;
            to.Proc_ON = from.Proc_ON;
            {
                V_COPYIN_RESULT result = attr5Marshaler.CopyIn(typePtr, from.udtCmdType, ref to.udtCmdType);
                if (result != V_COPYIN_RESULT.OK) return result;
            }
            to.udtControlMode = (uint) from.udtControlMode;
            {
                V_COPYIN_RESULT result = attr7Marshaler.CopyIn(typePtr, from.udtMove, ref to.udtMove);
                if (result != V_COPYIN_RESULT.OK) return result;
            }
            {
                V_COPYIN_RESULT result = attr8Marshaler.CopyIn(typePtr, from.udtLoad, ref to.udtLoad);
                if (result != V_COPYIN_RESULT.OK) return result;
            }
            {
                V_COPYIN_RESULT result = attr9Marshaler.CopyIn(typePtr, from.udtUnLoad, ref to.udtUnLoad);
                if (result != V_COPYIN_RESULT.OK) return result;
            }
            to.isContinue = from.isContinue;
            to.isStop = from.isStop;
            to.isPause = from.isPause;
            to.BlockControlTimeOut = from.BlockControlTimeOut ? (byte) 1 : (byte) 0;
            to.HIDControlTimeOut = from.HIDControlTimeOut ? (byte) 1 : (byte) 0;
            to.ReserveSectionTimeOut = from.ReserveSectionTimeOut ? (byte) 1 : (byte) 0;
            {
                V_COPYIN_RESULT result = attr16Marshaler.CopyIn(typePtr, from.ReserveSectionPassReply, ref to.ReserveSectionPassReply);
                if (result != V_COPYIN_RESULT.OK) return result;
            }
            {
                V_COPYIN_RESULT result = attr17Marshaler.CopyIn(typePtr, from.BlockSectionPassReply, ref to.BlockSectionPassReply);
                if (result != V_COPYIN_RESULT.OK) return result;
            }
            {
                V_COPYIN_RESULT result = attr18Marshaler.CopyIn(typePtr, from.HIDSectionPassReply, ref to.HIDSectionPassReply);
                if (result != V_COPYIN_RESULT.OK) return result;
            }
            return V_COPYIN_RESULT.OK;
        }

        public override void CopyOut(System.IntPtr from, System.IntPtr to)
        {
            __MotionInfo_Inter_Comm_SendData nativeImg = (__MotionInfo_Inter_Comm_SendData) Marshal.PtrToStructure(from, typeof(__MotionInfo_Inter_Comm_SendData));
            GCHandle tmpGCHandleTo = GCHandle.FromIntPtr(to);
            MotionInfo_Inter_Comm_SendData toObj = tmpGCHandleTo.Target as MotionInfo_Inter_Comm_SendData;
            CopyOut(ref nativeImg, ref toObj);
            tmpGCHandleTo.Target = toObj;
        }

        public override void CopyOut(System.IntPtr from, ref MotionInfo_Inter_Comm_SendData to)
        {
            __MotionInfo_Inter_Comm_SendData nativeImg = (__MotionInfo_Inter_Comm_SendData) Marshal.PtrToStructure(from, typeof(__MotionInfo_Inter_Comm_SendData));
            CopyOut(ref nativeImg, ref to);
        }

        public static void StaticCopyOut(System.IntPtr from, ref MotionInfo_Inter_Comm_SendData to)
        {
            __MotionInfo_Inter_Comm_SendData nativeImg = (__MotionInfo_Inter_Comm_SendData) Marshal.PtrToStructure(from, typeof(__MotionInfo_Inter_Comm_SendData));
            CopyOut(ref nativeImg, ref to);
        }

        public static void CopyOut(ref __MotionInfo_Inter_Comm_SendData from, ref MotionInfo_Inter_Comm_SendData to)
        {
            if (to == null) {
                to = new MotionInfo_Inter_Comm_SendData();
            }
            to.vehID = from.vehID;
            to.vehName = ReadString(from.vehName);
            to.cmd_Send = from.cmd_Send;
            to.cmd_Receive = from.cmd_Receive;
            to.Proc_ON = from.Proc_ON;
            Veh_HandShakeData.VehCmdTypeMarshaler.CopyOut(ref from.udtCmdType, ref to.udtCmdType);
            to.udtControlMode = (Veh_HandShakeData.VehControlMode) from.udtControlMode;
            Veh_HandShakeData.MotionInfo_MoveMarshaler.CopyOut(ref from.udtMove, ref to.udtMove);
            Veh_HandShakeData.MotionInfo_LoadMarshaler.CopyOut(ref from.udtLoad, ref to.udtLoad);
            Veh_HandShakeData.MotionInfo_UnLoadMarshaler.CopyOut(ref from.udtUnLoad, ref to.udtUnLoad);
            to.isContinue = from.isContinue;
            to.isStop = from.isStop;
            to.isPause = from.isPause;
            to.BlockControlTimeOut = from.BlockControlTimeOut != 0 ? true : false;
            to.HIDControlTimeOut = from.HIDControlTimeOut != 0 ? true : false;
            to.ReserveSectionTimeOut = from.ReserveSectionTimeOut != 0 ? true : false;
            Veh_HandShakeData.MotionInfo_ReserveSectionPassReplyMarshaler.CopyOut(ref from.ReserveSectionPassReply, ref to.ReserveSectionPassReply);
            Veh_HandShakeData.MotionInfo_BlockSectionPassReplyMarshaler.CopyOut(ref from.BlockSectionPassReply, ref to.BlockSectionPassReply);
            Veh_HandShakeData.MotionInfo_HIDSectionPassReplyMarshaler.CopyOut(ref from.HIDSectionPassReply, ref to.HIDSectionPassReply);
        }

    }
    #endregion

    #region __MotionInfo_Vehicle_Inter_Comm_ReportData_134
    [StructLayout(LayoutKind.Sequential)]
    public struct __MotionInfo_Vehicle_Inter_Comm_ReportData_134
    {
        public int vehID;
        public IntPtr vehName;
        public Veh_HandShakeData.__MotionInfo_Load loadStatus;
        public Veh_HandShakeData.__MotionInfo_UnLoad unLoadStatus;
        public Veh_HandShakeData.__MotionInfo_BlockSectionPassReqst BlockSectionPassReqst;
        public Veh_HandShakeData.__MotionInfo_HIDSectionPassReqst HIDSectionPassReqst;
        public Veh_HandShakeData.__MotionInfo_ReserveSectionPassReqst ReserveSectionPassReqst;
        public uint WheelAngle;
        public uint ConrtolMode;
        public int WhichType;
        public int LocationType;
        public IntPtr Section;
        public IntPtr Address;
        public IntPtr Stage;
        public double DistanceFromSectionStart;
        public double WalkLength;
        public double PowerConsume;
        public int Guiding;
        public IntPtr ReserveSection;
        public IntPtr BlockControlSection;
        public IntPtr HIDControlSection;
        public int Proc_ON;
        public int cmd_Send;
        public int cmd_Receive;
        public int cmpCode;
        public int cmpStatus;
        public int stopStatusForEvent;
        public int vehModeStatus;
        public int vehActionStatus;
        public int eventTypes;
        public int actionType;
        public int vehLeftGuideLockStatus;
        public int vehRightGuideLockStatus;
        public int vehPauseStatus;
        public int vehBlockStopStatus;
        public int vehReserveStopStatus;
        public int vehHIDStopStatus;
        public int vehObstacleStopStatus;
        public int vehBlockSectionPassReqst;
        public int vehHIDSectionPassReqst;
        public int vehReserveSectionPassReqst;
        public int locationTypes;
        public int vehLoadStatus;
        public int vehObstDist;
        public int vehPowerStatus;
        public int ChargeStatus;
        public int BatteryCapacity;
        public int BatteryTemperature;
        public int ErrorCode;
        public int ErrorStatus;
    }
    #endregion

    #region MotionInfo_Vehicle_Inter_Comm_ReportData_134Marshaler
    public sealed class MotionInfo_Vehicle_Inter_Comm_ReportData_134Marshaler : DDS.OpenSplice.CustomMarshalers.FooDatabaseMarshaler<MotionInfo_Vehicle_Inter_Comm_ReportData_134>
    {
        public static readonly string fullyScopedName = "Veh_HandShakeData::MotionInfo_Vehicle_Inter_Comm_ReportData_134";
        private Veh_HandShakeData.MotionInfo_LoadMarshaler attr2Marshaler;
        private Veh_HandShakeData.MotionInfo_UnLoadMarshaler attr3Marshaler;
        private Veh_HandShakeData.MotionInfo_BlockSectionPassReqstMarshaler attr4Marshaler;
        private Veh_HandShakeData.MotionInfo_HIDSectionPassReqstMarshaler attr5Marshaler;
        private Veh_HandShakeData.MotionInfo_ReserveSectionPassReqstMarshaler attr6Marshaler;

        public override void InitEmbeddedMarshalers(IDomainParticipant participant)
        {
            if (attr2Marshaler == null) {
                attr2Marshaler = DatabaseMarshaler.GetMarshaler(participant, typeof(Veh_HandShakeData.MotionInfo_Load)) as Veh_HandShakeData.MotionInfo_LoadMarshaler;
                if (attr2Marshaler == null) {
                    attr2Marshaler = new Veh_HandShakeData.MotionInfo_LoadMarshaler();
                    DatabaseMarshaler.Add(participant, typeof(Veh_HandShakeData.MotionInfo_Load), attr2Marshaler);
                    attr2Marshaler.InitEmbeddedMarshalers(participant);
                }
            }
            if (attr3Marshaler == null) {
                attr3Marshaler = DatabaseMarshaler.GetMarshaler(participant, typeof(Veh_HandShakeData.MotionInfo_UnLoad)) as Veh_HandShakeData.MotionInfo_UnLoadMarshaler;
                if (attr3Marshaler == null) {
                    attr3Marshaler = new Veh_HandShakeData.MotionInfo_UnLoadMarshaler();
                    DatabaseMarshaler.Add(participant, typeof(Veh_HandShakeData.MotionInfo_UnLoad), attr3Marshaler);
                    attr3Marshaler.InitEmbeddedMarshalers(participant);
                }
            }
            if (attr4Marshaler == null) {
                attr4Marshaler = DatabaseMarshaler.GetMarshaler(participant, typeof(Veh_HandShakeData.MotionInfo_BlockSectionPassReqst)) as Veh_HandShakeData.MotionInfo_BlockSectionPassReqstMarshaler;
                if (attr4Marshaler == null) {
                    attr4Marshaler = new Veh_HandShakeData.MotionInfo_BlockSectionPassReqstMarshaler();
                    DatabaseMarshaler.Add(participant, typeof(Veh_HandShakeData.MotionInfo_BlockSectionPassReqst), attr4Marshaler);
                    attr4Marshaler.InitEmbeddedMarshalers(participant);
                }
            }
            if (attr5Marshaler == null) {
                attr5Marshaler = DatabaseMarshaler.GetMarshaler(participant, typeof(Veh_HandShakeData.MotionInfo_HIDSectionPassReqst)) as Veh_HandShakeData.MotionInfo_HIDSectionPassReqstMarshaler;
                if (attr5Marshaler == null) {
                    attr5Marshaler = new Veh_HandShakeData.MotionInfo_HIDSectionPassReqstMarshaler();
                    DatabaseMarshaler.Add(participant, typeof(Veh_HandShakeData.MotionInfo_HIDSectionPassReqst), attr5Marshaler);
                    attr5Marshaler.InitEmbeddedMarshalers(participant);
                }
            }
            if (attr6Marshaler == null) {
                attr6Marshaler = DatabaseMarshaler.GetMarshaler(participant, typeof(Veh_HandShakeData.MotionInfo_ReserveSectionPassReqst)) as Veh_HandShakeData.MotionInfo_ReserveSectionPassReqstMarshaler;
                if (attr6Marshaler == null) {
                    attr6Marshaler = new Veh_HandShakeData.MotionInfo_ReserveSectionPassReqstMarshaler();
                    DatabaseMarshaler.Add(participant, typeof(Veh_HandShakeData.MotionInfo_ReserveSectionPassReqst), attr6Marshaler);
                    attr6Marshaler.InitEmbeddedMarshalers(participant);
                }
            }
        }

        public override V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, System.IntPtr from, System.IntPtr to)
        {
            GCHandle tmpGCHandle = GCHandle.FromIntPtr(from);
            MotionInfo_Vehicle_Inter_Comm_ReportData_134 fromData = tmpGCHandle.Target as MotionInfo_Vehicle_Inter_Comm_ReportData_134;
            return CopyIn(typePtr, fromData, to);
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_Vehicle_Inter_Comm_ReportData_134 from, System.IntPtr to)
        {
            __MotionInfo_Vehicle_Inter_Comm_ReportData_134 nativeImg = new __MotionInfo_Vehicle_Inter_Comm_ReportData_134();
            V_COPYIN_RESULT result = CopyIn(typePtr, from, ref nativeImg);
            if (result == V_COPYIN_RESULT.OK)
            {
                Marshal.StructureToPtr(nativeImg, to, false);
            }
            return result;
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_Vehicle_Inter_Comm_ReportData_134 from, ref __MotionInfo_Vehicle_Inter_Comm_ReportData_134 to)
        {
            if (from == null) return V_COPYIN_RESULT.INVALID;
            to.vehID = from.vehID;
            if (from.vehName == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.vehName, from.vehName)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            {
                V_COPYIN_RESULT result = attr2Marshaler.CopyIn(typePtr, from.loadStatus, ref to.loadStatus);
                if (result != V_COPYIN_RESULT.OK) return result;
            }
            {
                V_COPYIN_RESULT result = attr3Marshaler.CopyIn(typePtr, from.unLoadStatus, ref to.unLoadStatus);
                if (result != V_COPYIN_RESULT.OK) return result;
            }
            {
                V_COPYIN_RESULT result = attr4Marshaler.CopyIn(typePtr, from.BlockSectionPassReqst, ref to.BlockSectionPassReqst);
                if (result != V_COPYIN_RESULT.OK) return result;
            }
            {
                V_COPYIN_RESULT result = attr5Marshaler.CopyIn(typePtr, from.HIDSectionPassReqst, ref to.HIDSectionPassReqst);
                if (result != V_COPYIN_RESULT.OK) return result;
            }
            {
                V_COPYIN_RESULT result = attr6Marshaler.CopyIn(typePtr, from.ReserveSectionPassReqst, ref to.ReserveSectionPassReqst);
                if (result != V_COPYIN_RESULT.OK) return result;
            }
            to.WheelAngle = (uint) from.WheelAngle;
            to.ConrtolMode = (uint) from.ConrtolMode;
            to.WhichType = from.WhichType;
            to.LocationType = from.LocationType;
            if (from.Section == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Section, from.Section)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.Address == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Address, from.Address)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.Stage == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Stage, from.Stage)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.DistanceFromSectionStart = from.DistanceFromSectionStart;
            to.WalkLength = from.WalkLength;
            to.PowerConsume = from.PowerConsume;
            to.Guiding = from.Guiding;
            if (from.ReserveSection == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.ReserveSection, from.ReserveSection)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.BlockControlSection == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.BlockControlSection, from.BlockControlSection)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.HIDControlSection == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.HIDControlSection, from.HIDControlSection)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.Proc_ON = from.Proc_ON;
            to.cmd_Send = from.cmd_Send;
            to.cmd_Receive = from.cmd_Receive;
            to.cmpCode = from.cmpCode;
            to.cmpStatus = from.cmpStatus;
            to.stopStatusForEvent = from.stopStatusForEvent;
            to.vehModeStatus = from.vehModeStatus;
            to.vehActionStatus = from.vehActionStatus;
            to.eventTypes = from.eventTypes;
            to.actionType = from.actionType;
            to.vehLeftGuideLockStatus = from.vehLeftGuideLockStatus;
            to.vehRightGuideLockStatus = from.vehRightGuideLockStatus;
            to.vehPauseStatus = from.vehPauseStatus;
            to.vehBlockStopStatus = from.vehBlockStopStatus;
            to.vehReserveStopStatus = from.vehReserveStopStatus;
            to.vehHIDStopStatus = from.vehHIDStopStatus;
            to.vehObstacleStopStatus = from.vehObstacleStopStatus;
            to.vehBlockSectionPassReqst = from.vehBlockSectionPassReqst;
            to.vehHIDSectionPassReqst = from.vehHIDSectionPassReqst;
            to.vehReserveSectionPassReqst = from.vehReserveSectionPassReqst;
            to.locationTypes = from.locationTypes;
            to.vehLoadStatus = from.vehLoadStatus;
            to.vehObstDist = from.vehObstDist;
            to.vehPowerStatus = from.vehPowerStatus;
            to.ChargeStatus = from.ChargeStatus;
            to.BatteryCapacity = from.BatteryCapacity;
            to.BatteryTemperature = from.BatteryTemperature;
            to.ErrorCode = from.ErrorCode;
            to.ErrorStatus = from.ErrorStatus;
            return V_COPYIN_RESULT.OK;
        }

        public override void CopyOut(System.IntPtr from, System.IntPtr to)
        {
            __MotionInfo_Vehicle_Inter_Comm_ReportData_134 nativeImg = (__MotionInfo_Vehicle_Inter_Comm_ReportData_134) Marshal.PtrToStructure(from, typeof(__MotionInfo_Vehicle_Inter_Comm_ReportData_134));
            GCHandle tmpGCHandleTo = GCHandle.FromIntPtr(to);
            MotionInfo_Vehicle_Inter_Comm_ReportData_134 toObj = tmpGCHandleTo.Target as MotionInfo_Vehicle_Inter_Comm_ReportData_134;
            CopyOut(ref nativeImg, ref toObj);
            tmpGCHandleTo.Target = toObj;
        }

        public override void CopyOut(System.IntPtr from, ref MotionInfo_Vehicle_Inter_Comm_ReportData_134 to)
        {
            __MotionInfo_Vehicle_Inter_Comm_ReportData_134 nativeImg = (__MotionInfo_Vehicle_Inter_Comm_ReportData_134) Marshal.PtrToStructure(from, typeof(__MotionInfo_Vehicle_Inter_Comm_ReportData_134));
            CopyOut(ref nativeImg, ref to);
        }

        public static void StaticCopyOut(System.IntPtr from, ref MotionInfo_Vehicle_Inter_Comm_ReportData_134 to)
        {
            __MotionInfo_Vehicle_Inter_Comm_ReportData_134 nativeImg = (__MotionInfo_Vehicle_Inter_Comm_ReportData_134) Marshal.PtrToStructure(from, typeof(__MotionInfo_Vehicle_Inter_Comm_ReportData_134));
            CopyOut(ref nativeImg, ref to);
        }

        public static void CopyOut(ref __MotionInfo_Vehicle_Inter_Comm_ReportData_134 from, ref MotionInfo_Vehicle_Inter_Comm_ReportData_134 to)
        {
            if (to == null) {
                to = new MotionInfo_Vehicle_Inter_Comm_ReportData_134();
            }
            to.vehID = from.vehID;
            to.vehName = ReadString(from.vehName);
            Veh_HandShakeData.MotionInfo_LoadMarshaler.CopyOut(ref from.loadStatus, ref to.loadStatus);
            Veh_HandShakeData.MotionInfo_UnLoadMarshaler.CopyOut(ref from.unLoadStatus, ref to.unLoadStatus);
            Veh_HandShakeData.MotionInfo_BlockSectionPassReqstMarshaler.CopyOut(ref from.BlockSectionPassReqst, ref to.BlockSectionPassReqst);
            Veh_HandShakeData.MotionInfo_HIDSectionPassReqstMarshaler.CopyOut(ref from.HIDSectionPassReqst, ref to.HIDSectionPassReqst);
            Veh_HandShakeData.MotionInfo_ReserveSectionPassReqstMarshaler.CopyOut(ref from.ReserveSectionPassReqst, ref to.ReserveSectionPassReqst);
            to.WheelAngle = (Veh_HandShakeData.VehWheelSteeringAngle) from.WheelAngle;
            to.ConrtolMode = (Veh_HandShakeData.VehControlMode) from.ConrtolMode;
            to.WhichType = from.WhichType;
            to.LocationType = from.LocationType;
            to.Section = ReadString(from.Section);
            to.Address = ReadString(from.Address);
            to.Stage = ReadString(from.Stage);
            to.DistanceFromSectionStart = from.DistanceFromSectionStart;
            to.WalkLength = from.WalkLength;
            to.PowerConsume = from.PowerConsume;
            to.Guiding = from.Guiding;
            to.ReserveSection = ReadString(from.ReserveSection);
            to.BlockControlSection = ReadString(from.BlockControlSection);
            to.HIDControlSection = ReadString(from.HIDControlSection);
            to.Proc_ON = from.Proc_ON;
            to.cmd_Send = from.cmd_Send;
            to.cmd_Receive = from.cmd_Receive;
            to.cmpCode = from.cmpCode;
            to.cmpStatus = from.cmpStatus;
            to.stopStatusForEvent = from.stopStatusForEvent;
            to.vehModeStatus = from.vehModeStatus;
            to.vehActionStatus = from.vehActionStatus;
            to.eventTypes = from.eventTypes;
            to.actionType = from.actionType;
            to.vehLeftGuideLockStatus = from.vehLeftGuideLockStatus;
            to.vehRightGuideLockStatus = from.vehRightGuideLockStatus;
            to.vehPauseStatus = from.vehPauseStatus;
            to.vehBlockStopStatus = from.vehBlockStopStatus;
            to.vehReserveStopStatus = from.vehReserveStopStatus;
            to.vehHIDStopStatus = from.vehHIDStopStatus;
            to.vehObstacleStopStatus = from.vehObstacleStopStatus;
            to.vehBlockSectionPassReqst = from.vehBlockSectionPassReqst;
            to.vehHIDSectionPassReqst = from.vehHIDSectionPassReqst;
            to.vehReserveSectionPassReqst = from.vehReserveSectionPassReqst;
            to.locationTypes = from.locationTypes;
            to.vehLoadStatus = from.vehLoadStatus;
            to.vehObstDist = from.vehObstDist;
            to.vehPowerStatus = from.vehPowerStatus;
            to.ChargeStatus = from.ChargeStatus;
            to.BatteryCapacity = from.BatteryCapacity;
            to.BatteryTemperature = from.BatteryTemperature;
            to.ErrorCode = from.ErrorCode;
            to.ErrorStatus = from.ErrorStatus;
        }

    }
    #endregion

    #region __MotionInfo_Vehicle_Inter_Comm_ReportData
    [StructLayout(LayoutKind.Sequential)]
    public struct __MotionInfo_Vehicle_Inter_Comm_ReportData
    {
        public int vehID;
        public IntPtr vehName;
        public Veh_HandShakeData.__MotionInfo_Load loadStatus;
        public Veh_HandShakeData.__MotionInfo_UnLoad unLoadStatus;
        public Veh_HandShakeData.__MotionInfo_BlockSectionPassReqst BlockSectionPassReqst;
        public Veh_HandShakeData.__MotionInfo_HIDSectionPassReqst HIDSectionPassReqst;
        public Veh_HandShakeData.__MotionInfo_ReserveSectionPassReqst ReserveSectionPassReqst;
        public uint WheelAngle;
        public uint ConrtolMode;
        public int WhichType;
        public int LocationType;
        public IntPtr Section;
        public IntPtr Address;
        public IntPtr Stage;
        public double DistanceFromSectionStart;
        public double WalkLength;
        public double PowerConsume;
        public int Guiding;
        public IntPtr ReserveSection;
        public IntPtr BlockControlSection;
        public IntPtr HIDControlSection;
        public int Proc_ON;
        public int cmd_Send;
        public int cmd_Receive;
        public int cmpCode;
        public int cmpStatus;
        public int stopStatusForEvent;
        public int vehModeStatus;
        public int vehActionStatus;
        public int eventTypes;
        public int actionType;
        public int vehLeftGuideLockStatus;
        public int vehRightGuideLockStatus;
        public int vehPauseStatus;
        public int vehBlockStopStatus;
        public int vehReserveStopStatus;
        public int vehHIDStopStatus;
        public int vehObstacleStopStatus;
        public int vehBlockSectionPassReqst;
        public int vehHIDSectionPassReqst;
        public int vehReserveSectionPassReqst;
        public int locationTypes;
        public int vehLoadStatus;
        public int vehObstDist;
        public int vehPowerStatus;
        public int ChargeStatus;
        public int BatteryCapacity;
        public int BatteryTemperature;
        public int ErrorCode;
        public int ErrorStatus;
    }
    #endregion

    #region MotionInfo_Vehicle_Inter_Comm_ReportDataMarshaler
    public sealed class MotionInfo_Vehicle_Inter_Comm_ReportDataMarshaler : DDS.OpenSplice.CustomMarshalers.FooDatabaseMarshaler<MotionInfo_Vehicle_Inter_Comm_ReportData>
    {
        public static readonly string fullyScopedName = "Veh_HandShakeData::MotionInfo_Vehicle_Inter_Comm_ReportData";
        private Veh_HandShakeData.MotionInfo_LoadMarshaler attr2Marshaler;
        private Veh_HandShakeData.MotionInfo_UnLoadMarshaler attr3Marshaler;
        private Veh_HandShakeData.MotionInfo_BlockSectionPassReqstMarshaler attr4Marshaler;
        private Veh_HandShakeData.MotionInfo_HIDSectionPassReqstMarshaler attr5Marshaler;
        private Veh_HandShakeData.MotionInfo_ReserveSectionPassReqstMarshaler attr6Marshaler;

        public override void InitEmbeddedMarshalers(IDomainParticipant participant)
        {
            if (attr2Marshaler == null) {
                attr2Marshaler = DatabaseMarshaler.GetMarshaler(participant, typeof(Veh_HandShakeData.MotionInfo_Load)) as Veh_HandShakeData.MotionInfo_LoadMarshaler;
                if (attr2Marshaler == null) {
                    attr2Marshaler = new Veh_HandShakeData.MotionInfo_LoadMarshaler();
                    DatabaseMarshaler.Add(participant, typeof(Veh_HandShakeData.MotionInfo_Load), attr2Marshaler);
                    attr2Marshaler.InitEmbeddedMarshalers(participant);
                }
            }
            if (attr3Marshaler == null) {
                attr3Marshaler = DatabaseMarshaler.GetMarshaler(participant, typeof(Veh_HandShakeData.MotionInfo_UnLoad)) as Veh_HandShakeData.MotionInfo_UnLoadMarshaler;
                if (attr3Marshaler == null) {
                    attr3Marshaler = new Veh_HandShakeData.MotionInfo_UnLoadMarshaler();
                    DatabaseMarshaler.Add(participant, typeof(Veh_HandShakeData.MotionInfo_UnLoad), attr3Marshaler);
                    attr3Marshaler.InitEmbeddedMarshalers(participant);
                }
            }
            if (attr4Marshaler == null) {
                attr4Marshaler = DatabaseMarshaler.GetMarshaler(participant, typeof(Veh_HandShakeData.MotionInfo_BlockSectionPassReqst)) as Veh_HandShakeData.MotionInfo_BlockSectionPassReqstMarshaler;
                if (attr4Marshaler == null) {
                    attr4Marshaler = new Veh_HandShakeData.MotionInfo_BlockSectionPassReqstMarshaler();
                    DatabaseMarshaler.Add(participant, typeof(Veh_HandShakeData.MotionInfo_BlockSectionPassReqst), attr4Marshaler);
                    attr4Marshaler.InitEmbeddedMarshalers(participant);
                }
            }
            if (attr5Marshaler == null) {
                attr5Marshaler = DatabaseMarshaler.GetMarshaler(participant, typeof(Veh_HandShakeData.MotionInfo_HIDSectionPassReqst)) as Veh_HandShakeData.MotionInfo_HIDSectionPassReqstMarshaler;
                if (attr5Marshaler == null) {
                    attr5Marshaler = new Veh_HandShakeData.MotionInfo_HIDSectionPassReqstMarshaler();
                    DatabaseMarshaler.Add(participant, typeof(Veh_HandShakeData.MotionInfo_HIDSectionPassReqst), attr5Marshaler);
                    attr5Marshaler.InitEmbeddedMarshalers(participant);
                }
            }
            if (attr6Marshaler == null) {
                attr6Marshaler = DatabaseMarshaler.GetMarshaler(participant, typeof(Veh_HandShakeData.MotionInfo_ReserveSectionPassReqst)) as Veh_HandShakeData.MotionInfo_ReserveSectionPassReqstMarshaler;
                if (attr6Marshaler == null) {
                    attr6Marshaler = new Veh_HandShakeData.MotionInfo_ReserveSectionPassReqstMarshaler();
                    DatabaseMarshaler.Add(participant, typeof(Veh_HandShakeData.MotionInfo_ReserveSectionPassReqst), attr6Marshaler);
                    attr6Marshaler.InitEmbeddedMarshalers(participant);
                }
            }
        }

        public override V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, System.IntPtr from, System.IntPtr to)
        {
            GCHandle tmpGCHandle = GCHandle.FromIntPtr(from);
            MotionInfo_Vehicle_Inter_Comm_ReportData fromData = tmpGCHandle.Target as MotionInfo_Vehicle_Inter_Comm_ReportData;
            return CopyIn(typePtr, fromData, to);
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_Vehicle_Inter_Comm_ReportData from, System.IntPtr to)
        {
            __MotionInfo_Vehicle_Inter_Comm_ReportData nativeImg = new __MotionInfo_Vehicle_Inter_Comm_ReportData();
            V_COPYIN_RESULT result = CopyIn(typePtr, from, ref nativeImg);
            if (result == V_COPYIN_RESULT.OK)
            {
                Marshal.StructureToPtr(nativeImg, to, false);
            }
            return result;
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_Vehicle_Inter_Comm_ReportData from, ref __MotionInfo_Vehicle_Inter_Comm_ReportData to)
        {
            if (from == null) return V_COPYIN_RESULT.INVALID;
            to.vehID = from.vehID;
            if (from.vehName == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.vehName, from.vehName)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            {
                V_COPYIN_RESULT result = attr2Marshaler.CopyIn(typePtr, from.loadStatus, ref to.loadStatus);
                if (result != V_COPYIN_RESULT.OK) return result;
            }
            {
                V_COPYIN_RESULT result = attr3Marshaler.CopyIn(typePtr, from.unLoadStatus, ref to.unLoadStatus);
                if (result != V_COPYIN_RESULT.OK) return result;
            }
            {
                V_COPYIN_RESULT result = attr4Marshaler.CopyIn(typePtr, from.BlockSectionPassReqst, ref to.BlockSectionPassReqst);
                if (result != V_COPYIN_RESULT.OK) return result;
            }
            {
                V_COPYIN_RESULT result = attr5Marshaler.CopyIn(typePtr, from.HIDSectionPassReqst, ref to.HIDSectionPassReqst);
                if (result != V_COPYIN_RESULT.OK) return result;
            }
            {
                V_COPYIN_RESULT result = attr6Marshaler.CopyIn(typePtr, from.ReserveSectionPassReqst, ref to.ReserveSectionPassReqst);
                if (result != V_COPYIN_RESULT.OK) return result;
            }
            to.WheelAngle = (uint) from.WheelAngle;
            to.ConrtolMode = (uint) from.ConrtolMode;
            to.WhichType = from.WhichType;
            to.LocationType = from.LocationType;
            if (from.Section == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Section, from.Section)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.Address == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Address, from.Address)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.Stage == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Stage, from.Stage)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.DistanceFromSectionStart = from.DistanceFromSectionStart;
            to.WalkLength = from.WalkLength;
            to.PowerConsume = from.PowerConsume;
            to.Guiding = from.Guiding;
            if (from.ReserveSection == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.ReserveSection, from.ReserveSection)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.BlockControlSection == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.BlockControlSection, from.BlockControlSection)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.HIDControlSection == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.HIDControlSection, from.HIDControlSection)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.Proc_ON = from.Proc_ON;
            to.cmd_Send = from.cmd_Send;
            to.cmd_Receive = from.cmd_Receive;
            to.cmpCode = from.cmpCode;
            to.cmpStatus = from.cmpStatus;
            to.stopStatusForEvent = from.stopStatusForEvent;
            to.vehModeStatus = from.vehModeStatus;
            to.vehActionStatus = from.vehActionStatus;
            to.eventTypes = from.eventTypes;
            to.actionType = from.actionType;
            to.vehLeftGuideLockStatus = from.vehLeftGuideLockStatus;
            to.vehRightGuideLockStatus = from.vehRightGuideLockStatus;
            to.vehPauseStatus = from.vehPauseStatus;
            to.vehBlockStopStatus = from.vehBlockStopStatus;
            to.vehReserveStopStatus = from.vehReserveStopStatus;
            to.vehHIDStopStatus = from.vehHIDStopStatus;
            to.vehObstacleStopStatus = from.vehObstacleStopStatus;
            to.vehBlockSectionPassReqst = from.vehBlockSectionPassReqst;
            to.vehHIDSectionPassReqst = from.vehHIDSectionPassReqst;
            to.vehReserveSectionPassReqst = from.vehReserveSectionPassReqst;
            to.locationTypes = from.locationTypes;
            to.vehLoadStatus = from.vehLoadStatus;
            to.vehObstDist = from.vehObstDist;
            to.vehPowerStatus = from.vehPowerStatus;
            to.ChargeStatus = from.ChargeStatus;
            to.BatteryCapacity = from.BatteryCapacity;
            to.BatteryTemperature = from.BatteryTemperature;
            to.ErrorCode = from.ErrorCode;
            to.ErrorStatus = from.ErrorStatus;
            return V_COPYIN_RESULT.OK;
        }

        public override void CopyOut(System.IntPtr from, System.IntPtr to)
        {
            __MotionInfo_Vehicle_Inter_Comm_ReportData nativeImg = (__MotionInfo_Vehicle_Inter_Comm_ReportData) Marshal.PtrToStructure(from, typeof(__MotionInfo_Vehicle_Inter_Comm_ReportData));
            GCHandle tmpGCHandleTo = GCHandle.FromIntPtr(to);
            MotionInfo_Vehicle_Inter_Comm_ReportData toObj = tmpGCHandleTo.Target as MotionInfo_Vehicle_Inter_Comm_ReportData;
            CopyOut(ref nativeImg, ref toObj);
            tmpGCHandleTo.Target = toObj;
        }

        public override void CopyOut(System.IntPtr from, ref MotionInfo_Vehicle_Inter_Comm_ReportData to)
        {
            __MotionInfo_Vehicle_Inter_Comm_ReportData nativeImg = (__MotionInfo_Vehicle_Inter_Comm_ReportData) Marshal.PtrToStructure(from, typeof(__MotionInfo_Vehicle_Inter_Comm_ReportData));
            CopyOut(ref nativeImg, ref to);
        }

        public static void StaticCopyOut(System.IntPtr from, ref MotionInfo_Vehicle_Inter_Comm_ReportData to)
        {
            __MotionInfo_Vehicle_Inter_Comm_ReportData nativeImg = (__MotionInfo_Vehicle_Inter_Comm_ReportData) Marshal.PtrToStructure(from, typeof(__MotionInfo_Vehicle_Inter_Comm_ReportData));
            CopyOut(ref nativeImg, ref to);
        }

        public static void CopyOut(ref __MotionInfo_Vehicle_Inter_Comm_ReportData from, ref MotionInfo_Vehicle_Inter_Comm_ReportData to)
        {
            if (to == null) {
                to = new MotionInfo_Vehicle_Inter_Comm_ReportData();
            }
            to.vehID = from.vehID;
            to.vehName = ReadString(from.vehName);
            Veh_HandShakeData.MotionInfo_LoadMarshaler.CopyOut(ref from.loadStatus, ref to.loadStatus);
            Veh_HandShakeData.MotionInfo_UnLoadMarshaler.CopyOut(ref from.unLoadStatus, ref to.unLoadStatus);
            Veh_HandShakeData.MotionInfo_BlockSectionPassReqstMarshaler.CopyOut(ref from.BlockSectionPassReqst, ref to.BlockSectionPassReqst);
            Veh_HandShakeData.MotionInfo_HIDSectionPassReqstMarshaler.CopyOut(ref from.HIDSectionPassReqst, ref to.HIDSectionPassReqst);
            Veh_HandShakeData.MotionInfo_ReserveSectionPassReqstMarshaler.CopyOut(ref from.ReserveSectionPassReqst, ref to.ReserveSectionPassReqst);
            to.WheelAngle = (Veh_HandShakeData.VehWheelSteeringAngle) from.WheelAngle;
            to.ConrtolMode = (Veh_HandShakeData.VehControlMode) from.ConrtolMode;
            to.WhichType = from.WhichType;
            to.LocationType = from.LocationType;
            to.Section = ReadString(from.Section);
            to.Address = ReadString(from.Address);
            to.Stage = ReadString(from.Stage);
            to.DistanceFromSectionStart = from.DistanceFromSectionStart;
            to.WalkLength = from.WalkLength;
            to.PowerConsume = from.PowerConsume;
            to.Guiding = from.Guiding;
            to.ReserveSection = ReadString(from.ReserveSection);
            to.BlockControlSection = ReadString(from.BlockControlSection);
            to.HIDControlSection = ReadString(from.HIDControlSection);
            to.Proc_ON = from.Proc_ON;
            to.cmd_Send = from.cmd_Send;
            to.cmd_Receive = from.cmd_Receive;
            to.cmpCode = from.cmpCode;
            to.cmpStatus = from.cmpStatus;
            to.stopStatusForEvent = from.stopStatusForEvent;
            to.vehModeStatus = from.vehModeStatus;
            to.vehActionStatus = from.vehActionStatus;
            to.eventTypes = from.eventTypes;
            to.actionType = from.actionType;
            to.vehLeftGuideLockStatus = from.vehLeftGuideLockStatus;
            to.vehRightGuideLockStatus = from.vehRightGuideLockStatus;
            to.vehPauseStatus = from.vehPauseStatus;
            to.vehBlockStopStatus = from.vehBlockStopStatus;
            to.vehReserveStopStatus = from.vehReserveStopStatus;
            to.vehHIDStopStatus = from.vehHIDStopStatus;
            to.vehObstacleStopStatus = from.vehObstacleStopStatus;
            to.vehBlockSectionPassReqst = from.vehBlockSectionPassReqst;
            to.vehHIDSectionPassReqst = from.vehHIDSectionPassReqst;
            to.vehReserveSectionPassReqst = from.vehReserveSectionPassReqst;
            to.locationTypes = from.locationTypes;
            to.vehLoadStatus = from.vehLoadStatus;
            to.vehObstDist = from.vehObstDist;
            to.vehPowerStatus = from.vehPowerStatus;
            to.ChargeStatus = from.ChargeStatus;
            to.BatteryCapacity = from.BatteryCapacity;
            to.BatteryTemperature = from.BatteryTemperature;
            to.ErrorCode = from.ErrorCode;
            to.ErrorStatus = from.ErrorStatus;
        }

    }
    #endregion

    #region __MotionInfo_HandShake_SendData
    [StructLayout(LayoutKind.Sequential)]
    public struct __MotionInfo_HandShake_SendData
    {
        public int vehID;
        public IntPtr vehName;
        public int cmdSend;
        public int cmdReceive;
    }
    #endregion

    #region MotionInfo_HandShake_SendDataMarshaler
    public sealed class MotionInfo_HandShake_SendDataMarshaler : DDS.OpenSplice.CustomMarshalers.FooDatabaseMarshaler<MotionInfo_HandShake_SendData>
    {
        public static readonly string fullyScopedName = "Veh_HandShakeData::MotionInfo_HandShake_SendData";

        public override void InitEmbeddedMarshalers(IDomainParticipant participant)
        {
        }

        public override V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, System.IntPtr from, System.IntPtr to)
        {
            GCHandle tmpGCHandle = GCHandle.FromIntPtr(from);
            MotionInfo_HandShake_SendData fromData = tmpGCHandle.Target as MotionInfo_HandShake_SendData;
            return CopyIn(typePtr, fromData, to);
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_HandShake_SendData from, System.IntPtr to)
        {
            __MotionInfo_HandShake_SendData nativeImg = new __MotionInfo_HandShake_SendData();
            V_COPYIN_RESULT result = CopyIn(typePtr, from, ref nativeImg);
            if (result == V_COPYIN_RESULT.OK)
            {
                Marshal.StructureToPtr(nativeImg, to, false);
            }
            return result;
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_HandShake_SendData from, ref __MotionInfo_HandShake_SendData to)
        {
            if (from == null) return V_COPYIN_RESULT.INVALID;
            to.vehID = from.vehID;
            if (from.vehName == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.vehName, from.vehName)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.cmdSend = from.cmdSend;
            to.cmdReceive = from.cmdReceive;
            return V_COPYIN_RESULT.OK;
        }

        public override void CopyOut(System.IntPtr from, System.IntPtr to)
        {
            __MotionInfo_HandShake_SendData nativeImg = (__MotionInfo_HandShake_SendData) Marshal.PtrToStructure(from, typeof(__MotionInfo_HandShake_SendData));
            GCHandle tmpGCHandleTo = GCHandle.FromIntPtr(to);
            MotionInfo_HandShake_SendData toObj = tmpGCHandleTo.Target as MotionInfo_HandShake_SendData;
            CopyOut(ref nativeImg, ref toObj);
            tmpGCHandleTo.Target = toObj;
        }

        public override void CopyOut(System.IntPtr from, ref MotionInfo_HandShake_SendData to)
        {
            __MotionInfo_HandShake_SendData nativeImg = (__MotionInfo_HandShake_SendData) Marshal.PtrToStructure(from, typeof(__MotionInfo_HandShake_SendData));
            CopyOut(ref nativeImg, ref to);
        }

        public static void StaticCopyOut(System.IntPtr from, ref MotionInfo_HandShake_SendData to)
        {
            __MotionInfo_HandShake_SendData nativeImg = (__MotionInfo_HandShake_SendData) Marshal.PtrToStructure(from, typeof(__MotionInfo_HandShake_SendData));
            CopyOut(ref nativeImg, ref to);
        }

        public static void CopyOut(ref __MotionInfo_HandShake_SendData from, ref MotionInfo_HandShake_SendData to)
        {
            if (to == null) {
                to = new MotionInfo_HandShake_SendData();
            }
            to.vehID = from.vehID;
            to.vehName = ReadString(from.vehName);
            to.cmdSend = from.cmdSend;
            to.cmdReceive = from.cmdReceive;
        }

    }
    #endregion

    #region __MotionInfo_HandShake_RecieveData
    [StructLayout(LayoutKind.Sequential)]
    public struct __MotionInfo_HandShake_RecieveData
    {
        public int vehID;
        public IntPtr vehName;
        public int cmdSend;
        public int cmdReceive;
    }
    #endregion

    #region MotionInfo_HandShake_RecieveDataMarshaler
    public sealed class MotionInfo_HandShake_RecieveDataMarshaler : DDS.OpenSplice.CustomMarshalers.FooDatabaseMarshaler<MotionInfo_HandShake_RecieveData>
    {
        public static readonly string fullyScopedName = "Veh_HandShakeData::MotionInfo_HandShake_RecieveData";

        public override void InitEmbeddedMarshalers(IDomainParticipant participant)
        {
        }

        public override V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, System.IntPtr from, System.IntPtr to)
        {
            GCHandle tmpGCHandle = GCHandle.FromIntPtr(from);
            MotionInfo_HandShake_RecieveData fromData = tmpGCHandle.Target as MotionInfo_HandShake_RecieveData;
            return CopyIn(typePtr, fromData, to);
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_HandShake_RecieveData from, System.IntPtr to)
        {
            __MotionInfo_HandShake_RecieveData nativeImg = new __MotionInfo_HandShake_RecieveData();
            V_COPYIN_RESULT result = CopyIn(typePtr, from, ref nativeImg);
            if (result == V_COPYIN_RESULT.OK)
            {
                Marshal.StructureToPtr(nativeImg, to, false);
            }
            return result;
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, MotionInfo_HandShake_RecieveData from, ref __MotionInfo_HandShake_RecieveData to)
        {
            if (from == null) return V_COPYIN_RESULT.INVALID;
            to.vehID = from.vehID;
            if (from.vehName == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.vehName, from.vehName)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.cmdSend = from.cmdSend;
            to.cmdReceive = from.cmdReceive;
            return V_COPYIN_RESULT.OK;
        }

        public override void CopyOut(System.IntPtr from, System.IntPtr to)
        {
            __MotionInfo_HandShake_RecieveData nativeImg = (__MotionInfo_HandShake_RecieveData) Marshal.PtrToStructure(from, typeof(__MotionInfo_HandShake_RecieveData));
            GCHandle tmpGCHandleTo = GCHandle.FromIntPtr(to);
            MotionInfo_HandShake_RecieveData toObj = tmpGCHandleTo.Target as MotionInfo_HandShake_RecieveData;
            CopyOut(ref nativeImg, ref toObj);
            tmpGCHandleTo.Target = toObj;
        }

        public override void CopyOut(System.IntPtr from, ref MotionInfo_HandShake_RecieveData to)
        {
            __MotionInfo_HandShake_RecieveData nativeImg = (__MotionInfo_HandShake_RecieveData) Marshal.PtrToStructure(from, typeof(__MotionInfo_HandShake_RecieveData));
            CopyOut(ref nativeImg, ref to);
        }

        public static void StaticCopyOut(System.IntPtr from, ref MotionInfo_HandShake_RecieveData to)
        {
            __MotionInfo_HandShake_RecieveData nativeImg = (__MotionInfo_HandShake_RecieveData) Marshal.PtrToStructure(from, typeof(__MotionInfo_HandShake_RecieveData));
            CopyOut(ref nativeImg, ref to);
        }

        public static void CopyOut(ref __MotionInfo_HandShake_RecieveData from, ref MotionInfo_HandShake_RecieveData to)
        {
            if (to == null) {
                to = new MotionInfo_HandShake_RecieveData();
            }
            to.vehID = from.vehID;
            to.vehName = ReadString(from.vehName);
            to.cmdSend = from.cmdSend;
            to.cmdReceive = from.cmdReceive;
        }

    }
    #endregion

    #region __Between_Vehicle_Data
    [StructLayout(LayoutKind.Sequential)]
    public struct __Between_Vehicle_Data
    {
        public IntPtr Vehicle_ID;
        public byte InService;
        public byte EQ_Online;
        public byte EQ_Error;
        public byte IsMoving;
        public byte IsHoisting;
        public IntPtr Current_Zone_ID;
        public IntPtr Current_Zone_ID2;
        public IntPtr Current_Zone_ID3;
        public IntPtr Current_Section_ID;
        public int Current_Section_Offset;
        public double Current_Map_HeadingPose_Angle;
        public int Current_Map_AbsPos_X;
        public int Current_Map_AbsPos_Y;
        public IntPtr Current_Address_ID;
        public IntPtr Current_Stage_ID;
        public IntPtr BlockQry_Zone_ID_1;
        public IntPtr BlockQry_Zone_ID_2;
        public IntPtr BlockQry_Zone_ID_3;
        public IntPtr BlockQry_Zone_ID_4;
        public IntPtr BlockQry_Zone_ID_5;
        public IntPtr BlockQry_Zone_ID_6;
        public IntPtr BlockQry_Zone_ID_7;
        public IntPtr BlockQry_Zone_ID_8;
        public IntPtr BlockQry_Zone_ID_9;
        public IntPtr Blocking_Zone_Owner;
        public IntPtr Blocking_Zone_ID;
        public int Blocking_ZoneEntry_Distance;
        public IntPtr UniqueSections_FromNow2JustB4QryBlockZoneExit;
        public IntPtr UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraSplit;
        public IntPtr UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraMerge;
        public IntPtr UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraNearby;
        public IntPtr UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraTwoWay;
        public int Status1;
        public int Status2;
        public IntPtr Start_Time;
        public IntPtr Software_Version;
        public IntPtr Updating_Timestamp;
    }
    #endregion

    #region Between_Vehicle_DataMarshaler
    public sealed class Between_Vehicle_DataMarshaler : DDS.OpenSplice.CustomMarshalers.FooDatabaseMarshaler<Between_Vehicle_Data>
    {
        public static readonly string fullyScopedName = "Veh_HandShakeData::Between_Vehicle_Data";
        private IntPtr attr28Seq0Type = IntPtr.Zero;
        private static readonly int attr28Seq0Size = 1 * Marshal.SizeOf(typeof(IntPtr));
        private IntPtr attr29Seq0Type = IntPtr.Zero;
        private static readonly int attr29Seq0Size = 1 * Marshal.SizeOf(typeof(IntPtr));
        private IntPtr attr30Seq0Type = IntPtr.Zero;
        private static readonly int attr30Seq0Size = 1 * Marshal.SizeOf(typeof(IntPtr));
        private IntPtr attr31Seq0Type = IntPtr.Zero;
        private static readonly int attr31Seq0Size = 1 * Marshal.SizeOf(typeof(IntPtr));
        private IntPtr attr32Seq0Type = IntPtr.Zero;
        private static readonly int attr32Seq0Size = 1 * Marshal.SizeOf(typeof(IntPtr));

        public override void InitEmbeddedMarshalers(IDomainParticipant participant)
        {
        }

        public override V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, System.IntPtr from, System.IntPtr to)
        {
            GCHandle tmpGCHandle = GCHandle.FromIntPtr(from);
            Between_Vehicle_Data fromData = tmpGCHandle.Target as Between_Vehicle_Data;
            return CopyIn(typePtr, fromData, to);
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, Between_Vehicle_Data from, System.IntPtr to)
        {
            __Between_Vehicle_Data nativeImg = new __Between_Vehicle_Data();
            V_COPYIN_RESULT result = CopyIn(typePtr, from, ref nativeImg);
            if (result == V_COPYIN_RESULT.OK)
            {
                Marshal.StructureToPtr(nativeImg, to, false);
            }
            return result;
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, Between_Vehicle_Data from, ref __Between_Vehicle_Data to)
        {
            if (from == null) return V_COPYIN_RESULT.INVALID;
            if (from.Vehicle_ID == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Vehicle_ID, from.Vehicle_ID)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.InService = from.InService ? (byte) 1 : (byte) 0;
            to.EQ_Online = from.EQ_Online ? (byte) 1 : (byte) 0;
            to.EQ_Error = from.EQ_Error ? (byte) 1 : (byte) 0;
            to.IsMoving = from.IsMoving ? (byte) 1 : (byte) 0;
            to.IsHoisting = from.IsHoisting ? (byte) 1 : (byte) 0;
            if (from.Current_Zone_ID == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Current_Zone_ID, from.Current_Zone_ID)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.Current_Zone_ID2 == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Current_Zone_ID2, from.Current_Zone_ID2)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.Current_Zone_ID3 == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Current_Zone_ID3, from.Current_Zone_ID3)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.Current_Section_ID == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Current_Section_ID, from.Current_Section_ID)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.Current_Section_Offset = from.Current_Section_Offset;
            to.Current_Map_HeadingPose_Angle = from.Current_Map_HeadingPose_Angle;
            to.Current_Map_AbsPos_X = from.Current_Map_AbsPos_X;
            to.Current_Map_AbsPos_Y = from.Current_Map_AbsPos_Y;
            if (from.Current_Address_ID == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Current_Address_ID, from.Current_Address_ID)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.Current_Stage_ID == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Current_Stage_ID, from.Current_Stage_ID)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.BlockQry_Zone_ID_1 == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.BlockQry_Zone_ID_1, from.BlockQry_Zone_ID_1)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.BlockQry_Zone_ID_2 == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.BlockQry_Zone_ID_2, from.BlockQry_Zone_ID_2)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.BlockQry_Zone_ID_3 == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.BlockQry_Zone_ID_3, from.BlockQry_Zone_ID_3)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.BlockQry_Zone_ID_4 == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.BlockQry_Zone_ID_4, from.BlockQry_Zone_ID_4)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.BlockQry_Zone_ID_5 == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.BlockQry_Zone_ID_5, from.BlockQry_Zone_ID_5)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.BlockQry_Zone_ID_6 == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.BlockQry_Zone_ID_6, from.BlockQry_Zone_ID_6)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.BlockQry_Zone_ID_7 == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.BlockQry_Zone_ID_7, from.BlockQry_Zone_ID_7)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.BlockQry_Zone_ID_8 == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.BlockQry_Zone_ID_8, from.BlockQry_Zone_ID_8)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.BlockQry_Zone_ID_9 == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.BlockQry_Zone_ID_9, from.BlockQry_Zone_ID_9)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.Blocking_Zone_Owner == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Blocking_Zone_Owner, from.Blocking_Zone_Owner)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.Blocking_Zone_ID == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Blocking_Zone_ID, from.Blocking_Zone_ID)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.Blocking_ZoneEntry_Distance = from.Blocking_ZoneEntry_Distance;
            if (from.UniqueSections_FromNow2JustB4QryBlockZoneExit == null) return V_COPYIN_RESULT.INVALID;
            int attr28Seq0Length = from.UniqueSections_FromNow2JustB4QryBlockZoneExit.Length;
            // Unbounded sequence: bounds check not required...
            if (attr28Seq0Type == IntPtr.Zero) {
                IntPtr memberOwnerType = DDS.OpenSplice.Database.c.resolve(c.getBase(typePtr), fullyScopedName);
                IntPtr specifier = DDS.OpenSplice.Database.c.metaResolveSpecifier(memberOwnerType, "UniqueSections_FromNow2JustB4QryBlockZoneExit");
                IntPtr specifierType = DDS.OpenSplice.Database.c.specifierType(specifier);
                attr28Seq0Type = DDS.OpenSplice.Database.c.typeActualType(specifierType);
            }
            IntPtr attr28Seq0Buf = DDS.OpenSplice.Database.c.newSequence(attr28Seq0Type, attr28Seq0Length);
            if (attr28Seq0Buf == IntPtr.Zero) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            for (int i0 = 0; i0 < attr28Seq0Length; i0++) {
                if (from.UniqueSections_FromNow2JustB4QryBlockZoneExit[i0] == null) return V_COPYIN_RESULT.INVALID;
                // Unbounded string: bounds check not required...
                IntPtr stringElementPtr = IntPtr.Zero;
                if (!Write(c.getBase(typePtr), ref stringElementPtr, from.UniqueSections_FromNow2JustB4QryBlockZoneExit[i0])) return V_COPYIN_RESULT.OUT_OF_MEMORY;
                Marshal.WriteIntPtr(attr28Seq0Buf, stringElementPtr);
                attr28Seq0Buf = new IntPtr(attr28Seq0Buf.ToInt64() + attr28Seq0Size);
            }
            to.UniqueSections_FromNow2JustB4QryBlockZoneExit = new IntPtr(attr28Seq0Buf.ToInt64() - ((long) attr28Seq0Size * (long) attr28Seq0Length));
            if (from.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraSplit == null) return V_COPYIN_RESULT.INVALID;
            int attr29Seq0Length = from.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraSplit.Length;
            // Unbounded sequence: bounds check not required...
            if (attr29Seq0Type == IntPtr.Zero) {
                IntPtr memberOwnerType = DDS.OpenSplice.Database.c.resolve(c.getBase(typePtr), fullyScopedName);
                IntPtr specifier = DDS.OpenSplice.Database.c.metaResolveSpecifier(memberOwnerType, "UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraSplit");
                IntPtr specifierType = DDS.OpenSplice.Database.c.specifierType(specifier);
                attr29Seq0Type = DDS.OpenSplice.Database.c.typeActualType(specifierType);
            }
            IntPtr attr29Seq0Buf = DDS.OpenSplice.Database.c.newSequence(attr29Seq0Type, attr29Seq0Length);
            if (attr29Seq0Buf == IntPtr.Zero) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            for (int i0 = 0; i0 < attr29Seq0Length; i0++) {
                if (from.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraSplit[i0] == null) return V_COPYIN_RESULT.INVALID;
                // Unbounded string: bounds check not required...
                IntPtr stringElementPtr = IntPtr.Zero;
                if (!Write(c.getBase(typePtr), ref stringElementPtr, from.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraSplit[i0])) return V_COPYIN_RESULT.OUT_OF_MEMORY;
                Marshal.WriteIntPtr(attr29Seq0Buf, stringElementPtr);
                attr29Seq0Buf = new IntPtr(attr29Seq0Buf.ToInt64() + attr29Seq0Size);
            }
            to.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraSplit = new IntPtr(attr29Seq0Buf.ToInt64() - ((long) attr29Seq0Size * (long) attr29Seq0Length));
            if (from.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraMerge == null) return V_COPYIN_RESULT.INVALID;
            int attr30Seq0Length = from.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraMerge.Length;
            // Unbounded sequence: bounds check not required...
            if (attr30Seq0Type == IntPtr.Zero) {
                IntPtr memberOwnerType = DDS.OpenSplice.Database.c.resolve(c.getBase(typePtr), fullyScopedName);
                IntPtr specifier = DDS.OpenSplice.Database.c.metaResolveSpecifier(memberOwnerType, "UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraMerge");
                IntPtr specifierType = DDS.OpenSplice.Database.c.specifierType(specifier);
                attr30Seq0Type = DDS.OpenSplice.Database.c.typeActualType(specifierType);
            }
            IntPtr attr30Seq0Buf = DDS.OpenSplice.Database.c.newSequence(attr30Seq0Type, attr30Seq0Length);
            if (attr30Seq0Buf == IntPtr.Zero) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            for (int i0 = 0; i0 < attr30Seq0Length; i0++) {
                if (from.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraMerge[i0] == null) return V_COPYIN_RESULT.INVALID;
                // Unbounded string: bounds check not required...
                IntPtr stringElementPtr = IntPtr.Zero;
                if (!Write(c.getBase(typePtr), ref stringElementPtr, from.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraMerge[i0])) return V_COPYIN_RESULT.OUT_OF_MEMORY;
                Marshal.WriteIntPtr(attr30Seq0Buf, stringElementPtr);
                attr30Seq0Buf = new IntPtr(attr30Seq0Buf.ToInt64() + attr30Seq0Size);
            }
            to.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraMerge = new IntPtr(attr30Seq0Buf.ToInt64() - ((long) attr30Seq0Size * (long) attr30Seq0Length));
            if (from.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraNearby == null) return V_COPYIN_RESULT.INVALID;
            int attr31Seq0Length = from.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraNearby.Length;
            // Unbounded sequence: bounds check not required...
            if (attr31Seq0Type == IntPtr.Zero) {
                IntPtr memberOwnerType = DDS.OpenSplice.Database.c.resolve(c.getBase(typePtr), fullyScopedName);
                IntPtr specifier = DDS.OpenSplice.Database.c.metaResolveSpecifier(memberOwnerType, "UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraNearby");
                IntPtr specifierType = DDS.OpenSplice.Database.c.specifierType(specifier);
                attr31Seq0Type = DDS.OpenSplice.Database.c.typeActualType(specifierType);
            }
            IntPtr attr31Seq0Buf = DDS.OpenSplice.Database.c.newSequence(attr31Seq0Type, attr31Seq0Length);
            if (attr31Seq0Buf == IntPtr.Zero) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            for (int i0 = 0; i0 < attr31Seq0Length; i0++) {
                if (from.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraNearby[i0] == null) return V_COPYIN_RESULT.INVALID;
                // Unbounded string: bounds check not required...
                IntPtr stringElementPtr = IntPtr.Zero;
                if (!Write(c.getBase(typePtr), ref stringElementPtr, from.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraNearby[i0])) return V_COPYIN_RESULT.OUT_OF_MEMORY;
                Marshal.WriteIntPtr(attr31Seq0Buf, stringElementPtr);
                attr31Seq0Buf = new IntPtr(attr31Seq0Buf.ToInt64() + attr31Seq0Size);
            }
            to.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraNearby = new IntPtr(attr31Seq0Buf.ToInt64() - ((long) attr31Seq0Size * (long) attr31Seq0Length));
            if (from.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraTwoWay == null) return V_COPYIN_RESULT.INVALID;
            int attr32Seq0Length = from.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraTwoWay.Length;
            // Unbounded sequence: bounds check not required...
            if (attr32Seq0Type == IntPtr.Zero) {
                IntPtr memberOwnerType = DDS.OpenSplice.Database.c.resolve(c.getBase(typePtr), fullyScopedName);
                IntPtr specifier = DDS.OpenSplice.Database.c.metaResolveSpecifier(memberOwnerType, "UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraTwoWay");
                IntPtr specifierType = DDS.OpenSplice.Database.c.specifierType(specifier);
                attr32Seq0Type = DDS.OpenSplice.Database.c.typeActualType(specifierType);
            }
            IntPtr attr32Seq0Buf = DDS.OpenSplice.Database.c.newSequence(attr32Seq0Type, attr32Seq0Length);
            if (attr32Seq0Buf == IntPtr.Zero) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            for (int i0 = 0; i0 < attr32Seq0Length; i0++) {
                if (from.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraTwoWay[i0] == null) return V_COPYIN_RESULT.INVALID;
                // Unbounded string: bounds check not required...
                IntPtr stringElementPtr = IntPtr.Zero;
                if (!Write(c.getBase(typePtr), ref stringElementPtr, from.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraTwoWay[i0])) return V_COPYIN_RESULT.OUT_OF_MEMORY;
                Marshal.WriteIntPtr(attr32Seq0Buf, stringElementPtr);
                attr32Seq0Buf = new IntPtr(attr32Seq0Buf.ToInt64() + attr32Seq0Size);
            }
            to.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraTwoWay = new IntPtr(attr32Seq0Buf.ToInt64() - ((long) attr32Seq0Size * (long) attr32Seq0Length));
            to.Status1 = from.Status1;
            to.Status2 = from.Status2;
            if (from.Start_Time == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Start_Time, from.Start_Time)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.Software_Version == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Software_Version, from.Software_Version)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.Updating_Timestamp == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Updating_Timestamp, from.Updating_Timestamp)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            return V_COPYIN_RESULT.OK;
        }

        public override void CopyOut(System.IntPtr from, System.IntPtr to)
        {
            __Between_Vehicle_Data nativeImg = (__Between_Vehicle_Data) Marshal.PtrToStructure(from, typeof(__Between_Vehicle_Data));
            GCHandle tmpGCHandleTo = GCHandle.FromIntPtr(to);
            Between_Vehicle_Data toObj = tmpGCHandleTo.Target as Between_Vehicle_Data;
            CopyOut(ref nativeImg, ref toObj);
            tmpGCHandleTo.Target = toObj;
        }

        public override void CopyOut(System.IntPtr from, ref Between_Vehicle_Data to)
        {
            __Between_Vehicle_Data nativeImg = (__Between_Vehicle_Data) Marshal.PtrToStructure(from, typeof(__Between_Vehicle_Data));
            CopyOut(ref nativeImg, ref to);
        }

        public static void StaticCopyOut(System.IntPtr from, ref Between_Vehicle_Data to)
        {
            __Between_Vehicle_Data nativeImg = (__Between_Vehicle_Data) Marshal.PtrToStructure(from, typeof(__Between_Vehicle_Data));
            CopyOut(ref nativeImg, ref to);
        }

        public static void CopyOut(ref __Between_Vehicle_Data from, ref Between_Vehicle_Data to)
        {
            if (to == null) {
                to = new Between_Vehicle_Data();
            }
            to.Vehicle_ID = ReadString(from.Vehicle_ID);
            to.InService = from.InService != 0 ? true : false;
            to.EQ_Online = from.EQ_Online != 0 ? true : false;
            to.EQ_Error = from.EQ_Error != 0 ? true : false;
            to.IsMoving = from.IsMoving != 0 ? true : false;
            to.IsHoisting = from.IsHoisting != 0 ? true : false;
            to.Current_Zone_ID = ReadString(from.Current_Zone_ID);
            to.Current_Zone_ID2 = ReadString(from.Current_Zone_ID2);
            to.Current_Zone_ID3 = ReadString(from.Current_Zone_ID3);
            to.Current_Section_ID = ReadString(from.Current_Section_ID);
            to.Current_Section_Offset = from.Current_Section_Offset;
            to.Current_Map_HeadingPose_Angle = from.Current_Map_HeadingPose_Angle;
            to.Current_Map_AbsPos_X = from.Current_Map_AbsPos_X;
            to.Current_Map_AbsPos_Y = from.Current_Map_AbsPos_Y;
            to.Current_Address_ID = ReadString(from.Current_Address_ID);
            to.Current_Stage_ID = ReadString(from.Current_Stage_ID);
            to.BlockQry_Zone_ID_1 = ReadString(from.BlockQry_Zone_ID_1);
            to.BlockQry_Zone_ID_2 = ReadString(from.BlockQry_Zone_ID_2);
            to.BlockQry_Zone_ID_3 = ReadString(from.BlockQry_Zone_ID_3);
            to.BlockQry_Zone_ID_4 = ReadString(from.BlockQry_Zone_ID_4);
            to.BlockQry_Zone_ID_5 = ReadString(from.BlockQry_Zone_ID_5);
            to.BlockQry_Zone_ID_6 = ReadString(from.BlockQry_Zone_ID_6);
            to.BlockQry_Zone_ID_7 = ReadString(from.BlockQry_Zone_ID_7);
            to.BlockQry_Zone_ID_8 = ReadString(from.BlockQry_Zone_ID_8);
            to.BlockQry_Zone_ID_9 = ReadString(from.BlockQry_Zone_ID_9);
            to.Blocking_Zone_Owner = ReadString(from.Blocking_Zone_Owner);
            to.Blocking_Zone_ID = ReadString(from.Blocking_Zone_ID);
            to.Blocking_ZoneEntry_Distance = from.Blocking_ZoneEntry_Distance;
            IntPtr attr28Seq0Buf = from.UniqueSections_FromNow2JustB4QryBlockZoneExit;
            int attr28Seq0Length = DDS.OpenSplice.Database.c.arraySize(attr28Seq0Buf);
            if (to.UniqueSections_FromNow2JustB4QryBlockZoneExit == null || to.UniqueSections_FromNow2JustB4QryBlockZoneExit.Length != attr28Seq0Length) {
                string[] target = new string[attr28Seq0Length];
                initObjectSeq(to.UniqueSections_FromNow2JustB4QryBlockZoneExit, target);
                to.UniqueSections_FromNow2JustB4QryBlockZoneExit = target;
            }
            for (int i0 = 0; i0 < attr28Seq0Length; i0++) {
                IntPtr stringElementPtr = Marshal.ReadIntPtr(attr28Seq0Buf);
                to.UniqueSections_FromNow2JustB4QryBlockZoneExit[i0] = ReadString(stringElementPtr);
                attr28Seq0Buf = new IntPtr(attr28Seq0Buf.ToInt64() + attr28Seq0Size);
            }
            IntPtr attr29Seq0Buf = from.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraSplit;
            int attr29Seq0Length = DDS.OpenSplice.Database.c.arraySize(attr29Seq0Buf);
            if (to.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraSplit == null || to.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraSplit.Length != attr29Seq0Length) {
                string[] target = new string[attr29Seq0Length];
                initObjectSeq(to.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraSplit, target);
                to.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraSplit = target;
            }
            for (int i0 = 0; i0 < attr29Seq0Length; i0++) {
                IntPtr stringElementPtr = Marshal.ReadIntPtr(attr29Seq0Buf);
                to.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraSplit[i0] = ReadString(stringElementPtr);
                attr29Seq0Buf = new IntPtr(attr29Seq0Buf.ToInt64() + attr29Seq0Size);
            }
            IntPtr attr30Seq0Buf = from.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraMerge;
            int attr30Seq0Length = DDS.OpenSplice.Database.c.arraySize(attr30Seq0Buf);
            if (to.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraMerge == null || to.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraMerge.Length != attr30Seq0Length) {
                string[] target = new string[attr30Seq0Length];
                initObjectSeq(to.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraMerge, target);
                to.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraMerge = target;
            }
            for (int i0 = 0; i0 < attr30Seq0Length; i0++) {
                IntPtr stringElementPtr = Marshal.ReadIntPtr(attr30Seq0Buf);
                to.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraMerge[i0] = ReadString(stringElementPtr);
                attr30Seq0Buf = new IntPtr(attr30Seq0Buf.ToInt64() + attr30Seq0Size);
            }
            IntPtr attr31Seq0Buf = from.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraNearby;
            int attr31Seq0Length = DDS.OpenSplice.Database.c.arraySize(attr31Seq0Buf);
            if (to.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraNearby == null || to.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraNearby.Length != attr31Seq0Length) {
                string[] target = new string[attr31Seq0Length];
                initObjectSeq(to.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraNearby, target);
                to.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraNearby = target;
            }
            for (int i0 = 0; i0 < attr31Seq0Length; i0++) {
                IntPtr stringElementPtr = Marshal.ReadIntPtr(attr31Seq0Buf);
                to.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraNearby[i0] = ReadString(stringElementPtr);
                attr31Seq0Buf = new IntPtr(attr31Seq0Buf.ToInt64() + attr31Seq0Size);
            }
            IntPtr attr32Seq0Buf = from.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraTwoWay;
            int attr32Seq0Length = DDS.OpenSplice.Database.c.arraySize(attr32Seq0Buf);
            if (to.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraTwoWay == null || to.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraTwoWay.Length != attr32Seq0Length) {
                string[] target = new string[attr32Seq0Length];
                initObjectSeq(to.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraTwoWay, target);
                to.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraTwoWay = target;
            }
            for (int i0 = 0; i0 < attr32Seq0Length; i0++) {
                IntPtr stringElementPtr = Marshal.ReadIntPtr(attr32Seq0Buf);
                to.UniqueSections_FromNow2JustB4QryBlockZoneExit_OnlyExtraTwoWay[i0] = ReadString(stringElementPtr);
                attr32Seq0Buf = new IntPtr(attr32Seq0Buf.ToInt64() + attr32Seq0Size);
            }
            to.Status1 = from.Status1;
            to.Status2 = from.Status2;
            to.Start_Time = ReadString(from.Start_Time);
            to.Software_Version = ReadString(from.Software_Version);
            to.Updating_Timestamp = ReadString(from.Updating_Timestamp);
        }

    }
    #endregion

    #region __LoadPort_PIO_FromVehicle
    [StructLayout(LayoutKind.Sequential)]
    public struct __LoadPort_PIO_FromVehicle
    {
        public IntPtr EQ_Name;
        public IntPtr Port_Name;
        public IntPtr PIO_ID;
        public int PIO_Channel;
        public uint PortType;
        public byte InService;
        public byte DO01_VALID;
        public byte DO02_CS_0;
        public byte DO03_CS_1;
        public byte DO04_nil;
        public byte DO05_TR_REQ;
        public byte DO06_BUSY;
        public byte DO07_COMPT;
        public byte DO08_CONT;
        public byte SELECT;
    }
    #endregion

    #region LoadPort_PIO_FromVehicleMarshaler
    public sealed class LoadPort_PIO_FromVehicleMarshaler : DDS.OpenSplice.CustomMarshalers.FooDatabaseMarshaler<LoadPort_PIO_FromVehicle>
    {
        public static readonly string fullyScopedName = "Veh_HandShakeData::LoadPort_PIO_FromVehicle";

        public override void InitEmbeddedMarshalers(IDomainParticipant participant)
        {
        }

        public override V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, System.IntPtr from, System.IntPtr to)
        {
            GCHandle tmpGCHandle = GCHandle.FromIntPtr(from);
            LoadPort_PIO_FromVehicle fromData = tmpGCHandle.Target as LoadPort_PIO_FromVehicle;
            return CopyIn(typePtr, fromData, to);
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, LoadPort_PIO_FromVehicle from, System.IntPtr to)
        {
            __LoadPort_PIO_FromVehicle nativeImg = new __LoadPort_PIO_FromVehicle();
            V_COPYIN_RESULT result = CopyIn(typePtr, from, ref nativeImg);
            if (result == V_COPYIN_RESULT.OK)
            {
                Marshal.StructureToPtr(nativeImg, to, false);
            }
            return result;
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, LoadPort_PIO_FromVehicle from, ref __LoadPort_PIO_FromVehicle to)
        {
            if (from == null) return V_COPYIN_RESULT.INVALID;
            if (from.EQ_Name == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.EQ_Name, from.EQ_Name)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.Port_Name == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Port_Name, from.Port_Name)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.PIO_ID == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.PIO_ID, from.PIO_ID)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.PIO_Channel = from.PIO_Channel;
            to.PortType = (uint) from.PortType;
            to.InService = from.InService ? (byte) 1 : (byte) 0;
            to.DO01_VALID = from.DO01_VALID ? (byte) 1 : (byte) 0;
            to.DO02_CS_0 = from.DO02_CS_0 ? (byte) 1 : (byte) 0;
            to.DO03_CS_1 = from.DO03_CS_1 ? (byte) 1 : (byte) 0;
            to.DO04_nil = from.DO04_nil ? (byte) 1 : (byte) 0;
            to.DO05_TR_REQ = from.DO05_TR_REQ ? (byte) 1 : (byte) 0;
            to.DO06_BUSY = from.DO06_BUSY ? (byte) 1 : (byte) 0;
            to.DO07_COMPT = from.DO07_COMPT ? (byte) 1 : (byte) 0;
            to.DO08_CONT = from.DO08_CONT ? (byte) 1 : (byte) 0;
            to.SELECT = from.SELECT ? (byte) 1 : (byte) 0;
            return V_COPYIN_RESULT.OK;
        }

        public override void CopyOut(System.IntPtr from, System.IntPtr to)
        {
            __LoadPort_PIO_FromVehicle nativeImg = (__LoadPort_PIO_FromVehicle) Marshal.PtrToStructure(from, typeof(__LoadPort_PIO_FromVehicle));
            GCHandle tmpGCHandleTo = GCHandle.FromIntPtr(to);
            LoadPort_PIO_FromVehicle toObj = tmpGCHandleTo.Target as LoadPort_PIO_FromVehicle;
            CopyOut(ref nativeImg, ref toObj);
            tmpGCHandleTo.Target = toObj;
        }

        public override void CopyOut(System.IntPtr from, ref LoadPort_PIO_FromVehicle to)
        {
            __LoadPort_PIO_FromVehicle nativeImg = (__LoadPort_PIO_FromVehicle) Marshal.PtrToStructure(from, typeof(__LoadPort_PIO_FromVehicle));
            CopyOut(ref nativeImg, ref to);
        }

        public static void StaticCopyOut(System.IntPtr from, ref LoadPort_PIO_FromVehicle to)
        {
            __LoadPort_PIO_FromVehicle nativeImg = (__LoadPort_PIO_FromVehicle) Marshal.PtrToStructure(from, typeof(__LoadPort_PIO_FromVehicle));
            CopyOut(ref nativeImg, ref to);
        }

        public static void CopyOut(ref __LoadPort_PIO_FromVehicle from, ref LoadPort_PIO_FromVehicle to)
        {
            if (to == null) {
                to = new LoadPort_PIO_FromVehicle();
            }
            to.EQ_Name = ReadString(from.EQ_Name);
            to.Port_Name = ReadString(from.Port_Name);
            to.PIO_ID = ReadString(from.PIO_ID);
            to.PIO_Channel = from.PIO_Channel;
            to.PortType = (Veh_HandShakeData.PortOwnerType) from.PortType;
            to.InService = from.InService != 0 ? true : false;
            to.DO01_VALID = from.DO01_VALID != 0 ? true : false;
            to.DO02_CS_0 = from.DO02_CS_0 != 0 ? true : false;
            to.DO03_CS_1 = from.DO03_CS_1 != 0 ? true : false;
            to.DO04_nil = from.DO04_nil != 0 ? true : false;
            to.DO05_TR_REQ = from.DO05_TR_REQ != 0 ? true : false;
            to.DO06_BUSY = from.DO06_BUSY != 0 ? true : false;
            to.DO07_COMPT = from.DO07_COMPT != 0 ? true : false;
            to.DO08_CONT = from.DO08_CONT != 0 ? true : false;
            to.SELECT = from.SELECT != 0 ? true : false;
        }

    }
    #endregion

    #region __LoadPort_PIO_ToVehicle
    [StructLayout(LayoutKind.Sequential)]
    public struct __LoadPort_PIO_ToVehicle
    {
        public IntPtr EQ_Name;
        public IntPtr Port_Name;
        public IntPtr PIO_ID;
        public int PIO_Channel;
        public uint PortType;
        public byte InService;
        public byte DI01_L_REQ;
        public byte DI02_U_REQ;
        public byte DI03_nil;
        public byte DI04_READY;
        public byte DI05_nil;
        public byte DI06_nil;
        public byte DI07_HO_AVBL;
        public byte DI08_ES;
        public byte READY_GO;
        public byte Tray_Detection;
    }
    #endregion

    #region LoadPort_PIO_ToVehicleMarshaler
    public sealed class LoadPort_PIO_ToVehicleMarshaler : DDS.OpenSplice.CustomMarshalers.FooDatabaseMarshaler<LoadPort_PIO_ToVehicle>
    {
        public static readonly string fullyScopedName = "Veh_HandShakeData::LoadPort_PIO_ToVehicle";

        public override void InitEmbeddedMarshalers(IDomainParticipant participant)
        {
        }

        public override V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, System.IntPtr from, System.IntPtr to)
        {
            GCHandle tmpGCHandle = GCHandle.FromIntPtr(from);
            LoadPort_PIO_ToVehicle fromData = tmpGCHandle.Target as LoadPort_PIO_ToVehicle;
            return CopyIn(typePtr, fromData, to);
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, LoadPort_PIO_ToVehicle from, System.IntPtr to)
        {
            __LoadPort_PIO_ToVehicle nativeImg = new __LoadPort_PIO_ToVehicle();
            V_COPYIN_RESULT result = CopyIn(typePtr, from, ref nativeImg);
            if (result == V_COPYIN_RESULT.OK)
            {
                Marshal.StructureToPtr(nativeImg, to, false);
            }
            return result;
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, LoadPort_PIO_ToVehicle from, ref __LoadPort_PIO_ToVehicle to)
        {
            if (from == null) return V_COPYIN_RESULT.INVALID;
            if (from.EQ_Name == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.EQ_Name, from.EQ_Name)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.Port_Name == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Port_Name, from.Port_Name)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.PIO_ID == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.PIO_ID, from.PIO_ID)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.PIO_Channel = from.PIO_Channel;
            to.PortType = (uint) from.PortType;
            to.InService = from.InService ? (byte) 1 : (byte) 0;
            to.DI01_L_REQ = from.DI01_L_REQ ? (byte) 1 : (byte) 0;
            to.DI02_U_REQ = from.DI02_U_REQ ? (byte) 1 : (byte) 0;
            to.DI03_nil = from.DI03_nil ? (byte) 1 : (byte) 0;
            to.DI04_READY = from.DI04_READY ? (byte) 1 : (byte) 0;
            to.DI05_nil = from.DI05_nil ? (byte) 1 : (byte) 0;
            to.DI06_nil = from.DI06_nil ? (byte) 1 : (byte) 0;
            to.DI07_HO_AVBL = from.DI07_HO_AVBL ? (byte) 1 : (byte) 0;
            to.DI08_ES = from.DI08_ES ? (byte) 1 : (byte) 0;
            to.READY_GO = from.READY_GO ? (byte) 1 : (byte) 0;
            to.Tray_Detection = from.Tray_Detection ? (byte) 1 : (byte) 0;
            return V_COPYIN_RESULT.OK;
        }

        public override void CopyOut(System.IntPtr from, System.IntPtr to)
        {
            __LoadPort_PIO_ToVehicle nativeImg = (__LoadPort_PIO_ToVehicle) Marshal.PtrToStructure(from, typeof(__LoadPort_PIO_ToVehicle));
            GCHandle tmpGCHandleTo = GCHandle.FromIntPtr(to);
            LoadPort_PIO_ToVehicle toObj = tmpGCHandleTo.Target as LoadPort_PIO_ToVehicle;
            CopyOut(ref nativeImg, ref toObj);
            tmpGCHandleTo.Target = toObj;
        }

        public override void CopyOut(System.IntPtr from, ref LoadPort_PIO_ToVehicle to)
        {
            __LoadPort_PIO_ToVehicle nativeImg = (__LoadPort_PIO_ToVehicle) Marshal.PtrToStructure(from, typeof(__LoadPort_PIO_ToVehicle));
            CopyOut(ref nativeImg, ref to);
        }

        public static void StaticCopyOut(System.IntPtr from, ref LoadPort_PIO_ToVehicle to)
        {
            __LoadPort_PIO_ToVehicle nativeImg = (__LoadPort_PIO_ToVehicle) Marshal.PtrToStructure(from, typeof(__LoadPort_PIO_ToVehicle));
            CopyOut(ref nativeImg, ref to);
        }

        public static void CopyOut(ref __LoadPort_PIO_ToVehicle from, ref LoadPort_PIO_ToVehicle to)
        {
            if (to == null) {
                to = new LoadPort_PIO_ToVehicle();
            }
            to.EQ_Name = ReadString(from.EQ_Name);
            to.Port_Name = ReadString(from.Port_Name);
            to.PIO_ID = ReadString(from.PIO_ID);
            to.PIO_Channel = from.PIO_Channel;
            to.PortType = (Veh_HandShakeData.PortOwnerType) from.PortType;
            to.InService = from.InService != 0 ? true : false;
            to.DI01_L_REQ = from.DI01_L_REQ != 0 ? true : false;
            to.DI02_U_REQ = from.DI02_U_REQ != 0 ? true : false;
            to.DI03_nil = from.DI03_nil != 0 ? true : false;
            to.DI04_READY = from.DI04_READY != 0 ? true : false;
            to.DI05_nil = from.DI05_nil != 0 ? true : false;
            to.DI06_nil = from.DI06_nil != 0 ? true : false;
            to.DI07_HO_AVBL = from.DI07_HO_AVBL != 0 ? true : false;
            to.DI08_ES = from.DI08_ES != 0 ? true : false;
            to.READY_GO = from.READY_GO != 0 ? true : false;
            to.Tray_Detection = from.Tray_Detection != 0 ? true : false;
        }

    }
    #endregion

    #region __EQ_Stages_Interface_IO
    [StructLayout(LayoutKind.Sequential)]
    public struct __EQ_Stages_Interface_IO
    {
        public IntPtr EQ_Name;
        public uint PortType;
        public byte InService;
        public byte EQ_Online;
        public byte EQ_Error;
        public IntPtr ST01_Port_Name;
        public int ST01_Stage_ID;
        public byte ST01_Ready;
        public byte ST01_Loaded;
        public IntPtr ST02_Port_Name;
        public int ST02_Stage_ID;
        public byte ST02_Ready;
        public byte ST02_Loaded;
        public IntPtr ST03_Port_Name;
        public int ST03_Stage_ID;
        public byte ST03_Ready;
        public byte ST03_Loaded;
        public IntPtr ST04_Port_Name;
        public int ST04_Stage_ID;
        public byte ST04_Ready;
        public byte ST04_Loaded;
        public IntPtr ST05_Port_Name;
        public int ST05_Stage_ID;
        public byte ST05_Ready;
        public byte ST05_Loaded;
        public IntPtr ST06_Port_Name;
        public int ST06_Stage_ID;
        public byte ST06_Ready;
        public byte ST06_Loaded;
        public IntPtr ST07_Port_Name;
        public int ST07_Stage_ID;
        public byte ST07_Ready;
        public byte ST07_Loaded;
        public IntPtr ST08_Port_Name;
        public int ST08_Stage_ID;
        public byte ST08_Ready;
        public byte ST08_Loaded;
        public IntPtr ST09_Port_Name;
        public int ST09_Stage_ID;
        public byte ST09_Ready;
        public byte ST09_Loaded;
        public IntPtr ST10_Port_Name;
        public int ST10_Stage_ID;
        public byte ST10_Ready;
        public byte ST10_Loaded;
        public IntPtr ST11_Port_Name;
        public int ST11_Stage_ID;
        public byte ST11_Ready;
        public byte ST11_Loaded;
        public IntPtr ST12_Port_Name;
        public int ST12_Stage_ID;
        public byte ST12_Ready;
        public byte ST12_Loaded;
        public IntPtr ST13_Port_Name;
        public int ST13_Stage_ID;
        public byte ST13_Ready;
        public byte ST13_Loaded;
        public IntPtr ST14_Port_Name;
        public int ST14_Stage_ID;
        public byte ST14_Ready;
        public byte ST14_Loaded;
        public IntPtr ST15_Port_Name;
        public int ST15_Stage_ID;
        public byte ST15_Ready;
        public byte ST15_Loaded;
        public IntPtr ST16_Port_Name;
        public int ST16_Stage_ID;
        public byte ST16_Ready;
        public byte ST16_Loaded;
    }
    #endregion

    #region EQ_Stages_Interface_IOMarshaler
    public sealed class EQ_Stages_Interface_IOMarshaler : DDS.OpenSplice.CustomMarshalers.FooDatabaseMarshaler<EQ_Stages_Interface_IO>
    {
        public static readonly string fullyScopedName = "Veh_HandShakeData::EQ_Stages_Interface_IO";

        public override void InitEmbeddedMarshalers(IDomainParticipant participant)
        {
        }

        public override V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, System.IntPtr from, System.IntPtr to)
        {
            GCHandle tmpGCHandle = GCHandle.FromIntPtr(from);
            EQ_Stages_Interface_IO fromData = tmpGCHandle.Target as EQ_Stages_Interface_IO;
            return CopyIn(typePtr, fromData, to);
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, EQ_Stages_Interface_IO from, System.IntPtr to)
        {
            __EQ_Stages_Interface_IO nativeImg = new __EQ_Stages_Interface_IO();
            V_COPYIN_RESULT result = CopyIn(typePtr, from, ref nativeImg);
            if (result == V_COPYIN_RESULT.OK)
            {
                Marshal.StructureToPtr(nativeImg, to, false);
            }
            return result;
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, EQ_Stages_Interface_IO from, ref __EQ_Stages_Interface_IO to)
        {
            if (from == null) return V_COPYIN_RESULT.INVALID;
            if (from.EQ_Name == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.EQ_Name, from.EQ_Name)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.PortType = (uint) from.PortType;
            to.InService = from.InService ? (byte) 1 : (byte) 0;
            to.EQ_Online = from.EQ_Online ? (byte) 1 : (byte) 0;
            to.EQ_Error = from.EQ_Error ? (byte) 1 : (byte) 0;
            if (from.ST01_Port_Name == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.ST01_Port_Name, from.ST01_Port_Name)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.ST01_Stage_ID = from.ST01_Stage_ID;
            to.ST01_Ready = from.ST01_Ready ? (byte) 1 : (byte) 0;
            to.ST01_Loaded = from.ST01_Loaded ? (byte) 1 : (byte) 0;
            if (from.ST02_Port_Name == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.ST02_Port_Name, from.ST02_Port_Name)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.ST02_Stage_ID = from.ST02_Stage_ID;
            to.ST02_Ready = from.ST02_Ready ? (byte) 1 : (byte) 0;
            to.ST02_Loaded = from.ST02_Loaded ? (byte) 1 : (byte) 0;
            if (from.ST03_Port_Name == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.ST03_Port_Name, from.ST03_Port_Name)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.ST03_Stage_ID = from.ST03_Stage_ID;
            to.ST03_Ready = from.ST03_Ready ? (byte) 1 : (byte) 0;
            to.ST03_Loaded = from.ST03_Loaded ? (byte) 1 : (byte) 0;
            if (from.ST04_Port_Name == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.ST04_Port_Name, from.ST04_Port_Name)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.ST04_Stage_ID = from.ST04_Stage_ID;
            to.ST04_Ready = from.ST04_Ready ? (byte) 1 : (byte) 0;
            to.ST04_Loaded = from.ST04_Loaded ? (byte) 1 : (byte) 0;
            if (from.ST05_Port_Name == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.ST05_Port_Name, from.ST05_Port_Name)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.ST05_Stage_ID = from.ST05_Stage_ID;
            to.ST05_Ready = from.ST05_Ready ? (byte) 1 : (byte) 0;
            to.ST05_Loaded = from.ST05_Loaded ? (byte) 1 : (byte) 0;
            if (from.ST06_Port_Name == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.ST06_Port_Name, from.ST06_Port_Name)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.ST06_Stage_ID = from.ST06_Stage_ID;
            to.ST06_Ready = from.ST06_Ready ? (byte) 1 : (byte) 0;
            to.ST06_Loaded = from.ST06_Loaded ? (byte) 1 : (byte) 0;
            if (from.ST07_Port_Name == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.ST07_Port_Name, from.ST07_Port_Name)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.ST07_Stage_ID = from.ST07_Stage_ID;
            to.ST07_Ready = from.ST07_Ready ? (byte) 1 : (byte) 0;
            to.ST07_Loaded = from.ST07_Loaded ? (byte) 1 : (byte) 0;
            if (from.ST08_Port_Name == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.ST08_Port_Name, from.ST08_Port_Name)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.ST08_Stage_ID = from.ST08_Stage_ID;
            to.ST08_Ready = from.ST08_Ready ? (byte) 1 : (byte) 0;
            to.ST08_Loaded = from.ST08_Loaded ? (byte) 1 : (byte) 0;
            if (from.ST09_Port_Name == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.ST09_Port_Name, from.ST09_Port_Name)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.ST09_Stage_ID = from.ST09_Stage_ID;
            to.ST09_Ready = from.ST09_Ready ? (byte) 1 : (byte) 0;
            to.ST09_Loaded = from.ST09_Loaded ? (byte) 1 : (byte) 0;
            if (from.ST10_Port_Name == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.ST10_Port_Name, from.ST10_Port_Name)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.ST10_Stage_ID = from.ST10_Stage_ID;
            to.ST10_Ready = from.ST10_Ready ? (byte) 1 : (byte) 0;
            to.ST10_Loaded = from.ST10_Loaded ? (byte) 1 : (byte) 0;
            if (from.ST11_Port_Name == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.ST11_Port_Name, from.ST11_Port_Name)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.ST11_Stage_ID = from.ST11_Stage_ID;
            to.ST11_Ready = from.ST11_Ready ? (byte) 1 : (byte) 0;
            to.ST11_Loaded = from.ST11_Loaded ? (byte) 1 : (byte) 0;
            if (from.ST12_Port_Name == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.ST12_Port_Name, from.ST12_Port_Name)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.ST12_Stage_ID = from.ST12_Stage_ID;
            to.ST12_Ready = from.ST12_Ready ? (byte) 1 : (byte) 0;
            to.ST12_Loaded = from.ST12_Loaded ? (byte) 1 : (byte) 0;
            if (from.ST13_Port_Name == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.ST13_Port_Name, from.ST13_Port_Name)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.ST13_Stage_ID = from.ST13_Stage_ID;
            to.ST13_Ready = from.ST13_Ready ? (byte) 1 : (byte) 0;
            to.ST13_Loaded = from.ST13_Loaded ? (byte) 1 : (byte) 0;
            if (from.ST14_Port_Name == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.ST14_Port_Name, from.ST14_Port_Name)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.ST14_Stage_ID = from.ST14_Stage_ID;
            to.ST14_Ready = from.ST14_Ready ? (byte) 1 : (byte) 0;
            to.ST14_Loaded = from.ST14_Loaded ? (byte) 1 : (byte) 0;
            if (from.ST15_Port_Name == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.ST15_Port_Name, from.ST15_Port_Name)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.ST15_Stage_ID = from.ST15_Stage_ID;
            to.ST15_Ready = from.ST15_Ready ? (byte) 1 : (byte) 0;
            to.ST15_Loaded = from.ST15_Loaded ? (byte) 1 : (byte) 0;
            if (from.ST16_Port_Name == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.ST16_Port_Name, from.ST16_Port_Name)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.ST16_Stage_ID = from.ST16_Stage_ID;
            to.ST16_Ready = from.ST16_Ready ? (byte) 1 : (byte) 0;
            to.ST16_Loaded = from.ST16_Loaded ? (byte) 1 : (byte) 0;
            return V_COPYIN_RESULT.OK;
        }

        public override void CopyOut(System.IntPtr from, System.IntPtr to)
        {
            __EQ_Stages_Interface_IO nativeImg = (__EQ_Stages_Interface_IO) Marshal.PtrToStructure(from, typeof(__EQ_Stages_Interface_IO));
            GCHandle tmpGCHandleTo = GCHandle.FromIntPtr(to);
            EQ_Stages_Interface_IO toObj = tmpGCHandleTo.Target as EQ_Stages_Interface_IO;
            CopyOut(ref nativeImg, ref toObj);
            tmpGCHandleTo.Target = toObj;
        }

        public override void CopyOut(System.IntPtr from, ref EQ_Stages_Interface_IO to)
        {
            __EQ_Stages_Interface_IO nativeImg = (__EQ_Stages_Interface_IO) Marshal.PtrToStructure(from, typeof(__EQ_Stages_Interface_IO));
            CopyOut(ref nativeImg, ref to);
        }

        public static void StaticCopyOut(System.IntPtr from, ref EQ_Stages_Interface_IO to)
        {
            __EQ_Stages_Interface_IO nativeImg = (__EQ_Stages_Interface_IO) Marshal.PtrToStructure(from, typeof(__EQ_Stages_Interface_IO));
            CopyOut(ref nativeImg, ref to);
        }

        public static void CopyOut(ref __EQ_Stages_Interface_IO from, ref EQ_Stages_Interface_IO to)
        {
            if (to == null) {
                to = new EQ_Stages_Interface_IO();
            }
            to.EQ_Name = ReadString(from.EQ_Name);
            to.PortType = (Veh_HandShakeData.PortOwnerType) from.PortType;
            to.InService = from.InService != 0 ? true : false;
            to.EQ_Online = from.EQ_Online != 0 ? true : false;
            to.EQ_Error = from.EQ_Error != 0 ? true : false;
            to.ST01_Port_Name = ReadString(from.ST01_Port_Name);
            to.ST01_Stage_ID = from.ST01_Stage_ID;
            to.ST01_Ready = from.ST01_Ready != 0 ? true : false;
            to.ST01_Loaded = from.ST01_Loaded != 0 ? true : false;
            to.ST02_Port_Name = ReadString(from.ST02_Port_Name);
            to.ST02_Stage_ID = from.ST02_Stage_ID;
            to.ST02_Ready = from.ST02_Ready != 0 ? true : false;
            to.ST02_Loaded = from.ST02_Loaded != 0 ? true : false;
            to.ST03_Port_Name = ReadString(from.ST03_Port_Name);
            to.ST03_Stage_ID = from.ST03_Stage_ID;
            to.ST03_Ready = from.ST03_Ready != 0 ? true : false;
            to.ST03_Loaded = from.ST03_Loaded != 0 ? true : false;
            to.ST04_Port_Name = ReadString(from.ST04_Port_Name);
            to.ST04_Stage_ID = from.ST04_Stage_ID;
            to.ST04_Ready = from.ST04_Ready != 0 ? true : false;
            to.ST04_Loaded = from.ST04_Loaded != 0 ? true : false;
            to.ST05_Port_Name = ReadString(from.ST05_Port_Name);
            to.ST05_Stage_ID = from.ST05_Stage_ID;
            to.ST05_Ready = from.ST05_Ready != 0 ? true : false;
            to.ST05_Loaded = from.ST05_Loaded != 0 ? true : false;
            to.ST06_Port_Name = ReadString(from.ST06_Port_Name);
            to.ST06_Stage_ID = from.ST06_Stage_ID;
            to.ST06_Ready = from.ST06_Ready != 0 ? true : false;
            to.ST06_Loaded = from.ST06_Loaded != 0 ? true : false;
            to.ST07_Port_Name = ReadString(from.ST07_Port_Name);
            to.ST07_Stage_ID = from.ST07_Stage_ID;
            to.ST07_Ready = from.ST07_Ready != 0 ? true : false;
            to.ST07_Loaded = from.ST07_Loaded != 0 ? true : false;
            to.ST08_Port_Name = ReadString(from.ST08_Port_Name);
            to.ST08_Stage_ID = from.ST08_Stage_ID;
            to.ST08_Ready = from.ST08_Ready != 0 ? true : false;
            to.ST08_Loaded = from.ST08_Loaded != 0 ? true : false;
            to.ST09_Port_Name = ReadString(from.ST09_Port_Name);
            to.ST09_Stage_ID = from.ST09_Stage_ID;
            to.ST09_Ready = from.ST09_Ready != 0 ? true : false;
            to.ST09_Loaded = from.ST09_Loaded != 0 ? true : false;
            to.ST10_Port_Name = ReadString(from.ST10_Port_Name);
            to.ST10_Stage_ID = from.ST10_Stage_ID;
            to.ST10_Ready = from.ST10_Ready != 0 ? true : false;
            to.ST10_Loaded = from.ST10_Loaded != 0 ? true : false;
            to.ST11_Port_Name = ReadString(from.ST11_Port_Name);
            to.ST11_Stage_ID = from.ST11_Stage_ID;
            to.ST11_Ready = from.ST11_Ready != 0 ? true : false;
            to.ST11_Loaded = from.ST11_Loaded != 0 ? true : false;
            to.ST12_Port_Name = ReadString(from.ST12_Port_Name);
            to.ST12_Stage_ID = from.ST12_Stage_ID;
            to.ST12_Ready = from.ST12_Ready != 0 ? true : false;
            to.ST12_Loaded = from.ST12_Loaded != 0 ? true : false;
            to.ST13_Port_Name = ReadString(from.ST13_Port_Name);
            to.ST13_Stage_ID = from.ST13_Stage_ID;
            to.ST13_Ready = from.ST13_Ready != 0 ? true : false;
            to.ST13_Loaded = from.ST13_Loaded != 0 ? true : false;
            to.ST14_Port_Name = ReadString(from.ST14_Port_Name);
            to.ST14_Stage_ID = from.ST14_Stage_ID;
            to.ST14_Ready = from.ST14_Ready != 0 ? true : false;
            to.ST14_Loaded = from.ST14_Loaded != 0 ? true : false;
            to.ST15_Port_Name = ReadString(from.ST15_Port_Name);
            to.ST15_Stage_ID = from.ST15_Stage_ID;
            to.ST15_Ready = from.ST15_Ready != 0 ? true : false;
            to.ST15_Loaded = from.ST15_Loaded != 0 ? true : false;
            to.ST16_Port_Name = ReadString(from.ST16_Port_Name);
            to.ST16_Stage_ID = from.ST16_Stage_ID;
            to.ST16_Ready = from.ST16_Ready != 0 ? true : false;
            to.ST16_Loaded = from.ST16_Loaded != 0 ? true : false;
        }

    }
    #endregion

    #region __InterVehicles_BlockZones_Control
    [StructLayout(LayoutKind.Sequential)]
    public struct __InterVehicles_BlockZones_Control
    {
        public IntPtr BlockZoneID;
        public IntPtr HasLockedBy_VehID;
        public IntPtr RequestUnlockingBy_VehID;
        public IntPtr HasUnlockedBy_VehID;
        public IntPtr HasAcquiredBy_VehID;
        public IntPtr Locking_Timestamp;
        public IntPtr RequestUnlocking_Timestamp;
        public IntPtr Unlocking_Timestamp;
        public IntPtr Acquiring_Timestamp;
        public IntPtr ServerHeartbeating_Timestamp;
        public IntPtr SeverInstanceID;
        public byte InService;
    }
    #endregion

    #region InterVehicles_BlockZones_ControlMarshaler
    public sealed class InterVehicles_BlockZones_ControlMarshaler : DDS.OpenSplice.CustomMarshalers.FooDatabaseMarshaler<InterVehicles_BlockZones_Control>
    {
        public static readonly string fullyScopedName = "Veh_HandShakeData::InterVehicles_BlockZones_Control";

        public override void InitEmbeddedMarshalers(IDomainParticipant participant)
        {
        }

        public override V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, System.IntPtr from, System.IntPtr to)
        {
            GCHandle tmpGCHandle = GCHandle.FromIntPtr(from);
            InterVehicles_BlockZones_Control fromData = tmpGCHandle.Target as InterVehicles_BlockZones_Control;
            return CopyIn(typePtr, fromData, to);
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, InterVehicles_BlockZones_Control from, System.IntPtr to)
        {
            __InterVehicles_BlockZones_Control nativeImg = new __InterVehicles_BlockZones_Control();
            V_COPYIN_RESULT result = CopyIn(typePtr, from, ref nativeImg);
            if (result == V_COPYIN_RESULT.OK)
            {
                Marshal.StructureToPtr(nativeImg, to, false);
            }
            return result;
        }

        public V_COPYIN_RESULT CopyIn(System.IntPtr typePtr, InterVehicles_BlockZones_Control from, ref __InterVehicles_BlockZones_Control to)
        {
            if (from == null) return V_COPYIN_RESULT.INVALID;
            if (from.BlockZoneID == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.BlockZoneID, from.BlockZoneID)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.HasLockedBy_VehID == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.HasLockedBy_VehID, from.HasLockedBy_VehID)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.RequestUnlockingBy_VehID == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.RequestUnlockingBy_VehID, from.RequestUnlockingBy_VehID)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.HasUnlockedBy_VehID == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.HasUnlockedBy_VehID, from.HasUnlockedBy_VehID)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.HasAcquiredBy_VehID == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.HasAcquiredBy_VehID, from.HasAcquiredBy_VehID)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.Locking_Timestamp == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Locking_Timestamp, from.Locking_Timestamp)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.RequestUnlocking_Timestamp == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.RequestUnlocking_Timestamp, from.RequestUnlocking_Timestamp)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.Unlocking_Timestamp == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Unlocking_Timestamp, from.Unlocking_Timestamp)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.Acquiring_Timestamp == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.Acquiring_Timestamp, from.Acquiring_Timestamp)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.ServerHeartbeating_Timestamp == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.ServerHeartbeating_Timestamp, from.ServerHeartbeating_Timestamp)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            if (from.SeverInstanceID == null) return V_COPYIN_RESULT.INVALID;
            // Unbounded string: bounds check not required...
            if (!Write(c.getBase(typePtr), ref to.SeverInstanceID, from.SeverInstanceID)) return V_COPYIN_RESULT.OUT_OF_MEMORY;
            to.InService = from.InService ? (byte) 1 : (byte) 0;
            return V_COPYIN_RESULT.OK;
        }

        public override void CopyOut(System.IntPtr from, System.IntPtr to)
        {
            __InterVehicles_BlockZones_Control nativeImg = (__InterVehicles_BlockZones_Control) Marshal.PtrToStructure(from, typeof(__InterVehicles_BlockZones_Control));
            GCHandle tmpGCHandleTo = GCHandle.FromIntPtr(to);
            InterVehicles_BlockZones_Control toObj = tmpGCHandleTo.Target as InterVehicles_BlockZones_Control;
            CopyOut(ref nativeImg, ref toObj);
            tmpGCHandleTo.Target = toObj;
        }

        public override void CopyOut(System.IntPtr from, ref InterVehicles_BlockZones_Control to)
        {
            __InterVehicles_BlockZones_Control nativeImg = (__InterVehicles_BlockZones_Control) Marshal.PtrToStructure(from, typeof(__InterVehicles_BlockZones_Control));
            CopyOut(ref nativeImg, ref to);
        }

        public static void StaticCopyOut(System.IntPtr from, ref InterVehicles_BlockZones_Control to)
        {
            __InterVehicles_BlockZones_Control nativeImg = (__InterVehicles_BlockZones_Control) Marshal.PtrToStructure(from, typeof(__InterVehicles_BlockZones_Control));
            CopyOut(ref nativeImg, ref to);
        }

        public static void CopyOut(ref __InterVehicles_BlockZones_Control from, ref InterVehicles_BlockZones_Control to)
        {
            if (to == null) {
                to = new InterVehicles_BlockZones_Control();
            }
            to.BlockZoneID = ReadString(from.BlockZoneID);
            to.HasLockedBy_VehID = ReadString(from.HasLockedBy_VehID);
            to.RequestUnlockingBy_VehID = ReadString(from.RequestUnlockingBy_VehID);
            to.HasUnlockedBy_VehID = ReadString(from.HasUnlockedBy_VehID);
            to.HasAcquiredBy_VehID = ReadString(from.HasAcquiredBy_VehID);
            to.Locking_Timestamp = ReadString(from.Locking_Timestamp);
            to.RequestUnlocking_Timestamp = ReadString(from.RequestUnlocking_Timestamp);
            to.Unlocking_Timestamp = ReadString(from.Unlocking_Timestamp);
            to.Acquiring_Timestamp = ReadString(from.Acquiring_Timestamp);
            to.ServerHeartbeating_Timestamp = ReadString(from.ServerHeartbeating_Timestamp);
            to.SeverInstanceID = ReadString(from.SeverInstanceID);
            to.InService = from.InService != 0 ? true : false;
        }

    }
    #endregion

}

