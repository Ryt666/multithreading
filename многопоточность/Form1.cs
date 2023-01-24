using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace multithreading
{
    public partial class Form1 : Form
    {
        private Worker _worker;

        public Form1()//выполняет в основном потоке
        {
            InitializeComponent();

            btnStart.Click += btnStart_Click;
            btnStop.Click += btnStop_Click;
            Load += Form1_Load;


           // worker.DoWork += worker_DoWork;//возникает, когда мы запускаем какой-нибудь ассинхронный процесс
           // worker.ProgressChanged += worker_ProgressChanged;//изменение процесса
           // worker.RunWorkerCompleted += worker_RunWorkerCompleted;//ассинхронный процесс завершается
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Action action = () =>
            {
               
                while (true)
                {
                   Invoke((Action) (() => lblTime.Text = DateTime.Now.ToLongTimeString()));
                    Thread.Sleep(1000);
                }
            };
            Task task = new Task(action);
            task.Start();

        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            
            if (_worker != null)
            {
                
                _worker.Cancel();
                
                Thread.Sleep(Timeout.Infinite);

            }
            
        }
        
       private async void btnStart_Click(object sender, EventArgs e)//основной поток
        {
            _worker = new Worker();
            _worker.ProcessChanged += worker_ProcessChanged;
           
            btnStart.Enabled = false;

            var cancelled = await Task<bool>.Factory.StartNew(_worker.Work);
            //вызов задачи; в классе Task вызываем свойство Factory и выполняем задачу методом StartNew
            string message = cancelled ? "Процесс отменен" : "Процесс завершен!";
            MessageBox.Show(message);
            btnStart.Enabled = true;//снова делаем доступной кнопку

        }

        private void worker_ProcessChanged(int progress)
        {
            Action action = () =>
            {
                progressBar.Value = progress;
            };
           this.helpIR(action);
            //progressBar.Value = progress;//меняем значение на указанную величину
            //progressBar был создан в основном потоке
        }


    }
}
