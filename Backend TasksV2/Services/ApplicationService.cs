using Backend.Helpers;

namespace Backend.Services
{
    public class ApplicationService : IApplicationService
    {
        //We could use reflection instead but can be slow, so we use a feature included in 4.5 or beyond using CallerFilePath attribute
        private static readonly log4net.ILog log = LogHelper.GetLogger();

        IMenu _menu;
        INetSalary _netSalary;
        IViewList _viewList;

        public ApplicationService(IMenu menu, INetSalary netSalary, IViewList viewList)
        {
            _menu = menu;
            _netSalary = netSalary;
            _viewList = viewList;
        }

        public void Run()
        {
            var userChoice = _menu.DisplayMenu();

            //Only three integer options are valid as DisplayMenu method return value
            switch(userChoice)
            {
                case 1:
                    //View Movie Stars List
                    log.Debug("Call ViewMovieStars Method");
                    _viewList.ViewMovieStarList();
                    break;
                case 2:
                    //Calculate Net Salary
                    log.Debug("Call NetSalary Method");
                    _netSalary.CalculateNetSalary();
                    break;
                case 3:
                    //Exit
                    log.Debug("Exit Application");
                    break;
            }
        }
    }
}
