using LoginTokenTask.Models;

namespace LoginTokenTask.service
{
    public interface IGadgetService
    {
        string Login(LoginUser loginUser);
        List<Gadget> GetAllGadget();
    }
}
