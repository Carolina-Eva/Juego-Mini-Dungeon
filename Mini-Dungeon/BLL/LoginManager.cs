using DAL;

namespace BLL
{
    public class LoginManager
    {
        Repository _repository = new Repository();
        public async Task<bool> Login(string username, string password)
        {
            if (await _repository.ValidarCrendeciales(username, password))
                return true;
            return false;
        }

        public async Task<int> ObtenerUsuarioId(string username)
        {
            return await _repository.ObtenerUsuarioId(username);
        }
    }
}
