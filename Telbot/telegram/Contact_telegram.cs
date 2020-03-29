using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        //}


        public  async void getContacts(EventHandler handler)
        {

            try
            {
                client = new TelegramClient(G.telegram.api_id, G.telegram.api_hash);
                await client.ConnectAsync();
            }
            catch (Exception e)
            {
                Log.e("telegram connection failed.error=" + e.ToString(), "Contact_telegram", "getContacts");
            }



            if (client == null)
            {
                response.status = 0;
                response.message = "عملیات ارسال کد شکست خورد.لطفا دوباره امتحان کنید";
                response.data = "";
                Log.e("get telegram channel failed", "Channel_telegram", "getChannels");
                handler(response, new EventArgs());
                return;
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
                if (!client.IsConnected)
                    await client.ConnectAsync();

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


        public  async void addNumberToChannel(EventHandler handler, TLVector<TLInputPhoneContact> contacts, TLChannel channel)
        {
            try
            {
                client = new TelegramClient(G.telegram.api_id, G.telegram.api_hash);
                await client.ConnectAsync();
            }
            catch (Exception e)
            {
                Log.e("telegram connection failed.error=" + e.ToString(), "Contact_telegram", "addNumberToChannel");
            }


            if (client == null)
            {
                response.status = 0;
                response.message = "عملیات شکست خورد.لطفا دوباره امتحان کنید";
                response.data = new List<TLUser>();
                Log.e("add numbers to channel failed", "Contact_telegram", "addNumberToChannel");
                handler(response, new EventArgs());
                return;
            }

            //----------------------------------check phone is user?----------------------------------------
            //foreach (TLInputPhoneContact c in contacts)
            //{
            //    try
            //    {
            //        //TLreq
            //        //var result = await client.ImportContactsAsync(c.Phone);
            //        contacts.Remove(c);
            //    }
            //    catch (Exception e)
            //    {

            //    }
            //}

           


            //-----------------------------------add to contacts-----------------------------------------------
            TLImportedContacts importedContacts = null;
            try
            {
                //if (!client.IsConnected)
                await client.ConnectAsync();

               
                //foreach (TLInputPhoneContact c in contacts)
                //{
                    //importedContacts = await client.SendRequestAsync<TLImportedContacts>(new TLRequestImportContacts
                    //{
                    //    Contacts = new TLVector<TLInputPhoneContact> { new TLInputPhoneContact{FirstName = c.FirstName, LastName = c.LastName, Phone = c.Phone}}
                    //});

                    importedContacts = await client.SendRequestAsync<TLImportedContacts>(new TLRequestImportContacts
                    {
                        Contacts = contacts
                    });

                    importedContacts.ToString();
                    //await Task.Delay(3000);
                //}

            }
            catch (Exception e)
            {
                //importedContacts.ToString();
                response.status = 0;
                response.message = "عملیات  شکست خورد.لطفا دوباره امتحان کنید";
                response.data = new List<TLUser>();
                Log.e("add numbers to telegram contacts failed.error=" + e.ToString(), "Contact_telegram", "addNumberToChannel");
            }

            //------------------------------------get new users from contacts------------------------------------------------

           
            try
            {
                //client.Dispose();

                //if (!client.IsConnected)
                await client.ConnectAsync();

                var result = await client.GetContactsAsync();
                List<TLUser> all_users = result.Users.OfType<TLUser>().ToList<TLUser>();

                List<TLUser> new_users = new List<TLUser>();

                foreach (TLUser u in all_users)
                {
                    foreach (TLInputPhoneContact c in contacts)
                    {
                        if (c.Phone == u.Phone)
                        {
                            new_users.Add(u);
                        }
                    }
                }


                //------------------------------------add to searched users------------------------------------------------
                try
                {
                    await client.ConnectAsync();
                    var req1 = new TeleSharp.TL.Contacts.TLRequestSearch() { Q = "Laadrii", Limit = 1 };
                    var update1 = await client.SendRequestAsync<TeleSharp.TL.Contacts.TLFound>(req1);
                    update1.ToString();
                    List<TLUser>searched_users = update1.Users.OfType<TLUser>().ToList<TLUser>();

                    TLInputUser new_user;
                    TLVector<TLAbsInputUser> to_added_users = new TLVector<TLAbsInputUser>();
                    TLVector<TLAbsInputChannel> target_channel = new TLVector<TLAbsInputChannel>();
                    target_channel.Add(new TLInputChannel() { ChannelId = channel.Id, AccessHash = (long)channel.AccessHash });

                    foreach (TLUser user in searched_users)
                    {
                        new_user = new TLInputUser() { UserId = user.Id, AccessHash = (long)user.AccessHash};
                        to_added_users.Add(new_user);
                    }
                    var req = new TLRequestInviteToChannel()
                    {

                        Channel = target_channel[0],
                        Users = to_added_users
                    };

                    //await client.ConnectAsync();
                    var update = await client.SendRequestAsync<TLUpdates>(req);

                    List<TLUser> added_users = new List<TLUser>();
                }
                catch(Exception e)
                {
                    e.ToString();
                }


                //------------------------------------add to channel in one step------------------------------------------------

                //if (!client.IsConnected) 
                //await client.ConnectAsync();


                //TLInputUser new_user;
                //TLVector<TLAbsInputUser> to_added_users = new TLVector<TLAbsInputUser>();
                //TLVector<TLAbsInputChannel> target_channel = new TLVector<TLAbsInputChannel>();
                //target_channel.Add(new TLInputChannel() { ChannelId = channel.Id, AccessHash = (long)channel.AccessHash });
                //foreach (TLUser user in new_users)
                //{
                //    new_user = new TLInputUser() { UserId = user.Id, AccessHash = (long)user.AccessHash };
                //    to_added_users.Add(new_user);
                //}
                
                //var req = new TLRequestInviteToChannel()
                //{

                //    Channel = target_channel[0],
                //    Users = to_added_users
                //};

                //await client.ConnectAsync();
                //var update = await client.SendRequestAsync<TLUpdates>(req);

                response.status = 1;
                response.message = "عملیات با موفقیت انجام شد";
                response.data = new List<TLUser>();

            }
            catch (Exception e)
            {
                response.status = 0;
                response.message = "عملیات  شکست خورد.لطفا دوباره امتحان کنید";
                response.data = new List<TLUser>();
                Log.e("add contacts to channel failed.error=" + e.ToString(), "Contact_telegram", "addNumberToChannel");
            }




            //------------------------------------add to channel one by one with delay------------------------------------------------

            //await client.ConnectAsync();
            //foreach (TLUser user in new_users)
            //{


            //    if (!client.IsConnected) await client.ConnectAsync();

            //    TLInputUser u = new TLInputUser() { UserId = user.Id, AccessHash = (long)user.AccessHash };
            //    TLVector<TLAbsInputUser> u2 = new TLVector<TLAbsInputUser>();
            //    TLVector<TLAbsInputChannel> c2 = new TLVector<TLAbsInputChannel>();
            //    u2.Add(u);
            //    c2.Add(new TLInputChannel() { ChannelId = channel.Id, AccessHash = (long)channel.AccessHash });
            //    var req2 = new TLRequestInviteToChannel()
            //    {

            //        Channel = c2[0],
            //        Users = u2
            //    };
            //    await client.ConnectAsync();

            //    try
            //    {
            //        var update = await client.SendRequestAsync<TLUpdates>(req2);
            //        added_users.Add(user);
            //    }
            //    catch (Exception e)
            //    {
            //        e.ToString();
            //    }


            //    Random rand = new Random();
            //    int num = (int)rand.Next(3, 15);
            //    await Task.Delay(num * 1000);

            //}



            handler(response, new EventArgs());
        }

        
    }
}
