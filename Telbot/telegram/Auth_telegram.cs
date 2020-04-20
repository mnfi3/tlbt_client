using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telbot.Dialogs;
using Telbot.model;
using Telbot.storage;
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
        private  TelegramClient client = null;

        public Auth_telegram()
        {
            response = new TelegramResponse();

            //if (client == null)
            //{
            //    //if (G.telegram.api_id == 0 || G.telegram.api_hash.Length < 3) return null;
            //    try
            //    {
            //        client = new TelegramClient(G.telegram.api_id, G.telegram.api_hash);
            //        //await client.ConnectAsync();
            //    }
            //    catch (MissingApiConfigurationException ex)
            //    {
            //        FailedDialog _dialog = new FailedDialog("مقادیر api_id یا  api_hash را با دقت وارد کنید");
            //        _dialog.ShowDialog();
            //        Log.e("telegram connection failed.error=" + ex.ToString(), "Base_telegram", "getTelegramClient");
            //    }
            //    catch (System.Net.Sockets.SocketException ex)
            //    {
            //        FailedDialog _dialog = new FailedDialog("خطا در ارتباط با سرور تلگرام");
            //        _dialog.ShowDialog();
            //        Log.e("telegram connection failed.error=" + ex.ToString(), "Base_telegram", "getTelegramClient");
            //    }
            //    catch (Exception e)
            //    {
            //        Log.e("telegram connection failed.error=" + e.ToString(), "Base_telegram", "getTelegramClient");
            //    }
            //}
            

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
            if (File.Exists("session.dat"))
            {
                try
                {
                    File.Delete("session.dat");
                }
                catch (Exception e)
                {
                    Log.e("delete session file failed.error=" + e.ToString(), "Auth_telegram", "sendVerificationCode");
                }
            }



            try
            {
                client = new TelegramClient(G.telegram.api_id, G.telegram.api_hash, null, "s_" + G.telegram.mobile);
                //await client.ConnectAsync();
            }
            catch (Exception e)
            {
                Log.e("verification code send failed", "Auth_telegram", "sendVerificationCode");
                response.status = 0;
                response.message = "عملیات ارسال کد شکست خورد.لطفا دوباره امتحان کنید";
                response.data = "";
                Log.e("verification code send failed_1.error=" + e.ToString(), "Auth_telegram", "sendVerificationCode");
                handler(response, new EventArgs());
                return;
            }
              

                

            try
            {
                //if (!client.IsConnected)
                //var session = new FileSessionStore();

                //client = new TelegramClient(G.telegram.api_id, G.telegram.api_hash, session, "session");
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
                    Log.e("verification code send failed_2.error= hash is null", "Auth_telegram", "sendVerificationCode");
                }
            }
            catch (Exception e)
            {
                response.status = 0;
                response.message = "عملیات ارسال کد شکست خورد.لطفا دوباره امتحان کنید";
                response.data = "";
                Log.e("verification code send failed_3.error=" + e.ToString(), "Auth_telegram", "sendVerificationCode");
            }
           
            handler(response, new EventArgs());
        }



        public  async void verifyCode( EventHandler handler, string hash, string code)
        {
            try
            {
                client = new TelegramClient(G.telegram.api_id, G.telegram.api_hash, null, "s_" + G.telegram.mobile);
                //await client.ConnectAsync();
            }
            catch (Exception e)
            {
                Log.e("verification code send failed", "Auth_telegram", "sendVerificationCode");
                response.status = 0;
                response.message = "عملیات تایید کد شکست خورد.لطفا دوباره امتحان کنید";
                response.data = "";
                Log.e("verify code failed.error=" + e.ToString(), "Auth_telegram", "verifyCode");
                handler(response, new EventArgs());
                return;
            }


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
                await client.ConnectAsync();

                user = await client.MakeAuthAsync(G.telegram.mobile, hash, code);

                response.status = 1;
                response.message = "با موفقیت وارد حساب تلگرام شدید";
                response.data = user;
                Log.i("login to telegram was successfull", "Auth_telegram", "verifyCode");

                client.Dispose();
                client = null;

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
            try
            {
                client = new TelegramClient(G.telegram.api_id, G.telegram.api_hash, null, "s_" + G.telegram.mobile);
                //await client.ConnectAsync();
            }
            catch (Exception e)
            {
                Log.e("verification code send failed", "Auth_telegram", "sendVerificationCode");
                response.status = 0;
                response.message = "عملیات تایید رمز شکست خورد.لطفا دوباره امتحان کنید";
                response.data = "";
                Log.e("verify two step verification failed.error=" + e.ToString(), "Auth_telegram", "verifyTwoStepPassword");
                handler(response, new EventArgs());
                return;
            }


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
                await client.ConnectAsync();

                var passwordSetting = await client.GetPasswordSetting();
                TLUser user = null;
                user = await client.MakeAuthWithPasswordAsync(passwordSetting, password);
                if (user != null)
                {
                    response.status = 1;
                    response.message = "ورود به حساب تلگرام با موفقیت انجام شد";
                    response.data = user;

                    client.Dispose();
                    client = null;

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
                response.status = 0;
                response.message = "ورود به حساب تلگرام شکست خورد لطفا دوباره امتحان کنید";
                response.data = null;
                Log.e("verify two step verification failed.error=" + e.ToString(), "Auth_telegram", "verifyTwoStepPassword");
            }
            handler(response, new EventArgs());
        }


        public async void isUserAuthorized(EventHandler handler)
        {
            //if (client == null)
            //{
            //    response.status = 0;
            //    response.message = "عملیات شکست خورد";
            //    response.data = "";
            //    Log.e("telegram authorize failed", "Auth_telegram", "isUserAuthorized");
            //    handler(response, new EventArgs());
            //    return;
            //}

            try
            {
                //if (client != null)
                //{
                //    client.Dispose();
                //    client = null;
                //}

                client = new TelegramClient(G.telegram.api_id, G.telegram.api_hash, null, "s_" + G.telegram.mobile);
                await client.ConnectAsync();
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





        public void logout()
        {
            if (File.Exists("session.dat"))
            {
                //save new telegram
                Telegram_pref pref = new Telegram_pref();
                G.telegram.is_logged_in = 0;
                pref.saveTelegram(G.telegram);

                try
                {
                    client.Dispose();
                    client = null;

                    File.Delete("session.dat");
                    Log.i("telegram session delete successfull", "Auth_telegram", "logout");
                }
                catch (Exception ex)
                {
                    Log.e("telegram session delete failed.error=" + ex.ToString(), "Auth_telegram", "logout");
                }
            }
        }

    }
}
