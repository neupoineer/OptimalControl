using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OptimalControl.Common
{
    /// <summary>
    /// 权限菜单数据管理类
    /// </summary>
    internal class RightsMenuDataManager
    {
        #region Private Members
        /// <summary>
        /// 保存当前被管理的菜单对象
        /// </summary>
        MenuStrip _msMain = null;
        /// <summary>
        /// 保存当前权限项 ID
        /// </summary>
        int _rightsId = 0; 
        #endregion

        #region Internal Properties
        /// <summary>
        /// 被管理的菜单对象
        /// </summary>
        internal MenuStrip MsMain
        {
            get { return _msMain; }
            set { _msMain = value; }
        } 
        #endregion

        #region Private Methods
        /// <summary>
        /// 使用递归法读取菜单权限子项
        /// </summary>
        /// <param name="rightCollection">要保存到的菜单根项权限集合</param>
        /// <param name="tsmiRootLevel">当前菜单权限相对根项</param>
        private Model.Rights GetMenuRightsChildrenItem(Dictionary<string, Model.Rights> rightCollection, ToolStripMenuItem tsmiRootLevel)
        {
            // 显示下级菜单项元素 Text
            Model.Rights secondRights = null;

            // 使用 ToolStripItem 基类遍历获取下级菜单项
            foreach (ToolStripItem tsmiNextLevel in tsmiRootLevel.DropDownItems)
            {
                // ID 号累加
                _rightsId++;
                // 显示下级菜单项元素 Text
                secondRights = new Model.Rights();
                secondRights.Id = _rightsId;
                secondRights.ModelName = tsmiNextLevel.Name;
                secondRights.RightsState = false;
                secondRights.ParentLevelRightsName = rightCollection[tsmiRootLevel.Name].ModelName;

                // 如果是菜单项而不是其它菜单项类型
                if (tsmiNextLevel is ToolStripMenuItem)
                {
                    secondRights.RightsCaption = tsmiNextLevel.Text;
                    // 添加当前项到集合
                    rightCollection.Add(secondRights.ModelName, secondRights);
                    // 使用递归添加所有次级子项到集合
                    GetMenuRightsChildrenItem(rightCollection, tsmiNextLevel as ToolStripMenuItem);
                }
                // 如果是分隔项而不是其它菜单项类型
                else if (tsmiNextLevel is ToolStripSeparator)
                {
                    secondRights.RightsCaption = "━━━━";
                     // 添加此菜单分隔项到集合
                    rightCollection.Add((tsmiNextLevel as ToolStripSeparator).Name, secondRights);
                }
            }

            return secondRights;
        }
        /// <summary>
        /// 使用递归法加载菜单权限子项
        /// </summary>
        /// <param name="rightCollection">权限集合</param>
        /// <param name="tsmiRootLevel">当前菜单权限相对根项</param>
        private void LoadMenuRightsChildrenItem(Dictionary<string, Model.Rights> rightCollection, ToolStripMenuItem tsmiRootLevel)
        {
            // 使用 ToolStripItem 基类遍历获取下级菜单项
            foreach (ToolStripItem tsmiNextLevel in tsmiRootLevel.DropDownItems)
            {
                foreach (Model.Rights tmpRights in rightCollection.Values)
                {
                    // 如果是菜单项而不是其它菜单项类型
                    if (tsmiNextLevel is ToolStripMenuItem)
                    {
                        // 如果内部名称相同
                        if (tsmiNextLevel.Name == tmpRights.ModelName)
                        {
                            // 设置名称和显隐状态
                            tsmiNextLevel.Text = tmpRights.RightsCaption;
                            //tsmiNextLevel.Visible = tmpRights.RightsState;
                            tsmiNextLevel.Enabled = tmpRights.RightsState;  // 防止菜单项快捷键激发事件

                            // 使用递归加载所有次级子项
                            LoadMenuRightsChildrenItem(rightCollection, tsmiNextLevel as ToolStripMenuItem);
                            break;
                        }
                    }
                    // 如果是分隔项而不是其它菜单项类型
                    else if (tsmiNextLevel is ToolStripSeparator)
                    {
                        // 如果内部名称相同
                        if (tsmiNextLevel.Name == tmpRights.ModelName)
                        {
                            // 设置显隐状态
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
        /// 使用递归法读取菜单权限项
        /// </summary>
        /// <param name="rightCollection">要保存到的目标权限集合</param>
        /// <returns>权限集合</returns>
        internal Dictionary<string, Model.Rights> ReadMenuRightsItem(Dictionary<string, Model.Rights> rightCollection)
        {
            if (!(this.MsMain is MenuStrip))
                throw new Exception("未指定被管理的菜单对象！请使用本方法的另一重载指定菜单对象！");

            // 调用另一重载
            return this.ReadMenuRightsItem(this._msMain, rightCollection);
        }
        /// <summary>
        /// 使用递归法读取菜单权限项
        /// </summary>
        /// <param name="msCurrentMenu">当前被管理的菜单对象</param>
        /// <param name="rightCollection">要保存到的目标权限集合</param>
        /// <returns>权限集合</returns>
        internal Dictionary<string, Model.Rights> ReadMenuRightsItem(MenuStrip msCurrentMenu, Dictionary<string, Model.Rights> rightCollection)
        {
            // 遍历获取菜单根项
            foreach (ToolStripMenuItem tsmiRootLevel in msCurrentMenu.Items)
            {
                if (tsmiRootLevel is ToolStripMenuItem)
                {
                    // ID 号累加
                    _rightsId++;
                    // 显示菜单根项元素 Text
                    Model.Rights rootRights = new Model.Rights();
                    rootRights.Id = _rightsId;
                    rootRights.ModelName = tsmiRootLevel.Name;
                    rootRights.RightsCaption = tsmiRootLevel.Text;
                    rootRights.RightsState = false;
                    rootRights.ParentLevelRightsName = msCurrentMenu.Name;

                    // 如果未添加此权限则添加此权限到菜单根项的权限集合
                    bool isExist = false;
                    foreach (Model.Rights tmpRights in rightCollection.Values)
                    {
                        if (tmpRights.ModelName == rootRights.ModelName)
                            isExist = true;
                    }
                    if (!isExist)
                        rightCollection.Add(rootRights.ModelName, rootRights);

                    // 使用递归添加所有子项
                    GetMenuRightsChildrenItem(rightCollection, tsmiRootLevel);
                }
            }

            return rightCollection;
        }
        /// <summary>
        /// 使用递归法加载菜单权限项
        /// </summary>
        /// <param name="rightCollection">要保存到的目标权限集合</param>
        /// <returns>权限集合</returns>
        internal void LoadMenuRightsItem(Dictionary<string, Model.Rights> rightCollection)
        {
            if (!(this.MsMain is MenuStrip))
                throw new Exception("未指定被管理的菜单对象！请使用本方法的另一重载指定菜单对象！");

            // 调用另一重载
            this.LoadMenuRightsItem(this._msMain, rightCollection);
        }
        /// <summary>
        /// 使用递归法加载菜单权限项
        /// </summary>
        /// <param name="msCurrentMenu">当前被管理的菜单对象</param>
        /// <param name="rightCollection">权限集合</param>
        internal void LoadMenuRightsItem(MenuStrip msCurrentMenu, Dictionary<string, Model.Rights> rightCollection)
        {
            foreach (Model.Rights tmpRights in rightCollection.Values)
            {
                // 遍历获取菜单根项
                foreach (ToolStripMenuItem tsmiRootLevel in msCurrentMenu.Items)
                {
                    if (tsmiRootLevel is ToolStripMenuItem)
                    {
                        // 如果内部名称相同
                        if (tsmiRootLevel.Name == tmpRights.ModelName)
                        {
                            // 设置名称和显隐状态
                            tsmiRootLevel.Text = tmpRights.RightsCaption;
                            //tsmiRootLevel.Visible = tmpRights.RightsState;
                            tsmiRootLevel.Enabled = tmpRights.RightsState;   // 防止菜单项快捷键激发事件
                            // 使用递归加载所有子项
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
