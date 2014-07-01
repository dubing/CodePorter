using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using CodePorter.Core;

namespace CodePorter
{
    public partial class MainForm : Form
    {
        private TreeNode TableNode;
        private TreeNode ViewNode;

        public MainForm()
        {
            InitializeComponent();
        }

        #region 窗体事件
        private void MainForm_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            ClearLog();
            CodeGenerator.Instance.CurrentSettings = GetCollection(CodePorterSetting.GetCollection);
            CodeGenerator.Instance.CurrentSettings.Add(CodePorterSetting.ConnectionString, txtDBConnection.Text.Trim());
            CodeGenerator.Instance.CurrentSettings.Add(CodePorterSetting.DefaultNamespace, txtNameSpace.Text.Trim());
            CodeGenerator.Instance.CurrentSettings.Add(CodePorterSetting.DANamespace, txtDANameSpace.Text.Trim());
            LoadTreeView();
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            if (!CheckInput()) return;
            new Thread(GenerateTemplates).Start();
            txtLog.AppendText(CodePorterMessage.Running + CodePorterMessage.Line);
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            ClearLog();
        }

        private void treeViewDB_AfterCheck(object sender, TreeViewEventArgs e)
        {
            SetNodeChecked(e.Node, e.Node.Checked);
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            var newForm = new HelpForm { StartPosition = FormStartPosition.CenterScreen };
            newForm.ShowDialog();
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            string myFavoritesPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            System.Diagnostics.Process.Start(myFavoritesPath);
        }
        #endregion

        #region 自定义事件
        /// <summary>
        /// 获取匹配规则对象集合
        /// </summary>
        /// <returns></returns>
        private NameValueCollection GetCollection(string xmlRulePath)
        {
            var asm = Assembly.GetExecutingAssembly();
            NameValueCollection mrc;
            XmlDocument xd;
            using (var xmlres = asm.GetManifestResourceStream(CodePorterSetting.CodeSet))
            {
                mrc = new NameValueCollection();
                xd = new XmlDocument();
                if (xmlres != null) xd.Load(xmlres);
            }
            if (xd.DocumentElement != null)
            {
                var configNode = xd.DocumentElement.SelectSingleNode(xmlRulePath);
                if (configNode != null)
                    foreach (XmlNode node in configNode)
                    {
                        mrc.Add(node.Name, node.InnerText);
                    }
            }
            return mrc;
        }

        private void LoadTreeView()
        {
            treeViewDB.Nodes.Clear();
            toolStripStatusLabelDB.Text = CodePorterMessage.Loading;

            var checkedNodes = new List<string>(CodeGenerator.Instance.CurrentSettings[CodePorterSetting.CheckedNodes].Split(new[] { ',' }));

            var rootNode = treeViewDB.Nodes.Add(CodePorterSetting.RootNode);
            if (CodeGenerator.Instance.GetTableNames().Count == 0)
            {
                lblMessage.Text = CodePorterMessage.LoadRes;
                if (MessageBox.Show(CodePorterMessage.NoTable, "", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    ClearLog();
                    toolStripStatusLabelDB.Text = CodePorterMessage.Finish;
                    lblMessage.Text = CodePorterMessage.SetRightConn;
                    return;
                }
            }
            else
            {
                txtLog.AppendText(CodePorterMessage.LoadSuccess + CodePorterMessage.Line);
                toolStripStatusLabelDB.Text = CodePorterMessage.Ready;
            }
            TableNode = AddTreeNode(rootNode, checkedNodes, CodePorterSetting.Tables, CodeGenerator.Instance.GetTableNames());
            ViewNode = AddTreeNode(rootNode, checkedNodes, CodePorterSetting.Views, CodeGenerator.Instance.GetViewNames());
            rootNode.ExpandAll();
            toolStripStatusLabelDB.Text = CodePorterMessage.Finish;

        }

        private TreeNode AddTreeNode(TreeNode rootNode, List<string> checkedNodes, string text, IEnumerable<string> childNodes)
        {
            TreeNode node = rootNode.Nodes.Add(text);
            foreach (var name in childNodes)
            {
                node.Nodes.Add(name);
            }
            //if (checkedNodes.Contains(node.Text))
            //{
            //    node.Checked = true;
            //}
            return node;
        }

        private void SetNodeChecked(TreeNode node, bool isChecked)
        {
            foreach (TreeNode child in node.Nodes)
            {
                child.Checked = isChecked;
                SetNodeChecked(child, isChecked);
            }
        }

        private void ClearLog()
        {
            treeViewDB.Nodes.Clear();
            txtLog.Clear();
            lblMessage.Text = "";
            toolStripStatusLabelDB.Text = "";
        }

        private bool CheckInput()
        {
            if (string.IsNullOrWhiteSpace(txtDBConnection.Text))
            {
                lblMessage.Text = CodePorterMessage.SetConn;
                return false;
            }
            if (string.IsNullOrEmpty(txtNameSpace.Text))
            {
                lblMessage.Text = CodePorterMessage.SetNameSpace;
                return false;
            }
            if (treeViewDB.Nodes.Count == 0)
            {
                lblMessage.Text = CodePorterMessage.LoadRes;
                return false;
            }
            return true;
        }

        private void GenerateTemplates()
        {
            if (treeViewDB.Nodes.Count > 0)
            {
                toolStripStatusLabelDB.Text = CodePorterMessage.Running;
                try
                {
                    GenerateCode(CodePorterSetting.TableGenerator, TableNode);
                    GenerateCode(CodePorterSetting.ViewGenerator, ViewNode);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                toolStripStatusLabelDB.Text = CodePorterMessage.Finish;
            }
        }

        private void GenerateCode(string generatorKey, TreeNode generatorNode)
        {
            string generatorValue = CodeGenerator.Instance.CurrentSettings[generatorKey];
            if (!string.IsNullOrEmpty(generatorValue))
            {
                foreach (string generator in generatorValue.Split(new[] { ',' }))
                {
                    if (generatorNode.Nodes.Cast<TreeNode>().Any(node => node.Checked))
                    {
                        CodeGenerator.Instance.NeedMergeTarget(CodePorterSetting.GeneratorSettings + generator, true);
                    }
                    foreach (TreeNode node in generatorNode.Nodes)
                    {
                        if (node.Checked)
                        {
                            CodeGenerator.Instance.GenerateCode(CodePorterSetting.GeneratorSettings + generator, node.Text);
                        }
                    }
                }
            }
        }
        #endregion







    }
}
