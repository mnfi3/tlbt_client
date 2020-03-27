using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using TeleSharp.TL;
using TeleSharp.TL.Messages;
using TLSharp.Core;
using TLSharp.Core.Exceptions;
using TLSharp.Core.Network;
using TLSharp.Core.Network.Exceptions;
using TLSharp.Core.Utils;
using System.Linq.Expressions;
using TeleSharp.TL.Account;
using TeleSharp.TL.Channels;
using System.Windows;

namespace Telbot
{
    class TeleSharpcs
    {
        private string NumberToSendMessage { get; set; }

        private string NumberToAuthenticate { get; set; }

        private string CodeToAuthenticate { get; set; }

        private string PasswordToAuthenticate { get; set; }

        private string NotRegisteredNumberToSignUp { get; set; }

        private string UserNameToSendMessage { get; set; }

        private string NumberToGetUserFull { get; set; }

        private string NumberToAddToChat { get; set; }

        private string ApiHash { get; set; }

        private int ApiId { get; set; }

        class Assert
        {
            static internal void IsNotNull(object obj)
            {
                IsNotNullHanlder(obj);
            }

            static internal void IsTrue(bool cond)
            {
                IsTrueHandler(cond);
            }
        }

        internal static Action<object> IsNotNullHanlder;
        internal static Action<bool> IsTrueHandler;

        public void Init(Action<object> notNullHandler, Action<bool> trueHandler)
        {
            IsNotNullHanlder = notNullHandler;
            IsTrueHandler = trueHandler;

            // Setup your API settings and phone numbers in app.config
            GatherTestConfiguration();
        }



        public static class MemberInfoGetting
        {
            public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
            {
                MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
                return expressionBody.Member.Name;
            }
        }
        private TelegramClient NewClient()
        {
            try
            {
                return new TelegramClient(ApiId, ApiHash);
            }
            catch (MissingApiConfigurationException ex)
            {
                throw new Exception("Please add your API settings to the `app.config` file. (More info: {MissingApiConfigurationException.InfoUrl})",
                                    ex);
            }
        }

        private void GatherTestConfiguration()
        {
            string appConfigMsgWarning = "{0} not configured in app.config! Some tests may fail.";

            ApiHash = ConfigurationManager.AppSettings[MemberInfoGetting.GetMemberName(() => ApiHash)];
            if (string.IsNullOrEmpty(ApiHash))
                Debug.WriteLine(appConfigMsgWarning, MemberInfoGetting.GetMemberName(() => ApiHash));

            var apiId = ConfigurationManager.AppSettings[MemberInfoGetting.GetMemberName(() => ApiId)];
            if (string.IsNullOrEmpty(apiId))
                Debug.WriteLine(appConfigMsgWarning, MemberInfoGetting.GetMemberName(() => ApiId));
            else
                ApiId = int.Parse(apiId);

            NumberToAuthenticate = ConfigurationManager.AppSettings[MemberInfoGetting.GetMemberName(() => NumberToAuthenticate)];
            if (string.IsNullOrEmpty(NumberToAuthenticate))
                Debug.WriteLine(appConfigMsgWarning, MemberInfoGetting.GetMemberName(() => NumberToAuthenticate));

            CodeToAuthenticate = ConfigurationManager.AppSettings[MemberInfoGetting.GetMemberName(() => CodeToAuthenticate)];
            if (string.IsNullOrEmpty(CodeToAuthenticate))
                Debug.WriteLine(appConfigMsgWarning, MemberInfoGetting.GetMemberName(() => CodeToAuthenticate));

            PasswordToAuthenticate = ConfigurationManager.AppSettings[MemberInfoGetting.GetMemberName(() => PasswordToAuthenticate)];
            if (string.IsNullOrEmpty(PasswordToAuthenticate))
                Debug.WriteLine(appConfigMsgWarning, MemberInfoGetting.GetMemberName(() => PasswordToAuthenticate));

            NotRegisteredNumberToSignUp = ConfigurationManager.AppSettings[MemberInfoGetting.GetMemberName(() => NotRegisteredNumberToSignUp)];
            if (string.IsNullOrEmpty(NotRegisteredNumberToSignUp))
                Debug.WriteLine(appConfigMsgWarning, MemberInfoGetting.GetMemberName(() => NotRegisteredNumberToSignUp));

            UserNameToSendMessage = ConfigurationManager.AppSettings[MemberInfoGetting.GetMemberName(() => UserNameToSendMessage)];
            if (string.IsNullOrEmpty(UserNameToSendMessage))
                Debug.WriteLine(appConfigMsgWarning, MemberInfoGetting.GetMemberName(() => UserNameToSendMessage));

            NumberToGetUserFull = ConfigurationManager.AppSettings[MemberInfoGetting.GetMemberName(() => NumberToGetUserFull)];
            if (string.IsNullOrEmpty(NumberToGetUserFull))
                Debug.WriteLine(appConfigMsgWarning, MemberInfoGetting.GetMemberName(() => NumberToGetUserFull));

            NumberToAddToChat = ConfigurationManager.AppSettings[MemberInfoGetting.GetMemberName(() => NumberToAddToChat)];
            if (string.IsNullOrEmpty(NumberToAddToChat))
                Debug.WriteLine(appConfigMsgWarning, MemberInfoGetting.GetMemberName(() => NumberToAddToChat));
        }
        public virtual async Task AuthUser()
        {
            //MessageBox.Show("Hello, world!");
            var client = NewClient();

            await client.ConnectAsync();
            
            
           var hash = await client.SendCodeRequestAsync(NumberToAuthenticate);
            

            int a = 9;
            var code = CodeToAuthenticate; // you can change code in debugger too

            if (String.IsNullOrWhiteSpace(code))
            {
                throw new Exception("CodeToAuthenticate is empty in the app.config file, fill it with the code you just got now by SMS/Telegram");
            }

            TLUser user = null;
            try
            {
                user = await client.MakeAuthAsync(NumberToAuthenticate, hash, code);
            }
            catch (CloudPasswordNeededException ex)
            {
                //var passwordSetting = await client.GetPasswordSetting();
                //var password = PasswordToAuthenticate;

                //user = await client.MakeAuthWithPasswordAsync(passwordSetting, password);
            }
            catch (InvalidPhoneCodeException ex)
            {
                throw new Exception("CodeToAuthenticate is wrong in the app.config file, fill it with the code you just got now by SMS/Telegram",
                                    ex);
            }
            Assert.IsNotNull(user);
            Assert.IsTrue(client.IsUserAuthorized());
        }

        public virtual async Task SendMessageTest()
        {
            NumberToSendMessage = ConfigurationManager.AppSettings[MemberInfoGetting.GetMemberName(() => NumberToSendMessage)];
            if (string.IsNullOrWhiteSpace(NumberToSendMessage))

                throw new Exception("Please fill the" + MemberInfoGetting.GetMemberName(() => NumberToSendMessage) + " setting in app.config file first");

            // this is because the contacts in the address come without the "+" prefix
            var normalizedNumber = NumberToSendMessage.StartsWith("+") ?
                NumberToSendMessage.Substring(1, NumberToSendMessage.Length - 1) :
                NumberToSendMessage;

            var client = NewClient();

            await client.ConnectAsync();

            var result = await client.GetContactsAsync();


            var user = result.Users
                .OfType<TLUser>()
                .FirstOrDefault(x => x.Phone == normalizedNumber);

            if (user == null)
            {
                throw new System.Exception("Number was not found in Contacts List of user: " + NumberToSendMessage);
            }

            await client.SendTypingAsync(new TLInputPeerUser() { UserId = user.Id });
            Thread.Sleep(3000);
            await client.SendMessageAsync(new TLInputPeerUser() { UserId = user.Id }, "TEST");
        }
        public virtual async Task addMemmberTest()
        {
            NumberToSendMessage = ConfigurationManager.AppSettings[MemberInfoGetting.GetMemberName(() => NumberToSendMessage)];
            if (string.IsNullOrWhiteSpace(NumberToSendMessage))

                throw new Exception("Please fill the" + MemberInfoGetting.GetMemberName(() => NumberToSendMessage) + " setting in app.config file first");

            // this is because the contacts in the address come without the "+" prefix
            var normalizedNumber = NumberToSendMessage.StartsWith("+") ?
                NumberToSendMessage.Substring(1, NumberToSendMessage.Length - 1) :
                NumberToSendMessage;

            var client = NewClient();

            await client.ConnectAsync();

            var result = await client.GetContactsAsync();


            var user = result.Users
                .OfType<TLUser>()
                .FirstOrDefault(x => x.Phone == normalizedNumber);

            if (user == null)
            {
                throw new System.Exception("Number was not found in Contacts List of user: " + NumberToSendMessage);
            }

            await client.ConnectAsync();

            var dialogs = (TLDialogs)await client.GetUserDialogsAsync();
            var chat = dialogs.Chats
                .OfType<TLChannel>()
                .FirstOrDefault(c => c.Title == "khune");
            int b = 5;

            TLInputUser u = new TLInputUser() { UserId = user.Id, AccessHash  = (long) user.AccessHash };
            int a = 5;
            await client.ConnectAsync();

            //var req = new TLRequestAddChatUser()
            //{
            //    ChatId = chat.Id,
            //    UserId = u,
            //    FwdLimit = 100
            //};
            TLVector<TLAbsInputUser> u2 = new TLVector<TLAbsInputUser>();
            TLVector<TLAbsInputChannel> c2 = new TLVector<TLAbsInputChannel>();
            u2.Add(u);
            c2.Add(new TLInputChannel() { ChannelId = chat.Id,AccessHash = (long)chat.AccessHash });
            var req2 = new TLRequestInviteToChannel()
            {

                //Channel = new TLInputChannel() { ChannelId = chat.Id },
                Channel = c2[0],
                Users = u2
            };
            await client.ConnectAsync();
            try
            {
                var update = await client.SendRequestAsync<TLUpdates>(req2);
            }

            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
            }
            int a2 = 5;


        }
        //public async Task<TLUpdates> InviteUserToChannel(TLAbsInputUser user, TLInputChannel channelid)
        //{
        //    TLVector<TLAbsInputUser> u = new TLVector<TLAbsInputUser>();
        //    u.lists.Add(user);
        //    var req = new TLRequestInviteToChannel()
        //    {
        //        channel = channelid,
        //        users = u
        //    };
        //    var update = await client.SendRequestAsync<TLUpdates>(req);
        //    return update;
        //}

        public virtual async Task SendMessageToChannelTest()
        {
            var client = NewClient();

            await client.ConnectAsync();

            var dialogs = (TLDialogs)await client.GetUserDialogsAsync();
            var chat = dialogs.Chats
                .OfType<TLChannel>()
                .FirstOrDefault(c => c.Title == "TestGroup");

            await client.SendMessageAsync(new TLInputPeerChannel() { ChannelId = chat.Id, AccessHash = chat.AccessHash.Value }, "TEST MSG");
        }

        public virtual async Task SendPhotoToContactTest()
        {
            var client = NewClient();

            await client.ConnectAsync();

            var result = await client.GetContactsAsync();

            var user = result.Users
                .OfType<TLUser>()
                .FirstOrDefault(x => x.Phone == NumberToSendMessage);

            var fileResult = (TLInputFile)await client.UploadFile("cat.jpg", new StreamReader("data/cat.jpg"));
            await client.SendUploadedPhoto(new TLInputPeerUser() { UserId = user.Id }, fileResult, "kitty");
        }

        public virtual async Task SendBigFileToContactTest()
        {
            var client = NewClient();

            await client.ConnectAsync();

            var result = await client.GetContactsAsync();

            var user = result.Users
                .OfType<TLUser>()
                .FirstOrDefault(x => x.Phone == NumberToSendMessage);

            var fileResult = (TLInputFileBig)await client.UploadFile("some.zip", new StreamReader("<some big file path>"));

            await client.SendUploadedDocument(
                new TLInputPeerUser() { UserId = user.Id },
                fileResult,
                "some zips",
                "application/zip",
                new TLVector<TLAbsDocumentAttribute>());
        }

        public virtual async Task DownloadFileFromContactTest()
        {
            var client = NewClient();

            await client.ConnectAsync();

            var result = await client.GetContactsAsync();

            var user = result.Users
                .OfType<TLUser>()
                .FirstOrDefault(x => x.Phone == NumberToSendMessage);

            var inputPeer = new TLInputPeerUser() { UserId = user.Id };
            var res = await client.SendRequestAsync<TLMessagesSlice>(new TLRequestGetHistory() { Peer = inputPeer });
            var document = res.Messages
                .OfType<TLMessage>()
                .Where(m => m.Media != null)
                .Select(m => m.Media)
                .OfType<TLMessageMediaDocument>()
                .Select(md => md.Document)
                .OfType<TLDocument>()
                .First();

            var resFile = await client.GetFile(
                new TLInputDocumentFileLocation()
                {
                    AccessHash = document.AccessHash,
                    Id = document.Id,
                    Version = document.Version
                },
                document.Size);

            Assert.IsTrue(resFile.Bytes.Length > 0);
        }

        public virtual async Task DownloadFileFromWrongLocationTest()
        {
            var client = NewClient();

            await client.ConnectAsync();

            var result = await client.GetContactsAsync();

            var user = result.Users
                .OfType<TLUser>()
                .FirstOrDefault(x => x.Id == 5880094);

            var photo = ((TLUserProfilePhoto)user.Photo);
            var photoLocation = (TLFileLocation)photo.PhotoBig;

            var resFile = await client.GetFile(new TLInputFileLocation()
            {
                LocalId = photoLocation.LocalId,
                Secret = photoLocation.Secret,
                VolumeId = photoLocation.VolumeId
            }, 1024);

            var res = await client.GetUserDialogsAsync();

            Assert.IsTrue(resFile.Bytes.Length > 0);
        }

        public virtual async Task SignUpNewUser()
        {
            var client = NewClient();
            await client.ConnectAsync();

            var hash = await client.SendCodeRequestAsync(NotRegisteredNumberToSignUp);
            var code = "";

            var registeredUser = await client.SignUpAsync(NotRegisteredNumberToSignUp, hash, code, "TLSharp", "User");
            Assert.IsNotNull(registeredUser);
            Assert.IsTrue(client.IsUserAuthorized());

            var loggedInUser = await client.MakeAuthAsync(NotRegisteredNumberToSignUp, hash, code);
            Assert.IsNotNull(loggedInUser);
        }

        public virtual async Task CheckPhones()
        {
            var client = NewClient();
            await client.ConnectAsync();

            var result = await client.IsPhoneRegisteredAsync(NumberToAuthenticate);
            Assert.IsTrue(result);
        }

        public virtual async Task FloodExceptionShouldNotCauseCannotReadPackageLengthError()
        {
            for (int i = 0; i < 50; i++)
            {
                try
                {
                    await CheckPhones();
                }
                catch (FloodException floodException)
                {
                    Console.WriteLine("FLOODEXCEPTION: " + floodException);
                    Thread.Sleep(floodException.TimeToWait);
                }
            }
        }

        public virtual async Task SendMessageByUserNameTest()
        {
            UserNameToSendMessage = ConfigurationManager.AppSettings[MemberInfoGetting.GetMemberName(() => UserNameToSendMessage)];
            if (string.IsNullOrWhiteSpace(UserNameToSendMessage))
                throw new Exception("Please fill the" + MemberInfoGetting.GetMemberName(() => UserNameToSendMessage) + " setting in app.config file first");

            var client = NewClient();

            await client.ConnectAsync();

            var result = await client.SearchUserAsync(UserNameToSendMessage);

            var user = result.Users
                .Where(x => x.GetType() == typeof(TLUser))
                .OfType<TLUser>()
                .FirstOrDefault(x => x.Username == UserNameToSendMessage.TrimStart('@'));

            if (user == null)
            {
                var contacts = await client.GetContactsAsync();

                user = contacts.Users
                    .Where(x => x.GetType() == typeof(TLUser))
                    .OfType<TLUser>()
                    .FirstOrDefault(x => x.Username == UserNameToSendMessage.TrimStart('@'));
            }

            if (user == null)
            {
                throw new System.Exception("Username was not found: " + UserNameToSendMessage);
            }

            await client.SendTypingAsync(new TLInputPeerUser() { UserId = user.Id });
            Thread.Sleep(3000);
            await client.SendMessageAsync(new TLInputPeerUser() { UserId = user.Id }, "TEST");
        }
    }
}
