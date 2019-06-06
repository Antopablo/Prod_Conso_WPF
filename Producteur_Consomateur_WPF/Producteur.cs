using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Producteur_Consomateur_WPF
{
    public class Producteur : INotifyPropertyChanged
    {
        public MainWindow mw { get; set; }
        private string _nom;
        private bool _pause;
        private BackgroundWorker worker_Prod;
        public event PropertyChangedEventHandler PropertyChanged;

        public Producteur(string nom, MainWindow m)
        {
            _nom = nom;
            _pause = false;
            mw = m;
            Worker_Prod = new BackgroundWorker();
            Worker_Prod.WorkerReportsProgress = true;
            Worker_Prod.WorkerSupportsCancellation = true;
            Worker_Prod.DoWork += Worker_Prod_DoWork;
            worker_Prod.ProgressChanged += Worker_Prod_ProgressChanged;
            worker_Prod.RunWorkerCompleted += Worker_Prod_RunWorkerCompleted;
        }

        private void Worker_Prod_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void Worker_Prod_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (mw.Pipe.Value < 20)
            {
                mw.Pipe.Value += e.ProgressPercentage;
            }
        }

        private void Worker_Prod_DoWork(object sender, DoWorkEventArgs e)
        {
            while (Pause == false)
            {
                int i = 0;
                i++;
                (sender as BackgroundWorker).ReportProgress(i);
                Thread.Sleep(50);
            }
                
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        public bool Pause
        {
            get { return _pause; }
            set {
                if (this._pause != value)
                {
                    this._pause = value;
                    this.NotifyPropertyChanged("Pause");
                }
            }
        }

        public BackgroundWorker Worker_Prod
        {
            get { return worker_Prod; }
            set { worker_Prod = value; }
        }


    }
}
