using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace MonitoringTool
{
    /// <summary>
    /// ����DCS��������
    /// </summary>
    /*
    struct DcsData
    {
        public int order;
        public int exAddress;
        public int length;
        public string name;
        public string code;
        public double data;
        public string inAddress;
    }
    */

    public partial class ClientThread
    {
        private string mbs_File = AppDomain.CurrentDomain.BaseDirectory + "cache\\mbs_File.mbs"; //��Ҫ���͵�modbus�����ļ�
        public TcpClient tcpClient;
        public double[] sendData = new double[24];
        private string[] sendDataString = new string[24];

        public ClientThread(TcpClient tcpclient)
        {
            //service����ӹܶ���Ϣ�Ŀ���
            this.tcpClient = tcpclient;
            mbs_File = ConfigAppSettings.GetValue("MBSFile").Trim();
            if (mbs_File == "")
            {
                mbs_File = AppDomain.CurrentDomain.BaseDirectory + "cache\\mbs_File.mbs"; //��Ҫ���͵�modbus�����ļ�
            }
        }

        public void ClientService()
        {
            byte[] myReadBuffer = new byte[1024];

            NetworkStream clientStream = tcpClient.GetStream();
            while (true)
            {
                // Check to see if this NetworkStream is readable.
                if (clientStream.CanRead)
                {
                    try
                    {
                        StringBuilder myCompleteMessage = new StringBuilder();
                        int numberOfBytesRead = 0;
                        // Incoming message may be larger than the buffer size.
                        do
                        {
                            numberOfBytesRead = clientStream.Read(myReadBuffer, 0, myReadBuffer.Length);
                        }
                        while (clientStream.DataAvailable);
                        if (numberOfBytesRead < 1)
                        {
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        string error = e.Message;
                    }

                }

                //�ж�������
                //if (myReadBuffer[6] != 3) //�������վ�Ų�һ��
                //{
                //    continue;
                //}

                int dataNr = Convert.ToInt32(myReadBuffer[11]) * 2;
                int frameLength = dataNr + 9;
                byte[] myWriteBuffer = new byte[frameLength];
                string[] writeString = new string[dataNr]; //��������

                int startAddress = 40000 + Convert.ToInt32(myReadBuffer[8]) * 256 + Convert.ToInt32(myReadBuffer[9]) + 1;
                int length = Convert.ToInt32(myReadBuffer[11]);

                //                Console.WriteLine(startAddress + "-" + length);

                GetSendData(startAddress, length, ref writeString);

                // Check to see if this NetworkStream is writable.
                if (clientStream.CanWrite)
                {
                    myWriteBuffer[0] = myReadBuffer[0];
                    myWriteBuffer[1] = myReadBuffer[1];
                    myWriteBuffer[2] = myReadBuffer[2];
                    myWriteBuffer[3] = myReadBuffer[3];
                    myWriteBuffer[4] = myReadBuffer[4];
                    myWriteBuffer[5] = Convert.ToByte(myReadBuffer[11] * 2 + 3);
                    myWriteBuffer[6] = myReadBuffer[6];
                    myWriteBuffer[7] = myReadBuffer[7];
                    myWriteBuffer[8] = Convert.ToByte(myReadBuffer[11] * 2);

                    for (int i = 0; i < dataNr / 2; i++)
                    {
                        try
                        {
                            if (writeString[i] != null)
                            {
                                myWriteBuffer[9 + 2 * i] = Convert.ToByte(writeString[i].Substring(0, 8), 2);
                                myWriteBuffer[10 + 2 * i] = Convert.ToByte(writeString[i].Substring(8), 2);
                            }
                        }
                        catch (Exception e)
                        {
                            string eMessage = e.Message;
                        }
                    }
                    try
                    {
                        clientStream.Write(myWriteBuffer, 0, myWriteBuffer.Length);
                    }
                    catch (Exception e)
                    {
                        string error = e.Message;
                    }
                }

            }
            clientStream.Close();
            Thread.CurrentThread.Abort();
        }

        /******************************************************************************************************************/

        /// <summary>
        /// Read and clear the MBS file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        private void ReadMbsFile(string fileName)        //��ȡ�ļ�
        {
            try
            {
                FileInfo file = new FileInfo(fileName);            //����ѡ���ļ��򿪻�ȡ������Ϣ
                int fileByteLength = (int)file.Length;        //��ȡ  8λ���ݸ���
                if (fileByteLength > 0)
                {
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);  //���ֽڶ�ȡ�ļ�����(ֻ��)
                    byte[] mybytes = new byte[fileByteLength];
                    fs.Read(mybytes, 0, fileByteLength);
                    fs.Flush();     //�ͷ����������
                    fs.Close();
                    fs.Dispose();

                    StreamWriter fss = new StreamWriter(fileName, false, System.Text.Encoding.Default);
                    fss.Write("");
                    fss.Close();     //�ر��ļ�
                    fss.Dispose();

                    int file_double_length = fileByteLength / 8;    //��ȡ 64λ���ݸ���
                    for (int i = 0; i < file_double_length; i++)
                    {
                        sendData[i] = BitConverter.ToDouble(mybytes, (8 * i));
                        ToIEEE754(sendData[i], ref sendDataString[i]);
                    }
                }
            }
            catch (System.Exception e)
            {
                string error = e.Message;
            }
        }

        /******************************************************************************************************************/

        /// <summary>
        /// Gets the send data.
        /// </summary>
        /// <param name="startAddress">The start address.</param>
        /// <param name="length">The length.</param>
        /// <param name="writeString">The write string.</param>
        /// <returns>isOK</returns>
        private bool GetSendData(int startAddress, int length, ref string[] writeString) //��ý�Ҫ���͵�����
        {
            bool isOK = true;
            try
            {
                for (int i = 0; i < length; i += 2)
                {
                    int j = i / 2;
                    int exAddress = startAddress + i;
                    //DcsData dcsData = DcsInit(exAddress);
                    ReadMbsFile(mbs_File);
                    writeString[i + 1] = sendDataString[j].Substring(0, 16);
                    writeString[i] = sendDataString[j].Substring(16);
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
            }
            return isOK;
        }

        /// <summary>
        /// DCS���ݳ�ʼ��.
        /// </summary>
        /// <param name="exAddress">The ex address.</param>
        /// <returns></returns>
        /*
        private DcsData DcsInit(int exAddress)
        {
            DcsData dcsData = new DcsData();
            dcsData.exAddress = exAddress;
            return dcsData;
        }
        */

        /// <summary>
        /// ��ʮ������ת����IEEE754��ʽ�Ķ�������
        /// </summary>
        /// <param name="octData">The octdata.</param>
        /// <param name="dataString">Retutn the data string.</param>
        /// <returns></returns>
        private bool ToIEEE754(double octData, ref string dataString)
        {
            bool isOK = true;

            if (octData == 0)
            {
                dataString = "00000000" + "00000000" + "00000000" + "00000000";
                return isOK;
            }

            try
            {
                string sign = "";  //���ţ�1λ
                string exponent = ""; //ָ����8λ
                string mantissa = "";//β����23λ,����С����ǰ��1λ
                if (octData >= 0) { sign = "0"; } else { sign = "1"; }
                octData = Math.Abs(octData);//ȡ�����ݵľ���ֵ
                int pointIndex = octData.ToString().IndexOf(".");
                double intPart = 0;
                double fractionPart = 0.0;
                int power = 0;//��
                string intString = "";
                string fractionString = "";
                intPart = Math.Floor(octData);  //�����������
                fractionPart = octData - intPart;//���С�����ֲ���
                intString = Convert.ToString(Convert.ToInt32(intPart), 2); //����������ֶ������ַ���

                //����β������
                int mantissaLen = 24;//β������
                int fractionLen = 50; //����С������
                for (int i = 0; i < fractionLen; i++)
                {
                    fractionPart = fractionPart * 2;
                    if (fractionPart >= 1)
                    {
                        fractionPart = fractionPart - Math.Floor(fractionPart);
                        fractionString = fractionString + "1";
                    }
                    else { fractionString = fractionString + "0"; }
                }
                mantissa = intString + fractionString;
                mantissa = mantissa.Substring(mantissa.IndexOf("1") + 1, mantissaLen - 1);
                //β���������

                //����ָ������
                if (intPart != 0) { power = intString.Length - 1; }
                else
                {
                    if (fractionPart != 0) { power = -fractionString.IndexOf("1") - 1; } else { power = -127; }

                }
                exponent = Convert.ToString(power + 127, 2);
                string bu = "00000000";
                exponent = bu.Substring(0, 8 - exponent.Length) + exponent;
                //ָ���������

                dataString = sign + exponent + mantissa;
            }
            catch (Exception e)
            {
                isOK = false;
                string error =  e.Message;
            }
            return isOK;
        }

    }

}
