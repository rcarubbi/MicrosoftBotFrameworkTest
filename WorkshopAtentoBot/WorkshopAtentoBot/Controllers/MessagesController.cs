using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WorkshopAtentoBot.Dialogs;
using WorkshopAtentoBot.Services;

namespace WorkshopAtentoBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
         
       
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            try
            {
                if (activity.Type == ActivityTypes.Message)
                {

                    StateClient stateClient = activity.GetStateClient();
               
                    BotData userData = await stateClient.BotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
                    

                    ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                  
                    if (activity.Attachments != null && activity.Attachments.Count > 0)
                    {           
                        userData.SetProperty<bool>("spokenAnswer", true);
                        activity.Text = await ReconhecimentoService.ReconhecerFala(activity.Attachments[0].ContentUrl);
                        Activity reply = activity.CreateReply($"Você disse: {activity.Text}");
                        await connector.Conversations.ReplyToActivityAsync(reply);
                    }
                    else
                    {
                        userData.SetProperty<bool>("spokenAnswer", false);
                    }
                    await stateClient.BotState.SetUserDataAsync(activity.ChannelId, activity.From.Id, userData);
                    stateClient.Dispose();
                    stateClient = null;


                    await Conversation.SendAsync(activity, MakeLuisDialog);
                }
                else
                {
                    HandleSystemMessage(activity);
                }
              
            }
            catch (Exception ex)
            {
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                Activity reply = activity.CreateReply(ex.Message);
                await connector.Conversations.ReplyToActivityAsync(reply);

             
                    Activity reply2 = activity.CreateReply(ex.StackTrace);
                    await connector.Conversations.ReplyToActivityAsync(reply2);
               
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private IDialog<object> MakeLuisDialog()
        {
            return Chain.From(() => new LUISDialog());
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}