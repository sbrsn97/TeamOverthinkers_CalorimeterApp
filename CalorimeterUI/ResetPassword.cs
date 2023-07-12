﻿using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CalorimeterUI.UI_Methods;

namespace CalorimeterUI
{
	public partial class ResetPassword : Form
	{
		public ResetPassword()
		{
			InitializeComponent();
		}

		private void ResetPassword_Load(object sender, EventArgs e)
		{
			pbCheckMail.Visible = false;
			if (SignIn.DarkMode)
			{
				this.BackColor = Color.FromArgb(23, 31, 5);
				lblMail.ForeColor = Color.DarkKhaki;
			}
			else if (SignIn.DarkMode == false)
			{
				this.BackColor = Color.FromArgb(225, 233, 196);
				lblMail.ForeColor = Color.Olive;
			}
			this.Size = new Size(420, 450);
			this.Location = this.Owner.Location;
			this.Left += 320;
		}

		private void btnSend_Click(object sender, EventArgs e)
		{
			BusinessLayer bl = new BusinessLayer();
			Users? user = new Users();
			user = bl.Users.Search(txtMail.Text);
			if (user == null)
			{
				MessageBox.Show("This mail adress is not registered.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			else
			{
				string newHashPw = "";
				string newPassword = Methods.GeneratePassword();
				string password = $"<h1>Your new Password is: <b>{newPassword}</b><h1>";
				Methods.SendEmail(password, "", txtMail.Text, "New Password");
				newHashPw = Methods.GenerateHash(newPassword);
				user.Password = newHashPw;
				bl.Users.Update(user);
				DialogResult ok = MessageBox.Show("Your new password has been sent to your mail adress.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
				if (ok == DialogResult.OK)
				{
					this.Close();
				}
			}
		}
	}
}
