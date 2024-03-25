using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace Afkgame
{
    internal class GameManager
    {
        public double Money { get; set; }

        // Liste pour gérer les workers
        public List<Worker> Workers { get; private set; }

        public int TotalDevJuniors { get; private set; }
        public int TotalDevSeniors {  get; private set; }
        public int TotalDesigners { get; private set; }
        public int MaxWorkers { get; set; }
        public int Cycles { get; private set; }
        public double Rent { get; set; }


        private Timer moneyGenerationTimer;
        public event EventHandler MoneyGenerated;
        public event EventHandler SalaryPaid;
        public event EventHandler RentPaid;


        public GameManager()
        {
            Money = 25; 
            Workers = new List<Worker>();
            TotalDevJuniors = 0;
            TotalDevSeniors = 0;
            TotalDesigners = 0;
            MaxWorkers = 10;
            Cycles = 0;
            Rent = 45;

            // Timer
            moneyGenerationTimer = new Timer(1000); // 10 secondes
            moneyGenerationTimer.Elapsed += OnMoneyGenerated;
            moneyGenerationTimer.AutoReset = true;
            moneyGenerationTimer.Enabled = true;
        }

        // Méthode pour acheter un worker
        public bool BuyWorker(Worker worker)
        {
            if (Money >= worker.Price && Workers.Count < MaxWorkers)
            {
                Money -= worker.Price;
                Workers.Add(worker);
                if (worker.Type == "Junior Dev")
                {
                    TotalDevJuniors++;
                }
                else if(worker.Type == "Senior Dev")
                {
                    TotalDevSeniors++;
                }
                else if(worker.Type == "Designer")
                {
                    TotalDesigners++;
                }

                return true;
            }
            return false;
        }


        private void OnMoneyGenerated(object sender, ElapsedEventArgs e)
        {
            GenerateMoney();
            Application.Current?.Dispatcher.Invoke(() =>
            {
                MoneyGenerated?.Invoke(this, EventArgs.Empty);
            });
        }
        private void GenerateMoney()
        {
            double totalMoneyGenerated = Workers.Sum(worker => worker.MoneyGenerated);
            Money += totalMoneyGenerated;
            Cycles += 1;
            // Pay workers
            if (Cycles % 10 == 0 && Cycles != 0)
            {
                PayWorkers();
            }
            if (Cycles % 18 == 0 && Cycles != 0)
            {
                PayRent();
            }
        }


        private void PayWorkers()
        {
            double totalSalary = Workers.Sum(worker => worker.Salary);
            Money -= totalSalary;
            MessageBox.Show($"You just paid yours workers {totalSalary}");
            SalaryPaid?.Invoke(this, EventArgs.Empty);
        }


        private void PayRent()
        {
            Money -= Rent;
            MessageBox.Show($"You just paid your rent {Rent}");
            RentPaid?.Invoke(this, EventArgs.Empty);
        }
    }
}
