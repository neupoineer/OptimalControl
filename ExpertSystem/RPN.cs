using System;
using System.Collections.Generic;

namespace ExpertSystem
{
    /// <summary>
    /// Reverse Polish Notation
    /// 逆波兰式类
    /// </summary>
    public class RPN
    {
        private Stack<object> m_tokens = new Stack<object>();
        private string _RPNExpression;

        /// <summary>
        /// 最终逆波兰式堆栈
        /// </summary>
        public Stack<object> Tokens
        {
            get { return m_tokens; }
        }

        /// <summary>
        /// 生成的逆波兰式字符串
        /// </summary>
        public string RPNExpression
        {
            get
            {
                if (_RPNExpression == null)
                {
                    foreach (var item in Tokens)
                    {
                        if (item is Operand)
                        {
                            _RPNExpression += ((Operand) item).Value + ",";
                        }
                        if (item is Operators)
                        {
                            _RPNExpression += ((Operators) item).Value + ",";
                        }
                    }
                }
                return _RPNExpression;
            }
        }

        /// <summary>
        /// 允许使用的运算符
        /// </summary>
        private List<string> m_Operators = new List<string>
            (new string[]
            {
                "(", "tan", ")", "atan", "!", "*", "/", "%", "+", "-", "<", ">", "=", "&", "|", ",", "$"
            });

        /// <summary>
        /// 检查表达式中特殊符号(双引号、单引号、井号、左右括号)是否匹配
        /// </summary>
        /// <param name="exp">表达式.</param>
        /// <returns></returns>
        private bool IsMatching(string exp)
        {
            string opt = ""; //临时存储 " ' # (

            for (int i = 0; i < exp.Length; i++)
            {
                string chr = exp.Substring(i, 1); //读取每个字符
                if ("\"'#".Contains(chr)) //当前字符是双引号、单引号、井号的一种
                {
                    if (opt.Contains(chr)) //之前已经读到过该字符
                    {
                        opt = opt.Remove(opt.IndexOf(chr), 1); //移除之前读到的该字符，即匹配的字符
                    }
                    else
                    {
                        opt += chr; //第一次读到该字符时，存储
                    }
                }
                else if ("()".Contains(chr)) //左右括号
                {
                    if (chr == "(")
                    {
                        opt += chr;
                    }
                    else if (chr == ")")
                    {
                        if (opt.Contains("("))
                        {
                            opt = opt.Remove(opt.IndexOf("("), 1);
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return (opt == "");
        }

        /// <summary>
        /// 从表达式中查找运算符位置
        /// </summary>
        /// <param name="exp">表达式</param>
        /// <param name="findOpt">要查找的运算符</param>
        /// <returns>返回运算符位置</returns>
        private int FindOperator(string exp, string findOpt)
        {
            string opt = "";
            for (int i = 0; i < exp.Length; i++)
            {
                string chr = exp.Substring(i, 1);
                if ("\"'#".Contains(chr)) //忽略双引号、单引号、井号中的运算符
                {
                    if (opt.Contains(chr))
                    {
                        opt = opt.Remove(opt.IndexOf(chr), 1);
                    }
                    else
                    {
                        opt += chr;
                    }
                }
                if (opt == "")
                {
                    if (findOpt != "")
                    {
                        if (findOpt == chr)
                        {
                            return i;
                        }
                    }
                    else
                    {
                        if (m_Operators.Exists(x => x.Contains(chr)))
                        {
                            return i;
                        }
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// 语法解析,将中缀表达式转换成后缀表达式
        /// </summary>
        /// <param name="exp">中缀表达式</param>
        /// <returns></returns>
        public bool Parse(string exp)
        {
            m_tokens.Clear(); //清空语法单元堆栈
            if (exp.Trim() == "") //表达式不能为空
            {
                return false;
            }
            else if (!this.IsMatching(exp)) //括号、引号、单引号等必须配对
            {
                return false;
            }

            Stack<object> operands = new Stack<object>(); //操作数堆栈
            Stack<Operators> operators = new Stack<Operators>(); //运算符堆栈
            OperatorsType optType = OperatorsType.ERR; //运算符类型
            string curOpd = ""; //当前操作数
            string curOpt = ""; //当前运算符
            int curPos = 0; //当前位置
            //int funcCount = 0;                                        //函数数量

            curPos = FindOperator(exp, "");

            exp += "$"; //结束操作符
            while (true)
            {
                curPos = FindOperator(exp, "");

                curOpd = exp.Substring(0, curPos).Trim();
                curOpt = exp.Substring(curPos, 1);

                //////////////测试代码///////////////////////////////////
                //System.Diagnostics.Debug.WriteLine("***************");
                //System.Diagnostics.Debug.WriteLine("当前读取的操作数：" + curOpd);

                //foreach (var item in operands.ToArray())
                //{
                //    if (item is Operand)
                //    {
                //        System.Diagnostics.Debug.WriteLine("操作数栈：" + ((Operand)item).Value);
                //    }
                //    if (item is Operator)
                //    {
                //        System.Diagnostics.Debug.WriteLine("操作数栈：" + ((Operator)item).Value);
                //    }
                //}

                //System.Diagnostics.Debug.WriteLine("当前读取的运算符：" + curOpt);
                //foreach (var item in operators.ToArray())
                //{
                //    System.Diagnostics.Debug.WriteLine("运算符栈：" + item.Value);
                //}
                ////////////////////////////////////////////////////////

                //存储当前操作数到操作数堆栈
                if (curOpd != "")
                {
                    operands.Push(new Operand(curOpd, curOpd));
                }

                //若当前运算符为结束运算符，则停止循环
                if (curOpt == "$")
                {
                    break;
                }
                //若当前运算符为左括号,则直接存入堆栈。
                if (curOpt == "(")
                {
                    operators.Push(new Operators(OperatorsType.LB, "("));
                    exp = exp.Substring(curPos + 1).Trim();
                    continue;
                }

                //若当前运算符为右括号,则依次弹出运算符堆栈中的运算符并存入到操作数堆栈,直到遇到左括号为止,此时抛弃该左括号.
                if (curOpt == ")")
                {
                    while (operators.Count > 0)
                    {
                        if (operators.Peek().Type != OperatorsType.LB)
                        {
                            operands.Push(operators.Pop());
                        }
                        else
                        {
                            operators.Pop();
                            break;
                        }
                    }
                    exp = exp.Substring(curPos + 1).Trim();
                    continue;
                }


                //调整运算符
                curOpt = Operators.AdjustOperator(curOpt, exp, ref curPos);

                optType = Operators.ConvertOperator(curOpt);

                //若运算符堆栈为空,或者若运算符堆栈栈顶为左括号,则将当前运算符直接存入运算符堆栈.
                if (operators.Count == 0 || operators.Peek().Type == OperatorsType.LB)
                {
                    operators.Push(new Operators(optType, curOpt));
                    exp = exp.Substring(curPos + 1).Trim();
                    continue;
                }

                //若当前运算符优先级大于运算符栈顶的运算符,则将当前运算符直接存入运算符堆栈.
                if (Operators.ComparePriority(optType, operators.Peek().Type) > 0)
                {
                    operators.Push(new Operators(optType, curOpt));
                }
                else
                {
                    //若当前运算符若比运算符堆栈栈顶的运算符优先级低或相等，则输出栈顶运算符到操作数堆栈，直至运算符栈栈顶运算符低于（不包括等于）该运算符优先级，
                    //或运算符栈栈顶运算符为左括号
                    //并将当前运算符压入运算符堆栈。
                    while (operators.Count > 0)
                    {
                        if (Operators.ComparePriority(optType, operators.Peek().Type) <= 0 &&
                            operators.Peek().Type != OperatorsType.LB)
                        {
                            operands.Push(operators.Pop());

                            if (operators.Count == 0)
                            {
                                operators.Push(new Operators(optType, curOpt));
                                break;
                            }
                        }
                        else
                        {
                            operators.Push(new Operators(optType, curOpt));
                            break;
                        }
                    }

                }
                exp = exp.Substring(curPos + 1).Trim();
            }
            //转换完成,若运算符堆栈中尚有运算符时,
            //则依序取出运算符到操作数堆栈,直到运算符堆栈为空
            while (operators.Count > 0)
            {
                operands.Push(operators.Pop());
            }
            //调整操作数栈中对象的顺序并输出到最终栈
            while (operands.Count > 0)
            {
                m_tokens.Push(operands.Pop());
            }

            return true;
        }

        /// <summary>
        /// 计算后缀表达式的值.
        /// </summary>
        /// <returns>
        /// 计算结果
        /// </returns>
        public object Evaluate()
        {
            /*
              逆波兰表达式求值算法：
              1、循环扫描语法单元的项目。
              2、如果扫描的项目是操作数，则将其压入操作数堆栈，并扫描下一个项目。
              3、如果扫描的项目是一个二元运算符，则对栈的顶上两个操作数执行该运算。
              4、如果扫描的项目是一个一元运算符，则对栈的最顶上操作数执行该运算。
              5、将运算结果重新压入堆栈。
              6、重复步骤2-5，堆栈中即为结果值。
            */

            if (m_tokens.Count == 0) return null;

            object value = null;
            Stack<Operand> opds = new Stack<Operand>();
            Stack<object> pars = new Stack<object>();
            Operand opdA, opdB;

            foreach (object item in m_tokens)
            {
                if (item is Operand)
                {
                    //TODO 解析公式，替换参数

                    //如果为操作数则压入操作数堆栈
                    opds.Push((Operand) item);
                }
                else
                {
                    switch (((Operators) item).Type)
                    {
                            #region 乘,*,multiplication

                        case OperatorsType.MUL:
                            opdA = opds.Pop();
                            opdB = opds.Pop();
                            if (Operand.IsNumber(opdA.Value) && Operand.IsNumber(opdB.Value))
                            {
                                opds.Push(new Operand(OperandType.NUMBER,
                                    double.Parse(opdB.Value.ToString())*double.Parse(opdA.Value.ToString())));
                            }
                            else
                            {
                                throw new Exception("乘运算的两个操作数必须均为数字");
                            }
                            break;

                            #endregion

                            #region 除,/,division

                        case OperatorsType.DIV:
                            opdA = opds.Pop();
                            opdB = opds.Pop();
                            if (Operand.IsNumber(opdA.Value) && Operand.IsNumber(opdB.Value))
                            {
                                opds.Push(new Operand(OperandType.NUMBER,
                                    double.Parse(opdB.Value.ToString())/double.Parse(opdA.Value.ToString())));
                            }
                            else
                            {
                                throw new Exception("除运算的两个操作数必须均为数字");
                            }
                            break;

                            #endregion

                            #region 余,%,modulus

                        case OperatorsType.MOD:
                            opdA = opds.Pop();
                            opdB = opds.Pop();
                            if (Operand.IsNumber(opdA.Value) && Operand.IsNumber(opdB.Value))
                            {
                                opds.Push(new Operand(OperandType.NUMBER,
                                    double.Parse(opdB.Value.ToString())%double.Parse(opdA.Value.ToString())));
                            }
                            else
                            {
                                throw new Exception("余运算的两个操作数必须均为数字");
                            }
                            break;

                            #endregion

                            #region 加,+,Addition

                        case OperatorsType.ADD:
                            opdA = opds.Pop();
                            opdB = opds.Pop();
                            if (Operand.IsNumber(opdA.Value) && Operand.IsNumber(opdB.Value))
                            {
                                opds.Push(new Operand(OperandType.NUMBER,
                                    double.Parse(opdB.Value.ToString()) + double.Parse(opdA.Value.ToString())));
                            }
                            else
                            {
                                throw new Exception("加运算的两个操作数必须均为数字");
                            }
                            break;

                            #endregion

                            #region 减,-,subtraction

                        case OperatorsType.SUB:
                            opdA = opds.Pop();
                            opdB = opds.Pop();
                            if (Operand.IsNumber(opdA.Value) && Operand.IsNumber(opdB.Value))
                            {
                                opds.Push(new Operand(OperandType.NUMBER,
                                    double.Parse(opdB.Value.ToString()) - double.Parse(opdA.Value.ToString())));
                            }
                            else
                            {
                                throw new Exception("减运算的两个操作数必须均为数字");
                            }
                            break;

                            #endregion

                            #region 正切,tan,subtraction

                        case OperatorsType.TAN:
                            opdA = opds.Pop();
                            if (Operand.IsNumber(opdA.Value))
                            {
                                opds.Push(new Operand(OperandType.NUMBER,
                                    Math.Tan(double.Parse(opdA.Value.ToString())*Math.PI/180)));
                            }
                            else
                            {
                                throw new Exception("正切运算的1个操作数必须均为角度数字");
                            }
                            break;

                            #endregion

                            #region 反正切,atan,subtraction

                        case OperatorsType.ATAN:
                            opdA = opds.Pop();
                            if (Operand.IsNumber(opdA.Value))
                            {
                                opds.Push(new Operand(OperandType.NUMBER, Math.Atan(double.Parse(opdA.Value.ToString()))));
                            }
                            else
                            {
                                throw new Exception("反正切运算的1个操作数必须均为数字");
                            }
                            break;

                            #endregion

                            #region  小于,<,less than

                        case OperatorsType.LT:
                            opdA = opds.Pop();
                            opdB = opds.Pop();
                            if (Operand.IsNumber(opdA.Value) && Operand.IsNumber(opdB.Value))
                            {
                                opds.Push(new Operand(OperandType.BOOLEAN,
                                    double.Parse(opdB.Value.ToString()) < double.Parse(opdA.Value.ToString())));
                            }
                            else
                            {
                                throw new Exception("小于运算的两个操作数必须均为数字");
                            }
                            break;

                            #endregion

                            #region  小于等于,<=,less than or equal to

                        case OperatorsType.LE:
                            opdA = opds.Pop();
                            opdB = opds.Pop();
                            if (Operand.IsNumber(opdA.Value) && Operand.IsNumber(opdB.Value))
                            {
                                opds.Push(new Operand(OperandType.BOOLEAN,
                                    double.Parse(opdB.Value.ToString()) <= double.Parse(opdA.Value.ToString())));
                            }
                            else
                            {
                                throw new Exception("小于等于运算的两个操作数必须均为数字");
                            }
                            break;

                            #endregion

                            #region  大于,>,greater than

                        case OperatorsType.GT:
                            opdA = opds.Pop();
                            opdB = opds.Pop();
                            if (Operand.IsNumber(opdA.Value) && Operand.IsNumber(opdB.Value))
                            {
                                opds.Push(new Operand(OperandType.BOOLEAN,
                                    double.Parse(opdB.Value.ToString()) > double.Parse(opdA.Value.ToString())));
                            }
                            else
                            {
                                throw new Exception("大于运算的两个操作数必须均为数字");
                            }
                            break;

                            #endregion

                            #region  大于等于,>=,greater than or equal to

                        case OperatorsType.GE:
                            opdA = opds.Pop();
                            opdB = opds.Pop();
                            if (Operand.IsNumber(opdA.Value) && Operand.IsNumber(opdB.Value))
                            {
                                opds.Push(new Operand(OperandType.BOOLEAN,
                                    double.Parse(opdB.Value.ToString()) >= double.Parse(opdA.Value.ToString())));
                            }
                            else
                            {
                                throw new Exception("大于等于运算的两个操作数必须均为数字");
                            }
                            break;

                            #endregion

                            #region  等于,=,equal to

                        case OperatorsType.ET:
                            opdA = opds.Pop();
                            opdB = opds.Pop();
                            if (Operand.IsNumber(opdA.Value) && Operand.IsNumber(opdB.Value))
                            {
                                opds.Push(new Operand(OperandType.BOOLEAN,
                                    double.Parse(opdB.Value.ToString()) == double.Parse(opdA.Value.ToString())));
                            }
                            else
                            {
                                throw new Exception("等于运算的两个操作数必须均为数字");
                            }
                            break;

                            #endregion

                            #region  不等于,<>,unequal to

                        case OperatorsType.UT:
                            opdA = opds.Pop();
                            opdB = opds.Pop();
                            if (Operand.IsNumber(opdA.Value) && Operand.IsNumber(opdB.Value))
                            {
                                opds.Push(new Operand(OperandType.BOOLEAN,
                                    double.Parse(opdB.Value.ToString()) != double.Parse(opdA.Value.ToString())));
                            }
                            else
                            {
                                throw new Exception("不等于运算的两个操作数必须均为数字");
                            }
                            break;

                            #endregion

                            #region  逻辑非,!,NOT

                        case OperatorsType.NOT:
                            opdA = opds.Pop();
                            if (Operand.IsBool(opdA.Value))
                            {
                                opds.Push(new Operand(OperandType.BOOLEAN, !bool.Parse(opdA.Value.ToString())));
                            }
                            else
                            {
                                throw new Exception("逻辑非运算的操作数必须为布尔值");
                            }
                            break;

                            #endregion

                            #region  逻辑与,&,AND

                        case OperatorsType.AND:
                            opdA = opds.Pop();
                            opdB = opds.Pop();
                            if (Operand.IsBool(opdA.Value) && Operand.IsBool(opdB.Value))
                            {
                                opds.Push(new Operand(OperandType.BOOLEAN,
                                    bool.Parse(opdB.Value.ToString()) & bool.Parse(opdA.Value.ToString())));
                            }
                            else
                            {
                                throw new Exception("逻辑与运算的两个操作数必须均为布尔值");
                            }
                            break;

                            #endregion

                            #region  逻辑或,|,OR

                        case OperatorsType.OR:
                            opdA = opds.Pop();
                            opdB = opds.Pop();
                            if (Operand.IsBool(opdA.Value) && Operand.IsBool(opdB.Value))
                            {
                                opds.Push(new Operand(OperandType.BOOLEAN,
                                    bool.Parse(opdB.Value.ToString()) | bool.Parse(opdA.Value.ToString())));
                            }
                            else
                            {
                                throw new Exception("逻辑或运算的两个操作数必须均为布尔值");
                            }
                            break;

                            #endregion

                    }
                }
            }

            if (opds.Count == 1)
            {
                value = opds.Pop().Value;
            }

            return value;
        }

    }
}
