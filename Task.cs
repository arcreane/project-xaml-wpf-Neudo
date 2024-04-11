using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afkgame
{
    internal class Task
    {
        public string TaskName { get; set; }
        public double DevelopersRequired { get; set; }
        public double DesignersRequired {  get; set; }
        public double MoneyRewardOnSuccess { get; set; }
        public double MoneyPenaltyOnFailure { get; set; }
        public double CyclesRequired { get; set; }

        public Task(string taskName, int developersRequired, int designersRequired, double moneyRewardOnSuccess, double moneyPenaltyOnFailure, int cyclesRequired)
        {
            TaskName = taskName;
            DevelopersRequired = developersRequired;
            DesignersRequired = designersRequired;
            MoneyRewardOnSuccess = moneyRewardOnSuccess;
            MoneyPenaltyOnFailure = moneyPenaltyOnFailure;
            CyclesRequired = cyclesRequired;
        }
    }
}
