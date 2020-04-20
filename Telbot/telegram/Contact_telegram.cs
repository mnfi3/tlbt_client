using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Telbot.Dialogs;
using Telbot.system;
using TeleSharp.TL;
using TeleSharp.TL.Channels;
using TeleSharp.TL.Contacts;
using TeleSharp.TL.Messages;
using TLSharp.Core;
using TLSharp.Core.Exceptions;

namespace Telbot.telegram
{
    class Contact_telegram
    {
        TelegramResponse response;
        private TelegramClient client = null;

        public Contact_telegram()
        {
            response = new TelegramResponse();
            //client = Base_telegram.getTelegramClient();

        }





        public async void getContacts(EventHandler handler)
        {

            try
            {
                client = new TelegramClient(G.telegram.api_id, G.telegram.api_hash, null, "s_" + G.telegram.mobile);
                //await client.ConnectAsync();
            }
            catch (MissingApiConfigurationException ex)
            {
                FailedDialog _dialog = new FailedDialog("مقادیر api_id یا  api_hash را با دقت وارد کنید");
                _dialog.ShowDialog();
                Log.e("telegram connection failed.error=" + ex.ToString(), "Contact_telegram", "getContacts");
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                FailedDialog _dialog = new FailedDialog("خطا در ارتباط با سرور تلگرام");
                _dialog.ShowDialog();
                Log.e("telegram connection failed.error=" + ex.ToString(), "Contact_telegram", "getContacts");
            }
            catch (Exception e)
            {
                Log.e("telegram connection failed.error=" + e.ToString(), "Contact_telegram", "getContacts");
            }





            if (client == null)
            {
                response.status = 0;
                response.message = "عملیات شکست خورد.لطفا دوباره امتحان کنید";
                response.data = new List<TLUser>();
                Log.e("get telegram contacts failed", "Contact_telegram", "getContacts");
                handler(response, new EventArgs());
                return;
            }




            try
            {
                try
                {
                    await client.ConnectAsync();
                }
                catch (Exception e)
                {
                    response.status = 0;
                    response.message = "عملیات شکست خورد.لطفا دوباره امتحان کنید";
                    response.data = new List<TLUser>();
                    Log.e("connection fail to get telegram contacts.error=" + e.ToString(), "Contact_telegram", "getContacts");
                    handler(response, new EventArgs());
                    return;
                }

                var result = await client.GetContactsAsync();
                if (result.Users.Count > 0)
                {
                    List<TLUser> users = result.Users.OfType<TLUser>().ToList<TLUser>();
                    response.status = 1;
                    response.message = "";
                    response.data = users;
                }
                else
                {
                    response.status = 0;
                    response.message = "عملیات ارسال کد شکست خورد.لطفا دوباره امتحان کنید";
                    Log.e("get telegram contacts failed or no contact exist for this user", "Contact_telegram", "getContacts");
                    response.data = new List<TLUser>();
                }
            }
            catch (Exception e)
            {
                response.status = 0;
                response.message = "عملیات ارسال کد شکست خورد.لطفا دوباره امتحان کنید";
                response.data = new List<TLUser>();
                Log.e("get telegram contacts failed.error=" + e.ToString(), "Contact_telegram", "getContacts");
            }





            handler(response, new EventArgs());
        }














        //###########################################LAST FUNCTION####################################################
        public async void addNumberToChannel1(EventHandler task_finish_handler, EventHandler on_contact_added_handler, TLVector<TLInputPhoneContact> contacts, TLChannel channel)
        {
            try
            {
                client = new TelegramClient(G.telegram.api_id, G.telegram.api_hash, null, "s_" + G.telegram.mobile);
                //await client.ConnectAsync();
            }
            catch (MissingApiConfigurationException ex)
            {
                FailedDialog _dialog = new FailedDialog("مقادیر api_id یا  api_hash را با دقت وارد کنید");
                _dialog.ShowDialog();
                Log.e("telegram connection failed.error=" + ex.ToString(), "Contact_telegram", "addNumberToChannel");
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                FailedDialog _dialog = new FailedDialog("خطا در ارتباط با سرور تلگرام");
                _dialog.ShowDialog();
                Log.e("telegram connection failed.error=" + ex.ToString(), "Contact_telegram", "addNumberToChannel");
            }
            catch (Exception e)
            {
                Log.e("telegram connection failed.error=" + e.ToString(), "Contact_telegram", "addNumberToChannel");
            }

            if (client == null)
            {
                response.status = 0;
                response.message = "عملیات شکست خورد.لطفا دوباره امتحان کنید";
                Log.e("add Number To Channel failed", "Contact_telegram", "addNumberToChannel");
                task_finish_handler(response, new EventArgs());
                return;
            }




            //-----------------------------------add to contacts-----------------------------------------------
            TLImportedContacts importedContacts = null;
            List<TLUser> imported_users = new List<TLUser>();
            Random rand = new Random();
            List<TLUser> added_users = new List<TLUser>();
            foreach (TLInputPhoneContact c in contacts)
            {
                response.message = "  درحال افزودن کاربر به مخاطبین " + c.Phone;
                response.data = new List<TLUser>();
                on_contact_added_handler(response, new EventArgs());

                try
                {
                    await client.ConnectAsync();
                }
                catch (Exception e)
                {
                    Log.e("telegram connection failed.error=" + e.ToString(), "Contact_telegram", "addNumberToChannel");
                    response.status = 0;
                    response.message = "مشکلی در ارتباط با تلگرام رخ داده است.لطفا دوباره سعی کنید";
                    response.data = new List<TLUser>();

                    task_finish_handler(response, new EventArgs());
                    return;
                }

                int num = (int)rand.Next(10, 15);
                await Task.Delay(num * 1000);


                importedContacts = await client.SendRequestAsync<TLImportedContacts>(new TLRequestImportContacts
                {
                    Contacts = new TLVector<TLInputPhoneContact> { new TLInputPhoneContact { FirstName = c.FirstName, LastName = c.LastName, Phone = c.Phone, ClientId = (int)rand.Next(1, 20000) } }
                });


                //contact has not telegram  accoutn
                if (importedContacts.Users.Count == 0)
                {
                    response.message = "  کاربر در تلگرام حساب ندارد " + c.Phone;
                    response.data = new List<TLUser>();
                    on_contact_added_handler(response, new EventArgs());
                }
                //contact has telegram  accoutn
                else
                {
                    response.message = "  کاربر با موفقیت به لیست مخاطبین افزوده شد.در حال افزودن به چت... " + c.Phone;
                    response.data = new List<TLUser>();
                    on_contact_added_handler(response, new EventArgs());

                    imported_users.Add(importedContacts.Users.OfType<TLUser>().ToList<TLUser>()[0]);

                    //-----------------------------------add to channel-----------------------------------------------
                    try
                    {
                        await client.ConnectAsync();
                    }
                    catch (Exception e)
                    {
                        Log.e("telegram connection failed.error=" + e.ToString(), "Contact_telegram", "addNumberToChannel");
                        response.status = 0;
                        response.message = "مشکلی در ارتباط با تلگرام رخ داده است.لطفا دوباره سعی کنید";
                        response.data = new List<TLUser>();

                        task_finish_handler(response, new EventArgs());
                        return;
                    }


                    num = (int)rand.Next(30, 35);
                    await Task.Delay(num * 1000);

                    TLVector<TLAbsInputUser> to_added_users = new TLVector<TLAbsInputUser>() { new TLInputUser() { UserId = imported_users[imported_users.Count - 1].Id, AccessHash = (long)imported_users[imported_users.Count - 1].AccessHash } };
                    TLVector<TLAbsInputChannel> target_channels = new TLVector<TLAbsInputChannel>() { new TLInputChannel() { ChannelId = channel.Id, AccessHash = (long)channel.AccessHash } };
                    var req2 = new TLRequestInviteToChannel()
                    {

                        Channel = target_channels[0],
                        Users = to_added_users
                    };

                    try
                    {
                        var update = await client.SendRequestAsync<TLUpdates>(req2);
                        added_users.Add(imported_users[imported_users.Count - 1]);

                        response.message = "  کاربر با موفقیت به چت افزوده شد " + c.Phone;
                        response.data = new List<TLUser>();
                        on_contact_added_handler(response, new EventArgs());
                    }
                    catch (Exception e)
                    {
                        Log.e("add to channel failed.phone = " + c.Phone + " . error=" + e.ToString(), "Contact_telegram", "addNumberToChannel");
                        response.message = "   دسترسی به افزودن این کاربر به چت وجود ندارد" + c.Phone;
                        response.data = new List<TLUser>();
                        on_contact_added_handler(response, new EventArgs());
                    }
                }

                num = (int)rand.Next(20, 25);
                await Task.Delay(num * 1000);

            }


            response.status = 1;
            response.message = " تعداد کل افراد افزوده شده به چت : " + added_users.Count.ToString();
            response.data = new List<TLUser>();

            task_finish_handler(response, new EventArgs());
        }















        //###########################################LAST FUNCTION EDITED####################################################
        public async void addNumberToChannel11(EventHandler task_finish_handler, EventHandler on_contact_added_handler, TLVector<TLInputPhoneContact> contacts, TLChannel channel)
        {
           

            //-----------------------------------add to contacts-----------------------------------------------
            TLImportedContacts importedContacts = null;
            List<TLUser> imported_users = new List<TLUser>();
            Random rand = new Random();
            List<TLUser> added_users = new List<TLUser>();
            int i = -1;
            foreach (TLInputPhoneContact c in contacts)
            {
                i++;
                client = getTelegramClient(i);

                response.message = "  درحال افزودن کاربر به مخاطبین " + c.Phone;
                response.data = new List<TLUser>();
                on_contact_added_handler(response, new EventArgs());

                try
                {
                    await client.ConnectAsync();
                }
                catch (Exception e)
                {
                    Log.e("telegram connection failed.error=" + e.ToString(), "Contact_telegram", "addNumberToChannel");

                }


                await Task.Delay(G.telegram.getDelayTime() / 3);


                importedContacts = await client.SendRequestAsync<TLImportedContacts>(new TLRequestImportContacts
                {
                    Contacts = new TLVector<TLInputPhoneContact> { new TLInputPhoneContact { FirstName = c.FirstName, LastName = c.LastName, Phone = c.Phone, ClientId = (int)rand.Next(1, 20000) } }
                });


                //contact has not telegram  account
                if (importedContacts.Users.Count == 0)
                {
                    response.message = "  کاربر در تلگرام حساب ندارد " + c.Phone;
                    response.data = new List<TLUser>();
                    on_contact_added_handler(response, new EventArgs());
                }
                //contact has telegram  account
                else
                {
                    response.message = "  کاربر با موفقیت به لیست مخاطبین افزوده شد.در حال افزودن به چت... " + c.Phone;
                    response.data = new List<TLUser>();
                    on_contact_added_handler(response, new EventArgs());

                    imported_users.Add(importedContacts.Users.OfType<TLUser>().ToList<TLUser>()[0]);

                    //-----------------------------------add to channel-----------------------------------------------
                    try
                    {
                        await client.ConnectAsync();
                    }
                    catch (Exception e)
                    {
                        Log.e("telegram connection failed.error=" + e.ToString(), "Contact_telegram", "addNumberToChannel");
                    }


                    await Task.Delay(G.telegram.getDelayTime() / 3);

                    TLVector<TLAbsInputUser> to_added_users = new TLVector<TLAbsInputUser>() { new TLInputUser() { UserId = imported_users[imported_users.Count - 1].Id, AccessHash = (long)imported_users[imported_users.Count - 1].AccessHash } };
                    TLVector<TLAbsInputChannel> target_channels = new TLVector<TLAbsInputChannel>() { new TLInputChannel() { ChannelId = channel.Id, AccessHash = (long)channel.AccessHash } };
                    var req2 = new TLRequestInviteToChannel()
                    {

                        Channel = target_channels[0],
                        Users = to_added_users
                    };

                    try
                    {
                        var update = await client.SendRequestAsync<TLUpdates>(req2);
                        added_users.Add(imported_users[imported_users.Count - 1]);

                        response.message = "  کاربر با موفقیت به چت افزوده شد " + c.Phone;
                        response.data = new List<TLUser>();
                        on_contact_added_handler(response, new EventArgs());
                    }
                    catch (Exception e)
                    {
                        Log.e("add to channel failed.phone = " + c.Phone + " . error=" + e.ToString(), "Contact_telegram", "addNumberToChannel");
                        response.message = "   دسترسی به افزودن این کاربر به چت وجود ندارد" + c.Phone;
                        response.data = new List<TLUser>();
                        on_contact_added_handler(response, new EventArgs());
                    }
                }

                await Task.Delay(G.telegram.getDelayTime() / 3);

            }


            response.status = 1;
            response.message = " تعداد کل افراد افزوده شده به چت : " + added_users.Count.ToString();
            response.data = new List<TLUser>();

            task_finish_handler(response, new EventArgs());
        }












        public async void addNumberToChannel(EventHandler task_finish_handler, EventHandler on_contact_added_handler, TLVector<TLInputPhoneContact> contacts, TLChannel channel)
        {



            //-----------------------------------add to contacts-----------------------------------------------
            TLImportedContacts importedContacts = null;
            List<TLUser> imported_users = new List<TLUser>();
            Random rand = new Random();
            List<TLUser> added_users = new List<TLUser>();
            int i = -1;
            foreach (TLInputPhoneContact c in contacts)
            {
                i++;
                client = getTelegramClient(i);

                response.message = "  درحال افزودن کاربر به مخاطبین " + c.Phone;
                response.data = new List<TLUser>();
                on_contact_added_handler(response, new EventArgs());

                try
                {
                    await client.ConnectAsync();
                }
                catch (Exception e)
                {
                    Log.e("telegram connection failed.phone = " + c.Phone + " . error=" + e.ToString(), "Contact_telegram", "addNumberToChannel"); 
                }


                //await Task.Delay(G.telegram.getDelayTime() / 3);


                try
                {
                    importedContacts = await client.SendRequestAsync<TLImportedContacts>(new TLRequestImportContacts
                    {
                        Contacts = new TLVector<TLInputPhoneContact> { new TLInputPhoneContact { FirstName = c.FirstName, LastName = c.LastName, Phone = c.Phone, ClientId = (int)rand.Next(1, 20000) } }
                    });


                    //contact has not telegram  account
                    if (importedContacts.Users.Count == 0)
                    {
                        response.message = "  کاربر در تلگرام حساب ندارد " + c.Phone;
                        response.data = new List<TLUser>();
                        on_contact_added_handler(response, new EventArgs());
                    }
                        
                    //contact has telegram  account
                    else
                    {
                        response.message = "  کاربر با موفقیت به لیست مخاطبین افزوده شد.در حال افزودن به چت... " + c.Phone;
                        response.data = new List<TLUser>();
                        on_contact_added_handler(response, new EventArgs());

                        imported_users.Add(importedContacts.Users.OfType<TLUser>().ToList<TLUser>()[0]);

                        //-----------------------------------add to channel-----------------------------------------------
                        try
                        {
                            await client.ConnectAsync();
                        }
                        catch (Exception e)
                        {
                            Log.e("telegram connection failed.error=" + e.ToString(), "Contact_telegram", "addNumberToChannel");
                        }



                        await Task.Delay(G.telegram.getDelayTime() / 3);

                        TLVector<TLAbsInputUser> to_added_users = new TLVector<TLAbsInputUser>() { new TLInputUser() { UserId = imported_users[imported_users.Count - 1].Id, AccessHash = (long)imported_users[imported_users.Count - 1].AccessHash } };
                        TLVector<TLAbsInputChannel> target_channels = new TLVector<TLAbsInputChannel>() { new TLInputChannel() { ChannelId = channel.Id, AccessHash = (long)channel.AccessHash } };
                        var req2 = new TLRequestInviteToChannel()
                        {

                            Channel = target_channels[0],
                            Users = to_added_users
                        };

                        try
                        {
                            var update = await client.SendRequestAsync<TLUpdates>(req2);
                            added_users.Add(imported_users[imported_users.Count - 1]);

                            response.message = "  کاربر با موفقیت به چت افزوده شد " + c.Phone;
                            response.data = new List<TLUser>();
                            on_contact_added_handler(response, new EventArgs());
                        }
                        catch (Exception e)
                        {
                            Log.e("add to channel failed.phone = " + c.Phone + " . error=" + e.ToString(), "Contact_telegram", "addNumberToChannel");

                            response.message = "   دسترسی به افزودن این کاربر به چت وجود ندارد" + c.Phone;
                            response.data = new List<TLUser>();
                            on_contact_added_handler(response, new EventArgs());
                        }



                        await Task.Delay(G.telegram.getDelayTime() / 3);
                    }




                   

                }
                catch (Exception e)
                {
                    Log.e("add to contact failed.phone = " + c.Phone + " . error=" + e.ToString(), "Contact_telegram", "addNumberToChannel");
                    response.message = "  کاربر در تلگرام حساب ندارد " + c.Phone;
                    response.data = new List<TLUser>();
                    on_contact_added_handler(response, new EventArgs());
                }


                await Task.Delay(500);
                //client.Dispose();


            }


            response.status = 1;
            response.message = " تعداد کل افراد افزوده شده به چت : " + added_users.Count.ToString();
            response.data = new List<TLUser>();

            task_finish_handler(response, new EventArgs());
        }
















        private TelegramClient getTelegramClient(int num)
        {
            int index = num % G.telegram.mobiles.Count;

            //try one
            try
            {
                client = new TelegramClient(G.telegram.api_id, G.telegram.api_hash, null, "s_" + G.telegram.mobiles[index]);
                //await client.ConnectAsync();
            }
            catch (MissingApiConfigurationException ex)
            {
                //FailedDialog _dialog = new FailedDialog("مقادیر api_id یا  api_hash را با دقت وارد کنید");
                //_dialog.ShowDialog();
                Log.e("telegram connection failed.error=" + ex.ToString(), "Contact_telegram", "getTelegramClient");
                client = null;
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                //FailedDialog _dialog = new FailedDialog("خطا در ارتباط با سرور تلگرام");
                //_dialog.ShowDialog();
                Log.e("telegram connection failed.error=" + ex.ToString(), "Contact_telegram", "getTelegramClient");
                client = null;
            }
            catch (Exception e)
            {
                Log.e("telegram connection failed.error=" + e.ToString(), "Contact_telegram", "getTelegramClient");
                client = null;
            }


            //try two
            num++;
            index = num % G.telegram.mobiles.Count;

            if (client == null)
            {
                try
                {
                    client = new TelegramClient(G.telegram.api_id, G.telegram.api_hash, null, "s_" + G.telegram.mobiles[index]);
                    //await client.ConnectAsync();
                }
                catch (MissingApiConfigurationException ex)
                {
                    //FailedDialog _dialog = new FailedDialog("مقادیر api_id یا  api_hash را با دقت وارد کنید");
                    //_dialog.ShowDialog();
                    Log.e("telegram connection failed.error=" + ex.ToString(), "Contact_telegram", "getTelegramClient");
                    client = null;
                }
                catch (System.Net.Sockets.SocketException ex)
                {
                    //FailedDialog _dialog = new FailedDialog("خطا در ارتباط با سرور تلگرام");
                    //_dialog.ShowDialog();
                    Log.e("telegram connection failed.error=" + ex.ToString(), "Contact_telegram", "getTelegramClient");
                    client = null;
                }
                catch (Exception e)
                {
                    Log.e("telegram connection failed.error=" + e.ToString(), "Contact_telegram", "getTelegramClient");
                    client = null;
                }
            }






            //try three
            num++;
            index = num % G.telegram.mobiles.Count;
            if (client == null)
            {
                try
                {
                    client = new TelegramClient(G.telegram.api_id, G.telegram.api_hash, null, "s_" + G.telegram.mobiles[index]);
                    //await client.ConnectAsync();
                }
                catch (MissingApiConfigurationException ex)
                {
                    //FailedDialog _dialog = new FailedDialog("مقادیر api_id یا  api_hash را با دقت وارد کنید");
                    //_dialog.ShowDialog();
                    Log.e("telegram connection failed.error=" + ex.ToString(), "Contact_telegram", "getTelegramClient");
                    client = null;
                }
                catch (System.Net.Sockets.SocketException ex)
                {
                    //FailedDialog _dialog = new FailedDialog("خطا در ارتباط با سرور تلگرام");
                    //_dialog.ShowDialog();
                    Log.e("telegram connection failed.error=" + ex.ToString(), "Contact_telegram", "getTelegramClient");
                    client = null;
                }
                catch (Exception e)
                {
                    Log.e("telegram connection failed.error=" + e.ToString(), "Contact_telegram", "getTelegramClient");
                    client = null;
                }
            }


            //try four
            num++;
            index = num % G.telegram.mobiles.Count;
            if (client == null)
            {
                try
                {
                    client = new TelegramClient(G.telegram.api_id, G.telegram.api_hash, null, "s_" + G.telegram.mobiles[index]);
                    //await client.ConnectAsync();
                }
                catch (MissingApiConfigurationException ex)
                {
                    //FailedDialog _dialog = new FailedDialog("مقادیر api_id یا  api_hash را با دقت وارد کنید");
                    //_dialog.ShowDialog();
                    Log.e("telegram connection failed.error=" + ex.ToString(), "Contact_telegram", "getTelegramClient");
                    client = null;
                }
                catch (System.Net.Sockets.SocketException ex)
                {
                    //FailedDialog _dialog = new FailedDialog("خطا در ارتباط با سرور تلگرام");
                    //_dialog.ShowDialog();
                    Log.e("telegram connection failed.error=" + ex.ToString(), "Contact_telegram", "getTelegramClient");
                    client = null;
                }
                catch (Exception e)
                {
                    Log.e("telegram connection failed.error=" + e.ToString(), "Contact_telegram", "getTelegramClient");
                    client = null;
                }
            }



            //try five
            num++;
            index = num % G.telegram.mobiles.Count;
            if (client == null)
            {
                try
                {
                    client = new TelegramClient(G.telegram.api_id, G.telegram.api_hash, null, "s_" + G.telegram.mobiles[index]);
                    //await client.ConnectAsync();
                }
                catch (MissingApiConfigurationException ex)
                {
                    //FailedDialog _dialog = new FailedDialog("مقادیر api_id یا  api_hash را با دقت وارد کنید");
                    //_dialog.ShowDialog();
                    Log.e("telegram connection failed.error=" + ex.ToString(), "Contact_telegram", "getTelegramClient");
                    client = null;
                }
                catch (System.Net.Sockets.SocketException ex)
                {
                    //FailedDialog _dialog = new FailedDialog("خطا در ارتباط با سرور تلگرام");
                    //_dialog.ShowDialog();
                    Log.e("telegram connection failed.error=" + ex.ToString(), "Contact_telegram", "getTelegramClient");
                    client = null;
                }
                catch (Exception e)
                {
                    Log.e("telegram connection failed.error=" + e.ToString(), "Contact_telegram", "getTelegramClient");
                    client = null;
                }
            }


            //try six
            num++;
            index = num % G.telegram.mobiles.Count;
            if (client == null)
            {
                try
                {
                    client = new TelegramClient(G.telegram.api_id, G.telegram.api_hash, null, "s_" + G.telegram.mobiles[index]);
                    //await client.ConnectAsync();
                }
                catch (MissingApiConfigurationException ex)
                {
                    //FailedDialog _dialog = new FailedDialog("مقادیر api_id یا  api_hash را با دقت وارد کنید");
                    //_dialog.ShowDialog();
                    Log.e("telegram connection failed.error=" + ex.ToString(), "Contact_telegram", "getTelegramClient");
                    client = null;
                }
                catch (System.Net.Sockets.SocketException ex)
                {
                    //FailedDialog _dialog = new FailedDialog("خطا در ارتباط با سرور تلگرام");
                    //_dialog.ShowDialog();
                    Log.e("telegram connection failed.error=" + ex.ToString(), "Contact_telegram", "getTelegramClient");
                    client = null;
                }
                catch (Exception e)
                {
                    Log.e("telegram connection failed.error=" + e.ToString(), "Contact_telegram", "getTelegramClient");
                    client = null;
                }
            }










            return client;
        }


    }
}
