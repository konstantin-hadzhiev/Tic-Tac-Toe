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
    public partial class Form1 : Form, ServiceReference1.ITictactoeCallback
    {
        ServiceReference1.TictactoeClient proxy;
        SoundPlayer move = new SoundPlayer(@"Resources\blop.wav");
        SoundPlayer win = new SoundPlayer(@"Resources\win.wav");
        SoundPlayer loss = new SoundPlayer(@"Resources\loss.wav");
        bool turn = true;
        int wins = 0;
        
       
        Button[] buttonArray;
        public Form1()
        {
            
            InitializeComponent();

            buttonArray = new Button[9];
            buttonArray[0] = button1;
            buttonArray[1] = button2;
            buttonArray[2] = button3;
            buttonArray[3] = button4;
            buttonArray[4] = button5;
            buttonArray[5] = button6;
            buttonArray[6] = button7;
            buttonArray[7] = button8;
            buttonArray[8] = button9;
            proxy = new ServiceReference1.TictactoeClient(new InstanceContext(this));
            this.Text = "Tic Tac Toe - " + LoginForm.username;
            
            
            proxy.SubscribeToGame();

            proxy.AddPlayerToList(LoginForm.username);
            
            //label3.Text = LoginForm.username + " has 0 wins";
            //label4.Text = proxy.GetName(LoginForm.username)+" has 0 wins";
            
            
        }


       
       
        
        //Callback for chat 
       public void OnChatCallback(string username, string message)
       {
           chatBox.Items.Add(DateTime.Now.ToString("HH:MM") + " " + username + ": " + message);
       }

        //Callback for moves
       public void OnMoveCallback(int buttonId, string XO)
       {
           buttonArray[buttonId].Text = XO;
           turn = !turn;
           foreach (Button btn in buttonArray)
           {
               //btn.Enabled = true;
               if (btn.Text != "")
               {
                   btn.Enabled = false;
               }
               else
               {
                   btn.Enabled = true;
               }
           }
           buttonArray[buttonId].Enabled = false;
           //if (this.CheckWinner(button1.Text, button2.Text, button3.Text, button4.Text, button5.Text, button6.Text, button7.Text, button8.Text, button9.Text))
           //{
           //    MessageBox.Show("You loose!");
           //    this.ResetBoard();
           //} 

           int checkwinner = this.CheckWinner(button1.Text, button2.Text, button3.Text, button4.Text, button5.Text, button6.Text, button7.Text, button8.Text, button9.Text);
           if (checkwinner == 1)
           {
               loss.Play();
               button1.BackColor = Color.Red;
               button2.BackColor = Color.Red;
               button3.BackColor = Color.Red;
               MessageBox.Show("You lose!");
               int score = Convert.ToInt32(label4.Text);
               score++;
               label4.Text = score.ToString();
               this.ResetBoard();
           }
           else if (checkwinner == 2)
           {
               loss.Play();
               button4.BackColor = Color.Red;
               button5.BackColor = Color.Red;
               button6.BackColor = Color.Red;
               MessageBox.Show("You lose!");
               int score = Convert.ToInt32(label4.Text);
               score++;
               label4.Text = score.ToString();
               this.ResetBoard();
               
           }
           else if (checkwinner == 3)
           {
               loss.Play();
               button7.BackColor = Color.Red;
               button8.BackColor = Color.Red;
               button9.BackColor = Color.Red;
               MessageBox.Show("You lose!");
               int score = Convert.ToInt32(label4.Text);
               score++;
               label4.Text = score.ToString();
               this.ResetBoard();
               
           }
           else if (checkwinner == 4)
           {
               loss.Play();
               button1.BackColor = Color.Red;
               button4.BackColor = Color.Red;
               button7.BackColor = Color.Red;
               MessageBox.Show("You lose!");
               int score = Convert.ToInt32(label4.Text);
               score++;
               label4.Text = score.ToString();
               this.ResetBoard();
               
           }
           else if (checkwinner == 5)
           {
               loss.Play();
               button2.BackColor = Color.Red;
               button5.BackColor = Color.Red;
               button8.BackColor = Color.Red;
               MessageBox.Show("You lose!");
               int score = Convert.ToInt32(label4.Text);
               score++;
               label4.Text = score.ToString();
               this.ResetBoard();
               
           }
           else if (checkwinner == 6)
           {
               loss.Play();
               button3.BackColor = Color.Red;
               button6.BackColor = Color.Red;
               button9.BackColor = Color.Red;
               MessageBox.Show("You lose!");
               int score = Convert.ToInt32(label4.Text);
               score++;
               label4.Text = score.ToString();
               this.ResetBoard();
               
           }
           else if (checkwinner == 7)
           {
               loss.Play();
               button1.BackColor = Color.Red;
               button5.BackColor = Color.Red;
               button9.BackColor = Color.Red;
               MessageBox.Show("You lose!");
               int score = Convert.ToInt32(label4.Text);
               score++;
               label4.Text = score.ToString();
               this.ResetBoard();
               
           }
           else if (checkwinner == 8)
           {
               loss.Play();
               button3.BackColor = Color.Red;
               button5.BackColor = Color.Red;
               button7.BackColor = Color.Red;
               MessageBox.Show("You lose!");
               int score = Convert.ToInt32(label4.Text);
               score++;
               label4.Text = score.ToString();
               this.ResetBoard();
              
           }
           else if (checkwinner == 9)
           {
               foreach (Button bttn in buttonArray)
               {
                   bttn.BackColor = Color.LightBlue;
               }
               MessageBox.Show("Draw!");
               int score = Convert.ToInt32(label8.Text);
               score++;
               label8.Text = score.ToString();
               this.ResetBoard();
           }
       }


        //Send message button
       private void sendMessage_Click(object sender, EventArgs e)
       {
           if (textBox1.Text != "")
           {
               chatBox.Items.Add(DateTime.Now.ToString("HH:MM") + " " + LoginForm.username + ": " + textBox1.Text);
               proxy.Chat(LoginForm.username, textBox1.Text);
               textBox1.Clear();
           }
       }
        

        //event for each button clicked
       private void button_click(object sender, EventArgs e)
       {
          
           Button b = (Button)sender;
           if (turn == true)
           {
               move.Play();
               b.Text = "X";
               b.ForeColor = Color.Red;
               proxy.MakeMove(LoginForm.username, b.TabIndex, b.Text);
           }
           else
           {
               move.Play();
               b.Text = "O";
               b.ForeColor = Color.Blue;
               proxy.MakeMove(LoginForm.username, b.TabIndex, b.Text);
           }
           
           int checkwinner = this.CheckWinner(button1.Text, button2.Text, button3.Text, button4.Text, button5.Text, button6.Text, button7.Text, button8.Text, button9.Text);
           if (checkwinner == 1)
           {
               wins++;
               win.Play();
               button1.BackColor = Color.Green;
               button2.BackColor = Color.Green;
               button3.BackColor = Color.Green;

               
               //label3.Text = LoginForm.username+" has "+ wins + " wins";
               //label4.Text = proxy.GetName(LoginForm.username) + " has " + wins + " wins";
               //label3.Text = "You have " + wins + " wins";
               int score = Convert.ToInt32(label3.Text);
               score++;
               label3.Text = score.ToString();
               MessageBox.Show("You win! Your score is now " + score);
               

               this.ResetBoard();
           }
           else if (checkwinner == 2)
           {
               win.Play();
               button4.BackColor = Color.Green;
               button5.BackColor = Color.Green;
               button6.BackColor = Color.Green;

               
               int score = Convert.ToInt32(label3.Text);
               score++;
               label3.Text = score.ToString();
               MessageBox.Show("You win! Your score is now " + score);
               this.ResetBoard();
           }
           else if (checkwinner == 3)
           {
               win.Play();
               button7.BackColor = Color.Green;
               button8.BackColor = Color.Green;
               button9.BackColor = Color.Green;

               
               int score = Convert.ToInt32(label3.Text);
               score++;
               label3.Text = score.ToString();
               MessageBox.Show("You win! Your score is now " + score);
               this.ResetBoard();
           }
           else if (checkwinner == 4)
           {
               win.Play();
               button1.BackColor = Color.Green;
               button4.BackColor = Color.Green;
               button7.BackColor = Color.Green;

               
               int score = Convert.ToInt32(label3.Text);
               score++;
               label3.Text = score.ToString();
               MessageBox.Show("You win! Your score is now " + score);
               this.ResetBoard();
           }
           else if (checkwinner == 5)
           {
               win.Play();
               button2.BackColor = Color.Green;
               button5.BackColor = Color.Green;
               button8.BackColor = Color.Green;

               
               int score = Convert.ToInt32(label3.Text);
               score++;
               label3.Text = score.ToString();
               MessageBox.Show("You win! Your score is now " + score);
               this.ResetBoard();
           }
           else if (checkwinner == 6)
           {
               win.Play();
               button3.BackColor = Color.Green;
               button6.BackColor = Color.Green;
               button9.BackColor = Color.Green;

              
               int score = Convert.ToInt32(label3.Text);
               score++;
               label3.Text = score.ToString();
               MessageBox.Show("You win! Your score is now " + score);
               this.ResetBoard();
           }
           else if (checkwinner == 7)
           {
               win.Play();
               button1.BackColor = Color.Green;
               button5.BackColor = Color.Green;
               button9.BackColor = Color.Green;

               
               int score = Convert.ToInt32(label3.Text);
               score++;
               label3.Text = score.ToString();
               MessageBox.Show("You win! Your score is now " + score);
               this.ResetBoard();
           }
           else if (checkwinner == 8)
           {
               win.Play();
               button3.BackColor = Color.Green;
               button5.BackColor = Color.Green;
               button7.BackColor = Color.Green;

               
               int score = Convert.ToInt32(label3.Text);
               score++;
               label3.Text = score.ToString();
               MessageBox.Show("You win! Your score is now " + score);
               this.ResetBoard();
           }
           else if (checkwinner == 9)
           {
               foreach (Button bttn in buttonArray)
               {
                   bttn.BackColor = Color.LightBlue;
               }
               
               MessageBox.Show("Draw!");
               int score = Convert.ToInt32(label8.Text);
               score++;
               label8.Text = score.ToString();

               this.ResetBoard();
           }
           turn = !turn;
           b.Enabled = false;
           foreach (Button btn in buttonArray)
           {
               btn.Enabled = false;
           }
       }


        //game logic for checking if there is a winner
       public int CheckWinner(string str1, string str2, string str3, string str4, string str5, string str6, string str7, string str8, string str9)
       {
           bool weHaveAwinner = false;
           int whichbuttonswin = 0;
           
           
           if (str1 == str2 && str2 == str3 && str1 != "" && str2 != "" && str3 != "")
           {
               if (str1 == "X")
               {              
                   weHaveAwinner = true;
                   whichbuttonswin = 1;
                   
                   
               }
               if (str1 == "O")
               {                  
                   weHaveAwinner = true;
                   whichbuttonswin = 1;
                  
                   
               }
           }
           if (str4 == str5 && str5 == str6 && str4 != "")
           {
               if (str4 == "X")
               {                  
                   weHaveAwinner = true;
                   whichbuttonswin = 2;
               }
               if (str4 == "O")
               {                  
                   weHaveAwinner = true;
                   whichbuttonswin = 2;
               }
           }
           if (str7 == str8 && str8 == str9 && str7 != "")
           {
               if (str7 == "X")
               {                   
                   weHaveAwinner = true;
                   whichbuttonswin = 3;
               }
               if (str7 == "O")
               {                  
                   weHaveAwinner = true;
                   whichbuttonswin = 3;
               }
           }
           if (str1 == str4 && str4 == str7 && str1 != "")
           {
               if (str1 == "X")
               {                   
                   weHaveAwinner = true;
                   whichbuttonswin = 4;
               }
               if (str1 == "O")
               {                  
                   weHaveAwinner = true;
                   whichbuttonswin = 4;
               }
           }
           if (str2 == str5 && str5 == str8 && str2 != "")
           {
               if (str2 == "X")
               {                   
                   weHaveAwinner = true;
                   whichbuttonswin = 5;
               }
               if (str2 == "O")
               {                  
                   weHaveAwinner = true;
                   whichbuttonswin = 5;
               }
           }
           if (str3 == str6 && str6 == str9 && str3 != "")
           {
               if (str3 == "X")
               {                  
                   weHaveAwinner = true;
                   whichbuttonswin = 6;
               }
               if (str3 == "O")
               {                  
                   weHaveAwinner = true;
                   whichbuttonswin = 6;
               }
           }
           if (str1 == str5 && str5 == str9 && str1 != "")
           {
               if (str1 == "X")
               {                  
                   weHaveAwinner = true;
                   whichbuttonswin = 7;
               }
               if (str1 == "O")
               {                  
                   weHaveAwinner = true;
                   whichbuttonswin = 7;
               }
           }
           if (str3 == str5 && str5 == str7 && str3 != "")
           {
               if (str3 == "X")
               {
                   weHaveAwinner = true;
                   whichbuttonswin = 8;
               }
               if (str3 == "O")
               {
                   weHaveAwinner = true;
                   whichbuttonswin = 8;
               }
           }
           if (whichbuttonswin == 0 && str1 != "" && str2 != "" && str3 != "" && str4 != "" && str5 != "" && str6 != "" && str7 != "" && str8 != "" && str9 != "")
           {
               whichbuttonswin = 9;  
           }


           //return weHaveAwinner;
           return whichbuttonswin;

       }


        //resets the board
       public void ResetBoard()
       {
           foreach (Button b in buttonArray)
           {
               b.Text = "";
               b.Enabled = true;
               b.BackColor = SystemColors.Control;
               b.UseVisualStyleBackColor = true;
               
           }
       }

       private void tb_chat_enter(object sender, EventArgs e)
       {
           ActiveForm.AcceptButton = sendMessage;
       }
    }
}
