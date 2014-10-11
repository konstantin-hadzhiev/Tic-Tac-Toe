using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using System.Media;


namespace Tictactoe_client
{
    public partial class LoginForm : Form
    {
        ServiceReference1.TictactoeClient proxy;
        public static string username;
        SoundPlayer type = new SoundPlayer(@"Resources\type.wav");
        
        
        
        public LoginForm()
        {
            InitializeComponent();
            proxy = new ServiceReference1.TictactoeClient(new InstanceContext(this));
            button1.Enabled = false;

            
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            username = textBox1.Text;
            Cursor.Current = Cursors.WaitCursor;
            label2.Text = "Player " + username + " is connecting...";
            new Form1().Show();
            this.Hide();
        }

        

        private void login_enter(object sender, EventArgs e)
        {
            
                ActiveForm.AcceptButton = button1;
           
        }

        private void enableButton(object sender, EventArgs e)
        {
            
                button1.Enabled = true;
            if (textBox1.Text == "")
            {
                button1.Enabled = false;
            }
            
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            type.Play();
        }
    }
}
