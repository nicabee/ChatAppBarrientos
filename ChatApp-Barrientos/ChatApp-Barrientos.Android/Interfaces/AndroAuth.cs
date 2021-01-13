using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ChatApp_Barrientos.Droid;
using ChatApp_Barrientos.Interfaces;
using ChatApp_Barrientos.Models;
using ChatApp_Barrientos.Helpers;
using Plugin.CloudFirestore;
using Firebase;
using Firebase.Auth;
using Xamarin.Forms;


[assembly: Dependency(typeof(AndroAuth))]
namespace ChatApp_Barrientos.Droid
{
    
    public class AndroAuth : firebasebarrientos
    {
        DataClass dataClass = DataClass.GetInstance;
        public FirebaseAuthResponseModel IsLoggedIn()
        {
            try
            {
                FirebaseAuthResponseModel response = new FirebaseAuthResponseModel() { Status = true, Response = "Currently logged in." };
                if (FirebaseAuth.Instance.CurrentUser.Uid == null)
                {
                    response = new FirebaseAuthResponseModel() { Status = false, Response = "Currently logged out." };
                    dataClass.isSignedIn = false;
                    dataClass.loggedInUser = new UserModel();
                }
                else
                {
                    dataClass.loggedInUser = new UserModel()
                    {
                        uid = FirebaseAuth.Instance.CurrentUser.Uid,
                        email = FirebaseAuth.Instance.CurrentUser.Email,
                        name = dataClass.loggedInUser.name,
                        userType = dataClass.loggedInUser.userType,
                        //
                        contacts = dataClass.loggedInUser.contacts,
                        //
                        created_at = dataClass.loggedInUser.created_at
                        //created_at = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                    };
                    dataClass.isSignedIn = true;
                }
                return response;
            }
            catch (Exception ex)
            {
                FirebaseAuthResponseModel response = new FirebaseAuthResponseModel() { Status = false, Response = ex.Message };
                dataClass.isSignedIn = false;
                dataClass.loggedInUser = new UserModel();
                return response;
            }
        }

        public async Task<FirebaseAuthResponseModel> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                FirebaseAuthResponseModel response = new FirebaseAuthResponseModel() { Status = true, Response = "Login successful." };
                IAuthResult result = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);

                if (result.User.IsEmailVerified && email == result.User.Email)
                {
                    var document = await CrossCloudFirestore.Current
                                        .Instance
                                        .GetCollection("users")
                                        .GetDocument(result.User.Uid)
                                        .GetDocumentAsync();
                    var yourModel = document.ToObject<UserModel>();

                    dataClass.loggedInUser = new UserModel()
                    {
                        uid = result.User.Uid,
                        email = result.User.Email,
                        name = yourModel.name,
                        userType = yourModel.userType,
                        created_at = yourModel.created_at,
                        //
                        contacts = yourModel.contacts
                    };
                    dataClass.isSignedIn = true;
                }
                else
                {
                    FirebaseAuth.Instance.CurrentUser.SendEmailVerification();
                    response.Status = false;
                    response.Response = "Email not verified. Sent another verification email.";
                    dataClass.loggedInUser = new UserModel();
                    dataClass.isSignedIn = false;
                }

                return response;
            }
            catch (Exception ex)
            {
                FirebaseAuthResponseModel response = new FirebaseAuthResponseModel() { Status = false, Response = ex.Message };
                dataClass.isSignedIn = false;
                return response;
            }
        }

        public async Task<FirebaseAuthResponseModel> ResetPassword(string email)
        {
            try
            {
                FirebaseAuthResponseModel response = new FirebaseAuthResponseModel() { Status = true, Response = "Email has been sent to your email address." };
                await FirebaseAuth.Instance.SendPasswordResetEmailAsync(email);
                return response;
            }
            catch (Exception ex)
            {
                FirebaseAuthResponseModel response = new FirebaseAuthResponseModel() { Status = false, Response = ex.Message };
                return response;
            }
        }

        public FirebaseAuthResponseModel SignOut()
        {
            try
            {
                FirebaseAuthResponseModel response = new FirebaseAuthResponseModel() { Status = true, Response = "Sign out successful." };
                FirebaseAuth.Instance.SignOut();
                dataClass.isSignedIn = false;
                dataClass.loggedInUser = new UserModel();
                return response;
            }
            catch (Exception ex)
            {
                FirebaseAuthResponseModel response = new FirebaseAuthResponseModel() { Status = false, Response = ex.Message };
                dataClass.isSignedIn = true;
                return response;
            }
        }
        public async Task<FirebaseAuthResponseModel> SignUpWithEmailPassword(string name, string email, string password)
        {
            try
            {
                FirebaseAuthResponseModel response = new FirebaseAuthResponseModel() { Status = true, Response = "Sign up successful. Verification email sent." };
                await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                FirebaseAuth.Instance.CurrentUser.SendEmailVerification();

                int ndx = email.IndexOf("@");
                int cnt = email.Length - ndx;
                string defaultName = string.IsNullOrEmpty(name) ? email.Remove(ndx, cnt) : name;

                dataClass.loggedInUser = new UserModel()
                {
                    uid = FirebaseAuth.Instance.CurrentUser.Uid,
                    email = email,
                    name = defaultName,
                    userType = 0,
                    //
                    created_at = DateTime.UtcNow.ToString()
                    //created_at = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                };
                return response;
            }
            catch (Exception ex)
            {
                FirebaseAuthResponseModel response = new FirebaseAuthResponseModel() { Status = false, Response = ex.Message };
                return response;
            }
        }
        //public async Task<FirebaseAuthResponseModel> SignUpWithEmailPassword(string name, string email, string password)
        //{
        //    try
        //    {
        //        FirebaseAuthResponseModel response = new FirebaseAuthResponseModel() { Status = true, Response = "Sign up successful. Verification email sent." };
        //        await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
        //        FirebaseAuth.Instance.CurrentUser.SendEmailVerification();


        //        int ndx = email.IndexOf("@");
        //        int cnt = email.Length - ndx;
        //        string defaultName = string.IsNullOrEmpty(name) ? email.Remove(ndx, cnt) : name;

        //        dataClass.loggedInUser = new UserModel()
        //        {
        //            uid = FirebaseAuth.Instance.CurrentUser.Uid,
        //            email = email,
        //            name = defaultName,
        //            userType = 0,
        //            created_at = DateTime.UtcNow
        //        };
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        FirebaseAuthResponseModel response = new FirebaseAuthResponseModel() { Status = false, Response = ex.Message };
        //        return response;
        //    }
        //}


        //    public bool IsSignIn()
        //    {
        //        var user = Firebase.Auth.FirebaseAuth.Instance.CurrentUser;
        //        return user != null;
        //    }
        //    public async Task<string> doLogin(string em, string pass)
        //    {
        //        string ret = "1";
        //        try
        //        {
        //            var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(em, pass);
        //            var token = await user.User.GetIdTokenAsync(false);
        //            return ret;
        //            //  return token.Token;
        //        }
        //        catch (FirebaseAuthInvalidUserException notFound)
        //        {
        //            return notFound.Message;
        //        }
        //        catch (System.Exception err)
        //        {
        //            return err.Message;
        //        }
        //    }

        //    public async Task<string> doRegister(string em, string pass)
        //    {
        //        string ret = "1";
        //        try
        //        {
        //            var user = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(em, pass);
        //            FirebaseAuth.Instance.CurrentUser.SendEmailVerification();
        //            var token = await user.User.GetIdTokenAsync(false);
        //            return ret;
        //        //    return token.Token;
        //        }
        //        catch (System.Exception err)
        //        {
        //            return err.Message;
        //        }
        //    }

        //    public async Task<string> ResetPassword(string em)
        //    {
        //        string ret = "1";
        //        try
        //        {
        //            await FirebaseAuth.Instance.SendPasswordResetEmailAsync(em);
        //         //   var token = await user.User.GetIdTokenAsync(false);
        //            return ret;
        //            //    firebasebarrientos response = new firebasebarrientos() { Status = true, Response = "Email has been sent to your email address." };
        //            //    await FirebaseAuth.Instance.SendPasswordResetEmailAsync(email);
        //            //    return response;
        //        }
        //            catch (System.Exception err)
        //            {
        //            //    firebasebarrientos response = new firebasebarrientos() { Status = false, Response = ex.Message };
        //            //    return response;
        //            return err.Message;
        //        }

        //    }
        //    public bool SignOut()
        //    {
        //        try
        //        {
        //            Firebase.Auth.FirebaseAuth.Instance.SignOut();
        //            return true;
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //    }

        //}
    }
}