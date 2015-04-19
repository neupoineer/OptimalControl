using System.Collections.Generic;
using Model.Control;

namespace Model.Modbus
{
    /// <summary>
    /// Device
    /// </summary>
    public class Device:ModelBase
    {
        private bool _state;
        private bool _syncState;
        public ModbusTcpDevice ModbusTcpDevice = new ModbusTcpDevice();
        private bool _modbusTcpMasterCreated;
        private bool _modbusTcpMasterUpdated;
        private List<Variable> _variables = new List<Variable>();

        /// <summary>
        /// The Device enabled
        /// </summary>
        public bool State
        {
            get { return _state; }
            set { _state = value; }
        }

        /// <summary>
        /// The DeviceEnabledn enabled
        /// </summary>
        public bool SyncState
        {
            get { return _syncState; }
            set { _syncState = value; }
        }

        /// <summary>
        /// The modbus TCP master created
        /// </summary>
        public bool ModbusTcpMasterCreated
        {
            get { return _modbusTcpMasterCreated; }
            set { _modbusTcpMasterCreated = value; }
        }

        /// <summary>
        /// The modbus TCP master updated
        /// </summary>
        public bool ModbusTcpMasterUpdated
        {
            get { return _modbusTcpMasterUpdated; }
            set { _modbusTcpMasterUpdated = value; }
        }

        /// <summary>
        /// The Parameters
        /// </summary>
        public List<Variable> Variables
        {
            get { return _variables; }
            set { _variables = value; }
        }

        public Device()
        {
            
        }

        public Device(bool state, bool syncState, ModbusTcpDevice modbusTcpDevice, bool modbusTcpMasterCreated, bool modbusTcpMasterUpdated, List<Variable> variables)
        {
            State = state;
            SyncState = syncState;
            ModbusTcpDevice = modbusTcpDevice;
            ModbusTcpMasterCreated = modbusTcpMasterCreated;
            ModbusTcpMasterUpdated = modbusTcpMasterUpdated;
            Variables = variables;
        }
    }
}
