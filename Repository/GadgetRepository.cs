using LoginTokenTask.Context;
using LoginTokenTask.Models;

namespace LoginTokenTask.Repository
{
    public class GadgetRepository : IGadgetRepository
    {

        private readonly LoginDbContext _loginDbContext;
        public GadgetRepository(LoginDbContext loginDbContext)
        {
            _loginDbContext = loginDbContext;
        }
        #region get all gadget list
        //get all gadget list
        public List<Gadget> getAllGadget()
        {
            return _loginDbContext.GagetTbl.ToList();
        }

        #endregion


        #region login
        //login
        public bool Login(LoginUser loginUser)
        {
            LoginUser loginresult = _loginDbContext.UserTbl.Where(u => u.UserName == loginUser.UserName).FirstOrDefault();
            if (loginUser == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        #endregion

    }
}
