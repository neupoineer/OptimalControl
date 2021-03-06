﻿using System.Net.Sockets;
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
        private TcpListener _tcpListener;
        private ModbusTcpSlave _modbusTcpSlave;

        /// <summary>
        /// The modbus IP address
        /// </summary>
        public string IP
        {
            get { return _ipAddress; }
            set { _ipAddress = value; }
        }

        /// <summary>
        /// The modbus port
        /// </summary>
        public int Port
        {
            get { return _portName; }
            set { _portName = value; }
        }

        /// <summary>
        /// The modbus device ID
        /// </summary>
        public byte UnitID
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

        public TcpListener TcpListener
        {
            get { return _tcpListener; }
            set { _tcpListener = value; }
        }

        public ModbusTcpSlave ModbusTcpSlave
        {
            get { return _modbusTcpSlave; }
            set { _modbusTcpSlave = value; }
        }

        public ModbusTcpDevice()
        {
        }

        public ModbusTcpDevice(string ipAddress, int portName, TcpClient tcpClient, ModbusIpMaster modbusTcpMaster, byte unitId, TcpListener tcpListener, ModbusTcpSlave modbusTcpSlave)
        {
            IP = ipAddress;
            Port = portName;
            TcpClient = tcpClient;
            ModbusTcpMaster = modbusTcpMaster;
            UnitID = unitId;
            TcpListener = tcpListener;
            ModbusTcpSlave = modbusTcpSlave;
        }
    }
}
