using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    class DialogueNode
    {
        private string _message;
        private List<string> _responses = new List<string>();
        private Dictionary<string, DialogueNode> _responsePointers = new Dictionary<string, DialogueNode>();

        public DialogueNode(string message)
        {
            _message = message;
        }

        public void AddChoice(string message)
        {
            _responses.Add(message);
        }

        public void AddChoice(string message, DialogueNode nextNode)
        {
            AddChoice(message);
            _responsePointers[message] = nextNode;
        }

        public void AddResponse(string message)
        {
            message = $"\"{message}\"";
            AddChoice(message);
        }

        public void AddResponse(string message, DialogueNode nextNode)
        {
            message = $"\"{message}\"";
            AddChoice(message, nextNode);
        }

        public void Display()
        {
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
            Menu responseMenu = new Menu(_message, responseChoices);
            responseMenu.Open();
        }
    }
}
