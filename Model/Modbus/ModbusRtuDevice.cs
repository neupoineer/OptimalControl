using System.IO.Ports;

namespace Model.Modbus
{
    /// <summary>
    /// Modbus RTU Device
    /// </summary>
    public class ModbusRtuDevice
    {
        private SerialPort _serialPortObject;
        private byte _unitId;

        /// <summary>
        /// The serial port object
        /// </summary>
        public SerialPort SerialPortObject
        {
            get { return _serialPortObject; }
            set { _serialPortObject = value; }
        }

        /// <summary>
        /// The device identifier
        /// </summary>
        public byte UnitID
        {
            get { return _unitId; }
            set { _unitId = value; }
        }

        public ModbusRtuDevice()
        {
        }

        public ModbusRtuDevice(SerialPort serialPortObject, byte unitId)
        {
            SerialPortObject = serialPortObject;
            UnitID = unitId;
        }
    }
}
