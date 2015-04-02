using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OptimalControl.Common
{
    /// <summary>
    /// Ȩ�޲˵����ݹ�����
    /// </summary>
    internal class RightsMenuDataManager
    {
        #region Private Members
        /// <summary>
        /// ���浱ǰ������Ĳ˵�����
        /// </summary>
        MenuStrip _msMain = null;
        /// <summary>
        /// ���浱ǰȨ���� ID
        /// </summary>
        int _rightsId = 0; 
        #endregion

        #region Internal Properties
        /// <summary>
        /// ������Ĳ˵�����
        /// </summary>
        internal MenuStrip MsMain
        {
            get { return _msMain; }
            set { _msMain = value; }
        } 
        #endregion

        #region Private Methods
        /// <summary>
        /// ʹ�õݹ鷨��ȡ�˵�Ȩ������
        /// </summary>
        /// <param name="rightCollection">Ҫ���浽�Ĳ˵�����Ȩ�޼���</param>
        /// <param name="tsmiRootLevel">��ǰ�˵�Ȩ����Ը���</param>
        private Model.Rights GetMenuRightsChildrenItem(Dictionary<string, Model.Rights> rightCollection, ToolStripMenuItem tsmiRootLevel)
        {
            // ��ʾ�¼��˵���Ԫ�� Text
            Model.Rights secondRights = null;

            // ʹ�� ToolStripItem ���������ȡ�¼��˵���
            foreach (ToolStripItem tsmiNextLevel in tsmiRootLevel.DropDownItems)
            {
                // ID ���ۼ�
                _rightsId++;
                // ��ʾ�¼��˵���Ԫ�� Text
                secondRights = new Model.Rights();
                secondRights.Id = _rightsId;
                secondRights.ModelName = tsmiNextLevel.Name;
                secondRights.RightsState = false;
                secondRights.ParentLevelRightsName = rightCollection[tsmiRootLevel.Name].ModelName;

                // ����ǲ˵�������������˵�������
                if (tsmiNextLevel is ToolStripMenuItem)
                {
                    secondRights.RightsCaption = tsmiNextLevel.Text;
                    // ��ӵ�ǰ�����
                    rightCollection.Add(secondRights.ModelName, secondRights);
                    // ʹ�õݹ�������дμ��������
                    GetMenuRightsChildrenItem(rightCollection, tsmiNextLevel as ToolStripMenuItem);
                }
                // ����Ƿָ�������������˵�������
                else if (tsmiNextLevel is ToolStripSeparator)
                {
                    secondRights.RightsCaption = "��������";
                     // ��Ӵ˲˵��ָ������
                    rightCollection.Add((tsmiNextLevel as ToolStripSeparator).Name, secondRights);
                }
            }

            return secondRights;
        }
        /// <summary>
        /// ʹ�õݹ鷨���ز˵�Ȩ������
        /// </summary>
        /// <param name="rightCollection">Ȩ�޼���</param>
        /// <param name="tsmiRootLevel">��ǰ�˵�Ȩ����Ը���</param>
        private void LoadMenuRightsChildrenItem(Dictionary<string, Model.Rights> rightCollection, ToolStripMenuItem tsmiRootLevel)
        {
            // ʹ�� ToolStripItem ���������ȡ�¼��˵���
            foreach (ToolStripItem tsmiNextLevel in tsmiRootLevel.DropDownItems)
            {
                foreach (Model.Rights tmpRights in rightCollection.Values)
                {
                    // ����ǲ˵�������������˵�������
                    if (tsmiNextLevel is ToolStripMenuItem)
                    {
                        // ����ڲ�������ͬ
                        if (tsmiNextLevel.Name == tmpRights.ModelName)
                        {
                            // �������ƺ�����״̬
                            tsmiNextLevel.Text = tmpRights.RightsCaption;
                            //tsmiNextLevel.Visible = tmpRights.RightsState;
                            tsmiNextLevel.Enabled = tmpRights.RightsState;  // ��ֹ�˵����ݼ������¼�

                            // ʹ�õݹ�������дμ�����
                            LoadMenuRightsChildrenItem(rightCollection, tsmiNextLevel as ToolStripMenuItem);
                            break;
                        }
                    }
                    // ����Ƿָ�������������˵�������
                    else if (tsmiNextLevel is ToolStripSeparator)
                    {
                        // ����ڲ�������ͬ
                        if (tsmiNextLevel.Name == tmpRights.ModelName)
                        {
                            // ��������״̬
                            tsmiNextLevel.Visible = tmpRights.RightsState;
                            break;
                        }
                    }
                }
            }
        } 
        #endregion

        #region Internal Methods
        /// <summary>
        /// ʹ�õݹ鷨��ȡ�˵�Ȩ����
        /// </summary>
        /// <param name="rightCollection">Ҫ���浽��Ŀ��Ȩ�޼���</param>
        /// <returns>Ȩ�޼���</returns>
        internal Dictionary<string, Model.Rights> ReadMenuRightsItem(Dictionary<string, Model.Rights> rightCollection)
        {
            if (!(this.MsMain is MenuStrip))
                throw new Exception("δָ��������Ĳ˵�������ʹ�ñ���������һ����ָ���˵�����");

            // ������һ����
            return this.ReadMenuRightsItem(this._msMain, rightCollection);
        }
        /// <summary>
        /// ʹ�õݹ鷨��ȡ�˵�Ȩ����
        /// </summary>
        /// <param name="msCurrentMenu">��ǰ������Ĳ˵�����</param>
        /// <param name="rightCollection">Ҫ���浽��Ŀ��Ȩ�޼���</param>
        /// <returns>Ȩ�޼���</returns>
        internal Dictionary<string, Model.Rights> ReadMenuRightsItem(MenuStrip msCurrentMenu, Dictionary<string, Model.Rights> rightCollection)
        {
            // ������ȡ�˵�����
            foreach (ToolStripMenuItem tsmiRootLevel in msCurrentMenu.Items)
            {
                if (tsmiRootLevel is ToolStripMenuItem)
                {
                    // ID ���ۼ�
                    _rightsId++;
                    // ��ʾ�˵�����Ԫ�� Text
                    Model.Rights rootRights = new Model.Rights();
                    rootRights.Id = _rightsId;
                    rootRights.ModelName = tsmiRootLevel.Name;
                    rootRights.RightsCaption = tsmiRootLevel.Text;
                    rootRights.RightsState = false;
                    rootRights.ParentLevelRightsName = msCurrentMenu.Name;

                    // ���δ��Ӵ�Ȩ������Ӵ�Ȩ�޵��˵������Ȩ�޼���
                    bool isExist = false;
                    foreach (Model.Rights tmpRights in rightCollection.Values)
                    {
                        if (tmpRights.ModelName == rootRights.ModelName)
                            isExist = true;
                    }
                    if (!isExist)
                        rightCollection.Add(rootRights.ModelName, rootRights);

                    // ʹ�õݹ������������
                    GetMenuRightsChildrenItem(rightCollection, tsmiRootLevel);
                }
            }

            return rightCollection;
        }
        /// <summary>
        /// ʹ�õݹ鷨���ز˵�Ȩ����
        /// </summary>
        /// <param name="rightCollection">Ҫ���浽��Ŀ��Ȩ�޼���</param>
        /// <returns>Ȩ�޼���</returns>
        internal void LoadMenuRightsItem(Dictionary<string, Model.Rights> rightCollection)
        {
            if (!(this.MsMain is MenuStrip))
                throw new Exception("δָ��������Ĳ˵�������ʹ�ñ���������һ����ָ���˵�����");

            // ������һ����
            this.LoadMenuRightsItem(this._msMain, rightCollection);
        }
        /// <summary>
        /// ʹ�õݹ鷨���ز˵�Ȩ����
        /// </summary>
        /// <param name="msCurrentMenu">��ǰ������Ĳ˵�����</param>
        /// <param name="rightCollection">Ȩ�޼���</param>
        internal void LoadMenuRightsItem(MenuStrip msCurrentMenu, Dictionary<string, Model.Rights> rightCollection)
        {
            foreach (Model.Rights tmpRights in rightCollection.Values)
            {
                // ������ȡ�˵�����
                foreach (ToolStripMenuItem tsmiRootLevel in msCurrentMenu.Items)
                {
                    if (tsmiRootLevel is ToolStripMenuItem)
                    {
                        // ����ڲ�������ͬ
                        if (tsmiRootLevel.Name == tmpRights.ModelName)
                        {
                            // �������ƺ�����״̬
                            tsmiRootLevel.Text = tmpRights.RightsCaption;
                            //tsmiRootLevel.Visible = tmpRights.RightsState;
                            tsmiRootLevel.Enabled = tmpRights.RightsState;   // ��ֹ�˵����ݼ������¼�
                            // ʹ�õݹ������������
                            LoadMenuRightsChildrenItem(rightCollection, tsmiRootLevel);
                            break;
                        }
                    }
                }
            }
        } 
        #endregion
    }
}
