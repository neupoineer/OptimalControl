using System.Drawing;
using ZedGraph;

namespace Model.Control
{
    /// <summary>
    /// The Curve property
    /// </summary>
    public class Curve : ModelBase
    {
        private PointPairList _dataList = new PointPairList();
        private int _deviceId;
        private ushort _address;
        private Color _lineColor;
        private bool _lineType;
        private float _lineWidth;
        private SymbolType _symbolType;
        private float _symbolSize;
        private string _xTitle;
        private string _yTitle;
        private double _yMax;
        private double _yMin;

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
        public int DeviceID
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
        public Color LineColor
        {
            get { return _lineColor; }
            set { _lineColor = value; }
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
        public SymbolType SymbolType
        {
            get { return _symbolType; }
            set { _symbolType = value; }
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
        public string XTitle
        {
            get { return _xTitle; }
            set { _xTitle = value; }
        }

        /// <summary>
        /// The y axis title
        /// </summary>
        public string YTitle
        {
            get { return _yTitle; }
            set { _yTitle = value; }
        }

        /// <summary>
        /// The pane Y axis max
        /// </summary>
        public double YMax
        {
            get { return _yMax; }
            set { _yMax = value; }
        }

        /// <summary>
        /// The pane Y axis min
        /// </summary>
        public double YMin
        {
            get { return _yMin; }
            set { _yMin = value; }
        }

        public Curve()
        {

        }

        public Curve(PointPairList dataList, int deviceId, ushort address, Color lineColour, bool lineType,
            float lineWidth, SymbolType curveSymbolType, float symbolSize, string xAxisTitle, string yAxisTitle,
            double yAxisMax, double yAxisMin)
        {
            DataList = dataList;
            DeviceID = deviceId;
            Address = address;
            LineColor = lineColour;
            LineType = lineType;
            LineWidth = lineWidth;
            SymbolType = curveSymbolType;
            SymbolSize = symbolSize;
            XTitle = xAxisTitle;
            YTitle = yAxisTitle;
            YMax = yAxisMax;
            YMin = yAxisMin;
        }
    }

}
