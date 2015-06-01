using System.Collections.Generic;
using Model.Control;

namespace Model.Modbus
{
    /// <summary>
    /// 设备模型
    /// </summary>
    public class Device : ModelBase
    {
        #region Private Members

        private bool _state;
        private bool _syncState;
        public ModbusTcpDevice ModbusTcpDevice = new ModbusTcpDevice();
        private bool _modbusTcpMasterCreated;
        private bool _modbusTcpMasterUpdated;
        private List<Variable> _variables = new List<Variable>();

        #endregion

        #region Public Properties

        /// <summary>
        /// 是否启用设备
        /// </summary>
        public bool State
        {
            get { return _state; }
            set { _state = value; }
        }

        /// <summary>
        /// 是否同步数据
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
        /// 变量集合
        /// </summary>
        public List<Variable> Variables
        {
            get { return _variables; }
            set { _variables = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 无参构造
        /// </summary>
        public Device()
        {

        }

        /// <summary>
        /// 带参构造
        /// </summary>
        public Device(
            bool state,
            bool syncState,
            ModbusTcpDevice modbusTcpDevice,
            bool modbusTcpMasterCreated,
            bool modbusTcpMasterUpdated,
            List<Variable> variables)
        {
            State = state;
            SyncState = syncState;
            ModbusTcpDevice = modbusTcpDevice;
            ModbusTcpMasterCreated = modbusTcpMasterCreated;
            ModbusTcpMasterUpdated = modbusTcpMasterUpdated;
            Variables = variables;
        }

        #endregion
    }
}
