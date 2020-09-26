using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ChatApp_Barrientos.Models;

namespace ChatApp_Barrientos.Interfaces
{
    public interface firebasebarrientos
{
        Task<FirebaseAuthResponseModel> LoginWithEmailPassword(string email, string password);
        Task<FirebaseAuthResponseModel> SignUpWithEmailPassword(string name, string email, string password);
        FirebaseAuthResponseModel SignOut();
        FirebaseAuthResponseModel IsLoggedIn();
        Task<FirebaseAuthResponseModel> ResetPassword(string email);
        //Task<string> doLogin(string em, string pass);
        //Task<string> doRegister(string em, string pass);

        //bool SignOut();
        //bool IsSignIn();

        //Task<string> ResetPassword(string em);
    }
}
