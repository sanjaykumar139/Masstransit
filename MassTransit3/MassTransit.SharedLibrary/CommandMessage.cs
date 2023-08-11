namespace MassTransit.SharedLibrary
{
    public class CommandMessage
    {
        private object id;
        private object message;

        public CommandMessage(object id, object message)
        {
            this.id = id;
            this.message = message;
        }
    }
}