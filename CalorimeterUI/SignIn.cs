using BLL;
using DAL;
using DrakeUI.Framework;
using Entities;
using System.Windows.Forms;
using static CalorimeterUI.UI_Methods;

namespace CalorimeterUI
{
	public partial class SignIn : Form
	{
		public SignIn()
		{
			InitializeComponent();
		}
		Context db = new Context();
		BusinessLayer bll = new BusinessLayer();
		#region Move Form
		private bool mouseDown;
		private Point lastLocation;

		private void SignIn_MouseDown(object sender, MouseEventArgs e)
		{
			mouseDown = true;
			lastLocation = e.Location;
		}

		private void SignIn_MouseMove(object sender, MouseEventArgs e)
		{
			if (mouseDown)
			{
				this.Location = new Point(
					(this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

				this.Update();
			}
		}

		private void SignIn_MouseUp(object sender, MouseEventArgs e)
		{
			mouseDown = false;
		}
		#endregion
		#region ToggleMode
		public static bool DarkMode;
		private void toggleMode_ToggledChanged()
		{
			if (toggleMode.Toggled == true)
			{
				this.BackgroundImage = Image.FromFile("..\\..\\..\\Image\\arkaplandark.png");
				DarkMode = true;
			}
			else
			{
				this.BackgroundImage = Image.FromFile("..\\..\\..\\Image\\arkaplan.png");
				DarkMode = false;
			}
		}
		#endregion
		#region Password Hide And Show
		private void pbHidePw_Click(object sender, EventArgs e)
		{
			if (txtPwd.UseSystemPasswordChar == true)
			{
				txtPwd.UseSystemPasswordChar = false;
			}
			else if (txtPwd.UseSystemPasswordChar == false)
			{
				txtPwd.UseSystemPasswordChar = true;
			}

			if (txtPwd.UseSystemPasswordChar == false)
			{
				pbHidePw.BackgroundImage = null;
				pbHidePw.BackgroundImage = Image.FromFile("..\\..\\..\\Image\\show.png");
			}
			else if (txtPwd.UseSystemPasswordChar == true)
			{
				pbHidePw.BackgroundImage = null;
				pbHidePw.BackgroundImage = Image.FromFile("..\\..\\..\\Image\\hide.png");
			}
		}
		#endregion
		private void SignIn_Load(object sender, EventArgs e)
		{

		}

		#region Sign in process
		private void btnSignIn_Click(object sender, EventArgs e)
		{
			Methods.RememberMe(chkRemember, txtMail.Text, txtPwd.Text);
		}

		private void txtMail_TextChanged(object sender, EventArgs e)
		{
			Methods.EmailValidation(txtMail, errorProviderEmail, pcEmailVerification);
		}

		private void lblForgetPwd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ForgetPassword rst = new ForgetPassword();
			rst.Owner = this;
			Methods.Visibility(dlblExitForm);
			Methods.Visibility(dlblDownForm);
			Methods.Visibility(toggleMode);
			rst.ShowDialog();
			this.Show();
			Methods.Visibility(dlblExitForm);
			Methods.Visibility(dlblDownForm);
			Methods.Visibility(toggleMode);
		}
		#endregion

		#region Form Exit and Down process
		private void dlblDownForm_Click(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
		}

		private void dlblExitForm_Click(object sender, EventArgs e)
		{
			Environment.Exit(0);
		}
		#endregion
	}
}