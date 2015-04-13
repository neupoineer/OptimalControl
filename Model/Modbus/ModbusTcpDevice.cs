using System.Net.Sockets;
using Modbus.Device;

namespace Model.Modbus
{
    /// <summary>
    /// Modbus TCP Device
    /// </summary>
    public class ModbusTcpDevice
    {

        private string _ipAddress;
        private int _portName;
        private byte _unitId;
        private TcpClient _tcpClient;
        private ModbusIpMaster _modbusTcpMaster;


        /// <summary>
        /// The modbus IP address
        /// </summary>
        public string IpAddress
        {
            get { return _ipAddress; }
            set { _ipAddress = value; }
        }

        /// <summary>
        /// The modbus port
        /// </summary>
        public int PortName
        {
            get { return _portName; }
            set { _portName = value; }
        }

        /// <summary>
        /// The modbus device ID
        /// </summary>
        public byte UnitId
        {
            get { return _unitId; }
            set { _unitId = value; }
        }

        /// <summary>
        /// The TCP client
        /// </summary>
        public TcpClient TcpClient
        {
            get { return _tcpClient; }
            set { _tcpClient = value; }
        }

        /// <summary>
        /// The modbus IP master
        /// </summary>
        public ModbusIpMaster ModbusTcpMaster
        {
            get { return _modbusTcpMaster; }
            set { _modbusTcpMaster = value; }
        }

        public ModbusTcpDevice()
        {
            
        }

        public ModbusTcpDevice(string ipAddress, int portName, TcpClient tcpClient, ModbusIpMaster modbusTcpMaster, byte unitId)
        {
            IpAddress = ipAddress;
            PortName = portName;
            TcpClient = tcpClient;
            ModbusTcpMaster = modbusTcpMaster;
            UnitId = unitId;
        }
    }
}
