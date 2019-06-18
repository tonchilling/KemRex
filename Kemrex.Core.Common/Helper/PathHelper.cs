using Kemrex.Core.Common.Constraints;
using Kemrex.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kemrex.Core.Common.Helper
{
    public static class PathHelper
    {
        public static PathModel Home => new PathModel(KemrexPath.ACTION_INDEX, KemrexPath.CONTROLLER_HOME);
        public static PathModel Authen => new PathModel(KemrexPath.ACTION_INDEX, KemrexPath.CONTROLLER_AUTHEN);
        public static PathModel SignIn => new PathModel(KemrexPath.ACTION_SIGNIN, KemrexPath.CONTROLLER_AUTHEN);
        public static PathModel SignOut => new PathModel(KemrexPath.ACTION_SIGNOUT, KemrexPath.CONTROLLER_AUTHEN);

        public static PathModel OperationTeam => new PathModel(KemrexPath.ACTION_INDEX, KemrexPath.CONTROLLER_TEAM_OPERATION);
        public static PathModel OperationTeamDetail => new PathModel(KemrexPath.ACTION_DETAIL, KemrexPath.CONTROLLER_TEAM_OPERATION);
        public static PathModel OperationTeamDetailSet => new PathModel(KemrexPath.ACTION_DETAIL + KemrexPath.ACTION_SET, KemrexPath.CONTROLLER_TEAM_OPERATION);
        public static PathModel OperationTeamDetailDelete => new PathModel(KemrexPath.ACTION_DETAIL + KemrexPath.ACTION_DELETE, KemrexPath.CONTROLLER_TEAM_OPERATION);
        public static PathModel OperationTeamDelete => new PathModel(KemrexPath.ACTION_DELETE, KemrexPath.CONTROLLER_TEAM_OPERATION);

        public static PathModel SaleTeam => new PathModel(KemrexPath.ACTION_INDEX, KemrexPath.CONTROLLER_TEAM_SALE);
        public static PathModel SaleTeamDetail => new PathModel(KemrexPath.ACTION_DETAIL, KemrexPath.CONTROLLER_TEAM_SALE);
        public static PathModel SaleTeamDetailSet => new PathModel(KemrexPath.ACTION_DETAIL + KemrexPath.ACTION_SET, KemrexPath.CONTROLLER_TEAM_SALE);
        public static PathModel SaleTeamDetailDelete => new PathModel(KemrexPath.ACTION_DETAIL + KemrexPath.ACTION_DELETE, KemrexPath.CONTROLLER_TEAM_SALE);
        public static PathModel SaleTeamDelete => new PathModel(KemrexPath.ACTION_DELETE, KemrexPath.CONTROLLER_TEAM_SALE);

    }
}