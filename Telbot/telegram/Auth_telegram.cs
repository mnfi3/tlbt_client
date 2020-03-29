using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telbot.Dialogs;
using Telbot.system;
using TeleSharp.TL;
using TeleSharp.TL.Users;
using TLSharp.Core;
using TLSharp.Core.Exceptions;

namespace Telbot.telegram
{
    class Auth_telegram
    {
        TelegramResponse response;
        private static TelegramClient client;

        public Auth_telegram()
        {
            response = new TelegramResponse();
            client = Base_telegram.getTelegramClient();

        }


        //private TelegramClient newClient()
        //{
        //    if (client != null) return client;
        //    if (G.telegram.api_id == 0 || G.telegram.api_hash.Length < 3) return null;
        //    try
        //    {
        //        return new TelegramClient(G.telegram.api_id, G.telegram.api_hash);
        //    }
        //    catch (MissingApiConfigurationException ex)
        //    {
        //        FailedDialog _dialog = new FailedDialog("مقادیر api_id یا  api_hash را با دقت وارد کنید");
        //        _dialog.ShowDialog();
        //        return null;
        //    }
        //    catch (System.Net.Sockets.SocketException ex)
        //    {
        //        FailedDialog _dialog = new FailedDialog("خطا در ارتباط با سرور تلگرام");
        //        _dialog.ShowDialog();
        //        return null;
        //    }
        //}




        public  async void sendVerificationCode(EventHandler handler)
        {
            if (client == null)
            {
                response.status = 0;
                response.message = "عملیات ارسال کد شکست خورد.لطفا دوباره امتحان کنید";
                response.data = "";
                Log.e("verification code send failed", "Auth_telegram", "sendVerificationCode");
                handler(response, new EventArgs());

                return;
            }

            try
            {
                //if (!client.IsConnected)
                await client.ConnectAsync();

                var hash = await client.SendCodeRequestAsync(G.telegram.mobile);

                if (hash.Length > 0)
                {
                    response.status = 1;
                    response.message = "";
                    response.data = hash;
                }
                else
                {
                    response.status = 0;
                    response.message = "عملیات ارسال کد شکست خورد.لطفا دوباره امتحان کنید";
                    response.data = hash;
                    Log.e("verification code send failed", "Auth_telegram", "sendVerificationCode");
                }
            }
            catch (Exception e)
            {
                response.status = 0;
                response.message = "عملیات ارسال کد شکست خورد.لطفا دوباره امتحان کنید";
                response.data = "";
                Log.e("verification code send failed.error=" + e.ToString(), "Auth_telegram", "sendVerificationCode");
            }
           
            handler(response, new EventArgs());
        }



        public  async void verifyCode( EventHandler handler, string hash, string code)
        {
            if (client == null)
            {
                response.status = 0;
                response.message = "عملیات شکست خورد";
                response.data = "";
                Log.e("verify code failed", "Auth_telegram", "verifyCode");
                handler(response, new EventArgs());
                return;
            }

            TLUser user = null;
            try
            {
                user = await client.MakeAuthAsync(G.telegram.mobile, hash, code);

                response.status = 1;
                response.message = "با موفقیت وارد حساب تلگرام شدید";
                response.data = user;
                Log.i("login to telegram was successfull", "Auth_telegram", "verifyCode");
                handler(response, new EventArgs());
            }
            catch (CloudPasswordNeededException ex)
            {
                response.status = 2;
                response.message = "کد تایید دو مرحله ای را وارد کنید";
                response.data = user;
                Log.e("verify code failed or need for two step verification password.error=" + ex.ToString() , "Auth_telegram", "verifyCode");
                handler(response, new EventArgs());
            }
            catch (InvalidPhoneCodeException ex)
            {
                //FailedDialog _dialog = new FailedDialog("کد تایید اشتباه می باشد");
                //_dialog.ShowDialog();

                response.status = 0;
                response.message = "کد تایید اشتباه می باشد";
                response.data = new TLUser(); ;
                Log.e("verify code failed.error=" + ex.ToString(), "Auth_telegram", "verifyCode");
                handler(response, new EventArgs());
            }
        }


        public  async void verifyTwoStepPassword(EventHandler handler, string password)
        {
            if (client == null)
            {
                response.status = 0;
                response.message = "عملیات شکست خورد";
                response.data = "";
                Log.e("verify two step verification failed", "Auth_telegram", "verifyTwoStepPassword");
                handler(response, new EventArgs());
                return;
            }

            try
            {
                var passwordSetting = await client.GetPasswordSetting();
                TLUser user = null;
                user = await client.MakeAuthWithPasswordAsync(passwordSetting, password);
                if (user != null)
                {
                    response.status = 1;
                    response.message = "ورود به حساب تلگرام با موفقیت انجام شد";
                    response.data = user;
                    Log.i("login to telegram was successfull", "Auth_telegram", "verifyTwoStepPassword");
                }
                else
                {
                    response.status = 0;
                    response.message = "ورود به حساب تلگرام شکست خورد لطفا رمز خود را با دقت وارد کنید";
                    response.data = user;
                    Log.e("verify two step verification failed", "Auth_telegram", "verifyTwoStepPassword");
                }

            }
            catch (Exception e)
            {
                Log.e("verify two step verification failed.error=" + e.ToString(), "Auth_telegram", "verifyTwoStepPassword");
            }
            handler(response, new EventArgs());
        }


        public  void isUserAuthorized(EventHandler handler)
        {
            if (client == null)
            {
                response.status = 0;
                response.message = "عملیات شکست خورد";
                response.data = "";
                Log.e("telegram authorize failed", "Auth_telegram", "isUserAuthorized");
                handler(response, new EventArgs());
                return;
            }

            try
            {
                bool is_user_logged_in = client.IsUserAuthorized();
                if (is_user_logged_in)
                {
                    response.status = 1;
                    response.message = "ورود به حساب تلگرام با موفقیت انجام شد";
                    response.data = is_user_logged_in;
                }
                else
                {
                    response.status = 0;
                    response.message = "ورود به حساب تلگرام شکست خورد لطفا دوباره امتحان کنید";
                    response.data = is_user_logged_in;
                    Log.e("telegram authorize failed", "Auth_telegram", "isUserAuthorized");
                }

            }
            catch (Exception e)
            {
                Log.e("telegram authorize failed.error=" + e.ToString(), "Auth_telegram", "isUserAuthorized");
            }


            handler(response, new EventArgs());
        }


    }
}
