namespace Model.Control.Postfix
{
    /// <summary>
    /// 操作数类型
    /// </summary>
    public enum OperandType
    {
        /// <summary>
        /// 函数
        /// </summary>
        FUNC = 1,

        /// <summary>
        /// 日期
        /// </summary>
        DATE = 2,

        /// <summary>
        /// 数字
        /// </summary>
        NUMBER = 3,

        /// <summary>
        /// 布尔
        /// </summary>
        BOOLEAN = 4,

        /// <summary>
        /// 字符串
        /// </summary>
        STRING = 5

    }
}
