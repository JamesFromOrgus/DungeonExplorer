using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    /// <summary>
    /// Similar to room, this is a node of a graph data-structure, allowing user to navigate between different
    /// dialogue messages.
    /// </summary>
    public class DialogueNode
    {
        private string _message;
        private List<string> _responses = new List<string>();
        private Dictionary<string, DialogueNode> _responsePointers = new Dictionary<string, DialogueNode>();

        /// <summary>
        /// Message with no speaker.
        /// </summary>
        public DialogueNode(string message)
        {
            _message = message;
        }

        /// <summary>
        /// Display the message with the speaker notated, and with speech marks to indicate verbally-spoken dialogue.
        /// </summary>
        public DialogueNode(string speaker, string message)
        {
            _message = $"[{speaker}]:\n\"{message}\"";
        }
        
        /// <summary>
        /// Add a response that will end dialogue.
        /// </summary>
        public void AddChoice(string message)
        {
            _responses.Add(message);
        }

        /// <summary>
        /// Add a response that will navigate to another dialogue message.
        /// </summary>
        public void AddChoice(string message, DialogueNode nextNode)
        {
            AddChoice(message);
            _responsePointers[message] = nextNode;
        }
        
        /// <summary>
        /// Same as above with speech marks automatically added to indicate verbal response.
        /// </summary>
        public void AddResponse(string message)
        {
            message = $"\"{message}\"";
            AddChoice(message);
        }
        /// <summary>
        /// Same as above with speech marks automatically added to indicate verbal response.
        /// </summary>
        public void AddResponse(string message, DialogueNode nextNode)
        {
            message = $"\"{message}\"";
            AddChoice(message, nextNode);
        }
    
        /// <summary>
        /// Display message and give user option to respond.
        /// </summary>
        public void Display()
        {
            Menu responseMenu;
            // FIX FOR HARRY'S BUG
            if (_responses.Count == 0)
            {
                responseMenu = new Menu(_message, new[] { new Choice("Exit", () => {})});
                responseMenu.Open();
                return;
            }
            Choice[] responseChoices = new Choice[_responses.Count];
            for (int i = 0; i < responseChoices.Length; i++)
            {
                string response = _responses[i];
                responseChoices[i] = new Choice(response, () =>
                {
                    if (_responsePointers.ContainsKey(response))
                    {
                        _responsePointers[response].Display();
                    }
                });
            }
            responseMenu = new Menu(_message, responseChoices);
            responseMenu.Open();
        }
    }
}
