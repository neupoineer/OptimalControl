using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Control.Postfix
{
    /// <summary>
    /// 操作符类
    /// </summary>
    public class Operators
    {
        public Operators(OperatorsType type, string value)
        {
            this.Type = type;
            this.Value = value;
        }

        /// <summary>
        /// 运算符类型
        /// </summary>
        public OperatorsType Type { get; set; }

        /// <summary>
        /// 运算符值
        /// </summary>
        public string Value { get; set; }


        /// <summary>
        /// 对于>或者&lt;运算符，判断实际是否为>=,&lt;&gt;、&lt;=，并调整当前运算符位置
        /// </summary>
        /// <param name="currentOpt">当前运算符</param>
        /// <param name="currentExp">当前表达式</param>
        /// <param name="currentOptPos">当前运算符位置</param>
        /// <returns>返回调整后的运算符</returns>
        public static string AdjustOperator(string currentOpt, string currentExp, ref int currentOptPos)
        {
            switch (currentOpt)
            {
                case "<":
                    if (currentExp.Substring(currentOptPos, 2) == "<=")
                    {
                        currentOptPos++;
                        return "<=";
                    }
                    if (currentExp.Substring(currentOptPos, 2) == "<>")
                    {
                        currentOptPos++;
                        return "<>";
                    }
                    return "<";

                case ">":
                    if (currentExp.Substring(currentOptPos, 2) == ">=")
                    {
                        currentOptPos++;
                        return ">=";
                    }
                    return ">";
                case "t":
                    if (currentExp.Substring(currentOptPos, 3) == "tan")
                    {
                        currentOptPos += 2;
                        return "tan";
                    }
                    return "error";
                case "a":
                    if (currentExp.Substring(currentOptPos, 4) == "atan")
                    {
                        currentOptPos += 3;
                        return "atan";
                    }
                    return "error";
                default:
                    return currentOpt;
            }
        }

        /// <summary>
        /// 转换运算符到指定的类型
        /// </summary>
        /// <param name="opt">运算符</param>
        /// <param name="isBinaryOperator">是否为二元运算符</param>
        /// <returns>返回指定的运算符类型</returns>
        public static OperatorsType ConvertOperator(string opt, bool isBinaryOperator)
        {
            switch (opt)
            {
                case "!": return OperatorsType.NOT;
                case "+": return isBinaryOperator ? OperatorsType.ADD : OperatorsType.PS;
                case "-": return isBinaryOperator ? OperatorsType.SUB : OperatorsType.NS;
                case "*": return isBinaryOperator ? OperatorsType.MUL : OperatorsType.ERR;
                case "/": return isBinaryOperator ? OperatorsType.DIV : OperatorsType.ERR;
                case "%": return isBinaryOperator ? OperatorsType.MOD : OperatorsType.ERR;
                case "<": return isBinaryOperator ? OperatorsType.LT : OperatorsType.ERR;
                case ">": return isBinaryOperator ? OperatorsType.GT : OperatorsType.ERR;
                case "<=": return isBinaryOperator ? OperatorsType.LE : OperatorsType.ERR;
                case ">=": return isBinaryOperator ? OperatorsType.GE : OperatorsType.ERR;
                case "<>": return isBinaryOperator ? OperatorsType.UT : OperatorsType.ERR;
                case "=": return isBinaryOperator ? OperatorsType.ET : OperatorsType.ERR;
                case "&": return isBinaryOperator ? OperatorsType.AND : OperatorsType.ERR;
                case "|": return isBinaryOperator ? OperatorsType.OR : OperatorsType.ERR;
                case ",": return isBinaryOperator ? OperatorsType.CA : OperatorsType.ERR;
                case "$": return isBinaryOperator ? OperatorsType.END : OperatorsType.ERR;
                default: return OperatorsType.ERR;
            }
        }

        /// <summary>
        /// 转换运算符到指定的类型
        /// </summary>
        /// <param name="opt">运算符</param>
        /// <returns>返回指定的运算符类型</returns>
        public static OperatorsType ConvertOperator(string opt)
        {
            switch (opt)
            {
                case "!": return OperatorsType.NOT;
                case "+": return OperatorsType.ADD;
                case "-": return OperatorsType.SUB;
                case "*": return OperatorsType.MUL;
                case "/": return OperatorsType.DIV;
                case "%": return OperatorsType.MOD;
                case "<": return OperatorsType.LT;
                case ">": return OperatorsType.GT;
                case "<=": return OperatorsType.LE;
                case ">=": return OperatorsType.GE;
                case "<>": return OperatorsType.UT;
                case "=": return OperatorsType.ET;
                case "&": return OperatorsType.AND;
                case "|": return OperatorsType.OR;
                case ",": return OperatorsType.CA;
                case "$": return OperatorsType.END;
                case "tan": return OperatorsType.TAN;
                case "atan": return OperatorsType.ATAN;
                default: return OperatorsType.ERR;
            }
        }

        /// <summary>
        /// 运算符是否为二元运算符,该方法有问题，暂不使用
        /// </summary>
        /// <param name="tokens">语法单元堆栈</param>
        /// <param name="operators">运算符堆栈</param>
        /// <param name="currentOpd">当前操作数</param>
        /// <returns>是返回真,否返回假</returns>
        public static bool IsBinaryOperator(ref Stack<object> tokens, ref Stack<Operators> operators, string currentOpd)
        {
            if (currentOpd != "")
            {
                return true;
            }
            else
            {
                object token = tokens.Peek();
                if (token is Operand)
                {
                    if (operators.Peek().Type != OperatorsType.LB)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (((Operators)token).Type == OperatorsType.RB)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 运算符优先级比较
        /// </summary>
        /// <param name="optA">运算符类型A</param>
        /// <param name="optB">运算符类型B</param>
        /// <returns>A与B相比，-1，低；0,相等；1，高</returns>
        public static int ComparePriority(OperatorsType optA, OperatorsType optB)
        {
            if (optA == optB)
            {
                //A、B优先级相等
                return 0;
            }

            //乘,除,余(*,/,%)
            if ((optA >= OperatorsType.MUL && optA <= OperatorsType.MOD) &&
                (optB >= OperatorsType.MUL && optB <= OperatorsType.MOD))
            {
                return 0;
            }
            //加,减(+,-)
            if ((optA >= OperatorsType.ADD && optA <= OperatorsType.SUB) &&
                (optB >= OperatorsType.ADD && optB <= OperatorsType.SUB))
            {
                return 0;
            }
            //小于,小于或等于,大于,大于或等于(<,<=,>,>=)
            if ((optA >= OperatorsType.LT && optA <= OperatorsType.GE) &&
                (optB >= OperatorsType.LT && optB <= OperatorsType.GE))
            {
                return 0;
            }
            //等于,不等于(=,<>)
            if ((optA >= OperatorsType.ET && optA <= OperatorsType.UT) &&
                (optB >= OperatorsType.ET && optB <= OperatorsType.UT))
            {
                return 0;
            }
            //三角函数
            if ((optA>=OperatorsType.TAN && optA<=OperatorsType.ATAN)&&
                    (optB >= OperatorsType.TAN && optB <= OperatorsType.ATAN))
            {
                return 0;
            }

            if (optA < optB)
            {
                //A优先级高于B
                return 1;
            }

            //A优先级低于B
            return -1;

        }
    }
}
