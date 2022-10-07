using LoginTokenTask.Models;

namespace LoginTokenTask.Repository
{
    public interface IGadgetRepository
    {
        bool Login(LoginUser loginUser);
        List<Gadget> getAllGadget();
    }
}
