
namespace Quan_Ly_Thu_Vien
{
    partial class TaiKhoan
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
            this.components = new System.ComponentModel.Container();
            this.btThoat = new FontAwesome.Sharp.IconButton();
            this.BtDangKyTK = new FontAwesome.Sharp.IconButton();
            this.btThayDoiMK = new FontAwesome.Sharp.IconButton();
            this.btThongTinNV = new FontAwesome.Sharp.IconButton();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.SuspendLayout();
            // 
            // btThoat
            // 
            this.btThoat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btThoat.Dock = System.Windows.Forms.DockStyle.Top;
            this.btThoat.FlatAppearance.BorderSize = 0;
            this.btThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btThoat.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btThoat.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btThoat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btThoat.IconChar = FontAwesome.Sharp.IconChar.Times;
            this.btThoat.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(49)))), ((int)(((byte)(1)))));
            this.btThoat.IconSize = 30;
            this.btThoat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btThoat.Location = new System.Drawing.Point(0, 141);
            this.btThoat.Name = "btThoat";
            this.btThoat.Rotation = 0D;
            this.btThoat.Size = new System.Drawing.Size(365, 48);
            this.btThoat.TabIndex = 8;
            this.btThoat.Text = "Thoát";
            this.btThoat.UseVisualStyleBackColor = true;
            this.btThoat.Click += new System.EventHandler(this.btThoat_Click);
            // 
            // BtDangKyTK
            // 
            this.BtDangKyTK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtDangKyTK.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtDangKyTK.FlatAppearance.BorderSize = 0;
            this.BtDangKyTK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtDangKyTK.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.BtDangKyTK.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtDangKyTK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(133)))), ((int)(((byte)(52)))));
            this.BtDangKyTK.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
            this.BtDangKyTK.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(133)))), ((int)(((byte)(52)))));
            this.BtDangKyTK.IconSize = 37;
            this.BtDangKyTK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtDangKyTK.Location = new System.Drawing.Point(0, 98);
            this.BtDangKyTK.Name = "BtDangKyTK";
            this.BtDangKyTK.Rotation = 0D;
            this.BtDangKyTK.Size = new System.Drawing.Size(365, 43);
            this.BtDangKyTK.TabIndex = 7;
            this.BtDangKyTK.Text = "Đăng kí tài khoản";
            this.BtDangKyTK.UseVisualStyleBackColor = true;
            this.BtDangKyTK.Click += new System.EventHandler(this.BtDangKyTK_Click);
            // 
            // btThayDoiMK
            // 
            this.btThayDoiMK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btThayDoiMK.Dock = System.Windows.Forms.DockStyle.Top;
            this.btThayDoiMK.FlatAppearance.BorderSize = 0;
            this.btThayDoiMK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btThayDoiMK.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btThayDoiMK.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btThayDoiMK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(133)))), ((int)(((byte)(52)))));
            this.btThayDoiMK.IconChar = FontAwesome.Sharp.IconChar.UnlockAlt;
            this.btThayDoiMK.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(133)))), ((int)(((byte)(52)))));
            this.btThayDoiMK.IconSize = 30;
            this.btThayDoiMK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btThayDoiMK.Location = new System.Drawing.Point(0, 55);
            this.btThayDoiMK.Name = "btThayDoiMK";
            this.btThayDoiMK.Rotation = 0D;
            this.btThayDoiMK.Size = new System.Drawing.Size(365, 43);
            this.btThayDoiMK.TabIndex = 6;
            this.btThayDoiMK.Text = "Thay đổi mật khẩu";
            this.btThayDoiMK.UseVisualStyleBackColor = true;
            this.btThayDoiMK.Click += new System.EventHandler(this.btThayDoiMK_Click);
            // 
            // btThongTinNV
            // 
            this.btThongTinNV.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btThongTinNV.Dock = System.Windows.Forms.DockStyle.Top;
            this.btThongTinNV.FlatAppearance.BorderSize = 0;
            this.btThongTinNV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btThongTinNV.Flip = FontAwesome.Sharp.FlipOrientation.Normal;
            this.btThongTinNV.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btThongTinNV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(133)))), ((int)(((byte)(52)))));
            this.btThongTinNV.IconChar = FontAwesome.Sharp.IconChar.UserAlt;
            this.btThongTinNV.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(133)))), ((int)(((byte)(52)))));
            this.btThongTinNV.IconSize = 30;
            this.btThongTinNV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btThongTinNV.Location = new System.Drawing.Point(0, 0);
            this.btThongTinNV.Name = "btThongTinNV";
            this.btThongTinNV.Rotation = 0D;
            this.btThongTinNV.Size = new System.Drawing.Size(365, 55);
            this.btThongTinNV.TabIndex = 5;
            this.btThongTinNV.Text = "Thông tin cá nhân";
            this.btThongTinNV.UseVisualStyleBackColor = true;
            this.btThongTinNV.Click += new System.EventHandler(this.btThongTinNV_Click);
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 15;
            this.guna2Elipse1.TargetControl = this;
            // 
            // TaiKhoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 188);
            this.Controls.Add(this.btThoat);
            this.Controls.Add(this.BtDangKyTK);
            this.Controls.Add(this.btThayDoiMK);
            this.Controls.Add(this.btThongTinNV);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "TaiKhoan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private FontAwesome.Sharp.IconButton btThoat;
        private FontAwesome.Sharp.IconButton btThayDoiMK;
        private FontAwesome.Sharp.IconButton btThongTinNV;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        public FontAwesome.Sharp.IconButton BtDangKyTK;
    }
}