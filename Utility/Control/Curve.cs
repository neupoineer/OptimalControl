using System.Drawing;
using ZedGraph;

namespace Utility.Control
{
    /// <summary>
    /// The Curve property
    /// </summary>
    public class Curve:ModelBase
    {
        private PointPairList _dataList;
        private int _deviceId;
        private ushort _address;
        private Color _lineColour;
        private bool _lineType;
        private float _lineWidth;
        private SymbolType _curveSymbolType;
        private float _symbolSize;
        private string _xAxisTitle;
        private string _yAxisTitle;
        private double _yAxisMax;
        private double _yAxisMin;

        /// <summary>
        /// The curve list
        /// </summary>
        public PointPairList DataList
        {
            get { return _dataList; }
            set { _dataList = value; }
        }

        /// <summary>
        /// The device identifier
        /// </summary>
        public int DeviceId
        {
            get { return _deviceId; }
            set { _deviceId = value; }
        }

        /// <summary>
        /// The address
        /// </summary>
        public ushort Address
        {
            get { return _address; }
            set { _address = value; }
        }

        /// <summary>
        /// The curve line colour
        /// </summary>
        public Color LineColour
        {
            get { return _lineColour; }
            set { _lineColour = value; }
        }

        /// <summary>
        /// The curve line type
        /// </summary>
        public bool LineType
        {
            get { return _lineType; }
            set { _lineType = value; }
        }

        /// <summary>
        /// The curve line width
        /// </summary>
        public float LineWidth
        {
            get { return _lineWidth; }
            set { _lineWidth = value; }
        }

        /// <summary>
        /// The curve symbol type
        /// </summary>
        public SymbolType CurveSymbolType
        {
            get { return _curveSymbolType; }
            set { _curveSymbolType = value; }
        }

        /// <summary>
        /// The curve symbol size
        /// </summary>
        public float SymbolSize
        {
            get { return _symbolSize; }
            set { _symbolSize = value; }
        }

        /// <summary>
        /// The x axis title
        /// </summary>
        public string XAxisTitle
        {
            get { return _xAxisTitle; }
            set { _xAxisTitle = value; }
        }

        /// <summary>
        /// The y axis title
        /// </summary>
        public string YAxisTitle
        {
            get { return _yAxisTitle; }
            set { _yAxisTitle = value; }
        }

        /// <summary>
        /// The pane Y axis max
        /// </summary>
        public double YAxisMax
        {
            get { return _yAxisMax; }
            set { _yAxisMax = value; }
        }

        /// <summary>
        /// The pane Y axis min
        /// </summary>
        public double YAxisMin
        {
            get { return _yAxisMin; }
            set { _yAxisMin = value; }
        }

        public Curve()
        {
            
        }

        public Curve(PointPairList dataList, int deviceId, ushort address, Color lineColour, bool lineType, float lineWidth, SymbolType curveSymbolType, float symbolSize, string xAxisTitle, string yAxisTitle, double yAxisMax, double yAxisMin)
        {
            DataList = dataList;
            DeviceId = deviceId;
            Address = address;
            LineColour = lineColour;
            LineType = lineType;
            LineWidth = lineWidth;
            CurveSymbolType = curveSymbolType;
            SymbolSize = symbolSize;
            XAxisTitle = xAxisTitle;
            YAxisTitle = yAxisTitle;
            YAxisMax = yAxisMax;
            YAxisMin = yAxisMin;
        }
    }

}
