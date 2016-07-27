using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace MSMQSimpleApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void SendQueueMessage()
        {
            using (MessageQueue data = new MessageQueue())
            {
                data.Path = @".\private$\data";

                if(!MessageQueue.Exists(data.Path))
                {
                    MessageQueue.Create(data.Path);
                }

                System.Messaging.Message message = new System.Messaging.Message();
                message.Body = textBox1.Text;
                data.Send(message);
            }
        }


        private void ReceiveQueueMessage()
        {
            using (MessageQueue data = new MessageQueue())
            {
                data.Path = @".\private$\data";
                System.Messaging.Message message = new System.Messaging.Message();
                message = data.Receive(new TimeSpan(0, 0, 5));
                message.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });

                string msgToDisplay = message.Body.ToString();
                textBox2.Text = msgToDisplay;
            }
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            SendQueueMessage();
        }

        private void btn_receive_Click(object sender, EventArgs e)
        {
            ReceiveQueueMessage();
        }
    }
}
