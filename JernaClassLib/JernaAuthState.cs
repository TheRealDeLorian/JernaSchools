using JernaClassLib.Exceptions;
using JernaClassLib.IServices;

namespace JernaClassLib
{
    public class JernaAuthState
    {
        private readonly IAuthService _authService;
        private readonly ISecureStorageService _secureStorageService;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1);

        public JernaAuthState(IAuthService auth, ISecureStorageService sss)
        {
            _authService = auth;
            _secureStorageService = sss;
        }

        public async Task<User> GetUserAsync()
        {
            await _semaphore.WaitAsync();

            try
            {
                string authValue = await _secureStorageService.GetValueFromKeyAsync(Constants.AuthKeyDefault) ?? throw new NothingInStorageException();
                return await _authService.GetUserAsync(authValue) ?? throw new InvalidAuthCodeException();
            }
            catch (NothingInStorageException)
            {
                var userKey = await _authService.GenerateRandomUserAsync();
                await _secureStorageService.StoreKeyValueAsync(Constants.AuthKeyDefault, userKey.AuthCode);
                return userKey.User;
            }
            catch (InvalidAuthCodeException)
            {
                await _secureStorageService.DeleteKeyValueAsync(Constants.AuthKeyDefault);
                var userKey = await _authService.GenerateRandomUserAsync();
                await _secureStorageService.StoreKeyValueAsync(Constants.AuthKeyDefault, userKey.AuthCode);
                return userKey.User;
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                var userKey = await _authService.GenerateRandomUserAsync();
                await _secureStorageService.StoreKeyValueAsync(Constants.AuthKeyDefault, userKey.AuthCode);
                return userKey.User;
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
