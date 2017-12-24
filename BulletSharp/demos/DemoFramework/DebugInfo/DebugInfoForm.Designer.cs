namespace DemoFramework.DebugInfo
{
    partial class DebugInfoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.worldTree = new System.Windows.Forms.TreeView();
            this.snapshotButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // worldTree
            // 
            this.worldTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.worldTree.Location = new System.Drawing.Point(13, 42);
            this.worldTree.Name = "worldTree";
            this.worldTree.Size = new System.Drawing.Size(381, 416);
            this.worldTree.TabIndex = 0;
            // 
            // snapshotButton
            // 
            this.snapshotButton.Location = new System.Drawing.Point(13, 13);
            this.snapshotButton.Name = "snapshotButton";
            this.snapshotButton.Size = new System.Drawing.Size(108, 23);
            this.snapshotButton.TabIndex = 1;
            this.snapshotButton.Text = "Take snapshot";
            this.snapshotButton.UseVisualStyleBackColor = true;
            this.snapshotButton.Click += new System.EventHandler(this.snapshotButton_Click);
            // 
            // DebugInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 470);
            this.Controls.Add(this.snapshotButton);
            this.Controls.Add(this.worldTree);
            this.Name = "DebugInfoForm";
            this.Text = "Debug Info";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView worldTree;
        private System.Windows.Forms.Button snapshotButton;
    }
}