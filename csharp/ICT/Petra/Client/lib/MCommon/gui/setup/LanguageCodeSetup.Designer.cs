// auto generated with nant generateWinforms from LanguageCodeSetup.yaml
//
// DO NOT edit manually, DO NOT edit with the designer
//
//
// DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
//
// @Authors:
//       auto generated
//
// Copyright 2004-2010 by OM International
//
// This file is part of OpenPetra.org.
//
// OpenPetra.org is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// OpenPetra.org is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with OpenPetra.org.  If not, see <http://www.gnu.org/licenses/>.
//
using System;
using System.Windows.Forms;
using Mono.Unix;
using Ict.Common.Controls;
using Ict.Petra.Client.CommonControls;

namespace Ict.Petra.Client.MCommon.Gui.Setup
{
    partial class TFrmLanguageCodeSetup
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TFrmLanguageCodeSetup));

            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.grdDetails = new Ict.Common.Controls.TSgrdDataGridPaged();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnNew = new System.Windows.Forms.Button();
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtDetailLanguageCode = new System.Windows.Forms.TextBox();
            this.lblDetailLanguageCode = new System.Windows.Forms.Label();
            this.txtDetailLanguageDescription = new System.Windows.Forms.TextBox();
            this.lblDetailLanguageDescription = new System.Windows.Forms.Label();
            this.chkDetailCongressLanguage = new System.Windows.Forms.CheckBox();
            this.lblDetailCongressLanguage = new System.Windows.Forms.Label();
            this.chkDetailDeletable = new System.Windows.Forms.CheckBox();
            this.lblDetailDeletable = new System.Windows.Forms.Label();
            this.tbrMain = new System.Windows.Forms.ToolStrip();
            this.tbbSave = new System.Windows.Forms.ToolStripButton();
            this.tbbNew = new System.Windows.Forms.ToolStripButton();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mniFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mniFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mniSeparator0 = new System.Windows.Forms.ToolStripSeparator();
            this.mniFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mniSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mniClose = new System.Windows.Forms.ToolStripMenuItem();
            this.mniEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mniEditUndoCurrentField = new System.Windows.Forms.ToolStripMenuItem();
            this.mniEditUndoScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.mniSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mniEditFind = new System.Windows.Forms.ToolStripMenuItem();
            this.mniHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mniHelpPetraHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mniSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mniHelpBugReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mniSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mniHelpAboutPetra = new System.Windows.Forms.ToolStripMenuItem();
            this.mniHelpDevelopmentTeam = new System.Windows.Forms.ToolStripMenuItem();
            this.stbMain = new Ict.Common.Controls.TExtStatusBarHelp();

            this.pnlContent.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlDetails.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tbrMain.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.stbMain.SuspendLayout();

            //
            // pnlContent
            //
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.AutoSize = true;
            this.pnlContent.Controls.Add(this.pnlGrid);
            this.pnlContent.Controls.Add(this.pnlDetails);
            //
            // pnlGrid
            //
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.AutoSize = true;
            this.pnlGrid.Controls.Add(this.grdDetails);
            this.pnlGrid.Controls.Add(this.pnlButtons);
            //
            // grdDetails
            //
            this.grdDetails.Name = "grdDetails";
            this.grdDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDetails.Selection.FocusRowEntered += new SourceGrid.RowEventHandler(this.FocusedRowChanged);
            //
            // pnlButtons
            //
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlButtons.AutoSize = true;
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.AutoSize = true;
            this.pnlButtons.Controls.Add(this.tableLayoutPanel1);
            //
            // btnNew
            //
            this.btnNew.Location = new System.Drawing.Point(2,2);
            this.btnNew.Name = "btnNew";
            this.btnNew.AutoSize = true;
            this.btnNew.Click += new System.EventHandler(this.NewRecord);
            this.btnNew.Text = "New";
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Controls.Add(this.btnNew, 0, 0);
            //
            // pnlDetails
            //
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDetails.AutoSize = true;
            //
            // tableLayoutPanel2
            //
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.AutoSize = true;
            this.pnlDetails.Controls.Add(this.tableLayoutPanel2);
            //
            // txtDetailLanguageCode
            //
            this.txtDetailLanguageCode.Location = new System.Drawing.Point(2,2);
            this.txtDetailLanguageCode.Name = "txtDetailLanguageCode";
            this.txtDetailLanguageCode.Size = new System.Drawing.Size(150, 28);
            //
            // lblDetailLanguageCode
            //
            this.lblDetailLanguageCode.Location = new System.Drawing.Point(2,2);
            this.lblDetailLanguageCode.Name = "lblDetailLanguageCode";
            this.lblDetailLanguageCode.AutoSize = true;
            this.lblDetailLanguageCode.Text = "Language Code:";
            this.lblDetailLanguageCode.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.lblDetailLanguageCode.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblDetailLanguageCode.TextAlign = System.Drawing.ContentAlignment.TopRight;
            //
            // txtDetailLanguageDescription
            //
            this.txtDetailLanguageDescription.Location = new System.Drawing.Point(2,2);
            this.txtDetailLanguageDescription.Name = "txtDetailLanguageDescription";
            this.txtDetailLanguageDescription.Size = new System.Drawing.Size(145, 28);
            //
            // lblDetailLanguageDescription
            //
            this.lblDetailLanguageDescription.Location = new System.Drawing.Point(2,2);
            this.lblDetailLanguageDescription.Name = "lblDetailLanguageDescription";
            this.lblDetailLanguageDescription.AutoSize = true;
            this.lblDetailLanguageDescription.Text = "Language Description:";
            this.lblDetailLanguageDescription.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.lblDetailLanguageDescription.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblDetailLanguageDescription.TextAlign = System.Drawing.ContentAlignment.TopRight;
            //
            // chkDetailCongressLanguage
            //
            this.chkDetailCongressLanguage.Location = new System.Drawing.Point(2,2);
            this.chkDetailCongressLanguage.Name = "chkDetailCongressLanguage";
            this.chkDetailCongressLanguage.Size = new System.Drawing.Size(30, 28);
            this.chkDetailCongressLanguage.Text = "";
            this.chkDetailCongressLanguage.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            //
            // lblDetailCongressLanguage
            //
            this.lblDetailCongressLanguage.Location = new System.Drawing.Point(2,2);
            this.lblDetailCongressLanguage.Name = "lblDetailCongressLanguage";
            this.lblDetailCongressLanguage.AutoSize = true;
            this.lblDetailCongressLanguage.Text = "Congress Language:";
            this.lblDetailCongressLanguage.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.lblDetailCongressLanguage.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblDetailCongressLanguage.TextAlign = System.Drawing.ContentAlignment.TopRight;
            //
            // chkDetailDeletable
            //
            this.chkDetailDeletable.Location = new System.Drawing.Point(2,2);
            this.chkDetailDeletable.Name = "chkDetailDeletable";
            this.chkDetailDeletable.Size = new System.Drawing.Size(30, 28);
            this.chkDetailDeletable.Text = "";
            this.chkDetailDeletable.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            //
            // lblDetailDeletable
            //
            this.lblDetailDeletable.Location = new System.Drawing.Point(2,2);
            this.lblDetailDeletable.Name = "lblDetailDeletable";
            this.lblDetailDeletable.AutoSize = true;
            this.lblDetailDeletable.Text = "Deletable:";
            this.lblDetailDeletable.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.lblDetailDeletable.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblDetailDeletable.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Controls.Add(this.lblDetailLanguageCode, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblDetailCongressLanguage, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblDetailDeletable, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.txtDetailLanguageCode, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.chkDetailCongressLanguage, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.chkDetailDeletable, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblDetailLanguageDescription, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtDetailLanguageDescription, 3, 0);
            //
            // tbbSave
            //
            this.tbbSave.Name = "tbbSave";
            this.tbbSave.AutoSize = true;
            this.tbbSave.Click += new System.EventHandler(this.FileSave);
            this.tbbSave.Image = ((System.Drawing.Bitmap)resources.GetObject("tbbSave.Glyph"));
            this.tbbSave.ToolTipText = "Saves changed data";
            this.tbbSave.Text = "&Save";
            //
            // tbbNew
            //
            this.tbbNew.Name = "tbbNew";
            this.tbbNew.AutoSize = true;
            this.tbbNew.Click += new System.EventHandler(this.NewRecord);
            this.tbbNew.Text = "New Language Code";
            //
            // tbrMain
            //
            this.tbrMain.Name = "tbrMain";
            this.tbrMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbrMain.AutoSize = true;
            this.tbrMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                           tbbSave,
                        tbbNew});
            //
            // mniFileSave
            //
            this.mniFileSave.Name = "mniFileSave";
            this.mniFileSave.AutoSize = true;
            this.mniFileSave.Click += new System.EventHandler(this.FileSave);
            this.mniFileSave.Image = ((System.Drawing.Bitmap)resources.GetObject("mniFileSave.Glyph"));
            this.mniFileSave.ToolTipText = "Saves changed data";
            this.mniFileSave.Text = "&Save";
            //
            // mniSeparator0
            //
            this.mniSeparator0.Name = "mniSeparator0";
            this.mniSeparator0.AutoSize = true;
            this.mniSeparator0.Text = "-";
            //
            // mniFilePrint
            //
            this.mniFilePrint.Name = "mniFilePrint";
            this.mniFilePrint.AutoSize = true;
            this.mniFilePrint.Text = "&Print...";
            //
            // mniSeparator1
            //
            this.mniSeparator1.Name = "mniSeparator1";
            this.mniSeparator1.AutoSize = true;
            this.mniSeparator1.Text = "-";
            //
            // mniClose
            //
            this.mniClose.Name = "mniClose";
            this.mniClose.AutoSize = true;
            this.mniClose.Click += new System.EventHandler(this.actClose);
            this.mniClose.Image = ((System.Drawing.Bitmap)resources.GetObject("mniClose.Glyph"));
            this.mniClose.ToolTipText = "Closes this window";
            this.mniClose.Text = "&Close";
            //
            // mniFile
            //
            this.mniFile.Name = "mniFile";
            this.mniFile.AutoSize = true;
            this.mniFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                           mniFileSave,
                        mniSeparator0,
                        mniFilePrint,
                        mniSeparator1,
                        mniClose});
            this.mniFile.Text = "&File";
            //
            // mniEditUndoCurrentField
            //
            this.mniEditUndoCurrentField.Name = "mniEditUndoCurrentField";
            this.mniEditUndoCurrentField.AutoSize = true;
            this.mniEditUndoCurrentField.Text = "Undo &Current Field";
            //
            // mniEditUndoScreen
            //
            this.mniEditUndoScreen.Name = "mniEditUndoScreen";
            this.mniEditUndoScreen.AutoSize = true;
            this.mniEditUndoScreen.Text = "&Undo Screen";
            //
            // mniSeparator2
            //
            this.mniSeparator2.Name = "mniSeparator2";
            this.mniSeparator2.AutoSize = true;
            this.mniSeparator2.Text = "-";
            //
            // mniEditFind
            //
            this.mniEditFind.Name = "mniEditFind";
            this.mniEditFind.AutoSize = true;
            this.mniEditFind.Text = "&Find...";
            //
            // mniEdit
            //
            this.mniEdit.Name = "mniEdit";
            this.mniEdit.AutoSize = true;
            this.mniEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                           mniEditUndoCurrentField,
                        mniEditUndoScreen,
                        mniSeparator2,
                        mniEditFind});
            this.mniEdit.Text = "&Edit";
            //
            // mniHelpPetraHelp
            //
            this.mniHelpPetraHelp.Name = "mniHelpPetraHelp";
            this.mniHelpPetraHelp.AutoSize = true;
            this.mniHelpPetraHelp.Text = "&Petra Help";
            //
            // mniSeparator3
            //
            this.mniSeparator3.Name = "mniSeparator3";
            this.mniSeparator3.AutoSize = true;
            this.mniSeparator3.Text = "-";
            //
            // mniHelpBugReport
            //
            this.mniHelpBugReport.Name = "mniHelpBugReport";
            this.mniHelpBugReport.AutoSize = true;
            this.mniHelpBugReport.Text = "Bug &Report";
            //
            // mniSeparator4
            //
            this.mniSeparator4.Name = "mniSeparator4";
            this.mniSeparator4.AutoSize = true;
            this.mniSeparator4.Text = "-";
            //
            // mniHelpAboutPetra
            //
            this.mniHelpAboutPetra.Name = "mniHelpAboutPetra";
            this.mniHelpAboutPetra.AutoSize = true;
            this.mniHelpAboutPetra.Text = "&About Petra";
            //
            // mniHelpDevelopmentTeam
            //
            this.mniHelpDevelopmentTeam.Name = "mniHelpDevelopmentTeam";
            this.mniHelpDevelopmentTeam.AutoSize = true;
            this.mniHelpDevelopmentTeam.Text = "&The Development Team...";
            //
            // mniHelp
            //
            this.mniHelp.Name = "mniHelp";
            this.mniHelp.AutoSize = true;
            this.mniHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                           mniHelpPetraHelp,
                        mniSeparator3,
                        mniHelpBugReport,
                        mniSeparator4,
                        mniHelpAboutPetra,
                        mniHelpDevelopmentTeam});
            this.mniHelp.Text = "&Help";
            //
            // mnuMain
            //
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.mnuMain.AutoSize = true;
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                           mniFile,
                        mniEdit,
                        mniHelp});
            //
            // stbMain
            //
            this.stbMain.Name = "stbMain";
            this.stbMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.stbMain.AutoSize = true;

            //
            // TFrmLanguageCodeSetup
            //
            this.Font = new System.Drawing.Font("Verdana", 8.25f);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.ClientSize = new System.Drawing.Size(660, 700);

            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.tbrMain);
            this.Controls.Add(this.mnuMain);
            this.MainMenuStrip = mnuMain;
            this.Controls.Add(this.stbMain);
            this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");

            this.Name = "TFrmLanguageCodeSetup";
            this.Text = "Maintain Language Codes";

            this.Activated += new System.EventHandler(this.TFrmPetra_Activated);
            this.Load += new System.EventHandler(this.TFrmPetra_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.TFrmPetra_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.Closed += new System.EventHandler(this.TFrmPetra_Closed);

            this.stbMain.ResumeLayout(false);
            this.mnuMain.ResumeLayout(false);
            this.tbrMain.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.pnlDetails.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlGrid;
        private Ict.Common.Controls.TSgrdDataGridPaged grdDetails;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Panel pnlDetails;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox txtDetailLanguageCode;
        private System.Windows.Forms.Label lblDetailLanguageCode;
        private System.Windows.Forms.TextBox txtDetailLanguageDescription;
        private System.Windows.Forms.Label lblDetailLanguageDescription;
        private System.Windows.Forms.CheckBox chkDetailCongressLanguage;
        private System.Windows.Forms.Label lblDetailCongressLanguage;
        private System.Windows.Forms.CheckBox chkDetailDeletable;
        private System.Windows.Forms.Label lblDetailDeletable;
        private System.Windows.Forms.ToolStrip tbrMain;
        private System.Windows.Forms.ToolStripButton tbbSave;
        private System.Windows.Forms.ToolStripButton tbbNew;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem mniFile;
        private System.Windows.Forms.ToolStripMenuItem mniFileSave;
        private System.Windows.Forms.ToolStripSeparator mniSeparator0;
        private System.Windows.Forms.ToolStripMenuItem mniFilePrint;
        private System.Windows.Forms.ToolStripSeparator mniSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mniClose;
        private System.Windows.Forms.ToolStripMenuItem mniEdit;
        private System.Windows.Forms.ToolStripMenuItem mniEditUndoCurrentField;
        private System.Windows.Forms.ToolStripMenuItem mniEditUndoScreen;
        private System.Windows.Forms.ToolStripSeparator mniSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mniEditFind;
        private System.Windows.Forms.ToolStripMenuItem mniHelp;
        private System.Windows.Forms.ToolStripMenuItem mniHelpPetraHelp;
        private System.Windows.Forms.ToolStripSeparator mniSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mniHelpBugReport;
        private System.Windows.Forms.ToolStripSeparator mniSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mniHelpAboutPetra;
        private System.Windows.Forms.ToolStripMenuItem mniHelpDevelopmentTeam;
        private Ict.Common.Controls.TExtStatusBarHelp stbMain;
    }
}
