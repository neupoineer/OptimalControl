using System.Drawing;
using ZedGraph;

namespace Model.Control
{
    /// <summary>
    /// 曲线模型
    /// </summary>
    public class Curve : ModelBase
    {
        #region Private Members
        private PointPairList _dataList = new PointPairList();
        private string _variableCode;
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
        #endregion

        #region Public Properties
        /// <summary>
        /// 数据数组
        /// </summary>
        public PointPairList DataList
        {
            get { return _dataList; }
            set { _dataList = value; }
        }

        /// <summary>
        /// 设备ID
        /// </summary>
        public int DeviceID
        {
            get { return _deviceId; }
            set { _deviceId = value; }
        }

        /// <summary>
        /// 变量地址
        /// </summary>
        public ushort Address
        {
            get { return _address; }
            set { _address = value; }
        }

        /// <summary>
        /// 曲线颜色
        /// </summary>
        public Color LineColor
        {
            get { return _lineColor; }
            set { _lineColor = value; }
        }

        /// <summary>
        /// 曲线类型
        /// </summary>
        public bool LineType
        {
            get { return _lineType; }
            set { _lineType = value; }
        }

        /// <summary>
        /// 线宽
        /// </summary>
        public float LineWidth
        {
            get { return _lineWidth; }
            set { _lineWidth = value; }
        }

        /// <summary>
        /// 符号类型
        /// </summary>
        public SymbolType SymbolType
        {
            get { return _symbolType; }
            set { _symbolType = value; }
        }

        /// <summary>
        /// 符号大小
        /// </summary>
        public float SymbolSize
        {
            get { return _symbolSize; }
            set { _symbolSize = value; }
        }

        /// <summary>
        /// 横轴标题
        /// </summary>
        public string XTitle
        {
            get { return _xTitle; }
            set { _xTitle = value; }
        }

        /// <summary>
        /// 纵轴标题
        /// </summary>
        public string YTitle
        {
            get { return _yTitle; }
            set { _yTitle = value; }
        }

        /// <summary>
        /// 纵轴最大值
        /// </summary>
        public double YMax
        {
            get { return _yMax; }
            set { _yMax = value; }
        }

        /// <summary>
        /// 纵轴最小值
        /// </summary>
        public double YMin
        {
            get { return _yMin; }
            set { _yMin = value; }
        }

        /// <summary>
        /// 变量编码
        /// </summary>
        public string VariableCode
        {
            get { return _variableCode; }
            set { _variableCode = value; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 无参构造
        /// </summary>
        public Curve()
        {
        }

        /// <summary>
        /// 带参构造
        /// </summary>
        public Curve(
            int id, 
            string name, 
            PointPairList dataList, 
            int deviceId, 
            ushort address, 
            Color lineColour,
            bool lineType, 
            float lineWidth, 
            SymbolType curveSymbolType, 
            float symbolSize, 
            string xAxisTitle,
            string yAxisTitle, 
            double yAxisMax, 
            double yAxisMin, 
            string variableCode) 
            : base(id, name)
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
            VariableCode = variableCode;
        }
        #endregion
    }
}
